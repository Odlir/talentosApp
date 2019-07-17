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

namespace Talentos_Master
{
    public partial class frmAgradecimientoFinal : IPaginaContenida
    {
        Session SessionActual;
        TalentosReference.WSTalentosSoapClient client;

        public frmAgradecimientoFinal()
        {
            InitializeComponent();
            SessionActual = Session.getInstance();

            client = new Talentos_Master.TalentosReference.WSTalentosSoapClient();

            if (SessionActual.participante.Sexo.Equals(1))
                txtfullname.Text = "Bienvenida, " + SessionActual.participante.NickName;//string.Concat("Hola ",SessionActual.participante.NickName);
            else
                txtfullname.Text = "Bienvenido, " + SessionActual.participante.NickName;//string.Concat("Hola ",SessionActual.participante.NickName);
        }

        private void Border_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            HtmlPage.Window.Invoke("CloseWindow");
        }

        private void txtCerrarSession_MouseLeftButtonDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            SessionActual = Session.deleteInstance();
            _cambiarContenido.Invoke(Enumerador.Pagina.Login);
        }

        private void Image_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            HtmlPage.Window.Navigate(new Uri("http://www.davidfischman.com/index.php?lang=es"), "_blank");
        }

        private void txtReiniciarJuego_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            SeguridadReference.UsuarioBE be = new Talentos_Master.SeguridadReference.UsuarioBE();
            be = SessionActual.participante;



            //SessionActual = Session.ReiniciarInstance();
            //SessionActual = Session.getInstance();
            SessionActual.participante = be;
            SessionActual.paso1 = false;
            SessionActual.paso2 = false;
            SessionActual.paso3 = false;
            SessionActual.paso4 = false;
            SessionActual.cantCalificadosBuzon1 = 0;
            SessionActual.cantCalificadosBuzon2 = 0;

            SessionActual.resultado = new Talentos_Master.TalentosReference.ResultadoBE();
            SessionActual.juego = new Talentos_Master.TalentosReference.JuegoBE();

            SessionActual.cantSeleccionadosBuzon1 = 0;
            SessionActual.cantSeleccionadosBuzon3 = 0;

            SessionActual.terminoCalificacion = false;
            SessionActual.terminoClasificacion = false;
            SessionActual.terminoSeleccion = false;

            SessionActual.pasoCorrec = false;

            SessionActual.revisaSelec = false;

            SessionActual.revisaClasif = false;

            SessionActual.Buzon1 = new Buzon();
            SessionActual.Buzon2 = new Buzon();
            SessionActual.Buzon3 = new Buzon();

            SessionActual.lstImages = new List<Image>();
            SessionActual.lstImgEspalda = new List<Image>();
            SessionActual.lstTalentos = new System.Collections.ObjectModel.ObservableCollection<Talentos_Master.TalentosReference.TalentoBE>();

            client.ListarTalentosCompleted += new EventHandler<Talentos_Master.TalentosReference.ListarTalentosCompletedEventArgs>(obtenerTalentos_Completed);
            client.ListarTalentosAsync();

        }

      

        private void txtAgradecimiento_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            var window4 = new wAgradecimiento();
            window4.ShowDialog();
        }

        private void txtReferencias_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            var window4 = new wReferencias();
            window4.ShowDialog();
        }

        private void obtenerTalentos_Completed(object sender, TalentosReference.ListarTalentosCompletedEventArgs e)
        {
            SessionActual.lstTalentos = e.Result;
            _cambiarContenido.Invoke(Enumerador.Pagina.MasterRueda);
        }

    }
}
