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
    /// Descripción breve de wsTemperamentos
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // Para permitir que se llame a este servicio Web desde un script, usando ASP.NET AJAX, quite la marca de comentario de la línea siguiente. 
    // [System.Web.Script.Services.ScriptService]
    public class wsTemperamentos : System.Web.Services.WebService
    {
        double puntaje_extroversion_vs_introversio_bar = 0;
        double puntaje_intuicion_vs_sensacion_bar = 0;
        double puntaje_racional_vs_emotivo_bar = 0;
        double puntaje_organizado_vs_casual_bar = 0;

        List<double> ScaleNumbersEI = new List<double>();
        List<double> ScaleNumbersIS = new List<double>();
        List<double> ScaleNumbersRE = new List<double>();
        List<double> ScaleNumbersOC = new List<double>();

        //String strGlobal

        [WebMethod]
        public string GenerarReporte(ReporteTemperamentoBE objTemperamento, bool esAdulto)
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


                            }

                            puntaje_casual += objPuntajeTemperamento.puntaje_subsection;
                            if (objPuntajeTemperamento.name_subsection != null)
                                lstPuntajeTemperamento.Add(objPuntajeTemperamento);
                        }
                        break;

                }

            }


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

            if (esAdulto)
            {
                nombrePDF = "Reporte_Temperamentos_Adulto_" + nombreEvaluado.Replace(" ", "").Replace(",", "") + ".pdf";
            }
            else
            {
                nombrePDF = "Reporte_Temperamentos_Joven_" + nombreEvaluado.Replace(" ", "").Replace(",", "") + ".pdf";
            }


            calcularReporte(nombrePDF, nombreEvaluado, fechaInicioTest, puntaje_extroversion, puntaje_introversion,
                            puntaje_intuicion, puntaje_sensacion, puntaje_racional, puntaje_emotivo, puntaje_organizado, puntaje_casual,
                            lstPuntajeTemperamento, esAdulto);

            return nombrePDF;
        }



        private void calcularReporte(string nombrePDF, string nameEvaluated, string fechaInitTest, double puntaje_extroversion,
                                     double puntaje_introversion, double puntaje_intuicion, double puntaje_sensacion,
                                     double puntaje_racional, double puntaje_emotivo, double puntaje_organizado, double puntaje_casual,
                                     List<PuntajeTemperamentoBE> lstPtsTemperamento, bool esAdulto)
        {

            bool usekey = Convert.ToBoolean(ConfigurationManager.AppSettings["USEKEY_TEMP"]);
            string nombreWord = "";
            string fileName = "";

            if (esAdulto)
            {
                nombreWord = "Reporte_Temperamentos_Adulto" + "" + ".docx";
                fileName = Server.MapPath(@"~/Reporte/Plantilla/Plantilla_Reporte_Temperamentos_adulto.docx");
            }
            else
            {
                nombreWord = "Reporte_Temperamentos_Joven" + "" + ".docx";
                fileName = Server.MapPath(@"~/Reporte/Plantilla/Plantilla_Reporte_Temperamentos_joven.docx");
            }

            string pathPDF = Server.MapPath("~/Reporte/");
            string archivoNuevo = Server.MapPath("~/Reporte/" + nombreWord);

            List<ElementoTemperamentoBE> lstElementos = new List<ElementoTemperamentoBE>();
            CarreraBC objCarreraBC = new CarreraBC();

            List<string> lstElementos_Extroversion_Introversion = new List<string>();
            List<string> lstElementos_Racional_Emocional = new List<string>();
            List<string> lstElementos_Organizado_Casual = new List<string>();
            List<string> lstElementos_Intuicion_Sensacion = new List<string>();
            List<FortalezaTemperamentoBE> lstElemtos_Fortalezas = new List<FortalezaTemperamentoBE>();

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


                using (WordprocessingDocument doc = WordprocessingDocument.Open(archivoNuevo, true))
                {
                    List<Table> lstTables = doc.MainDocumentPart.Document.Body.Elements<Table>().ToList();
                    lstElementos_Extroversion_Introversion = GenerarGrafico_Extroversion_Introversion(doc, lstTables[3], lstPtsTemperamento);
                    lstElementos_Intuicion_Sensacion = GenerarGrafico_Intuicion_Sensacion(doc, lstTables[4], lstPtsTemperamento);
                    lstElementos_Racional_Emocional = GenerarGrafico_Racional_Emocional(doc, lstTables[5], lstPtsTemperamento);
                    lstElementos_Organizado_Casual = GenerarGrafico_Organizado_Casual(doc, lstTables[6], lstPtsTemperamento);


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

                    //MostrarFortalezas(lstTables, archivoNuevo, lstElemtos_Fortalezas, strTipologiaRueda, doc);
                }



                using (WordprocessingDocument doc = WordprocessingDocument.Open(archivoNuevo, true))
                {
                    List<Table> lstTables = doc.MainDocumentPart.Document.Body.Elements<Table>().ToList();

                    /*
                    strTipologiaRueda = GenerarTipologia((puntaje_extroversion / 4), (puntaje_introversion / 4), (puntaje_sensacion / 4), (puntaje_intuicion / 4),
                                                         (puntaje_racional / 4), (puntaje_emotivo / 4), (puntaje_casual / 4), (puntaje_organizado / 4), doc, lstTables[1]);
                    */
                    strTipologiaRueda = Generar_Tipologia(puntaje_extroversion_vs_introversio_bar, puntaje_intuicion_vs_sensacion_bar,
                                     puntaje_racional_vs_emotivo_bar, puntaje_organizado_vs_casual_bar, doc, lstTables[1]);

                    string straux = objCarreraBC.ObtenerDescripcionRueda(strTipologiaRueda.ToUpper());
                    string L1 = strTipologiaRueda.Substring(0, 1);
                    string L2 = strTipologiaRueda.Substring(1, 1);
                    string L3 = strTipologiaRueda.Substring(2, 1);
                    string L4 = strTipologiaRueda.Substring(3, 1);

                    SearchAndReplace(doc, fileName, "L1", L1);
                    SearchAndReplace(doc, fileName, "L2", L2);
                    SearchAndReplace(doc, fileName, "L3", L3);
                    SearchAndReplace(doc, fileName, "L4", L4);



                    SearchAndReplace(doc, fileName, "TIPOLOGIA", "(" + strTipologiaRueda + ")");
                    SearchAndReplace(doc, fileName, "DescripcionRueda", straux);

                    MostrarAreas_DescripcionSuperRueda(doc, fileName, L1, L2, L3, L4);

                    lstElemtos_Fortalezas = objCarreraBC.ObtenerFortalezas(L1.ToUpper(),
                                                                           L2.ToUpper(),
                                                                           L3.ToUpper(),
                                                                           L4.ToUpper());


                    strTipologiaRueda = strTipologiaRueda.ToUpper();

                    MostrarFortalezas(lstTables, archivoNuevo, lstElemtos_Fortalezas, strTipologiaRueda, doc);

                }


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

        private void MostrarFortalezas(List<Table> lstTables, string fileName, List<FortalezaTemperamentoBE> lstFortalezas, string strTipologiaRueda, WordprocessingDocument doc)
        {

            try
            {
                string L1 = strTipologiaRueda.Substring(0, 1);
                string L2 = strTipologiaRueda.Substring(1, 1);
                string L3 = strTipologiaRueda.Substring(2, 1);
                string L4 = strTipologiaRueda.Substring(3, 1);

                if (lstFortalezas.Count > 0)
                {
                    Table table = null;
                    table = lstTables[7];

                    TableRow rt1 = table.Elements<TableRow>().ElementAt(1);
                    Table table1 = rt1.Elements<TableCell>().ElementAt(1).Elements<Table>().First();


                    TableRow rt2 = table.Elements<TableRow>().ElementAt(2);
                    Table table2 = rt2.Elements<TableCell>().ElementAt(1).Elements<Table>().First();

                    TableRow rt3 = table.Elements<TableRow>().ElementAt(3);
                    Table table3 = rt3.Elements<TableCell>().ElementAt(1).Elements<Table>().First();

                    TableRow rt4 = table.Elements<TableRow>().ElementAt(4);
                    Table table4 = rt4.Elements<TableCell>().ElementAt(1).Elements<Table>().First();


                    int cont = 1;
                    int contTal = 1;
                    int fila = 0;
                    int tope = 13;

                    contTal = 1;
                    TableRow row1 = table.Elements<TableRow>().ElementAt(1);
                    TableCell cell1 = row1.Elements<TableCell>().ElementAt(0);
                    Paragraph p1 = cell1.Elements<Paragraph>().First();
                    Run r1 = p1.Elements<Run>().First();
                    Text t1 = r1.Elements<Text>().First();

                    int indiceNombreFortaleza = 0;


                    for (int i = 0; i < lstFortalezas.Count; i++)
                    {
                        if (lstFortalezas[i].eje.Equals(L1))
                        {
                            indiceNombreFortaleza = i;
                            TableRow row = table1.Elements<TableRow>().ElementAt(fila);
                            TableCell cell = row.Elements<TableCell>().ElementAt(0);
                            Paragraph p = cell.Elements<Paragraph>().First();
                            Run r = p.Elements<Run>().First();
                            Text t = r.Elements<Text>().First();
                            t.Text = lstFortalezas[i].descripcion;
                            cont++;
                            contTal++;
                            fila++;
                        }

                    }

                    t1.Text = lstFortalezas[indiceNombreFortaleza - 1].nombre;

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


                    TableRow row2 = table.Elements<TableRow>().ElementAt(2);
                    TableCell cell2 = row2.Elements<TableCell>().ElementAt(0);
                    Paragraph p2 = cell2.Elements<Paragraph>().First();
                    Run r2 = p2.Elements<Run>().First();
                    Text t2 = r2.Elements<Text>().First();

                    fila = 0;

                    for (int i = 0; i < lstFortalezas.Count; i++)
                    {
                        if (lstFortalezas[i].eje.Equals(L2))
                        {
                            indiceNombreFortaleza = i;
                            TableRow row = table2.Elements<TableRow>().ElementAt(fila);
                            TableCell cell = row.Elements<TableCell>().ElementAt(0);
                            Paragraph p = cell.Elements<Paragraph>().First();
                            Run r = p.Elements<Run>().First();
                            Text t = r.Elements<Text>().First();
                            t.Text = lstFortalezas[i].descripcion;
                            cont++;
                            contTal++;
                            fila++;
                        }

                    }

                    t2.Text = lstFortalezas[indiceNombreFortaleza - 1].nombre;

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


                    TableRow row3 = table.Elements<TableRow>().ElementAt(3);
                    TableCell cell3 = row3.Elements<TableCell>().ElementAt(0);
                    Paragraph p3 = cell3.Elements<Paragraph>().First();
                    Run r3 = p3.Elements<Run>().First();
                    Text t3 = r3.Elements<Text>().First();

                    fila = 0;

                    for (int i = 0; i < lstFortalezas.Count; i++)
                    {
                        if (lstFortalezas[i].eje.Equals(L3))
                        {
                            indiceNombreFortaleza = i;
                            TableRow row = table3.Elements<TableRow>().ElementAt(fila);
                            TableCell cell = row.Elements<TableCell>().ElementAt(0);
                            Paragraph p = cell.Elements<Paragraph>().First();
                            Run r = p.Elements<Run>().First();
                            Text t = r.Elements<Text>().First();
                            t.Text = lstFortalezas[i].descripcion;
                            cont++;
                            contTal++;
                            fila++;
                        }

                    }

                    t3.Text = lstFortalezas[indiceNombreFortaleza].nombre;

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

                    TableRow row4 = table.Elements<TableRow>().ElementAt(4);
                    TableCell cell4 = row4.Elements<TableCell>().ElementAt(0);
                    Paragraph p4 = cell4.Elements<Paragraph>().First();
                    Run r4 = p4.Elements<Run>().First();
                    Text t4 = r4.Elements<Text>().First();

                    fila = 0;

                    for (int i = 0; i < lstFortalezas.Count; i++)
                    {
                        if (lstFortalezas[i].eje.Equals(L4))
                        {
                            indiceNombreFortaleza = i;
                            TableRow row = table4.Elements<TableRow>().ElementAt(fila);
                            TableCell cell = row.Elements<TableCell>().ElementAt(0);
                            Paragraph p = cell.Elements<Paragraph>().First();
                            Run r = p.Elements<Run>().First();
                            Text t = r.Elements<Text>().First();
                            t.Text = lstFortalezas[i].descripcion;
                            cont++;
                            contTal++;
                            fila++;
                        }

                    }

                    t4.Text = lstFortalezas[indiceNombreFortaleza].nombre;

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

                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
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
            return puntaje;
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

            /*
            puntajeIS = calcularPuntajeSubSection("CONCEPTUAL", "APLICADOR", lstPT).ToString();
            puntaje_intuicion_vs_sensacion_bar = (puntaje_intuicion_vs_sensacion_bar + Convert.ToDouble(puntajeIS));
            imagen = pathImages + puntajeIS.Replace(".", "_") + ".png";

            switch (puntajeIS)
            {
                case "7":
                case "6.5":
                case "6":
                case "5.5":
                case "5":
                case "4.5":
                    rowIS = table.Elements<TableRow>().ElementAt(1);
                    cellIS = rowIS.Elements<TableCell>().ElementAt(4);
                    AddImage(cellIS, doc.MainDocumentPart, imagen, "imId22");
                    lstElementos_resultado.Add("CONCEPTUAL");
                    break;

                case "4":
                    //Mostrar Barra hacia arriba
                    rowIS = table.Elements<TableRow>().ElementAt(1);
                    cellIS = rowIS.Elements<TableCell>().ElementAt(4);
                    AddImage(cellIS, doc.MainDocumentPart, imagen, "imId22");

                    //Mostrar Barra hacia abajo
                    rowIS = table.Elements<TableRow>().ElementAt(2);
                    cellIS = rowIS.Elements<TableCell>().ElementAt(4);
                    AddImage(cellIS, doc.MainDocumentPart, imagen, "imId22_1");

                    lstElementos_resultado.Add("CONCEPTUAL/APLICADOR");
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
                    cellIS = rowIS.Elements<TableCell>().ElementAt(4);
                    AddImage(cellIS, doc.MainDocumentPart, imagen, "imId22");
                    lstElementos_resultado.Add("APLICADOR");
                    break;
            }
            */

            return lstElementos_resultado;

        }


        private List<string> GenerarGrafico_Racional_Emocional(WordprocessingDocument doc, Table table, List<PuntajeTemperamentoBE> lstPT)
        {
            string pathImages = Server.MapPath("~/Images/Temperamentos/");

            double dblPuntajeOC = calcularPuntajeSubSection("OBJETIVO", "COMPASIVO", lstPT);
            string puntajeOC = round(dblPuntajeOC).ToString();
            puntaje_racional_vs_emotivo_bar = dblPuntajeOC;

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

            /*
            puntajeOC = calcularPuntajeSubSection("CUESTIONADOR", "CONCILIADOR", lstPT).ToString();
            puntaje_racional_vs_emotivo_bar = (puntaje_racional_vs_emotivo_bar + Convert.ToDouble(puntajeOC));
            imagen = pathImages + puntajeOC.Replace(".", "_") + ".png";

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
                    AddImage(cell, doc.MainDocumentPart, imagen, "imId23");
                    lstElemento_resultado.Add("CUESTIONADOR");
                    break;
                case "4":
                    row = table.Elements<TableRow>().ElementAt(1);
                    cell = row.Elements<TableCell>().ElementAt(3);
                    AddImage(cell, doc.MainDocumentPart, imagen, "imId23");

                    row = table.Elements<TableRow>().ElementAt(2);
                    cell = row.Elements<TableCell>().ElementAt(3);
                    AddImage(cell, doc.MainDocumentPart, imagen, "imId23_1");

                    lstElemento_resultado.Add("CUESTIONADOR/CONCILIADOR");
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
                    AddImage(cell, doc.MainDocumentPart, imagen, "imId23");
                    lstElemento_resultado.Add("CONCILIADOR");
                    break;

            }
            */

            dblPuntajeOC = calcularPuntajeSubSection("DIRECTO", "EMPATICO", lstPT);
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

            /*
            puntajeRE = calcularPuntajeSubSection("CERRAR E IMPLEMENTAR", "EXPLORA ALTERNATIVAS", lstPT).ToString();
            puntaje_organizado_vs_casual_bar = (puntaje_organizado_vs_casual_bar + Convert.ToDouble(puntajeRE));
            imagen = pathImages + puntajeRE.Replace(".", "_") + ".png";

            switch (puntajeRE)
            {
                case "7":
                case "6.5":
                case "6":
                case "5.5":
                case "5":
                case "4.5":
                    row = table.Elements<TableRow>().ElementAt(1);
                    cell = row.Elements<TableCell>().ElementAt(4);
                    AddImage(cell, doc.MainDocumentPart, imagen, "imId26");
                    lstElementos_resultado.Add("CERRAR E IMPLEMENTAR");
                    break;

                case "4":
                    row = table.Elements<TableRow>().ElementAt(1);
                    cell = row.Elements<TableCell>().ElementAt(4);
                    AddImage(cell, doc.MainDocumentPart, imagen, "imId26");

                    row = table.Elements<TableRow>().ElementAt(2);
                    cell = row.Elements<TableCell>().ElementAt(4);
                    AddImage(cell, doc.MainDocumentPart, imagen, "imId26_1");

                    lstElementos_resultado.Add("CERRAR E IMPLEMENTAR/EXPLORA ALTERNATIVAS");
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
                    AddImage(cell, doc.MainDocumentPart, imagen, "imId26");
                    lstElementos_resultado.Add("EXPLORA ALTERNATIVAS");
                    break;

            }
            */


            return lstElementos_resultado;

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

        private double calcularEDT(double puntaje1, double puntaje2)
        {
            double puntaje = (puntaje1 + puntaje2) / 2;
            if (puntaje == 0) puntaje = 0.5;

            return puntaje;
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
                case "0": puntaje = 0;
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
                case "0": puntaje = 0;
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
    }
}

