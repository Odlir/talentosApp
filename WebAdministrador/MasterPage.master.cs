using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Security;

public partial class MasterPage : System.Web.UI.MasterPage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["Usuario"] == null)
        {
            Response.Redirect("Login.aspx");

        }
        else
        {
            if (Page.IsPostBack == false)
            {
                //if (Session["Usuario"] != null)
                //{
                //    if (((SeguridadReference.UsuarioBE)Session["Usuario"]).Sexo == 1)
                Label1.Text = "Bienvenid@ " + ((SeguridadReference.UsuarioBE)Session["Usuario"]).Nombres;
                //    else
                //        Label1.Text = "Bienvenida " + ((SeguridadReference.UsuarioBE)Session["Usuario"]).Nombres;
                //}
            }
        }
    }

    protected void lnkRegistrarEvento_Click(object sender, EventArgs e)
    {
        Context.Items.Add("Modo", "1");
        Context.Items.Add("EventoId", "0");

        Server.Transfer("frmEvento.aspx");
    }

    protected void lnkCerrarSesion_Click(object sender, EventArgs e)
    {
        FormsAuthentication.SignOut();
        Server.Transfer("Login.aspx");
    }

    protected void LinkButton5_Click(object sender, EventArgs e)
    {
        if (Session["Usuario"] != null)
        {
            SeguridadReference.SesionBE sessionUnica = new SeguridadReference.SesionBE();
            sessionUnica.Participante_id = ((SeguridadReference.UsuarioBE)Session["Usuario"]).UsuarioId;
            SeguridadReference.WebServiceTalentos ws = new SeguridadReference.WebServiceTalentos();
            ws.EliminarSesion(sessionUnica);
        }
        FormsAuthentication.SignOut();
        Response.Redirect("Login.aspx");
    }
}
