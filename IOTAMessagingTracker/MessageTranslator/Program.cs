using ClassLibrary;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Messaging;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Serialization;

namespace MessageTranslator
{
    class Program
    {
        static void Main(string[] args)
        {
            while(true)
            {
                MessageQueue msMq = msMq = new MessageQueue(MessageConstants.XmlMessageQueue);

                try
                {
                    msMq.Formatter = new XmlMessageFormatter(new Type[] { typeof(XmlDocument) });

                    var message = (XmlDocument)msMq.Receive().Body;

                    RateObject response;

                    XmlSerializer serializer = new XmlSerializer(typeof(RateObject));
                    using (XmlReader reader = new XmlNodeReader(message))
                    {
                        response = (RateObject)serializer.Deserialize(reader);
                    }
                    Console.WriteLine(response.PriceUSD);



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
            Console.ReadKey();

        }
    }
}
