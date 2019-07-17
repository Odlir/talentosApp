using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UPC.Talentos.BL.BE;
using UPC.Talentos.DL.DALC;

namespace UPC.Talentos.BL.BC
{
    public class ParametroBC
    {
        public ParametroBE obtenerTemaActual()
        {
            ParametroDALC objParametroDALC = new ParametroDALC();
            ParametroBE objParametroBE = new ParametroBE();
            objParametroBE = objParametroDALC.obtenerTemaActual();
            return objParametroBE;
        }
        public void actualizarTemaActual(ParametroBE objParametroBE)
        {
            ParametroDALC objParamteroDALC = new ParametroDALC();
            objParamteroDALC.actualizarTemaActual(objParametroBE);
        }
    }
}
