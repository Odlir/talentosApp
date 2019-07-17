
namespace UPC.Talentos.BL.BE
{
    public class ResultadoParaleloBE
    {
        public int Resultado_Id { get; set; }
        public int Participante_Id { get; set; }
        public string Nombres { get; set; }
        public string ApellidoPaterno { get; set; }
        public string ApellidoMaterno { get; set; }
        public string DNI { get; set; }
        public string Sexo { get; set; }
        public string FechaNacimiento { get; set; }
        public string NivelInstruccion { get; set; }
        public string CargoEmpresa { get; set; }
        public string CorreoElectronico { get; set; }
        public string MasDesarrollados { get; set; }
        public string MenosDesarrollados { get; set; }
        public string TalentosEspecificos { get; set; }
        public string TalentosEspecificosMas { get; set; }
        public string TalentosEspecificosMenos { get; set; }
        public string Virtudes { get; set; }
        public int Buzon_Id { get; set; }
        //public int CantidadMasDesarrollado { get; set; }
        //public int CantidadIntermedioDesarrollado { get; set; }
        //public int CantidadMenosDesarrollado { get; set; }
        public bool EsMasivo { get; set; }
        public string Fecha { get; set; }
        public string Empresa { get; set; }
        public string CodigoEvaluacion { get; set; }
    }
}
