using System.ServiceModel;
using WcfWsSecurity.Contracts;

namespace WcfWsSecurity.Client
{
    internal class SampleServiceClient:ClientBase<ISampleService>, ISampleService
    {
        public string Hello(string name) => Channel.Hello(name);
    }
}
