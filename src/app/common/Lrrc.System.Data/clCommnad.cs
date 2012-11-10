using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using Lrrc.Sys.Data.Interface;
using Lrrc.Sys.Data.Exeception;

namespace Lrrc.Sys.Data
{
    /// <summary>
    /// Objeto Command. Administrador de Objetos de la Capa de Datos.
    /// Author: Luis R. Romero Castillo
    /// Owner: Luis R. Romero Castillo
    /// </summary>
    public abstract class Command : Adapter, IPutCommand, IGetCommand, IDelCommand, ICommand
    {
        private SqlCommand fPutCommand,
            fGetCommand,
            fDelCommand;

        private String fDataBase,
            fTabla,
            fPkColumn,
            fCondicion,
            fCondicionDelete;

        private DataRow fFila,
            fFilaProceso;

        private Object fDeltaKey,
            fPkId;

        /// <summary>
        /// Crea una instancia tipo Command.
        /// </summary>
        public Command(ref Connection Connection) : base(ref Connection)
        {
            fCondicion = "";
        }
        /// <summary>
        /// Obtiene o establece el nombre de la base de datos que utiliza el objeto.
        /// </summary>
        public String DataBase
        {
            get { return fDataBase; }
            set { fDataBase = value; }
        }
        /// <summary>
        /// Obtiene la Interfaz ICommand Activa.
        /// </summary>
        public ICommand ICommand
        {
            get { return (ICommand)this; }
        }
        /// <summary>
        /// Obtiene o establece el nombre de la tabla asociada al objeto.
        /// </summary>
        public String Tabla
        {
            get { return fTabla; }
            set { fTabla = value; }
        }
        /// <summary>
        /// Obtiene el DataTable correspondiente al Command activo.
        /// </summary>
        public DataTable GetTabla
        {
            get { return DataSet.Tables[fTabla]; }
        }
        /// <summary>
        /// Importa un objeto DataTable con todas sus filas hacia un DataTable existente en el DataSet activo.
        /// De manera absoluta, este método considera a la Columna Accion, la cual es la que determina el tipo
        /// de operación que se realiza al momento de ejecutarse contra la base de datos.
        /// Por defecto, al momento de utilizar este método, esta columna se llena con un valor 2 (Actualización).
        /// </summary>
        /// <param name="Tabla">DataTable en Memoria</param>
        public void ImportarTabla(DataTable Tabla)
        {
            try
            {
                if (InDataSet(fTabla))
                {
                    DataSet.Tables[fTabla].Rows.Clear();
                    
                    DataRow fNuevaFila;

                    foreach (DataRow Fila in Tabla.Rows)
                    {
                        fNuevaFila = DataSet.Tables[fTabla].NewRow();

                        fNuevaFila["Accion"] = 2;

                        foreach (DataColumn Columna in fNuevaFila.Table.Columns)
                        {
                            fNuevaFila[Columna.ColumnName] = Fila[Columna.ColumnName];
                        }

                        DataSet.Tables[fTabla].Rows.Add(fNuevaFila);
                    }
                }
            }
            catch (Exception e)
            {
                throw new Excepcion(e.Message);
            }
        }
        /// <summary>
        /// Método virtual para inicializar o configurar el SqlCommand dedicado a actualizaciones en la base de datos.
        /// </summary>
        public virtual void InitPutCommand()
        {
            if (fPutCommand != null)
                fPutCommand.Dispose();

            fPutCommand = new SqlCommand();
            
            fPutCommand.CommandType = CommandType.StoredProcedure;
            
            fPutCommand.CommandTimeout = 0;
            
            fPutCommand.Parameters.Clear();
        }
        /// <summary>
        /// Obtiene o establece el objeto SqlCommand dedicado a actualizaciones en la base de datos.
        /// </summary>
        public SqlCommand PutCommand
        {
            set { fPutCommand = value; }
            get { return fPutCommand; }
        }
        /// <summary>
        /// Método virtual para inicializar o configurar el SqlCommand dedicado a lecturas en la base de datos.
        /// </summary>
        public virtual void InitGetCommand()
        {
            if (fGetCommand != null)
            {
                fGetCommand.Cancel();

                fGetCommand.Dispose();

                fGetCommand.Connection = null;

                fGetCommand = null;
            }

            fGetCommand = new SqlCommand();
            
            fGetCommand.CommandType = CommandType.StoredProcedure;
            
            fGetCommand.CommandTimeout = 0;
            
            fGetCommand.Parameters.Clear();
        }
        /// <summary>
        /// Obtiene o establece el objeto SqlCommand dedicado a lecturas en la base de datos.
        /// </summary>
        public SqlCommand GetCommand
        {
            set { fGetCommand = value; }
            get { return fGetCommand; }
        }
        /// <summary>
        /// Método virtual para inicializar o configurar el SqlCommand dedicado a eliminaciones en la base de datos.
        /// </summary>
        public virtual void InitDelCommand()
        {
            if (fDelCommand != null)
                fDelCommand.Dispose();

            fDelCommand = new SqlCommand();
            
            fDelCommand.CommandType = CommandType.StoredProcedure;
            
            fDelCommand.CommandTimeout = 0;
            
            fDelCommand.Parameters.Clear();
        }
        /// <summary>
        /// Obtiene o establece el objeto SqlCommand dedicado a eliminaciones en la base de datos.
        /// </summary>
        public SqlCommand DelCommand
        {
            get { return fDelCommand; }
            set { fDelCommand = value; }
        }
        /// <summary>
        /// Obtiene o establece el PrimaryKey del parámetro utilizado en el DelCommand.
        /// </summary>
        public Object DeltaKey
        {
            get { return fDeltaKey; }
            set { fDeltaKey = value; }
        }
        /// <summary>
        /// Retorna una objeto DataTable con el esquema de datos de la tabla especificada.
        /// </summary>
        public DataTable CargarEsquema()
        {
            CrearEsquema(this);

            foreach (DataColumn dC in DataSet.Tables[fTabla].Columns)
            {
                dC.ReadOnly = false;

                dC.AllowDBNull = true;
            }

            DataSet.Tables[fTabla].Columns[fPkColumn].AutoIncrement = true;

            DataSet.Tables[fTabla].Columns[fPkColumn].Unique = true;
            DataSet.Tables[fTabla].Columns[fPkColumn].AutoIncrementSeed = 0;

            DataSet.Tables[fTabla].Columns[fPkColumn].AutoIncrementStep = -1;

            return DataSet.Tables[fTabla];
        }
        /// <summary>
        /// Establece la relación entre dos tablas (Master/Detail) de un mismo objeto DataSet.
        /// </summary>
        /// <param name="Relacion">Nombre de la Relación</param>
        /// <param name="ChildTableName">Nombre de la Tabla Hija</param>
        /// <param name="ChildColumnName">Nombre de la Columna de la Tabla Hija</param>
        public void Relacionar(String Relacion, String ChildTableName, String ChildColumnName)
        {
            try
            {
                DataColumn ChildColumn = DataSet.Tables[ChildTableName].Columns[ChildColumnName];

                CrearRelacion(Relacion, DataSet.Tables[fTabla].Columns[fPkColumn], ChildColumn, false);
            }
            catch(Exception Error)
            {
                throw new Excepcion(Error.Message);
            }
        }
        /// <summary>
        /// Establece la relación entre dos tablas (Master/Detail) de un mismo objeto DataSet.
        /// </summary>
        /// <param name="Relacion">Nombre de la Relación</param>
        /// <param name="Detalle">Implementación del Objeto ICommand</param>
        public void Relacionar(String Relacion, ICommand Detalle)
        {
            try
            {
                CrearRelacion(Relacion, DataSet.Tables[fTabla].Columns[fPkColumn],
                    DataSet.Tables[Detalle.Tabla].Columns[fPkColumn], false);
            }
            catch (Exception Error)
            {
                throw new Excepcion(Error.Message);
            }
        }
        /// <summary>
        /// Establece la relación entre dos tablas (Master/Detail) de un mismo objeto DataSet.
        /// </summary>
        /// <param name="Relacion">Nombre de la Relación</param>
        /// <param name="Detalle">Implementación del Objeto ICommand</param>
        /// <param name="ColumnasHijas">String con los nombres de columnas, separados por ",", que serán utilizados en la relación</param>
        public void Relacionar(String Relacion, ICommand Detalle, String ColumnasHijas)
        {
            try
            {
                Char[] delimiterChar = { ',' };

                String[] childString = ColumnasHijas.Split(delimiterChar);

                DataColumn[] childColumn = new DataColumn[childString.Length],
                    parentColumn = new DataColumn[childString.Length];

                for (Byte Index = 0; Index < childString.Length; Index++)
                {
                    childColumn[Index] = DataSet.Tables[Detalle.Tabla].Columns[childString[Index]];

                    parentColumn[Index] = DataSet.Tables[fTabla].Columns[fPkColumn];

                    CrearRelacion(Relacion + Index.ToString(), parentColumn[Index], childColumn[Index], false);
                }
            }
            catch (Exception Error)
            {
                throw new Excepcion(Error.Message);
            }
        }
        /// <summary>
        /// Crea las relaciones entre uno o mas objetos Datatable.
        /// </summary>
        /// <param name="Nombre">Nombre de la Relación</param>
        /// <param name="parentColumn">Columna Padre</param>
        /// <param name="childColumn">Columna Hija</param>
        /// <param name="Anidar">Determina si se anida la relación</param>
        private void CrearRelacion(String Nombre, DataColumn parentColumn, DataColumn childColumn, Boolean Anidar)
        {
            if (!DataSet.Relations.Contains(Nombre))
            {
                DataRelation fRelacion = new DataRelation(Nombre, parentColumn, childColumn, true);

                fRelacion.Nested = Anidar;

                DataSet.Relations.Add(fRelacion);
            }
        }
        /// <summary>
        /// Crea  un nuevo DataRow de la tabla establecida en el Command.
        /// </summary>
        public void NuevaFila()
        {
            try
            {
                if (DataSet.Tables[fTabla] == null)
                    CargarEsquema();

                fFila = DataSet.Tables[fTabla].NewRow();

                fPkId = fFila[fPkColumn];
            }
            catch
            {
                throw new Excepcion(Message.Err_InsertingRow_Msg, Message.Err_InsertingRow);
            }
        }
        /// <summary>
        /// Inserta una nueva fila en la tabla establecida en el Objeto Command activo.
        /// </summary>
        public void InsertarFila()
        {
            InsertarFila(true, true);
        }
        /// <summary>
        /// Inserta una nueva fila en la tabla establecida en el Objeto Command activo.
        /// </summary>
        public void InsertarFila(Boolean Validar)
        {
            InsertarFila(true, Validar);
        }
        /// <summary>
        /// Obtiene el valor del Id insertado.
        /// </summary>
        public Object GetPkId
        {
            get { return fPkId; }
        }
        /// <summary>
        /// Inserta una nueva fila en la tabla establecida en el Objeto Command activo.
        /// <param name="Verificar">Verifica si la fila existe en la colección de DataRows del DataTable</param>
        /// <param name="Validar">Determina si la vila debe ejecutar las reglas de insersión de la capa de datos.</param>
        /// </summary>
        public void InsertarFila(Boolean Verificar, Boolean Validar)
        {
            try
            {
                if (Validar)
                    ValidarFila();
            }
            catch (Excepcion Error)
            {
                throw new Excepcion(Error.Mensaje, Error.Numero);
            }
            catch (Exception Err)
            {
                throw new Excepcion(Err.Message);
            }

            if (!FilaEncontrada() || !Verificar)
            {
                try
                {
                    DataSet.Tables[fTabla].Rows.Add(fFila);

                    if (!Validar)
                        fFila[fPkColumn] = 0;

                    //fFila[fPkColumn] = fPkId; 
                    fPkId = fFila[fPkColumn];
                }
                catch (ConstraintException eC)
                {
                    throw new Excepcion(eC.Message);
                }
                catch (Exception e)
                {
                    throw new Excepcion(e.Message);
                }
            }
            else
                throw new Excepcion(Message.Err_DuplicatedRow_Msg, Message.Err_DuplicatedRow);
        }
        /// <summary>
        /// Marca como eliminada una fila de la colección de la tabla establecida en el Command.
        /// Este método considera la generación de excepciones en caso de error o violaciones de Constraints.
        /// </summary>
        /// <param name="PkValue">Código del Registro</param>
        public void EliminarFila(Object PkValue)
        {
            EliminarFila(PkValue, true);
        }
        /// <summary>
        /// Marca como eliminada una fila de la colección de la tabla establecida en el Command.
        /// </summary>
        /// <param name="PkValue">Código del Registro</param>
        /// <param name="DispararExcepcion">Disparar Exepción en caso de error</param>
        public void EliminarFila(Object PkValue, Boolean DispararExcepcion)
        {
            foreach (DataRow Fila in DataSet.Tables[fTabla].Rows)
            {
                if (Fila.RowState != DataRowState.Deleted)
                {
                    if (Fila[fPkColumn].ToString() == PkValue.ToString())
                    {
                        fFilaProceso = Fila;

                        if (!DispararExcepcion)
                            EliminarFilasHijas();

                        if (!TieneFilasHijas())
                        {
                            Fila["Accion"] = 2;
                            
                            Fila["Activo"] = false;
                            
                            Fila.AcceptChanges();
                            
                            Fila.Delete();

                            break;
                        }
                        else if (DispararExcepcion)
                            throw new Excepcion(Message.Err_RowDependencies_Msg, Message.Err_RowDependencies);
                    }
                }
            }
        }
        /// <summary>
        /// Determina si la fila activa tiene relaciones descendientes.
        /// </summary>
        /// <returns></returns>
        public void EliminarFilasHijas()
        {
            foreach (DataRelation Relacion in DataSet.Tables[Tabla].ChildRelations)
            {
                foreach (DataRow Fila in fFilaProceso.GetChildRows(Relacion))
                {
                    Fila["Accion"] = 2;
                    
                    Fila["Activo"] = false;
                    
                    Fila.AcceptChanges();
                    
                    Fila.Delete();
                }
            }
        }
        /// <summary>
        /// Obtiene un objeto DataRow a partir de la llave suministrada.
        /// </summary>
        /// <param name="Llave">Object con la llave de la fila</param>
        public void ObtenerFila(Object Llave)
        {
            try
            {
                DataRow[] dR = DataSet.Tables[fTabla].Select(fPkColumn + " = " + Llave.ToString());

                if (dR.GetLength(0) > 0)
                {
                    fFila = dR[0];
                    
                    fPkId = fFila[fPkColumn];
                }
            }
            catch
            {
                throw new Excepcion(Message.Err_EmptyCondition_Msg, Message.Err_EmptyCondition);
            }
        }
        /// <summary>
        /// Obtiene o establece un objeto tipo DataRow de la tabla establecida en el Command.
        /// </summary>
        public DataRow Fila
        {
            get { return fFila; }
            set { fFila = value; }
        }
        /// <summary>
        /// Realiza las validaciones de cada columna de la fila indicada.
        /// </summary>
        public virtual void ValidarFila()
        {
        }
        /// <summary>
        /// Obtiene o estable la Columna Pk del DataTable utilizado por el Command.
        /// A través de esta se realizan las conciliaciones de valores en las relaciones Master/Detail.
        /// </summary>
        public String PkColumn
        {
            get { return fPkColumn; }
            set { fPkColumn = value; }
        }
        /// <summary>
        /// Obtiene o Establece la condición que será evaluada en el método FilaEncontrada.
        /// </summary>
        protected String Condicion
        {
            get { return fCondicion; }
            set { fCondicion = value; }
        }
        /// <summary>
        /// Obtiene o Establece la condición que será evaluada en el método TieneRelaciones.
        /// Esta condición se sobre pone a las relaciones establecidas por el objeto. En otras palabras,
        /// en caso de que sea implementada, las relaciones serán obviadas.
        /// </summary>
        public String CondicionDelete
        {
            get { return fCondicionDelete; }
            set { fCondicionDelete = value; }
        }
        /// <summary>
        /// Realiza una búsqueda en el objeto Command activo para determinar si existe una fila que cumpla con la condición especificada.
        /// Esta implementación se hace como resultado de que la creación de Unique y NonUnique Index no es permitida en Ado.Net 2.0.
        /// </summary>
        /// <returns>True/False</returns>
        protected Boolean FilaEncontrada()
        {
            try
            {
                return (DataSet.Tables[Tabla].Select(PkColumn + "=" + fFila[fPkColumn]).GetLength(0) > 0);
            }
            catch
            {
                throw new Excepcion(Message.Err_EmptyCondition_Msg, Message.Err_EmptyCondition);
            }
        }
        /// <summary>
        /// Refresca la colección de tablas relacionadas, removiendo las filas y sus respectivas relaciones.
        /// </summary>
        public void Refrescar()
        {
            if (DataSet.Tables[Tabla] != null)
            {
                try
                {
                    for (Byte Index = 0; Index < DataSet.Tables[Tabla].ChildRelations.Count; Index++)
                    {
                        DataSet.Tables[Tabla].ChildRelations[Index].ChildTable.Rows.Clear();
                    }

                    DataSet.Tables[Tabla].Rows.Clear();
                }
                catch (Exception ex) { 
                    
                    throw ex; }
            }
        }
        /// <summary>
        /// Determina si la fila activa tiene relaciones descendientes.
        /// </summary>
        /// <returns></returns>
        public Boolean TieneFilasHijas()
        {
            if (fCondicionDelete != null)
            {
                if (DataSet.Tables[fTabla].Select(fCondicionDelete).Length > 0)
                    throw new Excepcion(Message.Err_RowDependencies_Msg, Message.Err_RowDependencies);
            }
            else
            {
                DataRelationCollection drC = DataSet.Tables[fTabla].ChildRelations;

                foreach (DataRelation Relacion in drC)
                {
                    if (fFilaProceso.GetChildRows(Relacion).Length > 0)
                        return true;
                }
            }

            return false;
        }
    }
}