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
    //Esta clase representa a cada uno de los talentos.
    public class TalentoBE
    {
        private int idTalento;
        private String nombre;
        private String descripcion;
        public int TipoTalento { get; set; } // 1: General, 2: Especifico, 3: Virtud

        public String Descripcion
        {
            get { return descripcion; }
            set { descripcion = value; }
        }
        private int idTendencia;

        private String nombreTendencia;

        public String NombreTendencia
        {
            get { return nombreTendencia; }
            set { nombreTendencia = value; }
        }
        private String colorTendencia;

        public String ColorTendencia
        {
            get { return colorTendencia; }
            set { colorTendencia = value; }
        }


        //TODO: eliminar 
        private int idColor;
        private String recomendacion;


        //TODO: esto pertenece a skinbe. Posiblemente, se queda
        private String example;  // url de la espalda
        private String image;  // url de la imagen
        public string ImagenEspalda { get; set; }

        //TODO: esto pertenece a resultadobe. Posiblemente, se queda
        public bool seleccionado { get; set; }
        public int puntaje { get; set; }

        public int ordenInsercion { get; set; }

        public TalentoBE()
        {
        }

        public TalentoBE(String nombre, String descrip, int idTalento, String example, String image, int idColor)
        {
            this.idTalento = idTalento;
            this.nombre = nombre;
            this.descripcion = descrip;
            this.example = example;
            this.image = image;
            this.idColor = idColor;
            seleccionado = false;
            puntaje = 0;
            ordenInsercion = 0;
        }

        public TalentoBE(String descripcion, int idTalento, int idColor, String recomendacion)
        {
            this.idTalento = idTalento;
            this.nombre = descripcion;
            this.idColor = idColor;
            this.recomendacion = recomendacion;
        }

        #region Metodos de acceso

        public int IdTalento
        {
            get { return idTalento; }
            set { idTalento = value; }
        }

        public String Nombre
        {
            get { return nombre; }
            set { nombre = value; }
        }

        public String Example
        {
            get { return example; }
            set { example = value; }
        }

        public String Image
        {
            get { return image; }
            set { image = value; }
        }

        public int IdColor
        {
            get { return idColor; }
            set { idColor = value; }
        }


        public int IdTendencia
        {
            get { return idTendencia; }
            set { idTendencia = value; }
        }

        public String Recomendacion
        {
            get { return recomendacion; }
            set { recomendacion = value; }
        }


        #endregion
    }
}
