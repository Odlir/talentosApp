using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class ModificarTendencia : System.Web.UI.Page
{
    int idTendencia;

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            idTendencia = Convert.ToInt32(Context.Items["TendenciaId"].ToString());

            ViewState["TendenciaId"] = idTendencia;

            CargarTendencia(idTendencia);
        }
    }

    private void CargarTendencia(int idTendencia)
    {
        TalentosReference.WSTalentos objService = new TalentosReference.WSTalentos();
        TalentosReference.TendenciaBE objTendencia = new TalentosReference.TendenciaBE();

        objTendencia = objService.ObtenerTendencia(idTendencia);

        txtTendencia.Text = objTendencia.Nombre;
        txtDescripcion.Text = objTendencia.Descripcion;
        txtColor.Text = objTendencia.Color;
    }

    protected void btnGrabar_Click(object sender, EventArgs e)
    {
        TalentosReference.TendenciaBE objTendencia = new TalentosReference.TendenciaBE();
        bool resultado = false;

        try
        {
            objTendencia.TendenciaId = Convert.ToInt32(ViewState["TendenciaId"]);
            objTendencia.Nombre = txtTendencia.Text.Trim();
            objTendencia.Descripcion = txtDescripcion.Text.Trim();
            objTendencia.Color = txtColor.Text.Trim();

            TalentosReference.WSTalentos objService = new TalentosReference.WSTalentos();

            resultado = objService.ActualizarTendencia(objTendencia);

            if (resultado)
            {
                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(),
                  "Actualización",
                  "alert('Se actualizó correctamente la Tendencia');",
                  true);
            }
            else
            {
                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(),
             "Error",
             "alert('No se logro actualizar la Tendencia');",
             true);
            }
        }
        catch (Exception)
        {
            ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(),
              "Error",
              "alert('Se produjo un error al momento de actualizar la Tendencia');",
              true);
        }
    }

    protected void btnRegresar_Click(object sender, EventArgs e)
    {
        Server.Transfer("AdministrarTendencias.aspx");
    }
}
