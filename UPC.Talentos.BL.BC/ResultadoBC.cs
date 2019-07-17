using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UPC.Talentos.BL.BE;
using UPC.Talentos.DL.DALC;

namespace UPC.Talentos.BL.BC
{
    public class ResultadoBC
    {


        public int InsertarResultado(ResultadoBE objResultado)
        {
            ResultadoDALC objResultadoDALC = new ResultadoDALC();
            int idResultado = 0;

            try
            {
                idResultado = objResultadoDALC.InsertarResultado(objResultado);

                if (idResultado > 0)
                {
                    objResultado.Resultado_id = idResultado;

                    objResultadoDALC.ResultadoActualizar(objResultado);
                }
                else
                    throw new Exception("El código devuelto al registrar el resultado fue de cero.");
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return idResultado;
            
        }

        public int InsertarResultadoAdulto(ResultadoBE objResultado)
        {
            ResultadoDALC objResultadoDALC = new ResultadoDALC();
            int idResultado = 0;
            int success = 0;
            try
            {
                idResultado = objResultadoDALC.InsertarResultadoAdulto(objResultado);

                if (idResultado > 0)
                {
                    objResultado.Resultado_id = idResultado;

                    success = objResultadoDALC.ResultadoActualizarAdulto(objResultado);
                }
                else
                    throw new Exception("El código devuelto al registrar el resultado Adulto fue de cero.");
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return success;
        }



        public int ResultadoActualizar(ResultadoBE objResultado)
        {
            ResultadoDALC objDalc = new ResultadoDALC();
            return (objDalc.ResultadoActualizar(objResultado));
        }

        public List<CuadroResultadoBE> CuadroResultadoListar(int idResultado)
        {
            ResultadoDALC objDalc = new ResultadoDALC();

            return objDalc.CuadroResultadoListar();
        }

        public List<ResultadoParaleloBE> ResultadosParticipantesListar(string FechaInicio, string FechaFin, string Empresa)
        {
            ResultadoDALC objDalc = new ResultadoDALC();

            return objDalc.ResultadosParticipantesListar(FechaInicio, FechaFin, Empresa);
        }

        public List<ResultadoParaleloBE> ResultadosParticipantesImportar(string FechaInicio, string FechaFin, string Empresa)
        {
            ResultadoDALC objDalc = new ResultadoDALC();

            return objDalc.ResultadosParticipantesImportar(FechaInicio, FechaFin, Empresa);
        }

        public ResultadoFinalBE ObtenerResultadoParticipante(string codigoEvaluacion, ref int[] orientaciones)
        {
            ResultadoDALC objResultadoDALC = new ResultadoDALC();
            ParticipanteDALC objParticipanteDALC = new ParticipanteDALC();
            ResultadoFinalBE objResultadoBE = new ResultadoFinalBE();
            List<TalentoComplexBE> lstTalentos = null;

            try
            {
                objResultadoBE.NombreParticipante = objParticipanteDALC.ObtenerNombreParticipantexCodigo(codigoEvaluacion);
                objResultadoBE.FechaTest = objResultadoDALC.ObtenerFechaTestxCodigo(codigoEvaluacion);
                lstTalentos = objResultadoDALC.ObtenerResultadoParticipante(codigoEvaluacion);
            }
            catch (Exception)
            {
                throw;
            }

            objResultadoBE.lstTalentosMasDesarrollados = new List<TalentoComplexBE>();
            objResultadoBE.lstTalentosMenosDesarrollados = new List<TalentoComplexBE>();
            objResultadoBE.lstTalentosIntermedioDesarrollados = new List<TalentoComplexBE>();
            objResultadoBE.lstTEMasDesarrollados = new List<TalentoComplexBE>();
            objResultadoBE.lstTEMenosDesarrollados = new List<TalentoComplexBE>();
            objResultadoBE.lstVirtudes = new List<TalentoComplexBE>();
            objResultadoBE.lstTalentosTotales = new List<TalentoComplexBE>();

            try
            {
                for (int i = 0; i < lstTalentos.Count; i++)
                {
                    switch (lstTalentos[i].Buzon_Id)
                    {
                        // Talentos más desarrollados (Buzon 1)
                        case 1:
                            if (lstTalentos[i].Seleccionado)
                            {
                                objResultadoBE.lstTalentosMasDesarrollados.Add(lstTalentos[i]);
                                orientaciones[lstTalentos[i].idTendencia - 1]++;
                            }
                            objResultadoBE.lstTalentosTotales.Add(lstTalentos[i]);
                            break;
                        // Talentos intermedios desarrollados (Buzon 2)
                        case 2:
                            if (lstTalentos[i].Seleccionado)
                                objResultadoBE.lstTalentosIntermedioDesarrollados.Add(lstTalentos[i]);
                            objResultadoBE.lstTalentosTotales.Add(lstTalentos[i]);
                            break;
                        // Talentos menos desarrollados (Buzon 3)
                        case 3:
                            if (lstTalentos[i].Seleccionado)
                                objResultadoBE.lstTalentosMenosDesarrollados.Add(lstTalentos[i]);
                            objResultadoBE.lstTalentosTotales.Add(lstTalentos[i]);
                            break;
                        // Talentos Especificos más desarrollados (Buzon 4)
                        case 4:
                            objResultadoBE.lstTEMasDesarrollados.Add(lstTalentos[i]);
                            objResultadoBE.lstTalentosTotales.Add(lstTalentos[i]);
                            break;
                        // Talentos Especificos menos desarrollados (Buzon 6)
                        case 6:
                            objResultadoBE.lstTEMenosDesarrollados.Add(lstTalentos[i]);
                            objResultadoBE.lstTalentosTotales.Add(lstTalentos[i]);
                            break;
                        // Virtudes (Buzon 7)
                        case 7:
                            if (lstTalentos[i].Seleccionado)
                                objResultadoBE.lstVirtudes.Add(lstTalentos[i]);
                            break;
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }

            return objResultadoBE;
        }


        public ResultadoFinalBE ObtenerResultadoParticipanteAdulto(string codigoEvaluacion, ref int[] orientaciones)
        {
            ResultadoDALC objResultadoDALC = new ResultadoDALC();
            ParticipanteDALC objParticipanteDALC = new ParticipanteDALC();
            ResultadoFinalBE objResultadoBE = new ResultadoFinalBE();
            List<TalentoComplexBE> lstTalentos = null;

            try
            {
                objResultadoBE.NombreParticipante = objParticipanteDALC.ObtenerNombreParticipanteAdultoxCodigo(codigoEvaluacion);
                objResultadoBE.FechaTest = objResultadoDALC.ObtenerFechaTestAdultoxCodigo(codigoEvaluacion);
                lstTalentos = objResultadoDALC.ObtenerResultadoParticipanteAdulto(codigoEvaluacion);
            }
            catch (Exception)
            {
                throw;
            }

            objResultadoBE.lstTalentosMasDesarrollados = new List<TalentoComplexBE>();
            objResultadoBE.lstTalentosMenosDesarrollados = new List<TalentoComplexBE>();
            objResultadoBE.lstTalentosIntermedioDesarrollados = new List<TalentoComplexBE>();
            objResultadoBE.lstTEMasDesarrollados = new List<TalentoComplexBE>();
            objResultadoBE.lstTEMenosDesarrollados = new List<TalentoComplexBE>();
            objResultadoBE.lstVirtudes = new List<TalentoComplexBE>();
            objResultadoBE.lstTalentosTotales = new List<TalentoComplexBE>();

            try
            {
                for (int i = 0; i < lstTalentos.Count; i++)
                {
                    switch (lstTalentos[i].Buzon_Id)
                    {
                        // Talentos más desarrollados (Buzon 1)
                        case 1:
                            if (lstTalentos[i].Seleccionado)
                            {
                                objResultadoBE.lstTalentosMasDesarrollados.Add(lstTalentos[i]);
                                orientaciones[lstTalentos[i].idTendencia - 1]++;
                            }
                            objResultadoBE.lstTalentosTotales.Add(lstTalentos[i]);
                            break;
                        // Talentos intermedios desarrollados (Buzon 2)
                        case 2:
                            if (lstTalentos[i].Seleccionado)
                                objResultadoBE.lstTalentosIntermedioDesarrollados.Add(lstTalentos[i]);
                            objResultadoBE.lstTalentosTotales.Add(lstTalentos[i]);
                            break;
                        // Talentos menos desarrollados (Buzon 3)
                        case 3:
                            if (lstTalentos[i].Seleccionado)
                                objResultadoBE.lstTalentosMenosDesarrollados.Add(lstTalentos[i]);
                            objResultadoBE.lstTalentosTotales.Add(lstTalentos[i]);
                            break;
                        // Talentos Especificos más desarrollados (Buzon 4)
                        case 4:
                            objResultadoBE.lstTEMasDesarrollados.Add(lstTalentos[i]);
                            objResultadoBE.lstTalentosTotales.Add(lstTalentos[i]);
                            break;
                        // Talentos Especificos menos desarrollados (Buzon 6)
                        case 6:
                            objResultadoBE.lstTEMenosDesarrollados.Add(lstTalentos[i]);
                            objResultadoBE.lstTalentosTotales.Add(lstTalentos[i]);
                            break;
                        // Virtudes (Buzon 7)
                        case 7:
                            if (lstTalentos[i].Seleccionado)
                                objResultadoBE.lstVirtudes.Add(lstTalentos[i]);
                            break;
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }

            return objResultadoBE;
        }

        public void CargarResultadosMasivos(List<ResultadoFinalBE> lstResultados)
        {
            string codigosMasDesarrollados = "";
            string codigosMenosDesarrollados = "";
            string codigosTEMasDesarrollados = "";
            string codigosTEMenosDesarrollados = "";
            string codigosVirtudes = "";
            string codigosTalentos = "";
            string masDesarrollados = "";
            string menosDesarrollados = "";
            string TEMasDesarrollados = "";
            string TEMenosDesarrollados = "";
            string virtudes = "";
            string seleccionados = "";
            string buzones = "";
            int codigoParticipante = 0;
            int codigoResultado = 0;
            TalentoDALC objtalentoDALC = new TalentoDALC();
            ResultadoDALC objResultadoDALC = new ResultadoDALC();

            foreach (var item in lstResultados)
            {
                codigosMasDesarrollados = "";
                codigosMenosDesarrollados = "";
                codigosTEMasDesarrollados = "";
                codigosTEMenosDesarrollados = "";
                codigosVirtudes = "";
                codigosTalentos = "";
                masDesarrollados = "";
                menosDesarrollados = "";
                TEMasDesarrollados = "";
                TEMenosDesarrollados = "";
                virtudes = "";
                seleccionados = "";
                buzones = "";

                for (int i = 0; i < item.lstTalentosMasDesarrollados.Count; i++)
                {
                    masDesarrollados = masDesarrollados + "'" + item.lstTalentosMasDesarrollados[i].nombre + "', ";
                    seleccionados = seleccionados + "1, ";
                    buzones = buzones + "1, ";
                }

                for (int i = 0; i < item.lstTalentosMenosDesarrollados.Count; i++)
                {
                    menosDesarrollados = menosDesarrollados + "'" + item.lstTalentosMenosDesarrollados[i].nombre + "', ";
                    seleccionados = seleccionados + "1, ";
                    buzones = buzones + "3, ";
                }

                for (int i = 0; i < item.lstTEMasDesarrollados.Count; i++)
                {
                    TEMasDesarrollados = TEMasDesarrollados + "'" + item.lstTEMasDesarrollados[i].nombre + "', ";
                    seleccionados = seleccionados + "1, ";
                    buzones = buzones + "4, ";
                }

                for (int i = 0; i < item.lstTEMenosDesarrollados.Count; i++)
                {
                    TEMenosDesarrollados = TEMenosDesarrollados + "'" + item.lstTEMenosDesarrollados[i].nombre + "', ";
                    seleccionados = seleccionados + "1, ";
                    buzones = buzones + "6, ";
                }

                for (int i = 0; i < item.lstVirtudes.Count; i++)
                {
                    virtudes = virtudes + "'" + item.lstVirtudes[i].nombre + "', ";
                    seleccionados = seleccionados + "1, ";
                    buzones = buzones + "7, ";
                }

                //clausula = masDesarrollados + menosDesarrollados + TEMasDesarrollados + TEMenosDesarrollados + virtudes;

                try
                {
                    codigosMasDesarrollados = objtalentoDALC.ObtenerCodigos(masDesarrollados);
                    codigosMenosDesarrollados = objtalentoDALC.ObtenerCodigos(menosDesarrollados);
                    codigosTEMasDesarrollados = objtalentoDALC.ObtenerCodigos(TEMasDesarrollados);
                    codigosTEMenosDesarrollados = objtalentoDALC.ObtenerCodigos(TEMenosDesarrollados);
                    codigosVirtudes = objtalentoDALC.ObtenerCodigos(virtudes);

                    codigosTalentos = codigosMasDesarrollados + ", " + codigosMenosDesarrollados + ", " + codigosTEMasDesarrollados + ", " +
                        codigosTEMenosDesarrollados + ", " + codigosVirtudes;
                }
                catch (Exception ex)
                {
                    throw ex;
                }


                ParticipanteBE objParticipanteBE = new ParticipanteBE();
                ParticipanteDALC objParticipanteDALC = new ParticipanteDALC();

                objParticipanteBE.DNI = item.DNI;
                objParticipanteBE.Nombres = item.NombreParticipante;
                objParticipanteBE.ApellidoMaterno = item.ApellidoMaterno;
                objParticipanteBE.ApellidoPaterno = item.ApellidoPaterno;
                objParticipanteBE.Cargo = item.CargoEmpresa;
                objParticipanteBE.CorreoElectronico = item.CorreoElectronico;
                objParticipanteBE.NivelInstruccion = item.NivelInstruccion;
                objParticipanteBE.FechaNacimiento = item.FechaNacimiento;
                objParticipanteBE.Sexo = item.Sexo;
                objParticipanteBE.Institucion = item.Institucion;
                try
                {
                    codigoParticipante = objParticipanteDALC.InsertarParticipante(objParticipanteBE);
                }
                catch (Exception)
                {
                    throw;
                }


                if (codigoParticipante > 0)
                {
                    ResultadoBE objResultadoBE = new ResultadoBE();

                    objResultadoBE.NombreParticipante = item.NombreParticipante;
                    objResultadoBE.DNI = item.DNI;
                    objResultadoBE.CorreoElectronico = item.CorreoElectronico;
                    objResultadoBE.Participante_id = codigoParticipante;
                    objResultadoBE.Fecha = DateTime.Now;
                    objResultadoBE.TalentoId = codigosTalentos;
                    objResultadoBE.Seleccionado = seleccionados;
                    objResultadoBE.BuzonId = buzones;
                    objResultadoBE.EsMasivo = true;
                    objResultadoBE.CodEvaluacion = item.CodigoEvaluacion;

                    try
                    {
                        codigoResultado = objResultadoDALC.InsertarResultado(objResultadoBE);
                    }
                    catch (Exception ex)
                    {
                        throw ex;
                    }


                    if (codigoResultado > 0)
                    {
                        objResultadoBE.Resultado_id = codigoResultado;

                        try
                        {
                            objResultadoDALC.ResultadoActualizar(objResultadoBE);
                        }
                        catch (Exception ex)
                        {
                            throw ex;
                        }

                    }
                }
            }
        }

        //public List<int> ObtenerCarreras(List<int> fortalezas)    {
        //    BaseHechos objBH = new BaseHechos();
        //    objBH.SetHechosFortalezas(fortalezas);
        //    int[] HechosFortalezas = objBH.HechosFortalezas;
        //    MotorInferencia objMI = new MotorInferencia();
        //    List<int> orientaciones = objMI.GetOrientaciones(HechosFortalezas);
        //    objBH.SetHechosOrientaciones(orientaciones);
        //    int[] HechosOrientaciones = objBH.HechosOrientaciones;
        //    List<int> carrerasList = objMI.GetCarreras(HechosOrientaciones);


        //    //BaseHechos bhList = new BaseHechos();
        //    //bhList.HechosFortalezas=fortalezas;

        //    //SE.Tratamiento.MI.MotorInferencia mi = new SE.Tratamiento.MI.MotorInferencia();

        //    //List<int> Orientaciones = new List<int>();
        //    //Orientaciones= mi.GetOrientaciones(fortalezas);

        //    //List<int> Carreras = new List<int>();
        //    //Carreras=mi.GetCarreras(Orientaciones);

        //    //return Carreras;
        //    return carrerasList;
        //}

        public void InsertaResultadoTemp(int idParticipante, string lstIdTalento, int idBuzon)
        {
            ResultadoDALC objResultadoDALC = new ResultadoDALC();

            try
            {
                objResultadoDALC.InsertaResultadoTemp(idParticipante, lstIdTalento, idBuzon);

            }
            catch (Exception ex)
            {
                throw ex;
            }

        }


        public List<ResultadoComFacultadBE> getCompatibilidadxFacultad(string coduser, double ptsExtroIntro, double ptsIntuicion_Sensacion, double ptsRacional_Emotivo, double ptsOrganizadoCasual)
        {
            ResultadoDALC objDalc = new ResultadoDALC();

            return objDalc.ObtenerResultadoCompatibilidadXFacultad(coduser, ptsExtroIntro, ptsIntuicion_Sensacion, ptsRacional_Emotivo, ptsOrganizadoCasual);
        }

        public List<ResultadoComCampoLaboralBE> getCompatibilidadxCampoLaboral(string coduser, double ptsExtroIntro, double ptsIntuicion_Sensacion, double ptsRacional_Emotivo, double ptsOrganizadoCasual)
        {
            ResultadoDALC objDalc = new ResultadoDALC();

            return objDalc.ObtenerResultadoCompatibilidadXCampoLaboral(coduser, ptsExtroIntro, ptsIntuicion_Sensacion, ptsRacional_Emotivo, ptsOrganizadoCasual);
        }


        //General Report
        public List<GeneralReportCategoryBE> getResultGeneralCategory(string idEncuesta)
        {
            ResultadoDALC objDalc = new ResultadoDALC();
            return objDalc.getResultGrlReportTalentCategory(Convert.ToInt32(idEncuesta));
        }

        public GeneralResultBE getGeneralResult(string idEncuesta)
        {

            ResultadoDALC objDalc = new ResultadoDALC();
            List<GeneralTalentBE> lstTalents = null;
            GeneralResultBE objResultadoBE = new GeneralResultBE();

            int PollId = Convert.ToInt32(idEncuesta);

            objResultadoBE.institution = objDalc.getCompanyName(PollId);
            objResultadoBE.testDate = objDalc.getDatePoll(PollId);
            lstTalents = objDalc.getlstGeneralTalents(PollId);

            objResultadoBE.lstTalentsMostDe = new List<GeneralTalentBE>();
            objResultadoBE.lstTalentsSpecifics = new List<GeneralTalentBE>();

            for (int i = 0; i < lstTalents.Count; i++)
            {
                switch (lstTalents[i].idTipoTalento)
                {
                    // Talentos más desarrollados (Buzon 1)
                    case 1:
                        objResultadoBE.lstTalentsMostDe.Add(lstTalents[i]);
                        break;
                    // Talentos intermedios desarrollados (Buzon 2)
                    case 2:
                        objResultadoBE.lstTalentsSpecifics.Add(lstTalents[i]);
                        break;
                    // Talentos menos desarrollados (Buzon 3)
                    // Talentos Especificos más desarrollados (Buzon 4)
                    // Talentos Especificos menos desarrollados (Buzon 6)
                    // Virtudes (Buzon 7)

                }
            }





            return objResultadoBE;
        }


        public List<GeneralInteresesBE> getResultIntereses(string idEncuesta)
        {
            ResultadoDALC objDalc = new ResultadoDALC();
            return objDalc.getResultGeneralIntereses(Convert.ToInt32(idEncuesta));
        }

        public List<GeneralTemperamentosBE> getResultTemperamentos(string idEncuesta)
        {
            ResultadoDALC objDalc = new ResultadoDALC();
            return objDalc.getResultGeneralTemperamentos(Convert.ToInt32(idEncuesta));
        }

        //Add Data to General Report

        public void addInterestsResult(string strUserId, int intSchedullingId, int intADM, int intARG, int intART, int intCOM,
                                         int intCON, int intCUL, int intDEP, int intDIS, int intFIN, int intINF, int intJUR,
                                         int intMAR, int intMEC, int intMIN, int intPED, int intSAL, int intSOC, int intTRA,
                                         int intTUR)
        {
            ResultadoDALC objDalc = new ResultadoDALC();
            objDalc.insertInterestsResult(strUserId,
                                            intSchedullingId,
                                            intADM,
                                            intARG,
                                            intART,
                                            intCOM,
                                            intCON,
                                            intCUL,
                                            intDEP,
                                            intDIS,
                                            intFIN,
                                            intINF,
                                            intJUR,
                                            intMAR,
                                            intMEC,
                                            intMIN,
                                            intPED,
                                            intSAL,
                                            intSOC,
                                            intTRA,
                                            intTUR);
        }

        public void addTemperamentsResult(string strUserId, int intSchedullingTemperamentId, int intSchedullingInterestsId,
                                             double dblAmbDinamic_AmbTranquilo, double dblSociable_Intimo, double dblEntusiasta_Calmado,
                                             double dblComunicativo_Reservado, double dblInstintivo_Esceptico, double dblOriginal_Tradicional,
                                             double dblCreativo_Realista, double dblObjetivo_Compasivo, double dblDistante_Susceptible,
                                             double dblDirecto_Empatico, double dblPlanificado_Espontaneo, double dblMetodico_Eventual,
                                             double dblEstructurado_Flexible)
        {
            ResultadoDALC objDalc = new ResultadoDALC();
            objDalc.insertTemperamentsResult(strUserId, intSchedullingTemperamentId, intSchedullingInterestsId,
                                             dblAmbDinamic_AmbTranquilo, dblSociable_Intimo, dblEntusiasta_Calmado,
                                             dblComunicativo_Reservado, dblInstintivo_Esceptico, dblOriginal_Tradicional,
                                             dblCreativo_Realista, dblObjetivo_Compasivo, dblDistante_Susceptible,
                                             dblDirecto_Empatico, dblPlanificado_Espontaneo, dblMetodico_Eventual,
                                             dblEstructurado_Flexible);
        }

        
            
        



    }
}
