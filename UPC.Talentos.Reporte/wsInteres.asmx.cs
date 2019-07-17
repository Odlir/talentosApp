using System;
using System.Collections.Generic;
using System.Linq;
using System.Configuration;
using System.Web.Services;
using UPC.Talentos.Reporte.BL.BE;
using UPC.Talentos.Reporte.BL.BC;
using DocumentFormat.OpenXml.Packaging;
using System.IO;
using DocumentFormat.OpenXml.Wordprocessing;
using System.Net;
using System.Collections.Specialized;
using wp = DocumentFormat.OpenXml.Drawing.Wordprocessing;
using a = DocumentFormat.OpenXml.Drawing;
using pic = DocumentFormat.OpenXml.Drawing.Pictures;
using System.Drawing;
using DocumentFormat.OpenXml;
using System.Globalization;



namespace UPC.Talentos.Reporte
{
    /// <summary>
    /// Summary description for wsInteres
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    // [System.Web.Script.Services.ScriptService]
    public class wsInteres : System.Web.Services.WebService
    {

        [WebMethod]
        public string CalcularReporteIntereses(ReporteBE objInteresBE)
        {
            
            string nombrePDF = "";
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

            string respuesta = "";
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
                respuesta = (objInteresBE.report.result[i].answer == null) ? "0" : objInteresBE.report.result[i].answer.ToUpper().Trim();
                switch (respuesta)
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
                respuesta = (objInteresBE.report.result[i + 1].answer == null) ? "0" : objInteresBE.report.result[i + 1].answer.ToUpper().Trim();
                switch (respuesta)
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
                respuesta = (objInteresBE.report.result[i + 2].answer == null) ? "0" : objInteresBE.report.result[i + 2].answer.ToUpper().Trim();
                switch (respuesta)
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
            objSAL.Puntaje = CalculoPuntaje(countSAL); //Calculo especial por tener 6 preguntas
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

            string fechaInicioTest = "";

            if (!String.IsNullOrEmpty(objInteresBE.report.initdate.date))
            {
                fechaInicioTest = objInteresBE.report.initdate.date.Substring(0, 10); //Año - Mes - Dia
                string[] fechas = fechaInicioTest.Split('-');
                fechaInicioTest = String.Concat(fechas[2], "-", fechas[1], "-", fechas[0]); //Dia- Mes - Año
            }

            string nombre = "";
            if (!string.IsNullOrEmpty(objInteresBE.report.evaluated.name))
                //nombre = objInteresBE.report.evaluated.name.Substring(0, 10);
                nombre = objInteresBE.report.evaluated.name.Substring(0, objInteresBE.report.evaluated.name.IndexOf(','));
            else
                nombre = objInteresBE.report.evaluator.name;

            string nombreaux = nombre.ToUpper();
            nombreaux = nombreaux.Replace("Ó", "O");
            nombreaux = nombreaux.Replace("Í", "I");
            nombreaux = nombreaux.Replace("Á", "A");
            nombreaux = nombreaux.Replace("É", "E");
            nombreaux = nombreaux.Replace("Ú", "U");
            nombreaux = nombreaux.Replace("Ñ", "N");
            nombrePDF = "Reporte_Intereses_" + nombreaux.Replace(" ", "").Replace(",", "") + ".pdf";

            try
            {
                CalcularReporte(lstPuntajeTotal, countADM, countAGR, countART, countCOM, countCON, countCUL, countDEP, countDIS, countFIN, countINF, countJUR, countMAR, countMEC, countMIN, countPED, countSAL, countSOC, countTRA, countTUR, nombrePDF, nombre, fechaInicioTest);
                //CalcularDatosReporte(lstPuntajeTotal, countAGR, countART, countCOM, countCON, countDEP, countDIS, countEMP, countINF, countJUR, countMIN, countPED, countSAL, countTRA, countTUR, nombrePDF);
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return nombrePDF;
        }

        private int CalculoPuntaje(int puntaje) {

            double puntajeaux = puntaje;
            puntajeaux = (puntajeaux / 6) * 4;
            return Convert.ToInt32(puntajeaux);
        }

        private void CalcularReporte(List<PuntajeTotalInteresBE> lstPuntajeTotal, int countADM, int countAGR, int countART,
            int countCOM, int countCON, int countCUL, int countDEP, int countDIS, int countFIN, int countINF, int countJUR,
            int countMAR, int countMEC, int countMIN, int countPED, int countSAL, int countSOC, int countTRA, int countTUR,
            string nombrePDF, string nombrePersona, string fechaInitTest)
        {
            bool usekey = Convert.ToBoolean(ConfigurationManager.AppSettings["USEKEY_INT"]);
            string nombreWord = "Reporte_Intereses" + "" + ".docx";
            string fileName = Server.MapPath(@"~/Reporte/Plantilla/Pantilla_Intereses.docx");
            //string nombrePDF = nombreWord.Replace(".docx", ".pdf");
            string archivoNuevo = Server.MapPath("~/Reporte/" + nombreWord);

            try
            {
                List<PuntajeTotalInteresBE> lstPT = null;
                List<string> lstAreas = null;

                EliminarArchivosTemporales(archivoNuevo, "");
                File.Copy(fileName, archivoNuevo);

                SearchAndReplace(archivoNuevo, "FECHA", fechaInitTest);
                SearchAndReplace(archivoNuevo, "NOMBREEVALUADO", nombrePersona);

                string[] nom = nombrePersona.Split(',');
                string nomClean = "";
                for (int i = nom.Length - 1; i >= 0; i--)
                {
                    nomClean = nomClean + " " + nom[i];
                }
                //nomClean =  CultureInfo.InvariantCulture.TextInfo.ToTitleCase(nomClean.Trim());
                nomClean = nomClean.Trim();

                SearchAndReplace(archivoNuevo, "NOMBREEV", nomClean);
                SearchAndReplace(archivoNuevo, "NOMBREEV", nomClean);


                using (WordprocessingDocument doc = WordprocessingDocument.Open(archivoNuevo, true))
                {
                    List<Table> lstTables = doc.MainDocumentPart.Document.Body.Elements<Table>().ToList();

                    
                    

                    lstPT = CalculoPT(lstTables[0], countADM,  countAGR,  countART, countCOM,  countCON,  countCUL,  
                                      countDEP,  countDIS,  countFIN,  countINF,  countJUR, countMAR,  countMEC,  
                                      countMIN,  countPED,  countSAL,  countSOC,  countTRA,  countTUR);

                    //Puntaje Alumno Test
                    MostrarPuntajesTest(lstTables[0], lstPT);

                    MostrarBarrasPT(doc, lstTables[0], lstPT);

                    //lstAreas = CalcularAreasMasDesarrolladas(lstPT, lstTables[1]);

                    
                }

                // lstAreas = CalcularAreasMasDesarrolladas(archivoNuevo, lstPT);
                // CalcularAreasMenosDesarrolladas(archivoNuevo, lstPT);

                using (WordprocessingDocument doc = WordprocessingDocument.Open(archivoNuevo, true))
                {

                    List<Table> lstTables = doc.MainDocumentPart.Document.Body.Elements<Table>().ToList();

                    int contMasDesarrolladas = CalcularAreasMasDesarrolladas_2(lstPT, lstTables[1]);
                    lstAreas = CalcularAreasMedianoInteres_2(lstPT, lstTables[2]);
                    int contMenosDesarrolladas = CalcularAreasMenosDesarrolladas_2(lstPT, lstTables[3]);
                    int altRecomendadas = CalcularCarrerasRelacionadas(doc, lstTables[4], archivoNuevo, lstPT, lstAreas);
                    
                    // Remove tabla de iconos
                    if (altRecomendadas == 0)
                    {
                        RemoverTabla(doc, 4);
                        
                    }



                    if (contMenosDesarrolladas == 0) // Remove tabla de areas de bajo interes
                    {
                        RemoverParrafo(doc, "AREASBAJOINT");
                        RemoverTabla(doc, 3);
                    }
                    else 
                    {

                        string mensajeMenosDesarrolladas = "● Áreas de bajo interés y sus carreras asociadas son:";
                        SearchAndReplace(doc, fileName, "AREASBAJOINT", mensajeMenosDesarrolladas);
                    }

                    // si la cantidad de areas de alto interes + la cantidad de areas de bajo interes es igual a 19 - elimina la tabla de mediano interes.
                    if ((contMasDesarrolladas + contMenosDesarrolladas) == 19) 
                    {
                        RemoverParrafo(doc, "AREASMEDIANOINT");
                        RemoverTabla(doc, 2);
                    }
                    else
                    {
                        string mensajeMedioDesarrolladas = "● Áreas de mediano interés y sus carreras asociadas son:";
                        SearchAndReplace(doc, fileName, "AREASMEDIANOINT", mensajeMedioDesarrolladas);
                    }


                    //Remove tabla de alto interes.
                    if (contMasDesarrolladas == 0)
                    {
                        RemoverParrafo(doc, "AREASALTOINT");
                        RemoverTabla(doc, 1);
                    }
                    else 
                    {
                        string mensajeMenosDesarrolladas = "● Áreas de alto interés: son las áreas de interés que más has desarrollado y sus carreras asociadas son:";
                        SearchAndReplace(doc, fileName, "AREASALTOINT", mensajeMenosDesarrolladas);
                    }



                }


                //string archivoPDF = Server.MapPath("~/Reporte/Reporte_Intereses.pdf");

                string pathPDF = Server.MapPath("~/Reporte/");

                if (usekey)
                {
                    //Libreria Externa
                    ConvertirWordToPDF(archivoNuevo, pathPDF, nombrePDF);
                }
                else
                {
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

                        /*
                        document.Close(ref save,
                                       ref missing,
                                       ref missing);

                        word.Application.Quit(ref missing,
                                              ref missing,
                                              ref missing);
                        */

                        ((Microsoft.Office.Interop.Word._Document)document).Close(ref save,
                                       ref missing,
                                       ref missing);

                        ((Microsoft.Office.Interop.Word._Application)word).Quit();
                    }



                }
                
                 

                EliminarArchivosTemporales(archivoNuevo, "");
            }
            catch (Exception ex)
            {
                throw ex;
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


        /**
         * Agreado el: 25/03/2014
         * por: Luis Gutierrez
         * 
         * **/
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
                t1.Text = lstPT[i].Puntaje.ToString();

            }

        }



        /************************************
         * Carreras Altamente Recomendadas
        *************************************/
        private int CalcularCarrerasRelacionadas(WordprocessingDocument doc, Table table, string fileName, List<PuntajeTotalInteresBE> lstPuntajeTotal, List<string> lstAreas)
        {
            
            string condicion1, condicion2, condicion3;
            List<string> lstCarreras = new List<string>();
            List<string> lstCarrerasAux = new List<string>();
            List<string> carreras = null;
            CarreraBC objCarreraBC = new CarreraBC();

            int count = 1;

            try
            {
                condicion1 = "";
                condicion2 = "";
                condicion3 = "";

                for (int i = 0; i < lstAreas.Count; i++)
                {

                    condicion1 = lstAreas[i];

                    for (int h = 0; h < lstPuntajeTotal.Count; h++)
                    {
                        if (lstPuntajeTotal[h].Area.ToString().ToUpper().StartsWith(condicion1))
                        {
                            PuntajeTotalInteresBE objPuntaje = new PuntajeTotalInteresBE();
                            objPuntaje = lstPuntajeTotal[h];

                            if (objPuntaje.Puntaje < 50 )
                            {
                                condicion1 = "";
                            }
                            break;
                        }
                    }




                    for (int j = i + 1; j < lstAreas.Count; j++)
                    {
                        condicion2 = lstAreas[j];

                        for (int k = 0; k < lstPuntajeTotal.Count; k++)
                        {
                            if (lstPuntajeTotal[k].Area.ToString().ToUpper().StartsWith(condicion2))
                            {
                                PuntajeTotalInteresBE objPuntaje = new PuntajeTotalInteresBE();
                                objPuntaje = lstPuntajeTotal[k];

                                if (objPuntaje.Puntaje < 50)
                                {
                                    condicion2 = "";
                                }
                                break;
                            }
                        }

                        for (int l = j + 1; l < lstAreas.Count; l++)
                        {
                            condicion3 = lstAreas[l];

                            for (int m = 0; m < lstPuntajeTotal.Count; m++)
                            {
                                if (lstPuntajeTotal[m].Area.ToString().ToUpper().StartsWith(condicion3))
                                {
                                    PuntajeTotalInteresBE objPuntaje = new PuntajeTotalInteresBE();
                                    objPuntaje = lstPuntajeTotal[m];
                                    if (objPuntaje.Puntaje < 50)
                                    {
                                        condicion3 = "";
                                    }
                                    break;
                                }
                            }
                        

                            carreras = objCarreraBC.ListarCarreras(condicion1, condicion2, condicion3, 1);
                            if (carreras.Count > 0)
                                lstCarrerasAux.AddRange(carreras);

                        }


                        
                    }
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }


            lstCarreras = lstCarrerasAux.Distinct<string>().ToList();

            string pathImage = Server.MapPath("~/Images/Carreras/");
            string imagen = "";
            int cont = 0;
            int fila = 0;
            int columna = 0;

            //Agreado por Luis Gutierrez
            int carrerasAltamenteRecomendadas = lstCarreras.Count;
            // 

            if (carrerasAltamenteRecomendadas > 0)
            {
                try
                {
                    for (int i = 0; i < 16; i += 2)
                    {
                        if (cont > lstCarreras.Count - 1)
                        {
                            break;
                        }

                        for (int j = 0; j < 4; j++)
                        {
                            if (cont > lstCarreras.Count - 1)
                            {
                                break;
                            }

                            imagen = pathImage + lstCarreras[cont] + ".png";
                            imagen = imagen.Replace("ó", "o");
                            imagen = imagen.Replace("í", "i");
                            imagen = imagen.Replace("á", "a");
                            imagen = imagen.Replace("é", "e");
                            imagen = imagen.Replace("ú", "u");
                            imagen = imagen.Replace("ñ", "n");
                            imagen = imagen.Replace("/", "_");

                            if (System.IO.File.Exists(imagen))
                            {
                                // Setear Imagen
                                TableRow rowImage = table.Elements<TableRow>().ElementAt(i);
                                TableCell cellImage = rowImage.Elements<TableCell>().ElementAt(j);
                                AddImage(cellImage, doc.MainDocumentPart, imagen, "imgId" + cont.ToString());
                            }

                            // Setear Texto
                            TableRow rowTexto = table.Elements<TableRow>().ElementAt(i + 1);
                            TableCell cellTexto = rowTexto.Elements<TableCell>().ElementAt(j);
                            Paragraph p = cellTexto.Elements<Paragraph>().First();
                            Run r = p.Elements<Run>().First();
                            Text t = r.Elements<Text>().First();
                            t.Text = lstCarreras[cont];

                            cont++;
                            columna++;
                        }
                        fila += 2;
                        columna = 0;
                    }
                }
                catch (Exception ex)
                {
                    throw ex;
                }

                for (int i = fila + 1; i <= 16; i++)
                {
                    TableRow row = table.Elements<TableRow>().Last();
                    row.RemoveAllChildren();
                    row.Remove();
                }

                for (int j = columna; j <= 4; j++)
                {
                    TableRow row = table.Elements<TableRow>().Last();
                    TableCell cell = row.Elements<TableCell>().Last();
                    Paragraph p = cell.Elements<Paragraph>().First();
                    Run r = p.Elements<Run>().First();
                    Text t = r.Elements<Text>().First();

                    if (t.Text.Contains("CARRERA"))
                    {
                        cell.RemoveAllChildren();
                        cell.Remove();
                    }
                }
            }

            
            if (carrerasAltamenteRecomendadas > 0)
            {
                //string mensajeAltRecomendada = "A partir de tu perfil de intereses no podemos recomendar carreras que pertenezcan a esta categoría.";
                string mensajeAltRecomendada = "● Las carreras recomendadas, a partir de tu combinación de intereses son:";
                SearchAndReplace(doc, fileName, "MENSAJEALTREC", mensajeAltRecomendada);
            }
            else
            {
                RemoverParrafo(doc, "MENSAJEALTREC");
            }
            
            return carrerasAltamenteRecomendadas;
        }

        /*************************
         ** Carreras Recomendadas
         *************************/
        private int CalcularCarrerasRecomendadas(WordprocessingDocument doc, Table table, string fileName, List<PuntajeTotalInteresBE> lstPuntajeTotal, List<string> lstAreas)
        {
            //string etiqueta = "CARRERARECOMENDADA{0}";
            //string etiquetaAux = "CARRERARECOMENDADA1";
            //string cadena = "";
            string condicion1, condicion2;
            List<string> lstCarreras = new List<string>();
            List<string> lstCarrerasAux = new List<string>();
            List<string> carreras = null;
            CarreraBC objCarreraBC = new CarreraBC();
            int cont = 1;

            condicion1 = "";
            condicion2 = "";
            

            try
            {
                for (int i = 0; i < lstAreas.Count; i++)
                {
                    condicion1 = lstAreas[i];

                    for (int j = 0; j < lstPuntajeTotal.Count; j++)
                    {
                        if (condicion1 == lstPuntajeTotal[j].Codigo)
                        {
                            if (lstPuntajeTotal[j].Puntaje >= 50 && lstPuntajeTotal[j].Puntaje <= 74)
                            {
                                carreras = objCarreraBC.ListarCarreras(condicion1, "","", 2);
                                if (carreras.Count > 0)
                                    lstCarrerasAux.AddRange(carreras);
                                break;
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw;
            }

            int puntaje1 = 0;
            int puntaje2 = 0;

            try
            {
                for (int i = 0; i < lstAreas.Count; i++)
                {
                    condicion1 = lstAreas[i];
                    for (int j = i + 1; j < lstAreas.Count; j++)
                    {
                        condicion2 = lstAreas[j];
                        for (int k = 0; k < lstPuntajeTotal.Count; k++)
                        {
                            if (lstPuntajeTotal[k].Codigo.Equals(condicion1))
                                puntaje1 = lstPuntajeTotal[k].Puntaje;
                            if (lstPuntajeTotal[k].Codigo.Equals(condicion2))
                                puntaje2 = lstPuntajeTotal[k].Puntaje;
                            if (puntaje1 != 0 && puntaje2 != 0)
                                break;
                        }

                        if ((puntaje1 > 75 || puntaje2 > 75) && ((puntaje1 >= 50 && puntaje1 <= 74) || (puntaje2 >= 50 && puntaje2 <= 74)))
                        {
                            carreras = objCarreraBC.ListarCarreras(condicion1, condicion2,"", 1);
                            if (carreras.Count > 0)
                                lstCarrerasAux.AddRange(carreras);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw;
            }

            string pathImage = Server.MapPath("~/Images/Carreras/");
            string imagen = "";
            cont = 0;
            int fila = 0;
            int columna = 0;
            int intCarrerasRecomendadas = 0;

            if (lstCarrerasAux.Count > 0)
            {
                lstCarreras = lstCarrerasAux.Distinct<string>().ToList();

                try
                {
                    for (int i = 0; i < 16; i += 2)
                    {
                        if (cont > lstCarreras.Count - 1)
                        {
                            break;
                        }

                        for (int j = 0; j < 4; j++)
                        {
                            if (cont > lstCarreras.Count - 1)
                            {
                                break;
                            }

                            imagen = pathImage + lstCarreras[cont] + ".png";
                            imagen = imagen.Replace("ó", "o");
                            imagen = imagen.Replace("í", "i");
                            imagen = imagen.Replace("á", "a");
                            imagen = imagen.Replace("é", "e");
                            imagen = imagen.Replace("ú", "u");
                            imagen = imagen.Replace("ñ", "n");
                            imagen = imagen.Replace("/", "_");

                            if (System.IO.File.Exists(imagen))
                            {
                                // Setear Imagen
                                TableRow rowImage = table.Elements<TableRow>().ElementAt(i);
                                TableCell cellImage = rowImage.Elements<TableCell>().ElementAt(j);
                                AddImage(cellImage, doc.MainDocumentPart, imagen, "imgIdRec" + cont.ToString());
                            }

                            // Setear Texto
                            TableRow rowTexto = table.Elements<TableRow>().ElementAt(i + 1);
                            TableCell cellTexto = rowTexto.Elements<TableCell>().ElementAt(j);
                            Paragraph p = cellTexto.Elements<Paragraph>().First();
                            Run r = p.Elements<Run>().First();
                            Text t = r.Elements<Text>().First();
                            t.Text = lstCarreras[cont];

                            cont++;
                            columna++;
                        }
                        fila += 2;
                        columna = 0;
                    }
                }
                catch (Exception ex)
                {
                    throw ex;
                }

                for (int i = fila + 1; i <= 16; i++)
                {
                    TableRow row = table.Elements<TableRow>().Last();
                    row.RemoveAllChildren();
                    row.Remove();
                }

                for (int j = columna; j <= 4; j++)
                {
                    TableRow row = table.Elements<TableRow>().Last();
                    TableCell cell = row.Elements<TableCell>().Last();
                    Paragraph p = cell.Elements<Paragraph>().First();
                    Run r = p.Elements<Run>().First();
                    Text t = r.Elements<Text>().First();

                    if (t.Text.Contains("CARRERA"))
                    {
                        cell.RemoveAllChildren();
                        cell.Remove();
                    }
                }

                //for (int i = 0; i < lstCarreras.Count; i++)
                //{
                //    etiquetaAux = etiqueta.Replace("{0}", cont.ToString());
                //    cadena = lstCarreras[i];
                //    SearchAndReplace(fileName, etiquetaAux, cadena);
                //    cont++;
                //}

                //RemoverParrafo(doc, "MENSAJERECOMENDADA");
            }
            else
            {
                string mensajeRecomendada = "A partir de tu perfil de intereses no podemos recomendar carreras que pertenezcan a esta categoría.";
                SearchAndReplace(doc, fileName, "MENSAJERECOMENDADA", mensajeRecomendada);


            }

            intCarrerasRecomendadas = lstCarreras.Count;
            
            return intCarrerasRecomendadas;
        }



        /**
         *  CalcularAreasMedianoInteres
         * */
        private void CalcularAreasMedianoInteres(List<PuntajeTotalInteresBE> lstPuntajeTotal, Table table)
        {
            int cont = 0;
            List<PuntajeTotalInteresBE> lstAreaMedianoInteres = new List<PuntajeTotalInteresBE>();
            lstAreaMedianoInteres = lstPuntajeTotal.OrderByDescending(x => x.Puntaje).ToList();
            for (int i = 0; i < lstAreaMedianoInteres.Count; i++)
            {
                if (lstAreaMedianoInteres[i].Puntaje >= 26 && lstAreaMedianoInteres[i].Puntaje <= 75)
                {

                    llenaTabla(table, cont, lstAreaMedianoInteres[i].Codigo);
                    cont++;
                }
                
            }


            // Eliminar las filas que sobran
            for (int i = 0; i < 14; i++)
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


        }

       
        private List<string> CalcularAreasMedianoInteres_2(List<PuntajeTotalInteresBE> lstPuntajeTotal, Table table)
        {
            int cont = 0;
            List<string> lstAreas = new List<string>();
            
            List<PuntajeTotalInteresBE> lstAreaMedianoInteres = new List<PuntajeTotalInteresBE>();
            lstAreaMedianoInteres = lstPuntajeTotal.OrderByDescending(x => x.Puntaje).ToList();
            for (int i = 0; i < lstAreaMedianoInteres.Count; i++)
            {
                if (lstAreaMedianoInteres[i].Puntaje >= 26 && lstAreaMedianoInteres[i].Puntaje <= 75)
                {

                    llenaTabla(table, cont, lstAreaMedianoInteres[i].Codigo);
                    cont++;
                }

                if (lstAreaMedianoInteres[i].Puntaje > 50)
                {
                    lstAreas.Add(lstAreaMedianoInteres[i].Codigo);
                }

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

            return lstAreas;

        }

        /**
         *  CalcularAreasMenosDesarrolladas
         * */
        private void CalcularAreasMenosDesarrolladas(List<PuntajeTotalInteresBE> lstPuntajeTotal, Table table)
        {
            int fila = 1;
            List<PuntajeTotalInteresBE> lstAreMenosDesarrolladas = new List<PuntajeTotalInteresBE>();
            lstAreMenosDesarrolladas = lstPuntajeTotal.OrderBy(x => x.Puntaje).ToList();
            for (int i = 0; i < lstAreMenosDesarrolladas.Count; i++)
            {
                if (lstAreMenosDesarrolladas[i].Puntaje > 25)
                {
                    //fila = (i == 0) ? 1 : 0;
                    break;
                }
                llenaTabla(table, i, lstAreMenosDesarrolladas[i].Codigo);
            }
            

            // Eliminar las filas que sobran
            for (int i = 1; i < 15; i++)
            {
                TableRow rowTal = table.Elements<TableRow>().Last();//.ElementAt(fila);
                TableCell cellTal = rowTal.Elements<TableCell>().ElementAt(0);
                Paragraph pTal = cellTal.Elements<Paragraph>().First();
                Run rTal = pTal.Elements<Run>().First();
                Text tTal = rTal.Elements<Text>().First();

                TableCell cellDes = rowTal.Elements<TableCell>().ElementAt(1);

                if (tTal.Text.Contains("AG"))
                {
                    cellTal.RemoveAllChildren();
                    cellTal.Remove();
                    cellDes.RemoveAllChildren();
                    cellDes.Remove();
                    rowTal.RemoveAllChildren();
                    rowTal.Remove();
                }
            }
        }

        private int CalcularAreasMenosDesarrolladas_2(List<PuntajeTotalInteresBE> lstPuntajeTotal, Table table)
        {
            int cont = 0;

            List<PuntajeTotalInteresBE> lstAreMenosDesarrolladas = new List<PuntajeTotalInteresBE>();
            lstAreMenosDesarrolladas = lstPuntajeTotal.OrderBy(x => x.Puntaje).ToList();
            for (int i = 0; i < lstAreMenosDesarrolladas.Count; i++)
            {
                if (lstAreMenosDesarrolladas[i].Puntaje > 25)
                {
                    
                    break;
                }
                llenaTabla(table, i, lstAreMenosDesarrolladas[i].Codigo);
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

        /**
         * CalcularAreasMasDesarrolladas
         **/
        private List<string> CalcularAreasMasDesarrolladas(List<PuntajeTotalInteresBE> lstPuntajeTotal, Table table)
        {
            int cont = 1;
            int contSin = 1;
            int fila = 1;
            List<string> lstAreas = new List<string>();
            
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
            }

            

            // Eliminar las filas que sobran
            for (int i = fila; i < 14; i++)
            {
                TableRow rowTal = table.Elements<TableRow>().Last();//.ElementAt(fila);
                TableCell cellTal = rowTal.Elements<TableCell>().ElementAt(0);
                Paragraph pTal = cellTal.Elements<Paragraph>().First();
                Run rTal = pTal.Elements<Run>().First();
                Text tTal = rTal.Elements<Text>().First();

                TableCell cellDes = rowTal.Elements<TableCell>().ElementAt(1);

                if (tTal.Text.Contains("AG"))
                {
                    cellTal.RemoveAllChildren();
                    cellTal.Remove();
                    cellDes.RemoveAllChildren();
                    cellDes.Remove();
                    rowTal.RemoveAllChildren();
                    rowTal.Remove();
                }
            }

            

            return lstAreas;
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

        private void RemoverParrafo(string fileName, string textoEliminar)
        {
            using (WordprocessingDocument WPD = WordprocessingDocument
                .Open(fileName, true))
            {
                MainDocumentPart MDP = WPD.MainDocumentPart;
                Document D = MDP.Document;
                bool termino = false;
                foreach (Paragraph P in D.Descendants<Paragraph>())
                {
                    foreach (Run R in P.Descendants<Run>())
                    {
                        if (R.Descendants<Text>()
                            .Where(T => T.Text == textoEliminar).Count() > 0)
                        {
                            P.Remove();
                            termino = true;
                            break;
                        }
                    }

                    if (termino)
                        break;
                }
                D.Save();
            }
        }

        private void RemoverParrafo(WordprocessingDocument WPD, string textoEliminar)
        {
            //using (WordprocessingDocument WPD = WordprocessingDocument
            //    .Open(fileName, true))
            //{
            MainDocumentPart MDP = WPD.MainDocumentPart;
            Document D = MDP.Document;
            bool termino = false;
            foreach (Paragraph P in D.Descendants<Paragraph>())
            {
                foreach (Run R in P.Descendants<Run>())
                {
                    if (R.Descendants<Text>()
                        .Where(T => T.Text == textoEliminar).Count() > 0)
                    {
                        P.Remove();
                        termino = true;
                        break;
                    }
                }

                if (termino)
                    break;
            }
            D.Save();
            //}
        }

        private void RemoverTabla(WordprocessingDocument WPD, int indice)
        {
            //doc.MainDocumentPart.Document.Body.Elements

            MainDocumentPart MDP = WPD.MainDocumentPart;
            Document D = MDP.Document;
            D.Body.Elements<Table>().ElementAt(indice).Remove();

            /*
            bool termino = false;
            var document = WordprocessingDocument.Open(fileName, true);
            var doc = document.MainDocumentPart.Document;
            var table = doc.Body.Elements<Table>();
            foreach (var i in table)
            {
                if (i.InnerText.Contains("~CIB"))
                {
                    i.Remove();
                }
            
            }
            doc.Save();
            */

            D.Save();
        }

        // To search and replace content in a document part.
        public static void SearchAndReplace(string fileName, string searchText, string newText)
        {
            using (WordprocessingDocument doc = WordprocessingDocument.Open(fileName, true))
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

        private void ConvertirWordToPDF(string fileToConvert, string filePDF, string nombrePDF)
        {
            using (var client = new WebClient())
            {

                var data = new NameValueCollection();
                string strApiKey = ConfigurationManager.AppSettings["Apikey"].ToString();

                data.Add("OutputFileName", nombrePDF);
                data.Add("ApiKey", strApiKey);

                try
                {

                    client.QueryString.Add(data);

                    var response = client.UploadFile("http://do.convertapi.com/word2pdf", fileToConvert);

                    var responseHeaders = client.ResponseHeaders;

                    var path = Path.Combine(filePDF, responseHeaders["OutputFileName"]);

                    File.WriteAllBytes(path, response);

                }

                catch (WebException ex)
                {

                    throw ex;

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

        /**
         * Fecha creación: 25/03/14
         * 
         * **/
        private void llenaTabla(Table table, int numRow, string codArea)
        {
            /*
            if (numRow > 2)
                numRow = 2;

            if (numRow <= 0)
                numRow = 0;
            */
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
    }
}