using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Messaging;

namespace MessageQueueConsoleTest
{
    class Program
    {
        const string queueName = @".\private$\MessageQueueConsoleTest";
        static void Main(string[] args)
        {
            Program myProgram = new Program();
            myProgram.Run();
        }

        private void Run()
        {
            while(true)
            {
                Console.Clear();
                Console.WriteLine("1. Send message");
                Console.WriteLine("2. Receive message");
                Console.WriteLine("3. Exit");

                char input = Console.ReadKey().KeyChar;
                switch(input)
                {
                    case '1':
                        SendMessageToQueue();
                        break;
                    case '2':
                        ReceiveMessageFromQueue();
                        break;
                    case '3':
                        Environment.Exit(0);
                        break;
                    default:
                        break;
                }
            }
        }

        private static void SendMessageToQueue()
        {
            // check if queue exists, if not create it
            MessageQueue msMq = null;

            if (!MessageQueue.Exists(queueName))
                msMq = MessageQueue.Create(queueName);
            else
                msMq = new MessageQueue(queueName);

            try
            {
                // msMq.Send("Sending data to MSMQ at " + DateTime.Now.ToString());
                Person p = new Person()
                {
                    FirstName = "Andreas",
                    LastName = "Engedal"
                };

                msMq.Send(p);
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

            Console.WriteLine("\nMessage sent!");
            Console.ReadKey();
        }

        private static void ReceiveMessageFromQueue()
        {
            MessageQueue msMq = msMq = new MessageQueue(queueName);

            try
            {
                // msMq.Formatter = new XmlMessageFormatter(new Type[] {typeof(string)});
                msMq.Formatter = new XmlMessageFormatter(new Type[] { typeof(Person) });

                var message = (Person)msMq.Receive().Body;

                Console.WriteLine("FirstName: " + message.FirstName + ", LastName: " + message.LastName);

                // Console.WriteLine(message.Body.ToString());
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

            Console.WriteLine("\nMessage received!");
            Console.ReadKey();
        }
    }
}
