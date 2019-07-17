using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using System.Windows.Browser;
using System.Diagnostics;
//using System.Web;


//TODO: (ITERACION 2 - I ENTREGA) Incluir en la Secuencia del Juego

//TODO: (ITERACION 2 - I ENTREGA) Desarrollar CU Ingresar al Juego

namespace Talentos_Master
{
    public partial class frmLogin : IPaginaContenida
    {
       // private BuzonGlobal session;
        private Session SessionActual;
 
        public frmLogin()
        {
            InitializeComponent();

            SessionActual = Session.getInstance();

            if (SessionActual.EsMasivo)
            {
                txtUsuario.Text = SessionActual.CorreoParticipanteMasivo;
                txtUsuario.Foreground = new SolidColorBrush(Colors.Black);
                txtUsuario.IsReadOnly = true;
            }
        }

        #region Ingresar al Juego

            public bool ValidaDatos()
            {
                if (txtUsuario.Text.Length == 0 && txtPassword.Password.Length == 0)
                {
                    lblMensaje.Text = " * Es necesario ingresar usuario y contraseña";
                    return false;
                }
                    return true;
            }

            private void imgIngresar_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
            {
                if (ValidaDatos())
                {
                    SessionActual = Session.getInstance();
                    
                    string mail = txtUsuario.Text.ToString();
                    string password = txtPassword.Password.ToString();
                    
                    SeguridadReference.WebServiceTalentosSoapClient WS = new Talentos_Master.SeguridadReference.WebServiceTalentosSoapClient();

                    if (SessionActual.EsMasivo)
                    {
                        WS.LoginMasivoCompleted +=new EventHandler<Talentos_Master.SeguridadReference.LoginMasivoCompletedEventArgs>(WS_LoginMasivoCompleted);
                        WS.LoginMasivoAsync(mail, password);
                    }
                    else
                    {
                        SeguridadReference.UsuarioBE BE = new Talentos_Master.SeguridadReference.UsuarioBE();
                        BE.Correo = txtUsuario.Text.ToString();
                        BE.Password = txtPassword.Password.ToString();

                        WS.LoginPhpCompleted += new EventHandler<SeguridadReference.LoginPhpCompletedEventArgs>(ValidarUser_Completed);
                        WS.LoginPhpAsync(BE);
                    }

                    //SeguridadReference.UsuarioBE BE = new Talentos_Master.SeguridadReference.UsuarioBE();
                    //BE.NickName = txtUsuario.Text.ToString();
                    //BE.Password = txtPassword.Password.ToString();
                    //SeguridadReference.WebServiceTalentosSoapClient WS = new Talentos_Master.SeguridadReference.WebServiceTalentosSoapClient();
                    //WS.UsuarioValidarCompleted += new EventHandler<SeguridadReference.UsuarioValidarCompletedEventArgs>(ValidarUser_Completed);
                    //WS.UsuarioValidarAsync(BE);
                }
            }

            private void WS_LoginMasivoCompleted(object sender, SeguridadReference.LoginMasivoCompletedEventArgs e)
            {
                SeguridadReference.UsuarioBE objUsuarioBE = new Talentos_Master.SeguridadReference.UsuarioBE();
                objUsuarioBE = e.Result;
                SessionActual = Session.getInstance();

                if (objUsuarioBE != null)
                {
                    objUsuarioBE.NickName = objUsuarioBE.Nombres;
                    SessionActual.participante = objUsuarioBE;
                    SessionActual.resultado.Participante_id = SessionActual.participante.UsuarioId;

                    SessionActual.paso1 = true;
                    SessionActual.paso2 = false;
                    SessionActual.paso3 = false;
                    SessionActual.paso4 = false;

                    _cambiarContenido.Invoke(Enumerador.Pagina.MasterRueda);
                }
                else
                {
                    lblMensaje.Visibility = Visibility.Visible;
                    lblMensaje.Text = " * Contraseña incorrecta";
                    txtUsuario.Text = SessionActual.CorreoParticipanteMasivo;
                    txtPassword.Password = "";
                    txtPassword.Focus();
                }
            }

            private void ValidarUser_Completed(object sender, SeguridadReference.LoginPhpCompletedEventArgs e)
            {
                SeguridadReference.UsuarioBE be = new Talentos_Master.SeguridadReference.UsuarioBE();
                be = e.Result;

                if (be.Nombres != null)
                {
                    SessionActual = Session.getInstance();
                    be.NickName = be.Nombres;
                    
                    SessionActual.participante = be;

                    SessionActual.resultado.Participante_id = SessionActual.participante.UsuarioId;

                    SessionActual.paso1 = true;
                    SessionActual.paso2 = false;
                    SessionActual.paso3 = false;
                    SessionActual.paso4 = false;

                    //_cambiarContenido.Invoke(Enumerador.Pagina.PrimeraEtapa);
                    _cambiarContenido.Invoke(Enumerador.Pagina.MasterRueda);
                    ////ValidarLog();
                    ////quitar comentario!!!
                    //InsertarJuego();
                    ////comentar esto!!!
                    ////_cambiarContenido.Invoke(Enumerador.Pagina.MasterRueda);
                }
                else
                {
                    lblMensaje.Text = " * Usuario o contraseña incorrecta";
                    txtUsuario.Text = "";
                    txtPassword.Password = "";
                    txtUsuario.Focus();
                    return;
                }
            }

            //private void ValidarUser_Completed(object sender, SeguridadReference.UsuarioValidarCompletedEventArgs e)
            //{
            //    SeguridadReference.UsuarioBE be = new Talentos_Master.SeguridadReference.UsuarioBE();
            //    be = e.Result;

            //    if (be.Nombres != null)
            //    {
            //        SessionActual = Session.getInstance();
            //        SessionActual.participante=be;
            //        //ValidarLog();
            //        //quitar comentario!!!
            //        InsertarJuego();
            //        //comentar esto!!!
            //        //_cambiarContenido.Invoke(Enumerador.Pagina.MasterRueda);
                    
            //    }
            //    else
            //    {
            //        lblMensaje.Text = " * Usuario o Contraseña Inválida";
            //        return;
            //    }
            //}

            #region validar tiempo transcurrido desde el último juego del usuario

                private void InsertarJuego()
                {
                    TalentosReference.ResultadoBE be = new Talentos_Master.TalentosReference.ResultadoBE();
                   
                    be.Participante_id = SessionActual.participante.UsuarioId;
                    be.Fecha = DateTime.Now;
                    be.DNI = SessionActual.DNI;
                    be.CodEvaluacion = SessionActual.CodEvaluacion;
                    TalentosReference.WSTalentosSoapClient ws = new Talentos_Master.TalentosReference.WSTalentosSoapClient();

                    ws.InsertarResultadoCompleted += new EventHandler<Talentos_Master.TalentosReference.InsertarResultadoCompletedEventArgs>(ResultadoInsert_completed);
                    ws.InsertarResultadoAsync(be);
                }

                private void ResultadoInsert_completed(object sender, TalentosReference.InsertarResultadoCompletedEventArgs e)
                {
                    SessionActual.resultado.Resultado_id = e.Result;
                }

            #endregion

        #endregion 

        //Registrar un nuevo usuario
        private void imgRegistrarse_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            _cambiarContenido.Invoke(Enumerador.Pagina.Registrarse);
        }

        private void Image_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            //TODO: VIDEO
           HtmlPage.Window.Invoke("ShowVideo");
        }

        private void Border_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {

        }

        private void registrate_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            HtmlPage.Window.Navigate(new Uri("http://www.davidfischman.com/registro.php?lang=es"), "_blank");
        }

        private void txtAqui_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            HtmlPage.Window.Navigate(new Uri("http://www.davidfischman.com/restablecerContrasena.php?lang=es"), "_blank");
        }

        private void Image_MouseLeftButtonDown_1(object sender, MouseButtonEventArgs e)
        {
            HtmlPage.Window.Navigate(new Uri("http://www.davidfischman.com/index.php?lang=es"), "_blank");
        }

        private void TxtUsuario_GotFocus(object sender, RoutedEventArgs e)
        {
            if (((SolidColorBrush)txtUsuario.Foreground).Color != Colors.Black)
            {
                txtUsuario.Text = "";
                ((SolidColorBrush)txtUsuario.Foreground).Color = Colors.Black;
            }
        }

        private void TxtPassword_GotFocus(object sender, RoutedEventArgs e)
        {
            if (((SolidColorBrush)txtPassword.Foreground).Color != Colors.Black)
            {
                txtPassword.Password = "";
                ((SolidColorBrush)txtPassword.Foreground).Color = Colors.Black;
            }
        }
    }
}