using System;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;

namespace UPC.Seguridad.Talentos.BL.BE
{
    public class UsuarioBE
    {
        public int UsuarioId { set; get; } //
        public string Nombres { set; get; }//
        public string APaterno { set; get; } //
        public string AMaterno { set; get; } //
        public int Sexo { set; get; } //
        public string Correo { set; get; } //
        public string Password { set; get; } //
        public DateTime FechaNac { set; get; } // + edad
        public string Direccion { set; get; }//
        public string TelfFijo { set; get; } //
        public string TelfMovil { set; get; } //
        public int NivelInstruccionId { set; get; }//
        public int TipoParticipanteId { set; get; }//
        public int CarreraId { set; get; }//
        public string Carrera { set; get; }
        public string CentroEstudio { set; get; }//
        public int DistritoEstudioId { set; get; }//
        public string CentroTrabajo { set; get; }//
        public string DireccionCentroTrabajo { set; get; }//
        public int DistritoTrabajoId { set; get; }
        public string TelfTrabajo { set; get; }
        public string CorreoTrabajo { set; get; }
        public string CargoTrabajo { set; get; }
        public int OcupacionId { set; get; }
        public string NickName { get; set; }
    }
}
