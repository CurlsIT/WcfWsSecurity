using System;
using System.ServiceModel;

namespace WcfWsSecurity.Service
{
    class Program
    {
        static void Main(string[] args)
        {
            using (var serviceHost = new ServiceHost(typeof(SampleService)))
            {
                serviceHost.Open();

                Console.WriteLine("Service started. Press [Enter] to exit.");
                Console.ReadLine();
            }
        }
    }
}
