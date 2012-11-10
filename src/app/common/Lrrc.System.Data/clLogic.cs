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
    /// Clase abstracta para la definici�n y uso en la capa de negocios.
    /// </summary>
    public abstract class Logic
    {
        private Connection fConnection;

        private DataTable fTblLogic;
        /// <summary>
        /// Escribir la implementaci�n de este m�todo en las clases hijas en donde deber�n de definirse
        /// la l�gica y llamadas de actualizaci�n al motor de base de datos.
        /// </summary>
        public abstract void Sincronizar();
        /// <summary>
        /// Escribir la implementaci�n de este m�todo en las clases hijas en donde deber�n de definirse
        /// la l�gica, destrucci�n y liberaci�n de los objetos y data de la memoria.
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