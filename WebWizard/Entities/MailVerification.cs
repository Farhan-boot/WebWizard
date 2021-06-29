using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;
using System.Web.UI;
using System.Web.Security;
using System.IO;
using System.Text;
using System.Configuration;
using System.Security.Cryptography;
using System.Net.Mail;
using System.Net.Http;
using System.Net;
using System.Security.Cryptography.X509Certificates;
using System.Net.Security;

namespace WebWizard.Entities
{
    public class MailVerification
    {
        public void Email(string mailAddress, string verificationCode)
        {
            string fromAddress = "info@webwizerd.com";
            string fromPassword = "Ak4g5*d1";
            string toAddress = mailAddress;
            using (MailMessage mm = new MailMessage(fromAddress, toAddress))
            {
                mm.Subject = "Verification Code";
                mm.Body = "Your verification code is: " + verificationCode;
                mm.IsBodyHtml = true;
                System.Net.Mail.SmtpClient smtp = new System.Net.Mail.SmtpClient("208.91.198.210", 25);
                smtp.Host = "208.91.198.210";
                smtp.EnableSsl = true;
                NetworkCredential NetworkCred = new NetworkCredential(fromAddress, fromPassword);
                smtp.UseDefaultCredentials = true;
                smtp.Credentials = NetworkCred;
                //smtp.Port = 587;
                //smtp.Port = 25;
                NEVER_EAT_POISON_Disable_CertificateValidation();

                smtp.Send(mm);
            }
        }

        // [Obsolete("Do not use this in Production code!!!", true)]
        public static void NEVER_EAT_POISON_Disable_CertificateValidation()
        {
            // Disabling certificate validation can expose you to a man-in-the-middle attack
            // which may allow your encrypted message to be read by an attacker
            // https://stackoverflow.com/a/14907718/740639
            ServicePointManager.ServerCertificateValidationCallback =
                delegate (
                    object s,
                    X509Certificate certificate,
                    X509Chain chain,
                    SslPolicyErrors sslPolicyErrors
                )
                {
                    return true;
                };
        }

    }
}