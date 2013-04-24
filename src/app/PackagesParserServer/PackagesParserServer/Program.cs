using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Data;
using System.Threading;

namespace PackagesParserServer
{
    class Program
    {
        static void Main(string[] args)
        {
            while(true){
                Thread.Sleep(5000);
                RunParser();
            }
        }
        static void RunParser()
        {
            try
            {
                var connection = new SqlConnection(ConfigurationManager.ConnectionStrings[0].ConnectionString);
                connection.Open();
                var command = new SqlCommand("Spc_Parse_Packages", connection);
                command.CommandType = CommandType.StoredProcedure;
                command.ExecuteNonQuery();
                connection.Close();
            }
            catch
            {
                
            }
        }
    }
}
