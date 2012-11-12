using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Configuration;
using ACAPackagesListener.API;
using Bll.AutoConsumerAnalisys;
using Lrrc.Sys.Data;
using Lrrc.Sys.Data.Interface;
using Lrrc.Sys.Data.Exeception;
using System.Data.SqlClient;

namespace Dal.AutoConsumerAnalisys
{
    internal sealed class Packages : Command
    {
        private String cfDataBase = Globals.cfDataBase;
        private const String cfTabla = "Packages";
        private const String cfColumn = "PackageId";

        public Packages(ref Connection Connection)
            : base(ref Connection)
        {
            InitGetCommand();
            InitPutCommand();
            DataBase = cfDataBase;
            Tabla = cfTabla;
            PkColumn = cfColumn;
        }
        public DataTable Buscar(Int32 PackageId, Boolean UsarDataSet)
        {
            InitGetCommand();
            if (PackageId > 0)
                this.PackageId = PackageId;
            return Llenar(this, UsarDataSet);
        }


        public override void InitPutCommand()
        {
            base.InitPutCommand();
            PutCommand.Connection = base.GetConnection.Conexion;
            PutCommand.CommandText = "Spc_Put_Package";

            PutCommand.Parameters.Add(new SqlParameter("@Accion", SqlDbType.TinyInt, 2, "Accion"));
            PutCommand.Parameters.Add(new SqlParameter("@PackageId", SqlDbType.Int, 4, "PackageId"));
            PutCommand.Parameters.Add(new SqlParameter("@ErrorNumber", SqlDbType.SmallInt, 2, "ErrorNumber"));
            PutCommand.Parameters.Add(new SqlParameter("@ErrorMessage", SqlDbType.VarChar, 200, "ErrorMessage"));
            PutCommand.Parameters.Add(new SqlParameter("@Ip", SqlDbType.VarChar, 35, "Ip"));
            PutCommand.Parameters.Add(new SqlParameter("@Message", SqlDbType.VarChar, 250, "Message"));
            PutCommand.Parameters.Add(new SqlParameter("@Parsed", SqlDbType.Bit, 1, "Parsed"));
            PutCommand.Parameters.Add(new SqlParameter("@Activo", SqlDbType.Bit, 1, "Activo"));

            PutCommand.Parameters["@" + cfColumn].Direction = ParameterDirection.InputOutput;
            PutCommand.Parameters["@ErrorNumber"].Direction = ParameterDirection.InputOutput;
            PutCommand.Parameters["@ErrorMessage"].Direction = ParameterDirection.InputOutput;
        }
        public override void InitGetCommand()
        {
            base.InitGetCommand();
            GetCommand.Connection = base.GetConnection.Conexion;
            GetCommand.CommandText = "Spc_Get_Package";

            GetCommand.Parameters.Add(new SqlParameter("@PackageId", SqlDbType.Int, 4));
        }


        private Int32 PackageId
        {
            get { return (Int32)GetCommand.Parameters["@PackageId"].Value; }
            set { GetCommand.Parameters["@PackageId"].Value = value; }
        }
    }
}
