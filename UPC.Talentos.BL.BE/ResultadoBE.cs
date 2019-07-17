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

namespace UPC.Talentos.BL.BE
{
    public class ResultadoBE
    {
        public string DNI { get; set; }
        public string CodEvaluacion { get; set; }
        public string CorreoElectronico { get; set; }
        private int _Resultado_id;

        public bool EsMasivo { get; set; }

        private String buzonId;

        public String BuzonId
        {
            get { return buzonId; }
            set { buzonId = value; }
        }
        private String talentoId;

        public String TalentoId
        {
            get { return talentoId; }
            set { talentoId = value; }
        }
        private String _Seleccionado;

        public String Seleccionado
        {
            get { return _Seleccionado; }
            set { _Seleccionado = value; }
        }
        private String _Puntaje;

        public String Puntaje
        {
            get { return _Puntaje; }
            set { _Puntaje = value; }
        }

        private DateTime fecha;

        public DateTime Fecha
        {
            get { return fecha; }
            set { fecha = value; }
        }

        private int participante_id;

        public int Participante_id
        {
            get { return participante_id; }
            set { participante_id = value; }
        }

        public string NombreParticipante { get; set; }

        public string TendenciaId { get; set; }
        public string TipoDesarrollo { get; set; } // Más Desarrollado: 1, Menos Desarrollado: 2, Taleno Especifico: 3, Virtud: 4

        //TODO: Por eliminar
        private int _Buzon_id;
        private int _Talento_id;

        public int Resultado_id
        {
            get { return _Resultado_id; }
            set { _Resultado_id = value; }
        }



      


        public int Buzon_id
        {
            get { return _Buzon_id; }
            set { _Buzon_id = value; }
        }



        private int _CantAmarillo;

        public int CantAmarillo
        {
            get { return _CantAmarillo; }
            set { _CantAmarillo = value; }
        }
        private int _CantAnaranjado;

        public int CantAnaranjado
        {
            get { return _CantAnaranjado; }
            set { _CantAnaranjado = value; }
        }
        private int _CantRojo;

        public int CantRojo
        {
            get { return _CantRojo; }
            set { _CantRojo = value; }
        }
        private int _CantAzul;

        public int CantAzul
        {
            get { return _CantAzul; }
            set { _CantAzul = value; }
        }
        private int _CantVerde;

        public int CantVerde
        {
            get { return _CantVerde; }
            set { _CantVerde = value; }
        }
        private int _CantGuinda;

        public int CantGuinda
        {
            get { return _CantGuinda; }
            set { _CantGuinda = value; }
        }
    }
}
