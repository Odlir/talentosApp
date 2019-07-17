using System;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Browser;
using Talentos_Master.TalentosReference;

namespace Talentos_Master
{
    public partial class frmAgradecimientoJuego : IPaginaContenida
    {
        private Session SessionActual;

        TalentosReference.WSTalentosSoapClient client;

        public frmAgradecimientoJuego()
        {
            InitializeComponent();

            client = new WSTalentosSoapClient();
            SessionActual = Session.getInstance();

            if (SessionActual.participante.Sexo.Equals(1))
                txtfullname.Text = "Bienvenida, " + SessionActual.participante.NickName;
            else
                txtfullname.Text = "Bienvenido, " + SessionActual.participante.NickName;

        }

        private void Border_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            HtmlPage.Window.Invoke("CloseWindow");
        }

        private void txtAqui_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            _cambiarContenido.Invoke(Enumerador.Pagina.Calificacion);
        }

        private void txtCerrarSession_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            HtmlPage.Window.Invoke("CloseWindow");
        }

        private void brInstruccion3_MouseLeftButtonDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            _cambiarContenido.Invoke(Enumerador.Pagina.Sugerencias);
        }

        private void brInstrcResult_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            //comentar paso?
          
            SessionActual.paso3 = true;
            SessionActual.paso2 = false;
            SessionActual.paso1 = false;
            SessionActual.paso4 = false;
            SessionActual.pasoTE = false;
            SessionActual.pasoVirtud = false;

            //_cambiarContenido.Invoke(Enumerador.Pagina.ResultadosClasificacion);
            //_cambiarContenido.Invoke(Enumerador.Pagina.EnvioReporte);
        }

        private void Image_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            HtmlPage.Window.Navigate(new Uri("http://www.davidfischman.com/index.php?lang=es"), "_blank");
        }

        private void txtInstruccion1_MouseEnter(object sender, MouseEventArgs e)
        {
            TextBlock txt = (TextBlock)(sender as TextBlock);

            txt.Foreground= new SolidColorBrush(Color.FromArgb((byte)255, (byte)174, (byte)89, (byte)5));
        }

        private void txtInstruccion1_MouseLeave(object sender, MouseEventArgs e)
        {
            TextBlock txt = (TextBlock)(sender as TextBlock);

            txt.Foreground = new SolidColorBrush(Color.FromArgb((byte)255, (byte)255, (byte)255, (byte)255));
        }

        private void brInstruc1_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (!SessionActual.paso1)
            {
                SessionActual.Buzon1.activo = true;
                SessionActual.Buzon2.activo = SessionActual.Buzon3.activo = false;
                _cambiarContenido.Invoke(Enumerador.Pagina.ClasificacionDetalle);
            }
        }

        private void brInstruc2_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (SessionActual.lstTalentos.Count.Equals(0) && SessionActual.Buzon1.lstTalento.Count >= 10 && SessionActual.Buzon3.lstTalento.Count >= 5)
                _cambiarContenido(Enumerador.Pagina.SeleccionarPrincipal);
        }

        private void txtAgradecimiento_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            var window = new wAgradecimiento();
            window.ShowDialog();
        }

        private void txtReferencias_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            var window = new wReferencias();
            window.ShowDialog();
        }

        private void obtenerTalentos_Completed(object sender, TalentosReference.ListarTalentosCompletedEventArgs e)
        {
            SessionActual.lstTalentos = e.Result;
            _cambiarContenido.Invoke(Enumerador.Pagina.MasterRueda);
        }
    }
}
