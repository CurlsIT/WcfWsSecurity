using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace WcfWsSecurity.Client
{
    public class ViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        private string name;
        public string Name
        {
            get { return name; }
            set
            {
                name = value;
                UpdateServiceResponse();
                OnPropertyChanged();
            }
        }

        private string serviceResponse;
        public string ServiceResponse
        {
            get { return serviceResponse; }
            set
            {
                serviceResponse = value;
                OnPropertyChanged();
            }
        }

        private void UpdateServiceResponse()
        {
            using (var sampleServiceClient = new SampleServiceClient())
            {
                ServiceResponse = sampleServiceClient.Hello(Name);
            }
        }

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
