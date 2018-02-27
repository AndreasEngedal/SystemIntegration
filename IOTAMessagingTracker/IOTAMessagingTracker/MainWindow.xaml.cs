using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Messaging;
using System.Threading;
using System.ComponentModel;
using System.Collections.ObjectModel;
using ClassLibrary;

namespace IOTAMessagingTracker
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        ObservableCollection<RateObject> rateObjects;

        private readonly BackgroundWorker worker = new BackgroundWorker();

        public MainWindow()
        {
            InitializeComponent();
            rateObjects = new ObservableCollection<RateObject>();
            lvRates.ItemsSource = rateObjects;

            worker.DoWork += ReceiveMessages;
            worker.RunWorkerAsync();
            worker.WorkerReportsProgress = true;
            worker.ProgressChanged += new ProgressChangedEventHandler(UpdateList);
        }

        private void ReceiveMessages(object sender, DoWorkEventArgs e)
        {
            MessageQueue msMq = msMq = new MessageQueue(MessageConstants.ObjectMessageQueue);

            while (true)
            {
                try
                {
                    // msMq.Formatter = new XmlMessageFormatter(new Type[] {typeof(string)});
                    msMq.Formatter = new XmlMessageFormatter(new Type[] { typeof(RateObject) });

                    RateObject rate = (RateObject)msMq.Receive().Body;
                    worker.ReportProgress(0, rate);

                }
                catch (MessageQueueException ee)
                {
                    MessageBox.Show(ee.Message);
                }
                catch (Exception eee)
                {
                    MessageBox.Show(eee.Message);
                }
                finally
                {
                    msMq.Close();
                }

                Thread.Sleep(500);
            }
        }

        private void UpdateList(object sender, ProgressChangedEventArgs e)
        {
            RateObject obj = (RateObject) e.UserState;
            rateObjects.Add(obj);
        }
    }
}
