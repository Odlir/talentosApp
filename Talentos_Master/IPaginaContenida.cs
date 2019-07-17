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

namespace Talentos_Master
{
    public class IPaginaContenida: UserControl
    {
        protected Comun.CambiarContenido _cambiarContenido;
        protected Comun.CambiarInstruccion _cambiarInstruccion;
        protected Comun.CambiarNavegacion _cambiarNavegacion;
        //protected Comun.OcultarInstrucciones _OcutarInstrucciones;
        protected Comun.ValidarSeleccionTalentos _ValidarSelecciones;

        public void SetDelegate(Comun.CambiarContenido lpDelegate)
        {
            _cambiarContenido = lpDelegate;
        }
        public void SetDelegate(Comun.CambiarInstruccion lpDelegate)
        {
            _cambiarInstruccion = lpDelegate;
        }

        public void SetDelegate(Comun.CambiarNavegacion lpDelegate)
        {
            _cambiarNavegacion = lpDelegate;
        }

        //public void SetDelegate(Comun.OcultarInstrucciones lpDelegate)
        //{
        //    _OcutarInstrucciones = lpDelegate;
        //}

        public void SetDelegate(Comun.ValidarSeleccionTalentos lpDelegate)
        {
            _ValidarSelecciones = lpDelegate;
        }


    }
}
