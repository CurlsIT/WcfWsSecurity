// source: http://www.edgeoftheworld.fr/wp/work/separate-certificates-for-transport-and-message-security-in-wcf-2
using System.IdentityModel.Selectors;
using System.IdentityModel.Tokens;
using System.ServiceModel;
using System.ServiceModel.Security.Tokens;

namespace WcfWsSecurity.Client.WcfExtensions
{
    /// <summary>
    /// This class extends SecurityTokenManager to enable the separation between Transport layer encryption and Message layer encryption
    /// </summary>
    internal class DualCertificateSecurityTokenManager : ClientCredentialsSecurityTokenManager
    {
        DualCertificateClientCredentials _credentials;

        public DualCertificateSecurityTokenManager(DualCertificateClientCredentials credentials)
            : base(credentials)
        {
            this._credentials = credentials;
        }

        public override SecurityTokenProvider CreateSecurityTokenProvider(
            SecurityTokenRequirement requirement)
        {
            SecurityTokenProvider result = null;

            if (requirement.Properties.ContainsKey(ServiceModelSecurityTokenRequirement.TransportSchemeProperty) &&
                requirement.TokenType == SecurityTokenTypes.X509Certificate)
            {
                result = new X509SecurityTokenProvider(
                    this._credentials.TransportCertificate);
            }
            else if (requirement.KeyUsage == SecurityKeyUsage.Signature &&
                     requirement.TokenType == SecurityTokenTypes.X509Certificate)
            {
                result = new X509SecurityTokenProvider(
                    this._credentials.ClientCertificate.Certificate);
            }
            else
            {
                result = base.CreateSecurityTokenProvider(requirement);
            }

            return result;
        }
    }
}
