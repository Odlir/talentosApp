using System;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;

namespace Talentos_Master
{
    public class TalentitoMapaUC
    {

        public int id { set; get; }
         public string color { get; set; }

        public string nombre { get; set; }

        public string source { get; set; }

      

        public TalentitoMapaUC (string _color, string _nombre, int _id, string _source)
        {
            this.id = _id;
            this.color = _color;
            this.nombre = _nombre;
            this.source = _source;
        }
    }
}
