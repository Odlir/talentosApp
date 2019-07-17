using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;

public partial class ModificarVirtud : System.Web.UI.Page
{
    int idVirtud;

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            idVirtud = Convert.ToInt32(Context.Items["idVirtud"].ToString());

            ViewState["idVirtud"] = idVirtud;

            //TalentosReference.WSTalentosSoapClient objService = new TalentosReference.WSTalentosSoapClient();
            TalentosReference.WSTalentos objService = new TalentosReference.WSTalentos();

            CargarTendencias(objService);
            CargarVirtud(objService, idVirtud);
        }
    }

    private void CargarVirtud(TalentosReference.WSTalentos objService, int idVirtud)
    {
        TalentosReference.TalentoComplexBE objVirtud = new TalentosReference.TalentoComplexBE();

        objVirtud = objService.ListarVirtud(idVirtud).ToList()[0];
        string imagen = objVirtud.image.Substring(objVirtud.image.LastIndexOf("/") + 1, objVirtud.image.Length - objVirtud.image.LastIndexOf("/") - 1);

        txtVirtud.Text = objVirtud.nombre;
        txtDescripcion.Text = objVirtud.descripcion;
        ddlTendencia.SelectedValue = objVirtud.idTendencia.ToString();
        imgImagen.Attributes.Add("src", objVirtud.image);
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
        TalentosReference.TalentoComplexBE objVirtud = new TalentosReference.TalentoComplexBE();
        bool resultado = false;

        try
        {
            objVirtud.idTalento = Convert.ToInt32(ViewState["idVirtud"]);
            objVirtud.nombre = txtVirtud.Text.Trim();
            objVirtud.descripcion = txtDescripcion.Text.Trim();
            objVirtud.idTendencia = Convert.ToInt32(ddlTendencia.SelectedItem.Value);

            TalentosReference.WSTalentos objService = new TalentosReference.WSTalentos();

            resultado = objService.ActualizarVirtud(objVirtud);

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
                  "alert('Se actualizó correctamente la Virtud');",
                  true);
            }
            else
            {
                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(),
             "Error",
             "alert('No se logro actualizar la Virtud');",
             true);
            }
        }
        catch (Exception)
        {
            ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(),
              "Error",
              "alert('Se produjo un error al momento de actualizar la Virtud');",
              true);
        }
    }

    protected void btnRegresar_Click(object sender, EventArgs e)
    {
        Server.Transfer("AdministrarVirtudes.aspx");
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
