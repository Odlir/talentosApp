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
    public class GeneralReportCategoryBE
    {
        public int TendenciaId { get; set; }
        public string tendenciaDesc { get; set; }
        public int percentCategory { get; set; }
        public int countTalents { get; set; }
        public int TotalTalents { get; set; }
    }
}
