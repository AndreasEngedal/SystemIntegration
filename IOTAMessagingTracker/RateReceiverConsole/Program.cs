using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using ClassLibrary;
using RestSharp;
using Newtonsoft.Json;
using System.Xml;
using System.Messaging;

namespace RateReceiverConsole
{
    class Program
    {
        private static string coinType = "iota";

        static void Main(string[] args)
        {
            RestClient client = new RestClient("https://api.coinmarketcap.com/v1/ticker");
            while (true)
            {
                RestRequest request = new RestRequest(Method.GET);
                request.Resource = "/{coinType}/";
                request.AddParameter("coinType", coinType, ParameterType.UrlSegment);
                request.OnBeforeDeserialization = resp => { resp.ContentType = "application/json"; };


                client.ExecuteAsync(request, response => {
                    string xmlObj = "RateObject";
                    XmlDocument doc = (XmlDocument)JsonConvert.DeserializeXmlNode("{\"" + xmlObj + "\":" + response.Content + "}", "root");
                    
                    SendXmlMessage(doc);
                    doc.PreserveWhitespace = true;
                    doc.Save("test.xml");
                });

                System.Threading.Thread.Sleep(1000);
            }

            Console.ReadLine();
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
            Console.ReadKey();
        }
    }
}
