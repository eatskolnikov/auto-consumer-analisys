using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Collections;
using System.Data.SqlClient;

namespace Lrrc.Sys.Data
{
    /// <summary>
    /// Clase para permutación y combinación algebráica de cadenas de texto.
    /// </summary>
    public sealed class Permute
    {
        String _cnString,
            _sp_GetPermutadas,
            _sTabla,
            _sCampo,
            _sDescripcion,
            _sp_DelPermutadas,
            //_GetCommand,
            Tabla;

        Int32 _iCampoVal;

        SqlCommand cmd;

        DataTable DataT;
        
        SqlConnection cn;

        /// <summary>
        /// Contructor de la clase
        /// </summary>
        public Permute()
        {

        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="cnString"></param>
        /// <param name="sp_GetPermutadas"></param>
        public Permute(String cnString, String sp_GetPermutadas)
        {
            _cnString = cnString;

            _sp_GetPermutadas = sp_GetPermutadas;
        }
        /// <summary>
        /// Establece la dimensión de un arreglo. En caso de que los tokens excedan la dimensión límite se fija.
        /// </summary>
        /// <param name="Longitud">Logintud de caracteres</param>
        /// <returns></returns>
        private Int32 ArrayLength(Int32 Longitud)
        {
            Int32 Int32LenArray = 1,
                Int32Count = 0;

            while (Longitud - Int32Count > 0)
            {
                Int32LenArray += (Int32LenArray * (Longitud - Int32Count));

                Int32Count++;

                if (Int32LenArray > 40319)
                {
                    Int32LenArray = 40319;

                    break;
                }
            }

            return Int32LenArray;

        }
        /// <summary>
        /// Intercambia las palabras.
        /// </summary>
        /// <param name="Arreglo">Arreglo de datos</param>
        private void SwitchDigits(ref Int32[] Arreglo)
        {

            Int32 Int32Bound = Arreglo.Length - 1,
                Int32Ret = Arreglo[Int32Bound];

            Arreglo[Int32Bound] = Arreglo[Int32Bound - 1];

            Arreglo[Int32Bound - 1] = Int32Ret;
        }
        /// <summary>
        /// Agrega un elemento.
        /// </summary>
        /// <param name="Arreglo">Arreglo de datos</param>
        /// <param name="FromPosition">Posición inicial</param>
        /// <param name="ToPosition">Posición final</param>
        /// <param name="ValuePosition">Valor de la posición</param>
        /// <returns>Int32</returns>
        private Int32 AddElementValue(Int32[] Arreglo, Int32 FromPosition, Int32 ToPosition, Int32 ValuePosition)
        {
            Int32 Int32Position = 0;

            while (Int32Position != -1)
            {
                Int32Position = FindValue(Arreglo, FromPosition, ToPosition, ValuePosition);

                if (Int32Position != -1)
                    ValuePosition++;
            }

            return ValuePosition;
        }
        /// <summary>
        /// Obtiene el valor en una posición del arreglo.
        /// </summary>
        /// <param name="Arreglo">Arreglo de datos</param>
        /// <param name="StartPosition">Posición inicial</param>
        /// <param name="EndPosition">Posición final</param>
        /// <param name="Value">Valor</param>
        /// <returns>Int32</returns>
        private Int32 FindValue(Int32[] Arreglo, Int32 StartPosition, Int32 EndPosition, Int32 Value)
        {
            Int32 Int32Count,
                Int32Ret = -1;

            for (Int32Count = StartPosition; Int32Count <= EndPosition; Int32Count++)
            {
                if ((Int32)Arreglo[Int32Count] == Value)
                {
                    Int32Ret = Int32Count;

                    break;
                }
            }

            return Int32Ret;
        }
        /// <summary>
        /// Obtiene el reemplazo de nombres propios en lugar de abreviaturas de los mismos.
        /// </summary>
        /// <param name="Descripcion">Texto a evaluar</param>
        /// <returns>String</returns>
        private String RemoverAbreviaturas(String Descripcion)
        {
            Descripcion = Descripcion.Replace(".", "");

            Descripcion = Descripcion.ToLower() + " ";

            Descripcion = Descripcion.Replace("js ", "jesus");

            Descripcion = Descripcion.Replace("ml ", "manuel");

            Descripcion = Descripcion.Replace("lpez ", "lopez");

            Descripcion = Descripcion.Replace("gmez ", "gomez");

            Descripcion = Descripcion.Replace("jmez ", "jimenez");

            Descripcion = Descripcion.Replace("hdez ", "hernandez");

            Descripcion = Descripcion.Replace("fdez ", "fernandez");

            Descripcion = Descripcion.Replace("glez ", "gonzalez");

            Descripcion = Descripcion.Replace("fdo ", "fernando");

            Descripcion = Descripcion.Replace("fco ", "francisco");

            Descripcion = Descripcion.Replace("stgo ", "santiago");

            Descripcion = Descripcion.Replace("dgo ", "domingo");

            Descripcion = Descripcion.Replace("ant ", "antonio");

            Descripcion = Descripcion.Replace("alt ", "altagracia");

            return Descripcion;
        }
        /// <summary>
        /// Obtiene el fonético de una cadena de caracteres suministrada.
        /// </summary>
        /// <param name="Descripcion">Cadena de caracteres.</param>
        /// <returns>String</returns>
        public String GetPhonetic(String Descripcion)
        {
            String fAscii;

            Descripcion = RemoverAbreviaturas(Descripcion);

            Descripcion = Descripcion.ToLower() + " ";
            
            Descripcion = Descripcion.Replace("á", "a");
            
            Descripcion = Descripcion.Replace("é", "e");
            
            Descripcion = Descripcion.Replace("í", "i");
            
            Descripcion = Descripcion.Replace("ó", "o");
            
            Descripcion = Descripcion.Replace("ú", "u");

            for (Byte Index = 2; Index < 32; Index++)
            {
                fAscii = Char.ConvertFromUtf32(Index);

                Descripcion = Descripcion.Replace(fAscii, "");
            }
            for (Byte Index = 33; Index < 65; Index++)
            {
                fAscii = Char.ConvertFromUtf32(Index);

                Descripcion = Descripcion.Replace(fAscii, "");
            }
            for (Byte Index = 123; Index < 128; Index++)
            {
                fAscii = Char.ConvertFromUtf32(Index);

                Descripcion = Descripcion.Replace(fAscii, "");
            }

            Descripcion = Descripcion.Replace(" k", " c");

            if (Descripcion.Substring(0, 1) == "k")
                Descripcion = "c" + Descripcion.Substring(1);

            Descripcion = Descripcion.Replace(" de ", " ");
            
            Descripcion = Descripcion.Replace(" del ", " ");
            
            Descripcion = Descripcion.Replace(" la ", " ");
            
            Descripcion = Descripcion.Replace(" las ", " ");
            
            Descripcion = Descripcion.Replace(" lo ", " ");
            
            Descripcion = Descripcion.Replace(" los ", " ");
            
            Descripcion = Descripcion.Replace("chk", "c");
            
            Descripcion = Descripcion.Replace("ce", "se");
            
            Descripcion = Descripcion.Replace("ci", "si");
            
            Descripcion = Descripcion.Replace("que", "qe");
            
            Descripcion = Descripcion.Replace("qui", "qi");
            
            Descripcion = Descripcion.Replace("ck", "q");
            
            Descripcion = Descripcion.Replace("v", "b");
            
            Descripcion = Descripcion.Replace("w", "u");
            
            Descripcion = Descripcion.Replace("y", "i");
            
            Descripcion = Descripcion.Replace("z", "s");
            
            Descripcion = Descripcion.Replace("x", "s");
            
            Descripcion = Descripcion.Replace("ñ", "n");
            
            Descripcion = Descripcion.Replace("ph", "f");
            
            Descripcion = Descripcion.Replace("g", "j");
            
            Descripcion = Descripcion.Replace("k", "c");

            for (Byte Index = 98; Index < 100; Index++)
            {
                fAscii = Char.ConvertFromUtf32(Index);

                Descripcion = Descripcion.Replace(fAscii + fAscii, fAscii);
            }
            for (Byte Index = 102; Index < 104; Index++)
            {
                fAscii = Char.ConvertFromUtf32(Index);

                Descripcion = Descripcion.Replace(fAscii + fAscii, fAscii);
            }
            for (Byte Index = 106; Index < 110; Index++)
            {
                fAscii = Char.ConvertFromUtf32(Index);

                Descripcion = Descripcion.Replace(fAscii + fAscii, fAscii);
            }
            for (Byte Index = 112; Index < 116; Index++)
            {
                fAscii = Char.ConvertFromUtf32(Index);

                Descripcion = Descripcion.Replace(fAscii + fAscii, fAscii);
            }
            for (Byte Index = 118; Index < 122; Index++)
            {
                fAscii = Char.ConvertFromUtf32(Index);

                Descripcion = Descripcion.Replace(fAscii + fAscii, fAscii);
            }

            Descripcion = Descripcion.Replace("aa", "a");
            
            Descripcion = Descripcion.Replace("ee", "e");
            
            Descripcion = Descripcion.Replace("ii", "i");
            
            Descripcion = Descripcion.Replace("oo", "o");
            
            Descripcion = Descripcion.Replace("uu", "u");

            if (Descripcion.IndexOf(" sc") > 0)
                Descripcion = Descripcion.Replace(" sc", "c");

            Int32 fPos = Descripcion.IndexOf("rp");

            if (fPos > 2)
                Descripcion = Descripcion.Substring(0, fPos) + "lp" + Descripcion.Substring(fPos + 2);

            Descripcion = Descripcion.Replace("air ", "");
            
            Descripcion = Descripcion.Replace("and ", "");
            
            Descripcion = Descripcion.Replace("ant ", "");
            
            Descripcion = Descripcion.Replace("eau", "");
            
            Descripcion = Descripcion.Replace("eo", "");
            
            Descripcion = Descripcion.Replace("ou", "");
            
            Descripcion = Descripcion.Replace("ea", "");
            
            Descripcion = Descripcion.Replace("dj", "");
            
            Descripcion = Descripcion.Replace("h", "");
            
            Descripcion = Descripcion.Replace("s ", " ");
            
            Descripcion = Descripcion.Replace("t ", " ");
            
            Descripcion = Descripcion.Replace("r ", " ");
            
            Descripcion = Descripcion.Replace("d ", " ");
            
            Descripcion = Descripcion.Replace("enm", "em");
            
            Descripcion = Descripcion.Replace("  ", " ");
            
            Descripcion = Descripcion.Replace("ts", "s");
            
            return Descripcion.Trim();
        }
        /// <summary>
        /// Procesa, algebráicamente, un String y retorna una objeto ArrayList con las combinaciones permutadas.
        /// </summary>
        /// <param name="Descripcion">Texto por permutar y combinar</param>
        /// <returns>ArrayList</returns>
        public ArrayList ExecPermute(String Descripcion)
        {
            Descripcion = Descripcion.Replace("  ", " ");

            Int32 fCount;

            String[] fArreglo;

            fArreglo = Descripcion.Split(' ');

            if (fArreglo.Length == 1)
            {
                ArrayList fArrListReturn = new ArrayList();

                fArrListReturn.Add(Descripcion);

                return fArrListReturn;
            }

            Int32 fUpperBound = fArreglo.Length - 1,
                fLenght,
                fPrimaryCount;

            Boolean fSwitch = false,
                fControl = true;

            String fString = "";

            if (fUpperBound > 7)
            {
                fLenght = 5039;

                fUpperBound = 7;
            }
            else
                fLenght = ArrayLength(fUpperBound) - 1;

            ArrayList fArrListResult = new ArrayList();

            Int32[] fArrListElement = new Int32[fUpperBound + 1];

            Int32 fPosition = fUpperBound - 2;

            if (fPosition < 0)
                fPosition = 0;

            for (fCount = 0; fCount <= fUpperBound; fCount++)
            {
                fArrListElement[fCount] = fCount;
            }

            for (fPrimaryCount = 0; fPrimaryCount <= fLenght; fPrimaryCount++)
            {
                for (fCount = 0; fCount <= fUpperBound; fCount++)
                {
                    fString += fArreglo[fArrListElement[fCount]] + " ";
                }

                fArrListResult.Add(fString);

                fString = "";

                fPosition = (fUpperBound - 2);

                if (fPosition < 0)
                    fPosition = 0;

                if (!fSwitch)
                {
                    SwitchDigits(ref fArrListElement);

                    fSwitch = true;
                }
                else
                {
                    if (fPrimaryCount < fLenght)
                    {
                        while (fControl)
                        {
                            fArrListElement[fPosition] = AddElementValue(fArrListElement, 0, fPosition, fArrListElement[fPosition]);

                            if (fArrListElement[fPosition] > fUpperBound)
                            {
                                fPosition--;

                                if (fPosition == -1)
                                    break;
                            }
                            else
                                fControl = false;
                        }

                        fControl = true;

                        for (fCount = fPosition + 1; fCount <= fUpperBound; fCount++)
                        {
                            fArrListElement[fCount] = AddElementValue(fArrListElement, 0, fPosition + (fCount - (fPosition + 1)), 0);
                        }

                        fSwitch = false;
                    }
                }
            }

            return fArrListResult;
        }
        /// <summary>
        /// Constructor del permutador.
        /// </summary>
        /// <param name="cnString">String de Conexión</param>
        /// <param name="sp_GetPermutadas">Stored Procedure de Permutaciones</param>
        /// <param name="sTabla">Nombre de la Tabla</param>
        /// <param name="sCampo">Nombre de la Columna</param>
        /// <param name="iCampoVal">Nombre del Campo</param>
        /// <param name="sDescripcion">Descripción</param>
        /// <param name="gDelete">Stored Procedure de Eliminación</param>
        public void setPermutacion(String cnString, String sp_GetPermutadas, String sTabla, String sCampo, Int32 iCampoVal,
            String sDescripcion, String gDelete)
        {
            _cnString = cnString;
            _sp_GetPermutadas = sp_GetPermutadas;
            _sTabla = sTabla;
            _sCampo = sCampo;
            _sDescripcion = sDescripcion;
            _iCampoVal = iCampoVal;
            _sp_DelPermutadas = gDelete;

            Set(_sTabla, _sCampo, _iCampoVal, _sDescripcion);

        }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public DataTable CargarEsquema()
        {
            cmd = new SqlCommand();

            FillCommandParameters(ref cmd, "@Schema", "1");

            return FillDataTable(cmd, _sp_GetPermutadas);
        }
        private void FillCommandParameters(ref SqlCommand Command, String ParamName, String ParamValue)
        {
            Command.Parameters.AddWithValue(ParamName, ParamValue);
        }
        private DataTable FillDataTable(SqlCommand Command, String CommandText)
        {
            cn = new SqlConnection();
            SqlDataAdapter da;
            DataTable dt = new DataTable();
            //
            cn.ConnectionString = _cnString;
            cn.Open();
            //
            Command.Connection = cn;
            Command.CommandType = CommandType.StoredProcedure;
            Command.CommandText = _sp_GetPermutadas;
            //
            da = new SqlDataAdapter(Command);
            try
            {
                da.Fill(dt);
            }
            catch (SqlException eSQL)
            {
                throw eSQL;
            }
            catch (Exception e)
            {
                throw e;
            }
            finally
            {
                cn.Close();
            }

            return dt;
        }
        private void Set(String TableName, String FieldName, Int32 FieldId, String Descripcion)
        {
            try
            {
                if (String.IsNullOrEmpty(FieldName))
                    throw new Exception("El nombre de la Columna " + FieldName + " de relación se encuentra vacío.");
                if (FieldId == 0)
                    throw new Exception("El código de la Columna " + FieldName + " de relación debe ser mayor a cero.");
                if (String.IsNullOrEmpty(Descripcion))
                    throw new Exception("El dato Descripción se encuentra vacío.");

                Tabla = TableName;

                DataT = CargarEsquema();

                Delete();

                ArrayList fList = new ArrayList();

                fList = ExecPermute(Descripcion);

                for (Int32 Index = 0; Index < fList.Count; Index++)
                {
                    DataRow dr = DataT.NewRow();

                    dr[FieldName] = FieldId;
                    dr["Descripcion"] = fList[Index];
                    dr["Activo"] = true;

                    DataT.Rows.Add(dr);
                }

                if (cn.State == ConnectionState.Closed)
                    cn.Open();

                SqlBulkCopy fSqlBulkCopy = new SqlBulkCopy(cn);

                SqlBulkCopyColumnMapping mapping;

                foreach (DataColumn col in DataT.Columns)
                {
                    mapping = new SqlBulkCopyColumnMapping(col.ColumnName, col.ColumnName);
                    fSqlBulkCopy.ColumnMappings.Add(mapping);
                }

                fSqlBulkCopy.BatchSize = 100;

                fSqlBulkCopy.DestinationTableName = Tabla;

                fSqlBulkCopy.WriteToServer(DataT);

                fSqlBulkCopy.Close();

                fList = null;

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        private void Delete()
        {
            cmd = new SqlCommand();

            FillCommandParameters(ref cmd, "@" + _sCampo, _iCampoVal.ToString());

            FillDataTable(cmd, _sp_DelPermutadas);
        }
    }
}