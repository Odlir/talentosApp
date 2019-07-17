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
using System.Collections.Generic;

namespace UPC.Talentos.BL.BE
{
    public class BuzonBE
    {
        private int idBuzon;

        public int IdBuzon
        {
            get { return idBuzon; }
            set { idBuzon = value; }
        }

        private String descripcion;

        public String Descripcion
        {
            get { return descripcion; }
            set { descripcion = value; }
        }


        private List<TalentoBE> _lstTalento;

        public List<TalentoBE> lstTalento
        {
            get { return _lstTalento; }
            set { lstTalento = value; }
        }

        private List<Image> lstImagebuzon;

        public List<Image> LstImagebuzon
        {
            get { return lstImagebuzon; }
            set { lstImagebuzon = value; }
        }

        private List<Image> lstImgEspaldabuzon;

        public List<Image> LstImgEspaldabuzon
        {
            get { return lstImgEspaldabuzon; }
            set { lstImgEspaldabuzon = value; }
        }


        private List<Border> lstBuzon1Borde;

        public List<Border> LstBuzon1Borde
        {
            get { return lstBuzon1Borde; }
            set { lstBuzon1Borde = value; }
        }

        private int puntajeBuzon;

        public int PuntajeBuzon
        {
            get { return puntajeBuzon; }
            set { puntajeBuzon = value; }
        }

        private bool _activo;

        public bool activo
        {
            get { return _activo; }
            set { _activo = value; }
        }



        //public BuzonBE()
        //{
        //    lstTalento = new List<TalentoBE>();
        //    lstTalento.Clear();
        //    lstImagebuzon = new List<Image>();
        //    lstImgEspaldabuzon = new List<Image>();
        //    lstBuzon1Borde = new List<Border>();
        //    puntajeBuzon = 0;
        //    _activo = false;


        //}


    }
}
