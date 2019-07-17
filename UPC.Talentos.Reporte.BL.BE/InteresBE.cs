using System.Collections.Generic;

namespace UPC.Talentos.Reporte.BL.BE
{
    public class InteresBE
    {
        public string intereses_id { get; set; }
        public PersonaBE evaluated = new PersonaBE();
        public EncuestaBE encuesta = new EncuestaBE();
        public PersonaBE evaluator = new PersonaBE();
        public FechaEvaluacionBE initdate = new FechaEvaluacionBE();
        public FechaEvaluacionBE enddate = new FechaEvaluacionBE();
        public PreguntaBE[] result;
    }

    public class ReporteBE
    {
        public InteresBE report = new InteresBE();
    }
}
