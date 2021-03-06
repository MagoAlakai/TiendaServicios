using System.Net.Security;
using System.Security.Cryptography.X509Certificates;

namespace TiendaServicios.Api.Gateway;

public static class HttpClientHandlerExtensions
{
    /// <summary>
    /// Allows the http client to accept self-signed SSL certificates
    /// </summary>
    /// <param name="handler"></param>
    public static void UseUnsignedServerCertificateValidation(this HttpClientHandler handler)
    {
        X509Certificate2 value = new("Certificates/tienda-svc.crt", "tienda-svc");
        handler.ClientCertificates.Add(value);
        handler.ServerCertificateCustomValidationCallback = delegate (HttpRequestMessage sender, X509Certificate2? certificate, X509Chain? chain, SslPolicyErrors ssl_policy_errors)
        {
            if (ssl_policy_errors == SslPolicyErrors.None)
            {
                return true;
            }

            if ((ssl_policy_errors & SslPolicyErrors.RemoteCertificateChainErrors) == 0)
            {
                return false;
            }

            if (certificate == null || chain == null || chain?.ChainStatus == null)
            {
                return false;
            }

            X509ChainStatus[] chainStatus = chain!.ChainStatus;
            for (int i = 0; i < chainStatus.Length; i++)
            {
                X509ChainStatus x509ChainStatus = chainStatus[i];
                if ((!(certificate!.Subject == certificate!.Issuer) || x509ChainStatus.Status != X509ChainStatusFlags.UntrustedRoot) && x509ChainStatus.Status != 0)
                {
                    return false;
                }
            }

            return true;
        };
    }
}
