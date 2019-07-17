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
    public class Enumerador
    {
        public enum Pagina
        {
            Inicio = 1,
            Login = 2,
            PrimeraEtapa = 3,
            Registrarse = 4,
            ClasificacionPrincipal = 6,
            //ResultadosClasificacion = 7,
            Calificacion = 8,
            ClasificacionDetalle = 9,
            ClasificacionDetalleTE = 10,
            SeleccionarPrincipal = 12,
            MasterRueda = 14,
            MasterRuedaSegundaEtapa = 15,
            SegundaEtapa = 16,
            TerceraEtapa = 17,
            ResultadoIndividual = 18,
            ResultadoEstadistico = 19,
            ResultadoEstadisticoComponent = 20,
            ResultadoRadar = 21,
            Sugerencias=22,
            AgradecimientoJuego=23,
            AgradecimientoFinal=24,
            DetalleRecomendacion=25,
            SeleccionarTalentosEspecificos = 26,
            SeleccionarVirtudes = 27
        }
        public enum Instruccion
        {
            PrimeraInstruccion = 1,
            SegundaInstruccion = 2,
            TerceraInstruccion = 3,
            CuartaInstruccion = 4,
            SegundaInstruccionMeIdentifica = 5,
            SegundaInstruccionNoMeIdentifica = 6,
            AsignaPuntajeMeIdentifica = 7,
            AsignaPuntajeNoMeIdentifica = 8,
            instruccionCorreccion=9,
            instruccionCorreccionTE = 10,
            //ResultInstrucciones=10,
            EnBlanco=11,
            Anterior=12,
            Siguiente=13,
            SinNavegacion=14,
            InstruccionTalentoEspecifico = 15,
            InstruccionVirtudes = 16
        }

        public enum Navegacion
        {
            Anterior=1,
            Siguiente=2,
            Blanco=3,
        }
    }
}
