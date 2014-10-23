using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;

namespace Testumium.Helpers
{
    public class Configurations
    {
        public static string SMTPServer
        {
            get
            {
                return ConfigurationManager.AppSettings["SMTP.Server"];
            }
        }

        public static int SMTPPort
        {
            get
            {
                return Convert.ToInt32(ConfigurationManager.AppSettings["SMTP.Port"]);
            }
        }

        public static string SMTPUsername
        {
            get
            {
                return ConfigurationManager.AppSettings["SMTP.Username"];
            }
        }

        public static string SMTPPassword
        {
            get
            {
                return ConfigurationManager.AppSettings["SMTP.Password"];
            }
        }

        public static Boolean SMTPEnableSsl
        {
            get
            {
                return Convert.ToBoolean(ConfigurationManager.AppSettings["SMTP.EnableSSL"]);
            }
        }
    }
}