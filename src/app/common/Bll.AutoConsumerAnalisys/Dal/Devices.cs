using System;
using System.Data;
using ACAPackagesListener.API;
using Lrrc.Sys.Data;
using System.Data.SqlClient;

namespace Dal.AutoConsumerAnalisys
{
    internal sealed class Devices : Command
    {
        private readonly String cfDataBase = Globals.cfDataBase;
        private const String cfTabla = "Devices";
        private const String cfColumn = "DeviceId";

        public Devices(ref Connection Connection)
            : base(ref Connection)
        {
            InitGetCommand();
            InitPutCommand();
            DataBase = cfDataBase;
            Tabla = cfTabla;
            PkColumn = cfColumn;
        }
        public DataTable Buscar(Int32 DeviceId, Boolean UsarDataSet)
        {
            InitGetCommand();
            if (DeviceId > 0)
                this.DeviceId = DeviceId;
            return Llenar(this, UsarDataSet);
        }


        public override void InitPutCommand()
        {
            base.InitPutCommand();
            PutCommand.Connection = base.GetConnection.Conexion;
            PutCommand.CommandText = "Spc_Put_Device";

            PutCommand.Parameters.Add(new SqlParameter("@Accion", SqlDbType.TinyInt, 2, "Accion"));
            PutCommand.Parameters.Add(new SqlParameter("@DeviceId", SqlDbType.Int, 4, "DeviceId"));
            PutCommand.Parameters.Add(new SqlParameter("@ErrorNumber", SqlDbType.SmallInt, 2, "ErrorNumber"));
            PutCommand.Parameters.Add(new SqlParameter("@ErrorMessage", SqlDbType.VarChar, 200, "ErrorMessage"));
            PutCommand.Parameters.Add(new SqlParameter("@Ip", SqlDbType.VarChar, 35, "Ip"));
            PutCommand.Parameters.Add(new SqlParameter("@Description", SqlDbType.VarChar, 300, "Description"));
            PutCommand.Parameters.Add(new SqlParameter("@LatLng", SqlDbType.VarChar, 250, "LatLng"));
            PutCommand.Parameters.Add(new SqlParameter("@Activo", SqlDbType.Bit, 1, "Activo"));

            PutCommand.Parameters["@" + cfColumn].Direction = ParameterDirection.InputOutput;
            PutCommand.Parameters["@ErrorNumber"].Direction = ParameterDirection.InputOutput;
            PutCommand.Parameters["@ErrorMessage"].Direction = ParameterDirection.InputOutput;
        }
        public override void InitGetCommand()
        {
            base.InitGetCommand();
            GetCommand.Connection = base.GetConnection.Conexion;
            GetCommand.CommandText = "Spc_Get_Device";
            GetCommand.Parameters.Add(new SqlParameter("@DeviceId", SqlDbType.Int, 4));
        }

        private Int32 DeviceId
        {
            get { return (Int32)GetCommand.Parameters["@DeviceId"].Value; }
            set { GetCommand.Parameters["@DeviceId"].Value = value; }
        }
        public Int32 InsertedDeviceId
        {
            get { return (Int32)PutCommand.Parameters["@DeviceId"].Value; }
            set { PutCommand.Parameters["@DeviceId"].Value = value; }
        }
    }
}
