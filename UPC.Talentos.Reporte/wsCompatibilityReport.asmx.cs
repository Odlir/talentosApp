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
    /// Descripción breve de wsCompatibilityReport
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // Para permitir que se llame a este servicio Web desde un script, usando ASP.NET AJAX, quite la marca de comentario de la línea siguiente. 
    // [System.Web.Script.Services.ScriptService]
    public class wsCompatibilityReport : System.Web.Services.WebService
    {


        [WebMethod]
        public string GenerarReporte(string codUser, ReporteTemperamentoBE objTemperamento, ReporteBE objInteresBE)
        {

            List<PuntajeTemperamentoBE> lstPuntajeTemperamento = new List<PuntajeTemperamentoBE>();
            PuntajeTemperamentoBE objPuntajeTemperamento = null;

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
                                 * */


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


            nombrePDF = "Reporte_Compatibilidad_" + nombreEvaluado.Replace(" ", "").Replace(",", "") + ".pdf";



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







            calcularReporteCompatibilidad(nombrePDF, nombreEvaluado, fechaInicioTest, puntaje_extroversion, puntaje_introversion,
                            puntaje_intuicion, puntaje_sensacion, puntaje_racional, puntaje_emotivo, puntaje_organizado, puntaje_casual,
                            lstPuntajeTemperamento, true, codUser,
                            countADM, countAGR, countART, countCOM, countCON, countCUL, countDEP, countDIS, countFIN, countINF, countJUR, countMAR, countMEC, countMIN, countPED, countSAL, countSOC, countTRA, countTUR);


            return nombrePDF;
        }

        private void calcularReporteCompatibilidad(string nombrePDF, string nameEvaluated, string fechaInitTest, double puntaje_extroversion,
                                     double puntaje_introversion, double puntaje_intuicion, double puntaje_sensacion,
                                     double puntaje_racional, double puntaje_emotivo, double puntaje_organizado, double puntaje_casual,
                                     List<PuntajeTemperamentoBE> lstPtsTemperamento, bool esAdulto, string codUser,
                                     int countADM, int countAGR, int countART, int countCOM, int countCON, int countCUL, int countDEP,
                                     int countDIS, int countFIN, int countINF, int countJUR, int countMAR, int countMEC, int countMIN,
                                     int countPED, int countSAL, int countSOC, int countTRA, int countTUR)
        {

            string nombreWord = "";
            string fileName = "";

            double puntaje_extroversion_vs_introversion = (puntaje_extroversion + puntaje_introversion) / 8;
            double puntaje_intuicion_vs_sensacion = (puntaje_intuicion + puntaje_sensacion) / 6;
            double puntaje_racional_vs_emotivo = (puntaje_racional + puntaje_emotivo) / 6;
            double puntaje_organizado_vs_casual = (puntaje_organizado + puntaje_casual) / 6;


            nombreWord = "Reporte_Compatibilidad" + "" + ".docx";
            fileName = Server.MapPath(@"~/Reporte/Plantilla/Plantilla_Reporte_Compatibilidad.docx");



            string pathPDF = Server.MapPath("~/Reporte/");
            string archivoNuevo = Server.MapPath("~/Reporte/" + nombreWord);


            //List<ElementoTemperamentoBE> lstElementos = new List<ElementoTemperamentoBE>();
            CarreraBC objCarreraBC = new CarreraBC();


            List<ResultadoComFacultadBE> lstResultaldoFacultad = new List<ResultadoComFacultadBE>();
            List<ResultadoComCampoLaboralBE> lstResultaldoCampoLaboral = new List<ResultadoComCampoLaboralBE>();

            ResultadoBC objResultadoBC = new ResultadoBC();
            lstResultaldoFacultad = objResultadoBC.getCompatibilidadxFacultad(codUser,
                                                                             puntaje_extroversion_vs_introversion,
                                                                             puntaje_intuicion_vs_sensacion,
                                                                             puntaje_racional_vs_emotivo,
                                                                             puntaje_organizado_vs_casual);

            lstResultaldoCampoLaboral = objResultadoBC.getCompatibilidadxCampoLaboral(codUser,
                                                                                puntaje_extroversion_vs_introversion,
                                                                                puntaje_intuicion_vs_sensacion,
                                                                                puntaje_racional_vs_emotivo,
                                                                                puntaje_organizado_vs_casual);


            List<PuntajeTotalInteresBE> lstPT = null;

            try
            {
                EliminarArchivosTemporales(archivoNuevo, "");
                File.Copy(fileName, archivoNuevo);

                //Setea en doc el nombre del evaluado
                SearchAndReplace(archivoNuevo, "#NOMBRE_PERSONA#", nameEvaluated);

                //Setea en doc la fecha que inicio el test
                SearchAndReplace(archivoNuevo, "#FECHA_TEST#", fechaInitTest);

                SearchAndReplace(archivoNuevo, "#NOMBRE_PERSONA#", nameEvaluated);
                SearchAndReplace(archivoNuevo, "#NOMBRE_PERSONA#", nameEvaluated);
                SearchAndReplace(archivoNuevo, "#NOMBRE_PERSONA#", nameEvaluated);


                /*
                string[] names = nameEvaluated.Split(' ');
                string name = "";
                for (int i = 0; i < names.Length-1 ; i++ )
                {
                    name+= names[i];
                }

                SearchAndReplace(archivoNuevo, "#name", name);
                */

                using (WordprocessingDocument doc = WordprocessingDocument.Open(archivoNuevo, true))
                {
                    List<Table> lstTables = doc.MainDocumentPart.Document.Body.Elements<Table>().ToList();

                    lstPT = CalculoPT(lstTables[0], countADM, countAGR, countART, countCOM, countCON, countCUL,
                                      countDEP, countDIS, countFIN, countINF, countJUR, countMAR, countMEC,
                                      countMIN, countPED, countSAL, countSOC, countTRA, countTUR);

                    //Puntaje Test Intereses
                    MostrarPuntajesTest(lstTables[0], lstPT);
                    MostrarBarrasPT(doc, lstTables[0], lstPT);

                    //Compatibilidad por Facultad
                    AppendResultFacultad(doc, lstTables[1], lstResultaldoFacultad);
                    AppendResultCampoLaboral(doc, lstTables[2], lstResultaldoCampoLaboral);

                }


                //Libreria Externa
                //ConvertirWordToPDF(archivoNuevo, nombreWord, pathPDF, nombrePDF);

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




                EliminarArchivosTemporales(archivoNuevo, "");
            }
            catch (Exception ex)
            {
                throw ex;
            }

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

        private int CalculoPuntajeInteres(int puntaje)
        {

            double puntajeaux = puntaje;
            puntajeaux = (puntajeaux / 6) * 4;
            return Convert.ToInt32(puntajeaux);
        }


        private void AppendResultFacultad(WordprocessingDocument doc, Table table, List<ResultadoComFacultadBE> lstComFacultad)
        {
            string pathImages = Server.MapPath("~/Images/Compatibilidad/Graybars/");
            string imgId = "imdf";
            string image = "";
            int lst_size = lstComFacultad.Count;
            int intCoincidencia = 0;
            TableRow row = null;
            TableCell cell = null;
            Paragraph paragraph = null;
            Run run = null;
            Text text = null;

            for (int i = 0; i < lst_size; i++)
            {
                row = table.Elements<TableRow>().ElementAt(1 + i);
                for (int j = 0; j < 3; j++)
                {
                    cell = row.Elements<TableCell>().ElementAt(j);
                    paragraph = cell.Elements<Paragraph>().First();
                    run = paragraph.Elements<Run>().First();
                    text = run.Elements<Text>().First();
                    if (j == 0) //[celda 0] Nombre Facultad
                        text.Text = lstComFacultad[i].nombreFacultad.Trim();
                    else if (j == 1) // [celda 1] Porcentaje de compatibilidad Facultad
                        text.Text = lstComFacultad[i].coincidencia.ToString() + "%";
                    else //[celda 2] Imagen
                    {
                        intCoincidencia = lstComFacultad[i].coincidencia;
                        if (intCoincidencia > 0) //Validar cuando el valor de coincidencia sea 0
                        {
                            image = pathImages + Convert.ToString(intCoincidencia) + ".png";
                            imgId = imgId + "" + i;
                            AddImage(cell, doc.MainDocumentPart, image, imgId);
                        }
                    }

                }




            }

        }


        private void AppendResultCampoLaboral(WordprocessingDocument doc, Table table, List<ResultadoComCampoLaboralBE> lstComCampoLaboral)
        {

            int lst_size = lstComCampoLaboral.Count;
            int lst_sizeCarreras = 0;
            List<String> lstCarrerasAsociadas = new List<string>();
            TableRow row = null;
            TableCell cell = null;
            Paragraph paragraph = null;
            Run run = null;
            Text text = null;

            int fila = 0;
            int tope = 13;

            for (int i = 0; i < lst_size; i++)
            {
                /*
                if (i > 9)
                    break;
                */

                row = table.Elements<TableRow>().ElementAt(1 + i);
                for (int j = 0; j < 2; j++)
                {
                    cell = row.Elements<TableCell>().ElementAt(j);
                    paragraph = cell.Elements<Paragraph>().First();
                    run = paragraph.Elements<Run>().First();
                    text = run.Elements<Text>().First();
                    if (j == 0) //[celda 0] Nombre Campo Laboral
                        text.Text = lstComCampoLaboral[i].nombreCamboLaboral.Trim();
                    else // [celda 1] Porcentaje de compatibilidad Campo Laboral
                        text.Text = lstComCampoLaboral[i].coincidencia.ToString() + "%";
                }

                lstCarrerasAsociadas = lstComCampoLaboral[i].lstCarreras;
                lst_sizeCarreras = lstCarrerasAsociadas.Count;

                Table tableCarreras = row.Elements<TableCell>().ElementAt(2).Elements<Table>().First();

                for (int k = 0; k < lst_sizeCarreras; k++)
                {
                    TableRow rowCarreras = tableCarreras.Elements<TableRow>().ElementAt(fila);
                    TableCell cellCarreras = rowCarreras.Elements<TableCell>().ElementAt(0);
                    Paragraph p = cellCarreras.Elements<Paragraph>().First();
                    Run r = p.Elements<Run>().First();
                    Text t = r.Elements<Text>().First();
                    t.Text = lstCarrerasAsociadas[k].ToString();
                    fila++;
                }

                for (int k = fila; k <= tope; k++)
                {
                    TableRow rowCarreras = tableCarreras.Elements<TableRow>().Last();
                    TableCell cellCarreras = rowCarreras.Elements<TableCell>().ElementAt(0);
                    Paragraph p = cellCarreras.Elements<Paragraph>().First();
                    Run r = p.Elements<Run>().First();
                    Text t = r.Elements<Text>().First();

                    if (t.Text.Trim().StartsWith("#CAREER"))
                    {
                        cellCarreras.RemoveAllChildren();
                        cellCarreras.Remove();
                        rowCarreras.RemoveAllChildren();
                        rowCarreras.Remove();
                    }
                }

                fila = 0;



            }

        }

        /* intereses start */
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
                        break;
                    case "AGR":
                        row1 = table.Elements<TableRow>().ElementAt(3);
                        break;
                    case "ART":
                        row1 = table.Elements<TableRow>().ElementAt(4);
                        break;
                    case "COM":
                        row1 = table.Elements<TableRow>().ElementAt(5);
                        break;
                    case "CON":
                        row1 = table.Elements<TableRow>().ElementAt(6);
                        break;
                    case "CUL":
                        row1 = table.Elements<TableRow>().ElementAt(7);
                        break;
                    case "DEP":
                        row1 = table.Elements<TableRow>().ElementAt(8);
                        break;
                    case "DIS":
                        row1 = table.Elements<TableRow>().ElementAt(9);
                        break;
                    case "FIN":
                        row1 = table.Elements<TableRow>().ElementAt(10);
                        break;
                    case "INF":
                        row1 = table.Elements<TableRow>().ElementAt(11);
                        break;
                    case "JUR":
                        row1 = table.Elements<TableRow>().ElementAt(12);
                        break;
                    case "MAR":
                        row1 = table.Elements<TableRow>().ElementAt(13);
                        break;
                    case "MEC":
                        row1 = table.Elements<TableRow>().ElementAt(14);
                        break;
                    case "MIN":
                        row1 = table.Elements<TableRow>().ElementAt(15);
                        break;
                    case "PED":
                        row1 = table.Elements<TableRow>().ElementAt(16);
                        break;
                    case "SAL":
                        row1 = table.Elements<TableRow>().ElementAt(17);
                        break;
                    case "SOC":
                        row1 = table.Elements<TableRow>().ElementAt(18);
                        break;
                    case "TRA":
                        row1 = table.Elements<TableRow>().ElementAt(19);
                        break;
                    case "TUR":
                        row1 = table.Elements<TableRow>().ElementAt(20);
                        break;
                }


                TableCell cell1 = row1.Elements<TableCell>().ElementAt(1);
                Paragraph p1 = cell1.Elements<Paragraph>().First();
                Run r1 = p1.Elements<Run>().First();
                Text t1 = r1.Elements<Text>().First();
                t1.Text = lstPT[i].Puntaje.ToString() + "%";

            }

        }


        private void MostrarBarrasPT(WordprocessingDocument doc, Table table, List<PuntajeTotalInteresBE> lstPT)
        {
            string pathImages = Server.MapPath("~/Images/Compatibilidad/Redbars/");

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

        /* intereses  end */

        public static void SearchAndReplace(WordprocessingDocument doc, string fileName, string searchText, string newText)
        {
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
                data.Add("OutputFileName", nombrePDF); //Optional
                data.Add("ApiKey", strApiKey);
                string paso = "";

                try
                {
                    client.QueryString.Add(data);
                    paso = "Antes Upload";
                    var response = client.UploadFile("http://do.convertapi.com/Word2Pdf", fileToConvert);
                    paso = "Upload Paso";
                    var responseHeaders = client.ResponseHeaders;
                    var path = Path.Combine(filePDF, responseHeaders["OutputFileName"]);
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

    }
}
