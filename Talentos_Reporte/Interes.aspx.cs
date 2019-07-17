using System;
using System.Web.UI;
using System.Web.Script.Serialization;
using wsInteres;
using System.Configuration;
using System.Net;

public partial class Interes : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            if (Request.QueryString.Count > 0)
            {
                string json = "";
                JavaScriptSerializer js = new JavaScriptSerializer();
                ReporteBE objInteresBE = new ReporteBE();
                
                string urlReporte = ConfigurationSettings.AppSettings["URL_REPORTE"].ToString();
                lblMensaje.Visible = false;

                try
                {
                    string id = Request.QueryString["id"].ToString();
                    string token = Request.QueryString["token"].ToString();
                    string urljson = ConfigurationManager.AppSettings["URL_JSON"].ToString();
                    string url = urljson + id + "?token=" + token;
                    var syncClient = new WebClient();
                    json = syncClient.DownloadString(url);
                }
                catch (Exception ex)
                {
                    redirect("Interes.aspx", "Error al descargar los datos del id ó token");
                    //throw;
                }

                if (json.Length > 20)
                {
                    wsInteres.wsInteres objInteres = new wsInteres.wsInteres();

                    string nombrePDF = "";
                    try
                    {
                        objInteresBE = js.Deserialize<ReporteBE>(json);
                        nombrePDF = objInteres.CalcularReporteIntereses(objInteresBE);
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

                        redirect("Interes.aspx", "Ocurrio un error al obtener el Id y Token.");
                        //throw;
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
