using System;

namespace acawebclient.ajax_service.forms
{
    public partial class DeviceForm : BasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (IsPostBack) return;
            Page.ClientScript.GetPostBackEventReference(btnUpdate, string.Empty);
            if (!Request.QueryString.HasKeys() || Request.QueryString["DeviceId"] == "") return;
            var deviceId = Convert.ToInt32(Request.QueryString["DeviceId"]);
            if (deviceId <= 0) return;
            var device = DeviceRepository.GetById(deviceId);
            tbxDescription.InnerText = device.Description;
            tbxIp.Value = device.Ip;
            tbxLatLng.Value = device.LatLng;
            hdnDeviceId.Value = device.DeviceId.ToString();
        }

        protected void btnUpdate_OnClick(object sender, EventArgs e)
        {
            var device = DeviceRepository.GetById(Convert.ToInt32(hdnDeviceId.Value));
            device.Ip = tbxIp.Value;
            device.Description = tbxDescription.Value;
            device.LatLng = tbxLatLng.Value;
            DeviceRepository.Update(device);
        }

        protected void btnDelete_OnClick(object sender, EventArgs e)
        {
            var device = DeviceRepository.GetById(Convert.ToInt32(hdnDeviceId.Value));
            DeviceRepository.Remove(device);
        }
    }
}