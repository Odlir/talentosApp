using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace UPC.Talentos.Reporte.BL.BE
{
    public class PreguntaTemperamentoBE
    {
        public string id { get; set; }
        public int question_number { get; set; }
        public string question { get; set; }
        public int sequence { get; set; }
        public int score { get; set; }
        public string answer { get; set; }
        public int ceil_score { get; set; }
        public double result_score { get; set; }
    }
}
