using System;
using System.Web.UI;
using System.Web.Script.Serialization;
using System.Configuration;
using wsTemperamentos;
using System.Net;


public partial class Temperamentos : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {

            if (Request.QueryString.Count > 0)
            {
                string json = "";
                JavaScriptSerializer js = new JavaScriptSerializer();
                ReporteTemperamentoBE objTemperamentoBE = new ReporteTemperamentoBE();
                string urlReporte = ConfigurationSettings.AppSettings["URL_REPORTE"].ToString();
                lblMensaje.Visible = false;

                try
                {

                    string id = Request.QueryString["id"].ToString();
                    string token = Request.QueryString["token"].ToString();
                    string urljson = ConfigurationManager.AppSettings["URL_JSON_TEMPERAMENTOS"].ToString();
                    string url = urljson + id + "?token=" + token;
                    var syncClient = new WebClient();
                    json = syncClient.DownloadString(url);
                }
                catch (Exception ex)
                {
                    redirect("Temperamentos.aspx", "Error al descargar los datos del id ó token");
                }

                if (json.Length > 20)
                {
                    wsTemperamentos.wsTemperamentos objTemperamento = new wsTemperamentos.wsTemperamentos();
                    string nombrePDF = "";
                    try
                    {
                        objTemperamentoBE = js.Deserialize<ReporteTemperamentoBE>(json);
                        nombrePDF = objTemperamento.GenerarReporte(objTemperamentoBE, false);
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

                        redirect("Temperamentos.aspx", "Ocurrio un error al obtener el Id y Token.");
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