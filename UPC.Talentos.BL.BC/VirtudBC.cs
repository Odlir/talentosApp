using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UPC.Talentos.BL.BE;
using UPC.Talentos.DL.DALC;

namespace UPC.Talentos.BL.BC
{
    public class VirtudBC
    {
        public List<TalentoComplexBE> ListarVirtudes(int idVirtud)
        {
            VirtudDALC objVirtudDALC = new VirtudDALC();

            return objVirtudDALC.ListarVirtudes(idVirtud);
        }

        public bool ActualizarVirtud(TalentoComplexBE objVirtud)
        {
            VirtudDALC objVirtudDALC = new VirtudDALC();

            return objVirtudDALC.ActualizarVirtud(objVirtud);
        }
    }
}
