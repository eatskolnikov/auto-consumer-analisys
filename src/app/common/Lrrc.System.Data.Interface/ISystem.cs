using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.SqlClient;

namespace Lrrc.Sys.Data.Interface
{
    /// <summary>
    /// Interfaz Padre para Interfaces de capa de datos.
    /// </summary>
    public interface ICommand
    {
        /// <summary>
        /// Obtiene o Establece el nombre de la base de datos.
        /// </summary>
        String DataBase { get; set;}
        /// <summary>
        /// Obtiene o Establece el nombre de la tabla de datos.
        /// </summary>
        String Tabla { get; set; }
        /// <summary>
        /// Obtiene o Establece el nombre de la columna primary key.
        /// </summary>
        String PkColumn { get; set; }
    }
    /// <summary>
    /// Interfaz para objetos de capa de datos con posibilidad de escritura.
    /// </summary>
    public interface IPutCommand : ICommand
    {
        /// <summary>
        /// M�todo para definici�n de escritura.
        /// </summary>
        void InitPutCommand();
        /// <summary>
        /// Obtiene o establece el SqlCommand de escritura.
        /// </summary>
        SqlCommand PutCommand { get; set; }
        /// <summary>
        /// Obtiene o establece la fila en memoria de la tabla del objeto de datos.
        /// </summary>
        DataRow Fila { get; set; }
        /// <summary>
        /// M�todo para validaci�n de filas en memoria.
        /// </summary>
        void ValidarFila();
    }
    /// <summary>
    /// Interfaz para objetos de capa de datos con posibilidad de lectura.
    /// </summary>
    public interface IGetCommand : ICommand
    {
        /// <summary>
        /// M�todo para definici�n de la lectura de datos.
        /// </summary>
        void InitGetCommand();
        /// <summary>
        /// Obtiene o Establece el SqlCommand de lectura del objeto de datos.
        /// </summary>
        SqlCommand GetCommand { get; set; }
    }
    /// <summary>
    /// Interfaz para objetos de capa de datos con posibilidad de eliminaci�n.
    /// </summary>
    public interface IDelCommand : ICommand
    {
        /// <summary>
        /// M�todo para definici�n de la eliminaci�n de datos.
        /// </summary>
        void InitDelCommand();
        /// <summary>
        /// Obtiene o establece el SqlCommand de eliminaci�n de datos.
        /// </summary>
        SqlCommand DelCommand { get; set; }
        /// <summary>
        /// Obtiene o establece el Primary Key con el que si dispara la eliminaci�n.
        /// </summary>
        Object DeltaKey { get; set; }
    }
}