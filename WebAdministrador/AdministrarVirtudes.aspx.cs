using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class AdministrarVirtudes : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            CargarTalentos();
        }
    }

    private void CargarTalentos()
    {
        TalentosReference.WSTalentos objService = new TalentosReference.WSTalentos();
        List<TalentosReference.TalentoComplexBE> lstTalentos = new List<TalentosReference.TalentoComplexBE>();

        lstTalentos = objService.ListarVirtud(0).ToList();

        gvVirtudes.DataSource = lstTalentos;
        gvVirtudes.DataBind();
    }

    protected void gvVirtudes_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        int idVirtud = Convert.ToInt32(e.CommandArgument.ToString());

        Context.Items.Add("idVirtud", idVirtud);

        Server.Transfer("ModificarVirtud.aspx");
    }
}
