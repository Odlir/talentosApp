
namespace UPC.Talentos.Reporte.BL.BE
{
    public class PreguntaBE
    {
        public string id { get; set; }
        public string section { get; set; }
        public int question_number { get; set; }
        public string question { get; set; }
        public int sequence { get; set; }
        public int score { get; set; }
        public string answer { get; set; }
        public int ceil_score { get; set; }
        public double result_score { get; set; }
        //public int PuntajeGusto { get; set; }
        //public int PuntajeHabilidades { get; set; }
        //public int PuntajeSatisfaccion { get; set; }
    }
}
