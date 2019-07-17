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
    public class TalentoUC
    {
        public int id { get; set; }
        public string source { get; set; }
        public string descripcion { get; set; }

        public string color { get; set; }

        public string nombre { get; set; }

        public bool seleccionado { get; set; }

        public int i { get; set; }

        public TalentoUC(int Id, string url,string name, string desc)
        {
            this.id = Id;
            this.descripcion = desc;
            this.source = url;
            this.nombre = name;
        }

        public TalentoUC( string _color, string _nombre)
        {
            
            this.color = _color;
            this.nombre = _nombre;
        }

        public TalentoUC(int Id, string url, string name, string desc, bool selecc, int index)
        {
            this.id = Id;
            this.descripcion = desc;
            this.source = url;
            this.nombre = name;
            this.seleccionado = selecc;
            this.i = index;
        }

    }
}
