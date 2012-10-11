using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ServiceModel;
using System.Runtime.Serialization;
using System.Net;
using System.Net.Security;
using System.Security.Cryptography.X509Certificates;
//Test Message None WSHttpBinding
namespace WCFClient
{
    class WCFClientTest
    {
        static void Main(string[] args)
        {

            try
            {
                //HTTP WSHttpBinding_IWCFService1
                WCFClient.ClientProxy.WCFServiceClient wcfServiceProxy = new WCFClient.ClientProxy.WCFServiceClient("WSHttpBinding_IWCFService");
                //通过代理调用SayHello服务
                string sName = "Frank Xu Lei  Message UserName WSHttpBinding";
                string sResult = string.Empty;
                wcfServiceProxy.ClientCredentials.UserName.UserName = "FrankXuLei";
                wcfServiceProxy.ClientCredentials.UserName.Password = "00000000";
                //wcfServiceProxy.ClientCredentials.Windows.ClientCredential.UserName = "Administrator";
                //wcfServiceProxy.ClientCredentials.Windows.ClientCredential.Domain = "COMPUTER";
                Util.SetCertificatePolicy();
                sResult = wcfServiceProxy.SayHello(sName);
                Console.WriteLine("Returned Result is {0}", sResult);
            }
            catch (Exception e)
            {
               Console.WriteLine("Exception : {0}", e.Message);
            }
            //For Debug
            Console.WriteLine("Press any key to exit");
            Console.Read();
            
        }

    }
//    I saw people ask questions on the forums regarding to “Could not establish trust relationship for the SSL/TLS secure channel with authority” while attempting to call the web service via a host domain name other than the one specified in Issue-To within the SSL certificate. Most likely you are using the same certificate for the WCF web services hosted on other domains, for example, development or demo server.

//A custom remote certificate validation can be used to avoid the strict validation, instead, just make it trust anything. 

//In your code, simply make a call to the static method SetCertificatePolicy() once within your application before making any request to the web services.
    // note this code is not intended to used 
    // in production enviroment
    public static class Util
    {
        /// <summary>
        /// Sets the cert policy.
        /// </summary>
        public static void SetCertificatePolicy()
        {
            ServicePointManager.ServerCertificateValidationCallback
                       += RemoteCertificateValidate;
        }

        /// <summary>
        /// Remotes the certificate validate.
        /// </summary>
        private static bool RemoteCertificateValidate(
           object sender, X509Certificate cert,
            X509Chain chain, SslPolicyErrors error)
        {
            // trust any certificate!!!
            System.Console.WriteLine("Warning, trust any certificate");
            return true;
        }
    }

}
