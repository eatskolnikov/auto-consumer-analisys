using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using Lrrc.Sys.Data;
using Lrrc.Sys.Data.Exeception;
using Lrrc.Sys.Data.Interface;

namespace Lrrc.Sys.Data
{
    /// <summary>
    /// Administraci�n de Historiales de Bases de Datos.
    /// Author: Luis R. Romero Castillo
    /// Owner: Luis R. Romero Castillo
    /// </summary>
    internal class HistoryDetalle : Command
    {
        private String cfDataBase = "dbXmlHistory";
        private const String cfTabla = "dtLogHistory";
        private const String cfColumn = "LogHistoryId";

        /// <summary>
        /// Crea una instancia del objeto Historial de Bases de Datos.
        /// </summary>
        public HistoryDetalle(ref Connection Connection)
            : base(ref Connection)
        {
            InitGetCommand();
            InitPutCommand();
            DataBase = cfDataBase;
            Tabla = cfTabla;
            PkColumn = cfColumn;
        }
        /// <summary>
        /// M�todo de b�squeda.
        /// </summary>
        /// <param name="XmlHistoryId">C�digo del Registro</param>
        /// <param name="UsuarioId">C�dgo del Usuario</param>
        /// <param name="Activo">Estatus del Registro</param>
        /// <param name="UsarDataSet">Determina si la ejecuci�n de lectura se hace contra el DataSet</param>
        /// <returns>DataTable</returns>
        public DataTable Buscar(Int32 XmlHistoryId, Int16 UsuarioId, Boolean Activo, Boolean UsarDataSet)
        {
            InitGetCommand();

            if (XmlHistoryId > 0)
                this.XmlHistoryId = XmlHistoryId;
            if (UsuarioId > 0)
                this.UsuarioId = UsuarioId;

            return Llenar(this, UsarDataSet);
        }
        /// <summary>
        /// Inicializa el SqlCommand PutCommand para las actualizaciones a la Base de Datos.
        /// </summary>
        public override void InitPutCommand()
        {
            base.InitPutCommand();
            PutCommand.Connection = GetConnection.Conexion;
            PutCommand.CommandText = "Spc_Put_LogHistory";
            PutCommand.Parameters.Add(new SqlParameter("@Accion", SqlDbType.TinyInt, 2, "Accion"));
            PutCommand.Parameters.Add(new SqlParameter("@LogHistoryId", SqlDbType.Int, 4, "LogHistoryId"));
            PutCommand.Parameters.Add(new SqlParameter("@ErrorNumber", SqlDbType.SmallInt, 2, "ErrorNumber"));
            PutCommand.Parameters.Add(new SqlParameter("@ErrorMessage", SqlDbType.VarChar, 200, "ErrorMessage"));
            PutCommand.Parameters.Add(new SqlParameter("@UsuarioId", SqlDbType.Int, 4, "UsuarioId"));
            PutCommand.Parameters.Add(new SqlParameter("@DataBase", SqlDbType.VarChar, 50, "DataBase"));
            PutCommand.Parameters.Add(new SqlParameter("@DataTable", SqlDbType.VarChar, 50, "DataTable"));
            PutCommand.Parameters.Add(new SqlParameter("@Message", SqlDbType.VarChar, 1000, "Message"));

            PutCommand.Parameters["@" + cfColumn].Direction = ParameterDirection.InputOutput;
            PutCommand.Parameters["@ErrorNumber"].Direction = ParameterDirection.InputOutput;
            PutCommand.Parameters["@ErrorMessage"].Direction = ParameterDirection.InputOutput;
        }
        public override void ValidarFila()
        {
            Condicion = "0 = 1";
        }
        /// <summary>
        /// Inicializa el SqlCommand GetCommand para la realizaci�n de b�squedas o acceso a la informaci�n.
        /// </summary>
        public override void InitGetCommand()
        {
            base.InitGetCommand();
            GetCommand.Connection = GetConnection.Conexion;
            GetCommand.CommandText = "Spc_Get_LogHistory";
            GetCommand.Parameters.Add(new SqlParameter("@LogHistoryId", SqlDbType.Int, 4));
            GetCommand.Parameters.Add(new SqlParameter("@UsuarioId", SqlDbType.Int, 2));
        }
        /// <summary>
        /// Obtiene o establece el valor de la Calificaci�n Jur�dica del GetCommand.
        /// </summary>
        private Int32 XmlHistoryId
        {
            get { return (Int32)GetCommand.Parameters["@LogHistoryId"].Value; }
            set { GetCommand.Parameters["@LogHistoryId"].Value = value; }
        }
        /// <summary>
        /// Obtiene o establece el valor del c�digo de usuario del GetCommand.
        /// </summary>
        private Int32 UsuarioId
        {
            get { return (Int32)GetCommand.Parameters["@UsuarioId"].Value; }
            set { GetCommand.Parameters["@UsuarioId"].Value = value; }
        }
    }
}