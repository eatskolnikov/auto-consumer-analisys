using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Lrrc.Sys.Data
{
    /// <summary>
    /// Objeto para la creación de secuencias Ascii ordenadas ascendentemente.
    /// </summary>
    public sealed class Secuencia
    {
        private Permute fPermute;

        /// <summary>
        /// Constructor del objeto de secuencias Ascii ordenadas ascendentemente.
        /// </summary>
        public Secuencia()
        {
        }
        /// <summary>
        /// Produce y retorna un arreglo alfanumérico de valores Ascii a partir de la descripción proporcionada.
        /// </summary>
        /// <param name="Descripcion">Descripción por Convertir</param>
        /// <returns>Byte[]</returns>
        public Byte[] GetSecuencia(String Descripcion)
        {
            fPermute = new Permute();

            Descripcion = fPermute.GetPhonetic(Descripcion);

            Descripcion = Descripcion.Replace(" ", "");

            Byte[] fArrAscii = Encoding.ASCII.GetBytes(Descripcion);

            Array.Sort(fArrAscii);

            return fArrAscii;
        }
    }
}
