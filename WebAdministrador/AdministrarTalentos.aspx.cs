using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class AdministrarTalentos : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if(!Page.IsPostBack)
        {
            CargarTalentos();
        }
    }

    private void CargarTalentos()
    {
        TalentosReference.WSTalentos objService = new TalentosReference.WSTalentos();
        List<TalentosReference.TalentoComplexBE> lstTalentos = new List<TalentosReference.TalentoComplexBE>();

        lstTalentos = objService.ListarTalentosComplex(0).ToList();

        gvTalentos.DataSource = lstTalentos;
        gvTalentos.DataBind();
    }
    protected void gvTalentos_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        int idTalento = Convert.ToInt32(e.CommandArgument.ToString());

        Context.Items.Add("idTalento", idTalento);

        Server.Transfer("ModificarTalento.aspx"); 
    }
}
