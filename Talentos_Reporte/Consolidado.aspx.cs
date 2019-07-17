using System;
using System.Web.UI;
using System.Web.Script.Serialization;
using System.Configuration;
using wsConsolidatedReport;
using System.Net;

public partial class Consolidado : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {

            if (Request.QueryString.Count > 0)
            {
                string id = "";
                string jsonTemperamentos = "";
                string jsonInteres = "";
                JavaScriptSerializer js = new JavaScriptSerializer();
                ReporteTemperamentoBE objTemperamentoBE = new ReporteTemperamentoBE();
                ReporteBE objInteresBE = new ReporteBE();
                string urlReporte = ConfigurationSettings.AppSettings["URL_REPORTE"].ToString();
                lblMensaje.Visible = false;

                try
                {

                    id = Request.QueryString["id"].ToString();
                    string token = Request.QueryString["token"].ToString();
                    string url_jsonInteres = ConfigurationManager.AppSettings["URL_JSON"].ToString();
                    string url = url_jsonInteres + id + "?token=" + token;

                    var syncClient = new WebClient();
                    jsonInteres = syncClient.DownloadString(url);
                    
                }
                catch (Exception ex)
                {
                    redirect("Consolidado.aspx", "Error al descargar los datos del id ó token");
                }

                if (jsonInteres.Length > 20)
                {
                    wsConsolidatedReport.wsConsolidatedReport objReport = new wsConsolidatedReport.wsConsolidatedReport();
                    string nombrePDF = "";
                    try
                    {
                        objInteresBE = js.Deserialize<ReporteBE>(jsonInteres);


                        string url_jsonTemperamentos = ConfigurationManager.AppSettings["URL_JSON_TEMPERAMENTOS"].ToString();
                        string urltemp = url_jsonTemperamentos + objInteresBE.report.encuesta.id + "?token=" + objInteresBE.report.encuesta.token;
                        var syncClient2 = new WebClient();
                        jsonTemperamentos = syncClient2.DownloadString(urltemp);

                        objTemperamentoBE = js.Deserialize<ReporteTemperamentoBE>(jsonTemperamentos);
                        

                        nombrePDF = objReport.GenerarReporte(id, objTemperamentoBE, objInteresBE);
                        string filePath = urlReporte + nombrePDF;

                        WebClient User = new WebClient();
                        Byte[] FileBuffer = User.DownloadData(filePath);
                        if (FileBuffer != null)
                        {
                            Response.ContentType = "application/pdf";
                            Response.AddHeader("content-length", FileBuffer.Length.ToString());
                            Response.BinaryWrite(FileBuffer);
                        }
                    }
                    catch (Exception ex)
                    {

                        redirect("Consolidado.aspx", "Ocurrio un error al obtener el Id y Token.");
                    }
                }
            }

        }

    }

    private void redirect(string url, string mensaje)
    {
        //lblMensaje.Text = mensaje;
        //lblMensaje.Visible = true;
        Response.Redirect("~/" + url);
    }

}