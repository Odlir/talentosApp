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

/// <summary>
/// Este user control es un botón que será usado durante el desarrollo del Juego de Talentos
/// <summary>

namespace Talentos_Master
{
    public partial class ButtonSeven : UserControl {
        public ButtonSeven() {
            InitializeComponent();
        }

      
        Point tempPoint = new Point();

        private void LayoutRoot_MouseMove(object sender, MouseEventArgs e) {
 
            Point p = e.GetPosition(this);
            tempPoint.X = p.X / ActualWidth;
            tempPoint.Y = 1;
            brushLight.Center = tempPoint;
            brushLight.GradientOrigin = tempPoint;

        }

        string _title = String.Empty;

        /// <summary>
        /// Titulo de un elemento
        /// </summary>
        public string Title {
            get { return _title; }
            set { _title = value; }
        }


        /// <summary>
        /// El color de transición cuando pasamos el mouse sobre el botón
        /// </summary>
        public Color TransitionColor {
            get {
                return transitionColor.Color;
            }
            set {
                transitionColor.Color = value;
                transitionSubColor.Color = Color.FromArgb(64, value.R, value.G, value.B);
            }
        }

        /// <summary>
        /// La fuente de la imagen que mostrará el control
        /// </summary>
        public ImageSource ImageSource {
            get { return imgItem.Source; }
            set { imgItem.Source = value; }
        }

        private void LayoutRoot_MouseEnter(object sender, MouseEventArgs e) {

            animEnter.Begin();
        }

        private void LayoutRoot_MouseLeave(object sender, MouseEventArgs e) {

            animLeave.Begin();

        }

        /// <summary>
        /// Uri para navegar cuando se hace click sobre el control
        /// </summary>
        public string NavigateUri { get; set; }

        /// <summary>
        /// true para abrir el link en una nueva ventana.
        /// False para abrir el link en la ventana actual.
        /// default es false
        /// </summary>
        public bool OpenInNewWindow { get; set; }

        private void UserControl_MouseLeftButtonUp(object sender, MouseButtonEventArgs e) {
            if (NavigateUri != null) {
                if (OpenInNewWindow) {
                    System.Windows.Browser.HtmlPage.Window.Navigate(new Uri(NavigateUri, UriKind.Absolute), "_new");
                }
                else {
                    System.Windows.Browser.HtmlPage.Window.Navigate(new Uri(NavigateUri, UriKind.Absolute));
                }
            }

        }

    }
}
