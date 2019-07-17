
namespace UPC.Talentos.BL.BE
{
    public class CuadroResultadoBE
    {
        public int CuadroResultado_Id { get; set; }
        public string Texto { get; set; }
        public int Talento_Id { get; set; }
        public string Talento { get; set; }
        public int Tendencia_Id { get; set; }
        public string Tendencia { get; set; }
        public int TipoTalento_Id { get; set; }
        public bool Seleccionado { get; set; }
        public int TipoDesarrollo { get; set; } // Más Desarrollado: 1, Menos Desarrollado: 2, Taleno Especifico: 3, Virtud: 4

        public CuadroResultadoBE()
        {
            this.Seleccionado = false;
            this.TipoDesarrollo = 0;
        }
    }
}
