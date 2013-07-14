using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ACAPackagesListener.API.Models.Repositories;
using ACAPackagesListener.API.http;
using TeamNotification_Library.Service.Http;

namespace acawebclient.ajax_service.reports
{
    public partial class GetParsedPackages : BasePage
    {
        IParsedPackageRepository parsedPackageRepository;
        protected void Page_Load(object sender, EventArgs e)
        {
            var serializer = new JSONSerializer();
            var serverResponse = new ServerResponse();
            var response = "";
            parsedPackageRepository= new NHParsedPackageRepository();
            try
            {
                if (!IsPostBack && !String.IsNullOrEmpty(Request.QueryString["StartDate"]))
                {
                    var maxDate = Convert.ToInt32(DateTime.Today.ToString("yyyyMMdd"));
                    var minDate = 19000101;
                    var startDate = 0;
                    var endDate = 0;
                    var mac = Request.QueryString["MAC"];
                    Int32.TryParse(Request.QueryString["StartDate"], out startDate);
                    Int32.TryParse(Request.QueryString["EndDate"], out endDate);
                    var parsedPackages = parsedPackageRepository.GetByMacAndDates(mac, startDate == 0 ? minDate : startDate, endDate==0?maxDate:endDate);

                    serverResponse.success = true;
                    serverResponse.addMessage("Devices returned successfully");
                    serverResponse.objectData = serializer.Serialize(parsedPackages);
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