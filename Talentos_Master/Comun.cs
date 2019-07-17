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
    //Esta clase sirve para la Navegabilidad entre páginas Silverlight }
    //y el cambio de instrucciones según la página actual en la que se encuentre el jugador

    public class Comun
    {
        public delegate void CambiarContenido(Enumerador.Pagina ePaginaContenida);
        public delegate void CambiarInstruccion(Enumerador.Instruccion eInstruccion);
        public delegate void CambiarNavegacion(Enumerador.Navegacion eNavegacion);
        
        public delegate void ValidarSeleccionTalentos();
    }
}
