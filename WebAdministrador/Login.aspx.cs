using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Security;


//TODO: (ITERACION 2 - I ENTREGA) Desarrollar CU Ingresar al Juego

//TODO: (ITERACION 4) Modificar diseño de interfaces para Administrador



public partial class Login : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        Response.Buffer = true;
        Response.ExpiresAbsolute = DateTime.Now.AddDays(-1);
        Response.Expires = -1500;
        Response.CacheControl = "no-cache";
    }

    protected void btnIngresar_Click(object sender, EventArgs e)
    {
        SeguridadReference.UsuarioBE objAdmin = new SeguridadReference.UsuarioBE();

        objAdmin.Correo = txtUsuario.Text;
        objAdmin.Password = txtContrasena.Text;

        Request.ServerVariables["Remote_Address"].ToString();
        Context.Session.Add("Usuario", txtUsuario.Text);
        Context.Session.Add("Contrasena", txtContrasena.Text);
        Session["Usuario"] = objAdmin;
        FormsAuthentication.RedirectFromLoginPage(txtUsuario.Text, false);
        Response.Redirect("Inicio.aspx");

        /*SeguridadReference.WebServiceTalentos ws = new SeguridadReference.WebServiceTalentos();

        SeguridadReference.UsuarioBE objAdmin = new SeguridadReference.UsuarioBE();

        objAdmin.Correo = txtUsuario.Text;
        objAdmin.Password = txtContrasena.Text;

        lblMensajeError.Visible = false;

        objAdmin = ws.EncriptarMd5(objAdmin);
        if (objAdmin != null)
        {
            objAdmin = ws.ObtenerParticipante(objAdmin.Correo, objAdmin.Password);

            if (objAdmin != null)
            {
                if (objAdmin.TipoParticipanteId == 2)
                {
                    SeguridadReference.SesionBE sesionUnica = new SeguridadReference.SesionBE();
                    sesionUnica.Participante_id = objAdmin.UsuarioId;
                    int activo = ws.ValidarSesion(sesionUnica);
                    if (activo == 0)
                    {
                        Request.ServerVariables["Remote_Address"].ToString();
                        Context.Session.Add("Usuario", txtUsuario.Text);
                        Context.Session.Add("Contrasena", txtContrasena.Text);
                        Session["Usuario"] = objAdmin;
                        //ws.PutSession(objAdmin.UsuarioId);
                        //int iii = ws.GetSession();
                        FormsAuthentication.RedirectFromLoginPage(txtUsuario.Text, false);
                        // }
                    }
                    else
                    {
                        if (activo == -1)
                        {
                            lblMensajeError.Text = "No se permite el uso de caracteres especiales.";
                            lblMensajeError.Visible = true;
                            // window.mensaje.Text = "No se permite el uso de caracteres especiales.";
                        }
                        else
                        {
                            lblMensajeError.Text = "El usuario posee una sesión activa. Sólo se puede iniciar una sesión al mismo tiempo.";
                            lblMensajeError.Visible = true;
                            //window.mensaje.Text = "El usuario posee una sesión activa. Sólo se puede iniciar una sesión al mismo tiempo.";
                        }
                    }
                }
                else
                {
                    lblMensajeError.Text = "Usuario y/o contraseña inválidos.";
                    lblMensajeError.Visible = true;
                }
            }
            else
            {
                lblMensajeError.Text = "No se permite el uso de caracteres especiales.";
                lblMensajeError.Visible = true;
            }
        }

        else
        {
            lblMensajeError.Text = "No se permite el uso de caracteres especiales.";
            lblMensajeError.Visible = true;
        }*/
    }
}
