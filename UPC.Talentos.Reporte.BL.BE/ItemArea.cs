
namespace UPC.Talentos.Reporte.BL.BE
{
    public class ItemArea
    {
        public string Area { get; set; }
        public int NumeroPregunta { get; set; }

        public ItemArea(string pArea, int pNumeroPregunta)
        {
            this.Area = pArea;
            this.NumeroPregunta = pNumeroPregunta;
        }
    }
}
