using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Configuration;
using System.Linq;
using System.Text;
using ACAPackagesListener.API.Models.Enities;

namespace ACAPackagesListener.API
{
    public class Globals
    {
        public static string cfDataBase
        {
            get { return "autoconsumeranalisys"; }
        }

        public static User loggedUser { get; set; }

        public static string ConnectionString
        {
            get
            {
                var connectionStringsSection = ConfigurationManager.GetSection("connectionStrings") as ConnectionStringsSection;
                if (connectionStringsSection != null)
                {
                    return connectionStringsSection.ConnectionStrings[0].ConnectionString;;
                }
                return "";
            } //@"Data Source=AMAZONA-BJ2E5Q9;Initial Catalog=autoconsumeranalisys;Integrated Security=True"; }
        }
    }
}
