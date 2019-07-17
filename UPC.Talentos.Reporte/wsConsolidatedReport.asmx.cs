using System;
using System.Collections.Generic;
using System.Linq;
using System.Configuration;
using System.Web.Services;
using UPC.Talentos.BL.BE;
using UPC.Talentos.DL.DALC;
using UPC.Talentos.BL.BC;
using UPC.Talentos.Reporte.BL.BC;
using UPC.Talentos.Reporte.BL.BE;
using OfficeOpenXml;
using System.IO;
using OfficeOpenXml.Drawing.Chart;
using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Wordprocessing;
using DocumentFormat.OpenXml.Drawing.Charts;
using System.Net;
using System.Collections.Specialized;
using DocumentFormat.OpenXml;
using ConvertApi;
using OpenXmlPowerTools;
using System.Xml.Linq;

using wp = DocumentFormat.OpenXml.Drawing.Wordprocessing;
using a = DocumentFormat.OpenXml.Drawing;
using pic = DocumentFormat.OpenXml.Drawing.Pictures;
using System.Drawing;

namespace UPC.Talentos.Reporte
{
    /// <summary>
    /// Descripción breve de wsConsolidatedReport
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // Para permitir que se llame a este servicio Web desde un script, usando ASP.NET AJAX, quite la marca de comentario de la línea siguiente. 
    // [System.Web.Script.Services.ScriptService]
    public class wsConsolidatedReport : System.Web.Services.WebService
    {

        double puntaje_extroversion_vs_introversio_bar = 0;
        double puntaje_intuicion_vs_sensacion_bar = 0;
        double puntaje_racional_vs_emotivo_bar = 0;
        double puntaje_organizado_vs_casual_bar = 0;

        List<double> ScaleNumbersEI = new List<double>();
        List<double> ScaleNumbersIS = new List<double>();
        List<double> ScaleNumbersRE = new List<double>();
        List<double> ScaleNumbersOC = new List<double>();

        //General Report
        string strTemperamento_id = "";
        string strIntereses_id = "";
        string strCodUserTemperament = "";

        //Intereses-GeneralReport
        int intADM = 0,
            intARG = 0,
            intART = 0,
            intCOM = 0,
            intCON = 0,
            intCUL = 0,
            intDEP = 0,
            intDIS = 0,
            intFIN = 0,
            intINF = 0,
            intJUR = 0,
            intMAR = 0,
            intMEC = 0,
            intMIN = 0,
            intPED = 0,
            intSAL = 0,
            intSOC = 0,
            intTRA = 0,
            intTUR = 0;

        //Temperaments-GeneralReport
        double dblAmbDinamic_AmbTranquilo = 0.0;
        double dblSociable_Intimo = 0.0;
        double dblEntusiasta_Calmado = 0.0;
        double dblComunicativo_Reservado = 0.0;
        double dblInstintivo_Esceptico = 0.0;
        double dblOriginal_Tradicional = 0.0;
        double dblCreativo_Realista = 0.0;
        double dblObjetivo_Compasivo = 0.0;
        double dblDistante_Susceptible = 0.0;
        double dblDirecto_Empatico = 0.0;
        double dblPlanificado_Espontaneo = 0.0;
        double dblMetodico_Eventual = 0.0;
        double dblEstructurado_Flexible = 0.0;

        [WebMethod]
        public string GenerarReporte(string codUser, ReporteTemperamentoBE objTemperamento, ReporteBE objInteresBE)
        {

            List<PuntajeTemperamentoBE> lstPuntajeTemperamento = new List<PuntajeTemperamentoBE>();
            PuntajeTemperamentoBE objPuntajeTemperamento = null;

            //General Report - start
            strTemperamento_id = objTemperamento.report.temperamentos_id;
            strIntereses_id = objTemperamento.report.intereses_id;

            if (string.IsNullOrEmpty(strIntereses_id))
                strIntereses_id = objInteresBE.report.intereses_id;

            strCodUserTemperament = objInteresBE.report.encuesta.id;
            //General Report - end


            int contlstSections = objTemperamento.report.result.sections.Count();
            string section = "";
            string subsection = "";
            string respuesta = "";

            double puntaje_subsection = 0;
            //
            double puntaje_extroversion = 0;
            double puntaje_introversion = 0;
            double puntaje_intuicion = 0;
            double puntaje_sensacion = 0;
            double puntaje_racional = 0;
            double puntaje_emotivo = 0;
            double puntaje_organizado = 0;
            double puntaje_casual = 0;
            int contQuestions = 0;

            for (int i = 0; i < contlstSections; i++)
            {
                section = removecharacters(objTemperamento.report.result.sections[i].title);
                switch (section)
                {
                    case "EXTROVERTIDO":
                    case "EXTROVERSION":
                        for (int extro = 0; extro < objTemperamento.report.result.sections[i].sections.Count(); extro++)
                        {
                            objPuntajeTemperamento = new PuntajeTemperamentoBE();
                            subsection = removecharacters(objTemperamento.report.result.sections[i].sections[extro].title).Trim();
                            objPuntajeTemperamento.name_section = section;
                            puntaje_subsection = 0;

                            switch (subsection)
                            {
                                case "SOCIABLE":
                                    for (int soc = 0; soc < objTemperamento.report.result.sections[i].sections[extro].questions.Count(); soc++)
                                    {
                                        respuesta = (objTemperamento.report.result.sections[i].sections[extro].questions[soc].answer == null) ? "0" : removecharacters(objTemperamento.report.result.sections[i].sections[extro].questions[soc].answer).Trim();
                                        puntaje_subsection += calcularPuntaje(respuesta);
                                    }
                                    contQuestions = objTemperamento.report.result.sections[i].sections[extro].questions.Count();
                                    objPuntajeTemperamento.name_subsection = subsection;
                                    objPuntajeTemperamento.puntaje_subsection = (puntaje_subsection / contQuestions); //5
                                    break;
                                case "COMUNICATIVO":
                                    for (int com = 0; com < objTemperamento.report.result.sections[i].sections[extro].questions.Count(); com++)
                                    {
                                        respuesta = (objTemperamento.report.result.sections[i].sections[extro].questions[com].answer == null) ? "0" : removecharacters(objTemperamento.report.result.sections[i].sections[extro].questions[com].answer).Trim();
                                        puntaje_subsection += calcularPuntaje(respuesta);
                                    }
                                    contQuestions = objTemperamento.report.result.sections[i].sections[extro].questions.Count();
                                    objPuntajeTemperamento.name_subsection = subsection;
                                    objPuntajeTemperamento.puntaje_subsection = (puntaje_subsection / contQuestions); //4
                                    break;
                                case "ENTUSIASTA":
                                    for (int ent = 0; ent < objTemperamento.report.result.sections[i].sections[extro].questions.Count(); ent++)
                                    {
                                        respuesta = (objTemperamento.report.result.sections[i].sections[extro].questions[ent].answer == null) ? "0" : removecharacters(objTemperamento.report.result.sections[i].sections[extro].questions[ent].answer).Trim();
                                        puntaje_subsection += calcularPuntaje(respuesta);
                                    }
                                    contQuestions = objTemperamento.report.result.sections[i].sections[extro].questions.Count();
                                    objPuntajeTemperamento.name_subsection = subsection;
                                    objPuntajeTemperamento.puntaje_subsection = (puntaje_subsection / contQuestions); //4
                                    break;
                                case "AMBIENTES DINAMICOS":
                                    for (int amd = 0; amd < objTemperamento.report.result.sections[i].sections[extro].questions.Count(); amd++)
                                    {
                                        respuesta = (objTemperamento.report.result.sections[i].sections[extro].questions[amd].answer == null) ? "0" : removecharacters(objTemperamento.report.result.sections[i].sections[extro].questions[amd].answer).Trim();
                                        puntaje_subsection += calcularPuntaje(respuesta);
                                    }
                                    contQuestions = objTemperamento.report.result.sections[i].sections[extro].questions.Count();
                                    objPuntajeTemperamento.name_subsection = subsection;
                                    objPuntajeTemperamento.puntaje_subsection = (puntaje_subsection / contQuestions); //4
                                    break;

                            }
                            puntaje_extroversion += objPuntajeTemperamento.puntaje_subsection;
                            if (objPuntajeTemperamento.name_subsection != null)
                                lstPuntajeTemperamento.Add(objPuntajeTemperamento);
                        }
                        break;

                    case "INTROVERTIDO":
                    case "INTROVERSION":
                        for (int intro = 0; intro < objTemperamento.report.result.sections[i].sections.Count(); intro++)
                        {
                            objPuntajeTemperamento = new PuntajeTemperamentoBE();
                            subsection = removecharacters(objTemperamento.report.result.sections[i].sections[intro].title).Trim();
                            objPuntajeTemperamento.name_section = section;
                            puntaje_subsection = 0;

                            switch (subsection)
                            {
                                case "INTIMO":
                                    for (int inti = 0; inti < objTemperamento.report.result.sections[i].sections[intro].questions.Count(); inti++)
                                    {
                                        respuesta = (objTemperamento.report.result.sections[i].sections[intro].questions[inti].answer == null) ? "0" : removecharacters(objTemperamento.report.result.sections[i].sections[intro].questions[inti].answer).Trim();
                                        puntaje_subsection += calcularPuntajeInvertido(respuesta);
                                    }
                                    contQuestions = objTemperamento.report.result.sections[i].sections[intro].questions.Count();
                                    objPuntajeTemperamento.name_subsection = subsection;
                                    objPuntajeTemperamento.puntaje_subsection = (puntaje_subsection / contQuestions); //
                                    break;
                                case "RESERVADO":
                                    for (int res = 0; res < objTemperamento.report.result.sections[i].sections[intro].questions.Count(); res++)
                                    {
                                        respuesta = (objTemperamento.report.result.sections[i].sections[intro].questions[res].answer == null) ? "0" : removecharacters(objTemperamento.report.result.sections[i].sections[intro].questions[res].answer).Trim();
                                        puntaje_subsection += calcularPuntajeInvertido(respuesta);
                                    }
                                    contQuestions = objTemperamento.report.result.sections[i].sections[intro].questions.Count();
                                    objPuntajeTemperamento.name_subsection = subsection;
                                    objPuntajeTemperamento.puntaje_subsection = (puntaje_subsection / contQuestions); //4
                                    break;
                                case "CALMADO":
                                    for (int cal = 0; cal < objTemperamento.report.result.sections[i].sections[intro].questions.Count(); cal++)
                                    {
                                        respuesta = (objTemperamento.report.result.sections[i].sections[intro].questions[cal].answer == null) ? "0" : removecharacters(objTemperamento.report.result.sections[i].sections[intro].questions[cal].answer).Trim();
                                        puntaje_subsection += calcularPuntajeInvertido(respuesta);
                                    }
                                    contQuestions = objTemperamento.report.result.sections[i].sections[intro].questions.Count();
                                    objPuntajeTemperamento.name_subsection = subsection;
                                    objPuntajeTemperamento.puntaje_subsection = (puntaje_subsection / contQuestions); //4
                                    break;
                                case "AMBIENTES TRANQUILOS":
                                    for (int amt = 0; amt < objTemperamento.report.result.sections[i].sections[intro].questions.Count(); amt++)
                                    {
                                        respuesta = (objTemperamento.report.result.sections[i].sections[intro].questions[amt].answer == null) ? "0" : removecharacters(objTemperamento.report.result.sections[i].sections[intro].questions[amt].answer).Trim();
                                        puntaje_subsection += calcularPuntajeInvertido(respuesta);
                                    }
                                    contQuestions = objTemperamento.report.result.sections[i].sections[intro].questions.Count();
                                    objPuntajeTemperamento.name_subsection = subsection;
                                    objPuntajeTemperamento.puntaje_subsection = (puntaje_subsection / contQuestions); //4
                                    break;

                            }
                            puntaje_introversion += objPuntajeTemperamento.puntaje_subsection;
                            if (objPuntajeTemperamento.name_subsection != null)
                                lstPuntajeTemperamento.Add(objPuntajeTemperamento);
                        }
                        break;
                    case "INTUITIVO":
                    case "INTUICION":
                        for (int intu = 0; intu < objTemperamento.report.result.sections[i].sections.Count(); intu++)
                        {
                            objPuntajeTemperamento = new PuntajeTemperamentoBE();
                            subsection = removecharacters(objTemperamento.report.result.sections[i].sections[intu].title).Trim();
                            objPuntajeTemperamento.name_section = section;
                            puntaje_subsection = 0;

                            switch (subsection)
                            {
                                case "INSTINTIVO":
                                    for (int inst = 0; inst < objTemperamento.report.result.sections[i].sections[intu].questions.Count(); inst++)
                                    {
                                        respuesta = (objTemperamento.report.result.sections[i].sections[intu].questions[inst].answer == null) ? "0" : removecharacters(objTemperamento.report.result.sections[i].sections[intu].questions[inst].answer).Trim();
                                        puntaje_subsection += calcularPuntaje(respuesta);
                                    }
                                    contQuestions = objTemperamento.report.result.sections[i].sections[intu].questions.Count();
                                    objPuntajeTemperamento.name_subsection = subsection;
                                    objPuntajeTemperamento.puntaje_subsection = (puntaje_subsection / contQuestions); //4
                                    break;
                                case "CREATIVO":
                                    for (int cre = 0; cre < objTemperamento.report.result.sections[i].sections[intu].questions.Count(); cre++)
                                    {
                                        respuesta = (objTemperamento.report.result.sections[i].sections[intu].questions[cre].answer == null) ? "0" : removecharacters(objTemperamento.report.result.sections[i].sections[intu].questions[cre].answer).Trim();
                                        puntaje_subsection += calcularPuntaje(respuesta);
                                    }
                                    contQuestions = objTemperamento.report.result.sections[i].sections[intu].questions.Count();
                                    objPuntajeTemperamento.name_subsection = subsection;
                                    objPuntajeTemperamento.puntaje_subsection = (puntaje_subsection / contQuestions); //4
                                    break;
                                    /*
                                case "CONCEPTUAL":
                                    for (int con = 0; con < objTemperamento.report.result.sections[i].sections[intu].questions.Count(); con++)
                                    {
                                        respuesta = (objTemperamento.report.result.sections[i].sections[intu].questions[con].answer == null) ? "0" : removecharacters(objTemperamento.report.result.sections[i].sections[intu].questions[con].answer).Trim();
                                        puntaje_subsection += calcularPuntaje(respuesta);
                                    }
                                    contQuestions = objTemperamento.report.result.sections[i].sections[intu].questions.Count();
                                    objPuntajeTemperamento.name_subsection = subsection;
                                    objPuntajeTemperamento.puntaje_subsection = (puntaje_subsection / contQuestions); //3
                                    break;
                                    */
                                case "ORIGINAL":
                                    for (int ori = 0; ori < objTemperamento.report.result.sections[i].sections[intu].questions.Count(); ori++)
                                    {
                                        respuesta = (objTemperamento.report.result.sections[i].sections[intu].questions[ori].answer == null) ? "0" : removecharacters(objTemperamento.report.result.sections[i].sections[intu].questions[ori].answer).Trim();
                                        puntaje_subsection += calcularPuntaje(respuesta);
                                    }
                                    contQuestions = objTemperamento.report.result.sections[i].sections[intu].questions.Count();
                                    objPuntajeTemperamento.name_subsection = subsection;
                                    objPuntajeTemperamento.puntaje_subsection = (puntaje_subsection / contQuestions); //4
                                    break;

                            }
                            puntaje_intuicion += objPuntajeTemperamento.puntaje_subsection;
                            if (objPuntajeTemperamento.name_subsection != null)
                                lstPuntajeTemperamento.Add(objPuntajeTemperamento);
                        }
                        break;

                    case "SENSORIAL":
                    case "SENSACION":
                        for (int sens = 0; sens < objTemperamento.report.result.sections[i].sections.Count(); sens++)
                        {
                            objPuntajeTemperamento = new PuntajeTemperamentoBE();
                            subsection = removecharacters(objTemperamento.report.result.sections[i].sections[sens].title).Trim();
                            objPuntajeTemperamento.name_section = section;
                            puntaje_subsection = 0;

                            switch (subsection)
                            {
                                case "ESCEPTICO":
                                    for (int esc = 0; esc < objTemperamento.report.result.sections[i].sections[sens].questions.Count(); esc++)
                                    {
                                        respuesta = (objTemperamento.report.result.sections[i].sections[sens].questions[esc].answer == null) ? "0" : removecharacters(objTemperamento.report.result.sections[i].sections[sens].questions[esc].answer).Trim();
                                        puntaje_subsection += calcularPuntajeInvertido(respuesta);
                                    }
                                    contQuestions = objTemperamento.report.result.sections[i].sections[sens].questions.Count();
                                    objPuntajeTemperamento.name_subsection = subsection;
                                    objPuntajeTemperamento.puntaje_subsection = (puntaje_subsection / contQuestions); //4
                                    break;
                                case "REALISTA":
                                    for (int rea = 0; rea < objTemperamento.report.result.sections[i].sections[sens].questions.Count(); rea++)
                                    {
                                        respuesta = (objTemperamento.report.result.sections[i].sections[sens].questions[rea].answer == null) ? "0" : removecharacters(objTemperamento.report.result.sections[i].sections[sens].questions[rea].answer).Trim();
                                        puntaje_subsection += calcularPuntajeInvertido(respuesta);
                                    }
                                    contQuestions = objTemperamento.report.result.sections[i].sections[sens].questions.Count();
                                    objPuntajeTemperamento.name_subsection = subsection;
                                    objPuntajeTemperamento.puntaje_subsection = (puntaje_subsection / contQuestions); //4
                                    break;
                                    /*
                                case "APLICADOR":
                                    for (int apl = 0; apl < objTemperamento.report.result.sections[i].sections[sens].questions.Count(); apl++)
                                    {
                                        respuesta = (objTemperamento.report.result.sections[i].sections[sens].questions[apl].answer == null) ? "0" : removecharacters(objTemperamento.report.result.sections[i].sections[sens].questions[apl].answer).Trim();
                                        puntaje_subsection += calcularPuntajeInvertido(respuesta);
                                    }
                                    contQuestions = objTemperamento.report.result.sections[i].sections[sens].questions.Count();
                                    objPuntajeTemperamento.name_subsection = subsection;
                                    objPuntajeTemperamento.puntaje_subsection = (puntaje_subsection / contQuestions); //3
                                    break;
                                    */
                                case "TRADICIONAL":
                                    for (int esc = 0; esc < objTemperamento.report.result.sections[i].sections[sens].questions.Count(); esc++)
                                    {
                                        respuesta = (objTemperamento.report.result.sections[i].sections[sens].questions[esc].answer == null) ? "0" : removecharacters(objTemperamento.report.result.sections[i].sections[sens].questions[esc].answer).Trim();
                                        puntaje_subsection += calcularPuntajeInvertido(respuesta);
                                    }
                                    contQuestions = objTemperamento.report.result.sections[i].sections[sens].questions.Count();
                                    objPuntajeTemperamento.name_subsection = subsection;
                                    objPuntajeTemperamento.puntaje_subsection = (puntaje_subsection / contQuestions); //4
                                    break;

                            }
                            puntaje_sensacion += objPuntajeTemperamento.puntaje_subsection;
                            if (objPuntajeTemperamento.name_subsection != null)
                                lstPuntajeTemperamento.Add(objPuntajeTemperamento);
                        }
                        break;
                    case "RACIONAL": //Esta llegando Directo en vez de empatico
                        for (int rac = 0; rac < objTemperamento.report.result.sections[i].sections.Count(); rac++)
                        {
                            objPuntajeTemperamento = new PuntajeTemperamentoBE();
                            subsection = removecharacters(objTemperamento.report.result.sections[i].sections[rac].title).Trim();
                            objPuntajeTemperamento.name_section = section;
                            puntaje_subsection = 0;

                            switch (subsection)
                            {
                                case "OBJETIVO":
                                    for (int obj = 0; obj < objTemperamento.report.result.sections[i].sections[rac].questions.Count(); obj++)
                                    {
                                        respuesta = (objTemperamento.report.result.sections[i].sections[rac].questions[obj].answer == null) ? "0" : removecharacters(objTemperamento.report.result.sections[i].sections[rac].questions[obj].answer).Trim();
                                        puntaje_subsection += calcularPuntaje(respuesta);
                                    }
                                    contQuestions = objTemperamento.report.result.sections[i].sections[rac].questions.Count();
                                    objPuntajeTemperamento.name_subsection = subsection;
                                    objPuntajeTemperamento.puntaje_subsection = (puntaje_subsection / contQuestions); //4
                                    break;
                                case "DISTANTE":
                                    for (int dis = 0; dis < objTemperamento.report.result.sections[i].sections[rac].questions.Count(); dis++)
                                    {
                                        respuesta = (objTemperamento.report.result.sections[i].sections[rac].questions[dis].answer == null) ? "0" : removecharacters(objTemperamento.report.result.sections[i].sections[rac].questions[dis].answer).Trim();
                                        puntaje_subsection += calcularPuntaje(respuesta);
                                    }
                                    contQuestions = objTemperamento.report.result.sections[i].sections[rac].questions.Count();
                                    objPuntajeTemperamento.name_subsection = subsection;
                                    objPuntajeTemperamento.puntaje_subsection = (puntaje_subsection / contQuestions); //4
                                    break;
                                case "DIRECTO":
                                    for (int dir = 0; dir < objTemperamento.report.result.sections[i].sections[rac].questions.Count(); dir++)
                                    {
                                        respuesta = (objTemperamento.report.result.sections[i].sections[rac].questions[dir].answer == null) ? "0" : removecharacters(objTemperamento.report.result.sections[i].sections[rac].questions[dir].answer).Trim();
                                        puntaje_subsection += calcularPuntaje(respuesta);
                                    }
                                    contQuestions = objTemperamento.report.result.sections[i].sections[rac].questions.Count();
                                    objPuntajeTemperamento.name_subsection = subsection;
                                    objPuntajeTemperamento.puntaje_subsection = (puntaje_subsection / contQuestions); //4
                                    break;
                                    /*
                                case "CUESTIONADOR":
                                    for (int cue = 0; cue < objTemperamento.report.result.sections[i].sections[rac].questions.Count(); cue++)
                                    {
                                        respuesta = (objTemperamento.report.result.sections[i].sections[rac].questions[cue].answer == null) ? "0" : removecharacters(objTemperamento.report.result.sections[i].sections[rac].questions[cue].answer).Trim();
                                        puntaje_subsection += calcularPuntaje(respuesta);
                                    }
                                    contQuestions = objTemperamento.report.result.sections[i].sections[rac].questions.Count();
                                    objPuntajeTemperamento.name_subsection = subsection;
                                    objPuntajeTemperamento.puntaje_subsection = (puntaje_subsection / contQuestions); //3
                                    break;
                                    */
                            }
                            puntaje_racional += objPuntajeTemperamento.puntaje_subsection;
                            if (objPuntajeTemperamento.name_subsection != null)
                                lstPuntajeTemperamento.Add(objPuntajeTemperamento);
                        }
                        break;
                    case "EMOTIVO": //esta llegando emapatico en vez de directo
                    case "EMOCIONAL":
                        for (int emot = 0; emot < objTemperamento.report.result.sections[i].sections.Count(); emot++)
                        {
                            objPuntajeTemperamento = new PuntajeTemperamentoBE();
                            subsection = removecharacters(objTemperamento.report.result.sections[i].sections[emot].title).Trim();
                            objPuntajeTemperamento.name_section = section;
                            puntaje_subsection = 0;

                            switch (subsection)
                            {
                                case "COMPASIVO":
                                    for (int com = 0; com < objTemperamento.report.result.sections[i].sections[emot].questions.Count(); com++)
                                    {
                                        respuesta = (objTemperamento.report.result.sections[i].sections[emot].questions[com].answer == null) ? "0" : removecharacters(objTemperamento.report.result.sections[i].sections[emot].questions[com].answer).Trim();
                                        puntaje_subsection += calcularPuntajeInvertido(respuesta);
                                    }
                                    contQuestions = objTemperamento.report.result.sections[i].sections[emot].questions.Count();
                                    objPuntajeTemperamento.name_subsection = subsection;
                                    objPuntajeTemperamento.puntaje_subsection = (puntaje_subsection / contQuestions); //4
                                    break;
                                case "SUSCEPTIBLE":
                                    for (int sus = 0; sus < objTemperamento.report.result.sections[i].sections[emot].questions.Count(); sus++)
                                    {
                                        respuesta = (objTemperamento.report.result.sections[i].sections[emot].questions[sus].answer == null) ? "0" : removecharacters(objTemperamento.report.result.sections[i].sections[emot].questions[sus].answer).Trim();
                                        puntaje_subsection += calcularPuntajeInvertido(respuesta);
                                    }
                                    contQuestions = objTemperamento.report.result.sections[i].sections[emot].questions.Count();
                                    objPuntajeTemperamento.name_subsection = subsection;
                                    objPuntajeTemperamento.puntaje_subsection = (puntaje_subsection / contQuestions); //4
                                    break;
                                case "EMPATICO":
                                    for (int emp = 0; emp < objTemperamento.report.result.sections[i].sections[emot].questions.Count(); emp++)
                                    {
                                        respuesta = (objTemperamento.report.result.sections[i].sections[emot].questions[emp].answer == null) ? "0" : removecharacters(objTemperamento.report.result.sections[i].sections[emot].questions[emp].answer).Trim();
                                        puntaje_subsection += calcularPuntajeInvertido(respuesta);
                                    }
                                    contQuestions = objTemperamento.report.result.sections[i].sections[emot].questions.Count();
                                    objPuntajeTemperamento.name_subsection = subsection;
                                    objPuntajeTemperamento.puntaje_subsection = (puntaje_subsection / contQuestions); //4
                                    break;
                                    /*
                                case "CONCILIADOR":
                                    for (int cns = 0; cns < objTemperamento.report.result.sections[i].sections[emot].questions.Count(); cns++)
                                    {
                                        respuesta = (objTemperamento.report.result.sections[i].sections[emot].questions[cns].answer == null) ? "0" : removecharacters(objTemperamento.report.result.sections[i].sections[emot].questions[cns].answer).Trim();
                                        puntaje_subsection += calcularPuntajeInvertido(respuesta);
                                    }
                                    contQuestions = objTemperamento.report.result.sections[i].sections[emot].questions.Count();
                                    objPuntajeTemperamento.name_subsection = subsection;
                                    objPuntajeTemperamento.puntaje_subsection = (puntaje_subsection / contQuestions); //3
                                    break;
                                    */
                            }
                            puntaje_emotivo += objPuntajeTemperamento.puntaje_subsection;
                            if (objPuntajeTemperamento.name_subsection != null)
                                lstPuntajeTemperamento.Add(objPuntajeTemperamento);
                        }
                        break;
                    case "ORGANIZADO": // no esta llegando EXPLORA ALTERNATIVAS
                        for (int org = 0; org < objTemperamento.report.result.sections[i].sections.Count(); org++)
                        {
                            objPuntajeTemperamento = new PuntajeTemperamentoBE();
                            subsection = removecharacters(objTemperamento.report.result.sections[i].sections[org].title).Trim();
                            objPuntajeTemperamento.name_section = section;
                            puntaje_subsection = 0;

                            switch (subsection)
                            {
                                case "PLANIFICADO":
                                    for (int pla = 0; pla < objTemperamento.report.result.sections[i].sections[org].questions.Count(); pla++)
                                    {
                                        respuesta = (objTemperamento.report.result.sections[i].sections[org].questions[pla].answer == null) ? "0" : removecharacters(objTemperamento.report.result.sections[i].sections[org].questions[pla].answer).Trim();
                                        puntaje_subsection += calcularPuntaje(respuesta);
                                    }
                                    contQuestions = objTemperamento.report.result.sections[i].sections[org].questions.Count();
                                    objPuntajeTemperamento.name_subsection = subsection;
                                    objPuntajeTemperamento.puntaje_subsection = (puntaje_subsection / contQuestions); //6
                                    break;
                                case "METODICO":
                                    for (int met = 0; met < objTemperamento.report.result.sections[i].sections[org].questions.Count(); met++)
                                    {
                                        respuesta = (objTemperamento.report.result.sections[i].sections[org].questions[met].answer == null) ? "0" : removecharacters(objTemperamento.report.result.sections[i].sections[org].questions[met].answer).Trim();
                                        puntaje_subsection += calcularPuntaje(respuesta);
                                    }
                                    contQuestions = objTemperamento.report.result.sections[i].sections[org].questions.Count();
                                    objPuntajeTemperamento.name_subsection = subsection;
                                    objPuntajeTemperamento.puntaje_subsection = (puntaje_subsection / contQuestions); //5
                                    break;
                                case "ESTRUCTURADA":
                                    for (int est = 0; est < objTemperamento.report.result.sections[i].sections[org].questions.Count(); est++)
                                    {
                                        respuesta = (objTemperamento.report.result.sections[i].sections[org].questions[est].answer == null) ? "0" : removecharacters(objTemperamento.report.result.sections[i].sections[org].questions[est].answer).Trim();
                                        puntaje_subsection += calcularPuntaje(respuesta);
                                    }
                                    contQuestions = objTemperamento.report.result.sections[i].sections[org].questions.Count();
                                    objPuntajeTemperamento.name_subsection = subsection;
                                    objPuntajeTemperamento.puntaje_subsection = (puntaje_subsection / contQuestions); //4
                                    break;
                                    /*
                                case "CERRAR E IMPLEMENTAR":
                                    for (int exal = 0; exal < objTemperamento.report.result.sections[i].sections[org].questions.Count(); exal++)
                                    {
                                        respuesta = (objTemperamento.report.result.sections[i].sections[org].questions[exal].answer == null) ? "0" : removecharacters(objTemperamento.report.result.sections[i].sections[org].questions[exal].answer).Trim();
                                        puntaje_subsection += calcularPuntaje(respuesta);
                                    }
                                    contQuestions = objTemperamento.report.result.sections[i].sections[org].questions.Count();
                                    objPuntajeTemperamento.name_subsection = subsection;
                                    objPuntajeTemperamento.puntaje_subsection = (puntaje_subsection / contQuestions); //3
                                    break;
                                      */
                            }
                            puntaje_organizado += objPuntajeTemperamento.puntaje_subsection;
                            if (objPuntajeTemperamento.name_subsection != null)
                                lstPuntajeTemperamento.Add(objPuntajeTemperamento);
                        }
                        break;
                    case "CASUAL": // esta llegando explora alternativas en vez de cerrar e implementar
                        for (int cas = 0; cas < objTemperamento.report.result.sections[i].sections.Count(); cas++)
                        {
                            objPuntajeTemperamento = new PuntajeTemperamentoBE();
                            subsection = removecharacters(objTemperamento.report.result.sections[i].sections[cas].title).Trim();
                            objPuntajeTemperamento.name_section = section;
                            puntaje_subsection = 0;

                            switch (subsection)
                            {
                                case "ESPONTANEO":
                                    for (int esp = 0; esp < objTemperamento.report.result.sections[i].sections[cas].questions.Count(); esp++)
                                    {
                                        respuesta = (objTemperamento.report.result.sections[i].sections[cas].questions[esp].answer == null) ? "0" : removecharacters(objTemperamento.report.result.sections[i].sections[cas].questions[esp].answer).Trim();
                                        puntaje_subsection += calcularPuntajeInvertido(respuesta);
                                    }
                                    contQuestions = objTemperamento.report.result.sections[i].sections[cas].questions.Count();
                                    objPuntajeTemperamento.name_subsection = subsection;
                                    objPuntajeTemperamento.puntaje_subsection = (puntaje_subsection / contQuestions); //6
                                    break;
                                case "EVENTUAL":
                                    for (int eve = 0; eve < objTemperamento.report.result.sections[i].sections[cas].questions.Count(); eve++)
                                    {
                                        respuesta = (objTemperamento.report.result.sections[i].sections[cas].questions[eve].answer == null) ? "0" : removecharacters(objTemperamento.report.result.sections[i].sections[cas].questions[eve].answer).Trim();
                                        puntaje_subsection += calcularPuntajeInvertido(respuesta);
                                    }
                                    contQuestions = objTemperamento.report.result.sections[i].sections[cas].questions.Count();
                                    objPuntajeTemperamento.name_subsection = subsection;
                                    objPuntajeTemperamento.puntaje_subsection = (puntaje_subsection / contQuestions); //5
                                    break;
                                case "FLEXIBLE":
                                    for (int flex = 0; flex < objTemperamento.report.result.sections[i].sections[cas].questions.Count(); flex++)
                                    {
                                        respuesta = (objTemperamento.report.result.sections[i].sections[cas].questions[flex].answer == null) ? "0" : removecharacters(objTemperamento.report.result.sections[i].sections[cas].questions[flex].answer).Trim();
                                        puntaje_subsection += calcularPuntajeInvertido(respuesta);
                                    }
                                    contQuestions = objTemperamento.report.result.sections[i].sections[cas].questions.Count();
                                    objPuntajeTemperamento.name_subsection = subsection;
                                    objPuntajeTemperamento.puntaje_subsection = (puntaje_subsection / contQuestions); //4
                                    break;

                                    /*
                                case "EXPLORA ALTERNATIVAS":
                                    for (int ceim = 0; ceim < objTemperamento.report.result.sections[i].sections[cas].questions.Count(); ceim++)
                                    {
                                        respuesta = (objTemperamento.report.result.sections[i].sections[cas].questions[ceim].answer == null) ? "0" : removecharacters(objTemperamento.report.result.sections[i].sections[cas].questions[ceim].answer).Trim();
                                        puntaje_subsection += calcularPuntajeInvertido(respuesta);
                                    }
                                    contQuestions = objTemperamento.report.result.sections[i].sections[cas].questions.Count();
                                    objPuntajeTemperamento.name_subsection = subsection;
                                    objPuntajeTemperamento.puntaje_subsection = (puntaje_subsection / contQuestions); //3
                                    break;
                                    */

                            }

                            puntaje_casual += objPuntajeTemperamento.puntaje_subsection;
                            if (objPuntajeTemperamento.name_subsection != null)
                                lstPuntajeTemperamento.Add(objPuntajeTemperamento);
                        }
                        break;

                }

            }

            section = "";

            for (int i = 0; i < lstPuntajeTemperamento.Count(); i++)
            {
                section = lstPuntajeTemperamento[i].name_section.ToString().Trim();
                switch (section)
                {
                    case "EXTROVERTIDO":
                    case "EXTROVERSION":
                        lstPuntajeTemperamento[i].puntaje_section = puntaje_extroversion;
                        break;
                    case "INTROVERTIDO":
                    case "INTROVERSION":
                        lstPuntajeTemperamento[i].puntaje_section = puntaje_introversion;
                        break;
                    case "INTUITIVO":
                    case "INTUICION":
                        lstPuntajeTemperamento[i].puntaje_section = puntaje_intuicion;
                        break;
                    case "SENSORIAL":
                    case "SENSACION":
                        lstPuntajeTemperamento[i].puntaje_section = puntaje_sensacion;
                        break;
                    case "RACIONAL":
                        lstPuntajeTemperamento[i].puntaje_section = puntaje_racional;
                        break;
                    case "EMOTIVO":
                    case "EMOCIONAL":
                        lstPuntajeTemperamento[i].puntaje_section = puntaje_emotivo;
                        break;
                    case "ORGANIZADO":
                        lstPuntajeTemperamento[i].puntaje_section = puntaje_organizado;
                        break;
                    case "CASUAL":
                        lstPuntajeTemperamento[i].puntaje_section = puntaje_casual;
                        break;

                }
            }

            string nombreEvaluado = objTemperamento.report.evaluated.name;
            nombreEvaluado = removecharacters(nombreEvaluado);
            string nombrePDF = "";

            string fechaInicioTest = "";

            if (!String.IsNullOrEmpty(objTemperamento.report.initdate.date))
            {
                fechaInicioTest = objTemperamento.report.initdate.date.Substring(0, 10); //Año - Mes - Dia
                string[] fechas = fechaInicioTest.Split('-');
                fechaInicioTest = String.Concat(fechas[2], "-", fechas[1], "-", fechas[0]); //Dia- Mes - Año
            }


            nombrePDF = "Reporte_Consolidado_" + nombreEvaluado.Replace(" ", "").Replace(",", "") + ".pdf";



            /*********** INTERESES ******************/


            List<ItemArea> lstItemArea = new List<ItemArea>(){new ItemArea("ADM", 1), new ItemArea("AGR", 2), new ItemArea("ART", 3),
                                                              new ItemArea("COM", 4), new ItemArea("CON", 5), new ItemArea("CUL", 6),
                                                              new ItemArea("DEP", 7), new ItemArea("DIS", 8), new ItemArea("FIN", 9),
                                                              new ItemArea("INF", 10), new ItemArea("JUR", 11), new ItemArea("MAR", 12),
                                                              new ItemArea("MEC", 13), new ItemArea("MIN", 14), new ItemArea("PED", 15),
                                                              new ItemArea("SAL", 16), new ItemArea("SOC", 17), new ItemArea("TRA", 18),
                                                              new ItemArea("TUR", 19), new ItemArea("ADM", 20), new ItemArea("AGR", 21),
                                                              new ItemArea("ART", 22), new ItemArea("COM", 23), new ItemArea("CON", 24),
                                                              new ItemArea("CUL", 25), new ItemArea("DEP", 26), new ItemArea("DIS", 27),
                                                              new ItemArea("FIN", 28), new ItemArea("INF", 29), new ItemArea("JUR", 30),
                                                              new ItemArea("MAR", 31), new ItemArea("MEC", 32), new ItemArea("MIN", 33),
                                                              new ItemArea("PED", 34), new ItemArea("SAL", 35), new ItemArea("SOC", 36),
                                                              new ItemArea("TRA", 37), new ItemArea("TUR", 38), new ItemArea("ADM", 39),
                                                              new ItemArea("AGR", 40), new ItemArea("ART", 41), new ItemArea("COM", 42),
                                                              new ItemArea("CON", 43), new ItemArea("CUL", 44), new ItemArea("DEP", 45),
                                                              new ItemArea("DIS", 46), new ItemArea("FIN", 47), new ItemArea("INF", 48),
                                                              new ItemArea("JUR", 49), new ItemArea("MAR", 50), new ItemArea("MEC", 51),
                                                              new ItemArea("MIN", 52), new ItemArea("PED", 53), new ItemArea("SAL", 54),
                                                              new ItemArea("SOC", 55), new ItemArea("TRA", 56), new ItemArea("TUR", 57),
                                                              new ItemArea("ADM", 58), new ItemArea("AGR", 59), new ItemArea("ART", 60),
                                                              new ItemArea("COM", 61), new ItemArea("CON", 62), new ItemArea("CUL", 63),
                                                              new ItemArea("DEP", 64), new ItemArea("DIS", 65), new ItemArea("FIN", 66),
                                                              new ItemArea("INF", 67), new ItemArea("JUR", 68), new ItemArea("MAR", 69),
                                                              new ItemArea("MEC", 70), new ItemArea("MIN", 71), new ItemArea("PED", 72),
                                                              new ItemArea("SAL", 73), new ItemArea("SOC", 74), new ItemArea("TRA", 75),
                                                              new ItemArea("TUR", 76), new ItemArea("SAL", 77), new ItemArea("SAL", 78),};



            int countADM = 0;
            int countAGR = 0;
            int countART = 0;
            int countCOM = 0;
            int countCON = 0;
            int countCUL = 0;
            int countDEP = 0;
            int countDIS = 0;
            int countFIN = 0;
            int countINF = 0;
            int countJUR = 0;
            int countMAR = 0;
            int countMEC = 0;
            int countMIN = 0;
            int countPED = 0;
            int countSAL = 0;
            int countSOC = 0;
            int countTRA = 0;
            int countTUR = 0;

            int numeroPregunta = 1;

            List<PuntajeInteresBE> lstPuntajes = new List<PuntajeInteresBE>();

            string respuestaInteres = "";
            for (int i = 0; i < objInteresBE.report.result.Length; i += 3)
            {
                PuntajeInteresBE objPuntajeBE = new PuntajeInteresBE();
                objPuntajeBE.NumeroPregunta = numeroPregunta;
                objPuntajeBE.PuntajeTotal = 0;


                int pre = numeroPregunta;
                if (pre == 10 || pre == 25 || pre == 67 || pre == 53 || pre == 39)
                {
                    int g = 0;
                }


                // Puntaje Gustos
                respuestaInteres = (objInteresBE.report.result[i].answer == null) ? "0" : objInteresBE.report.result[i].answer.ToUpper().Trim();
                switch (respuestaInteres)
                {
                    case "TOTALMENTE EN DESACUERDO":
                        objPuntajeBE.PuntajeTotal = objPuntajeBE.PuntajeTotal + 1;
                        break;
                    case "EN DESACUERDO":
                        objPuntajeBE.PuntajeTotal = objPuntajeBE.PuntajeTotal + 2;
                        break;
                    case "DE ACUERDO":
                        objPuntajeBE.PuntajeTotal = objPuntajeBE.PuntajeTotal + 3;
                        break;
                    case "TOTALMENTE DE ACUERDO":
                        objPuntajeBE.PuntajeTotal = objPuntajeBE.PuntajeTotal + 4;
                        break;
                    case "0":
                        objPuntajeBE.PuntajeTotal = objPuntajeBE.PuntajeTotal + 0;
                        break;
                }

                // Puntaje Habilidades
                respuestaInteres = (objInteresBE.report.result[i + 1].answer == null) ? "0" : objInteresBE.report.result[i + 1].answer.ToUpper().Trim();
                switch (respuestaInteres)
                {
                    case "TOTALMENTE EN DESACUERDO":
                        objPuntajeBE.PuntajeTotal = objPuntajeBE.PuntajeTotal + 1;
                        break;
                    case "EN DESACUERDO":
                        objPuntajeBE.PuntajeTotal = objPuntajeBE.PuntajeTotal + 2;
                        break;
                    case "DE ACUERDO":
                        objPuntajeBE.PuntajeTotal = objPuntajeBE.PuntajeTotal + 3;
                        break;
                    case "TOTALMENTE DE ACUERDO":
                        objPuntajeBE.PuntajeTotal = objPuntajeBE.PuntajeTotal + 4;
                        break;
                    case "0":
                        objPuntajeBE.PuntajeTotal = objPuntajeBE.PuntajeTotal + 0;
                        break;
                }

                // Puntaje Satisfaccion
                respuestaInteres = (objInteresBE.report.result[i + 2].answer == null) ? "0" : objInteresBE.report.result[i + 2].answer.ToUpper().Trim();
                switch (respuestaInteres)
                {
                    case "SI":
                        objPuntajeBE.PuntajeTotal = objPuntajeBE.PuntajeTotal + 2;
                        break;
                    case "NO":
                        objPuntajeBE.PuntajeTotal = objPuntajeBE.PuntajeTotal + 1;
                        break;
                    case "0":
                        objPuntajeBE.PuntajeTotal = objPuntajeBE.PuntajeTotal + 0;
                        break;
                }

                bool encontrado = false;
                for (int j = 0; j < lstItemArea.Count && !encontrado; j++)
                {
                    if (numeroPregunta == lstItemArea[j].NumeroPregunta)
                    {
                        objPuntajeBE.Area = lstItemArea[j].Area;
                        encontrado = true;
                    }
                }

                lstPuntajes.Add(objPuntajeBE);
                numeroPregunta++;
            }

            for (int i = 0; i < lstPuntajes.Count; i++)
            {
                switch (lstPuntajes[i].Area)
                {

                    case "ADM":
                        countADM = countADM + lstPuntajes[i].PuntajeTotal;
                        break;
                    case "AGR":
                        countAGR = countAGR + lstPuntajes[i].PuntajeTotal;
                        break;
                    case "ART":
                        countART = countART + lstPuntajes[i].PuntajeTotal;
                        break;
                    case "COM":
                        countCOM = countCOM + lstPuntajes[i].PuntajeTotal;
                        break;
                    case "CON":
                        countCON = countCON + lstPuntajes[i].PuntajeTotal;
                        break;
                    case "CUL":
                        countCUL = countCUL + lstPuntajes[i].PuntajeTotal;
                        break;
                    case "DEP":
                        countDEP = countDEP + lstPuntajes[i].PuntajeTotal;
                        break;
                    case "DIS":
                        countDIS = countDIS + lstPuntajes[i].PuntajeTotal;
                        break;
                    case "FIN":
                        countFIN = countFIN + lstPuntajes[i].PuntajeTotal;
                        break;
                    case "INF":
                        countINF = countINF + lstPuntajes[i].PuntajeTotal;
                        break;
                    case "JUR":
                        countJUR = countJUR + lstPuntajes[i].PuntajeTotal;
                        break;
                    case "MAR":
                        countMAR = countMAR + lstPuntajes[i].PuntajeTotal;
                        break;
                    case "MEC":
                        countMEC = countMEC + lstPuntajes[i].PuntajeTotal;
                        break;
                    case "MIN":
                        countMIN = countMIN + lstPuntajes[i].PuntajeTotal;
                        break;
                    case "PED":
                        countPED = countPED + lstPuntajes[i].PuntajeTotal;
                        break;
                    case "SAL":
                        countSAL = countSAL + lstPuntajes[i].PuntajeTotal;
                        break;
                    case "SOC":
                        countSOC = countSOC + lstPuntajes[i].PuntajeTotal;
                        break;
                    case "TRA":
                        countTRA = countTRA + lstPuntajes[i].PuntajeTotal;
                        break;
                    case "TUR":
                        countTUR = countTUR + lstPuntajes[i].PuntajeTotal;
                        break;
                }
            }



            List<PuntajeTotalInteresBE> lstPuntajeTotal = new List<PuntajeTotalInteresBE>();

            PuntajeTotalInteresBE objADM = new PuntajeTotalInteresBE();
            objADM.Codigo = "ADM";
            objADM.Area = "Administración";
            objADM.Puntaje = countADM;


            PuntajeTotalInteresBE objAGR = new PuntajeTotalInteresBE();
            objAGR.Codigo = "AGR";
            objAGR.Area = "Agraria";
            objAGR.Puntaje = countAGR;

            PuntajeTotalInteresBE objART = new PuntajeTotalInteresBE();
            objART.Codigo = "ART";
            objART.Area = "Artística";
            objART.Puntaje = countART;

            PuntajeTotalInteresBE objCOM = new PuntajeTotalInteresBE();
            objCOM.Codigo = "COM";
            objCOM.Area = "Comunicación";
            objCOM.Puntaje = countCOM;

            PuntajeTotalInteresBE objCON = new PuntajeTotalInteresBE();
            objCON.Codigo = "CON";
            objCON.Area = "Construcción";
            objCON.Puntaje = countCON;

            PuntajeTotalInteresBE objCUL = new PuntajeTotalInteresBE();
            objCUL.Codigo = "CUL";
            objCUL.Area = "Culinaria";
            objCUL.Puntaje = countCUL;

            PuntajeTotalInteresBE objDEP = new PuntajeTotalInteresBE();
            objDEP.Codigo = "DEP";
            objDEP.Area = "Deportiva";
            objDEP.Puntaje = countDEP;

            PuntajeTotalInteresBE objDIS = new PuntajeTotalInteresBE();
            objDIS.Codigo = "DIS";
            objDIS.Area = "Diseño";
            objDIS.Puntaje = countDIS;

            PuntajeTotalInteresBE objFIN = new PuntajeTotalInteresBE();
            objFIN.Codigo = "FIN";
            objFIN.Area = "Financiera";
            objFIN.Puntaje = countFIN;

            PuntajeTotalInteresBE objINF = new PuntajeTotalInteresBE();
            objINF.Codigo = "INF";
            objINF.Area = "Informática";
            objINF.Puntaje = countINF;

            PuntajeTotalInteresBE objJUR = new PuntajeTotalInteresBE();
            objJUR.Codigo = "JUR";
            objJUR.Area = "Jurídica";
            objJUR.Puntaje = countJUR;

            PuntajeTotalInteresBE objMAR = new PuntajeTotalInteresBE();
            objMAR.Codigo = "MAR";
            objMAR.Area = "Marketing";
            objMAR.Puntaje = countMAR;

            PuntajeTotalInteresBE objMEC = new PuntajeTotalInteresBE();
            objMEC.Codigo = "MEC";
            objMEC.Area = "Mecanico";
            objMEC.Puntaje = countMEC;

            PuntajeTotalInteresBE objMIN = new PuntajeTotalInteresBE();
            objMIN.Codigo = "MIN";
            objMIN.Area = "Minera";
            objMIN.Puntaje = countMIN;

            PuntajeTotalInteresBE objPED = new PuntajeTotalInteresBE();
            objPED.Codigo = "PED";
            objPED.Area = "Pedagogia";
            objPED.Puntaje = countPED;

            PuntajeTotalInteresBE objSAL = new PuntajeTotalInteresBE();
            objSAL.Codigo = "SAL";
            objSAL.Area = "Salud";
            objSAL.Puntaje = CalculoPuntajeInteres(countSAL); //Calculo especial por tener 6 preguntas
            countSAL = objSAL.Puntaje;

            PuntajeTotalInteresBE objSOC = new PuntajeTotalInteresBE();
            objSOC.Codigo = "SOC";
            objSOC.Area = "Social";
            objSOC.Puntaje = countSOC;

            PuntajeTotalInteresBE objTRA = new PuntajeTotalInteresBE();
            objTRA.Codigo = "TRA";
            objTRA.Area = "Traducción";
            objTRA.Puntaje = countTRA;

            PuntajeTotalInteresBE objTUR = new PuntajeTotalInteresBE();
            objTUR.Codigo = "TUR";
            objTUR.Area = "Turismo";
            objTUR.Puntaje = countTUR;

            lstPuntajeTotal.Add(objADM);
            lstPuntajeTotal.Add(objAGR);
            lstPuntajeTotal.Add(objART);
            lstPuntajeTotal.Add(objCOM);
            lstPuntajeTotal.Add(objCON);
            lstPuntajeTotal.Add(objCUL);
            lstPuntajeTotal.Add(objDEP);
            lstPuntajeTotal.Add(objDIS);
            lstPuntajeTotal.Add(objFIN);
            lstPuntajeTotal.Add(objINF);
            lstPuntajeTotal.Add(objJUR);
            lstPuntajeTotal.Add(objMAR);
            lstPuntajeTotal.Add(objMEC);
            lstPuntajeTotal.Add(objMIN);
            lstPuntajeTotal.Add(objPED);
            lstPuntajeTotal.Add(objSAL);
            lstPuntajeTotal.Add(objSOC);
            lstPuntajeTotal.Add(objTRA);
            lstPuntajeTotal.Add(objTUR);


            for (int i = 0; i < lstPuntajeTotal.Count; i++)
            {
                for (int j = i + 1; j < lstPuntajeTotal.Count; j++)
                {
                    if (lstPuntajeTotal[i].Puntaje > lstPuntajeTotal[j].Puntaje)
                    {
                        PuntajeTotalInteresBE objAux = new PuntajeTotalInteresBE();
                        objAux.Area = lstPuntajeTotal[i].Area;
                        objAux.Codigo = lstPuntajeTotal[i].Codigo;
                        objAux.Puntaje = lstPuntajeTotal[i].Puntaje;

                        lstPuntajeTotal[i].Area = lstPuntajeTotal[j].Area;
                        lstPuntajeTotal[i].Codigo = lstPuntajeTotal[j].Codigo;
                        lstPuntajeTotal[i].Puntaje = lstPuntajeTotal[j].Puntaje;

                        lstPuntajeTotal[j].Area = objAux.Area;
                        lstPuntajeTotal[j].Codigo = objAux.Codigo;
                        lstPuntajeTotal[j].Puntaje = objAux.Puntaje;
                    }
                }
            }







            calcularReporteConsolidado(nombrePDF, nombreEvaluado, fechaInicioTest, puntaje_extroversion, puntaje_introversion,
                            puntaje_intuicion, puntaje_sensacion, puntaje_racional, puntaje_emotivo, puntaje_organizado, puntaje_casual,
                            lstPuntajeTemperamento, true, codUser,
                            countADM, countAGR, countART, countCOM, countCON, countCUL, countDEP, countDIS, countFIN, countINF, countJUR, countMAR, countMEC, countMIN, countPED, countSAL, countSOC, countTRA, countTUR);


            return nombrePDF;
        }



        private int CalculoPuntajeInteres(int puntaje)
        {

            double puntajeaux = puntaje;
            puntajeaux = (puntajeaux / 6) * 4;
            return Convert.ToInt32(puntajeaux);
        }


        private string removecharacters(string str)
        {
            str = str.ToUpper();
            str = str.Replace("Ó", "O");
            str = str.Replace("Í", "I");
            str = str.Replace("Á", "A");
            str = str.Replace("É", "E");
            str = str.Replace("Ú", "U");
            str = str.Replace("Ñ", "N");
            str = str.Replace(",", "");
            str = str.Replace(".", "");

            return str;
        }


        private void calcularReporteConsolidado(string nombrePDF, string nameEvaluated, string fechaInitTest, double puntaje_extroversion,
                                     double puntaje_introversion, double puntaje_intuicion, double puntaje_sensacion,
                                     double puntaje_racional, double puntaje_emotivo, double puntaje_organizado, double puntaje_casual,
                                     List<PuntajeTemperamentoBE> lstPtsTemperamento, bool esAdulto, string codUser,
                                     int countADM, int countAGR, int countART, int countCOM, int countCON, int countCUL, int countDEP,
                                     int countDIS, int countFIN, int countINF, int countJUR, int countMAR, int countMEC, int countMIN,
                                     int countPED, int countSAL, int countSOC, int countTRA, int countTUR)
        {

            string nombreWord = "";
            string fileName = "";

            bool usekey = Convert.ToBoolean(ConfigurationManager.AppSettings["USEKEY_CONS"]);
            nombreWord = "Reporte_Consolidado" + "" + ".docx";
            fileName = Server.MapPath(@"~/Reporte/Plantilla/Plantilla_Reporte_Consolidado.docx");


            string pathPDF = Server.MapPath("~/Reporte/");
            string archivoNuevo = Server.MapPath("~/Reporte/" + nombreWord);


            List<ElementoTemperamentoBE> lstElementos = new List<ElementoTemperamentoBE>();

            List<RecomendacionBE> lstDescripcionesMasDesarrolladas = new List<RecomendacionBE>();
            CarreraBC objCarreraBC = new CarreraBC();

            //TALENTOS
            //CreatPDFwithItextSharp(DNI, administrador);
            int[] orientaciones = new int[6] { 0, 0, 0, 0, 0, 0 }; //Ejecucion: 0, Pensamiento: 1, Innovacion: 2, Liderazgo: 3
            // Personas: 4, Estructura: 5

            ResultadoBC objResultadoBC = new ResultadoBC();
            TalentoBC objTalentoBC = new TalentoBC();

            ResultadoFinalBE objResultadoBE = objResultadoBC.ObtenerResultadoParticipanteAdulto(codUser, ref orientaciones);
            lstDescripcionesMasDesarrolladas = objTalentoBC.ObtenerDescripcionesTalentosMasDesarrollados(objResultadoBE.lstTalentosMasDesarrollados, 1);
            //ResultadoFinalBE objResultadoBE = objResultadoBC.ObtenerResultadoParticipante(codUser, ref orientaciones);


            List<PuntajeTotalInteresBE> lstPT = null;
            List<string> lstElementos_Extroversion_Introversion = new List<string>();
            List<string> lstElementos_Racional_Emocional = new List<string>();
            List<string> lstElementos_Organizado_Casual = new List<string>();
            List<string> lstElementos_Intuicion_Sensacion = new List<string>();

            string strTipologiaRueda = "";
            try
            {
                EliminarArchivosTemporales(archivoNuevo, "");
                File.Copy(fileName, archivoNuevo);


                lstElementos = objCarreraBC.ListarElementos();

                //Setea en doc el nombre del evaluado
                SearchAndReplace(archivoNuevo, "#NOMBRE_PERSONA#", nameEvaluated);

                //Setea en doc la fecha que inicio el test
                SearchAndReplace(archivoNuevo, "#FECHA_TEST#", fechaInitTest);
                SearchAndReplace(archivoNuevo, "NOMBREEV", nameEvaluated);

                using (WordprocessingDocument doc = WordprocessingDocument.Open(archivoNuevo, true))
                {
                    List<Table> lstTables = doc.MainDocumentPart.Document.Body.Elements<Table>().ToList();
                    lstElementos_Extroversion_Introversion = GenerarGrafico_Extroversion_Introversion(doc, lstTables[1], lstPtsTemperamento);
                    lstElementos_Intuicion_Sensacion = GenerarGrafico_Intuicion_Sensacion(doc, lstTables[2], lstPtsTemperamento);
                    lstElementos_Racional_Emocional = GenerarGrafico_Racional_Emocional(doc, lstTables[3], lstPtsTemperamento);
                    lstElementos_Organizado_Casual = GenerarGrafico_Organizado_Casual(doc, lstTables[4], lstPtsTemperamento);

                    int cont_aux = 1;
                    for (int i = 0; i < lstElementos_Extroversion_Introversion.Count(); i++)
                    {
                        for (int j = 0; j < lstElementos.Count(); j++)
                        {
                            if (lstElementos_Extroversion_Introversion[i].ToString().Equals(lstElementos[j].nombre_mayus.ToString()))
                            {
                                SearchAndReplace(doc, fileName, "ElementoEI" + cont_aux, lstElementos[j].nombre.ToString());
                                SearchAndReplace(doc, fileName, "Descelet" + cont_aux, lstElementos[j].descripcion.ToString());
                                cont_aux++;
                                break;
                            }

                        }
                    }


                    cont_aux = 1;

                    for (int i = 0; i < lstElementos_Intuicion_Sensacion.Count(); i++)
                    {
                        for (int j = 0; j < lstElementos.Count(); j++)
                        {
                            if (lstElementos_Intuicion_Sensacion[i].ToString().Equals(lstElementos[j].nombre_mayus.ToString()))
                            {
                                SearchAndReplace(doc, fileName, "ElementoIS" + cont_aux, lstElementos[j].nombre.ToString());
                                SearchAndReplace(doc, fileName, "DescIS" + cont_aux, lstElementos[j].descripcion.ToString());
                                cont_aux++;
                                break;
                            }
                        }
                    }

                    cont_aux = 1;

                    for (int i = 0; i < lstElementos_Racional_Emocional.Count(); i++)
                    {
                        for (int j = 0; j < lstElementos.Count(); j++)
                        {
                            if (lstElementos_Racional_Emocional[i].ToString().Equals(lstElementos[j].nombre_mayus.ToString()))
                            {
                                SearchAndReplace(doc, fileName, "ElementoRE" + cont_aux, lstElementos[j].nombre.ToString());
                                SearchAndReplace(doc, fileName, "DesceleRE" + cont_aux, lstElementos[j].descripcion.ToString());
                                cont_aux++;
                                break;
                            }
                        }
                    }

                    cont_aux = 1;

                    for (int i = 0; i < lstElementos_Organizado_Casual.Count(); i++)
                    {
                        for (int j = 0; j < lstElementos.Count(); j++)
                        {
                            if (lstElementos_Organizado_Casual[i].ToString().Equals(lstElementos[j].nombre_mayus.ToString()))
                            {
                                SearchAndReplace(doc, fileName, "ElementoOC" + cont_aux, lstElementos[j].nombre.ToString());
                                SearchAndReplace(doc, fileName, "descripcionOC" + cont_aux, lstElementos[j].descripcion.ToString());
                                cont_aux++;
                                break;
                            }
                        }
                    }



                }


                using (WordprocessingDocument doc = WordprocessingDocument.Open(archivoNuevo, true))
                {
                    List<Table> lstTables = doc.MainDocumentPart.Document.Body.Elements<Table>().ToList();


                    strTipologiaRueda = Generar_Tipologia(puntaje_extroversion_vs_introversio_bar, puntaje_intuicion_vs_sensacion_bar,
                                     puntaje_racional_vs_emotivo_bar, puntaje_organizado_vs_casual_bar, doc, lstTables[0]);

                    string straux = objCarreraBC.ObtenerDescripcionRueda(strTipologiaRueda.ToUpper());
                    string L1 = strTipologiaRueda.Substring(0, 1);
                    string L2 = (strTipologiaRueda.Length >= 4) ? strTipologiaRueda.Substring(1, 1) : "";
                    string L3 = (strTipologiaRueda.Length >= 4) ? strTipologiaRueda.Substring(2, 1) : "";
                    string L4 = (strTipologiaRueda.Length >= 4) ? strTipologiaRueda.Substring(3, 1) : "";
                    //string L2 = strTipologiaRueda.Substring(1, 1);
                    //string L3 = strTipologiaRueda.Substring(2, 1);
                    //string L4 = strTipologiaRueda.Substring(3, 1);

                    SearchAndReplace(doc, fileName, "L1", L1);
                    SearchAndReplace(doc, fileName, "L2", L2);
                    SearchAndReplace(doc, fileName, "L3", L3);
                    SearchAndReplace(doc, fileName, "L4", L4);

                    SearchAndReplace(doc, fileName, "TIPOLOGIA", "(" + strTipologiaRueda + ")");
                    SearchAndReplace(doc, fileName, "DescripcionRueda", straux);

                    MostrarAreas_DescripcionSuperRueda(doc, fileName, L1, L2, L3, L4);

                    //Talentos
                    FormatearTablaResultadoChart(lstTables[6], orientaciones);
                    FormatearTablaTalentosMasDesarrollados(lstTables[7], objResultadoBE.lstTalentosMasDesarrollados, objResultadoBE.lstTEMasDesarrollados);

                    ModifyChartSimplified(doc.MainDocumentPart, "B", 2, (100 * orientaciones[0] / 12).ToString(), false);
                    ModifyChartSimplified(doc.MainDocumentPart, "B", 3, (100 * orientaciones[1] / 12).ToString(), false);
                    ModifyChartSimplified(doc.MainDocumentPart, "B", 4, (100 * orientaciones[2] / 12).ToString(), false);
                    ModifyChartSimplified(doc.MainDocumentPart, "B", 5, (100 * orientaciones[3] / 12).ToString(), false);
                    ModifyChartSimplified(doc.MainDocumentPart, "B", 6, (100 * orientaciones[4] / 12).ToString(), false);
                    ModifyChartSimplified(doc.MainDocumentPart, "B", 7, (100 * orientaciones[5] / 12).ToString(), false);

                    int TalentSize = objResultadoBE.lstTalentosMasDesarrollados.Count();
                    MostrarDescripcionesTalentos(lstTables[8], archivoNuevo, lstDescripcionesMasDesarrolladas, true, TalentSize, doc);

                    //Intereses
                    lstPT = CalculoPT(lstTables[9], countADM, countAGR, countART, countCOM, countCON, countCUL,
                                      countDEP, countDIS, countFIN, countINF, countJUR, countMAR, countMEC,
                                      countMIN, countPED, countSAL, countSOC, countTRA, countTUR);

                    //Puntaje Alumno Test
                    MostrarPuntajesTest(lstTables[9], lstPT);

                    MostrarBarrasPT(doc, lstTables[9], lstPT);

                    int contMasDesarrolladas = CalcularAreasMasDesarrolladas_2(lstPT, lstTables[10]);

                    if(contMasDesarrolladas == 0)
                        RemoverTabla(doc, 10);
                }



                //add data to General Report
                objResultadoBC.addInterestsResult(codUser, Convert.ToInt32(strIntereses_id), intADM, intARG, intART, intCOM, intCON,
                                                    intCUL, intDEP, intDIS, intFIN, intINF, intJUR, intMAR, intMEC, intMIN, intPED,
                                                    intSAL, intSOC, intTRA, intTUR);

                objResultadoBC.addTemperamentsResult(strCodUserTemperament, Convert.ToInt32(strTemperamento_id), Convert.ToInt32(strIntereses_id),
                                                     dblAmbDinamic_AmbTranquilo, dblSociable_Intimo, dblEntusiasta_Calmado,
                                                     dblComunicativo_Reservado, dblInstintivo_Esceptico, dblOriginal_Tradicional,
                                                     dblCreativo_Realista, dblObjetivo_Compasivo, dblDistante_Susceptible,
                                                     dblDirecto_Empatico, dblPlanificado_Espontaneo, dblMetodico_Eventual,
                                                     dblEstructurado_Flexible);


                if (usekey)
                {
                    //Libreria Externa
                    ConvertirWordToPDF(archivoNuevo, nombreWord, pathPDF, nombrePDF);
                }
                else
                {
                    //ConvertirWordToPDF(archivoNuevo, nombreWord, pathPDF, nombrePDF);
                    // TODO: Descomentar las lineas de abajo para que funcione la generación del reporte con Interop

                    Microsoft.Office.Interop.Word.Application word = null;
                    Microsoft.Office.Interop.Word.Document document = null;
                    object missing = Type.Missing;

                    try
                    {
                        word = new Microsoft.Office.Interop.Word.Application();
                        document = new Microsoft.Office.Interop.Word.Document();

                        object readOnly = true;
                        object fileNameInterop = archivoNuevo;
                        document = word.Documents.Open(ref fileNameInterop, ref missing, ref readOnly,
                                                       ref missing, ref missing, ref missing,
                                                       ref missing, ref missing, ref missing,
                                                       ref missing, ref missing, ref missing,
                                                       ref missing, ref missing, ref missing,
                                                       ref missing);

                        document.Activate();

                        string archivoPDF = pathPDF + nombrePDF;

                        document.ExportAsFixedFormat(archivoPDF, Microsoft.Office.Interop.Word.WdExportFormat.wdExportFormatPDF, false,
                            Microsoft.Office.Interop.Word.WdExportOptimizeFor.wdExportOptimizeForPrint,
                            Microsoft.Office.Interop.Word.WdExportRange.wdExportAllDocument, 0, 0,
                            Microsoft.Office.Interop.Word.WdExportItem.wdExportDocumentWithMarkup, true, false,
                            Microsoft.Office.Interop.Word.WdExportCreateBookmarks.wdExportCreateNoBookmarks, false, false, true, ref missing);


                    }
                    catch (Exception)
                    {
                        throw;
                    }
                    finally
                    {
                        object save = false;

                        document.Close(ref save,
                                       ref missing,
                                       ref missing);

                        word.Application.Quit(ref missing,
                                              ref missing,
                                              ref missing);
                    }
                }



                EliminarArchivosTemporales(archivoNuevo, "");
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        private void MostrarAreas_DescripcionSuperRueda(WordprocessingDocument doc, string fileName, string L1, string L2, string L3, string L4)
        {
            switch (L1)
            {
                case "I": SearchAndReplace(doc, fileName, "desL1", "Introvertido"); break;
                case "i": SearchAndReplace(doc, fileName, "desL1", "introvertido"); break;
                case "E": SearchAndReplace(doc, fileName, "desL1", "Extrovertido"); break;
                case "e": SearchAndReplace(doc, fileName, "desL1", "extrovertido"); break;
            }

            switch (L2)
            {
                case "S": SearchAndReplace(doc, fileName, "desL2", "Sensorial"); break;
                case "N": SearchAndReplace(doc, fileName, "desL2", "Intuitivo"); break;
                case "s": SearchAndReplace(doc, fileName, "desL2", "sensorial"); break;
                case "n": SearchAndReplace(doc, fileName, "desL2", "intuitivo"); break;
            }

            switch (L3)
            {
                case "R": SearchAndReplace(doc, fileName, "desL3", "Racional"); break;
                case "M": SearchAndReplace(doc, fileName, "desL3", "Emocional"); break;
                case "r": SearchAndReplace(doc, fileName, "desL3", "racional"); break;
                case "m": SearchAndReplace(doc, fileName, "desL3", "emocional"); break;
            }

            switch (L4)
            {
                case "C": SearchAndReplace(doc, fileName, "desL4", "Casual"); break;
                case "O": SearchAndReplace(doc, fileName, "desL4", "Organizado"); break;
                case "c": SearchAndReplace(doc, fileName, "desL4", "casual"); break;
                case "o": SearchAndReplace(doc, fileName, "desL4", "organizado"); break;
            }
        }


        private string Generar_Tipologia(double pts_extrovertido_vs_introvertido, double pts_intuitivo_vs_sensorial,
                                        double pts_racional_vs_emotivo, double pts_organizado_vs_casual, WordprocessingDocument doc, Table table)
        {

            string llave = "";
            string imagen = "";
            string pathImages = Server.MapPath("~/Images/SuperRueda/");

            string strResultado_extroversion_vs_introversion = roundRueda(pts_extrovertido_vs_introvertido / 4).ToString();
            string strResultado_sensacion_vs_intuicion = roundRueda(pts_intuitivo_vs_sensorial / 3).ToString();
            string strResultado_racional_emotivo = roundRueda(pts_racional_vs_emotivo / 3).ToString();
            string strResultado_casual_organizado = roundRueda(pts_organizado_vs_casual / 3).ToString();


            if (strResultado_extroversion_vs_introversion.Equals("4") || strResultado_extroversion_vs_introversion.Equals("4.0"))
            {
                strResultado_extroversion_vs_introversion = getScaleforEI().ToString();
            }

            if (strResultado_sensacion_vs_intuicion.Equals("4") || strResultado_sensacion_vs_intuicion.Equals("4.0"))
            {
                strResultado_sensacion_vs_intuicion = getScaleforIS().ToString();
            }

            if (strResultado_racional_emotivo.Equals("4") || strResultado_racional_emotivo.Equals("4.0"))
            {
                strResultado_racional_emotivo = getScaleforRE().ToString();
            }

            if (strResultado_casual_organizado.Equals("4") || strResultado_casual_organizado.Equals("4.0"))
            {
                strResultado_casual_organizado = getScaleforOC().ToString();
            }


            TableRow row;
            TableCell cell;

            row = table.Elements<TableRow>().ElementAt(0);
            cell = row.Elements<TableCell>().ElementAt(0);

            switch (strResultado_extroversion_vs_introversion)
            {
                case "7":
                case "6.5":
                case "6":
                case "5.5":
                case "5":
                    llave = "E";
                    imagen = pathImages + "E_" + strResultado_extroversion_vs_introversion.Replace(".", "_") + ".png";
                    AddImage(cell, doc.MainDocumentPart, imagen, "imIds1"); //Intro Extro
                    break;
                case "4.5":
                case "4":
                    llave = "e";
                    imagen = pathImages + "e_" + strResultado_extroversion_vs_introversion.Replace(".", "_") + ".png";
                    AddImage(cell, doc.MainDocumentPart, imagen, "imIds1"); //Intro Extro
                    break;
                case "3.5":
                    llave = "i";
                    imagen = pathImages + "i_" + strResultado_extroversion_vs_introversion.Replace(".", "_") + ".png";
                    AddImage(cell, doc.MainDocumentPart, imagen, "imIds1"); //Intro Extro
                    break;
                case "3":
                case "2.5":
                case "2":
                case "1.5":
                case "1":
                    llave = "I";
                    imagen = pathImages + "I_" + strResultado_extroversion_vs_introversion.Replace(".", "_") + ".png";
                    AddImage(cell, doc.MainDocumentPart, imagen, "imIds1"); //Intro Extro
                    break;
            }


            row = table.Elements<TableRow>().ElementAt(1);
            cell = row.Elements<TableCell>().ElementAt(1);

            switch (strResultado_sensacion_vs_intuicion)
            {
                case "7":
                case "6.5":
                case "6":
                case "5.5":
                case "5":
                    llave = llave + "N";
                    imagen = pathImages + "N_" + strResultado_sensacion_vs_intuicion.Replace(".", "_") + ".png";
                    AddImage(cell, doc.MainDocumentPart, imagen, "imIds4");
                    break;
                case "4.5":
                case "4":
                    llave = llave + "n";
                    imagen = pathImages + "n_" + strResultado_sensacion_vs_intuicion.Replace(".", "_") + ".png";
                    AddImage(cell, doc.MainDocumentPart, imagen, "imIds4");
                    break;
                case "3.5":
                    llave = llave + "s";
                    imagen = pathImages + "s_" + strResultado_sensacion_vs_intuicion.Replace(".", "_") + ".png";
                    AddImage(cell, doc.MainDocumentPart, imagen, "imIds4");
                    break;
                case "3":
                case "2.5":
                case "2":
                case "1.5":
                case "1":
                    llave = llave + "S";
                    imagen = pathImages + "S_" + strResultado_sensacion_vs_intuicion.Replace(".", "_") + ".png";
                    AddImage(cell, doc.MainDocumentPart, imagen, "imIds4");
                    break;
            }

            cell = row.Elements<TableCell>().ElementAt(0);

            switch (strResultado_racional_emotivo)
            {
                case "7":
                case "6.5":
                case "6":
                case "5.5":
                case "5":
                    llave = llave + "R";
                    imagen = pathImages + "R_" + strResultado_racional_emotivo.Replace(".", "_") + ".png";
                    AddImage(cell, doc.MainDocumentPart, imagen, "imIds3"); //Racional Emocional
                    break;
                case "4.5":
                case "4":
                    llave = llave + "r";
                    imagen = pathImages + "r_" + strResultado_racional_emotivo.Replace(".", "_") + ".png";
                    AddImage(cell, doc.MainDocumentPart, imagen, "imIds3"); //Racional Emocional
                    break;
                case "3.5":
                    llave = llave + "m";
                    imagen = pathImages + "m_" + strResultado_racional_emotivo.Replace(".", "_") + ".png";
                    AddImage(cell, doc.MainDocumentPart, imagen, "imIds3"); //Racional Emocional
                    break;
                case "3":
                case "2.5":
                case "2":
                case "1.5":
                case "1":
                    llave = llave + "M";
                    imagen = pathImages + "M_" + strResultado_racional_emotivo.Replace(".", "_") + ".png";
                    AddImage(cell, doc.MainDocumentPart, imagen, "imIds3"); //Racional Emocional
                    break;
            }


            row = table.Elements<TableRow>().ElementAt(0);
            cell = row.Elements<TableCell>().ElementAt(1);

            switch (strResultado_casual_organizado)
            {
                case "7":
                case "6.5":
                case "6":
                case "5.5":
                case "5":
                    llave = llave + "O";
                    imagen = pathImages + "O_" + strResultado_casual_organizado.Replace(".", "_") + ".png";
                    AddImage(cell, doc.MainDocumentPart, imagen, "imIds2"); //Organizado - Casual
                    break;
                case "4.5":
                case "4":
                    llave = llave + "o";
                    imagen = pathImages + "o_" + strResultado_casual_organizado.Replace(".", "_") + ".png";
                    AddImage(cell, doc.MainDocumentPart, imagen, "imIds2"); //Organizado - Casual
                    break;
                case "3.5":
                    llave = llave + "c";
                    imagen = pathImages + "c_" + strResultado_casual_organizado.Replace(".", "_") + ".png";
                    AddImage(cell, doc.MainDocumentPart, imagen, "imIds2"); //Organizado - Casual
                    break;
                case "3":
                case "2.5":
                case "2":
                case "1.5":
                case "1":
                    llave = llave + "C";
                    imagen = pathImages + "C_" + strResultado_casual_organizado.Replace(".", "_") + ".png";
                    AddImage(cell, doc.MainDocumentPart, imagen, "imIds2"); //Organizado - Casual
                    break;
            }

            return llave;
        }


        private double getScaleforEI()
        {
            double strMaxValUp = 4.0;
            double strMaxValDown = 4.0;

            double newResult = 0;
            double resultMajor = ScaleNumbersEI[0];

            for (int i = 0; i < ScaleNumbersEI.Count(); i++)
            {

                if (ScaleNumbersEI[i] >= strMaxValUp)
                {
                    strMaxValUp = ScaleNumbersEI[i];
                }

                if (ScaleNumbersEI[i] <= strMaxValDown)
                {
                    strMaxValDown = ScaleNumbersEI[i];
                }
            }

            double dblMaxValScaleUp = convertScalePoints(strMaxValUp.ToString());
            double dblMaxValScaleDown = convertScalePoints(strMaxValDown.ToString());

            if (dblMaxValScaleUp > dblMaxValScaleDown) //Barra predominante hacia arriba
            {
                newResult = 4;
            }
            else if (dblMaxValScaleDown > dblMaxValScaleUp) //Barra predominandte hacia abajo
            {
                newResult = 3.5;
            }
            else if (dblMaxValScaleDown == dblMaxValScaleUp) //Todas la barras son iguales
            {
                if (ScaleNumbersEI[3] <= 4.0)
                    newResult = 3.5;
                else
                    newResult = 4.0;
            }


            return newResult;
        }

        private double getScaleforIS()
        {
            double strMaxValUp = 4.0;
            double strMaxValDown = 4.0;
            double newResult = 0;


            for (int i = 0; i < ScaleNumbersIS.Count(); i++)
            {
                if (ScaleNumbersIS[i] >= strMaxValUp)
                {
                    strMaxValUp = ScaleNumbersIS[i];
                }

                if (ScaleNumbersIS[i] <= strMaxValDown)
                {
                    strMaxValDown = ScaleNumbersIS[i];
                }
            }

            double dblMaxValScaleUp = convertScalePoints(strMaxValUp.ToString());
            double dblMaxValScaleDown = convertScalePoints(strMaxValDown.ToString());

            if (dblMaxValScaleUp > dblMaxValScaleDown) //Barra predominante hacia arriba
            {
                newResult = 4;
            }
            else if (dblMaxValScaleDown > dblMaxValScaleUp) //Barra predominandte hacia abajo
            {
                newResult = 3.5;
            }
            else if (dblMaxValScaleDown == dblMaxValScaleUp) //Todas la barras son iguales
            {
                if (ScaleNumbersIS[0] <= 4.0)
                    newResult = 3.5;
                else
                    newResult = 4.0;
            }

            return newResult;


        }

        private double getScaleforRE()
        {
            double strMaxValUp = 4.0;
            double strMaxValDown = 4.0;
            double newResult = 0;


            for (int i = 0; i < ScaleNumbersRE.Count(); i++)
            {
                if (ScaleNumbersRE[i] >= strMaxValUp)
                {
                    strMaxValUp = ScaleNumbersRE[i];
                }

                if (ScaleNumbersRE[i] <= strMaxValDown)
                {
                    strMaxValDown = ScaleNumbersRE[i];
                }
            }

            double dblMaxValScaleUp = convertScalePoints(strMaxValUp.ToString());
            double dblMaxValScaleDown = convertScalePoints(strMaxValDown.ToString());

            if (dblMaxValScaleUp > dblMaxValScaleDown) //Barra predominante hacia arriba
            {
                newResult = 4;
            }
            else if (dblMaxValScaleDown > dblMaxValScaleUp) //Barra predominandte hacia abajo
            {
                newResult = 3.5;
            }
            else if (dblMaxValScaleDown == dblMaxValScaleUp) //Todas la barras son iguales
            {
                if (ScaleNumbersRE[0] <= 4.0)
                    newResult = 3.5;
                else
                    newResult = 4.0;
            }

            return newResult;


        }

        private double getScaleforOC()
        {
            double strMaxValUp = 4.0;
            double strMaxValDown = 4.0;
            double newResult = 0;


            for (int i = 0; i < ScaleNumbersOC.Count(); i++)
            {
                if (ScaleNumbersOC[i] >= strMaxValUp)
                {
                    strMaxValUp = ScaleNumbersOC[i];
                }

                if (ScaleNumbersOC[i] <= strMaxValDown)
                {
                    strMaxValDown = ScaleNumbersOC[i];
                }
            }

            double dblMaxValScaleUp = convertScalePoints(strMaxValUp.ToString());
            double dblMaxValScaleDown = convertScalePoints(strMaxValDown.ToString());

            if (dblMaxValScaleUp > dblMaxValScaleDown) //Barra predominante hacia arriba
            {
                newResult = 4;
            }
            else if (dblMaxValScaleDown > dblMaxValScaleUp) //Barra predominandte hacia abajo
            {
                newResult = 3.5;
            }
            else if (dblMaxValScaleDown == dblMaxValScaleUp) //Todas la barras son iguales
            {
                if (ScaleNumbersOC[0] <= 4.0)
                    newResult = 3.5;
                else
                    newResult = 4.0;
            }

            return newResult;


        }



        private double convertScalePoints(string strNumber)
        {
            double newVal = 0;

            switch (strNumber)
            {
                case "7": newVal = 3; break;
                case "6.5": newVal = 2.5; break;
                case "6": newVal = 2; break;
                case "5.5": newVal = 1.5; break;
                case "5": newVal = 1; break;
                case "4.5": newVal = 0.5; break;
                case "4": newVal = 0; break;
                case "3.5": newVal = 0.5; break;
                case "3": newVal = 1; break;
                case "2.5": newVal = 1.5; break;
                case "2": newVal = 2; break;
                case "1.5": newVal = 2.5; break;
                case "1": newVal = 3; break;

            }

            return newVal;
        }

        private double round(double val)
        {
            double num = 0.0;

            if (val >= 6.75) { num = 7; }
            else if (val >= 6.25) { num = 6.5; }
            else if (val >= 5.75) { num = 6; }
            else if (val >= 5.25) { num = 5.5; }
            else if (val >= 4.75) { num = 5; }
            else if (val >= 4.25) { num = 4.5; }
            else if (val > 3.75) { num = 4; }
            else if (val > 3.25) { num = 3.5; }
            else if (val > 2.75) { num = 3; }
            else if (val > 2.25) { num = 2.5; }
            else if (val > 1.75) { num = 2; }
            else if (val > 1.25) { num = 1.5; }
            else if (val > 0.75) { num = 1; }
            //else if (val >= 0.25) { num = 1; }
            else num = 1;

            return num;
        }

        private double roundRueda(double val)
        {
            double num = 0.0;

            if (val >= 6.75) { num = 7; }
            else if (val >= 6.25) { num = 6.5; }
            else if (val >= 5.75) { num = 6; }
            else if (val >= 5.25) { num = 5.5; }
            else if (val >= 4.75) { num = 5; }
            else if (val >= 4.25) { num = 4.5; }
            else if (val > 3.75) { num = 4; }
            else if (val > 3.25) { num = 3.5; }
            else if (val > 2.75) { num = 3; }
            else if (val > 2.25) { num = 2.5; }
            else if (val > 1.75) { num = 2; }
            else if (val > 1.25) { num = 1.5; }
            else if (val > 0.75) { num = 1; }
            else num = 1;

            return num;
        }


        private List<string> GenerarGrafico_Extroversion_Introversion(WordprocessingDocument doc, Table table, List<PuntajeTemperamentoBE> lstPT)
        {
            string pathImages = Server.MapPath("~/Images/Temperamentos/");
            double dblPuntaje = calcularPuntajeSubSection("AMBIENTES DINAMICOS", "AMBIENTES TRANQUILOS", lstPT);
            string puntaje = round(dblPuntaje).ToString();
            puntaje_extroversion_vs_introversio_bar = dblPuntaje;

            //GenralReport-Variable
            dblAmbDinamic_AmbTranquilo = dblPuntaje;

            string imagen = pathImages + puntaje.Replace(".", "_") + ".png";

            List<string> lstElementos_resultado = new List<string>();
            ScaleNumbersEI.Add(Convert.ToDouble(puntaje));

            TableRow row;
            TableCell cell;
            switch (puntaje)
            {
                case "7":
                case "6.5":
                case "6":
                case "5.5":
                case "5":
                case "4.5":
                    row = table.Elements<TableRow>().ElementAt(1);
                    cell = row.Elements<TableCell>().ElementAt(1);
                    AddImage(cell, doc.MainDocumentPart, imagen, "imId1");
                    lstElementos_resultado.Add("AMBIENTES DINAMICOS");
                    break;
                case "4":
                    //Mostrar Barra hacia arriba
                    row = table.Elements<TableRow>().ElementAt(1);
                    cell = row.Elements<TableCell>().ElementAt(1);
                    AddImage(cell, doc.MainDocumentPart, imagen, "imId1");

                    //Mostrar barra hacia abajo
                    row = table.Elements<TableRow>().ElementAt(2);
                    cell = row.Elements<TableCell>().ElementAt(1);
                    AddImage(cell, doc.MainDocumentPart, imagen, "imId1_1");

                    lstElementos_resultado.Add("AMBIENTES DINAMICOS/AMBIENTES TRANQUILOS");
                    break;
                case "3.5":
                case "3":
                case "2.5":
                case "2":
                case "1.5":
                case "1":
                case "0.5":
                    //default:
                    row = table.Elements<TableRow>().ElementAt(2);
                    cell = row.Elements<TableCell>().ElementAt(1);
                    AddImage(cell, doc.MainDocumentPart, imagen, "imId1");
                    lstElementos_resultado.Add("AMBIENTES TRANQUILOS");
                    break;

            }

            dblPuntaje = calcularPuntajeSubSection("SOCIABLE", "INTIMO", lstPT);

            //General Report-Variable
            dblSociable_Intimo = dblPuntaje;

            puntaje = round(dblPuntaje).ToString();
            puntaje_extroversion_vs_introversio_bar = (puntaje_extroversion_vs_introversio_bar + dblPuntaje);

            imagen = pathImages + puntaje.Replace(".", "_") + ".png";
            ScaleNumbersEI.Add(Convert.ToDouble(puntaje));

            switch (puntaje)
            {
                case "7":
                case "6.5":
                case "6":
                case "5.5":
                case "5":
                case "4.5":
                    row = table.Elements<TableRow>().ElementAt(1);
                    cell = row.Elements<TableCell>().ElementAt(2);
                    AddImage(cell, doc.MainDocumentPart, imagen, "imId2");
                    lstElementos_resultado.Add("SOCIABLE");
                    break;
                case "4":
                    //Mostrar Barra hacia arriba
                    row = table.Elements<TableRow>().ElementAt(1);
                    cell = row.Elements<TableCell>().ElementAt(2);
                    AddImage(cell, doc.MainDocumentPart, imagen, "imId2");

                    //Mostrar Barra hacia abajo
                    row = table.Elements<TableRow>().ElementAt(2);
                    cell = row.Elements<TableCell>().ElementAt(2);
                    AddImage(cell, doc.MainDocumentPart, imagen, "imId2_1");

                    lstElementos_resultado.Add("SOCIABLE/INTIMO");
                    break;
                case "3.5":
                case "3":
                case "2.5":
                case "2":
                case "1.5":
                case "1":
                case "0.5":
                    //default:
                    row = table.Elements<TableRow>().ElementAt(2);
                    cell = row.Elements<TableCell>().ElementAt(2);
                    AddImage(cell, doc.MainDocumentPart, imagen, "imId2");
                    lstElementos_resultado.Add("INTIMO");
                    break;

            }

            dblPuntaje = calcularPuntajeSubSection("ENTUSIASTA", "CALMADO", lstPT);

            //GeneralReport-Variable
            dblEntusiasta_Calmado = dblPuntaje;

            puntaje = round(dblPuntaje).ToString();
            puntaje_extroversion_vs_introversio_bar = (puntaje_extroversion_vs_introversio_bar + dblPuntaje);

            imagen = pathImages + puntaje.Replace(".", "_") + ".png";
            ScaleNumbersEI.Add(Convert.ToDouble(puntaje));

            switch (puntaje)
            {
                case "7":
                case "6.5":
                case "6":
                case "5.5":
                case "5":
                case "4.5":
                    row = table.Elements<TableRow>().ElementAt(1);
                    cell = row.Elements<TableCell>().ElementAt(3);
                    AddImage(cell, doc.MainDocumentPart, imagen, "imId3");
                    lstElementos_resultado.Add("ENTUSIASTA");
                    break;
                case "4":
                    //Mostrar Barra hacia arriba
                    row = table.Elements<TableRow>().ElementAt(1);
                    cell = row.Elements<TableCell>().ElementAt(3);
                    AddImage(cell, doc.MainDocumentPart, imagen, "imId3");

                    //Mostrar Barra hacia abajo
                    row = table.Elements<TableRow>().ElementAt(2);
                    cell = row.Elements<TableCell>().ElementAt(3);
                    AddImage(cell, doc.MainDocumentPart, imagen, "imId3_1");

                    lstElementos_resultado.Add("ENTUSIASTA/CALMADO");
                    break;
                case "3.5":
                case "3":
                case "2.5":
                case "2":
                case "1.5":
                case "1":
                case "0.5":
                    //default:
                    row = table.Elements<TableRow>().ElementAt(2);
                    cell = row.Elements<TableCell>().ElementAt(3);
                    AddImage(cell, doc.MainDocumentPart, imagen, "imId3");
                    lstElementos_resultado.Add("CALMADO");
                    break;

            }

            dblPuntaje = calcularPuntajeSubSection("COMUNICATIVO", "RESERVADO", lstPT);

            //General-Variable
            dblComunicativo_Reservado = dblPuntaje;

            puntaje = round(dblPuntaje).ToString();
            puntaje_extroversion_vs_introversio_bar = (puntaje_extroversion_vs_introversio_bar + dblPuntaje);

            imagen = pathImages + puntaje.Replace(".", "_") + ".png";
            ScaleNumbersEI.Add(Convert.ToDouble(puntaje));

            switch (puntaje)
            {
                case "7":
                case "6.5":
                case "6":
                case "5.5":
                case "5":
                case "4.5":
                    row = table.Elements<TableRow>().ElementAt(1);
                    cell = row.Elements<TableCell>().ElementAt(4);
                    AddImage(cell, doc.MainDocumentPart, imagen, "imId4");
                    lstElementos_resultado.Add("COMUNICATIVO");
                    break;
                case "4":
                    //Mostrar Barra hacia arriba
                    row = table.Elements<TableRow>().ElementAt(1);
                    cell = row.Elements<TableCell>().ElementAt(4);
                    AddImage(cell, doc.MainDocumentPart, imagen, "imId4");

                    //Mostrar Barra hacia abajo
                    row = table.Elements<TableRow>().ElementAt(2);
                    cell = row.Elements<TableCell>().ElementAt(4);
                    AddImage(cell, doc.MainDocumentPart, imagen, "imId4_1");

                    lstElementos_resultado.Add("COMUNICATIVO/RESERVADO");
                    break;
                case "3.5":
                case "3":
                case "2.5":
                case "2":
                case "1.5":
                case "1":
                case "0.5":
                    //default:
                    row = table.Elements<TableRow>().ElementAt(2);
                    cell = row.Elements<TableCell>().ElementAt(4);
                    AddImage(cell, doc.MainDocumentPart, imagen, "imId4");
                    lstElementos_resultado.Add("RESERVADO");
                    break;
            }

            return lstElementos_resultado;

        }

        private List<string> GenerarGrafico_Intuicion_Sensacion(WordprocessingDocument doc, Table table, List<PuntajeTemperamentoBE> lstPT)
        {
            string pathImages = Server.MapPath("~/Images/Temperamentos/");

            double dblpuntajeIS = calcularPuntajeSubSection("INSTINTIVO", "ESCEPTICO", lstPT);

            //GeneralReport-Variable
            dblInstintivo_Esceptico = dblpuntajeIS;

            string puntajeIS = round(dblpuntajeIS).ToString();
            puntaje_intuicion_vs_sensacion_bar = dblpuntajeIS;

            string imagen = pathImages + puntajeIS.Replace(".", "_") + ".png";
            List<string> lstElementos_resultado = new List<string>();
            ScaleNumbersIS.Add(Convert.ToDouble(puntajeIS));

            TableRow rowIS;
            TableCell cellIS;

            switch (puntajeIS)
            {
                case "7":
                case "6.5":
                case "6":
                case "5.5":
                case "5":
                case "4.5":
                    rowIS = table.Elements<TableRow>().ElementAt(1);
                    cellIS = rowIS.Elements<TableCell>().ElementAt(1);
                    AddImage(cellIS, doc.MainDocumentPart, imagen, "imId5");
                    lstElementos_resultado.Add("INSTINTIVO");
                    break;

                case "4":
                    //Mostrar Barra hacia arriba
                    rowIS = table.Elements<TableRow>().ElementAt(1);
                    cellIS = rowIS.Elements<TableCell>().ElementAt(1);
                    AddImage(cellIS, doc.MainDocumentPart, imagen, "imId5");

                    //Mostrar Barra hacia abajo
                    rowIS = table.Elements<TableRow>().ElementAt(2);
                    cellIS = rowIS.Elements<TableCell>().ElementAt(1);
                    AddImage(cellIS, doc.MainDocumentPart, imagen, "imId5_1");

                    lstElementos_resultado.Add("INSTINTIVO/ESCEPTICO");
                    break;
                case "3.5":
                case "3":
                case "2.5":
                case "2":
                case "1.5":
                case "1":
                case "0.5":
                    //default:
                    rowIS = table.Elements<TableRow>().ElementAt(2);
                    cellIS = rowIS.Elements<TableCell>().ElementAt(1);
                    AddImage(cellIS, doc.MainDocumentPart, imagen, "imId5");
                    lstElementos_resultado.Add("ESCEPTICO");
                    break;

            }

            dblpuntajeIS = calcularPuntajeSubSection("ORIGINAL", "TRADICIONAL", lstPT);

            //GeneralReport-Variable
            dblOriginal_Tradicional = dblpuntajeIS;

            puntajeIS = round(dblpuntajeIS).ToString();
            puntaje_intuicion_vs_sensacion_bar = (puntaje_intuicion_vs_sensacion_bar + dblpuntajeIS);
            imagen = pathImages + puntajeIS.Replace(".", "_") + ".png";
            ScaleNumbersIS.Add(Convert.ToDouble(puntajeIS));

            switch (puntajeIS)
            {
                case "7":
                case "6.5":
                case "6":
                case "5.5":
                case "5":
                case "4.5":
                    rowIS = table.Elements<TableRow>().ElementAt(1);
                    cellIS = rowIS.Elements<TableCell>().ElementAt(2);
                    AddImage(cellIS, doc.MainDocumentPart, imagen, "imId20");
                    lstElementos_resultado.Add("ORIGINAL");
                    break;
                case "4":
                    //Mostrar Barra hacia arriba
                    rowIS = table.Elements<TableRow>().ElementAt(1);
                    cellIS = rowIS.Elements<TableCell>().ElementAt(2);
                    AddImage(cellIS, doc.MainDocumentPart, imagen, "imId20");

                    //Mostrar Barra hacia abajo
                    rowIS = table.Elements<TableRow>().ElementAt(2);
                    cellIS = rowIS.Elements<TableCell>().ElementAt(2);
                    AddImage(cellIS, doc.MainDocumentPart, imagen, "imId20_1");

                    lstElementos_resultado.Add("ORIGINAL/TRADICIONAL");
                    break;
                case "3.5":
                case "3":
                case "2.5":
                case "2":
                case "1.5":
                case "1":
                case "0.5":
                    //default:
                    rowIS = table.Elements<TableRow>().ElementAt(2);
                    cellIS = rowIS.Elements<TableCell>().ElementAt(2);
                    AddImage(cellIS, doc.MainDocumentPart, imagen, "imId20");
                    lstElementos_resultado.Add("TRADICIONAL");
                    break;

            }


            dblpuntajeIS = calcularPuntajeSubSection("CREATIVO", "REALISTA", lstPT);

            //GeneralReport-Varible
            dblCreativo_Realista = dblpuntajeIS;

            puntajeIS = round(dblpuntajeIS).ToString();
            puntaje_intuicion_vs_sensacion_bar = (puntaje_intuicion_vs_sensacion_bar + dblpuntajeIS);
            imagen = pathImages + puntajeIS.Replace(".", "_") + ".png";

            ScaleNumbersIS.Add(Convert.ToDouble(puntajeIS));

            switch (puntajeIS)
            {
                case "7":
                case "6.5":
                case "6":
                case "5.5":
                case "5":
                case "4.5":
                    rowIS = table.Elements<TableRow>().ElementAt(1);
                    cellIS = rowIS.Elements<TableCell>().ElementAt(3);
                    AddImage(cellIS, doc.MainDocumentPart, imagen, "imId21");
                    lstElementos_resultado.Add("CREATIVO");
                    break;
                case "4":
                    //Mostrar Barra hacia arriba
                    rowIS = table.Elements<TableRow>().ElementAt(1);
                    cellIS = rowIS.Elements<TableCell>().ElementAt(3);
                    AddImage(cellIS, doc.MainDocumentPart, imagen, "imId21");

                    //Mostrar Barra hacia abajo
                    rowIS = table.Elements<TableRow>().ElementAt(2);
                    cellIS = rowIS.Elements<TableCell>().ElementAt(3);
                    AddImage(cellIS, doc.MainDocumentPart, imagen, "imId21_1");

                    lstElementos_resultado.Add("CREATIVO/REALISTA");
                    break;
                case "3.5":
                case "3":
                case "2.5":
                case "2":
                case "1.5":
                case "1":
                case "0.5":
                    //default:
                    rowIS = table.Elements<TableRow>().ElementAt(2);
                    cellIS = rowIS.Elements<TableCell>().ElementAt(3);
                    AddImage(cellIS, doc.MainDocumentPart, imagen, "imId21");
                    lstElementos_resultado.Add("REALISTA");
                    break;

            }


            return lstElementos_resultado;

        }

        private List<string> GenerarGrafico_Racional_Emocional(WordprocessingDocument doc, Table table, List<PuntajeTemperamentoBE> lstPT)
        {
            string pathImages = Server.MapPath("~/Images/Temperamentos/");

            double dblPuntajeOC = calcularPuntajeSubSection("OBJETIVO", "COMPASIVO", lstPT);
            string puntajeOC = round(dblPuntajeOC).ToString();
            puntaje_racional_vs_emotivo_bar = dblPuntajeOC;

            //GeneralReport-Variable
            dblObjetivo_Compasivo = dblPuntajeOC;


            string imagen = pathImages + puntajeOC.Replace(".", "_") + ".png";
            List<string> lstElemento_resultado = new List<string>();
            ScaleNumbersRE.Add(Convert.ToDouble(puntajeOC));

            TableRow row;
            TableCell cell;
            switch (puntajeOC)
            {
                case "7":
                case "6.5":
                case "6":
                case "5.5":
                case "5":
                case "4.5":
                    row = table.Elements<TableRow>().ElementAt(1);
                    cell = row.Elements<TableCell>().ElementAt(1);
                    AddImage(cell, doc.MainDocumentPart, imagen, "imId6");
                    lstElemento_resultado.Add("OBJETIVO");
                    break;

                case "4":
                    //Mostrar Barra hacia arriba
                    row = table.Elements<TableRow>().ElementAt(1);
                    cell = row.Elements<TableCell>().ElementAt(1);
                    AddImage(cell, doc.MainDocumentPart, imagen, "imId6");

                    //Mostrar Barra hacia abajo
                    row = table.Elements<TableRow>().ElementAt(2);
                    cell = row.Elements<TableCell>().ElementAt(1);
                    AddImage(cell, doc.MainDocumentPart, imagen, "imId6_1");

                    lstElemento_resultado.Add("OBJETIVO/COMPASIVO");
                    break;
                case "3.5":
                case "3":
                case "2.5":
                case "2":
                case "1.5":
                case "1":
                case "0.5":
                    //default:
                    row = table.Elements<TableRow>().ElementAt(2);
                    cell = row.Elements<TableCell>().ElementAt(1);
                    AddImage(cell, doc.MainDocumentPart, imagen, "imId6");
                    lstElemento_resultado.Add("COMPASIVO");
                    break;

            }

            dblPuntajeOC = calcularPuntajeSubSection("DISTANTE", "SUSCEPTIBLE", lstPT);

            //GeneralReport-Variable
            dblDistante_Susceptible = dblPuntajeOC;

            puntajeOC = round(dblPuntajeOC).ToString();
            puntaje_racional_vs_emotivo_bar = (puntaje_racional_vs_emotivo_bar + dblPuntajeOC);
            imagen = pathImages + puntajeOC.Replace(".", "_") + ".png";
            ScaleNumbersRE.Add(Convert.ToDouble(puntajeOC));

            switch (puntajeOC)
            {
                case "7":
                case "6.5":
                case "6":
                case "5.5":
                case "5":
                case "4.5":
                    row = table.Elements<TableRow>().ElementAt(1);
                    cell = row.Elements<TableCell>().ElementAt(2);
                    AddImage(cell, doc.MainDocumentPart, imagen, "imId7");
                    lstElemento_resultado.Add("DISTANTE");
                    break;
                case "4":
                    //Mostrar Barra hacia arriba
                    row = table.Elements<TableRow>().ElementAt(1);
                    cell = row.Elements<TableCell>().ElementAt(2);
                    AddImage(cell, doc.MainDocumentPart, imagen, "imId7");

                    //Mostrar Barra hacia abajo
                    row = table.Elements<TableRow>().ElementAt(2);
                    cell = row.Elements<TableCell>().ElementAt(2);
                    AddImage(cell, doc.MainDocumentPart, imagen, "imId7_1");

                    lstElemento_resultado.Add("DISTANTE/SUSCEPTIBLE");
                    break;
                case "3.5":
                case "3":
                case "2.5":
                case "2":
                case "1.5":
                case "1":
                case "0.5":
                    //default:
                    row = table.Elements<TableRow>().ElementAt(2);
                    cell = row.Elements<TableCell>().ElementAt(2);
                    AddImage(cell, doc.MainDocumentPart, imagen, "imId7");
                    lstElemento_resultado.Add("SUSCEPTIBLE");
                    break;

            }



            dblPuntajeOC = calcularPuntajeSubSection("DIRECTO", "EMPATICO", lstPT);

            //GeneralReport-Variable
            dblDirecto_Empatico = dblPuntajeOC;

            puntajeOC = round(dblPuntajeOC).ToString();
            puntaje_racional_vs_emotivo_bar = (puntaje_racional_vs_emotivo_bar + dblPuntajeOC);
            imagen = pathImages + puntajeOC.Replace(".", "_") + ".png";
            ScaleNumbersRE.Add(Convert.ToDouble(puntajeOC));

            switch (puntajeOC)
            {
                case "7":
                case "6.5":
                case "6":
                case "5.5":
                case "5":
                case "4.5":
                    row = table.Elements<TableRow>().ElementAt(1);
                    cell = row.Elements<TableCell>().ElementAt(3);
                    AddImage(cell, doc.MainDocumentPart, imagen, "imId24");
                    lstElemento_resultado.Add("DIRECTO");
                    break;
                case "4":
                    row = table.Elements<TableRow>().ElementAt(1);
                    cell = row.Elements<TableCell>().ElementAt(3);
                    AddImage(cell, doc.MainDocumentPart, imagen, "imId24");

                    row = table.Elements<TableRow>().ElementAt(2);
                    cell = row.Elements<TableCell>().ElementAt(3);
                    AddImage(cell, doc.MainDocumentPart, imagen, "imId24_1");

                    lstElemento_resultado.Add("DIRECTO/EMPATICO");
                    break;
                case "3.5":
                case "3":
                case "2.5":
                case "2":
                case "1.5":
                case "1":
                case "0.5":
                    //default:
                    row = table.Elements<TableRow>().ElementAt(2);
                    cell = row.Elements<TableCell>().ElementAt(3);
                    AddImage(cell, doc.MainDocumentPart, imagen, "imId24");
                    lstElemento_resultado.Add("EMPATICO");
                    break;

            }

            return lstElemento_resultado;
        }

        private List<string> GenerarGrafico_Organizado_Casual(WordprocessingDocument doc, Table table, List<PuntajeTemperamentoBE> lstPT)
        {
            string pathImages = Server.MapPath("~/Images/Temperamentos/");

            double dblPuntajeOC = calcularPuntajeSubSection("PLANIFICADO", "ESPONTANEO", lstPT);
            string puntajeOC = round(dblPuntajeOC).ToString();
            puntaje_organizado_vs_casual_bar = dblPuntajeOC;

            //GenralReport-Variable
            dblPlanificado_Espontaneo = dblPuntajeOC;

            string imagen = pathImages + puntajeOC.Replace(".", "_") + ".png";
            List<string> lstElementos_resultado = new List<string>();
            ScaleNumbersOC.Add(Convert.ToDouble(puntajeOC));

            TableRow row;
            TableCell cell;
            switch (puntajeOC)
            {
                case "7":
                case "6.5":
                case "6":
                case "5.5":
                case "5":
                case "4.5":
                    row = table.Elements<TableRow>().ElementAt(1);
                    cell = row.Elements<TableCell>().ElementAt(1);
                    AddImage(cell, doc.MainDocumentPart, imagen, "imId8");
                    lstElementos_resultado.Add("PLANIFICADO");
                    break;
                case "4":
                    row = table.Elements<TableRow>().ElementAt(1);
                    cell = row.Elements<TableCell>().ElementAt(1);
                    AddImage(cell, doc.MainDocumentPart, imagen, "imId8");

                    row = table.Elements<TableRow>().ElementAt(2);
                    cell = row.Elements<TableCell>().ElementAt(1);
                    AddImage(cell, doc.MainDocumentPart, imagen, "imId8_1");

                    lstElementos_resultado.Add("PLANIFICADO/ESPONTANEO");
                    break;
                case "3.5":
                case "3":
                case "2.5":
                case "2":
                case "1.5":
                case "1":
                case "0.5":
                    //default:
                    row = table.Elements<TableRow>().ElementAt(2);
                    cell = row.Elements<TableCell>().ElementAt(1);
                    AddImage(cell, doc.MainDocumentPart, imagen, "imId8");
                    lstElementos_resultado.Add("ESPONTANEO");
                    break;

            }

            dblPuntajeOC = calcularPuntajeSubSection("METODICO", "EVENTUAL", lstPT);

            //GeneralReport-Variable
            dblMetodico_Eventual = dblPuntajeOC;

            puntajeOC = round(dblPuntajeOC).ToString();
            puntaje_organizado_vs_casual_bar = (puntaje_organizado_vs_casual_bar + dblPuntajeOC);
            imagen = pathImages + puntajeOC.Replace(".", "_") + ".png";
            ScaleNumbersOC.Add(Convert.ToDouble(puntajeOC));

            switch (puntajeOC)
            {
                case "7":
                case "6.5":
                case "6":
                case "5.5":
                case "5":
                case "4.5":
                    row = table.Elements<TableRow>().ElementAt(1);
                    cell = row.Elements<TableCell>().ElementAt(2);
                    AddImage(cell, doc.MainDocumentPart, imagen, "imId9");
                    lstElementos_resultado.Add("METODICO");
                    break;
                case "4":
                    row = table.Elements<TableRow>().ElementAt(1);
                    cell = row.Elements<TableCell>().ElementAt(2);
                    AddImage(cell, doc.MainDocumentPart, imagen, "imId9");

                    row = table.Elements<TableRow>().ElementAt(2);
                    cell = row.Elements<TableCell>().ElementAt(2);
                    AddImage(cell, doc.MainDocumentPart, imagen, "imId9_1");

                    lstElementos_resultado.Add("METODICO/EVENTUAL");
                    break;

                case "3.5":
                case "3":
                case "2.5":
                case "2":
                case "1.5":
                case "1":
                case "0.5":
                    //default:
                    row = table.Elements<TableRow>().ElementAt(2);
                    cell = row.Elements<TableCell>().ElementAt(2);
                    AddImage(cell, doc.MainDocumentPart, imagen, "imId9");
                    lstElementos_resultado.Add("EVENTUAL");
                    break;

            }

            dblPuntajeOC = calcularPuntajeSubSection("ESTRUCTURADA", "FLEXIBLE", lstPT);

            //GeneralReport-Varible
            dblEstructurado_Flexible = dblPuntajeOC;

            puntajeOC = round(dblPuntajeOC).ToString();
            puntaje_organizado_vs_casual_bar = (puntaje_organizado_vs_casual_bar + dblPuntajeOC);
            imagen = pathImages + puntajeOC.Replace(".", "_") + ".png";
            ScaleNumbersOC.Add(Convert.ToDouble(puntajeOC));

            switch (puntajeOC)
            {
                case "7":
                case "6.5":
                case "6":
                case "5.5":
                case "5":
                case "4.5":
                    row = table.Elements<TableRow>().ElementAt(1);
                    cell = row.Elements<TableCell>().ElementAt(3);
                    AddImage(cell, doc.MainDocumentPart, imagen, "imId25");
                    lstElementos_resultado.Add("ESTRUCTURADO");
                    break;

                case "4":
                    row = table.Elements<TableRow>().ElementAt(1);
                    cell = row.Elements<TableCell>().ElementAt(3);
                    AddImage(cell, doc.MainDocumentPart, imagen, "imId25");

                    row = table.Elements<TableRow>().ElementAt(2);
                    cell = row.Elements<TableCell>().ElementAt(3);
                    AddImage(cell, doc.MainDocumentPart, imagen, "imId25_1");
                    lstElementos_resultado.Add("ESTRUCTURADO/FLEXIBLE");
                    break;

                case "3.5":
                case "3":
                case "2.5":
                case "2":
                case "1.5":
                case "1":
                case "0.5":
                    //default:
                    row = table.Elements<TableRow>().ElementAt(2);
                    cell = row.Elements<TableCell>().ElementAt(3);
                    AddImage(cell, doc.MainDocumentPart, imagen, "imId25");
                    lstElementos_resultado.Add("FLEXIBLE");
                    break;

            }

            return lstElementos_resultado;

        }



        private double calcularPuntajeSubSection(string sub_section1, string sub_section2, List<PuntajeTemperamentoBE> lstPT)
        {
            int countLst = lstPT.Count();
            int cont = 0;
            string sub_section = "";
            double puntaje = 0;

            for (int i = 0; i < countLst; i++)
            {
                sub_section = lstPT[i].name_subsection.ToString();

                if (sub_section.Equals(sub_section1))
                {
                    puntaje += lstPT[i].puntaje_subsection;
                    cont++;
                }

                if (sub_section.Equals(sub_section2))
                {
                    puntaje += lstPT[i].puntaje_subsection;
                    cont++;
                }

                if (cont == 2)
                    break;

            }
            puntaje = puntaje / 2;
            //puntaje = round(puntaje);

            //return Convert.ToString(puntaje);
            return puntaje;
        }


        private double calcularPuntaje(string respuesta)
        {
            double puntaje = 0;
            switch (respuesta)
            {
                case "TOTALMENTE EN DESACUERDO": puntaje = 1;
                    break;
                case "MUY EN DESACUERDO": puntaje = 2;
                    break;
                case "EN DESACUERDO": puntaje = 3;
                    break;
                case "NI DE ACUERDO NI EN DESACUERDO": puntaje = 4;
                    break;
                case "DE ACUERDO": puntaje = 5;
                    break;
                case "MUY DE ACUERDO": puntaje = 6;
                    break;
                case "TOTALMENTE DE ACUERDO": puntaje = 7;
                    break;
                default: puntaje = 0;
                    break;
            }

            return puntaje;
        }


        private double calcularPuntajeInvertido(string respuesta)
        {
            double puntaje = 0;
            switch (respuesta)
            {
                case "TOTALMENTE EN DESACUERDO": puntaje = 7;
                    break;
                case "MUY EN DESACUERDO": puntaje = 6;
                    break;
                case "EN DESACUERDO": puntaje = 5;
                    break;
                case "NI DE ACUERDO NI EN DESACUERDO": puntaje = 4;
                    break;
                case "DE ACUERDO": puntaje = 3;
                    break;
                case "MUY DE ACUERDO": puntaje = 2;
                    break;
                case "TOTALMENTE DE ACUERDO": puntaje = 1;
                    break;
                default: puntaje = 0;
                    break;
            }

            return puntaje;
        }

        // To search and replace content in a document part.
        public static void SearchAndReplace(string fileName, string searchText, string newText)
        {
            using (WordprocessingDocument doc = WordprocessingDocument.Open(fileName, true))
            {
                var body = doc.MainDocumentPart.Document.Body;

                foreach (var text in body.Descendants<Text>())
                {
                    if (text.Text.Contains(searchText))
                    {
                        text.Text = text.Text.Replace(searchText, newText);
                        break;
                    }
                }
            }
        }

        public static void SearchAndReplace(WordprocessingDocument doc, string fileName, string searchText, string newText)
        {
            //using (WordprocessingDocument doc = WordprocessingDocument.Open(fileName, true))
            //{
            var body = doc.MainDocumentPart.Document.Body;

            foreach (var text in body.Descendants<Text>())
            {
                if (text.Text.Contains("#"))
                {
                    int a = 0;
                }
                if (text.Text.Contains(searchText))
                {
                    text.Text = text.Text.Replace(searchText, newText);
                    break;
                }
            }
            //}
        }

        private void EliminarArchivosTemporales(string rutaExcelChart, string rutaImagenChart)
        {
            if (System.IO.File.Exists(rutaExcelChart))
            {
                System.IO.File.Delete(rutaExcelChart);
            }

            if (System.IO.File.Exists(rutaImagenChart))
            {
                System.IO.File.Delete(rutaImagenChart);
            }
        }

        private void ConvertirWordToPDF(string fileToConvert, string nombreWord, string filePDF, string nombrePDF)
        {
            using (var client = new WebClient())
            {
                var data = new NameValueCollection();
                string strApiKey = ConfigurationManager.AppSettings["Apikey"].ToString();
                string strSecret = ConfigurationManager.AppSettings["ApiSecret"].ToString();
                data.Add("OutputFileName", nombrePDF); //Optional
                data.Add("ApiKey", strApiKey);
                string paso = "";

                try
                {
                    //client.QueryString.Add(data);
                    paso = "Antes Upload";
                    //var response = client.UploadFile("http://do.convertapi.com/Word2Pdf", fileToConvert);
                    client.Headers.Add("accept", "application/octet-stream");
                    var response = client.UploadFile("https://v2.convertapi.com/docx/to/pdf?Secret=" + strSecret, fileToConvert);
                    paso = "Upload Paso";
                    //var responseHeaders = client.ResponseHeaders;
                    var path = Path.Combine(filePDF, nombrePDF); /*responseHeaders["OutputFileName"]*/
                    File.WriteAllBytes(path, response);
                }
                catch (WebException ex)
                {
                    LogDALC objLogDALC = new LogDALC();
                    string mensaje = "Metodo: ConvertirWordToPDF. Archivo: " + fileToConvert + " (" + paso + "). Mensaje: " + ex.Message;
                    objLogDALC.InsertarLog(mensaje);

                    throw ex;
                }
            }

        }

        public static string GraphicDataUri = "http://schemas.openxmlformats.org/drawingml/2006/picture";
        public static void AddImage(TableCell body, MainDocumentPart mainpart, string filename, string SigId)
        {

            byte[] imageFileBytes;
            Bitmap image = null;

            // Open a stream on the image file and read it's contents.
            using (FileStream fsImageFile = System.IO.File.OpenRead(filename))
            {
                imageFileBytes = new byte[fsImageFile.Length];
                fsImageFile.Read(imageFileBytes, 0, imageFileBytes.Length);
                image = new Bitmap(fsImageFile);
            }
            long imageWidthEMU = (long)((image.Width / image.HorizontalResolution) * 914400L);
            long imageHeightEMU = (long)((image.Height / image.VerticalResolution) * 914400L);

            // add this is not already there
            try
            {
                if (mainpart.GetPartById(SigId) == null)
                {
                    var imagePart = mainpart.AddNewPart<ImagePart>("image/jpeg", SigId);

                    using (BinaryWriter writer = new BinaryWriter(imagePart.GetStream()))
                    {
                        writer.Write(imageFileBytes);
                        writer.Flush();
                    }
                }
            }
            catch // thrown if not found
            {
                var imagePart = mainpart.AddNewPart<ImagePart>("image/jpeg", SigId);

                using (BinaryWriter writer = new BinaryWriter(imagePart.GetStream()))
                {
                    writer.Write(imageFileBytes);
                    writer.Flush();
                }

            }
            Paragraph para =
            new Paragraph(
              new Run(
                new Drawing(
                  new wp.Inline(

                    new wp.Extent()
                    {
                        Cx = imageWidthEMU,
                        Cy = imageHeightEMU
                    },

                    new wp.EffectExtent()
                    {
                        LeftEdge = 19050L,
                        TopEdge = 0L,
                        RightEdge = 9525L,
                        BottomEdge = 0L
                    },

                    new wp.DocProperties()
                    {
                        Id = (UInt32Value)1U,
                        Name = "Inline Text Wrapping Picture",
                        Description = GraphicDataUri
                    },

                    new wp.NonVisualGraphicFrameDrawingProperties(
                      new a.GraphicFrameLocks() { NoChangeAspect = true }),

                    new a.Graphic(
                      new a.GraphicData(
                        new pic.Picture(

                          new pic.NonVisualPictureProperties(
                            new pic.NonVisualDrawingProperties()
                            {
                                Id = (UInt32Value)0U,
                                Name = filename
                            },
                            new pic.NonVisualPictureDrawingProperties()),

                          new pic.BlipFill(
                            new a.Blip() { Embed = SigId },
                            new a.Stretch(
                              new a.FillRectangle())),

                          new pic.ShapeProperties(
                            new a.Transform2D(
                              new a.Offset() { X = 0L, Y = 0L },
                              new a.Extents()
                              {
                                  Cx = imageWidthEMU,
                                  Cy = imageHeightEMU
                              }),

                            new a.PresetGeometry(
                              new a.AdjustValueList()
                            ) { Preset = a.ShapeTypeValues.Rectangle }))
                      ) { Uri = GraphicDataUri })
                  )
                  {
                      DistanceFromTop = (UInt32Value)0U,
                      DistanceFromBottom = (UInt32Value)0U,
                      DistanceFromLeft = (UInt32Value)0U,
                      DistanceFromRight = (UInt32Value)0U
                  }))

            );

            body.Append(para);
        }


        private void FormatearTablaResultadoChart(Table table, int[] orientaciones)
        {
            if (orientaciones[0] > 0)
            {
                TableRow row1 = table.Elements<TableRow>().ElementAt(2);
                TableCell cell1 = row1.Elements<TableCell>().ElementAt(0);
                Paragraph p1 = cell1.Elements<Paragraph>().First();
                Run r1 = p1.Elements<Run>().First();
                Text t1 = r1.Elements<Text>().First();
                t1.Text = (100 * orientaciones[0] / 12).ToString() + " %";
            }

            if (orientaciones[1] > 0)
            {
                TableRow row2 = table.Elements<TableRow>().ElementAt(2);
                TableCell cell2 = row2.Elements<TableCell>().ElementAt(2);
                Paragraph p2 = cell2.Elements<Paragraph>().First();
                Run r2 = p2.Elements<Run>().First();
                Text t2 = r2.Elements<Text>().First();
                t2.Text = (100 * orientaciones[1] / 12).ToString() + " %";
            }

            if (orientaciones[2] > 0)
            {
                TableRow row3 = table.Elements<TableRow>().ElementAt(2);
                TableCell cell3 = row3.Elements<TableCell>().ElementAt(4);
                Paragraph p3 = cell3.Elements<Paragraph>().First();
                Run r3 = p3.Elements<Run>().First();
                Text t3 = r3.Elements<Text>().First();
                t3.Text = (100 * orientaciones[2] / 12).ToString() + " %";
            }

            if (orientaciones[3] > 0)
            {
                TableRow row4 = table.Elements<TableRow>().ElementAt(2);
                TableCell cell4 = row4.Elements<TableCell>().ElementAt(6);
                Paragraph p4 = cell4.Elements<Paragraph>().First();
                Run r4 = p4.Elements<Run>().First();
                Text t4 = r4.Elements<Text>().First();
                t4.Text = (100 * orientaciones[3] / 12).ToString() + " %";
            }

            if (orientaciones[4] > 0)
            {
                TableRow row5 = table.Elements<TableRow>().ElementAt(2);
                TableCell cell5 = row5.Elements<TableCell>().ElementAt(8);
                Paragraph p5 = cell5.Elements<Paragraph>().First();
                Run r5 = p5.Elements<Run>().First();
                Text t5 = r5.Elements<Text>().First();
                t5.Text = (100 * orientaciones[4] / 12).ToString() + " %";
            }

            if (orientaciones[5] > 0)
            {
                TableRow row6 = table.Elements<TableRow>().ElementAt(2);
                TableCell cell6 = row6.Elements<TableCell>().ElementAt(10);
                Paragraph p6 = cell6.Elements<Paragraph>().First();
                Run r6 = p6.Elements<Run>().First();
                Text t6 = r6.Elements<Text>().First();
                t6.Text = (100 * orientaciones[5] / 12).ToString() + "%";
            }
        }

        private void FormatearTablaTalentosMasDesarrollados(Table table, List<TalentoComplexBE> lstTalentos, List<TalentoComplexBE> lstTalentosTE)
        {
            int countPersonas = 16;
            int countEmprendimiento = 16;
            int countInnovacion = 16;
            int countEstructuras = 16;
            int countPersuacion = 16;
            int countCognicion = 16;
            int countEspecifico = 16;

            try
            {
                for (int i = 0; i < lstTalentos.Count; i++)
                {
                    string nombreTalento = lstTalentos[i].nombre;

                    switch (lstTalentos[i].idTendencia)
                    {
                        case 1:
                            // Find the second row in the table.
                            TableRow row1 = table.Elements<TableRow>().ElementAt(countPersonas);

                            // Find the third cell in the row.
                            TableCell cell1 = row1.Elements<TableCell>().ElementAt(0);

                            // Find the first paragraph in the table cell.
                            Paragraph p1 = cell1.Elements<Paragraph>().First();

                            // Find the first run in the paragraph.
                            Run r1 = p1.Elements<Run>().First();

                            // Set the text for the run.
                            Text t1 = r1.Elements<Text>().First();
                            t1.Text = nombreTalento;

                            countPersonas -= 2;
                            break;
                        case 2:
                            // Find the second row in the table.
                            TableRow row2 = table.Elements<TableRow>().ElementAt(countEmprendimiento);

                            // Find the third cell in the row.
                            TableCell cell2 = row2.Elements<TableCell>().ElementAt(2);

                            // Find the first paragraph in the table cell.
                            Paragraph p2 = cell2.Elements<Paragraph>().First();

                            // Find the first run in the paragraph.
                            Run r2 = p2.Elements<Run>().First();

                            // Set the text for the run.
                            Text t2 = r2.Elements<Text>().First();
                            t2.Text = nombreTalento;

                            countEmprendimiento -= 2;
                            break;
                        case 3:
                            // Find the second row in the table.
                            TableRow row3 = table.Elements<TableRow>().ElementAt(countInnovacion);

                            // Find the third cell in the row.
                            TableCell cell3 = row3.Elements<TableCell>().ElementAt(4);

                            // Find the first paragraph in the table cell.
                            Paragraph p3 = cell3.Elements<Paragraph>().First();

                            // Find the first run in the paragraph.
                            Run r3 = p3.Elements<Run>().First();

                            // Set the text for the run.
                            Text t3 = r3.Elements<Text>().First();
                            t3.Text = nombreTalento;

                            countInnovacion -= 2;
                            break;
                        case 4:
                            // Find the second row in the table.
                            TableRow row4 = table.Elements<TableRow>().ElementAt(countEstructuras);

                            // Find the third cell in the row.
                            TableCell cell4 = row4.Elements<TableCell>().ElementAt(6);

                            // Find the first paragraph in the table cell.
                            Paragraph p4 = cell4.Elements<Paragraph>().First();

                            // Find the first run in the paragraph.
                            Run r4 = p4.Elements<Run>().First();

                            // Set the text for the run.
                            Text t4 = r4.Elements<Text>().First();
                            t4.Text = nombreTalento;

                            countEstructuras -= 2;
                            break;
                        case 5:
                            // Find the second row in the table.
                            TableRow row5 = table.Elements<TableRow>().ElementAt(countPersuacion);

                            // Find the third cell in the row.
                            TableCell cell5 = row5.Elements<TableCell>().ElementAt(8);

                            // Find the first paragraph in the table cell.
                            Paragraph p5 = cell5.Elements<Paragraph>().First();

                            // Find the first run in the paragraph.
                            Run r5 = p5.Elements<Run>().First();

                            // Set the text for the run.
                            Text t5 = r5.Elements<Text>().First();
                            t5.Text = nombreTalento;

                            countPersuacion -= 2;
                            break;
                        case 6:
                            // Find the second row in the table.
                            TableRow row6 = table.Elements<TableRow>().ElementAt(countCognicion);

                            // Find the third cell in the row.
                            TableCell cell6 = row6.Elements<TableCell>().ElementAt(10);

                            // Find the first paragraph in the table cell.
                            Paragraph p6 = cell6.Elements<Paragraph>().First();

                            // Find the first run in the paragraph.
                            Run r6 = p6.Elements<Run>().First();

                            // Set the text for the run.
                            Text t6 = r6.Elements<Text>().First();
                            t6.Text = nombreTalento;

                            countCognicion -= 2;
                            break;
                    }
                }

                for (int i = 0; i < lstTalentosTE.Count; i++)
                {
                    // Find the second row in the table.
                    TableRow row7 = table.Elements<TableRow>().ElementAt(countEspecifico);

                    // Find the third cell in the row.
                    TableCell cell7 = row7.Elements<TableCell>().ElementAt(12);

                    // Find the first paragraph in the table cell.
                    Paragraph p7 = cell7.Elements<Paragraph>().First();

                    // Find the first run in the paragraph.
                    Run r7 = p7.Elements<Run>().First();

                    // Set the text for the run.
                    Text t7 = r7.Elements<Text>().First();
                    t7.Text = lstTalentosTE[i].nombre;

                    countEspecifico -= 2;
                }

                // Blanquear celdas vacias
                for (int i = countPersonas; i >= 0; i -= 2)
                {
                    // Find the second row in the table.
                    TableRow row = table.Elements<TableRow>().ElementAt(i);

                    // Find the third cell in the row.
                    TableCell cell = row.Elements<TableCell>().ElementAt(0);

                    // Find the first paragraph in the table cell.
                    Paragraph p = cell.Elements<Paragraph>().First();

                    // Find the first run in the paragraph.
                    Run r = p.Elements<Run>().First();

                    // Set the text for the run.
                    Text t = r.Elements<Text>().First();
                    t.Text = "";

                    var tcp = new TableCellProperties(new TableCellWidth()
                    {
                        Type = TableWidthUnitValues.Dxa,
                        Width = "2000",
                    });
                    // Add cell shading.
                    var shading = new Shading()
                    {
                        Color = "auto",
                        Fill = "F9F9F9",
                        Val = ShadingPatternValues.Clear
                    };

                    tcp.Append(shading);
                    cell.Append(tcp);
                    cell.TableCellProperties.TableCellBorders.TopBorder.Color = "F9F9F9";
                    cell.TableCellProperties.TableCellBorders.TopBorder.Val = new EnumValue<BorderValues>(BorderValues.Thick);
                    //cell.TableCellProperties.TableCellBorders.BottomBorder.Color = "F9F9F9";
                    //cell.TableCellProperties.TableCellBorders.BottomBorder.Val = new EnumValue<BorderValues>(BorderValues.Thick);
                    cell.TableCellProperties.TableCellBorders.LeftBorder.Color = "F9F9F9";
                    cell.TableCellProperties.TableCellBorders.LeftBorder.Val = new EnumValue<BorderValues>(BorderValues.Thick);
                    //cell.TableCellProperties.TableCellBorders.RightBorder.Color = "F9F9F9";
                    //cell.TableCellProperties.TableCellBorders.RightBorder.Val = new EnumValue<BorderValues>(BorderValues.Thick);
                }

                for (int i = countEmprendimiento; i >= 0; i -= 2)
                {
                    // Find the second row in the table.
                    TableRow row = table.Elements<TableRow>().ElementAt(i);

                    // Find the third cell in the row.
                    TableCell cell = row.Elements<TableCell>().ElementAt(2);

                    // Find the first paragraph in the table cell.
                    Paragraph p = cell.Elements<Paragraph>().First();

                    // Find the first run in the paragraph.
                    Run r = p.Elements<Run>().First();

                    // Set the text for the run.
                    Text t = r.Elements<Text>().First();
                    t.Text = "";

                    var tcp = new TableCellProperties(new TableCellWidth()
                    {
                        Type = TableWidthUnitValues.Dxa,
                        Width = "2000"
                    });
                    // Add cell shading.
                    var shading = new Shading()
                    {
                        Color = "auto",
                        Fill = "F9F9F9",
                        Val = ShadingPatternValues.Clear
                    };

                    tcp.Append(shading);
                    cell.Append(tcp);
                    cell.TableCellProperties.TableCellBorders.TopBorder.Color = "F9F9F9";
                    cell.TableCellProperties.TableCellBorders.TopBorder.Val = new EnumValue<BorderValues>(BorderValues.Thick);
                    //cell.TableCellProperties.TableCellBorders.BottomBorder.Color = "F9F9F9";
                    //cell.TableCellProperties.TableCellBorders.BottomBorder.Val = new EnumValue<BorderValues>(BorderValues.Thick);
                    cell.TableCellProperties.TableCellBorders.LeftBorder.Color = "F9F9F9";
                    cell.TableCellProperties.TableCellBorders.LeftBorder.Val = new EnumValue<BorderValues>(BorderValues.Thick);
                    //cell.TableCellProperties.TableCellBorders.RightBorder.Color = "F9F9F9";
                    //cell.TableCellProperties.TableCellBorders.RightBorder.Val = new EnumValue<BorderValues>(BorderValues.Thick);
                }

                for (int i = countInnovacion; i >= 0; i -= 2)
                {
                    // Find the second row in the table.
                    TableRow row = table.Elements<TableRow>().ElementAt(i);

                    // Find the third cell in the row.
                    TableCell cell = row.Elements<TableCell>().ElementAt(4);

                    // Find the first paragraph in the table cell.
                    Paragraph p = cell.Elements<Paragraph>().First();

                    // Find the first run in the paragraph.
                    Run r = p.Elements<Run>().First();

                    // Set the text for the run.
                    Text t = r.Elements<Text>().First();
                    t.Text = "";

                    var tcp = new TableCellProperties(new TableCellWidth()
                    {
                        Type = TableWidthUnitValues.Dxa,
                        Width = "2000"
                    });
                    // Add cell shading.
                    var shading = new Shading()
                    {
                        Color = "auto",
                        Fill = "F9F9F9",
                        Val = ShadingPatternValues.Clear
                    };

                    tcp.Append(shading);
                    cell.Append(tcp);
                    cell.TableCellProperties.TableCellBorders.TopBorder.Color = "F9F9F9";
                    cell.TableCellProperties.TableCellBorders.TopBorder.Val = new EnumValue<BorderValues>(BorderValues.Thick);
                    //cell.TableCellProperties.TableCellBorders.BottomBorder.Color = "F9F9F9";
                    //cell.TableCellProperties.TableCellBorders.BottomBorder.Val = new EnumValue<BorderValues>(BorderValues.Thick);
                    cell.TableCellProperties.TableCellBorders.LeftBorder.Color = "F9F9F9";
                    cell.TableCellProperties.TableCellBorders.LeftBorder.Val = new EnumValue<BorderValues>(BorderValues.Thick);
                    //cell.TableCellProperties.TableCellBorders.RightBorder.Color = "F9F9F9";
                    //cell.TableCellProperties.TableCellBorders.RightBorder.Val = new EnumValue<BorderValues>(BorderValues.Thick);
                }

                for (int i = countEstructuras; i >= 0; i -= 2)
                {
                    // Find the second row in the table.
                    TableRow row = table.Elements<TableRow>().ElementAt(i);

                    // Find the third cell in the row.
                    TableCell cell = row.Elements<TableCell>().ElementAt(6);

                    // Find the first paragraph in the table cell.
                    Paragraph p = cell.Elements<Paragraph>().First();

                    // Find the first run in the paragraph.
                    Run r = p.Elements<Run>().First();

                    // Set the text for the run.
                    Text t = r.Elements<Text>().First();
                    t.Text = "";

                    var tcp = new TableCellProperties(new TableCellWidth()
                    {
                        Type = TableWidthUnitValues.Dxa,
                        Width = "2000"
                    });
                    // Add cell shading.
                    var shading = new Shading()
                    {
                        Color = "auto",
                        Fill = "F9F9F9",
                        Val = ShadingPatternValues.Clear
                    };

                    tcp.Append(shading);
                    cell.Append(tcp);
                    cell.TableCellProperties.TableCellBorders.TopBorder.Color = "F9F9F9";
                    cell.TableCellProperties.TableCellBorders.TopBorder.Val = new EnumValue<BorderValues>(BorderValues.Thick);
                    //cell.TableCellProperties.TableCellBorders.BottomBorder.Color = "F9F9F9";
                    //cell.TableCellProperties.TableCellBorders.BottomBorder.Val = new EnumValue<BorderValues>(BorderValues.Thick);
                    cell.TableCellProperties.TableCellBorders.LeftBorder.Color = "F9F9F9";
                    cell.TableCellProperties.TableCellBorders.LeftBorder.Val = new EnumValue<BorderValues>(BorderValues.Thick);
                    //cell.TableCellProperties.TableCellBorders.RightBorder.Color = "F9F9F9";
                    //cell.TableCellProperties.TableCellBorders.RightBorder.Val = new EnumValue<BorderValues>(BorderValues.Thick);
                }

                for (int i = countPersuacion; i >= 0; i -= 2)
                {
                    // Find the second row in the table.
                    TableRow row = table.Elements<TableRow>().ElementAt(i);

                    // Find the third cell in the row.
                    TableCell cell = row.Elements<TableCell>().ElementAt(8);

                    // Find the first paragraph in the table cell.
                    Paragraph p = cell.Elements<Paragraph>().First();

                    // Find the first run in the paragraph.
                    Run r = p.Elements<Run>().First();

                    // Set the text for the run.
                    Text t = r.Elements<Text>().First();
                    t.Text = "";

                    var tcp = new TableCellProperties(new TableCellWidth()
                    {
                        Type = TableWidthUnitValues.Dxa,
                        Width = "2000"
                    });
                    // Add cell shading.
                    var shading = new Shading()
                    {
                        Color = "auto",
                        Fill = "F9F9F9",
                        Val = ShadingPatternValues.Clear
                    };

                    tcp.Append(shading);
                    cell.Append(tcp);
                    cell.TableCellProperties.TableCellBorders.TopBorder.Color = "F9F9F9";
                    cell.TableCellProperties.TableCellBorders.TopBorder.Val = new EnumValue<BorderValues>(BorderValues.Thick);
                    //cell.TableCellProperties.TableCellBorders.BottomBorder.Color = "F9F9F9";
                    //cell.TableCellProperties.TableCellBorders.BottomBorder.Val = new EnumValue<BorderValues>(BorderValues.Thick);
                    cell.TableCellProperties.TableCellBorders.LeftBorder.Color = "F9F9F9";
                    cell.TableCellProperties.TableCellBorders.LeftBorder.Val = new EnumValue<BorderValues>(BorderValues.Thick);
                    //cell.TableCellProperties.TableCellBorders.RightBorder.Color = "F9F9F9";
                    //cell.TableCellProperties.TableCellBorders.RightBorder.Val = new EnumValue<BorderValues>(BorderValues.Thick);
                }

                for (int i = countCognicion; i >= 0; i -= 2)
                {
                    // Find the second row in the table.
                    TableRow row = table.Elements<TableRow>().ElementAt(i);

                    // Find the third cell in the row.
                    TableCell cell = row.Elements<TableCell>().ElementAt(10);

                    // Find the first paragraph in the table cell.
                    Paragraph p = cell.Elements<Paragraph>().First();

                    // Find the first run in the paragraph.
                    Run r = p.Elements<Run>().First();

                    // Set the text for the run.
                    Text t = r.Elements<Text>().First();
                    t.Text = "";

                    var tcp = new TableCellProperties(new TableCellWidth()
                    {
                        Type = TableWidthUnitValues.Dxa,
                        Width = "2000"
                    });
                    // Add cell shading.
                    var shading = new Shading()
                    {
                        Color = "auto",
                        Fill = "F9F9F9",
                        Val = ShadingPatternValues.Clear
                    };

                    tcp.Append(shading);
                    cell.Append(tcp);
                    cell.TableCellProperties.TableCellBorders.TopBorder.Color = "F9F9F9";
                    cell.TableCellProperties.TableCellBorders.TopBorder.Val = new EnumValue<BorderValues>(BorderValues.Thick);
                    //cell.TableCellProperties.TableCellBorders.BottomBorder.Color = "F9F9F9";
                    //cell.TableCellProperties.TableCellBorders.BottomBorder.Val = new EnumValue<BorderValues>(BorderValues.Thick);
                    cell.TableCellProperties.TableCellBorders.LeftBorder.Color = "F9F9F9";
                    cell.TableCellProperties.TableCellBorders.LeftBorder.Val = new EnumValue<BorderValues>(BorderValues.Thick);
                    //cell.TableCellProperties.TableCellBorders.RightBorder.Color = "F9F9F9";
                    //cell.TableCellProperties.TableCellBorders.RightBorder.Val = new EnumValue<BorderValues>(BorderValues.Thick);
                }

                for (int i = countEspecifico; i >= 0; i -= 2)
                {
                    // Find the second row in the table.
                    TableRow row = table.Elements<TableRow>().ElementAt(i);

                    // Find the third cell in the row.
                    TableCell cell = row.Elements<TableCell>().ElementAt(12);

                    // Find the first paragraph in the table cell.
                    Paragraph p = cell.Elements<Paragraph>().First();

                    // Find the first run in the paragraph.
                    Run r = p.Elements<Run>().First();

                    // Set the text for the run.
                    Text t = r.Elements<Text>().First();
                    t.Text = "";

                    var tcp = new TableCellProperties(new TableCellWidth()
                    {
                        Type = TableWidthUnitValues.Dxa,
                        Width = "2000"
                    });
                    // Add cell shading.
                    var shading = new Shading()
                    {
                        Color = "auto",
                        Fill = "F9F9F9",
                        Val = ShadingPatternValues.Clear
                    };

                    tcp.Append(shading);
                    cell.Append(tcp);
                    cell.TableCellProperties.TableCellBorders.TopBorder.Color = "F9F9F9";
                    cell.TableCellProperties.TableCellBorders.TopBorder.Val = new EnumValue<BorderValues>(BorderValues.Thick);
                    //cell.TableCellProperties.TableCellBorders.BottomBorder.Color = "F9F9F9";
                    //cell.TableCellProperties.TableCellBorders.BottomBorder.Val = new EnumValue<BorderValues>(BorderValues.Thick);
                    cell.TableCellProperties.TableCellBorders.LeftBorder.Color = "F9F9F9";
                    cell.TableCellProperties.TableCellBorders.LeftBorder.Val = new EnumValue<BorderValues>(BorderValues.Thick);
                    //cell.TableCellProperties.TableCellBorders.RightBorder.Color = "F9F9F9";
                    //cell.TableCellProperties.TableCellBorders.RightBorder.Val = new EnumValue<BorderValues>(BorderValues.Thick);
                }
            }
            catch (Exception ex)
            {
                LogDALC objLogDALC = new LogDALC();
                string mensaje = "Metodo: FormatearTablaTalentosMasDesarrollados (WS Reporte). Mensaje: " + ex.Message;
                objLogDALC.InsertarLog(mensaje);

                throw ex;
            }
        }


        private void ModifyChartSimplified(MainDocumentPart mainDocPart, string cellColumn, uint intRow, string cellValue, bool axisValue)
        {
            try
            {
                ChartPart c_p = mainDocPart.ChartParts.FirstOrDefault();
                var l = c_p.ChartSpace.Descendants<PieChartSeries>();
                PieChartSeries bs1 = c_p.ChartSpace.Descendants<PieChartSeries>().Where
                (s => string.Compare(s.InnerText, "Sheet1!$" + cellColumn + "$1", true) > 0).FirstOrDefault();
                if (axisValue)
                {
                    NumericValue nv1 = bs1.Descendants<NumericValue>().First();
                    nv1.Text = cellValue;
                }
                else
                {
                    DocumentFormat.OpenXml.Drawing.Charts.Values v1 =
                    bs1.Descendants<DocumentFormat.OpenXml.Drawing.Charts.Values>().FirstOrDefault();
                    NumericPoint np = v1.Descendants<NumericPoint>().ElementAt((int)intRow - 2);
                    NumericValue nv = np.Descendants<NumericValue>().First();
                    nv.Text = cellValue;
                }
            }
            catch
            {
                // Chart Element is not in a recognizable format. 
                // Most likely the defined Chart is  incorrect. Ignore the chart creation.
                return;
            }
        }


        /* interes */

        private List<PuntajeTotalInteresBE> CalculoPT(Table table,
            int countADM, int countAGR, int countART,
            int countCOM, int countCON, int countCUL, int countDEP, int countDIS, int countFIN, int countINF, int countJUR,
            int countMAR, int countMEC, int countMIN, int countPED, int countSAL, int countSOC, int countTRA, int countTUR)
        {
            List<PuntajeTotalInteresBE> lstPT = new List<PuntajeTotalInteresBE>();


            string celdaADM = "0";
            string celdaAGR = "0";
            string celdaART = "0";
            string celdaCOM = "0";
            string celdaCON = "0";
            string celdaCUL = "0";
            string celdaDEP = "0";
            string celdaDIS = "0";
            string celdaFIN = "0";
            string celdaINF = "0";
            string celdaJUR = "0";
            string celdaMAR = "0";
            string celdaMEC = "0";
            string celdaMIN = "0";
            string celdaPED = "0";
            string celdaSAL = "0";
            string celdaSOC = "0";
            string celdaTRA = "0";
            string celdaTUR = "0";


            PuntajeTotalInteresBE objADM = new PuntajeTotalInteresBE();
            objADM.Codigo = "ADM";
            objADM.Area = "Administración";

            #region ADM
            switch (countADM)
            {
                case 12:
                    celdaADM = "2";
                    break;
                case 13:
                    celdaADM = "4";
                    break;
                case 14:
                    celdaADM = "5";
                    break;
                case 15:
                    celdaADM = "6";
                    break;
                case 16:
                    celdaADM = "7";
                    break;
                case 17:
                    celdaADM = "10";
                    break;
                case 18:
                    celdaADM = "12";
                    break;
                case 19:
                    celdaADM = "14";
                    break;
                case 20:
                    celdaADM = "18";
                    break;
                case 21:
                    celdaADM = "21";
                    break;
                case 22:
                    celdaADM = "24";
                    break;
                case 23:
                    celdaADM = "27";
                    break;
                case 24:
                    celdaADM = "31";
                    break;
                case 25:
                    celdaADM = "34";
                    break;
                case 26:
                    celdaADM = "38";
                    break;
                case 27:
                    celdaADM = "43";
                    break;
                case 28:
                    celdaADM = "47";
                    break;
                case 29:
                    celdaADM = "53";
                    break;
                case 30:
                    celdaADM = "58";
                    break;
                case 31:
                    celdaADM = "63";
                    break;
                case 32:
                    celdaADM = "69";
                    break;
                case 33:
                    celdaADM = "74";
                    break;
                case 34:
                    celdaADM = "78";
                    break;
                case 35:
                    celdaADM = "82";
                    break;
                case 36:
                    celdaADM = "85";
                    break;
                case 37:
                    celdaADM = "88";
                    break;
                case 38:
                    celdaADM = "92";
                    break;
                case 39:
                    celdaADM = "94";
                    break;
                case 40:
                    celdaADM = "98";
                    break;


            }
            #endregion
            //if (!string.IsNullOrEmpty(celdaAGR))
            //{
            //    TableRow row = table.Elements<TableRow>().ElementAt(1);
            //    TableCell cell = row.Elements<TableCell>().ElementAt(4);
            //    Paragraph p = cell.Elements<Paragraph>().First();
            //    Run r = p.Elements<Run>().First();
            //    Text t = r.Elements<Text>().First();
            //    if (celdaAGR.Equals("0"))
            //        t.Text = "";
            //    else
            //        t.Text = celdaAGR;
            //}
            objADM.Puntaje = Convert.ToInt32(celdaADM);

            PuntajeTotalInteresBE objAGR = new PuntajeTotalInteresBE();
            objAGR.Codigo = "AGR";
            objAGR.Area = "Agraria";

            #region AGR
            switch (countAGR)
            {
                case 12:
                    celdaAGR = "5";
                    break;
                case 13:
                    celdaAGR = "12";
                    break;
                case 14:
                    celdaAGR = "15";
                    break;
                case 15:
                    celdaAGR = "19";
                    break;
                case 16:
                    celdaAGR = "22";
                    break;
                case 17:
                    celdaAGR = "26";
                    break;
                case 18:
                    celdaAGR = "30";
                    break;
                case 19:
                    celdaAGR = "34";
                    break;
                case 20:
                    celdaAGR = "41";
                    break;
                case 21:
                    celdaAGR = "49";
                    break;
                case 22:
                    celdaAGR = "54";
                    break;
                case 23:
                    celdaAGR = "60";
                    break;
                case 24:
                    celdaAGR = "65";
                    break;
                case 25:
                    celdaAGR = "69";
                    break;
                case 26:
                    celdaAGR = "73";
                    break;
                case 27:
                    celdaAGR = "78";
                    break;
                case 28:
                    celdaAGR = "81";
                    break;
                case 29:
                    celdaAGR = "84";
                    break;
                case 30:
                    celdaAGR = "87";
                    break;
                case 31:
                    celdaAGR = "90";
                    break;
                case 32:
                    celdaAGR = "94";
                    break;
                case 33:
                    celdaAGR = "96";
                    break;
                case 34:
                    celdaAGR = "97";
                    break;
                case 35:
                    celdaAGR = "98";
                    break;
                case 36:
                    celdaAGR = "98";
                    break;
                case 37:
                    celdaAGR = "99";
                    break;
                case 38:
                    celdaAGR = "99";
                    break;
                case 39:
                    celdaAGR = "99";
                    break;
                case 40:
                    celdaAGR = "100";
                    break;


            }
            #endregion
            //if (!string.IsNullOrEmpty(celdaAGR))
            //{
            //    TableRow row = table.Elements<TableRow>().ElementAt(1);
            //    TableCell cell = row.Elements<TableCell>().ElementAt(4);
            //    Paragraph p = cell.Elements<Paragraph>().First();
            //    Run r = p.Elements<Run>().First();
            //    Text t = r.Elements<Text>().First();
            //    if (celdaAGR.Equals("0"))
            //        t.Text = "";
            //    else
            //        t.Text = celdaAGR;
            //}
            objAGR.Puntaje = Convert.ToInt32(celdaAGR);

            PuntajeTotalInteresBE objART = new PuntajeTotalInteresBE();
            objART.Codigo = "ART";
            objART.Area = "Artística";

            #region ART
            switch (countART)
            {
                case 12:
                    celdaART = "1";
                    break;
                case 13:
                    celdaART = "3";
                    break;
                case 14:
                    celdaART = "5";
                    break;
                case 15:
                    celdaART = "6";
                    break;
                case 16:
                    celdaART = "8";
                    break;
                case 17:
                    celdaART = "9";
                    break;
                case 18:
                    celdaART = "12";
                    break;
                case 19:
                    celdaART = "15";
                    break;
                case 20:
                    celdaART = "18";
                    break;
                case 21:
                    celdaART = "21";
                    break;
                case 22:
                    celdaART = "24";
                    break;
                case 23:
                    celdaART = "29";
                    break;
                case 24:
                    celdaART = "33";
                    break;
                case 25:
                    celdaART = "36";
                    break;
                case 26:
                    celdaART = "41";
                    break;
                case 27:
                    celdaART = "46";
                    break;
                case 28:
                    celdaART = "51";
                    break;
                case 29:
                    celdaART = "57";
                    break;
                case 30:
                    celdaART = "62";
                    break;
                case 31:
                    celdaART = "67";
                    break;
                case 32:
                    celdaART = "72";
                    break;
                case 33:
                    celdaART = "77";
                    break;
                case 34:
                    celdaART = "81";
                    break;
                case 35:
                    celdaART = "85";
                    break;
                case 36:
                    celdaART = "88";
                    break;
                case 37:
                    celdaART = "90";
                    break;
                case 38:
                    celdaART = "93";
                    break;
                case 39:
                    celdaART = "96";
                    break;
                case 40:
                    celdaART = "98";
                    break;

            }
            #endregion
            //if (!string.IsNullOrEmpty(celdaART))
            //{
            //    TableRow row = table.Elements<TableRow>().ElementAt(2);
            //    TableCell cell = row.Elements<TableCell>().ElementAt(4);
            //    Paragraph p = cell.Elements<Paragraph>().First();
            //    Run r = p.Elements<Run>().First();
            //    Text t = r.Elements<Text>().First();
            //    if (celdaART.Equals("0"))
            //        t.Text = "";
            //    else
            //        t.Text = celdaART;
            //}
            objART.Puntaje = Convert.ToInt32(celdaART);

            PuntajeTotalInteresBE objCOM = new PuntajeTotalInteresBE();
            objCOM.Codigo = "COM";
            objCOM.Area = "Comunicación";

            #region COM
            switch (countCOM)
            {
                case 12:
                    celdaCOM = "1";
                    break;
                case 13:
                    celdaCOM = "2";
                    break;
                case 14:
                    celdaCOM = "3";
                    break;
                case 15:
                    celdaCOM = "5";
                    break;
                case 16:
                    celdaCOM = "7";
                    break;
                case 17:
                    celdaCOM = "10";
                    break;
                case 18:
                    celdaCOM = "13";
                    break;
                case 19:
                    celdaCOM = "16";
                    break;
                case 20:
                    celdaCOM = "20";
                    break;
                case 21:
                    celdaCOM = "24";
                    break;
                case 22:
                    celdaCOM = "28";
                    break;
                case 23:
                    celdaCOM = "34";
                    break;
                case 24:
                    celdaCOM = "39";
                    break;
                case 25:
                    celdaCOM = "46";
                    break;
                case 26:
                    celdaCOM = "52";
                    break;
                case 27:
                    celdaCOM = "57";
                    break;
                case 28:
                    celdaCOM = "62";
                    break;
                case 29:
                    celdaCOM = "67";
                    break;
                case 30:
                    celdaCOM = "72";
                    break;
                case 31:
                    celdaCOM = "77";
                    break;
                case 32:
                    celdaCOM = "82";
                    break;
                case 33:
                    celdaCOM = "87";
                    break;
                case 34:
                    celdaCOM = "89";
                    break;
                case 35:
                    celdaCOM = "92";
                    break;
                case 36:
                    celdaCOM = "94";
                    break;
                case 37:
                    celdaCOM = "96";
                    break;
                case 38:
                    celdaCOM = "97";
                    break;
                case 39:
                    celdaCOM = "99";
                    break;
                case 40:
                    celdaCOM = "100";
                    break;

            }
            #endregion
            //if (!string.IsNullOrEmpty(celdaCOM))
            //{
            //    TableRow row = table.Elements<TableRow>().ElementAt(3);
            //    TableCell cell = row.Elements<TableCell>().ElementAt(4);
            //    Paragraph p = cell.Elements<Paragraph>().First();
            //    Run r = p.Elements<Run>().First();
            //    Text t = r.Elements<Text>().First();
            //    if (celdaCOM.Equals("0"))
            //        t.Text = "";
            //    else
            //        t.Text = celdaCOM;
            //}
            objCOM.Puntaje = Convert.ToInt32(celdaCOM);

            PuntajeTotalInteresBE objCON = new PuntajeTotalInteresBE();
            objCON.Codigo = "CON";
            objCON.Area = "Construcción";

            #region CON
            switch (countCON)
            {
                case 12:
                    celdaCON = "8";
                    break;
                case 13:
                    celdaCON = "16";
                    break;
                case 14:
                    celdaCON = "19";
                    break;
                case 15:
                    celdaCON = "22";
                    break;
                case 16:
                    celdaCON = "27";
                    break;
                case 17:
                    celdaCON = "33";
                    break;
                case 18:
                    celdaCON = "37";
                    break;
                case 19:
                    celdaCON = "40";
                    break;
                case 20:
                    celdaCON = "47";
                    break;
                case 21:
                    celdaCON = "54";
                    break;
                case 22:
                    celdaCON = "58";
                    break;
                case 23:
                    celdaCON = "61";
                    break;
                case 24:
                    celdaCON = "64";
                    break;
                case 25:
                    celdaCON = "67";
                    break;
                case 26:
                    celdaCON = "70";
                    break;
                case 27:
                    celdaCON = "73";
                    break;
                case 28:
                    celdaCON = "75";
                    break;
                case 29:
                    celdaCON = "78";
                    break;
                case 30:
                    celdaCON = "79";
                    break;
                case 31:
                    celdaCON = "81";
                    break;
                case 32:
                    celdaCON = "84";
                    break;
                case 33:
                    celdaCON = "87";
                    break;
                case 34:
                    celdaCON = "88";
                    break;
                case 35:
                    celdaCON = "90";
                    break;
                case 36:
                    celdaCON = "91";
                    break;
                case 37:
                    celdaCON = "93";
                    break;
                case 38:
                    celdaCON = "94";
                    break;
                case 39:
                    celdaCON = "96";
                    break;
                case 40:
                    celdaCON = "98";
                    break;

            }
            #endregion
            //if (!string.IsNullOrEmpty(celdaCON))
            //{
            //    TableRow row = table.Elements<TableRow>().ElementAt(4);
            //    TableCell cell = row.Elements<TableCell>().ElementAt(4);
            //    Paragraph p = cell.Elements<Paragraph>().First();
            //    Run r = p.Elements<Run>().First();
            //    Text t = r.Elements<Text>().First();
            //    if (celdaCON.Equals("0"))
            //        t.Text = "";
            //    else
            //        t.Text = celdaCON;
            //}
            objCON.Puntaje = Convert.ToInt32(celdaCON);


            PuntajeTotalInteresBE objCUL = new PuntajeTotalInteresBE();
            objCUL.Codigo = "CUL";
            objCUL.Area = "Culinaria";

            #region CUL
            switch (countCUL)
            {
                case 12:
                    celdaCUL = "4";
                    break;
                case 13:
                    celdaCUL = "10";
                    break;
                case 14:
                    celdaCUL = "11";
                    break;
                case 15:
                    celdaCUL = "13";
                    break;
                case 16:
                    celdaCUL = "16";
                    break;
                case 17:
                    celdaCUL = "18";
                    break;
                case 18:
                    celdaCUL = "21";
                    break;
                case 19:
                    celdaCUL = "23";
                    break;
                case 20:
                    celdaCUL = "27";
                    break;
                case 21:
                    celdaCUL = "31";
                    break;
                case 22:
                    celdaCUL = "35";
                    break;
                case 23:
                    celdaCUL = "38";
                    break;
                case 24:
                    celdaCUL = "42";
                    break;
                case 25:
                    celdaCUL = "45";
                    break;
                case 26:
                    celdaCUL = "48";
                    break;
                case 27:
                    celdaCUL = "52";
                    break;
                case 28:
                    celdaCUL = "56";
                    break;
                case 29:
                    celdaCUL = "60";
                    break;
                case 30:
                    celdaCUL = "65";
                    break;
                case 31:
                    celdaCUL = "69";
                    break;
                case 32:
                    celdaCUL = "75";
                    break;
                case 33:
                    celdaCUL = "81";
                    break;
                case 34:
                    celdaCUL = "84";
                    break;
                case 35:
                    celdaCUL = "86";
                    break;
                case 36:
                    celdaCUL = "88";
                    break;
                case 37:
                    celdaCUL = "90";
                    break;
                case 38:
                    celdaCUL = "92";
                    break;
                case 39:
                    celdaCUL = "94";
                    break;
                case 40:
                    celdaCUL = "98";
                    break;

            }
            #endregion
            //if (!string.IsNullOrEmpty(celdaCON))
            //{
            //    TableRow row = table.Elements<TableRow>().ElementAt(4);
            //    TableCell cell = row.Elements<TableCell>().ElementAt(4);
            //    Paragraph p = cell.Elements<Paragraph>().First();
            //    Run r = p.Elements<Run>().First();
            //    Text t = r.Elements<Text>().First();
            //    if (celdaCON.Equals("0"))
            //        t.Text = "";
            //    else
            //        t.Text = celdaCON;
            //}
            objCUL.Puntaje = Convert.ToInt32(celdaCUL);

            PuntajeTotalInteresBE objDEP = new PuntajeTotalInteresBE();
            objDEP.Codigo = "DEP";
            objDEP.Area = "Deportiva";

            #region DEP
            switch (countDEP)
            {
                case 12:
                    celdaDEP = "5";
                    break;
                case 13:
                    celdaDEP = "12";
                    break;
                case 14:
                    celdaDEP = "17";
                    break;
                case 15:
                    celdaDEP = "21";
                    break;
                case 16:
                    celdaDEP = "25";
                    break;
                case 17:
                    celdaDEP = "28";
                    break;
                case 18:
                    celdaDEP = "31";
                    break;
                case 19:
                    celdaDEP = "36";
                    break;
                case 20:
                    celdaDEP = "42";
                    break;
                case 21:
                    celdaDEP = "48";
                    break;
                case 22:
                    celdaDEP = "52";
                    break;
                case 23:
                    celdaDEP = "55";
                    break;
                case 24:
                    celdaDEP = "60";
                    break;
                case 25:
                    celdaDEP = "63";
                    break;
                case 26:
                    celdaDEP = "66";
                    break;
                case 27:
                    celdaDEP = "69";
                    break;
                case 28:
                    celdaDEP = "71";
                    break;
                case 29:
                    celdaDEP = "74";
                    break;
                case 30:
                    celdaDEP = "77";
                    break;
                case 31:
                    celdaDEP = "80";
                    break;
                case 32:
                    celdaDEP = "84";
                    break;
                case 33:
                    celdaDEP = "86";
                    break;
                case 34:
                    celdaDEP = "89";
                    break;
                case 35:
                    celdaDEP = "91";
                    break;
                case 36:
                    celdaDEP = "92";
                    break;
                case 37:
                    celdaDEP = "94";
                    break;
                case 38:
                    celdaDEP = "96";
                    break;
                case 39:
                    celdaDEP = "98";
                    break;
                case 40:
                    celdaDEP = "99";
                    break;

            }
            #endregion
            //if (!string.IsNullOrEmpty(celdaDEP))
            //{
            //    TableRow row = table.Elements<TableRow>().ElementAt(5);
            //    TableCell cell = row.Elements<TableCell>().ElementAt(4);
            //    Paragraph p = cell.Elements<Paragraph>().First();
            //    Run r = p.Elements<Run>().First();
            //    Text t = r.Elements<Text>().First();
            //    if (celdaDEP.Equals("0"))
            //        t.Text = "";
            //    else
            //        t.Text = celdaDEP;
            //}
            objDEP.Puntaje = Convert.ToInt32(celdaDEP);

            PuntajeTotalInteresBE objDIS = new PuntajeTotalInteresBE();
            objDIS.Codigo = "DIS";
            objDIS.Area = "Diseño";

            #region DIS
            switch (countDIS)
            {
                case 12:
                    celdaDIS = "1";
                    break;
                case 13:
                    celdaDIS = "4";
                    break;
                case 14:
                    celdaDIS = "5";
                    break;
                case 15:
                    celdaDIS = "7";
                    break;
                case 16:
                    celdaDIS = "10";
                    break;
                case 17:
                    celdaDIS = "14";
                    break;
                case 18:
                    celdaDIS = "17";
                    break;
                case 19:
                    celdaDIS = "20";
                    break;
                case 20:
                    celdaDIS = "25";
                    break;
                case 21:
                    celdaDIS = "31";
                    break;
                case 22:
                    celdaDIS = "35";
                    break;
                case 23:
                    celdaDIS = "41";
                    break;
                case 24:
                    celdaDIS = "47";
                    break;
                case 25:
                    celdaDIS = "51";
                    break;
                case 26:
                    celdaDIS = "56";
                    break;
                case 27:
                    celdaDIS = "61";
                    break;
                case 28:
                    celdaDIS = "65";
                    break;
                case 29:
                    celdaDIS = "69";
                    break;
                case 30:
                    celdaDIS = "74";
                    break;
                case 31:
                    celdaDIS = "77";
                    break;
                case 32:
                    celdaDIS = "81";
                    break;
                case 33:
                    celdaDIS = "85";
                    break;
                case 34:
                    celdaDIS = "87";
                    break;
                case 35:
                    celdaDIS = "89";
                    break;
                case 36:
                    celdaDIS = "91";
                    break;
                case 37:
                    celdaDIS = "93";
                    break;
                case 38:
                    celdaDIS = "85";
                    break;
                case 39:
                    celdaDIS = "97";
                    break;
                case 40:
                    celdaDIS = "99";
                    break;

            }
            #endregion
            //if (!string.IsNullOrEmpty(celdaDIS))
            //{
            //    TableRow row = table.Elements<TableRow>().ElementAt(6);
            //    TableCell cell = row.Elements<TableCell>().ElementAt(4);
            //    Paragraph p = cell.Elements<Paragraph>().First();
            //    Run r = p.Elements<Run>().First();
            //    Text t = r.Elements<Text>().First();
            //    if (celdaDIS.Equals("0"))
            //        t.Text = "";
            //    else
            //        t.Text = celdaDIS;
            //}
            objDIS.Puntaje = Convert.ToInt32(celdaDIS);

            PuntajeTotalInteresBE objFIN = new PuntajeTotalInteresBE();
            objFIN.Codigo = "FIN";
            objFIN.Area = "Financiera";

            #region FIN
            switch (countFIN)
            {
                case 12:
                    celdaFIN = "2";
                    break;
                case 13:
                    celdaFIN = "5";
                    break;
                case 14:
                    celdaFIN = "8";
                    break;
                case 15:
                    celdaFIN = "11";
                    break;
                case 16:
                    celdaFIN = "13";
                    break;
                case 17:
                    celdaFIN = "16";
                    break;
                case 18:
                    celdaFIN = "19";
                    break;
                case 19:
                    celdaFIN = "22";
                    break;
                case 20:
                    celdaFIN = "27";
                    break;
                case 21:
                    celdaFIN = "32";
                    break;
                case 22:
                    celdaFIN = "36";
                    break;
                case 23:
                    celdaFIN = "41";
                    break;
                case 24:
                    celdaFIN = "45";
                    break;
                case 25:
                    celdaFIN = "49";
                    break;
                case 26:
                    celdaFIN = "53";
                    break;
                case 27:
                    celdaFIN = "57";
                    break;
                case 28:
                    celdaFIN = "60";
                    break;
                case 29:
                    celdaFIN = "65";
                    break;
                case 30:
                    celdaFIN = "70";
                    break;
                case 31:
                    celdaFIN = "74";
                    break;
                case 32:
                    celdaFIN = "79";
                    break;
                case 33:
                    celdaFIN = "85";
                    break;
                case 34:
                    celdaFIN = "87";
                    break;
                case 35:
                    celdaFIN = "90";
                    break;
                case 36:
                    celdaFIN = "92";
                    break;
                case 37:
                    celdaFIN = "94";
                    break;
                case 38:
                    celdaFIN = "96";
                    break;
                case 39:
                    celdaFIN = "98";
                    break;
                case 40:
                    celdaFIN = "99";
                    break;

            }
            #endregion
            //if (!string.IsNullOrEmpty(celdaFIN))
            //{
            //    TableRow row = table.Elements<TableRow>().ElementAt(7);
            //    TableCell cell = row.Elements<TableCell>().ElementAt(4);
            //    Paragraph p = cell.Elements<Paragraph>().First();
            //    Run r = p.Elements<Run>().First();
            //    Text t = r.Elements<Text>().First();
            //    if (celdaFIN.Equals("0"))
            //        t.Text = "";
            //    else
            //        t.Text = celdaFIN;
            //}
            objFIN.Puntaje = Convert.ToInt32(celdaFIN);

            PuntajeTotalInteresBE objINF = new PuntajeTotalInteresBE();
            objINF.Codigo = "INF";
            objINF.Area = "Informática";

            #region INF
            switch (countINF)
            {
                case 12:
                    celdaINF = "3";
                    break;
                case 13:
                    celdaINF = "7";
                    break;
                case 14:
                    celdaINF = "10";
                    break;
                case 15:
                    celdaINF = "12";
                    break;
                case 16:
                    celdaINF = "15";
                    break;
                case 17:
                    celdaINF = "20";
                    break;
                case 18:
                    celdaINF = "24";
                    break;
                case 19:
                    celdaINF = "30";
                    break;
                case 20:
                    celdaINF = "35";
                    break;
                case 21:
                    celdaINF = "41";
                    break;
                case 22:
                    celdaINF = "46";
                    break;
                case 23:
                    celdaINF = "51";
                    break;
                case 24:
                    celdaINF = "57";
                    break;
                case 25:
                    celdaINF = "61";
                    break;
                case 26:
                    celdaINF = "66";
                    break;
                case 27:
                    celdaINF = "70";
                    break;
                case 28:
                    celdaINF = "73";
                    break;
                case 29:
                    celdaINF = "77";
                    break;
                case 30:
                    celdaINF = "81";
                    break;
                case 31:
                    celdaINF = "85";
                    break;
                case 32:
                    celdaINF = "88";
                    break;
                case 33:
                    celdaINF = "91";
                    break;
                case 34:
                    celdaINF = "93";
                    break;
                case 35:
                    celdaINF = "95";
                    break;
                case 36:
                    celdaINF = "96";
                    break;
                case 37:
                    celdaINF = "97";
                    break;
                case 38:
                    celdaINF = "97";
                    break;
                case 39:
                    celdaINF = "98";
                    break;
                case 40:
                    celdaINF = "99";
                    break;


            }
            #endregion
            //if (!string.IsNullOrEmpty(celdaINF))
            //{
            //    TableRow row = table.Elements<TableRow>().ElementAt(8);
            //    TableCell cell = row.Elements<TableCell>().ElementAt(4);
            //    Paragraph p = cell.Elements<Paragraph>().First();
            //    Run r = p.Elements<Run>().First();
            //    Text t = r.Elements<Text>().First();
            //    if (celdaINF.Equals("0"))
            //        t.Text = "";
            //    else
            //        t.Text = celdaINF;
            //}
            objINF.Puntaje = Convert.ToInt32(celdaINF);

            PuntajeTotalInteresBE objJUR = new PuntajeTotalInteresBE();
            objJUR.Codigo = "JUR";
            objJUR.Area = "Jurídica";

            #region JUR
            switch (countJUR)
            {
                case 12:
                    celdaJUR = "1";
                    break;
                case 13:
                    celdaJUR = "3";
                    break;
                case 14:
                    celdaJUR = "4";
                    break;
                case 15:
                    celdaJUR = "6";
                    break;
                case 16:
                    celdaJUR = "8";
                    break;
                case 17:
                    celdaJUR = "10";
                    break;
                case 18:
                    celdaJUR = "13";
                    break;
                case 19:
                    celdaJUR = "16";
                    break;
                case 20:
                    celdaJUR = "19";
                    break;
                case 21:
                    celdaJUR = "23";
                    break;
                case 22:
                    celdaJUR = "28";
                    break;
                case 23:
                    celdaJUR = "33";
                    break;
                case 24:
                    celdaJUR = "39";
                    break;
                case 25:
                    celdaJUR = "44";
                    break;
                case 26:
                    celdaJUR = "50";
                    break;
                case 27:
                    celdaJUR = "55";
                    break;
                case 28:
                    celdaJUR = "60";
                    break;
                case 29:
                    celdaJUR = "66";
                    break;
                case 30:
                    celdaJUR = "71";
                    break;
                case 31:
                    celdaJUR = "76";
                    break;
                case 32:
                    celdaJUR = "81";
                    break;
                case 33:
                    celdaJUR = "86";
                    break;
                case 34:
                    celdaJUR = "89";
                    break;
                case 35:
                    celdaJUR = "92";
                    break;
                case 36:
                    celdaJUR = "93";
                    break;
                case 37:
                    celdaJUR = "95";
                    break;
                case 38:
                    celdaJUR = "96";
                    break;
                case 39:
                    celdaJUR = "97";
                    break;
                case 40:
                    celdaJUR = "99";
                    break;

            }
            #endregion
            //if (!string.IsNullOrEmpty(celdaJUR))
            //{
            //    TableRow row = table.Elements<TableRow>().ElementAt(9);
            //    TableCell cell = row.Elements<TableCell>().ElementAt(4);
            //    Paragraph p = cell.Elements<Paragraph>().First();
            //    Run r = p.Elements<Run>().First();
            //    Text t = r.Elements<Text>().First();
            //    if (celdaJUR.Equals("0"))
            //        t.Text = "";
            //    else
            //        t.Text = celdaJUR;
            //}
            objJUR.Puntaje = Convert.ToInt32(celdaJUR);



            PuntajeTotalInteresBE objMAR = new PuntajeTotalInteresBE();
            objMAR.Codigo = "MAR";
            objMAR.Area = "Marketing";

            #region MAR
            switch (countMAR)
            {
                case 12:
                    celdaMAR = "2";
                    break;
                case 13:
                    celdaMAR = "4";
                    break;
                case 14:
                    celdaMAR = "5";
                    break;
                case 15:
                    celdaMAR = "7";
                    break;
                case 16:
                    celdaMAR = "8";
                    break;
                case 17:
                    celdaMAR = "9";
                    break;
                case 18:
                    celdaMAR = "10";
                    break;
                case 19:
                    celdaMAR = "13";
                    break;
                case 20:
                    celdaMAR = "17";
                    break;
                case 21:
                    celdaMAR = "21";
                    break;
                case 22:
                    celdaMAR = "24";
                    break;
                case 23:
                    celdaMAR = "27";
                    break;
                case 24:
                    celdaMAR = "30";
                    break;
                case 25:
                    celdaMAR = "33";
                    break;
                case 26:
                    celdaMAR = "38";
                    break;
                case 27:
                    celdaMAR = "42";
                    break;
                case 28:
                    celdaMAR = "47";
                    break;
                case 29:
                    celdaMAR = "53";
                    break;
                case 30:
                    celdaMAR = "58";
                    break;
                case 31:
                    celdaMAR = "64";
                    break;
                case 32:
                    celdaMAR = "71";
                    break;
                case 33:
                    celdaMAR = "76";
                    break;
                case 34:
                    celdaMAR = "80";
                    break;
                case 35:
                    celdaMAR = "83";
                    break;
                case 36:
                    celdaMAR = "87";
                    break;
                case 37:
                    celdaMAR = "90";
                    break;
                case 38:
                    celdaMAR = "92";
                    break;
                case 39:
                    celdaMAR = "94";
                    break;
                case 40:
                    celdaMAR = "98";
                    break;

            }
            #endregion
            //if (!string.IsNullOrEmpty(celdaJUR))
            //{
            //    TableRow row = table.Elements<TableRow>().ElementAt(9);
            //    TableCell cell = row.Elements<TableCell>().ElementAt(4);
            //    Paragraph p = cell.Elements<Paragraph>().First();
            //    Run r = p.Elements<Run>().First();
            //    Text t = r.Elements<Text>().First();
            //    if (celdaJUR.Equals("0"))
            //        t.Text = "";
            //    else
            //        t.Text = celdaJUR;
            //}
            objMAR.Puntaje = Convert.ToInt32(celdaMAR);


            PuntajeTotalInteresBE objMEC = new PuntajeTotalInteresBE();
            objMEC.Codigo = "MEC";
            objMEC.Area = "Mecanico";

            #region MEC
            switch (countMEC)
            {
                case 12:
                    celdaMEC = "8";
                    break;
                case 13:
                    celdaMEC = "18";
                    break;
                case 14:
                    celdaMEC = "24";
                    break;
                case 15:
                    celdaMEC = "29";
                    break;
                case 16:
                    celdaMEC = "33";
                    break;
                case 17:
                    celdaMEC = "37";
                    break;
                case 18:
                    celdaMEC = "42";
                    break;
                case 19:
                    celdaMEC = "46";
                    break;
                case 20:
                    celdaMEC = "53";
                    break;
                case 21:
                    celdaMEC = "59";
                    break;
                case 22:
                    celdaMEC = "62";
                    break;
                case 23:
                    celdaMEC = "65";
                    break;
                case 24:
                    celdaMEC = "69";
                    break;
                case 25:
                    celdaMEC = "72";
                    break;
                case 26:
                    celdaMEC = "75";
                    break;
                case 27:
                    celdaMEC = "78";
                    break;
                case 28:
                    celdaMEC = "80";
                    break;
                case 29:
                    celdaMEC = "83";
                    break;
                case 30:
                    celdaMEC = "86";
                    break;
                case 31:
                    celdaMEC = "89";
                    break;
                case 32:
                    celdaMEC = "91";
                    break;
                case 33:
                    celdaMEC = "93";
                    break;
                case 34:
                    celdaMEC = "94";
                    break;
                case 35:
                    celdaMEC = "95";
                    break;
                case 36:
                    celdaMEC = "96";
                    break;
                case 37:
                    celdaMEC = "97";
                    break;
                case 38:
                    celdaMEC = "98";
                    break;
                case 39:
                    celdaMEC = "98";
                    break;
                case 40:
                    celdaMEC = "99";
                    break;

            }
            #endregion
            //if (!string.IsNullOrEmpty(celdaJUR))
            //{
            //    TableRow row = table.Elements<TableRow>().ElementAt(9);
            //    TableCell cell = row.Elements<TableCell>().ElementAt(4);
            //    Paragraph p = cell.Elements<Paragraph>().First();
            //    Run r = p.Elements<Run>().First();
            //    Text t = r.Elements<Text>().First();
            //    if (celdaJUR.Equals("0"))
            //        t.Text = "";
            //    else
            //        t.Text = celdaJUR;
            //}
            objMEC.Puntaje = Convert.ToInt32(celdaMEC);


            PuntajeTotalInteresBE objMIN = new PuntajeTotalInteresBE();
            objMIN.Codigo = "MIN";
            objMIN.Area = "Minera";

            #region MIN
            switch (countMIN)
            {
                case 12:
                    celdaMIN = "10";
                    break;
                case 13:
                    celdaMIN = "23";
                    break;
                case 14:
                    celdaMIN = "28";
                    break;
                case 15:
                    celdaMIN = "33";
                    break;
                case 16:
                    celdaMIN = "38";
                    break;
                case 17:
                    celdaMIN = "43";
                    break;
                case 18:
                    celdaMIN = "48";
                    break;
                case 19:
                    celdaMIN = "52";
                    break;
                case 20:
                    celdaMIN = "58";
                    break;
                case 21:
                    celdaMIN = "65";
                    break;
                case 22:
                    celdaMIN = "68";
                    break;
                case 23:
                    celdaMIN = "71";
                    break;
                case 24:
                    celdaMIN = "73";
                    break;
                case 25:
                    celdaMIN = "75";
                    break;
                case 26:
                    celdaMIN = "78";
                    break;
                case 27:
                    celdaMIN = "80";
                    break;
                case 28:
                    celdaMIN = "82";
                    break;
                case 29:
                    celdaMIN = "85";
                    break;
                case 30:
                    celdaMIN = "86";
                    break;
                case 31:
                    celdaMIN = "88";
                    break;
                case 32:
                    celdaMIN = "90";
                    break;
                case 33:
                    celdaMIN = "92";
                    break;
                case 34:
                    celdaMIN = "93";
                    break;
                case 35:
                    celdaMIN = "94";
                    break;
                case 36:
                    celdaMIN = "95";
                    break;
                case 37:
                    celdaMIN = "96";
                    break;
                case 38:
                    celdaMIN = "97";
                    break;
                case 39:
                    celdaMIN = "98";
                    break;
                case 40:
                    celdaMIN = "99";
                    break;

            }
            #endregion
            //if (!string.IsNullOrEmpty(celdaMIN))
            //{
            //    TableRow row = table.Elements<TableRow>().ElementAt(10);
            //    TableCell cell = row.Elements<TableCell>().ElementAt(4);
            //    Paragraph p = cell.Elements<Paragraph>().First();
            //    Run r = p.Elements<Run>().First();
            //    Text t = r.Elements<Text>().First();
            //    if (celdaMIN.Equals("0"))
            //        t.Text = "";
            //    else
            //        t.Text = celdaMIN;
            //}
            objMIN.Puntaje = Convert.ToInt32(celdaMIN);

            PuntajeTotalInteresBE objPED = new PuntajeTotalInteresBE();
            objPED.Codigo = "PED";
            objPED.Area = "Pedagogía";

            #region PED
            switch (countPED)
            {
                case 12:
                    celdaPED = "2";
                    break;
                case 13:
                    celdaPED = "4";
                    break;
                case 14:
                    celdaPED = "5";
                    break;
                case 15:
                    celdaPED = "6";
                    break;
                case 16:
                    celdaPED = "8";
                    break;
                case 17:
                    celdaPED = "10";
                    break;
                case 18:
                    celdaPED = "12";
                    break;
                case 19:
                    celdaPED = "16";
                    break;
                case 20:
                    celdaPED = "20";
                    break;
                case 21:
                    celdaPED = "26";
                    break;
                case 22:
                    celdaPED = "29";
                    break;
                case 23:
                    celdaPED = "32";
                    break;
                case 24:
                    celdaPED = "37";
                    break;
                case 25:
                    celdaPED = "41";
                    break;
                case 26:
                    celdaPED = "45";
                    break;
                case 27:
                    celdaPED = "51";
                    break;
                case 28:
                    celdaPED = "56";
                    break;
                case 29:
                    celdaPED = "61";
                    break;
                case 30:
                    celdaPED = "66";
                    break;
                case 31:
                    celdaPED = "71";
                    break;
                case 32:
                    celdaPED = "78";
                    break;
                case 33:
                    celdaPED = "82";
                    break;
                case 34:
                    celdaPED = "86";
                    break;
                case 35:
                    celdaPED = "89";
                    break;
                case 36:
                    celdaPED = "91";
                    break;
                case 37:
                    celdaPED = "93";
                    break;
                case 38:
                    celdaPED = "95";
                    break;
                case 39:
                    celdaPED = "96";
                    break;
                case 40:
                    celdaPED = "98";
                    break;

            }
            #endregion
            //if (!string.IsNullOrEmpty(celdaPED))
            //{
            //    TableRow row = table.Elements<TableRow>().ElementAt(11);
            //    TableCell cell = row.Elements<TableCell>().ElementAt(4);
            //    Paragraph p = cell.Elements<Paragraph>().First();
            //    Run r = p.Elements<Run>().First();
            //    Text t = r.Elements<Text>().First();
            //    if (celdaPED.Equals("0"))
            //        t.Text = "";
            //    else
            //        t.Text = celdaPED;
            //}
            objPED.Puntaje = Convert.ToInt32(celdaPED);

            PuntajeTotalInteresBE objSAL = new PuntajeTotalInteresBE();
            objSAL.Codigo = "SAL";
            objSAL.Area = "Salud";

            #region SAL
            switch (countSAL)
            {
                case 12:
                    celdaSAL = "2";
                    break;
                case 13:
                    celdaSAL = "5";
                    break;
                case 14:
                    celdaSAL = "7";
                    break;
                case 15:
                    celdaSAL = "10";
                    break;
                case 16:
                    celdaSAL = "13";
                    break;
                case 17:
                    celdaSAL = "17";
                    break;
                case 18:
                    celdaSAL = "21";
                    break;
                case 19:
                    celdaSAL = "25";
                    break;
                case 20:
                    celdaSAL = "30";
                    break;
                case 21:
                    celdaSAL = "36";
                    break;
                case 22:
                    celdaSAL = "41";
                    break;
                case 23:
                    celdaSAL = "46";
                    break;
                case 24:
                    celdaSAL = "51";
                    break;
                case 25:
                    celdaSAL = "57";
                    break;
                case 26:
                    celdaSAL = "62";
                    break;
                case 27:
                    celdaSAL = "67";
                    break;
                case 28:
                    celdaSAL = "71";
                    break;
                case 29:
                    celdaSAL = "75";
                    break;
                case 30:
                    celdaSAL = "79";
                    break;
                case 31:
                    celdaSAL = "83";
                    break;
                case 32:
                    celdaSAL = "87";
                    break;
                case 33:
                    celdaSAL = "91";
                    break;
                case 34:
                    celdaSAL = "94";
                    break;
                case 35:
                    celdaSAL = "95";
                    break;
                case 36:
                    celdaSAL = "97";
                    break;
                case 37:
                    celdaSAL = "98";
                    break;
                case 38:
                case 39:
                    celdaSAL = "99";
                    break;
                case 40:
                    celdaSAL = "100";
                    break;

            }
            #endregion
            //if (!string.IsNullOrEmpty(celdaSAL))
            //{
            //    TableRow row = table.Elements<TableRow>().ElementAt(12);
            //    TableCell cell = row.Elements<TableCell>().ElementAt(4);
            //    Paragraph p = cell.Elements<Paragraph>().First();
            //    Run r = p.Elements<Run>().First();
            //    Text t = r.Elements<Text>().First();
            //    if (celdaSAL.Equals("0"))
            //        t.Text = "";
            //    else
            //        t.Text = celdaSAL;
            //}
            objSAL.Puntaje = Convert.ToInt32(celdaSAL);



            PuntajeTotalInteresBE objSOC = new PuntajeTotalInteresBE();
            objSOC.Codigo = "SOC";
            objSOC.Area = "Social";

            #region SOC
            switch (countSOC)
            {
                case 12:
                    celdaSOC = "2";
                    break;
                case 13:
                    celdaSOC = "4";
                    break;
                case 14:
                    celdaSOC = "5";
                    break;
                case 15:
                    celdaSOC = "6";
                    break;
                case 16:
                    celdaSOC = "7";
                    break;
                case 17:
                    celdaSOC = "8";
                    break;
                case 18:
                    celdaSOC = "10";
                    break;
                case 19:
                    celdaSOC = "12";
                    break;
                case 20:
                    celdaSOC = "15";
                    break;
                case 21:
                    celdaSOC = "18";
                    break;
                case 22:
                    celdaSOC = "21";
                    break;
                case 23:
                    celdaSOC = "24";
                    break;
                case 24:
                    celdaSOC = "28";
                    break;
                case 25:
                    celdaSOC = "32";
                    break;
                case 26:
                    celdaSOC = "36";
                    break;
                case 27:
                    celdaSOC = "40";
                    break;
                case 28:
                    celdaSOC = "44";
                    break;
                case 29:
                    celdaSOC = "48";
                    break;
                case 30:
                    celdaSOC = "53";
                    break;
                case 31:
                    celdaSOC = "59";
                    break;
                case 32:
                    celdaSOC = "67";
                    break;
                case 33:
                    celdaSOC = "75";
                    break;
                case 34:
                    celdaSOC = "79";
                    break;
                case 35:
                    celdaSOC = "83";
                    break;
                case 36:
                    celdaSOC = "87";
                    break;
                case 37:
                    celdaSOC = "89";
                    break;
                case 38:
                    celdaSOC = "91";
                    break;
                case 39:
                    celdaSOC = "94";
                    break;
                case 40:
                    celdaSOC = "97";
                    break;

            }
            #endregion
            //if (!string.IsNullOrEmpty(celdaSAL))
            //{
            //    TableRow row = table.Elements<TableRow>().ElementAt(12);
            //    TableCell cell = row.Elements<TableCell>().ElementAt(4);
            //    Paragraph p = cell.Elements<Paragraph>().First();
            //    Run r = p.Elements<Run>().First();
            //    Text t = r.Elements<Text>().First();
            //    if (celdaSAL.Equals("0"))
            //        t.Text = "";
            //    else
            //        t.Text = celdaSAL;
            //}
            objSOC.Puntaje = Convert.ToInt32(celdaSOC);


            PuntajeTotalInteresBE objTRA = new PuntajeTotalInteresBE();
            objTRA.Codigo = "TRA";
            objTRA.Area = "Traducción";

            #region TRA
            switch (countTRA)
            {
                case 12:
                    celdaTRA = "2";
                    break;
                case 13:
                    celdaTRA = "5";
                    break;
                case 14:
                    celdaTRA = "7";
                    break;
                case 15:
                    celdaTRA = "10";
                    break;
                case 16:
                    celdaTRA = "12";
                    break;
                case 17:
                    celdaTRA = "15";
                    break;
                case 18:
                    celdaTRA = "19";
                    break;
                case 19:
                    celdaTRA = "23";
                    break;
                case 20:
                    celdaTRA = "28";
                    break;
                case 21:
                    celdaTRA = "35";
                    break;
                case 22:
                    celdaTRA = "41";
                    break;
                case 23:
                    celdaTRA = "47";
                    break;
                case 24:
                    celdaTRA = "52";
                    break;
                case 25:
                    celdaTRA = "57";
                    break;
                case 26:
                    celdaTRA = "61";
                    break;
                case 27:
                    celdaTRA = "65";
                    break;
                case 28:
                    celdaTRA = "69";
                    break;
                case 29:
                    celdaTRA = "73";
                    break;
                case 30:
                    celdaTRA = "76";
                    break;
                case 31:
                    celdaTRA = "80";
                    break;
                case 32:
                    celdaTRA = "84";
                    break;
                case 33:
                    celdaTRA = "87";
                    break;
                case 34:
                    celdaTRA = "89";
                    break;
                case 35:
                    celdaTRA = "91";
                    break;
                case 36:
                    celdaTRA = "92";
                    break;
                case 37:
                    celdaTRA = "94";
                    break;
                case 38:
                    celdaTRA = "96";
                    break;
                case 39:
                    celdaTRA = "97";
                    break;
                case 40:
                    celdaTRA = "99";
                    break;

            }
            #endregion
            //if (!string.IsNullOrEmpty(celdaTRA))
            //{
            //    TableRow row = table.Elements<TableRow>().ElementAt(13);
            //    TableCell cell = row.Elements<TableCell>().ElementAt(4);
            //    Paragraph p = cell.Elements<Paragraph>().First();
            //    Run r = p.Elements<Run>().First();
            //    Text t = r.Elements<Text>().First();
            //    if (celdaTRA.Equals("0"))
            //        t.Text = "";
            //    else
            //        t.Text = celdaTRA;
            //}
            objTRA.Puntaje = Convert.ToInt32(celdaTRA);

            PuntajeTotalInteresBE objTUR = new PuntajeTotalInteresBE();
            objTUR.Codigo = "TUR";
            objTUR.Area = "Turismo";

            #region TUR
            switch (countTUR)
            {
                case 12:
                    celdaTUR = "2";
                    break;
                case 13:
                    celdaTUR = "5";
                    break;
                case 14:
                    celdaTUR = "6";
                    break;
                case 15:
                    celdaTUR = "8";
                    break;
                case 16:
                    celdaTUR = "9";
                    break;
                case 17:
                    celdaTUR = "11";
                    break;
                case 18:
                    celdaTUR = "14";
                    break;
                case 19:
                    celdaTUR = "17";
                    break;
                case 20:
                    celdaTUR = "22";
                    break;
                case 21:
                    celdaTUR = "27";
                    break;
                case 22:
                    celdaTUR = "31";
                    break;
                case 23:
                    celdaTUR = "35";
                    break;
                case 24:
                    celdaTUR = "39";
                    break;
                case 25:
                    celdaTUR = "44";
                    break;
                case 26:
                    celdaTUR = "48";
                    break;
                case 27:
                    celdaTUR = "53";
                    break;
                case 28:
                    celdaTUR = "58";
                    break;
                case 29:
                    celdaTUR = "63";
                    break;
                case 30:
                    celdaTUR = "68";
                    break;
                case 31:
                    celdaTUR = "72";
                    break;
                case 32:
                    celdaTUR = "77";
                    break;
                case 33:
                    celdaTUR = "82";
                    break;
                case 34:
                    celdaTUR = "86";
                    break;
                case 35:
                    celdaTUR = "88";
                    break;
                case 36:
                    celdaTUR = "90";
                    break;
                case 37:
                    celdaTUR = "92";
                    break;
                case 38:
                    celdaTUR = "94";
                    break;
                case 39:
                    celdaTUR = "95";
                    break;
                case 40:
                    celdaTUR = "98";
                    break;

            }
            #endregion
            //if (!string.IsNullOrEmpty(celdaTUR))
            //{
            //    TableRow row = table.Elements<TableRow>().ElementAt(14);
            //    TableCell cell = row.Elements<TableCell>().ElementAt(4);
            //    Paragraph p = cell.Elements<Paragraph>().First();
            //    Run r = p.Elements<Run>().First();
            //    Text t = r.Elements<Text>().First();
            //    if (celdaTUR.Equals("0"))
            //        t.Text = "";
            //    else
            //        t.Text = celdaTUR;
            //}
            objTUR.Puntaje = Convert.ToInt32(celdaTUR);

            lstPT.Add(objADM);
            lstPT.Add(objAGR);
            lstPT.Add(objART);
            lstPT.Add(objCOM);
            lstPT.Add(objCON);
            lstPT.Add(objCUL);
            lstPT.Add(objDEP);
            lstPT.Add(objDIS);
            lstPT.Add(objFIN);
            lstPT.Add(objINF);
            lstPT.Add(objJUR);
            lstPT.Add(objMAR);
            lstPT.Add(objMEC);
            lstPT.Add(objMIN);
            lstPT.Add(objPED);
            lstPT.Add(objSAL);
            lstPT.Add(objSOC);
            lstPT.Add(objTRA);
            lstPT.Add(objTUR);


            for (int i = 0; i < lstPT.Count; i++)
            {
                for (int j = i + 1; j < lstPT.Count; j++)
                {
                    if (lstPT[i].Puntaje > lstPT[j].Puntaje)
                    {
                        PuntajeTotalInteresBE objAux = new PuntajeTotalInteresBE();
                        objAux.Area = lstPT[i].Area;
                        objAux.Codigo = lstPT[i].Codigo;
                        objAux.Puntaje = lstPT[i].Puntaje;

                        lstPT[i].Area = lstPT[j].Area;
                        lstPT[i].Codigo = lstPT[j].Codigo;
                        lstPT[i].Puntaje = lstPT[j].Puntaje;

                        lstPT[j].Area = objAux.Area;
                        lstPT[j].Codigo = objAux.Codigo;
                        lstPT[j].Puntaje = objAux.Puntaje;
                    }
                }
            }

            return lstPT;
        }

        private void MostrarPuntajesTest(Table table, List<PuntajeTotalInteresBE> lstPT)
        {

            int lstPT_size = lstPT.Count;
            string codigoArea = "";
            TableRow row1 = null;
            for (int i = 0; i < lstPT_size; i++)
            {
                codigoArea = lstPT[i].Codigo;
                switch (codigoArea)
                {
                    case "ADM":
                        row1 = table.Elements<TableRow>().ElementAt(2);
                        intADM = lstPT[i].Puntaje;
                        break;
                    case "AGR":
                        row1 = table.Elements<TableRow>().ElementAt(3);
                        intARG = lstPT[i].Puntaje;
                        break;
                    case "ART":
                        row1 = table.Elements<TableRow>().ElementAt(4);
                        intART = lstPT[i].Puntaje;
                        break;
                    case "COM":
                        row1 = table.Elements<TableRow>().ElementAt(5);
                        intCOM = lstPT[i].Puntaje;
                        break;
                    case "CON":
                        row1 = table.Elements<TableRow>().ElementAt(6);
                        intCON = lstPT[i].Puntaje;
                        break;
                    case "CUL":
                        row1 = table.Elements<TableRow>().ElementAt(7);
                        intCUL = lstPT[i].Puntaje;
                        break;
                    case "DEP":
                        row1 = table.Elements<TableRow>().ElementAt(8);
                        intDEP = lstPT[i].Puntaje;
                        break;
                    case "DIS":
                        row1 = table.Elements<TableRow>().ElementAt(9);
                        intDIS = lstPT[i].Puntaje;
                        break;
                    case "FIN":
                        row1 = table.Elements<TableRow>().ElementAt(10);
                        intFIN = lstPT[i].Puntaje;
                        break;
                    case "INF":
                        row1 = table.Elements<TableRow>().ElementAt(11);
                        intINF = lstPT[i].Puntaje;
                        break;
                    case "JUR":
                        row1 = table.Elements<TableRow>().ElementAt(12);
                        intJUR = lstPT[i].Puntaje;
                        break;
                    case "MAR":
                        row1 = table.Elements<TableRow>().ElementAt(13);
                        intMAR = lstPT[i].Puntaje;
                        break;
                    case "MEC":
                        row1 = table.Elements<TableRow>().ElementAt(14);
                        intMEC = lstPT[i].Puntaje;
                        break;
                    case "MIN":
                        row1 = table.Elements<TableRow>().ElementAt(15);
                        intMIN = lstPT[i].Puntaje;
                        break;
                    case "PED":
                        row1 = table.Elements<TableRow>().ElementAt(16);
                        intPED = lstPT[i].Puntaje;
                        break;
                    case "SAL":
                        row1 = table.Elements<TableRow>().ElementAt(17);
                        intSAL = lstPT[i].Puntaje;
                        break;
                    case "SOC":
                        row1 = table.Elements<TableRow>().ElementAt(18);
                        intSOC = lstPT[i].Puntaje;
                        break;
                    case "TRA":
                        row1 = table.Elements<TableRow>().ElementAt(19);
                        intTRA = lstPT[i].Puntaje;
                        break;
                    case "TUR":
                        row1 = table.Elements<TableRow>().ElementAt(20);
                        intTUR = lstPT[i].Puntaje;
                        break;
                }


                TableCell cell1 = row1.Elements<TableCell>().ElementAt(1);
                Paragraph p1 = cell1.Elements<Paragraph>().First();
                Run r1 = p1.Elements<Run>().First();
                Text t1 = r1.Elements<Text>().First();
                t1.Text = lstPT[i].Puntaje.ToString();

            }



        }

        private void MostrarBarrasPT(WordprocessingDocument doc, Table table, List<PuntajeTotalInteresBE> lstPT)
        {
            string pathImages = Server.MapPath("~/Images/");

            for (int i = 0; i < lstPT.Count; i++)
            {
                int puntaje = lstPT[i].Puntaje;



                if (puntaje != 0)
                {

                    //Nuevo
                    int ptImagen = (puntaje % 2 == 0) ? puntaje - 1 : puntaje;

                    string imagen = pathImages + ptImagen.ToString() + ".png";
                    //string imagen = pathImages + "99" +".png";

                    switch (lstPT[i].Codigo)
                    {

                        case "ADM":
                            TableRow row1 = table.Elements<TableRow>().ElementAt(2);
                            TableCell cell1 = row1.Elements<TableCell>().ElementAt(2);
                            AddImage(cell1, doc.MainDocumentPart, imagen, "imId1");
                            break;

                        case "AGR":
                            TableRow row2 = table.Elements<TableRow>().ElementAt(3);
                            TableCell cell2 = row2.Elements<TableCell>().ElementAt(2);
                            AddImage(cell2, doc.MainDocumentPart, imagen, "imId2");
                            break;

                        case "ART":
                            TableRow row3 = table.Elements<TableRow>().ElementAt(4);
                            TableCell cell3 = row3.Elements<TableCell>().ElementAt(2);
                            AddImage(cell3, doc.MainDocumentPart, imagen, "imId3");
                            break;

                        case "COM":
                            TableRow row4 = table.Elements<TableRow>().ElementAt(5);
                            TableCell cell4 = row4.Elements<TableCell>().ElementAt(2);
                            AddImage(cell4, doc.MainDocumentPart, imagen, "imId4");
                            break;

                        case "CON":
                            TableRow row5 = table.Elements<TableRow>().ElementAt(6);
                            TableCell cell5 = row5.Elements<TableCell>().ElementAt(2);
                            AddImage(cell5, doc.MainDocumentPart, imagen, "imId5");
                            break;

                        case "CUL":
                            TableRow row6 = table.Elements<TableRow>().ElementAt(7);
                            TableCell cell6 = row6.Elements<TableCell>().ElementAt(2);
                            AddImage(cell6, doc.MainDocumentPart, imagen, "imId6");
                            break;

                        case "DEP":
                            TableRow row7 = table.Elements<TableRow>().ElementAt(8);
                            TableCell cell7 = row7.Elements<TableCell>().ElementAt(2);
                            AddImage(cell7, doc.MainDocumentPart, imagen, "imId7");
                            break;

                        case "DIS":
                            TableRow row8 = table.Elements<TableRow>().ElementAt(9);
                            TableCell cell8 = row8.Elements<TableCell>().ElementAt(2);
                            AddImage(cell8, doc.MainDocumentPart, imagen, "imId8");
                            break;

                        case "FIN":
                            TableRow row9 = table.Elements<TableRow>().ElementAt(10);
                            TableCell cell9 = row9.Elements<TableCell>().ElementAt(2);
                            AddImage(cell9, doc.MainDocumentPart, imagen, "imId9");
                            break;

                        case "INF":
                            TableRow row10 = table.Elements<TableRow>().ElementAt(11);
                            TableCell cell10 = row10.Elements<TableCell>().ElementAt(2);
                            AddImage(cell10, doc.MainDocumentPart, imagen, "imId10");
                            break;

                        case "JUR":
                            TableRow row11 = table.Elements<TableRow>().ElementAt(12);
                            TableCell cell11 = row11.Elements<TableCell>().ElementAt(2);
                            AddImage(cell11, doc.MainDocumentPart, imagen, "imId11");
                            break;

                        case "MAR":
                            TableRow row12 = table.Elements<TableRow>().ElementAt(13);
                            TableCell cell12 = row12.Elements<TableCell>().ElementAt(2);
                            AddImage(cell12, doc.MainDocumentPart, imagen, "imId12");
                            break;

                        case "MEC":
                            TableRow row13 = table.Elements<TableRow>().ElementAt(14);
                            TableCell cell13 = row13.Elements<TableCell>().ElementAt(2);
                            AddImage(cell13, doc.MainDocumentPart, imagen, "imId13");
                            break;

                        case "MIN":
                            TableRow row14 = table.Elements<TableRow>().ElementAt(15);
                            TableCell cell14 = row14.Elements<TableCell>().ElementAt(2);
                            AddImage(cell14, doc.MainDocumentPart, imagen, "imId14");
                            break;

                        case "PED":
                            TableRow row15 = table.Elements<TableRow>().ElementAt(16);
                            TableCell cell15 = row15.Elements<TableCell>().ElementAt(2);
                            AddImage(cell15, doc.MainDocumentPart, imagen, "imId15");
                            break;

                        case "SAL":
                            TableRow row16 = table.Elements<TableRow>().ElementAt(17);
                            TableCell cell16 = row16.Elements<TableCell>().ElementAt(2);
                            AddImage(cell16, doc.MainDocumentPart, imagen, "imId16");
                            break;

                        case "SOC":
                            TableRow row17 = table.Elements<TableRow>().ElementAt(18);
                            TableCell cell17 = row17.Elements<TableCell>().ElementAt(2);
                            AddImage(cell17, doc.MainDocumentPart, imagen, "imId17");
                            break;

                        case "TRA":
                            TableRow row18 = table.Elements<TableRow>().ElementAt(19);
                            TableCell cell18 = row18.Elements<TableCell>().ElementAt(2);
                            AddImage(cell18, doc.MainDocumentPart, imagen, "imId18");
                            break;

                        case "TUR":
                            TableRow row19 = table.Elements<TableRow>().ElementAt(20);
                            TableCell cell19 = row19.Elements<TableCell>().ElementAt(2);
                            AddImage(cell19, doc.MainDocumentPart, imagen, "imId19");
                            break;
                    }

                }
            }


        }

        private void MostrarDescripcionesTalentos(Table table, string fileName, List<RecomendacionBE> lstDescripciones, bool administrador, int Talentsize, WordprocessingDocument doc)
        {

            TableRow rt1 = table.Elements<TableRow>().ElementAt(0);
            //Table table1_0 = rt1.Elements<TableCell>().ElementAt(0).Elements<Table>().First();
            Table table1 = rt1.Elements<TableCell>().ElementAt(1).Elements<Table>().First();

            TableRow rt2 = table.Elements<TableRow>().ElementAt(1);
            //Table table2_0 = rt2.Elements<TableCell>().ElementAt(0).Elements<Table>().First();
            Table table2 = rt2.Elements<TableCell>().ElementAt(1).Elements<Table>().First();

            TableRow rt3 = table.Elements<TableRow>().ElementAt(2);
            //Table table3_0 = rt3.Elements<TableCell>().ElementAt(0).Elements<Table>().First();
            Table table3 = rt3.Elements<TableCell>().ElementAt(1).Elements<Table>().First();

            TableRow rt4 = table.Elements<TableRow>().ElementAt(3);
            //Table table4_0 = rt4.Elements<TableCell>().ElementAt(0).Elements<Table>().First();
            Table table4 = rt4.Elements<TableCell>().ElementAt(1).Elements<Table>().First();

            TableRow rt5 = table.Elements<TableRow>().ElementAt(4);
            //Table table5_0 = rt5.Elements<TableCell>().ElementAt(0).Elements<Table>().First();
            Table table5 = rt5.Elements<TableCell>().ElementAt(1).Elements<Table>().First();

            TableRow rt6 = table.Elements<TableRow>().ElementAt(5);
            //Table table6_0 = rt6.Elements<TableCell>().ElementAt(0).Elements<Table>().First();
            Table table6 = rt6.Elements<TableCell>().ElementAt(1).Elements<Table>().First();

            TableRow rt7 = table.Elements<TableRow>().ElementAt(6);
            //Table table7_0 = rt7.Elements<TableCell>().ElementAt(0).Elements<Table>().First();
            Table table7 = rt7.Elements<TableCell>().ElementAt(1).Elements<Table>().First();

            TableRow rt8 = table.Elements<TableRow>().ElementAt(7);
            //Table table8_0 = rt8.Elements<TableCell>().ElementAt(0).Elements<Table>().First();
            Table table8 = rt8.Elements<TableCell>().ElementAt(1).Elements<Table>().First();

            TableRow rt9 = table.Elements<TableRow>().ElementAt(8);
            //Table table9_0 = rt9.Elements<TableCell>().ElementAt(0).Elements<Table>().First();
            Table table9 = rt9.Elements<TableCell>().ElementAt(1).Elements<Table>().First();

            TableRow rt10 = table.Elements<TableRow>().ElementAt(9);
            //Table table10_0 = rt10.Elements<TableCell>().ElementAt(0).Elements<Table>().First();
            Table table10 = rt10.Elements<TableCell>().ElementAt(1).Elements<Table>().First();

            TableRow rt11 = table.Elements<TableRow>().ElementAt(10);
            //Table table11_0 = rt11.Elements<TableCell>().ElementAt(0).Elements<Table>().First();
            Table table11 = rt11.Elements<TableCell>().ElementAt(1).Elements<Table>().First();

            TableRow rt12 = table.Elements<TableRow>().ElementAt(11);
            //Table table12_0 = rt12.Elements<TableCell>().ElementAt(0).Elements<Table>().First();
            Table table12 = rt12.Elements<TableCell>().ElementAt(1).Elements<Table>().First();

            int idTalentoAnterior = 0;
            int cont = 1;
            int contTal = 1;
            int fila = 0;
            int tope = 13;

            idTalentoAnterior = lstDescripciones[0].IdTalento;
            contTal = 1;

            string pathImages = Server.MapPath("~/Images/Talentos/");
            string imagen = pathImages + lstDescripciones[0].Talento + ".png";


            TableRow row1 = table.Elements<TableRow>().ElementAt(0);
            TableCell cell1 = row1.Elements<TableCell>().ElementAt(0);
            imagen = replaceTildes(imagen);
            AddImage(cell1, doc.MainDocumentPart, imagen, "imTalId1");

            /*
            Paragraph p1 = cell1_0.Elements<Paragraph>().First();
            Run r1 = p1.Elements<Run>().First();
            Text t1 = r1.Elements<Text>().First();
            t1.Text = lstDescripciones[0].Talento.ToString();
            */

            for (int i = 0; i < lstDescripciones.Count; i++)
            {
                if (!lstDescripciones[i].IdTalento.Equals(idTalentoAnterior))
                {
                    break;
                }
                TableRow row = table1.Elements<TableRow>().ElementAt(fila);
                TableCell cell = row.Elements<TableCell>().ElementAt(0);
                Paragraph p = cell.Elements<Paragraph>().First();
                Run r = p.Elements<Run>().First();
                Text t = r.Elements<Text>().First();
                t.Text = lstDescripciones[i].Descripcion;
                cont++;
                contTal++;
                fila++;
            }


            for (int i = fila; i <= tope; i++)
            {
                TableRow row = table1.Elements<TableRow>().Last();
                TableCell cell = row.Elements<TableCell>().ElementAt(0);
                Paragraph p = cell.Elements<Paragraph>().First();
                Run r = p.Elements<Run>().First();
                Text t = r.Elements<Text>().First();

                if (t.Text.Contains("DE"))
                {
                    cell.RemoveAllChildren();
                    cell.Remove();
                    row.RemoveAllChildren();
                    row.Remove();
                }
            }


            /********************************
             ******  FILA 2 EN PLANTILLA ****
             ********************************/

            idTalentoAnterior = lstDescripciones[cont - 1].IdTalento;
            contTal = 1;
            fila = 0;


            TableRow row2 = table.Elements<TableRow>().ElementAt(1);
            TableCell cell2 = row2.Elements<TableCell>().ElementAt(0);
            imagen = pathImages + lstDescripciones[cont - 1].Talento + ".png";
            imagen = replaceTildes(imagen);
            AddImage(cell2, doc.MainDocumentPart, imagen, "imTalId2");

            /*
            Paragraph p2 = cell2_0.Elements<Paragraph>().First();
            Run r2 = p2.Elements<Run>().First();
            Text t2 = r2.Elements<Text>().First();
            t2.Text = lstDescripciones[cont - 1].Talento.ToString();
            */


            for (int i = cont - 1; i < lstDescripciones.Count; i++)
            {
                if (!lstDescripciones[i].IdTalento.Equals(idTalentoAnterior))
                {
                    break;
                }

                TableRow row = table2.Elements<TableRow>().ElementAt(fila);
                TableCell cell = row.Elements<TableCell>().ElementAt(0);
                Paragraph p = cell.Elements<Paragraph>().First();
                Run r = p.Elements<Run>().First();
                Text t = r.Elements<Text>().First();
                t.Text = lstDescripciones[i].Descripcion;
                cont++;
                contTal++;
                fila++;
            }


            for (int i = fila; i <= tope; i++)
            {
                TableRow row = table2.Elements<TableRow>().Last();
                TableCell cell = row.Elements<TableCell>().ElementAt(0);
                Paragraph p = cell.Elements<Paragraph>().First();
                Run r = p.Elements<Run>().First();
                Text t = r.Elements<Text>().First();

                if (t.Text.Contains("DE"))
                {
                    cell.RemoveAllChildren();
                    cell.Remove();
                    row.RemoveAllChildren();
                    row.Remove();
                }
            }


            /********************************
             ****** FILA 3 EN PLANTILLA *****
             ********************************/
            idTalentoAnterior = lstDescripciones[cont - 1].IdTalento;
            contTal = 1;
            fila = 0;


            TableRow row3 = table.Elements<TableRow>().ElementAt(2);
            TableCell cell3 = row3.Elements<TableCell>().ElementAt(0);

            imagen = pathImages + lstDescripciones[cont - 1].Talento + ".png";
            imagen = replaceTildes(imagen);
            AddImage(cell3, doc.MainDocumentPart, imagen, "imTalId3");

            /*
            Paragraph p3 = cell3_0.Elements<Paragraph>().First();
            Run r3 = p3.Elements<Run>().First();
            Text t3 = r3.Elements<Text>().First();
            t3.Text = lstDescripciones[cont - 1].Talento.ToString();
            */

            for (int i = cont - 1; i < lstDescripciones.Count; i++)
            {
                if (!lstDescripciones[i].IdTalento.Equals(idTalentoAnterior))
                {
                    break;
                }

                TableRow row = table3.Elements<TableRow>().ElementAt(fila);
                TableCell cell = row.Elements<TableCell>().ElementAt(0);
                Paragraph p = cell.Elements<Paragraph>().First();
                Run r = p.Elements<Run>().First();
                Text t = r.Elements<Text>().First();
                t.Text = lstDescripciones[i].Descripcion;
                cont++;
                contTal++;
                fila++;
            }


            for (int i = fila; i <= tope; i++)
            {
                TableRow row = table3.Elements<TableRow>().Last();
                TableCell cell = row.Elements<TableCell>().ElementAt(0);
                Paragraph p = cell.Elements<Paragraph>().First();
                Run r = p.Elements<Run>().First();
                Text t = r.Elements<Text>().First();

                if (t.Text.Contains("DE"))
                {
                    cell.RemoveAllChildren();
                    cell.Remove();
                    row.RemoveAllChildren();
                    row.Remove();
                }
            }


            /********************************
            ****** FILA 4 EN PLANTILLA *****
            ********************************/
            idTalentoAnterior = lstDescripciones[cont - 1].IdTalento;
            contTal = 1;
            fila = 0;


            TableRow row4 = table.Elements<TableRow>().ElementAt(3);
            TableCell cell4 = row4.Elements<TableCell>().ElementAt(0);

            imagen = pathImages + lstDescripciones[cont - 1].Talento + ".png";
            imagen = replaceTildes(imagen);
            AddImage(cell4, doc.MainDocumentPart, imagen, "imTalId4");

            /*
            Paragraph p4 = cell4_0.Elements<Paragraph>().First();
            Run r4 = p4.Elements<Run>().First();
            Text t4 = r4.Elements<Text>().First();
            t4.Text = lstDescripciones[cont - 1].Talento.ToString();
            */

            for (int i = cont - 1; i < lstDescripciones.Count; i++)
            {
                if (!lstDescripciones[i].IdTalento.Equals(idTalentoAnterior))
                {
                    break;
                }

                TableRow row = table4.Elements<TableRow>().ElementAt(fila);
                TableCell cell = row.Elements<TableCell>().ElementAt(0);
                Paragraph p = cell.Elements<Paragraph>().First();
                Run r = p.Elements<Run>().First();
                Text t = r.Elements<Text>().First();
                t.Text = lstDescripciones[i].Descripcion;
                cont++;
                contTal++;
                fila++;
            }


            for (int i = fila; i <= tope; i++)
            {
                TableRow row = table4.Elements<TableRow>().Last();
                TableCell cell = row.Elements<TableCell>().ElementAt(0);
                Paragraph p = cell.Elements<Paragraph>().First();
                Run r = p.Elements<Run>().First();
                Text t = r.Elements<Text>().First();

                if (t.Text.Contains("DE"))
                {
                    cell.RemoveAllChildren();
                    cell.Remove();
                    row.RemoveAllChildren();
                    row.Remove();
                }
            }


            /********************************
            ****** FILA 5 EN PLANTILLA *****
            ********************************/
            idTalentoAnterior = lstDescripciones[cont - 1].IdTalento;
            contTal = 1;
            fila = 0;


            TableRow row5 = table.Elements<TableRow>().ElementAt(4);
            TableCell cell5 = row5.Elements<TableCell>().ElementAt(0);
            imagen = pathImages + lstDescripciones[cont - 1].Talento + ".png";
            imagen = replaceTildes(imagen);
            AddImage(cell5, doc.MainDocumentPart, imagen, "imTalId5");
            /*
            Paragraph p5 = cell5_0.Elements<Paragraph>().First();
            Run r5 = p5.Elements<Run>().First();
            Text t5 = r5.Elements<Text>().First();
            t5.Text = lstDescripciones[cont - 1].Talento.ToString();
            */

            for (int i = cont - 1; i < lstDescripciones.Count; i++)
            {
                if (!lstDescripciones[i].IdTalento.Equals(idTalentoAnterior))
                {
                    break;
                }

                TableRow row = table5.Elements<TableRow>().ElementAt(fila);
                TableCell cell = row.Elements<TableCell>().ElementAt(0);
                Paragraph p = cell.Elements<Paragraph>().First();
                Run r = p.Elements<Run>().First();
                Text t = r.Elements<Text>().First();
                t.Text = lstDescripciones[i].Descripcion;
                cont++;
                contTal++;
                fila++;
            }

            for (int i = fila; i <= tope; i++)
            {
                TableRow row = table5.Elements<TableRow>().Last();
                TableCell cell = row.Elements<TableCell>().ElementAt(0);
                Paragraph p = cell.Elements<Paragraph>().First();
                Run r = p.Elements<Run>().First();
                Text t = r.Elements<Text>().First();

                if (t.Text.Contains("DE"))
                {
                    cell.RemoveAllChildren();
                    cell.Remove();
                    row.RemoveAllChildren();
                    row.Remove();
                }
            }


            /********************************
            ****** FILA 6 EN PLANTILLA *****
            ********************************/
            idTalentoAnterior = lstDescripciones[cont - 1].IdTalento;
            contTal = 1;
            fila = 0;


            TableRow row6 = table.Elements<TableRow>().ElementAt(5);
            TableCell cell6 = row6.Elements<TableCell>().ElementAt(0);

            imagen = pathImages + lstDescripciones[cont - 1].Talento + ".png";
            imagen = replaceTildes(imagen);
            AddImage(cell6, doc.MainDocumentPart, imagen, "imTalId6");
            /*
            Paragraph p6 = cell6_0.Elements<Paragraph>().First();
            Run r6 = p6.Elements<Run>().First();
            Text t6 = r6.Elements<Text>().First();
            t6.Text = lstDescripciones[cont - 1].Talento.ToString();
            */

            for (int i = cont - 1; i < lstDescripciones.Count; i++)
            {
                if (!lstDescripciones[i].IdTalento.Equals(idTalentoAnterior))
                {
                    break;
                }

                TableRow row = table6.Elements<TableRow>().ElementAt(fila);
                TableCell cell = row.Elements<TableCell>().ElementAt(0);
                Paragraph p = cell.Elements<Paragraph>().First();
                Run r = p.Elements<Run>().First();
                Text t = r.Elements<Text>().First();
                t.Text = lstDescripciones[i].Descripcion;
                cont++;
                contTal++;
                fila++;
            }


            for (int i = fila; i <= tope; i++)
            {
                TableRow row = table6.Elements<TableRow>().Last();
                TableCell cell = row.Elements<TableCell>().ElementAt(0);
                Paragraph p = cell.Elements<Paragraph>().First();
                Run r = p.Elements<Run>().First();
                Text t = r.Elements<Text>().First();

                if (t.Text.Contains("DE"))
                {
                    cell.RemoveAllChildren();
                    cell.Remove();
                    row.RemoveAllChildren();
                    row.Remove();
                }
            }


            /********************************
            ****** FILA 7 EN PLANTILLA *****
            ********************************/
            idTalentoAnterior = lstDescripciones[cont - 1].IdTalento;
            contTal = 1;
            fila = 0;


            TableRow row7 = table.Elements<TableRow>().ElementAt(6);
            TableCell cell7 = row7.Elements<TableCell>().ElementAt(0);

            imagen = pathImages + lstDescripciones[cont - 1].Talento + ".png";
            imagen = replaceTildes(imagen);
            AddImage(cell7, doc.MainDocumentPart, imagen, "imTalId7");

            /*
            Paragraph p7 = cell7_0.Elements<Paragraph>().First();
            Run r7 = p7.Elements<Run>().First();
            Text t7 = r7.Elements<Text>().First();
            t7.Text = lstDescripciones[cont - 1].Talento.ToString();
            */

            for (int i = cont - 1; i < lstDescripciones.Count; i++)
            {
                if (!lstDescripciones[i].IdTalento.Equals(idTalentoAnterior))
                {
                    break;
                }

                TableRow row = table7.Elements<TableRow>().ElementAt(fila);
                TableCell cell = row.Elements<TableCell>().ElementAt(0);
                Paragraph p = cell.Elements<Paragraph>().First();
                Run r = p.Elements<Run>().First();
                Text t = r.Elements<Text>().First();
                t.Text = lstDescripciones[i].Descripcion;
                cont++;
                contTal++;
                fila++;
            }


            for (int i = fila; i <= tope; i++)
            {
                TableRow row = table7.Elements<TableRow>().Last();
                TableCell cell = row.Elements<TableCell>().ElementAt(0);
                Paragraph p = cell.Elements<Paragraph>().First();
                Run r = p.Elements<Run>().First();
                Text t = r.Elements<Text>().First();

                if (t.Text.Contains("DE"))
                {
                    cell.RemoveAllChildren();
                    cell.Remove();
                    row.RemoveAllChildren();
                    row.Remove();
                }
            }




            /********************************
            ****** FILA 8 EN PLANTILLA *****
            ********************************/
            idTalentoAnterior = lstDescripciones[cont - 1].IdTalento;
            contTal = 1;
            fila = 0;


            TableRow row8 = table.Elements<TableRow>().ElementAt(7);
            TableCell cell8 = row8.Elements<TableCell>().ElementAt(0);
            imagen = pathImages + lstDescripciones[cont - 1].Talento + ".png";
            imagen = replaceTildes(imagen);
            AddImage(cell8, doc.MainDocumentPart, imagen, "imTalId8");

            /*
            Paragraph p8 = cell8_0.Elements<Paragraph>().First();
            Run r8 = p8.Elements<Run>().First();
            Text t8 = r8.Elements<Text>().First();
            t8.Text = lstDescripciones[cont - 1].Talento.ToString();
            */

            for (int i = cont - 1; i < lstDescripciones.Count; i++)
            {
                if (!lstDescripciones[i].IdTalento.Equals(idTalentoAnterior))
                {
                    break;
                }

                TableRow row = table8.Elements<TableRow>().ElementAt(fila);
                TableCell cell = row.Elements<TableCell>().ElementAt(0);
                Paragraph p = cell.Elements<Paragraph>().First();
                Run r = p.Elements<Run>().First();
                Text t = r.Elements<Text>().First();
                t.Text = lstDescripciones[i].Descripcion;
                cont++;
                contTal++;
                fila++;
            }


            for (int i = fila; i <= tope; i++)
            {
                TableRow row = table8.Elements<TableRow>().Last();
                TableCell cell = row.Elements<TableCell>().ElementAt(0);
                Paragraph p = cell.Elements<Paragraph>().First();
                Run r = p.Elements<Run>().First();
                Text t = r.Elements<Text>().First();

                if (t.Text.Contains("DE"))
                {
                    cell.RemoveAllChildren();
                    cell.Remove();
                    row.RemoveAllChildren();
                    row.Remove();
                }
            }


            /********************************
            ****** FILA 9 EN PLANTILLA *****
            ********************************/
            idTalentoAnterior = lstDescripciones[cont - 1].IdTalento;
            contTal = 1;
            fila = 0;


            TableRow row9 = table.Elements<TableRow>().ElementAt(8);
            TableCell cell9 = row9.Elements<TableCell>().ElementAt(0);
            imagen = pathImages + lstDescripciones[cont - 1].Talento + ".png";
            imagen = replaceTildes(imagen);
            AddImage(cell9, doc.MainDocumentPart, imagen, "imTalId9");

            /*
            Paragraph p9 = cell9_0.Elements<Paragraph>().First();
            Run r9 = p9.Elements<Run>().First();
            Text t9 = r9.Elements<Text>().First();
            t9.Text = lstDescripciones[cont - 1].Talento.ToString();
            */

            for (int i = cont - 1; i < lstDescripciones.Count; i++)
            {
                if (!lstDescripciones[i].IdTalento.Equals(idTalentoAnterior))
                {
                    break;
                }

                TableRow row = table9.Elements<TableRow>().ElementAt(fila);
                TableCell cell = row.Elements<TableCell>().ElementAt(0);
                Paragraph p = cell.Elements<Paragraph>().First();
                Run r = p.Elements<Run>().First();
                Text t = r.Elements<Text>().First();
                t.Text = lstDescripciones[i].Descripcion;
                cont++;
                contTal++;
                fila++;
            }


            for (int i = fila; i <= tope; i++)
            {
                TableRow row = table9.Elements<TableRow>().Last();
                TableCell cell = row.Elements<TableCell>().ElementAt(0);
                Paragraph p = cell.Elements<Paragraph>().First();
                Run r = p.Elements<Run>().First();
                Text t = r.Elements<Text>().First();

                if (t.Text.Contains("DE"))
                {
                    cell.RemoveAllChildren();
                    cell.Remove();
                    row.RemoveAllChildren();
                    row.Remove();
                }
            }


            /********************************
            ****** FILA 10 EN PLANTILLA *****
            ********************************/
            idTalentoAnterior = lstDescripciones[cont - 1].IdTalento;
            contTal = 1;
            fila = 0;


            TableRow row10 = table.Elements<TableRow>().ElementAt(9);
            TableCell cell10 = row10.Elements<TableCell>().ElementAt(0);
            imagen = pathImages + lstDescripciones[cont - 1].Talento + ".png";
            imagen = replaceTildes(imagen);
            AddImage(cell10, doc.MainDocumentPart, imagen, "imTalId10");

            /*
            Paragraph p10 = cell10_0.Elements<Paragraph>().First();
            Run r10 = p10.Elements<Run>().First();
            Text t10 = r10.Elements<Text>().First();
            t10.Text = lstDescripciones[cont - 1].Talento.ToString();
            */

            for (int i = cont - 1; i < lstDescripciones.Count; i++)
            {
                if (!lstDescripciones[i].IdTalento.Equals(idTalentoAnterior))
                {
                    break;
                }

                TableRow row = table10.Elements<TableRow>().ElementAt(fila);
                TableCell cell = row.Elements<TableCell>().ElementAt(0);
                Paragraph p = cell.Elements<Paragraph>().First();
                Run r = p.Elements<Run>().First();
                Text t = r.Elements<Text>().First();
                t.Text = lstDescripciones[i].Descripcion;
                cont++;
                contTal++;
                fila++;
            }


            for (int i = fila; i <= tope; i++)
            {
                TableRow row = table10.Elements<TableRow>().Last();
                TableCell cell = row.Elements<TableCell>().ElementAt(0);
                Paragraph p = cell.Elements<Paragraph>().First();
                Run r = p.Elements<Run>().First();
                Text t = r.Elements<Text>().First();

                if (t.Text.Contains("DE"))
                {
                    cell.RemoveAllChildren();
                    cell.Remove();
                    row.RemoveAllChildren();
                    row.Remove();
                }
            }

            /********************************
            ****** FILA 11 EN PLANTILLA *****
            ********************************/
            idTalentoAnterior = lstDescripciones[cont - 1].IdTalento;
            contTal = 1;
            fila = 0;


            TableRow row11 = table.Elements<TableRow>().ElementAt(10);
            TableCell cell11 = row11.Elements<TableCell>().ElementAt(0);
            imagen = pathImages + lstDescripciones[cont - 1].Talento + ".png";
            imagen = replaceTildes(imagen);
            AddImage(cell11, doc.MainDocumentPart, imagen, "imTalId11");

            /*
            Paragraph p11 = cell11_0.Elements<Paragraph>().First();
            Run r11 = p11.Elements<Run>().First();
            Text t11 = r11.Elements<Text>().First();
            t11.Text = lstDescripciones[cont - 1].Talento.ToString();
            */

            for (int i = cont - 1; i < lstDescripciones.Count; i++)
            {
                if (!lstDescripciones[i].IdTalento.Equals(idTalentoAnterior))
                {
                    break;
                }

                TableRow row = table11.Elements<TableRow>().ElementAt(fila);
                TableCell cell = row.Elements<TableCell>().ElementAt(0);
                Paragraph p = cell.Elements<Paragraph>().First();
                Run r = p.Elements<Run>().First();
                Text t = r.Elements<Text>().First();
                t.Text = lstDescripciones[i].Descripcion;
                cont++;
                contTal++;
                fila++;
            }


            for (int i = fila; i <= tope; i++)
            {
                TableRow row = table11.Elements<TableRow>().Last();
                TableCell cell = row.Elements<TableCell>().ElementAt(0);
                Paragraph p = cell.Elements<Paragraph>().First();
                Run r = p.Elements<Run>().First();
                Text t = r.Elements<Text>().First();

                if (t.Text.Contains("DE"))
                {
                    cell.RemoveAllChildren();
                    cell.Remove();
                    row.RemoveAllChildren();
                    row.Remove();
                }
            }


            /********************************
            ****** FILA 12 EN PLANTILLA *****
            ********************************/
            idTalentoAnterior = lstDescripciones[cont - 1].IdTalento;
            contTal = 1;
            fila = 0;


            TableRow row12 = table.Elements<TableRow>().ElementAt(11);
            TableCell cell12 = row12.Elements<TableCell>().ElementAt(0);
            imagen = pathImages + lstDescripciones[cont - 1].Talento + ".png";
            imagen = replaceTildes(imagen);
            AddImage(cell12, doc.MainDocumentPart, imagen, "imTalId12");

            /*
            Paragraph p12 = cell12_0.Elements<Paragraph>().First();
            Run r12 = p12.Elements<Run>().First();
            Text t12 = r12.Elements<Text>().First();
            t12.Text = lstDescripciones[cont - 1].Talento.ToString();
            */

            for (int i = cont - 1; i < lstDescripciones.Count; i++)
            {
                if (!lstDescripciones[i].IdTalento.Equals(idTalentoAnterior))
                {
                    break;
                }

                TableRow row = table12.Elements<TableRow>().ElementAt(fila);
                TableCell cell = row.Elements<TableCell>().ElementAt(0);
                Paragraph p = cell.Elements<Paragraph>().First();
                Run r = p.Elements<Run>().First();
                Text t = r.Elements<Text>().First();
                t.Text = lstDescripciones[i].Descripcion;
                cont++;
                contTal++;
                fila++;
            }



            for (int i = fila; i <= tope; i++)
            {
                TableRow row = table12.Elements<TableRow>().Last();
                TableCell cell = row.Elements<TableCell>().ElementAt(0);
                Paragraph p = cell.Elements<Paragraph>().First();
                Run r = p.Elements<Run>().First();
                Text t = r.Elements<Text>().First();

                if (t.Text.Contains("DE"))
                {
                    cell.RemoveAllChildren();
                    cell.Remove();
                    row.RemoveAllChildren();
                    row.Remove();
                }
            }


        }

        private string replaceTildes(string str)
        {
            str = str.Replace("ó", "o");
            str = str.Replace("í", "i");
            str = str.Replace("á", "a");
            str = str.Replace("é", "e");

            return str;
        }

        private int CalcularAreasMasDesarrolladas_2(List<PuntajeTotalInteresBE> lstPuntajeTotal, Table table)
        {
            int cont = 0;


            List<PuntajeTotalInteresBE> lstAreMasDesarrolladas = new List<PuntajeTotalInteresBE>();
            lstAreMasDesarrolladas = lstPuntajeTotal.OrderByDescending(x => x.Puntaje).ToList();

            for (int i = 0; i < lstAreMasDesarrolladas.Count; i++)
            {
                if (lstAreMasDesarrolladas[i].Puntaje < 76)
                {
                    //fila = (i == 0) ? 1 : i; // eliminar esto
                    //fila = i;
                    break;
                }

                llenaTabla(table, i, lstAreMasDesarrolladas[i].Codigo);
                ++cont;
            }



            // Eliminar las filas que sobran
            for (int i = 1; i < 19; i++)
            {
                TableRow rowTal = table.Elements<TableRow>().Last();//.ElementAt(fila);
                TableCell cellTal = rowTal.Elements<TableCell>().ElementAt(0);
                Paragraph pTal = cellTal.Elements<Paragraph>().First();
                Run rTal = pTal.Elements<Run>().First();
                Text tTal = rTal.Elements<Text>().First();

                TableCell cellDes = rowTal.Elements<TableCell>().ElementAt(1);

                if (tTal.Text.Contains("###"))
                {
                    cellTal.RemoveAllChildren();
                    cellTal.Remove();
                    cellDes.RemoveAllChildren();
                    cellDes.Remove();
                    rowTal.RemoveAllChildren();
                    rowTal.Remove();
                }
            }



            return cont;
        }


        private void llenaTabla(Table table, int numRow, string codArea)
        {

            switch (codArea)
            {

                case "ADM":
                    for (int i = 0; i < 3; i++)
                    {
                        TableRow row1 = table.Elements<TableRow>().ElementAt(numRow + 1);
                        TableCell cell1 = row1.Elements<TableCell>().ElementAt(i);
                        Paragraph p1 = cell1.Elements<Paragraph>().First();
                        Run r1 = p1.Elements<Run>().First();
                        Text t1 = r1.Elements<Text>().First();
                        switch (i)
                        {
                            case 0: t1.Text = "ADMINISTRACION"; break;
                            case 1: t1.Text = "Interés en la planificación, organización, dirección y control de los recursos (máquinas, equipos, insumos, personas) de una empresa o fábrica con la finalidad de optimizar sus beneficios. "; break;
                            default: t1.Text = String.Concat("Administración / Gestión                                ",
                                                             "Administración y Gestión Comercial                       ",
                                                             "Administración y Negocios Internacionales                ",
                                                             "                                                         ",
                                                             "Ingeniería de Gestión Empresarial                      ",
                                                             "Ingeniería Industrial                                  ",
                                                             "Recursos Humanos                                       ");
                                break;
                        }

                    }
                    break;

                case "AGR":
                    for (int i = 0; i < 3; i++)
                    {
                        TableRow row1 = table.Elements<TableRow>().ElementAt(numRow + 1);
                        TableCell cell1 = row1.Elements<TableCell>().ElementAt(i);
                        Paragraph p1 = cell1.Elements<Paragraph>().First();
                        Run r1 = p1.Elements<Run>().First();
                        Text t1 = r1.Elements<Text>().First();
                        switch (i)
                        {
                            case 0: t1.Text = "AGRARIA"; break;
                            case 1: t1.Text = "Interés por la mejora de la calidad de la producción y transformación de productos alimentarios para el consumo humano."; break;
                            default: t1.Text = String.Concat("Administración y Agronegocios                        ",
                                                             "Ciencia de los Alimentos                             ",
                                                             "Industrias Alimentarias                              ",
                                                             "Ingeniería Agroindustrial                            ",
                                                             "Ingeniería de Industria Alimentaria                  ");
                                break;
                        }

                    }
                    break;

                case "ART":
                    for (int i = 0; i < 3; i++)
                    {
                        TableRow row1 = table.Elements<TableRow>().ElementAt(numRow + 1);
                        TableCell cell1 = row1.Elements<TableCell>().ElementAt(i);
                        Paragraph p1 = cell1.Elements<Paragraph>().First();
                        Run r1 = p1.Elements<Run>().First();
                        Text t1 = r1.Elements<Text>().First();
                        switch (i)
                        {
                            case 0: t1.Text = "ARTÍSTICA"; break;
                            case 1: t1.Text = "Interés por la producción o participación en presentaciones artísticas de teatro, danza o música."; break;
                            default: t1.Text = String.Concat("Artes Escénicas",
                                                             "                                                               ",
                                                             "Artista Profesional",
                                                             "                                                               ",
                                                             "Danza",
                                                             "                                                                ",
                                                             "Música                                                 ");
                                break;
                        }

                    }
                    break;

                case "COM":
                    for (int i = 0; i < 3; i++)
                    {
                        TableRow row1 = table.Elements<TableRow>().ElementAt(numRow + 1);
                        TableCell cell1 = row1.Elements<TableCell>().ElementAt(i);
                        Paragraph p1 = cell1.Elements<Paragraph>().First();
                        Run r1 = p1.Elements<Run>().First();
                        Text t1 = r1.Elements<Text>().First();
                        switch (i)
                        {
                            case 0: t1.Text = "COMUNICACIÓN"; break;
                            case 1: t1.Text = "Interés en la realización de producciones verbales o gráficas con la finalidad de informar o entretener a las personas a través de diferentes medios."; break;
                            default: t1.Text = String.Concat("Artes Visuales                                       ",
                                                             "Ciencias de la Comunicación                          ",
                                                             "Comunicación Audiovisual y Medios Interactivos       ",
                                                             "                                                     ",
                                                             "Comunicación e Imagen Empresarial                    ",
                                                             "Comunicación Social                                  ",
                                                             "Comunicación y Periodismo                            ",
                                                             "Locución                                                      ",
                                                             "Periodismo                                           ");
                                break;
                        }

                    }
                    break;
                case "CON":
                    for (int i = 0; i < 3; i++)
                    {
                        TableRow row1 = table.Elements<TableRow>().ElementAt(numRow + 1);
                        TableCell cell1 = row1.Elements<TableCell>().ElementAt(i);
                        Paragraph p1 = cell1.Elements<Paragraph>().First();
                        Run r1 = p1.Elements<Run>().First();
                        Text t1 = r1.Elements<Text>().First();
                        switch (i)
                        {
                            case 0: t1.Text = "CONSTRUCCIÓN"; break;
                            case 1: t1.Text = "Interés en la construcción y mantenimiento de la infraestructura acorde a  las necesidades de las personas."; break;
                            default: t1.Text = "Construcción Civil";
                                break;

                        }

                    }

                    break;

                case "CUL":
                    for (int i = 0; i < 3; i++)
                    {
                        TableRow row1 = table.Elements<TableRow>().ElementAt(numRow + 1);
                        TableCell cell1 = row1.Elements<TableCell>().ElementAt(i);
                        Paragraph p1 = cell1.Elements<Paragraph>().First();
                        Run r1 = p1.Elements<Run>().First();
                        Text t1 = r1.Elements<Text>().First();
                        switch (i)
                        {
                            case 0: t1.Text = "CULINARIA"; break;
                            case 1: t1.Text = "Interés en la preparación de alimentos,  la creación de nuevas recetas y la presentación atractiva de comidas, postres y pasteles."; break;
                            default: t1.Text = String.Concat("Arte culinario                                       ",
                                                             "Gastronomía                                                ",
                                                             "Gastronomía y Gestión de Restaurantes                ");
                                break;
                        }

                    }

                    break;

                case "DEP":
                    for (int i = 0; i < 3; i++)
                    {
                        TableRow row1 = table.Elements<TableRow>().ElementAt(numRow + 1);
                        TableCell cell1 = row1.Elements<TableCell>().ElementAt(i);
                        Paragraph p1 = cell1.Elements<Paragraph>().First();
                        Run r1 = p1.Elements<Run>().First();
                        Text t1 = r1.Elements<Text>().First();
                        switch (i)
                        {
                            case 0: t1.Text = "DEPORTIVA"; break;
                            case 1: t1.Text = "Interés por el entrenamiento y rendimiento  físico de las personas en actividades deportivas."; break;
                            default: t1.Text = String.Concat("Ciencias del Deporte                                    ",
                                                             "Preparador Físico                                              ",
                                                             "Administración y Negocios del Deporte                   ");
                                break;
                        }

                    }

                    break;
                case "DIS":
                    for (int i = 0; i < 3; i++)
                    {
                        TableRow row1 = table.Elements<TableRow>().ElementAt(numRow + 1);
                        TableCell cell1 = row1.Elements<TableCell>().ElementAt(i);
                        Paragraph p1 = cell1.Elements<Paragraph>().First();
                        Run r1 = p1.Elements<Run>().First();
                        Text t1 = r1.Elements<Text>().First();
                        switch (i)
                        {
                            case 0: t1.Text = "DISEÑO"; break;
                            case 1: t1.Text = "Interés en el diseño de diversos objetos como pequeñas obras de arte empleadas en campos como la decoración, la moda, la industria entre otros así como el diseño de casas y ambientes de trabajo y recreación para las personas."; break;
                            default: t1.Text = String.Concat("Dirección de Artes Gráficas y Publicitarias",
                                                             "                                                           ",
                                                             "Diseño Publicitario                                        ",
                                                             "Diseño de Moda                                              ",
                                                             "Diseño Gráfico                                             ",
                                                             "Diseño Industrial                                         ",
                                                             "Arquitectura                                              ",
                                                             "Arquitectura de Interiores                                ",
                                                             "Diseño de Interiores                                      ");
                                break;
                        }

                    }

                    break;
                case "FIN":
                    for (int i = 0; i < 3; i++)
                    {
                        TableRow row1 = table.Elements<TableRow>().ElementAt(numRow + 1);
                        TableCell cell1 = row1.Elements<TableCell>().ElementAt(i);
                        Paragraph p1 = cell1.Elements<Paragraph>().First();
                        Run r1 = p1.Elements<Run>().First();
                        Text t1 = r1.Elements<Text>().First();
                        switch (i)
                        {
                            case 0: t1.Text = "FINANCIERA"; break;
                            case 1: t1.Text = "Interés en la toma de decisiones sobre las finanzas, el registro y el control de los ingresos y egresos de una empresa u organización económica con la finalidad de obtener las mayores ganancias."; break;
                            default: t1.Text = String.Concat("Administración y Finanzas                              ",
                                                             "Contabilidad                                           ",
                                                             "Contabilidad y Administración                          ",
                                                             "Economía                                               ");
                                break;
                        }

                    }
                    break;

                case "INF":
                    for (int i = 0; i < 3; i++)
                    {
                        TableRow row1 = table.Elements<TableRow>().ElementAt(numRow + 1);
                        TableCell cell1 = row1.Elements<TableCell>().ElementAt(i);
                        Paragraph p1 = cell1.Elements<Paragraph>().First();
                        Run r1 = p1.Elements<Run>().First();
                        Text t1 = r1.Elements<Text>().First();
                        switch (i)
                        {
                            case 0: t1.Text = "INFORMÁTICA"; break;
                            case 1: t1.Text = "Interés en el diseño y control de los sistemas de almacenamiento, procesamiento y protección de la información."; break;
                            default: t1.Text = String.Concat("Ciencias de la Computación                            ",
                                                             "Diseño Web",
                                                             "                                                            ",
                                                             "Informática",
                                                             "                                                             ",
                                                             "Informática Empresarial                                   ",
                                                             "Ingeniería de Sistemas                                    ",
                                                             "Ingeniería de Sistemas de Información                ",
                                                             "                                      ",
                                                             "Ingeniería de Software                                    ",
                                                             "Seguridad Informática");
                                break;
                        }

                    }
                    break;

                case "JUR":
                    for (int i = 0; i < 3; i++)
                    {
                        TableRow row1 = table.Elements<TableRow>().ElementAt(numRow + 1);
                        TableCell cell1 = row1.Elements<TableCell>().ElementAt(i);
                        Paragraph p1 = cell1.Elements<Paragraph>().First();
                        Run r1 = p1.Elements<Run>().First();
                        Text t1 = r1.Elements<Text>().First();
                        switch (i)
                        {
                            case 0: t1.Text = "JURÍDICA"; break;
                            case 1: t1.Text = "Interés en la defensa de los derechos y la asesoría jurídica y legal de las personas así como en ser mediador entre dos partes para llegar a acuerdos."; break;
                            default: t1.Text = String.Concat("Derecho                                                       ",
                                                             "Derecho y Ciencias políticas                         ");
                                break;
                        }

                    }
                    break;

                case "MAR":
                    for (int i = 0; i < 3; i++)
                    {
                        TableRow row1 = table.Elements<TableRow>().ElementAt(numRow + 1);
                        TableCell cell1 = row1.Elements<TableCell>().ElementAt(i);
                        Paragraph p1 = cell1.Elements<Paragraph>().First();
                        Run r1 = p1.Elements<Run>().First();
                        Text t1 = r1.Elements<Text>().First();
                        switch (i)
                        {
                            case 0: t1.Text = "MARKETING"; break;
                            case 1: t1.Text = "Interés en la identificación de las necesidades de los consumidores para ofrecerles productos y servicios competitivos en el mercado."; break;
                            default: t1.Text = String.Concat("Administración y Marketing                           ",
                                                             "Comunicación e Imagen Empresarial                               ",
                                                             "Comunicación y Marketing                             ",
                                                             "Publicidad                                                ",
                                                             "Comunicación y Publicidad                                  ");
                                break;
                        }

                    }
                    break;


                case "MEC":
                    for (int i = 0; i < 3; i++)
                    {
                        TableRow row1 = table.Elements<TableRow>().ElementAt(numRow + 1);
                        TableCell cell1 = row1.Elements<TableCell>().ElementAt(i);
                        Paragraph p1 = cell1.Elements<Paragraph>().First();
                        Run r1 = p1.Elements<Run>().First();
                        Text t1 = r1.Elements<Text>().First();
                        switch (i)
                        {
                            case 0: t1.Text = "MECANICO/ELECTRONICA"; break;
                            case 1: t1.Text = "Interés en el funcionamiento y manejo de sistemas, máquinas y herramientas de tipo mecánico, eléctrico o electrónico. "; break;
                            default: t1.Text = String.Concat("Ingeniería Electrónica                                   ",
                                                             "Ingeniería Mecánica                                         ",
                                                             "Ingeniería Mecatrónica                                   ");
                                break;
                        }

                    }
                    break;

                case "MIN":
                    for (int i = 0; i < 3; i++)
                    {
                        TableRow row1 = table.Elements<TableRow>().ElementAt(numRow + 1);
                        TableCell cell1 = row1.Elements<TableCell>().ElementAt(i);
                        Paragraph p1 = cell1.Elements<Paragraph>().First();
                        Run r1 = p1.Elements<Run>().First();
                        Text t1 = r1.Elements<Text>().First();
                        switch (i)
                        {
                            case 0: t1.Text = "MINERA"; break;
                            case 1: t1.Text = "Interés por el estudio, la búsqueda y la extracción de los recursos minerales. "; break;
                            default: t1.Text = String.Concat("Ingeniería de Gestión Minera                         ",
                                                             "Ingeniería de Minas                                  ");
                                break;
                        }

                    }
                    break;

                case "PED":
                    for (int i = 0; i < 3; i++)
                    {
                        TableRow row1 = table.Elements<TableRow>().ElementAt(numRow + 1);
                        TableCell cell1 = row1.Elements<TableCell>().ElementAt(i);
                        Paragraph p1 = cell1.Elements<Paragraph>().First();
                        Run r1 = p1.Elements<Run>().First();
                        Text t1 = r1.Elements<Text>().First();
                        switch (i)
                        {
                            case 0: t1.Text = "PEDAGOGÍA"; break;
                            case 1: t1.Text = "Interés en la mejora de los procesos de enseñanza y  aprendizaje."; break;
                            default: t1.Text = String.Concat("Educación                                                 ",
                                                             "Educación Especial                                   ",
                                                             "Educación y Gestión del Aprendizaje                  ");
                                break;
                        }

                    }
                    break;
                case "SAL":
                    for (int i = 0; i < 3; i++)
                    {
                        TableRow row1 = table.Elements<TableRow>().ElementAt(numRow + 1);
                        TableCell cell1 = row1.Elements<TableCell>().ElementAt(i);
                        Paragraph p1 = cell1.Elements<Paragraph>().First();
                        Run r1 = p1.Elements<Run>().First();
                        Text t1 = r1.Elements<Text>().First();
                        switch (i)
                        {
                            case 0: t1.Text = "SALUD"; break;
                            case 1: t1.Text = "Interés en la atención de las necesidades de salud de las personas así como en la  orientación que se les puede brindar para mejorar su calidad de vida."; break;
                            default: t1.Text = String.Concat("Enfermería                                                     ",
                                                             "Medicina                                                       ",
                                                             "Odontología                                                     ",
                                                             "Terapia Física                                                  ",
                                                             "Nutrición y Dietética");
                                break;
                        }

                    }
                    break;

                case "SOC":
                    for (int i = 0; i < 3; i++)
                    {
                        TableRow row1 = table.Elements<TableRow>().ElementAt(numRow + 1);
                        TableCell cell1 = row1.Elements<TableCell>().ElementAt(i);
                        Paragraph p1 = cell1.Elements<Paragraph>().First();
                        Run r1 = p1.Elements<Run>().First();
                        Text t1 = r1.Elements<Text>().First();
                        switch (i)
                        {
                            case 0: t1.Text = "SOCIAL"; break;
                            case 1: t1.Text = "Interés y preocupación por el bienestar de las personas orientándolas hacia el logro de sus objetivos personales."; break;
                            default: t1.Text = "Psicología";
                                break;
                        }

                    }
                    break;

                case "TRA":
                    for (int i = 0; i < 3; i++)
                    {
                        TableRow row1 = table.Elements<TableRow>().ElementAt(numRow + 1);
                        TableCell cell1 = row1.Elements<TableCell>().ElementAt(i);
                        Paragraph p1 = cell1.Elements<Paragraph>().First();
                        Run r1 = p1.Elements<Run>().First();
                        Text t1 = r1.Elements<Text>().First();
                        switch (i)
                        {
                            case 0: t1.Text = "TRADUCCIÓN"; break;
                            case 1: t1.Text = "Interés en la interpretación de la producción oral y escrita de distintos grupos sociales como una manifestación de su cultura."; break;
                            default: t1.Text = "Traducción e Interpretación Profesional";
                                break;
                        }

                    }
                    break;
                case "TUR":
                    for (int i = 0; i < 3; i++)
                    {
                        TableRow row1 = table.Elements<TableRow>().ElementAt(numRow + 1);
                        TableCell cell1 = row1.Elements<TableCell>().ElementAt(i);
                        Paragraph p1 = cell1.Elements<Paragraph>().First();
                        Run r1 = p1.Elements<Run>().First();
                        Text t1 = r1.Elements<Text>().First();
                        switch (i)
                        {
                            case 0: t1.Text = "TURISMO"; break;
                            case 1: t1.Text = "Interés por la promoción y realización de la actividad turística y hotelera de un país."; break;
                            default: t1.Text = String.Concat("Ecoturismo",
                                                             "                                                             ",
                                                             "Turismo",
                                                             "                                                           ",
                                                             "Hotelería                                                  ",
                                                             "Hotelería y Administración                                   ");
                                break;
                        }

                    }
                    break;
            }

        }


        private void RemoverTabla(WordprocessingDocument WPD, int indice)
        {
            //doc.MainDocumentPart.Document.Body.Elements

            MainDocumentPart MDP = WPD.MainDocumentPart;
            Document D = MDP.Document;
            D.Body.Elements<Table>().ElementAt(indice).Remove();

            D.Save();
        }
    }


}
