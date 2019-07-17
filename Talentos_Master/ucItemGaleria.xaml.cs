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
using System.Windows.Media.Imaging;

namespace Talentos_Master
{
    public partial class ucItemGaleria : IPaginaContenida
    {
        Session SessionActual;

      

        public ucItemGaleria()
        {
            InitializeComponent();
            //imgTalento.Tag = "images/talentos/images/Image10.png";

            SessionActual = Session.getInstance();

           
        }

        

        private void btnVoltear_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            
            string url;
            if (imgTalento.Tag.ToString().Contains("a.png"))
            {
                url = imgTalento.Tag.ToString().Replace("a.png", ".png");
                url = url.Replace("talentos/example", "talentos/images");
            }
            else
            {
                url = imgTalento.Tag.ToString().Replace(".png", "a.png");
                url = url.Replace("talentos/images", "talentos/example");
            }
            

            imgTalento.Source = new BitmapImage(new Uri(url, UriKind.Relative));
            imgTalento.Tag = url;
        }

        private void chkSeleccionado_Checked(object sender, RoutedEventArgs e)
        {
            if (SessionActual.Buzon1.activo)
            {
                if (!SessionActual.Buzon1.lstTalento[Convert.ToInt16((sender as CheckBox).Tag)].seleccionado && SessionActual.cantSeleccionadosBuzon1.Equals(10))
                    (sender as CheckBox).IsChecked = false;
                else
                {
                    SessionActual.Buzon1.lstTalento[Convert.ToInt16((sender as CheckBox).Tag)].seleccionado = (bool)(sender as CheckBox).IsChecked;

                    if (SessionActual.Buzon1.lstTalento[Convert.ToInt16((sender as CheckBox).Tag)].seleccionado)
                        SessionActual.cantSeleccionadosBuzon1++;
                    else
                        SessionActual.cantSeleccionadosBuzon3--;
                }

            }
            else
            {
                if (SessionActual.Buzon3.activo)
                {
                    if (!SessionActual.Buzon3.lstTalento[Convert.ToInt16((sender as CheckBox).Tag)].seleccionado && SessionActual.cantSeleccionadosBuzon3.Equals(10))
                        (sender as CheckBox).IsChecked = false;
                    else
                    {
                        SessionActual.Buzon3.lstTalento[Convert.ToInt16((sender as CheckBox).Tag)].seleccionado = (bool)(sender as CheckBox).IsChecked;

                        if (SessionActual.Buzon3.lstTalento[Convert.ToInt16((sender as CheckBox).Tag)].seleccionado)
                            SessionActual.cantSeleccionadosBuzon3++;
                        else
                            SessionActual.cantSeleccionadosBuzon3--;
                    }

                }
            }

            _ValidarSelecciones.Invoke();



        }
    }
}
