// source: http://www.edgeoftheworld.fr/wp/work/separate-certificates-for-transport-and-message-security-in-wcf-2
using System.IdentityModel.Selectors;
using System.Security.Cryptography;
using System.Security.Cryptography.X509Certificates;
using System.ServiceModel.Description;

namespace WcfWsSecurity.Client.WcfExtensions
{
    /// <summary>
    /// This class extends ClientCredentials to enable the seperation between Transport layer encryption and Message layer encryption
    /// </summary>
    public class DualCertificateClientCredentials : ClientCredentials
    {
        /// <summary>
        /// The X509 Certificate that is to be used for https
        /// </summary>
        public X509Certificate2 TransportCertificate { get; set; }

        public DualCertificateClientCredentials(ClientCredentials existingCredentials)
          : base(existingCredentials)
        {
        }

        protected DualCertificateClientCredentials(DualCertificateClientCredentials other)
          : base(other)
        {
            TransportCertificate = other.TransportCertificate;
        }

        protected override ClientCredentials CloneCore()
        {
            return new DualCertificateClientCredentials(this);
        }

        public override SecurityTokenManager CreateSecurityTokenManager()
        {
            return new DualCertificateSecurityTokenManager(this);
        }

        public void SetTransportCertificate(string subjectName, StoreLocation storeLocation, StoreName storeName)
        {
            SetTransportCertificate(storeLocation, storeName, X509FindType.FindBySubjectDistinguishedName, subjectName);
        }

        public void SetTransportCertificate(StoreLocation storeLocation, StoreName storeName, X509FindType x509FindType, string subjectName)
        {
            TransportCertificate = FindCertificate(storeLocation, storeName, x509FindType, subjectName);
        }

        private static X509Certificate2 FindCertificate(StoreLocation location, StoreName name,
          X509FindType findType, string findValue)
        {
            X509Store store = new X509Store(name, location);
            try
            {
                store.Open(OpenFlags.ReadOnly);
                X509Certificate2Collection col = store.Certificates.Find(findType, findValue, true);

                if (col.Count > 0)
                {
                    return col[0];
                }
                else
                {
                    //Little improvement on blogesh’s code: let’s throw a meaningful exception if the certificate isn’t found
                    throw new CryptographicException("The SSL transport X.509 certificate could not be found.");
                }

            }
            finally
            {
                store.Close();
            }
        }
    }
}
