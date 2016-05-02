using System.Net.Security;
using System.ServiceModel;

namespace WcfWsSecurity.Contracts
{
    // Set the ProtectionLevel on the whole service to Sign.
    [ServiceContract(ProtectionLevel = ProtectionLevel.Sign)]
    public interface ISampleService
    {
        [OperationContract]
        string Hello(string name);
    }
}
