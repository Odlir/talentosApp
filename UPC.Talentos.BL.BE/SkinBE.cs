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
    public class SkinBE
    {
        private int skinId;

        public int SkinId
        {
            get { return skinId; }
            set { skinId = value; }
        }
        private String descripcion;

        public String Descripcion
        {
            get { return descripcion; }
            set { descripcion = value; }
        }
        private String rutaTalentos;

        public String RutaTalentos
        {
            get { return rutaTalentos; }
            set { rutaTalentos = value; }
        }
        private String rutaEjemplos;

        public String RutaEjemplos
        {
            get { return rutaEjemplos; }
            set { rutaEjemplos = value; }
        }
        private int activo;

        public int Activo
        {
            get { return activo; }
            set { activo = value; }
        }
    }
}
