using System;
using System.Configuration;
using System.Net;


public partial class General : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!this.IsPostBack)
        {
            if (!(string.IsNullOrEmpty(Request.QueryString["id"])))
            {
                try
                {

                    string id = Request.QueryString["id"];
                    string urlReporte = ConfigurationSettings.AppSettings["URL_REPORTE"].ToString();
                    wsGeneralReport.wsGeneralReport report = new wsGeneralReport.wsGeneralReport();

                    string nombrePDF = report.CalcularReporte(id, true);
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
                    redirect("General.aspx", "");
                }
            }
        }
    }

    private void redirect(string url, string mensaje)
    {
        Response.Redirect("~/" + url);
    }
}