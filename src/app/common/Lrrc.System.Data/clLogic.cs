using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using System.Threading;
using Lrrc.Sys.Data;
using Lrrc.Sys.Data.Exeception;
using Lrrc.Sys.Data.Interface;

namespace Lrrc.Sys.Data
{
    /// <summary>
    /// Clase abstracta para la definición y uso en la capa de negocios.
    /// </summary>
    public abstract class Logic
    {
        private Connection fConnection;

        private DataTable fTblLogic;
        /// <summary>
        /// Escribir la implementación de este método en las clases hijas en donde deberán de definirse
        /// la lógica y llamadas de actualización al motor de base de datos.
        /// </summary>
        public abstract void Sincronizar();
        /// <summary>
        /// Escribir la implementación de este método en las clases hijas en donde deberán de definirse
        /// la lógica, destrucción y liberación de los objetos y data de la memoria.
        /// </summary>
        public abstract void Destruir();
        /// <summary>
        /// Obtiene o establece el objeto Connection.
        /// </summary>
        public Connection GetConnection
        {
            get { return fConnection; }
            set { fConnection = value; }
        }
        /// <summary>
        /// Obtiene o establece el objeto DataTable de la capa de datos.
        /// </summary>
        public DataTable GetTblLogic
        {
            get
            {
                if (fTblLogic == null)
                    new DataTable();
                return
                    fTblLogic;
            }
            set { fTblLogic = value; }
        }
    }
}