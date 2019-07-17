using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UPC.Talentos.BL.BE;
using UPC.Talentos.DL.DALC;

namespace UPC.Talentos.BL.BC
{
    public class TendenciaBC
    {
        public TendenciaBE ObtenerTendencias(int codTend)
        {
            TendenciaBE TendenciaBE = new TendenciaBE();
            TendenciaDALC objTendenciaDALC = new TendenciaDALC();

            TendenciaBE = objTendenciaDALC.TendenciaObtener(codTend);

            return TendenciaBE;
        }

        public List<TendenciaBE> ListarTendencias()
        {
            List<TendenciaBE> lstTendencias = new List<TendenciaBE>();
            TendenciaDALC objTendenciaDALC = new TendenciaDALC();

            lstTendencias = objTendenciaDALC.ListarTendencias();

            return lstTendencias;
        }

        public bool ActualizarTendencia(TendenciaBE objTendencia)
        {
            TendenciaDALC objTendenciaDALC = new TendenciaDALC();

            return objTendenciaDALC.ActualizarTendencia(objTendencia);
        }

        public TendenciaBE TendenciaObtener(int codTendencia)
        {
            TendenciaDALC objTendenciaDALC = new TendenciaDALC();

            return objTendenciaDALC.TendenciaObtener(codTendencia);
        }
    }
}
