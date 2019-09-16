using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Web;

namespace Server.Common
{
    public class WebSettingsConfig
    {
        public static string UrlExpireTime
        {
            get {
                return AppSettingValue();
            }
        }
        public static string AppSettingValue([CallerMemberName]string key = null)
        {
            return ConfigurationManager.AppSettings[key];
        }
    }
}