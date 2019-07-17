using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UPC.Talentos.BL.BE;
using UPC.Talentos.DL.DALC;

namespace UPC.Talentos.BL.BC
{
    public class TipoTalentoBC
    {
        public List<TipoTalentoBE> ListarTipoTalento()
        {
            TipoTalentoDALC objTipoTalento = new TipoTalentoDALC();

            return objTipoTalento.ListarTipoTalento();
        }
    }
}
