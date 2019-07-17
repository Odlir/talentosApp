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
    public partial class MainPage : UserControl
    {
        public MainPage()
        {
            InitializeComponent();
            App.Current.Host.Content.IsFullScreen = true;
           
            CambiarContenido(Enumerador.Pagina.Inicio);
        }

        void Content_Resized(object sender, EventArgs e)
        {
            double height = App.Current.Host.Content.ActualHeight;
            double width = App.Current.Host.Content.ActualWidth;
        }

        public void CambiarContenido(Enumerador.Pagina ePagina)
        {
            LayoutRoot.Children.Clear();
            IPaginaContenida objPaginaContenida = null;
            switch (ePagina)
            {
                case Enumerador.Pagina.Inicio:
                    objPaginaContenida = new frmInicio();
                    
                    contenedor.Height = 760;
                    break;

                case Enumerador.Pagina.Login:
                    objPaginaContenida = new frmLogin();
                    contenedor.Height = 760;
                    break;
               
                case Enumerador.Pagina.MasterRueda:
                    objPaginaContenida = new frmMaster();
                    ((frmMaster)objPaginaContenida).CambiarContenido(Enumerador.Pagina.ClasificacionPrincipal);
                    ((frmMaster)objPaginaContenida).CambiarInstruccion(Enumerador.Instruccion.PrimeraInstruccion);
                    contenedor.Height = 1100;
                    break;

                case Enumerador.Pagina.ClasificacionDetalle:
                    objPaginaContenida = new frmMaster();
                    ((frmMaster)objPaginaContenida).CambiarContenido(Enumerador.Pagina.ClasificacionDetalle);
                    ((frmMaster)objPaginaContenida).CambiarInstruccion(Enumerador.Instruccion.instruccionCorreccion);
                    contenedor.Height = 1100;
                    break;

                case Enumerador.Pagina.MasterRuedaSegundaEtapa:
                    objPaginaContenida = new frmMaster();
                    ((frmMaster)objPaginaContenida).CambiarContenido(Enumerador.Pagina.Calificacion);
                    break;
              
                case Enumerador.Pagina.Sugerencias:
                    objPaginaContenida = new frmRecomendacionesFinales();

                    contenedor.Height = 1400;//1350;//1250;//1350;//1090;
                    
                    break;
                case Enumerador.Pagina.ClasificacionPrincipal:
                    objPaginaContenida = new frmClasificacionPrincipal();
                    break;
                case Enumerador.Pagina.AgradecimientoFinal:
                    objPaginaContenida = new frmAgradecimientoFinal();
                    contenedor.Height = 950;
                    break;
                case Enumerador.Pagina.AgradecimientoJuego:
                    objPaginaContenida = new frmAgradecimientoJuego();
                    contenedor.Height = 950;
                    break;
                case Enumerador.Pagina.Calificacion:
                    objPaginaContenida = new frmCalificacion();
                    contenedor.Height = 1050;//1100;
                    break;

                case Enumerador.Pagina.SeleccionarPrincipal:
                    objPaginaContenida = new frmMaster();
                    ((frmMaster)objPaginaContenida).CambiarContenido(Enumerador.Pagina.SeleccionarPrincipal);
                    ((frmMaster)objPaginaContenida).CambiarInstruccion(Enumerador.Instruccion.SegundaInstruccion);
                    contenedor.Height = 1100;
                    break;
                default:
                    objPaginaContenida = new frmLogin();
                    break;
                    
            }
            objPaginaContenida.Width = App.Current.Host.Content.ActualWidth;
            objPaginaContenida.SetDelegate(CambiarContenido);
            LayoutRoot.Children.Clear();
            LayoutRoot.Children.Add(objPaginaContenida);
        }
        private void cambio(object sender, System.Windows.RoutedPropertyChangedEventArgs<double> e)
        {
            //the_doors___morrison_hotel___peace_frog_2__mp3.Volume = this.volumen.Value;
        }

        private void UserControl_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            ((UserControl)LayoutRoot.Children[0]).Width = App.Current.Host.Content.ActualWidth;
        }
    }
}