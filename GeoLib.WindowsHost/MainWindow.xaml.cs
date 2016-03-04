using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading;
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
using GeoLib.Services;
using GeoLib.WindowsHost.Contracts;
using GeoLib.WindowsHost.Services;

namespace GeoLib.WindowsHost
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public static MainWindow MainUI { get; set; }
        public MainWindow()
        {
            InitializeComponent();

            BtnStart.IsEnabled = true;
            BtnStop.IsEnabled = false;

            MainUI = this;

            Title = "UI Running on Thred " + Thread.CurrentThread.ManagedThreadId;
        }

        private ServiceHost _hostGeoManager = null;
        private ServiceHost _hostMessageManager = null;

        private SynchronizationContext _SyncContext = null;

        private void BtnStart_Click(object sender, RoutedEventArgs e)
        {
            _hostGeoManager = new ServiceHost(typeof(GeoManager));
            _hostMessageManager = new ServiceHost(typeof(MessageManager));
            _hostGeoManager.Open();
            _hostMessageManager.Open();

            BtnStart.IsEnabled = false;
            BtnStop.IsEnabled = true;
        }

        private void BtnStop_Click(object sender, RoutedEventArgs e)
        {
            _hostGeoManager.Close();
            _hostMessageManager.Close();

            BtnStart.IsEnabled = true;
            BtnStop.IsEnabled = false;
        }

        public void ShowMessage(string message)
        {
            int thredId = Thread.CurrentThread.ManagedThreadId;

            lblMessage.Content = message + Environment.NewLine +
                                 " Thred " + thredId + " | Process " +
                                 Process.GetCurrentProcess().Id.ToString();
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            BtnStop_Click(null, null);
        }

        private void Window_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Return)
            {
                if (BtnStart.IsEnabled)
                {
                    BtnStart_Click(sender, e);
                }
                else
                {
                    BtnStop_Click(sender, e);
                }
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            ChannelFactory<IMessageService> factory = new ChannelFactory<IMessageService>("");

            IMessageService proxy = factory.CreateChannel();

            proxy.ShowMessage(DateTime.Now.ToLongDateString() + 
                " from in-process call.");

            factory.Close();
        }
    }
}
