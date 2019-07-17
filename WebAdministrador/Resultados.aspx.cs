using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using TalentosReference;
using System.Configuration;

public partial class Resultados : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }

    private void Cargar_Resultado()
    {
        TalentosReference.WSTalentos objService = new TalentosReference.WSTalentos();

        // MM/dd/AAAA
        string fechaInicio = (txtFechaInicio.Text.Equals("")) ? "0" : txtFechaInicio.Text.Trim();
        string fechaFin = (txtFechaFin.Text.Equals("")) ? "99999999" : txtFechaFin.Text.Trim();
        string empresa = txtEmpresa.Text.Trim();

        fechaInicio = fechaInicio.Substring(6, 4) + fechaInicio.Substring(0, 2) + fechaInicio.Substring(3, 2);
        fechaFin = fechaFin.Substring(6, 4) + fechaFin.Substring(0, 2) + fechaFin.Substring(3, 2);

        List<ResultadoParaleloBE> lstResultado = objService.ResultadosParticipantesListar(fechaInicio, fechaFin, empresa).ToList();

        gvResultados.DataSource = lstResultado;
        gvResultados.DataBind();
    }

    protected void btnBuscar_Click(object sender, EventArgs e)
    {
        Cargar_Resultado();
    }

    protected void btnImportar_Click(object sender, EventArgs e)
    {
        ReportesReference.wsReporte objService = new ReportesReference.wsReporte();

        // MM/dd/AAAA
        string fechaInicio = (txtFechaInicio.Text.Equals("")) ? "0" : txtFechaInicio.Text.Trim();
        string fechaFin = (txtFechaFin.Text.Equals("")) ? "99999999" : txtFechaFin.Text.Trim();
        string empresa = txtEmpresa.Text.Trim();

        fechaInicio = fechaInicio.Substring(6, 4) + fechaInicio.Substring(0, 2) + fechaInicio.Substring(3, 2);
        fechaFin = fechaFin.Substring(6, 4) + fechaFin.Substring(0, 2) + fechaFin.Substring(3, 2);

        objService.ResultadosParticipantesImportar(fechaInicio, fechaFin, empresa);
    }
    protected void gvResultados_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        string DNI = e.CommandName.ToString();
        string codigoEvaluacion = e.CommandArgument.ToString();
        string urlReporte = ConfigurationManager.AppSettings["URL_REPORTE"].ToString() + "?dni=NUM_DNI&cod=NUM_COD";

        urlReporte = urlReporte.Replace("NUM_DNI", DNI);
        urlReporte = urlReporte.Replace("NUM_COD", codigoEvaluacion);

        Response.Write("<script>");
        Response.Write("window.open('" + urlReporte + "', '_newtab');");
        Response.Write("</script>");

        //Response.Redirect(urlReporte);
    }
}
