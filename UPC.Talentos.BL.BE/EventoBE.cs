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
    public class EventoBE
    {
        private int eventoId;

        public int EventoId
        {
            get { return eventoId; }
            set { eventoId = value; }
        }
        private DateTime fecha;

        public DateTime Fecha
        {
            get { return fecha; }
            set { fecha = value; }
        }
        private string lugar;

        public string Lugar
        {
            get { return lugar; }
            set { lugar = value; }
        }
        private string descripcion;

        public string Descripcion
        {
            get { return descripcion; }
            set { descripcion = value; }
        }
        private string clave;

        public string Clave
        {
            get { return clave; }
            set { clave = value; }
        }
        private int responsableId;

        public int ResponsableId
        {
            get { return responsableId; }
            set { responsableId = value; }
        }
        private int skinId;

        public int SkinId
        {
            get { return skinId; }
            set { skinId = value; }
        }
    }
}
