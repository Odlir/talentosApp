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
    public class GeneralTalentBE
    {
        public int idTalento { get; set; }
        public int idTendencia { get; set; }
        public int idTipoTalento { get; set; }
        public int cantSelect { get; set; }
        public string nombre { get; set; }
        public string tendencia { get; set; }
    }
}
