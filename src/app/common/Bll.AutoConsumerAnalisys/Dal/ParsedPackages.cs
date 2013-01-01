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
    internal sealed class ParsedPackages : Command
    {
        private String cfDataBase = Globals.cfDataBase;
        private const String cfTabla = "ParsedPackages";
        private const String cfColumn = "ParsedPackageId";

        public ParsedPackages(ref Connection Connection)
            : base(ref Connection)
        {
            InitGetCommand();
            InitPutCommand();
            DataBase = cfDataBase;
            Tabla = cfTabla;
            PkColumn = cfColumn;
        }
        public DataTable Buscar(String MAC, Int32 StartDate, Int32 EndDate, Boolean UsarDataSet)
        {
            InitGetCommand();
            if (!String.IsNullOrEmpty(MAC))
                this.Mac = MAC;
            if (StartDate > 0)
                this.StartDate = StartDate;
            if (EndDate > 0)
                this.EndDate = EndDate;
            return Llenar(this, UsarDataSet);
        }
        public override void InitPutCommand()
        {
            base.InitPutCommand();
        }
        public override void InitGetCommand()
        {
            base.InitGetCommand();
            GetCommand.Connection = base.GetConnection.Conexion;
            GetCommand.CommandText = "Spc_Get_ParsedPackages";
            GetCommand.Parameters.Add(new SqlParameter("@Mac", SqlDbType.VarChar, 255));
            GetCommand.Parameters.Add(new SqlParameter("@StartDate", SqlDbType.Int, 4));
            GetCommand.Parameters.Add(new SqlParameter("@EndDate", SqlDbType.Int, 4));
        }

        private String Mac
        {
            get { return (String)GetCommand.Parameters["@Mac"].Value; }
            set { GetCommand.Parameters["@Mac"].Value = value; }
        }

        private Int32 StartDate
        {
            get { return (Int32)GetCommand.Parameters["@StartDate"].Value; }
            set { GetCommand.Parameters["@StartDate"].Value = value; }
        }

        private Int32 EndDate
        {
            get { return (Int32)GetCommand.Parameters["@EndDate"].Value; }
            set { GetCommand.Parameters["@EndDate"].Value = value; }
        }
    }
}
