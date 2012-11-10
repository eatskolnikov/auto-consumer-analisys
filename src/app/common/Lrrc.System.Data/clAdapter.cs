using System;
using System.IO;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using Lrrc.Sys.Data.Exeception;
using Lrrc.Sys.Data.Interface;
using System.Configuration;

namespace Lrrc.Sys.Data
{
    /// <summary>
    /// Objeto Adaptador de Conexiones.
    /// Author: Luis R. Romero Castillo
    /// Owner: Luis R. Romero Castillo
    /// </summary>
    public class Adapter
    {
        private Connection fConnection;

        private SqlDataAdapter fAdapter;

        private XmlHistory fXmlHistory;

        private HistoryDetalle fHistoryDetalle;

        private DataTable fInserted,
            fUpdated,
            fDeleted;

        private Boolean fConciliada,
            fEjecutado;

        private Object fValorConciliacion;


        private const String MSG_INSERT = "El registro con el {0} = {1} de la tabla {2} ha sido insertado por el usuario.";
        private const String MSG_UPDATE = "El registro con el {0} = {1} de la tabla {2} ha sido actualizado por el usuario.";
        private const String MSG_DELETE = "El registro con el {0} = {1} de la tabla {2} ha sido eliminado por el usuario.";

        /// <summary>
        /// Crea una instancia tipo Adapter.
        /// </summary>
        internal Adapter(ref Connection Connection)
        {
            fConnection = Connection;
        }
        /// <summary>
        /// Obtiene el objeto Connection pasado por referencia.
        /// </summary>
        public Connection GetConnection
        {
            get { return fConnection; }
        }
        /// <summary>
        /// Obtiene o establece un objeto tipo SqlDataAdapter.
        /// </summary>
        internal DataSet DataSet
        {
            get
            {
                if (fConnection.DataSet != null)
                    return fConnection.DataSet;
                else
                {
                    throw new Excepcion(Message.Err_DataSet_Msg, Message.Err_DataSet);
                }
            }
        }
        /// <summary>
        /// Ejecuta el método ExecuteNonQuery del DelCommand.
        /// </summary>
        /// <param name="Comando">Implementación de Objeto Tipo IDelCommand</param>
        internal void EjecutarSinQuery(IDelCommand Comando)
        {
            try
            {
                fConnection.Abrir();

                Comando.DelCommand.Connection = fConnection.Conexion;

                if (Comando.DelCommand.Connection.Database != Comando.DataBase)
                    Comando.DelCommand.Connection.ChangeDatabase(Comando.DataBase);

                Comando.DelCommand.Parameters[0].Value = Comando.DeltaKey;

                Comando.DelCommand.ExecuteNonQuery();

                fConnection.Cerrar();
            }
            catch (SqlException eSQL)
            {
                throw new Excepcion(eSQL.Message, eSQL.Number);
            }
            catch (Exception E)
            {
                throw new Excepcion(E.Message);
            }
        }
        /// <summary>
        /// Ejecuta el adaptador y crea el esquema de columnas de la tabla requerida por el objeto Command.
        /// </summary>
        /// <param name="Comando">IGetCommand</param>
        internal void CrearEsquema(IGetCommand Comando)
        {
            fAdapter = new SqlDataAdapter();

            fAdapter.SelectCommand = Comando.GetCommand;

            try
            {
                if (fConnection.Conexion == null)
                {
                    fConnection.Abrir();
                }
                else if (fConnection.Conexion.State != ConnectionState.Open)
                    fConnection.Abrir();

                Comando.GetCommand.Connection = fConnection.Conexion;
                Comando.GetCommand.Transaction = fConnection.GetTransaccion;

                if (Comando.GetCommand.Connection.Database != Comando.DataBase)
                    Comando.GetCommand.Connection.ChangeDatabase(Comando.DataBase);

                if (!Comando.GetCommand.Parameters.Contains("@Schema"))
                    Comando.GetCommand.Parameters.Add("@Schema", SqlDbType.Bit);

                Comando.GetCommand.Parameters["@Schema"].Value = 1;

                fAdapter.FillSchema(DataSet, SchemaType.Source, Comando.Tabla);

                DataSet.Tables[Comando.Tabla].AcceptChanges();
            }
            catch (SqlException eSQL)
            {
                throw new Excepcion(eSQL.Message, eSQL.Number);
            }
            catch (Exception E)
            {
                throw new Excepcion(E.Message);
            }
            finally
            {
                if (fConnection.GetTransaccion == null)
                    fConnection.Cerrar();

                fAdapter.Dispose();
            }
        }
        /// <summary>
        /// Ejecuta el adaptador, llena un objeto DataTable y lo retorna con los resultados de la ejecución.
        /// </summary>
        /// <param name="Comando">Objeto Command Activo</param>
        /// <param name="UsarDataSet">Indica al Adapter si los resultados de la búsqueda se reflejarán sobre el DataSet. Sólo ejecutorio para aquellos DataTables con esquemas de columnas definidos en el DataSet.</param>
        public DataTable Llenar(IGetCommand Comando, Boolean UsarDataSet)
        {
            fAdapter = new SqlDataAdapter();

            DataTable fTabla = new DataTable();

            fAdapter.SelectCommand = Comando.GetCommand;

            Boolean fInDataSet = false;

            try
            {
                fConnection.Abrir();

                Comando.GetCommand.Connection = fConnection.Conexion;

                if (Comando.GetCommand.Connection.Database != Comando.DataBase)
                    Comando.GetCommand.Connection.ChangeDatabase(Comando.DataBase);

                if (!Comando.GetCommand.Parameters.Contains("@Schema"))
                    Comando.GetCommand.Parameters.Add("@Schema", SqlDbType.Bit);

                Comando.GetCommand.Parameters["@Schema"].Value = 0;

                fInDataSet = InDataSet(Comando.Tabla);

                if (fInDataSet && UsarDataSet)
                {
                    DataSet.Tables[Comando.Tabla].Clear();

                    fAdapter.Fill(DataSet, Comando.Tabla);
                }
                else
                    fAdapter.Fill(fTabla);
            }
            catch (SqlException eSQL)
            {
                throw new Excepcion(eSQL.Message, eSQL.Number);
            }
            catch (Exception E)
            {
                throw new Excepcion(E.Message);
            }
            finally
            {
                fConnection.Cerrar();

                fAdapter.Dispose();
            }

            if (fInDataSet && UsarDataSet)
                return DataSet.Tables[Comando.Tabla];
            else
                return fTabla;
        }
        /// <summary>
        /// Ejecuta el método Update del DataAdapter contra la conexión de base de datos establecida. Este método trabaja con transacciones con un nivel de isolación Serializable.
        /// El SqlCommand utilizado por este método es el PutCommand, el cual debe ser funcional para cualquier actualización.
        /// </summary>
        /// <param name="Comando">Objeto Conectividad.System.Data.Command</param>
        public void Actualizar(IPutCommand Comando)
        {
            Actualizar(Comando, false);
        }
        /// <summary>
        /// Ejecuta el método Update del DataAdapter contra la conexión de base de datos establecida. 
        /// Este método trabaja con transacciones con un nivel de isolación Serializable.
        /// El SqlCommand utilizado por este método es el PutCommand, el cual debe ser funcional para 
        /// cualquier actualización.
        /// </summary>
        /// <param name="Comando">Implementación de Interfaz IPutCommand</param>
        /// <param name="ConHistorial">Determina si se escribe el historial de actualizaciones</param>
        public void Actualizar(IPutCommand Comando, Boolean ConHistorial)
        {
            if (InDataSet(Comando.Tabla))
            {
                if (DataSet.HasChanges())
                {
                    Boolean fAutomatica = false;

                    if (fConnection.Conexion.State != ConnectionState.Open)
                    {
                        fAutomatica = true;

                        fConnection.CrearTransaccion();
                    }

                    Comando.PutCommand.Connection = fConnection.Conexion;

                    fAdapter = new SqlDataAdapter();

                    fAdapter.AcceptChangesDuringUpdate = false;

                    fConciliada = false;

                    fEjecutado = false;

                    Comando.PutCommand.Transaction = fConnection.GetTransaccion;

                    fAdapter.DeleteCommand = Comando.PutCommand;

                    fAdapter.InsertCommand = Comando.PutCommand;

                    fAdapter.UpdateCommand = Comando.PutCommand;

                    if (Comando.PutCommand.Connection.Database != Comando.DataBase)
                        Comando.PutCommand.Connection.ChangeDatabase(Comando.DataBase);

                    try
                    {
                        fDeleted = DataSet.Tables[Comando.Tabla].GetChanges(DataRowState.Deleted);

                        fInserted = DataSet.Tables[Comando.Tabla].GetChanges(DataRowState.Added);

                        fUpdated = DataSet.Tables[Comando.Tabla].GetChanges(DataRowState.Modified);

                        if (fDeleted != null)
                        {
                            fEjecutado = true;

                            DataView dvDeleted = new DataView(fDeleted, "", "", DataViewRowState.Deleted);

                            fDeleted = dvDeleted.ToTable();

                            fAdapter.Update(fDeleted);
                        }

                        if (fUpdated != null)
                        {
                            fEjecutado = true;

                            fAdapter.Update(fUpdated);
                        }

                        if (fInserted != null)
                        {
                            foreach (DataRow Fila in fInserted.Rows)
                            {
                                Fila["Accion"] = 1;
                            }

                            fAdapter.RowUpdated += new SqlRowUpdatedEventHandler(_rowupdadated);

                            fAdapter.Update(fInserted);

                            fEjecutado = true;

                            fConciliada = true;
                        }

                        if (fAutomatica)
                            fConnection.CerrarTransaccion();

                        if (fEjecutado)
                        {
                            if (!Comando.PutCommand.Parameters.Contains("@ErrorNumber") ||
                                !Comando.PutCommand.Parameters.Contains("@ErrorMessage"))
                                throw new Excepcion(Message.Err_MissingsParams_Msg, Message.Err_MissingsParams);
                            else
                            {
                                if (Comando.PutCommand.Parameters["@ErrorNumber"] == null ||
                                    Comando.PutCommand.Parameters["@ErrorMessage"] == null)
                                    throw new Excepcion(Message.Err_EmptyParams_Msg, Message.Err_EmptyParams);
                                else
                                {
                                    if ((Int16)Comando.PutCommand.Parameters["@ErrorNumber"].Value > 0)
                                    {
                                        throw new Excepcion(Comando.PutCommand.Parameters["@ErrorMessage"].Value.ToString(),
                                            (Int16)Comando.PutCommand.Parameters["@ErrorNumber"].Value);
                                    }
                                    else
                                        DataSet.Tables[Comando.Tabla].AcceptChanges();
                                }
                            }

                            if (!ConHistorial)
                                ExecuteHistory(Comando.DataBase, Comando.Tabla, Comando.PkColumn,
                                    ref fInserted, ref fUpdated, ref fDeleted);
                        }

                    }
                    catch (SqlException eSQL)
                    {
                        if (fAutomatica)
                            fConnection.AbortarTransaccion();

                        throw new Excepcion(eSQL.Message, eSQL.Number);
                    }
                    catch (Exception E)
                    {
                        if (fAutomatica)
                            fConnection.AbortarTransaccion();

                        throw new Excepcion(E.Message);
                    }
                    finally
                    {
                        fAdapter.Dispose();
                    }
                }
            }
        }
        /// <summary>
        /// Evento para sincronización de filas hijas.
        /// </summary>
        private void _rowupdadated(Object sender, SqlRowUpdatedEventArgs e)
        {
            DataRow[] fRows;

            DataColumn[] childColumn;

            Boolean _done = false;

            // Obtiene la fila actualizada en el DataTable.
            foreach (DataRow Fila in DataSet.Tables[e.Row.Table.TableName].Rows)
            {
                if (Fila.RowState == DataRowState.Added) // Insersión de registros.
                {
                    if (e.Row.Table.PrimaryKey.Length > 0)
                    {
                        _done = true;

                        Fila[e.Row.Table.PrimaryKey[0].ToString()] = e.Row[e.Row.Table.PrimaryKey[0].ToString()];

                        Fila.AcceptChanges();

                        // Estable la propiedad con el valor retornado del banco de datos.
                        fValorConciliacion = Fila[e.Row.Table.PrimaryKey[0].ToString()];
                    }

                    // Obtiene la colección de Relaciones secundarias.
                    foreach (DataRelation Relacion in DataSet.Tables[e.Row.Table.TableName].ChildRelations)
                    {
                        if (Fila.RowState == DataRowState.Added && !_done)
                        {
                            // Actualiza la columna de la fila primaria con el valor retornado por la base de datos.
                            Fila[Relacion.ParentColumns[0].ColumnName] = e.Row[Relacion.ParentColumns[0].ColumnName];

                            Fila.AcceptChanges();

                            // Estable la propiedad con el valor retornado del banco de datos.
                            fValorConciliacion = Fila[Relacion.ParentColumns[0].ColumnName];
                        }

                        // Obtiene la colección de filas asociadas a la relación.
                        fRows = Fila.GetChildRows(Relacion);

                        // Obtiene la colección de columnas asociadas a la relación.
                        childColumn = Relacion.ChildColumns;

                        // Actualiza la columna en las filas secundarias.
                        foreach (DataRow childRows in fRows)
                        {
                            childRows[childColumn[0].ColumnName] = Fila[Relacion.ParentColumns[0].ColumnName];
                        }
                    }

                    // Abandona el proceso.
                    break;
                }
            }
        }
        /// <summary>
        /// Obtiene el valor que determina si se produjo una inserción de información.
        /// </summary>
        internal Boolean Conciliada
        {
            get { return fConciliada; }
        }
        /// <summary>
        /// Obtiene el valor establecido en la columna Pk de la tabla Parent al momento de producirse una conciliación
        /// entre padre e hijos.
        /// En caso de no haberse producido conciliación alguna retorna un valor null.
        /// </summary>
        internal Object ValorConciliacion
        {
            get
            {
                if (fConciliada)
                    return fValorConciliacion;
                else
                    return null;
            }
        }
        /// <summary>
        /// Retorna un valor lógico que indica si el nombre de la tabla se encuentra
        /// en la colección del DataSet.
        /// </summary>
        /// <param name="Tabla">Nombre de Tabla</param>
        /// <returns>Boolean</returns>
        internal Boolean InDataSet(String Tabla)
        {
            return (DataSet.Tables.Contains(Tabla));
        }
        private void ExecuteHistory(String DataBase, String DataTable, String DataColumn,
            ref DataTable TblInserted, ref DataTable TblUpdated, ref DataTable TblDeleted)
        {
            try
            {

                if (LogHistory)
                {
                    fXmlHistory = new XmlHistory(ref fConnection);

                    fXmlHistory.NuevaFila();

                    StringWriter fWriter = new StringWriter();

                    DataTable fTblHistory;

                    String sMessage = String.Empty;

                    if (TblInserted != null)
                    {
                        fTblHistory = TblInserted;
                        sMessage = MSG_INSERT;
                    }
                    else if (TblUpdated != null)
                    {
                        fTblHistory = TblUpdated;
                        sMessage = MSG_UPDATE;
                    }
                    else
                    {
                        fTblHistory = TblDeleted;
                        sMessage = MSG_DELETE;
                    }

                    fTblHistory.WriteXml(fWriter, XmlWriteMode.WriteSchema, false);

                    fXmlHistory.Fila["Accion"] = 1;
                    fXmlHistory.Fila["ID_XmlHistory"] = 0;
                    fXmlHistory.Fila["ID_Usuario"] = fConnection.UsuarioId;
                    fXmlHistory.Fila["XmlDataBase"] = DataBase;
                    fXmlHistory.Fila["XmlDataTable"] = DataTable;
                    fXmlHistory.Fila["XmlDataColumn"] = DataColumn;
                    fXmlHistory.Fila["XmlHistory"] = fWriter.ToString();
                    fXmlHistory.Fila["Activo"] = true;

                    fXmlHistory.InsertarFila(false, true);

                    Actualizar(fXmlHistory, true);

                    fHistoryDetalle = new HistoryDetalle(ref fConnection);
                    fHistoryDetalle.Refrescar();
                    foreach (DataRow dr in fTblHistory.Rows)
                    {
                        sMessage = String.Format(sMessage, DataColumn, dr[DataColumn], DataTable);

                        fHistoryDetalle.NuevaFila();
                        fHistoryDetalle.Fila["Accion"] = 1;
                        fHistoryDetalle.Fila["UsuarioId"] = fConnection.UsuarioId;
                        fHistoryDetalle.Fila["DataBase"] = DataBase;
                        fHistoryDetalle.Fila["DataTable"] = DataTable;
                        fHistoryDetalle.Fila["Message"] = sMessage;

                        fHistoryDetalle.InsertarFila(false);
                    }

                    Actualizar(fHistoryDetalle, true);

                    fHistoryDetalle = null;
                    fXmlHistory = null;
                }

            }
            catch { }

        }
        private Boolean LogHistory
        {
            get
            {
                try
                {
                    return Convert.ToBoolean(ConfigurationManager.AppSettings["LogHistory"]);
                }
                catch
                {
                    return false;
                }
            }
        }
    }
}