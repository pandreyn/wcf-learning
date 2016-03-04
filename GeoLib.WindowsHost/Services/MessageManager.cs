using System.Diagnostics;
using System.ServiceModel;
using GeoLib.WindowsHost.Contracts;

namespace GeoLib.WindowsHost.Services
{
    [ServiceBehavior(UseSynchronizationContext = false)]
    public class MessageManager: IMessageService
    {
        public void ShowMessage(string message)
        {
            MainWindow.MainUI.ShowMessage(message + " | Process " +
                Process.GetCurrentProcess().Id.ToString());
        }
    }
}