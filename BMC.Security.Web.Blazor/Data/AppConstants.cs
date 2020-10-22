using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BMC.Security.Web.Blazor.Data
{
    public static class AppConstants
    {
        public static string BlobConString { get; set; }
        public static string IoTCon { get; set; }
        public static List<Account> AdminAccounts { get; set; }
    }

    public static class MqttInfo
    {
        public static string MqttHost { get; set; }
        public static string MqttUser { get; set; }
        public static string MqttPass { get; set; }
    }
}
