﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ACAPackagesListener.API
{
    public class Globals
    {
        public static string cfDataBase
        {
            get { return "autoconsumeranalisys"; }
        }

        public static string ConnectionString
        {
            get { return @"Data Source=localhost\MSSQLSERVER2008;Initial Catalog=autoconsumeranalisys;Integrated Security=True"; }
        }
    }
}