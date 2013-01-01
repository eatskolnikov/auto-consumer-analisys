using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Dal.AutoConsumerAnalisys;
using Lrrc.Sys.Data;
using Lrrc.Sys.Data.Exeception;

namespace Bll.AutoConsumerAnalisys
{
    public sealed class AdmParsedPackages : Logic
    {
        private ParsedPackages  fParsedPackage;
        internal AdmParsedPackages(Connection Connection, ParsedPackages parsedPackage)
        {
            GetConnection = Connection;
            fParsedPackage = parsedPackage;
        }
        public static AdmParsedPackages Crear(ref Connection connection)
        {
            var objParsedPackage = new ParsedPackages(ref connection);
            objParsedPackage.CargarEsquema();
            return new AdmParsedPackages(connection, objParsedPackage);
        }
        public static AdmParsedPackages Iniciar(ref Connection connection)
        {
            var objParsedPackage = new ParsedPackages(ref connection);
            return new AdmParsedPackages(connection, objParsedPackage);
        }

        public void Buscar(String MAC, Int32 StartDate, Int32 EndDate, bool UsarDataset)
        {
            try
            {
                GetTblLogic = fParsedPackage.Buscar(MAC, StartDate, EndDate, UsarDataset);
            }
            catch (Excepcion e)
            {
                throw new Excepcion(e.Message);
            }
        }
        public override void Sincronizar()
        {
            try
            {
                fParsedPackage.Actualizar(fParsedPackage);
            }
            catch (Excepcion e)
            {
                throw new Exception(e.Mensaje);
            }
        }

        public override void Destruir()
        {
            fParsedPackage.Refrescar();
        }
    }
}
