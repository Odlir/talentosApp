using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class AdministrarTendencias : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            CargarTendencias();
        }
    }

    private void CargarTendencias()
    {
        TalentosReference.WSTalentos objService = new TalentosReference.WSTalentos();
        List<TalentosReference.TendenciaBE> lstTendencias = new List<TalentosReference.TendenciaBE>();
        TalentosReference.TendenciaBE objTalento = new TalentosReference.TendenciaBE();

        lstTendencias = objService.ListarTendencias().ToList();

        gvTendencias.DataSource = lstTendencias;
        gvTendencias.DataBind();
    }

    protected void gvTendencias_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        int idTendencia = Convert.ToInt32(e.CommandArgument.ToString());

        Context.Items.Add("TendenciaId", idTendencia);

        Server.Transfer("ModificarTendencia.aspx"); 
    }
}
