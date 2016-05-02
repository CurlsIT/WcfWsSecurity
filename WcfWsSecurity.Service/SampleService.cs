using WcfWsSecurity.Contracts;

namespace WcfWsSecurity.Service
{
    class SampleService : ISampleService
    {
        public string Hello(string name) => $"Hello {name}!";
    }
}
