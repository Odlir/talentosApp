using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace UPC.Talentos.Reporte.BL.BE
{
    public class TemperamentoBE
    {
        public string temperamentos_id { get; set; }
        public string intereses_id { get; set; }
        public PersonaBE evaluated = new PersonaBE();
        public PersonaBE evaluator = new PersonaBE();
        public FechaEvaluacionBE initdate = new FechaEvaluacionBE();
        public FechaEvaluacionBE enddate = new FechaEvaluacionBE();
        public ResultadoTemperamentoBE result { get; set; }
    }

    public class ReporteTemperamentoBE
    {
        public TemperamentoBE report = new TemperamentoBE();
    }
}
