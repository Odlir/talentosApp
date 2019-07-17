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
using System.Collections.Generic;

namespace UPC.Talentos.BL.BE
{
    public class ResultadoComCampoLaboralBE
    {
        public int idCampoLaboral { get; set; }
        public string nombreCamboLaboral { get; set; }
        public int coincidencia { get; set; }
        public List<String> lstCarreras { get; set; }
    }
}
