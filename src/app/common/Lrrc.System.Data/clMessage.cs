using System;
using System.Collections.Generic;
using System.Text;

namespace Lrrc.Sys.Data
{
    /// <summary>
    /// Clase para definición de mensajes de error previamente definidos.
    /// </summary>
    public sealed class Message
    {
        /// <summary>
        /// Mensaje de control del DataSet.
        /// </summary>
        public const String Err_DataSet_Msg = "Error tratando de manejar el set de datos.";
        /// <summary>
        /// Mensaje de control para parámetros faltantes.
        /// </summary>
        public const String Err_MissingsParams_Msg = "Se ha detectado la falta de uno de los parámetros de control de errores.";
        /// <summary>
        /// Mensaje de control para parámetros faltantes.
        /// </summary>
        public const String Err_EmptyParams_Msg = "Se ha detectado la falta de inicialización de los parámetros de control de errores en stored procedure.";
        /// <summary>
        /// Mensaje de control para errores de intentos de conexión.
        /// </summary>
        public const String Err_OpeningConnection_Msg = "Fallo tratando de establecer la conexión al motor de datos.";
        /// <summary>
        /// Mensaje de control cuando se trata de cerrar una conexión.
        /// </summary>
        public const String Err_ClosingConnection_Msg = "Fallo tratando de cerrar la conexión al motor de datos.";
        /// <summary>
        /// Mensaje de control tratando de insertar un registro en el datase de memoria.
        /// </summary>
        public const String Err_InsertingRow_Msg = "Error tratando de insertar el nuevo registro.";
        /// <summary>
        /// Mensaje de control tratando de insertar una fila existente, de acuerdo a la condición de evaluación,
        /// en el set de datos.
        /// </summary>
        public const String Err_DuplicatedRow_Msg = "El nuevo registro no pudo ser insertado porque existe uno con la misma llave.";
        /// <summary>
        /// Mensaje de control cuando se trata de borrar una fila que tiene dependencias.
        /// </summary>
        public const String Err_RowDependencies_Msg = "El registro no puede ser eliminado como resultado de que tiene dependencias.";
        /// <summary>
        /// Mensaje de control cuando la condición se encuentra vacía en el objeto de la capa de datos.
        /// </summary>
        public const String Err_EmptyCondition_Msg = "Fallo tratando de realizar la búsqueda o registro no encontrado";
        /// <summary>
        /// Mensaje de control cuando la llave primera sufre alguna violación.
        /// </summary>
        public const String Err_LavePrimaria_Msg = "Error en llave primaria del objeto de la capa de datos.";
        /// <summary>
        /// Mensaje de control cuando se trata de dejar una columna con un valor null no permitido.
        /// </summary>
        public const String Err_Null_Msg = "La columna no permite valores nulos.";
        /// <summary>
        /// Mensaje de control cuando se trata de dejar una columna con un valor null o cero.
        /// </summary>
        public const String Err_Cero_Msg = "La columna no permite valores nulos o en cero.";

        /// <summary>
        /// Número de control del DataSet.
        /// </summary>
        public const Int32 Err_DataSet = 1;
        /// <summary>
        /// Número de control para parámetros faltantes.
        /// </summary>
        public const Int32 Err_MissingsParams = 2;
        /// <summary>
        /// Número de control para parámetros faltantes.
        /// </summary>
        public const Int32 Err_EmptyParams = 3;
        /// <summary>
        /// Número de control para errores de intentos de conexión.
        /// </summary>
        public const Int32 Err_OpeningConnection = 4;
        /// <summary>
        /// Número de control cuando se trata de cerrar una conexión.
        /// </summary>
        public const Int32 Err_ClosingConnection = 5;
        /// <summary>
        /// Número de control tratando de insertar un registro en el datase de memoria.
        /// </summary>
        public const Int32 Err_InsertingRow = 6;
        /// <summary>
        /// Número de control tratando de insertar una fila existente, de acuerdo a la condición de evaluación,
        /// en el set de datos.
        /// </summary>
        public const Int32 Err_DuplicatedRow = 7;
        /// <summary>
        /// Número de control cuando se trata de borrar una fila que tiene dependencias.
        /// </summary>
        public const Int32 Err_RowDependencies = 8;
        /// <summary>
        /// Número de control cuando la condición se encuentra vacía en el objeto de la capa de datos.
        /// </summary>
        public const Int32 Err_EmptyCondition = 9;
        /// <summary>
        /// Número de control cuando la llave primera sufre alguna violación.
        /// </summary>
        public const Int32 Err_LavePrimaria = 10;
        /// <summary>
        /// Número de control cuando se trata de dejar una columna con un valor null no permitido.
        /// </summary>
        public const Int32 Err_Null = 11;
        /// <summary>
        /// Número de control cuando se trata de dejar una columna con un valor null o cero.
        /// </summary>
        public const Int32 Err_Cero = 12;
    }
}
