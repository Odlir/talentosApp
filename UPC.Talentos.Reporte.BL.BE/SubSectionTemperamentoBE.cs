using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace UPC.Talentos.Reporte.BL.BE
{
    public class SubSectionTemperamentoBE
    {
        public string title { get; set; }
        public string description { get; set; }
        public List<PreguntaTemperamentoBE> questions { get; set; }
        public ConsolidadoTemperamentoBE percentage = new ConsolidadoTemperamentoBE();
    }
}
