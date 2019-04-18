using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BMC.Security.Web.Helpers
{
    public class DeviceData
    {
        public string IP { get; set; }
        public string Name { get; set; }
        public int ID { get; set; }

        public static List<DeviceData> GetAllDevices()
        {
            return new List<DeviceData>()
            {
                new DeviceData (){ ID=1, Name="Lampu Depan WC", IP="192.168.1.27" },
                new DeviceData (){ ID=2, Name="Lampu Kamar Atas", IP="192.168.1.25" },
                new DeviceData (){ ID=3, Name="Lampu Tengah", IP="192.168.1.28" },
                new DeviceData (){ ID=4, Name="Kipas Musolla", IP="192.168.1.31" },
                new DeviceData (){ ID=5, Name="Kipas Printer 3D", IP="192.168.1.32" },
                new DeviceData (){ ID=6, Name="Kipas Depan Dapur", IP="192.168.1.33" },
                //new DeviceData (){ ID=7, Name="Lampu Musolla", IP="192.168.1.35" },
                new DeviceData (){ ID=7, Name="Lampu Ruang Tamu", IP="192.168.1.26" },
                new DeviceData (){ ID=8, Name="Kipas Kamar Depan", IP="192.168.1.36" },

                new DeviceData (){ ID=9, Name="Lampu Musola", IP="192.168.1.29" },
                new DeviceData (){ ID=10, Name="Door Lock 1", IP="192.168.1.40" },
                new DeviceData (){ ID=11, Name="Door Lock 2", IP="192.168.1.42" },

            };
        }
    }
}