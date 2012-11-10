using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using Lrrc.Sys.Data.Exeception;

namespace Lrrc.Sys.Data
{
    /// <summary>
    /// Admnistrador de Conexiones a Base de Datos.
    /// Author: Luis R. Romero Castillo
    /// Owner: Luis R. Romero Castillo
    /// </summary>
    public sealed class Connection
    {
        private SqlConnection fConexion;

        private SqlTransaction fTransaction;

        private String fCnString,
            fUsuario; 
       
        private Int32 fUsuarioId;

        private DataSet fDataSet;

        /// <summary>
        /// Crea una instancia tipo Connection.
        /// </summary>
        /// <param name="cnString">Connection String</param>
        public Connection(String cnString)
        {
            fCnString = cnString;

            fDataSet = new DataSet();
        }
        /// <summary>
        /// Obtiene el objeto DataSet en memoria.
        /// </summary>
        public DataSet DataSet
        {
            get { return fDataSet; }
        }
        /// <summary>
        /// Reconstruye el objeto DataSet en memoria.
        /// </summary>
        public void DataSetReset()
        {
            String fDsName = fDataSet.DataSetName;

            fDataSet.Dispose();

            fDataSet = null;

            fDataSet = new DataSet(fDsName);
        }        /// <summary>
        /// Obtiene el Id de Sesión de Usuario.
        /// </summary>
        public String SessionId
        {
            get { return SessionId; }
        }
        /// <summary>
        /// Obtiene o establece el Nombre de Usuario.
        /// </summary>
        public String Usuario
        {
            get { return fUsuario; }
            set { fUsuario = value; }
        }
        /// <summary>
        /// Obtiene el o establece Codigo de Usuario.
        /// </summary>
        public Int32 UsuarioId
        {
            get { return fUsuarioId; }
            set { fUsuarioId = value; }
        }
        /// <summary>
        /// Obtiene la conexión activa.
        /// </summary>
        public SqlConnection Conexion
        {
            get { return fConexion; }
        }
        /// <summary>
        /// Abre una nueva conexión.
        /// </summary>
        public void Abrir()
        {
            fConexion = new SqlConnection(fCnString);

            try
            {
                fConexion.Open();
            }
            catch (SqlException e)
            {
                throw new Excepcion(e.Message, e.Number);
            }
            catch
            {
                throw new Excepcion(Message.Err_OpeningConnection_Msg, Message.Err_OpeningConnection);
            }
        }
        /// <summary>
        /// Cierra la conexión activa.
        /// </summary>
        public void Cerrar()
        {
            try
            {
                fConexion.Close();
            }
            catch (SqlException eSql)
            {
                throw new Excepcion(eSql.Message, eSql.Number);
            }
            catch
            {
                throw new Excepcion(Message.Err_ClosingConnection_Msg, Message.Err_ClosingConnection);
            }
        }
        /// <summary>
        /// Crea una instancia de transacción e inicia su vigencia.
        /// </summary>
        public void CrearTransaccion()
        {
            try
            {
                if (fTransaction != null)
                    fTransaction.Dispose();

                Conexion.Open();

                fTransaction = Conexion.BeginTransaction(IsolationLevel.ReadCommitted);
            }
            catch (Excepcion Error)
            {
                fConexion.Close();

                throw new Excepcion(Error.Mensaje, Error.Numero);
            }
        }
        /// <summary>
        /// Confirma la operación producida en el banco de datos, libera la memoria y destruye la instancia de 
        /// la transacción.
        /// </summary>
        public void CerrarTransaccion()
        {
            try
            {
                if (fTransaction != null)
                {
                    fTransaction.Commit();

                    fTransaction.Dispose();

                    fTransaction = null;

                    fConexion.Close();
                }
            }
            catch (Excepcion Error)
            {
                fConexion.Close();

                throw new Excepcion(Error.Mensaje, Error.Numero);
            }
        }
        /// <summary>
        /// Rechaza la operación producida en el banco de datos, libera la memoria y destruye la instancia de 
        /// la transacción.
        /// </summary>
        public void AbortarTransaccion()
        {
            try
            {
                if (fTransaction != null)
                {
                    fTransaction.Rollback();

                    fTransaction.Dispose();

                    fTransaction = null;

                    fConexion.Close();
                }
            }
            catch (Excepcion Error)
            {
                fConexion.Close();

                throw new Excepcion(Error.Mensaje, Error.Numero);
            }
        }
        /// <summary>
        /// Obtiene o establece la conexion Ado.Net Sql.
        /// </summary>
        public SqlTransaction GetTransaccion
        {
            get { return fTransaction; }
        }
    }
}