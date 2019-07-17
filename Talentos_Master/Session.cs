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
using System.Collections.ObjectModel;
using Talentos_Master.SeguridadReference;
using Talentos_Master.TalentosReference;
using Talentos_Master.ReportesReference;
using System.Collections.Generic;

namespace Talentos_Master
{
    public class Session
    {
        public string CorreoParticipanteMasivo { get; set; }
        public bool EsMasivo { get; set; }
        public string DNI { get; set; }
        public string CodEvaluacion { get; set; }
        public List<CuadroResultadoBE> CuadroResultado { get; set; }
        public string Token { get; set; }

        //apuntar todo a los BE de la referencia
        //TODO: Cambiar UsuarioBE por ParticipanteBE
        public UsuarioBE participante { get; set; }
        public JuegoBE juego { get; set; }
        //public EventoBE evento { get; set; }
        public SkinBE skin { get; set; }

        //public List<TendenciaBE> tendencias { get; set; }

        public ObservableCollection<TalentoBE> lstTalentos { get; set; }
        public ObservableCollection<TalentoBE> lstTalentosEspecificos { get; set; }

        public Buzon Buzon1 { get; set; }
        public Buzon Buzon2 { get; set; }
        public Buzon Buzon3 { get; set; }
        //public Buzon BuzonTE { get; set; }
        public Buzon BuzonTEMas { get; set; }
        public Buzon BuzonTEMenos { get; set; }
        public Buzon BuzonTEIntermedio { get; set; }
        public Buzon BuzonVirtudes { get; set; }

        //lista de imagenes (ya instanciadas) de los talentos que han sido clasificados en cada uno de los buzones
        public List<Image> lstImages { get; set; }
        public List<Image> lstImagesTE { get; set; }

        //TODO: PARA BUZONBE
        //public List<Image> lstImagebuzon1 { get; set; }
        //public List<Image> lstImagebuzon2 { get; set; }
        //public List<Image> lstImagebuzon3 { get; set; }

        //lista de imagenes (ya instanciadas) del reverso de los talentos que han sido clasificados en cada uno de los buzones
        public List<Image> lstImgEspalda { get; set; }
        public List<Image> lstImgEspaldaTE { get; set; }
        //TODO: PARA BUZONBE
        //public List<Image> lstImgEspaldabuzon1 { get; set; }
        //public List<Image> lstImgEspaldabuzon2 { get; set; }
        //public List<Image> lstImgEspaldabuzon3 { get; set; }

        //lista de bordes (ya instanciados) de los talentos que han sido clasificados en cada uno de los buzones
        //TODO: PARA BUZONBE
        //public List<Border> lstBuzon1Borde { get; set; }
        //public List<Border> lstBuzon2Borde { get; set; }
        //public List<Border> lstBuzon3Borde { get; set; }

        //puntaje total asignado a los talentos correspondientes a cada buzón
        //TODO: PARA BUZONBE
        //public int puntajeBuzon_1 { get; set; }
        //public int puntajeBuzon_2 { get; set; }
        //public int puntajeBuzon_3 { get; set; }

        //TODO: PARA BUZONBE
        //public bool b1 { get; set; } //indica si el jugador se encuentra viendo los talentos del buzón 1
        //public bool b2 { get; set; } //indica si el jugador se encuentra viendo los talentos del buzón 2
        //public bool b3 { get; set; } //indica si el jugador se encuentra viendo los talentos del buzón 3

        public bool terminoClasificacion { get; set; }
        public bool terminoSeleccion { get; set; }
        public bool terminoSeleccionFinal { get; set; }
        public bool terminoClasificacionTE { get; set; }
        public bool terminoSeleccionTE { get; set; }
        public bool terminoCalificacion { get; set; }
       
        public ResultadoBE resultado {get; set;}
        
        // public int resultadoId { get; set; }

        public bool paso1 { get; set; }
        public bool paso2 { get; set; }
        public bool paso3 { get; set; }
        public bool paso4 { get; set; }
        public bool pasoCorrec { get; set; }
        public bool pasoCorrecTE { get; set; }
        public bool pasoTE { get; set; }
        public bool pasoVirtud { get; set; }

        public bool revisaSelec { get; set; }
        public bool revisaSelecTE { get; set; }
        public bool revisaClasif { get; set; }

        public int cantSeleccionadosBuzon1 { get; set; }
        public int cantSeleccionadosBuzon3 { get; set; }
        public int cantSeleccionadosBuzonTEMas { get; set; }
        public int cantSeleccionadosBuzonTEMenos { get; set; }
        public int cantSeleccionadosBuzonTE { get; set; }
        public int cantSeleccionadosBuzonVirtudes { get; set; }

        public int cantCalificadosBuzon1 { get; set; }
        public int cantCalificadosBuzon2 { get; set; }
        public int cantCalificadosBuzonTEMas { get; set; }
        public int cantCalificadosBuzonTEIntermedio { get; set; }
        //public int cantCalificadosBuzonTE { get; set; }
        public int cantCalificadosBuzonVirtud { get; set; }

        private static Session instance = null; //Instancia única 

        public const int MAX_TALENTOS_MAS_DESARROLLADOS = 12;
        public const int MAX_TALENTOS_MENOS_DESARROLLADOS = 6;
        public const int MAX_TALENTOS_ESPECIFICOS = 9;
        public const int MAX_VIRTUDES = 3;

        private Session()
        {
            TalentosReference.WSTalentosSoapClient client = new WSTalentosSoapClient();
            ReportesReference.wsReporteSoapClient reporteClient = new wsReporteSoapClient();
            lstTalentos = new ObservableCollection<TalentoBE>();
            lstTalentosEspecificos = new ObservableCollection<TalentoBE>();
            participante = new UsuarioBE();
            resultado = new ResultadoBE();
            juego = new JuegoBE();
            CuadroResultado = new List<CuadroResultadoBE>();

            cantSeleccionadosBuzon1 = 0;
            cantSeleccionadosBuzon3 = 0;
            cantSeleccionadosBuzonTE = 0;
            cantSeleccionadosBuzonVirtudes = 0;

            cantCalificadosBuzon1=0;
            cantCalificadosBuzon2 = 0;
            cantCalificadosBuzonTEMas = 0;
            cantCalificadosBuzonTEIntermedio = 0;
            cantCalificadosBuzonVirtud = 0;
            terminoSeleccionFinal = false;

            //TODO: INVOCAR WS
            //TalentoDALC objtalentoDALC = new TalentoDALC();
            //lstTalentos = objtalentoDALC.ObtenerTalentos();
            client.ListarTalentosCompleted+= new EventHandler<Talentos_Master.TalentosReference.ListarTalentosCompletedEventArgs>(obtenerTalentos_Completed);
            client.ListarTalentosAsync();

            //reporteClient.CuadroResultadoListarCompleted +=new EventHandler<CuadroResultadoListarCompletedEventArgs>(ObtenerCuadroResultado_Completed);
            //reporteClient.CuadroResultadoListarAsync();
        }

        private void obtenerTalentos_Completed(object sender, TalentosReference.ListarTalentosCompletedEventArgs e)
        {
            ObservableCollection<TalentoBE> lstTalentosAux = e.Result;
            //this.lstTalentos = e.Result;
            lstImgEspalda = new List<Image>();
            lstImgEspaldaTE = new List<Image>();
            lstImages = new List<Image>();
            lstImagesTE = new List<Image>();

            Buzon1 = new Buzon();
            Buzon2 = new Buzon();
            Buzon3 = new Buzon();
            BuzonTEIntermedio = new Buzon();
            BuzonTEMas = new Buzon();
            BuzonTEMenos = new Buzon();
            BuzonVirtudes = new Buzon();

            foreach (TalentoBE item in lstTalentosAux)
            {
                item.Image = "http://www.talentosdavidfischman.com/TalentosAdminUPC" + item.Image;
                item.ImagenEspalda = "http://www.talentosdavidfischman.com/TalentosAdminUPC" + item.Example;
                item.Example = "http://www.talentosdavidfischman.com/TalentosAdminUPC" + item.Example;

                if (item.TipoTalento.Equals(1)) // Talentos generales
                {
                    this.lstTalentos.Add(item);
                }
                else if (item.TipoTalento.Equals(2)) // Talentos especificos
                {
                    this.lstTalentosEspecificos.Add(item);
                }
                else if (item.TipoTalento.Equals(3)) // Virtud
                {
                    BuzonVirtudes.lstTalento.Add(item);
                }
            }

            this.BuzonVirtudes = BuzonVirtudes;
        }

        private void ObtenerCuadroResultado_Completed(object sender, ReportesReference.CuadroResultadoListarCompletedEventArgs e)
        {
            List<CuadroResultadoBE> lstCuadroResultado = e.Result;

            CuadroResultado = lstCuadroResultado;
        }

        //aplicando el patrón Singleton
        public static Session getInstance()
        {
            if (instance == null)
                instance = new Session ();
            return instance;
        }

        public static Session deleteInstance()
        {
            instance = new Session();
            return instance;
        }

        public static Session ReiniciarInstance()
        {
            instance = null;
            return instance;
        }
    }
}
