using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SarahApp.Libs
{
    public class CCTVData
    {
        const string CCTV_IP = "192.168.1.10";
        public string Url { get; set; }
        public int ID { get; set; }
        public string Name { get; set; }
        public static List<CCTVData> GetCCTVs()
        {
            return new List<CCTVData>()
            {
                new CCTVData (){ ID=1, Name="Parking Area", Url=  $"http://{CCTV_IP}/cgi-bin/snapshot.cgi?chn=1&u=admin&p=&q=0&d=1&rand=" },
                new CCTVData (){ ID=2, Name="Living Room", Url = $"http://{CCTV_IP}/cgi-bin/snapshot.cgi?chn=2&u=admin&p=&q=0&d=1&rand=" },
                new CCTVData (){ ID=3, Name="Top Floor", Url =  $"http://{CCTV_IP}/cgi-bin/snapshot.cgi?chn=3&u=admin&p=&q=0&d=1&rand=" },
                new CCTVData (){ ID=4, Name="Backyard", Url = $"http://{CCTV_IP}/cgi-bin/snapshot.cgi?chn=0&u=admin&p=&q=0&d=1&rand="},


            };
        }

    }
}
