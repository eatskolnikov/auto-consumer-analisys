using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Dal.AutoConsumerAnalisys;
using Lrrc.Sys.Data;
using Lrrc.Sys.Data.Exeception;

namespace Bll.AutoConsumerAnalisys
{
    public sealed class AdmPackages : Logic
    {
        private Packages fPackage;
        internal AdmPackages(Connection Connection, Packages package)
        {
            GetConnection = Connection;
            fPackage = package;
        }
        public static AdmPackages Crear(ref Connection connection)
        {
            var objPackage = new Packages(ref connection);
            objPackage.CargarEsquema();
            return new AdmPackages(connection, objPackage);
        }
        public void Insertar(Int32 PackageId, String Ip, String message, Boolean parsed, Boolean activo)
        {
            try
            {
                if(PackageId == 0)
                    fPackage.NuevaFila();
                else
                    fPackage.ObtenerFila(PackageId);

                fPackage.Fila["Accion"] = (PackageId == 0)?1:2;
                fPackage.Fila["Ip"] = Ip;
                fPackage.Fila["Message"] = message;
                fPackage.Fila["Parsed"] = parsed;
                fPackage.Fila["Activo"] = activo;

                if(PackageId == 0)
                    fPackage.InsertarFila();

                GetTblLogic = fPackage.GetTabla;
            }
            catch(Excepcion e)
            {
                throw new Excepcion(e.Message);
            }
        }
        public void Borrar(Int32 PackageId)
        {
            try
            {
                fPackage.EliminarFila(PackageId);
                GetTblLogic = fPackage.GetTabla;
            }
            catch (Excepcion Error)
            {
                throw new Excepcion(Error.Mensaje);
            }
        }
        public override void Sincronizar()
        {
            try{
                fPackage.Actualizar(fPackage);
            }catch(Excepcion e)
            {
                throw new Exception(e.Mensaje);
            }
        }

        public override void Destruir()
        {
            fPackage.Refrescar();
        }
    }
}
