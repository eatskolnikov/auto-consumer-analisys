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
    /// Administración de Historiales de Bases de Datos.
    /// Author: Luis R. Romero Castillo
    /// Owner: Luis R. Romero Castillo
    /// </summary>
    internal class XmlHistory : Command
    {
        private String cfDataBase = System.Configuration.ConfigurationManager.AppSettings["dbXmlHistory"];
        private const String cfTabla = "dtXmlHistory";
        private const String cfColumn = "ID_XmlHistory";

        /// <summary>
        /// Crea una instancia del objeto Historial de Bases de Datos.
        /// </summary>
        public XmlHistory(ref Connection Connection)
            : base(ref Connection)
        {
            InitGetCommand();
            InitPutCommand();
            DataBase = cfDataBase;
            Tabla = cfTabla;
            PkColumn = cfColumn;
        }
        /// <summary>
        /// Método de búsqueda.
        /// </summary>
        /// <param name="XmlHistoryId">Código del Registro</param>
        /// <param name="UsuarioId">Códgo del Usuario</param>
        /// <param name="Activo">Estatus del Registro</param>
        /// <param name="UsarDataSet">Determina si la ejecución de lectura se hace contra el DataSet</param>
        /// <returns>DataTable</returns>
        public DataTable Buscar(Int32 XmlHistoryId, Int16 UsuarioId, Boolean Activo, Boolean UsarDataSet)
        {
            InitGetCommand();

            if (XmlHistoryId > 0)
                this.XmlHistoryId = XmlHistoryId;
            if (UsuarioId > 0)
                this.UsuarioId = UsuarioId;
            this.Activo = Activo;

            return Llenar(this, UsarDataSet);
        }
        /// <summary>
        /// Inicializa el SqlCommand PutCommand para las actualizaciones a la Base de Datos.
        /// </summary>
        public override void InitPutCommand()
        {
            base.InitPutCommand();
            PutCommand.Connection = GetConnection.Conexion;
            PutCommand.CommandText = "Spc_Put_XmlHistory";
            PutCommand.Parameters.Add(new SqlParameter("@Accion", SqlDbType.TinyInt, 2, "Accion"));
            PutCommand.Parameters.Add(new SqlParameter("@ID_XmlHistory", SqlDbType.Int, 4, "ID_XmlHistory"));
            PutCommand.Parameters.Add(new SqlParameter("@ErrorNumber", SqlDbType.SmallInt, 2, "ErrorNumber"));
            PutCommand.Parameters.Add(new SqlParameter("@ErrorMessage", SqlDbType.VarChar, 200, "ErrorMessage"));
            PutCommand.Parameters.Add(new SqlParameter("@ID_Usuario", SqlDbType.SmallInt, 2, "ID_Usuario"));
            PutCommand.Parameters.Add(new SqlParameter("@XmlDataBase", SqlDbType.VarChar, 50, "XmlDataBase"));
            PutCommand.Parameters.Add(new SqlParameter("@XmlDataTable", SqlDbType.VarChar, 50, "XmlDataTable"));
            PutCommand.Parameters.Add(new SqlParameter("@XmlDataColumn", SqlDbType.VarChar, 50, "XmlDataColumn"));
            PutCommand.Parameters.Add(new SqlParameter("@XmlHistory", SqlDbType.Xml, 0, "XmlHistory"));
            PutCommand.Parameters.Add(new SqlParameter("@Activo", SqlDbType.Bit, 1, "Activo"));

            PutCommand.Parameters["@" + cfColumn].Direction = ParameterDirection.InputOutput;
            PutCommand.Parameters["@ErrorNumber"].Direction = ParameterDirection.InputOutput;
            PutCommand.Parameters["@ErrorMessage"].Direction = ParameterDirection.InputOutput;
        }
        public override void ValidarFila()
        {
            if (this.Fila["ID_XmlHistory"] != null && ((Int32)this.Fila["ID_XmlHistory"] == 0 && (Byte)this.Fila["Accion"] == 2))
                throw new Excepcion(Message.Err_LavePrimaria_Msg, Message.Err_LavePrimaria);
            if ((Int16)this.Fila["ID_Usuario"] == 0)
                throw new Excepcion(Message.Err_Null_Msg, Message.Err_Null);
            if (String.IsNullOrEmpty(this.Fila["XmlHistory"].ToString()))
                throw new Excepcion(Message.Err_Null_Msg, Message.Err_Null);

            Condicion = "0 = 1";
        }
        /// <summary>
        /// Inicializa el SqlCommand GetCommand para la realización de búsquedas o acceso a la información.
        /// </summary>
        public override void InitGetCommand()
        {
            base.InitGetCommand();
            GetCommand.Connection = GetConnection.Conexion;
            GetCommand.CommandText = "Spc_Get_XmlHistory";
            GetCommand.Parameters.Add(new SqlParameter("@ID_XmlHistory", SqlDbType.Int, 4));
            GetCommand.Parameters.Add(new SqlParameter("@ID_Usuario", SqlDbType.SmallInt, 2));
            GetCommand.Parameters.Add(new SqlParameter("@Activo", SqlDbType.Bit, 1));
        }
        /// <summary>
        /// Obtiene o establece el valor de la Calificación Jurídica del GetCommand.
        /// </summary>
        private Int32 XmlHistoryId
        {
            get { return (Int32)GetCommand.Parameters["@ID_XmlHistory"].Value; }
            set { GetCommand.Parameters["@ID_XmlHistory"].Value = value; }
        }
        /// <summary>
        /// Obtiene o establece el valor del código de usuario del GetCommand.
        /// </summary>
        private Int16 UsuarioId
        {
            get { return (Int16)GetCommand.Parameters["@ID_Usuario"].Value; }
            set { GetCommand.Parameters["@ID_Usuario"].Value = value; }
        }
        /// <summary>
        /// Obtiene o establece el el estatus del registro en la base de datos del GetCommand.
        /// </summary>
        private Boolean Activo
        {
            get { return (Boolean)GetCommand.Parameters["@Activo"].Value; }
            set { GetCommand.Parameters["@Activo"].Value = value; }
        }
    }
}