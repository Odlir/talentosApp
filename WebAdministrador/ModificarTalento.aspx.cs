using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;

public partial class ModificarTalento : System.Web.UI.Page
{
    int idTalento;

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            idTalento = Convert.ToInt32(Context.Items["idTalento"].ToString());

            ViewState["idTalento"] = idTalento;

            TalentosReference.WSTalentos objService = new TalentosReference.WSTalentos();

            CargarTendencias(objService);
            CargarTalento(objService, idTalento);
        }
    }

    private void CargarTalento(TalentosReference.WSTalentos objService, int idTalento)
    {
        TalentosReference.TalentoComplexBE objTalento = new TalentosReference.TalentoComplexBE();

        objTalento = objService.ListarTalentosComplex(idTalento).ToList()[0];
        string imagen = objTalento.image.Substring(objTalento.image.LastIndexOf("/") + 1, objTalento.image.Length - objTalento.image.LastIndexOf("/") - 1);

        txtTalento.Text = objTalento.nombre;
        txtDescripcion.Text = objTalento.descripcion;
        ddlTendencia.SelectedValue = objTalento.idTendencia.ToString();
        imgImagen.Attributes.Add("src", "." + objTalento.image);
        ViewState["nombreImagen"] = imagen;
    }

    private void CargarTendencias(TalentosReference.WSTalentos objService)
    {
        List<TalentosReference.TendenciaBE> lstTendencias = new List<TalentosReference.TendenciaBE>();
        TalentosReference.TendenciaBE objTalento = new TalentosReference.TendenciaBE();

        lstTendencias = objService.ListarTendencias().ToList();

        ddlTendencia.DataSource = lstTendencias;
        ddlTendencia.DataTextField = "Nombre";
        ddlTendencia.DataValueField = "TendenciaId";
        ddlTendencia.DataBind();
    }

    protected void btnGrabar_Click(object sender, EventArgs e)
    {
        TalentosReference.TalentoComplexBE objTalento = new TalentosReference.TalentoComplexBE();
        bool resultado = false;

        try
        {
            objTalento.idTalento = Convert.ToInt32(ViewState["idTalento"]);
            objTalento.nombre = txtTalento.Text.Trim();
            objTalento.descripcion = txtDescripcion.Text.Trim();
            objTalento.idTendencia = Convert.ToInt32(ddlTendencia.SelectedItem.Value);

            TalentosReference.WSTalentos objService = new TalentosReference.WSTalentos();

            resultado = objService.ActualizarTalento(objTalento);

            if (resultado)
            {
                string imagenOriginal = Server.MapPath("~/images/talentos/images/" + ViewState["nombreImagen"].ToString()); //"~/images/talentos/images/" + ViewState["nombreImagen"].ToString();
                string imagenNueva = Server.MapPath("~/images/tmp/" + ViewState["nombreImagen"].ToString());

                if (File.Exists(imagenNueva))
                {
                    File.Replace(imagenNueva, imagenOriginal, Server.MapPath("~/images/tmp/" + ViewState["nombreImagen"].ToString() + ".bk"));
                    File.Delete(imagenNueva);
                    imgImagen.Attributes.Add("src", "images/talentos/images/" + ViewState["nombreImagen"].ToString());
                }

                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(),
                  "Actualización",
                  "alert('Se actualizó correctamente el Talento');",
                  true);
            }
            else
            {
                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(),
             "Error",
             "alert('No se logro actualizar el Talento');",
             true);
            }
        }
        catch (Exception)
        {
            ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(),
              "Error",
              "alert('Se produjo un error al momento de actualizar el Talento');",
              true);
        }
    }

    protected void btnRegresar_Click(object sender, EventArgs e)
    {
        Server.Transfer("AdministrarTalentos.aspx");
    }

    protected void btnVisualizar_Click(object sender, EventArgs e)
    {
        if (fuImagen.HasFile)
        {
            try
            {
                string fileNameWithoutExtension = Path.GetFileNameWithoutExtension(fuImagen.FileName);
                string fileExtension = Path.GetExtension(fuImagen.FileName);
                string nombreImagenOriginal = ViewState["nombreImagen"].ToString();

                fileNameWithoutExtension = nombreImagenOriginal.Substring(0, nombreImagenOriginal.LastIndexOf("."));
                fuImagen.PostedFile.SaveAs(Server.MapPath("~/images/tmp/" + fileNameWithoutExtension + fileExtension));

                imgImagen.Attributes.Add("src", "images/tmp/" + fileNameWithoutExtension + fileExtension);
            }
            catch (Exception ex)
            {
                //StatusLabel.Text = "Upload status: The file could not be uploaded. The following error occured: " + ex.Message;
            }
        }
    }
}
