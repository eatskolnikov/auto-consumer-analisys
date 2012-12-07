using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ACAPackagesListener.API.JSON;
using ACAPackagesListener.API.http;
using Bll.AutoConsumerAnalisys;
using Lrrc.Sys.Data;
using TeamNotification_Library.Service.Http;

namespace acawebclient.ajax_service
{
    public partial class Devices : BasePage
    {
        private AdmDevices devices;
        protected void Page_Load(object sender, EventArgs e)
        {
            if(!IsPostBack)
            {
                devices = AdmDevices.Crear(ref Connection);
                var serializer = new JSONSerializer();
                var serverResponse = new ServerResponse();
                var response = "";
                try
                {
                    if(Request.QueryString.HasKeys())
                    {
                        var fields = Request.QueryString;
                        devices.Buscar(Int32.Parse(fields["DeviceId"]),true);
                        devices.Insertar(Int32.Parse(fields["DeviceId"]),
                                         devices.GetTblLogic.Rows[0]["Ip"].ToString(), fields["Description"],
                                         fields.Get("LatLng") == ""
                                             ? devices.GetTblLogic.Rows[0]["LatLng"].ToString()
                                             : fields.Get("LatLng"), true);
                        devices.Sincronizar();
                        serverResponse.success = true;
                        serverResponse.addMessage("Device updated successfully");
                        response = serializer.Serialize(serverResponse);
                    }
                    else
                    {
                        devices.Buscar(0, true);
                        serverResponse.success = true;
                        serverResponse.addMessage("Devices retrieved successfully");
                        var deviceCollection = new DeviceCollection(devices.GetTblLogic.Select());
                        serverResponse.objectData = serializer.Serialize(deviceCollection);
                        response = serializer.Serialize(serverResponse);
                    }
                }
                catch (Exception ex)
                {
                    serverResponse.success = false;
                    serverResponse.addMessage(ex.Message);
                }
                
                Response.Write(response);
            }
        }
    }
}