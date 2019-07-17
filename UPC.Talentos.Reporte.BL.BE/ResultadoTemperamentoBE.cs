using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace UPC.Talentos.Reporte.BL.BE
{
    public class ResultadoTemperamentoBE
    {
        public List<SectionTemperamentoBE> sections { get; set; }
        public ConsolidadoTemperamentoBE percentage = new ConsolidadoTemperamentoBE();
    }
}
