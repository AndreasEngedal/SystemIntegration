using System;
using ClassLibrary;
using RestSharp;
using System.Xml;
using System.Messaging;
using Newtonsoft.Json.Linq;
using System.Xml.Serialization;
using System.IO;

namespace RateReceiverConsole
{
    class Program
    {
        private static string coinType = "iota";

        static void Main(string[] args)
        {
			Console.Title = "RateReceiverConsole";
            RestClient client = new RestClient("https://api.coinmarketcap.com/v1/ticker");
            while (true)
            {
                RestRequest request = new RestRequest(Method.GET);
                request.Resource = "/{coinType}/";
                request.AddParameter("coinType", coinType, ParameterType.UrlSegment);
                request.OnBeforeDeserialization = resp => { resp.ContentType = "application/json"; };

				client.ExecuteAsync(request, response => {
					JArray a = JArray.Parse(response.Content);
					RawRateObject obj = a[0].ToObject<RawRateObject>();
					XmlDocument xml = SerializeToXmlDocument(obj);
					SendXmlMessage(xml);
				});

				System.Threading.Thread.Sleep(5000);
            }
        }

		public static XmlDocument SerializeToXmlDocument(object input)
		{
			XmlSerializer ser = new XmlSerializer(input.GetType());

			XmlDocument xd = null;

			using (MemoryStream memStm = new MemoryStream())
			{
				ser.Serialize(memStm, input);

				memStm.Position = 0;

				XmlReaderSettings settings = new XmlReaderSettings();
				settings.IgnoreWhitespace = true;

				using (var xtr = XmlReader.Create(memStm, settings))
				{
					xd = new XmlDocument();
					xd.Load(xtr);
				}
			}
			xd.Save("test.xml");
			return xd;
		}

		private static void SendXmlMessage(XmlDocument doc)
        {
            MessageQueue msMq = null;

            if (!MessageQueue.Exists(MessageConstants.XmlMessageQueue))
                msMq = MessageQueue.Create(MessageConstants.XmlMessageQueue);
            else
                msMq = new MessageQueue(MessageConstants.XmlMessageQueue);

            try
            {
                msMq.Send(doc);
            }
            catch (MessageQueueException ee)
            {
                Console.WriteLine("Error: " + ee.ToString());
            }
            catch (Exception eee)
            {
                Console.WriteLine("Error: " + eee.ToString());
            }
            finally
            {
                msMq.Close();
            }

            Console.WriteLine("Message sent!");
        }
    }
}
