using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using UPC.Talentos.BL.BE;
using UPC.Talentos.DL.DALC;

namespace UPC.Talentos.BL.BC
{
    public class JuegoBC
    {
        
        public int JuegoInsertar(JuegoBE objJuego)
        {
            JuegoDALC objDalc = new JuegoDALC();
            return (objDalc.JuegoInsertar(objJuego));
          
        }

        public int InsertarJuego(JuegoBE objJuego)
        {
            JuegoDALC dalc = new JuegoDALC();
            return (dalc.InsertarJuego(objJuego));
        }    
    }
}
