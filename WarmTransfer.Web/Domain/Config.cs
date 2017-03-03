﻿using System.Web.Configuration;

namespace WarmTransfer.Web.Domain
{
    public class Config
    {
        public static string AccountSid => 
            WebConfigurationManager.AppSettings["TwilioAccountSid"] ?? "ACXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXX";

        public static string AuthToken => 
            WebConfigurationManager.AppSettings["TwilioAuthToken"] ?? "aXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXX";

        public static string TwilioPhoneNumber => 
            WebConfigurationManager.AppSettings["TwilioPhoneNumber"] ?? "+12345678";

        public static string Domain => 
            WebConfigurationManager.AppSettings["Domain"] ?? "domain.ngrok.io";
    }
}