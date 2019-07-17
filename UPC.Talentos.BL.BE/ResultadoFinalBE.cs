using System.Collections.Generic;

namespace UPC.Talentos.BL.BE
{
    public class ResultadoFinalBE
    {
        public int Participante_Id { get; set; }
        public string NombreParticipante { get; set; }
        public string ApellidoPaterno { get; set; }
        public string ApellidoMaterno { get; set; }
        public string DNI { get; set; }
        public string Sexo { get; set; }
        public string FechaNacimiento { get; set; }
        public string FechaTest { get; set; }
        public string NivelInstruccion { get; set; }
        public string CargoEmpresa { get; set; }
        public string CorreoElectronico { get; set; }
        public string Institucion { get; set; }
        public List<TalentoComplexBE> Talentos { get; set; }
        public List<TalentoComplexBE> lstTalentosMasDesarrollados { get; set; }
        public List<TalentoComplexBE> lstTalentosMenosDesarrollados { get; set; }
        public List<TalentoComplexBE> lstTalentosIntermedioDesarrollados { get; set; }
        public List<TalentoComplexBE> lstTEMasDesarrollados { get; set; }
        public List<TalentoComplexBE> lstTEMenosDesarrollados { get; set; }
        public List<TalentoComplexBE> lstVirtudes { get; set; }
        public List<TalentoComplexBE> lstTalentosTotales { get; set; }
        public double Puntaje { get; set; }
        public string CodigoEvaluacion { get; set; }
    }
}
