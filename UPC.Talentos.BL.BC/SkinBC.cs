using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UPC.Talentos.BL.BE;
using UPC.Talentos.DL.DALC;

namespace UPC.Talentos.BL.BC
{
    public class SkinBC
    {
        public SkinBE SkinObtener(string descripcion)
        {
            SkinDALC dalc = new SkinDALC();
            return dalc.SkinObtener(descripcion);
        }

        public SkinBE SkinActivoObtener()
        {
            SkinDALC dalc = new SkinDALC();
            return dalc.SkinActivoObtener();
        }

        public bool SkinActualizar(SkinBE be)
        {
            SkinDALC dalc = new SkinDALC();
            return dalc.ActualizarSkin(be);
        }
    }
}
