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

namespace UPC.Talentos.BL.BE
{
    public class RecomendacionBE
    {
        public string Talento { get; set; }
        private int idRecomendacion;

        public int IdRecomendacion
        {
            get { return idRecomendacion; }
            set { idRecomendacion = value; }
        }
        private int idTalento;

        public int IdTalento
        {
            get { return idTalento; }
            set { idTalento = value; }
        }
        private string descripcion;

        public string Descripcion
        {
            get { return descripcion; }
            set { descripcion = value; }
        }

        public int Tipo { get; set; } //1: Mas Desarrollado, 2: Menos Desarrollado
    }
}
