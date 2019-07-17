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
    public class JuegoBE
    {
        //TODO: (ITERACION 5) Personalizar Juego por eventos: Skins personalizados

        private int _Juego_id;
        private int eventoId;
        private int participanteId;

        //TODO: Por eliminar
        private DateTime _Fecha;

        private string _NickName;

        #region Metodos de Acceso

        public int Juego_id
        {
            get { return _Juego_id; }
            set { _Juego_id = value; }
        }

        public int EventoId
        {
            get { return eventoId; }
            set { eventoId = value; }
        }

        public int ParticipanteId
        {
            get { return participanteId; }
            set { participanteId = value; }
        }

        public string NickName
        {
            get { return _NickName; }
            set { _NickName = value; }
        }


        public DateTime Fecha
        {
            get { return _Fecha; }
            set { _Fecha = value; }
        }

        #endregion
    }
}
