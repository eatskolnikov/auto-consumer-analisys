using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace ACAPackagesListener.API.JSON
{
    public class ParsedPackage
    {
        public ParsedPackage(DataRow row)
        {
            MAC = row["MAC"].ToString();
            LatLng = row["LatLng"].ToString();
            PackageDate = Int32.Parse(row["PackageDate"].ToString());
            Description = row["Description"].ToString();
        }
        public String LatLng;
        public String MAC;
        public Int32 PackageDate;
        public String Description;
    }
}
