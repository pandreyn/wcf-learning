using System.ServiceModel;

namespace GeoLib.Client.Contracts
{
    [ServiceContract(Namespace = "http://xer.ru/")]
    public interface IMessageService
    {
        [OperationContract]
        void ShowMessage(string message);
    }
}