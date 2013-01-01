using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Bll.AutoConsumerAnalisys;
using ACAPackagesListener.API.http;
using TeamNotification_Library.Service.Http;
using ACAPackagesListener.API.JSON;

namespace acawebclient.ajax_service.reports
{
    public partial class GetParsedPackages : BasePage
    {
        AdmParsedPackages admParsedPackages;
        protected void Page_Load(object sender, EventArgs e)
        {
            var serializer = new JSONSerializer();
            var serverResponse = new ServerResponse();
            var response = "";
            try
            {
                if (!IsPostBack && !String.IsNullOrEmpty(Request.QueryString["StartDate"]))
                {
                    admParsedPackages = AdmParsedPackages.Crear(ref this.Connection);
                    var startDate = 0;
                    var endDate = 0;
                    var mac = Request.QueryString["MAC"];
                    Int32.TryParse(Request.QueryString["StartDate"], out startDate);
                    Int32.TryParse(Request.QueryString["EndDate"], out endDate);
                    admParsedPackages.Buscar(mac, startDate, endDate, true);

                    serverResponse.success = true;
                    serverResponse.addMessage("Devices returned successfully");
                    var packagesCollection = new ParsedPackageCollection(admParsedPackages.GetTblLogic.Select());
                    serverResponse.objectData = serializer.Serialize(packagesCollection);
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