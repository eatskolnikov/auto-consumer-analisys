using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Bll.AutoConsumerAnalisys;

namespace acawebclient.ajax_service.forms
{
    public partial class DeviceForm : BasePage
    {
        private AdmDevices devices;
        protected void Page_Load(object sender, EventArgs e)
        {
            if(Request.QueryString.HasKeys() && Request.QueryString["DeviceId"] != "")
            {
                var deviceId = Convert.ToInt32(Request.QueryString["DeviceId"]);
                if(deviceId == 0)
                {
                    devices = AdmDevices.Crear(ref Connection);

                }
                else
                if(deviceId  > 0)
                {
                    devices = AdmDevices.Crear(ref Connection);
                    devices.Buscar(deviceId, true);
                    var row = devices.GetTblLogic.Rows[0];
                    tbxDescription.InnerText = row["Description"].ToString();
                    tbxIp.Value = row["Ip"].ToString();
                    tbxLatLng.Value = row["LatLng"].ToString();
                    hdnDeviceId.Value = row["DeviceId"].ToString();
                }
            }            
        }

        protected void btnUpdate_OnClick(object sender, EventArgs e)
        {
            devices = AdmDevices.Crear(ref Connection);
            devices.Insertar(Convert.ToInt32(hdnDeviceId.Value), tbxIp.Value,tbxDescription.Value,tbxLatLng.Value,true);
        }

        protected void btnDelete_OnClick(object sender, EventArgs e)
        {
            throw new NotImplementedException();
        }
    }
}