using System.Web.Configuration;

namespace WarmTransfer.Web.Domain
{
    public class Config
    {
        public static string AccountSID
        {
            get { return WebConfigurationManager.AppSettings["TwilioAccountSid"]; }
        }

        public static string AuthToken
        {
            get { return WebConfigurationManager.AppSettings["TwilioAuthToken"]; }
        }

        public static string TwilioPhoneNumber
        {
            get { return WebConfigurationManager.AppSettings["TwilioPhoneNumber"]; }
        }
    }
}