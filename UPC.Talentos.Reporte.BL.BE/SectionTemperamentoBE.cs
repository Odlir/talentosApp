using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace UPC.Talentos.Reporte.BL.BE
{
    public class SectionTemperamentoBE
    {
        public string title { get; set; }
        public string description { get; set; }
        public List<SubSectionTemperamentoBE> sections { get; set; }
        public ConsolidadoTemperamentoBE percentage = new ConsolidadoTemperamentoBE();
    }
}
