using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace ACAPackagesListener.API.JSON
{
    public class Device
    {
        public Device(DataRow row)
        {
            DeviceId = Int32.Parse(row["DeviceId"].ToString());
            Ip = row["Ip"].ToString();
            LatLng = row["LatLng"].ToString();
            Description = row["Description"].ToString();
        }

        public int DeviceId;
        public string Ip;
        public string LatLng;
        public string Description;
    }
}
