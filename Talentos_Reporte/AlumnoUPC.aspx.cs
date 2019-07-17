using System;
using System.Net;
using System.Configuration;

public partial class AlumnoUPC : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!this.IsPostBack)
        {
            if (!(string.IsNullOrEmpty(Request.QueryString["id"])))
            {
                string id = Request.QueryString["id"];
                string urlReporte = ConfigurationSettings.AppSettings["URL_REPORTE"].ToString();

                wsReporte.wsReporte reporte = new wsReporte.wsReporte();
                string nombrePDF = reporte.CalcularReporte(id, false);
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
        }
    }

    private void redirect(string url, string mensaje)
    {
        //lblMensaje.Text = mensaje;
        //lblMensaje.Visible = true;
        Response.Redirect("~/" + url);
    }
}
