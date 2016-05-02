// source: http://www.edgeoftheworld.fr/wp/work/separate-certificates-for-transport-and-message-security-in-wcf-2
using System;
using System.Configuration;
using System.ServiceModel.Configuration;
using System.ServiceModel.Description;

namespace WcfWsSecurity.Client.WcfExtensions
{
    /// <summary>
    /// This class defines the XML configuration element used to setup a DualCertificateClientCredentials behavior
    /// </summary>
    public class DualCertificateClientCredentialsElement : ClientCredentialsElement
    {
        ConfigurationPropertyCollection properties;

        public override Type BehaviorType
        {
            get
            {
                return typeof(DualCertificateClientCredentials);
            }
        }

        [ConfigurationProperty("transportCertificate")]
        public X509InitiatorCertificateClientElement TransportCertificate
        {
            get
            {
                return base["transportCertificate"]
                    as X509InitiatorCertificateClientElement;
            }
        }

        protected override ConfigurationPropertyCollection Properties
        {
            get
            {
                if (this.properties == null)
                {
                    ConfigurationPropertyCollection properties = base.Properties;
                    properties.Add(new ConfigurationProperty(
                        "transportCertificate",
                        typeof(X509InitiatorCertificateClientElement),
                        null, null, null,
                        ConfigurationPropertyOptions.None));
                    this.properties = properties;
                }
                return this.properties;
            }
        }

        protected override object CreateBehavior()
        {
            DualCertificateClientCredentials creds = new DualCertificateClientCredentials(
                base.CreateBehavior() as ClientCredentials);

            PropertyInformationCollection properties =
                ElementInformation.Properties;

            creds.SetTransportCertificate(TransportCertificate.StoreLocation,
                                            TransportCertificate.StoreName,
                                            TransportCertificate.X509FindType,
                                            TransportCertificate.FindValue);

            base.ApplyConfiguration(creds);
            return creds;
        }
    }
}
