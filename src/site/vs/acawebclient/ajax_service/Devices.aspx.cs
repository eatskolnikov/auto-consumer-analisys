using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ACAPackagesListener.API.Models.Entities;
using ACAPackagesListener.API.Models.Repositories;
using ACAPackagesListener.API.http;
using TeamNotification_Library.Service.Http;

namespace acawebclient.ajax_service
{
    public partial class Devices : BasePage
    {
        private IDeviceRepository deviceRepository;
        protected void Page_Load(object sender, EventArgs e)
        {
            deviceRepository = new NHDeviceRepository();
            if(!IsPostBack)
            {

                var serializer = new JSONSerializer();
                var serverResponse = new ServerResponse();
                var response = "";
                try
                {
                    if(Request.QueryString.HasKeys())
                    {
                        var fields = Request.QueryString;

                        if (Int32.Parse(fields["DeviceId"]) > 0)
                        {
                            var device = deviceRepository.GetById(Int32.Parse(fields["DeviceId"]));
                            device.Description = fields.Get("Description");
                            if (!String.IsNullOrEmpty(fields.Get("LatLng")))
                                device.LatLng = fields.Get("LatLng");
                        }
                        else
                        {
                            deviceRepository.Add(new Device
                                {
                                    Ip = "0.0.0.0",
                                    Description = fields.Get("Description"),
                                    LatLng = fields.Get("LatLng"),
                                    Activo =  true
                                });
                        }
                        serverResponse.success = true;
                        serverResponse.addMessage("Device updated successfully");
                        response = serializer.Serialize(serverResponse);
                    }
                    else
                    {
                        var devices = deviceRepository.GetAll();
                        serverResponse.success = true;
                        serverResponse.addMessage("Devices retrieved successfully");
                        serverResponse.objectData = serializer.Serialize(devices);
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