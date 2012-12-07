using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace ACAPackagesListener.API.JSON
{
    public class DeviceCollection
    {
        private List<JSON.Device> devices;

        public DeviceCollection(IEnumerable<DataRow> rows)
        {
            devices = new List<JSON.Device>();
            foreach (var row in rows)
            {
                var device = new JSON.Device(row);
                devices.Add(device);
            }
        }

        public JSON.Device[] Devices
        {
            get { return devices.ToArray(); }
            set { devices = new List<JSON.Device>(value); }
        }
    }
}
