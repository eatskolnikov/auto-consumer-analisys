using System;
using System.Collections.Generic;
using System.Text;

namespace Lrrc.Sys.Data.Exeception
{
    /// <summary>
    /// Administración de Excepciones.
    /// Author: Luis R. Romero Castillo
    /// Owner: Luis R. Romero Castillo
    /// </summary>
    public class Excepcion : Exception
    {
        private String fMessage;
        
        private Int32 fNumber;

        /// <summary>
        /// Constructor de la excepción.
        /// </summary>
        /// <param name="Message">Mensaje de excepción</param>
        /// <param name="ExBase">Excepción base</param>
        /// <param name="Number">Número de excepción</param>
        public Excepcion(String Message, Exception ExBase, Int32 Number)
            : base(Message, ExBase)
        {
            Mensaje = Message;
            
            Numero = Number;
        }
        /// <summary>
        /// Constructor de excepción.
        /// </summary>
        /// <param name="Message"></param>
        /// <param name="Number"></param>
        public Excepcion(String Message, Int32 Number)
            : base(Message)
        {
            Mensaje = Message;
            Numero = Number;
        }
        /// <summary>
        /// Constructor de excepción.
        /// </summary>
        /// <param name="Message"></param>
        public Excepcion(String Message)
            : base(Message)
        {
            Mensaje = Message;
        }
        /// <summary>
        /// Obtiene o Establece el Mensaje de la excepción.
        /// </summary>
        public String Mensaje
        {
            get { return fMessage; }
            set { fMessage = value; }
        }
        /// <summary>
        /// Obtiene o establece el Número de la excepción.
        /// </summary>
        public Int32 Numero
        {
            get { return fNumber; }
            set { fNumber = value; }
        }
    }
}