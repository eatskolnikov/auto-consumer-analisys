using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Dal.AutoConsumerAnalisys;
using Lrrc.Sys.Data;
using Lrrc.Sys.Data.Exeception;

namespace Bll.AutoConsumerAnalisys
{
    public sealed class AdmDevices : Logic
    {
        private Devices fDevices;
        internal AdmDevices(Connection Connection, Devices devices)
        {
            GetConnection = Connection;
            fDevices = devices;
        }
        public static AdmPackages Crear(ref Connection connection)
        {
            var objPackage = new Packages(ref connection);
            objPackage.CargarEsquema();
            return new AdmPackages(connection, objPackage);
        }
        public void Insertar(Int32 DeviceId, String Ip, String Description, String LonLat, Boolean activo)
        {
            try
            {
                if (DeviceId == 0)
                    fDevices.NuevaFila();
                else
                    fDevices.ObtenerFila(DeviceId);

                fDevices.Fila["Accion"] = (DeviceId == 0) ? 1 : 2;
                fDevices.Fila["Ip"] = Ip;
                fDevices.Fila["Description"] = Description;
                fDevices.Fila["LonLat"] = LonLat;
                fDevices.Fila["Activo"] = activo;

                if (DeviceId == 0)
                    fDevices.InsertarFila();

                GetTblLogic = fDevices.GetTabla;
            }
            catch(Excepcion e)
            {
                throw new Excepcion(e.Message);
            }
        }
        public void Borrar(Int32 DeviceId)
        {
            try
            {
                fDevices.EliminarFila(DeviceId);
                GetTblLogic = fDevices.GetTabla;
            }
            catch (Excepcion Error)
            {
                throw new Excepcion(Error.Mensaje);
            }
        }

        public override void Sincronizar()
        {
            try{
                fDevices.Actualizar(fDevices);
            }catch(Excepcion e){
                throw new Exception(e.Mensaje);
            }
        }

        public override void Destruir()
        {
            fDevices.Refrescar();
        }
    }
}
