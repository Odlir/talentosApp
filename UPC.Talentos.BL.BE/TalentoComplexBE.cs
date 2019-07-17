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
    public class TalentoComplexBE
    {
        public int idTalento { get; set; }
        public int idTendencia { get; set; }
        public int idTipoTalento { get; set; }
        public string nombre { get; set; }
        public string descripcion { get; set; }
        public string tendencia { get; set; }
        public string tipoTalento { get; set; }
        public string image { get; set; }
        public bool Seleccionado { get; set; }
        public int Buzon_Id { get; set; }
    }
}
