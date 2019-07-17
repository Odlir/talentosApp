using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UPC.Talentos.DL.DALC;
using UPC.Talentos.BL.BE;

namespace UPC.Talentos.BL.BC
{
    public class BuzonBC
    {
        public BuzonBE ObtenerBuzon()
        {
            BuzonBE be = new BuzonBE();
            be.IdBuzon = 2;
            return be;
        }
    }
}
