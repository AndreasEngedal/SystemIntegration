using System;
using System.IO;
using System.Messaging;
using System.Xml;
using System.Xml.Serialization;
using ClassLibrary;

namespace MessageTranslator
{
    class Program
    {
        static void Main(string[] args)
        {
			Console.Title = "MessageTranslator";
			while (true)
			{
				MessageQueue msMq = null;

				if (!MessageQueue.Exists(MessageConstants.XmlMessageQueue))
					msMq = MessageQueue.Create(MessageConstants.XmlMessageQueue);
				else
					msMq = new MessageQueue(MessageConstants.XmlMessageQueue);

				try
				{
					msMq.Formatter = new XmlMessageFormatter(new Type[] { typeof(XmlDocument) });

					XmlDocument xmlDoc = (XmlDocument)msMq.Receive().Body;
					//message.Save("test2.xml");

					using (TextReader sr = new StringReader(ConvertXmlDocumentToString(xmlDoc)))
					{
						var serializer = new XmlSerializer(typeof(RawRateObject));
						RawRateObject rawRateObj = (RawRateObject)serializer.Deserialize(sr);
						RateObject rateObj = ConvertRawRateObjectToRateObject(rawRateObj);
						SendObjectMessage(rateObj);
					}
				}
				catch (MessageQueueException ee)
				{
					Console.Write(ee.ToString());
				}
				catch (Exception eee)
				{
					Console.Write(eee.ToString());
				}
				finally
				{
					msMq.Close();
				}
			}
        }

		private static string ConvertXmlDocumentToString(XmlDocument doc)
		{
			using (var stringWriter = new StringWriter())
			using (var xmlTextWriter = XmlWriter.Create(stringWriter))
			{
				doc.WriteTo(xmlTextWriter);
				xmlTextWriter.Flush();
				return stringWriter.GetStringBuilder().ToString();
			}
		}

		private static void SendObjectMessage(RateObject rateObj)
		{
			MessageQueue msMq = null;

			if (!MessageQueue.Exists(MessageConstants.ObjectMessageQueue))
				msMq = MessageQueue.Create(MessageConstants.ObjectMessageQueue);
			else
				msMq = new MessageQueue(MessageConstants.ObjectMessageQueue);

			try
			{
				msMq.Send(rateObj);
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

		private static RateObject ConvertRawRateObjectToRateObject(RawRateObject obj)
		{
			RateObject rateObj = new RateObject
			{
				Id = obj.Id,
				Name = obj.Name,
				Rank = obj.Rank,
				Symbol = obj.Symbol,
				PriceUSD = obj.PriceUSD,
				PriceBTC = obj.PriceBTC,
				VolumeUSD24h = obj.VolumeUSD24h,
				MarketCapUSD = obj.MarketCapUSD,
				AvailableSupply = obj.AvailableSupply,
				TotalSupply = obj.TotalSupply,
				MaxSupply = obj.MaxSupply,
				PercentChange1h = obj.PercentChange1h,
				PercentChange24h = obj.PercentChange24h,
				PercentChange7d = obj.PercentChange7d,
				LastUpdated = obj.LastUpdated
			};
			return rateObj;
		}
	}
}
