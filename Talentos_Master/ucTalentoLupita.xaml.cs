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
    public partial class ucTalentoLupita : UserControl
    {
        public ucTalentoLupita()
        {
            InitializeComponent();
        }

        private void btnVoltear_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            string url;
            if (imgTalento.Tag.ToString().Contains("a.png"))
            {
                url = imgTalento.Tag.ToString().Replace(
                "a.png", ".png");
                url = url.Replace(
                "talentos/example", "talentos/images");
            }
            else
            {
                url = imgTalento.Tag.ToString().Replace(
                ".png", "a.png");
                url = url.Replace(
                "talentos/images", "talentos/example");
            }
            imgTalento.Source =
            new BitmapImage(new Uri(url, UriKind.Relative));
            imgTalento.Tag = url;
        }
    }
}
