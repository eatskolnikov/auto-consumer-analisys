using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace ACAPackagesListener.API.JSON
{
    public class ParsedPackageCollection
    {
        private List<JSON.ParsedPackage> parsedPackages;
        public ParsedPackageCollection(IEnumerable<DataRow> rows)
        {
            parsedPackages = new List<JSON.ParsedPackage>();
            foreach (var row in rows)
            {
                var parsedPackage = new JSON.ParsedPackage(row);
                parsedPackages.Add(parsedPackage);
            }
        }

        public string MAC { get; set; }

        public JSON.ParsedPackage[] ParsedPackages
        {
            get { return parsedPackages.ToArray(); }
            set { parsedPackages = new List<JSON.ParsedPackage>(value); }
        }
    }
}

