using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UPC.Talentos.BL.BE;
using UPC.Talentos.DL.DALC;

namespace UPC.Talentos.BL.BC
{
    public class TalentoBC
    {
        //public List<TalentoBE> ObtenerTalentos()
        //{
        //    List<TalentoBE> lstTalentoBE = new List<TalentoBE>();
        //    TalentoDALC objTalentoDALC = new TalentoDALC();

        //    lstTalentoBE = objTalentoDALC.ObtenerTalentos();

        //    return lstTalentoBE;
        //}

        public List<TalentoBE> ListarTalentos()
        {
            List<TalentoBE> lstTalentoBE = new List<TalentoBE>();
            TalentoDALC objTalentoDALC = new TalentoDALC();

            lstTalentoBE = objTalentoDALC.ListarTalentos();

            return lstTalentoBE;
        }

        public List<TalentoBE> ListarTalentosReporteTodos()
        {
            TalentoDALC objTalentoDALC = new TalentoDALC();

            return objTalentoDALC.ListarTalentosReporteTodos();
        }

        public List<TalentoComplexBE> ListarTalentosComplex(int idTalento)
        {
            List<TalentoComplexBE> lstTalentoBE = new List<TalentoComplexBE>();
            TalentoDALC objTalentoDALC = new TalentoDALC();

            lstTalentoBE = objTalentoDALC.ListarTalentosComplex(idTalento);

            return lstTalentoBE;
        }

        public bool ActualizarTalento(TalentoComplexBE objTalento)
        {
            TalentoDALC objTalentoDALC = new TalentoDALC();

            return objTalentoDALC.ActualizarTalento(objTalento);
        }

        public List<RecomendacionBE> ObtenerDescripcionesTalentosMasDesarrollados(List<TalentoComplexBE> lstTalentos, int tipo)
        {
            TalentoDALC objTalentoDALC = new TalentoDALC();
            List<RecomendacionBE> lstTalentosMasDesarrollados = null;
            

                    lstTalentosMasDesarrollados = objTalentoDALC.ObtenerDescripcionesTalentosSeleccionados(lstTalentos[0].idTalento, lstTalentos[1].idTalento, lstTalentos[2].idTalento,
                    lstTalentos[3].idTalento, lstTalentos[4].idTalento, lstTalentos[5].idTalento, lstTalentos[6].idTalento, lstTalentos[7].idTalento, lstTalentos[8].idTalento,
                    lstTalentos[9].idTalento, lstTalentos[10].idTalento, lstTalentos[11].idTalento, 0, 0, 0, 0, 0, 0, tipo);
                   

                

            return lstTalentosMasDesarrollados;
        }

        public List<RecomendacionBE> ObtenerDescripcionesTalentosMenosDesarrollados(List<TalentoComplexBE> lstTalentos, int tipo)
        {
            TalentoDALC objTalentoDALC = new TalentoDALC();
            List<RecomendacionBE> lstTalentosMenosDesarrollados = null;

            lstTalentosMenosDesarrollados = objTalentoDALC.ObtenerDescripcionesTalentosSeleccionados(0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0,
                lstTalentos[0].idTalento, lstTalentos[1].idTalento, lstTalentos[2].idTalento, lstTalentos[3].idTalento, lstTalentos[4].idTalento, lstTalentos[5].idTalento, tipo);

            return lstTalentosMenosDesarrollados;
        }


        //Version Adulto
        public List<RecomendacionBE> ObtenerDescripcionesTalentosMasDesarrolladosAdulto(List<TalentoComplexBE> lstTalentos, int tipo)
        {
            TalentoDALC objTalentoDALC = new TalentoDALC();
            List<RecomendacionBE> lstTalentosMasDesarrollados = null;


            lstTalentosMasDesarrollados = objTalentoDALC.ObtenerDescripcionesAdultoTalentosSeleccionados(lstTalentos[0].idTalento, lstTalentos[1].idTalento, lstTalentos[2].idTalento,
            lstTalentos[3].idTalento, lstTalentos[4].idTalento, lstTalentos[5].idTalento, lstTalentos[6].idTalento, lstTalentos[7].idTalento, lstTalentos[8].idTalento,
            lstTalentos[9].idTalento, lstTalentos[10].idTalento, lstTalentos[11].idTalento, 0, 0, 0, 0, 0, 0, tipo);




            return lstTalentosMasDesarrollados;
        }

        public List<RecomendacionBE> ObtenerDescripcionesTalentosMenosDesarrolladosAdulto(List<TalentoComplexBE> lstTalentos, int tipo)
        {
            TalentoDALC objTalentoDALC = new TalentoDALC();
            List<RecomendacionBE> lstTalentosMenosDesarrollados = null;

            lstTalentosMenosDesarrollados = objTalentoDALC.ObtenerDescripcionesAdultoTalentosSeleccionados(0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0,
                lstTalentos[0].idTalento, lstTalentos[1].idTalento, lstTalentos[2].idTalento, lstTalentos[3].idTalento, lstTalentos[4].idTalento, lstTalentos[5].idTalento, tipo);

            return lstTalentosMenosDesarrollados;
        }

    }

}