using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UPC.Talentos.DL.DALC;
using UPC.Talentos.BL.BE;

namespace UPC.Talentos.BL.BC
{
    public class RecomendacionBC
    {
        public List<RecomendacionBE> ObtenerRecomedacion(int idTalento)
        {
            RecomendacionDALC dalc = new RecomendacionDALC();
            return dalc.ObtenerRecomedacion(idTalento);
        }

        public List<RecomendacionBE> ObtenerSugerenciasTalentosSeleccionados(List<TalentoComplexBE> lstTalentosMas, List<TalentoComplexBE> lstTalentosMenos, bool administrador)
        {
            RecomendacionDALC objRecomendacionDALC = new RecomendacionDALC();
            List<RecomendacionBE> lstSugerencias = null;


            if (administrador)
            {
                        lstSugerencias = objRecomendacionDALC.ObtenerSugerenciasTalentosSeleccionados(lstTalentosMas[0].idTalento,
                        lstTalentosMas[1].idTalento, lstTalentosMas[2].idTalento, lstTalentosMas[3].idTalento,
                        lstTalentosMas[4].idTalento, lstTalentosMas[5].idTalento, lstTalentosMas[6].idTalento,
                        lstTalentosMas[7].idTalento, lstTalentosMas[8].idTalento, lstTalentosMas[9].idTalento,
                        lstTalentosMas[10].idTalento, lstTalentosMas[11].idTalento,
                        lstTalentosMenos[0].idTalento, lstTalentosMenos[1].idTalento, lstTalentosMenos[2].idTalento,
                        lstTalentosMenos[3].idTalento, lstTalentosMenos[4].idTalento, lstTalentosMenos[5].idTalento);
                        
                
            }
            else
            {
                
                        lstSugerencias = objRecomendacionDALC.ObtenerSugerenciasTalentosSeleccionados(lstTalentosMas[0].idTalento,
                        lstTalentosMas[1].idTalento, lstTalentosMas[2].idTalento, lstTalentosMas[3].idTalento,
                        lstTalentosMas[4].idTalento, lstTalentosMas[5].idTalento, lstTalentosMas[6].idTalento,
                        lstTalentosMas[7].idTalento, lstTalentosMas[8].idTalento, lstTalentosMas[9].idTalento,
                        lstTalentosMas[10].idTalento, lstTalentosMas[11].idTalento, 0, 0, 0, 0, 0, 0);
                        
            }

            return lstSugerencias;
        }

        public List<RecomendacionBE> ObtenerSugerenciasTalentosAdultoSeleccionados(List<TalentoComplexBE> lstTalentosMas, List<TalentoComplexBE> lstTalentosMenos, bool administrador)
        {
            RecomendacionDALC objRecomendacionDALC = new RecomendacionDALC();
            List<RecomendacionBE> lstSugerencias = null;


            if (administrador)
            {
                lstSugerencias = objRecomendacionDALC.ObtenerSugerenciasTalentosAdultoSeleccionados(lstTalentosMas[0].idTalento,
                lstTalentosMas[1].idTalento, lstTalentosMas[2].idTalento, lstTalentosMas[3].idTalento,
                lstTalentosMas[4].idTalento, lstTalentosMas[5].idTalento, lstTalentosMas[6].idTalento,
                lstTalentosMas[7].idTalento, lstTalentosMas[8].idTalento, lstTalentosMas[9].idTalento,
                lstTalentosMas[10].idTalento, lstTalentosMas[11].idTalento,
                lstTalentosMenos[0].idTalento, lstTalentosMenos[1].idTalento, lstTalentosMenos[2].idTalento,
                lstTalentosMenos[3].idTalento, lstTalentosMenos[4].idTalento, lstTalentosMenos[5].idTalento);


            }
            else
            {

                lstSugerencias = objRecomendacionDALC.ObtenerSugerenciasTalentosAdultoSeleccionados(lstTalentosMas[0].idTalento,
                lstTalentosMas[1].idTalento, lstTalentosMas[2].idTalento, lstTalentosMas[3].idTalento,
                lstTalentosMas[4].idTalento, lstTalentosMas[5].idTalento, lstTalentosMas[6].idTalento,
                lstTalentosMas[7].idTalento, lstTalentosMas[8].idTalento, lstTalentosMas[9].idTalento,
                lstTalentosMas[10].idTalento, lstTalentosMas[11].idTalento, 0, 0, 0, 0, 0, 0);

            }

            return lstSugerencias;
        }
    }
}
