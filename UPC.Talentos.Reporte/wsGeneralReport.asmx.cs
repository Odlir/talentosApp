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
    /// Descripción breve de wsGeneralReport
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // Para permitir que se llame a este servicio Web desde un script, usando ASP.NET AJAX, quite la marca de comentario de la línea siguiente. 
    // [System.Web.Script.Services.ScriptService]
    public class wsGeneralReport : System.Web.Services.WebService
    {


        List<GeneralTemperamentosBE> ListScoreTemperaments = new List<GeneralTemperamentosBE>();

        [WebMethod]
        public string CalcularReporte(string idEncuesta, bool administrador)
        {

            bool usekey = Convert.ToBoolean(ConfigurationManager.AppSettings["USEKEY_GEN"]);

            //CreatPDFwithItextSharp(DNI, administrador);
            int[] orientaciones = new int[6] { 0, 0, 0, 0, 0, 0 }; //Ejecucion: 0, Pensamiento: 1, Innovacion: 2, Liderazgo: 3
            // Personas: 4, Estructura: 5

            ResultadoBC objResultadoBC = new ResultadoBC();
            TalentoBC objTalentoBC = new TalentoBC();




            // Define an object to pass to the word API for missing parameters
            string nombreWord = "";

            //object fileName = null;
            string fileName = "";

            fileName = Server.MapPath(@"~/Reporte/Plantilla/Plantilla_Reporte_General.docx");


            try
            {
                GeneralResultBE objGeneralResult = objResultadoBC.getGeneralResult(idEncuesta);
                List<GeneralReportCategoryBE> ListReportCategory = objResultadoBC.getResultGeneralCategory(idEncuesta);
                List<GeneralInteresesBE> ListResultIntereses = objResultadoBC.getResultIntereses(idEncuesta);
                List<GeneralTemperamentosBE> ListResultTemperamentos = objResultadoBC.getResultTemperamentos(idEncuesta);

                string nombreWord_Aux = objGeneralResult.institution.ToUpper();
                if (string.IsNullOrEmpty(nombreWord_Aux))
                    nombreWord_Aux = "Reporte_General";

                nombreWord_Aux = nombreWord_Aux.Replace("Ó", "O");
                nombreWord_Aux = nombreWord_Aux.Replace("Í", "I");
                nombreWord_Aux = nombreWord_Aux.Replace("Á", "A");
                nombreWord_Aux = nombreWord_Aux.Replace("É", "E");
                nombreWord_Aux = nombreWord_Aux.Replace("Ú", "U");
                nombreWord_Aux = nombreWord_Aux.Replace("Ñ", "N");



                nombreWord = "Reporte_General_UPC_" + nombreWord_Aux.Replace(" ", "") + ".docx";
                //nombreWord = "Reporte_Talentos_.docx";
                string nombrePDF = nombreWord.Replace(".docx", ".pdf");
                string pathPDF = Server.MapPath("~/Reporte/");
                string archivoNuevo = Server.MapPath("~/Reporte/" + nombreWord);

                EliminarArchivosTemporales(archivoNuevo, "");
                EliminarArchivosTemporales(pathPDF + nombrePDF, "");

                File.Copy(fileName, archivoNuevo);

                using (WordprocessingDocument doc = WordprocessingDocument.Open(archivoNuevo, true))
                {
                    // Find the first table in the document.
                    List<Table> lstTables = doc.MainDocumentPart.Document.Body.Elements<Table>().ToList();

                    FormatearTablaResultadoChart(lstTables[5], ListReportCategory);

                    ModifyChartSimplified(doc.MainDocumentPart, "B", 2, ListReportCategory[0].percentCategory.ToString(), false);
                    ModifyChartSimplified(doc.MainDocumentPart, "B", 3, ListReportCategory[1].percentCategory.ToString(), false);
                    ModifyChartSimplified(doc.MainDocumentPart, "B", 4, ListReportCategory[2].percentCategory.ToString(), false);
                    ModifyChartSimplified(doc.MainDocumentPart, "B", 5, ListReportCategory[3].percentCategory.ToString(), false);
                    ModifyChartSimplified(doc.MainDocumentPart, "B", 6, ListReportCategory[4].percentCategory.ToString(), false);
                    ModifyChartSimplified(doc.MainDocumentPart, "B", 7, ListReportCategory[5].percentCategory.ToString(), false);

                    FormatearTablaTalentosMasDesarrollados(lstTables[6], objGeneralResult.lstTalentsMostDe, objGeneralResult.lstTalentsSpecifics);

                    // Intereses
                    MostrarPuntajesTest(lstTables[7], ListResultIntereses);
                    MostrarBarrasPT(doc, lstTables[7], ListResultIntereses);

                    //Temperamentos
                    GenerarGrafico_Extroversion_Introversion(doc, lstTables[1], ListResultTemperamentos);
                    GenerarGrafico_Intuicion_Sensacion(doc, lstTables[2], ListResultTemperamentos);
                    GenerarGrafico_Racional_Emocional(doc, lstTables[3], ListResultTemperamentos);
                    GenerarGrafico_Organizado_Casual(doc, lstTables[4], ListResultTemperamentos);
                    //PutScoreTemperaments(lstTables[5], ListScoreTemperaments);

                }

                //Setea en doc el nombre del evaluado
                SearchAndReplace(archivoNuevo, "#NOMBRE_PERSONA#", objGeneralResult.institution);

                //Setea en doc la fecha que inicio el test
                SearchAndReplace(archivoNuevo, "#FECHA_TEST#", objGeneralResult.testDate);

                if (usekey)
                {
                    //API online
                    ConvertirWordToPDF(archivoNuevo, nombreWord, pathPDF, nombrePDF);
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

                        document.Close(ref save,
                                       ref missing,
                                       ref missing);

                        word.Application.Quit(ref missing,
                                              ref missing,
                                              ref missing);
                    }
                }

                EliminarArchivosTemporales(archivoNuevo, "");

                return nombrePDF;
            }
            catch (Exception ex)
            {
                LogDALC objLogDALC = new LogDALC();
                string mensaje = "wsGeneralReport. Mensaje: " + ex.Message;
                objLogDALC.InsertarLog(mensaje);

                throw ex;
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

        private void FormatearTablaResultadoChart(Table table, List<GeneralReportCategoryBE> ListReportCategory)
        {

            TableRow row1 = table.Elements<TableRow>().ElementAt(2);
            TableCell cell1 = row1.Elements<TableCell>().ElementAt(0);
            Paragraph p1 = cell1.Elements<Paragraph>().First();
            Run r1 = p1.Elements<Run>().First();
            Text t1 = r1.Elements<Text>().First();
            t1.Text = ListReportCategory[0].percentCategory.ToString() + " %";

            TableRow row2 = table.Elements<TableRow>().ElementAt(2);
            TableCell cell2 = row2.Elements<TableCell>().ElementAt(2);
            Paragraph p2 = cell2.Elements<Paragraph>().First();
            Run r2 = p2.Elements<Run>().First();
            Text t2 = r2.Elements<Text>().First();
            t2.Text = ListReportCategory[1].percentCategory.ToString() + " %";

            TableRow row3 = table.Elements<TableRow>().ElementAt(2);
            TableCell cell3 = row3.Elements<TableCell>().ElementAt(4);
            Paragraph p3 = cell3.Elements<Paragraph>().First();
            Run r3 = p3.Elements<Run>().First();
            Text t3 = r3.Elements<Text>().First();
            t3.Text = ListReportCategory[2].percentCategory.ToString() + " %";

            TableRow row4 = table.Elements<TableRow>().ElementAt(2);
            TableCell cell4 = row4.Elements<TableCell>().ElementAt(6);
            Paragraph p4 = cell4.Elements<Paragraph>().First();
            Run r4 = p4.Elements<Run>().First();
            Text t4 = r4.Elements<Text>().First();
            t4.Text = ListReportCategory[3].percentCategory.ToString() + " %";

            TableRow row5 = table.Elements<TableRow>().ElementAt(2);
            TableCell cell5 = row5.Elements<TableCell>().ElementAt(8);
            Paragraph p5 = cell5.Elements<Paragraph>().First();
            Run r5 = p5.Elements<Run>().First();
            Text t5 = r5.Elements<Text>().First();
            t5.Text = ListReportCategory[4].percentCategory.ToString() + " %";

            TableRow row6 = table.Elements<TableRow>().ElementAt(2);
            TableCell cell6 = row6.Elements<TableCell>().ElementAt(10);
            Paragraph p6 = cell6.Elements<Paragraph>().First();
            Run r6 = p6.Elements<Run>().First();
            Text t6 = r6.Elements<Text>().First();
            t6.Text = ListReportCategory[5].percentCategory.ToString() + "%";



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


        private void FormatearTablaTalentosMasDesarrollados(Table table, List<GeneralTalentBE> lstTalentos, List<GeneralTalentBE> lstTalentosTE)
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


        // Intereses
        private void MostrarPuntajesTest(Table table, List<GeneralInteresesBE> lstPT)
        {
            int lenght = lstPT.Count;
            string strAreaCode = "";
            TableRow row1 = null;
            for (int i = 0; i < lenght; i++)
            {
                strAreaCode = lstPT[i].code;
                switch (strAreaCode)
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
                    case "MINE":
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
                t1.Text = lstPT[i].score.ToString();

            }

        }


        private void MostrarBarrasPT(WordprocessingDocument doc, Table table, List<GeneralInteresesBE> lstPT)
        {
            string pathImages = Server.MapPath("~/Images/");

            for (int i = 0; i < lstPT.Count; i++)
            {
                int puntaje = lstPT[i].score;


                if (puntaje != 0)
                {

                    //Nuevo
                    int ptImagen = (puntaje % 2 == 0) ? puntaje - 1 : puntaje;

                    string imagen = pathImages + ptImagen.ToString() + ".png";
                    //string imagen = pathImages + "99" +".png";

                    switch (lstPT[i].code)
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

                        case "MINE":
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


        //Temperamentos

        private double round(decimal valAux)
        {
            double num = 0.0;
            double val = Convert.ToDouble(valAux);

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

        private void GenerarGrafico_Extroversion_Introversion(WordprocessingDocument doc, Table table, List<GeneralTemperamentosBE> ListResultTemperamentos)
        {
            string pathImages = Server.MapPath("~/Images/Temperamentos/");
            string puntaje = round(ListResultTemperamentos[0].score).ToString();
            string imagen = pathImages + puntaje.Replace(".", "_") + ".png";
            double extrovertido = 0;
            double introvertido = 0;

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
                    AddImage(cell, doc.MainDocumentPart, imagen, "imIdg1");
                    extrovertido = convertScalePoints(puntaje);
                    break;
                case "4":
                    //Mostrar Barra hacia arriba
                    row = table.Elements<TableRow>().ElementAt(1);
                    cell = row.Elements<TableCell>().ElementAt(1);
                    AddImage(cell, doc.MainDocumentPart, imagen, "imIdg1");

                    //Mostrar barra hacia abajo
                    row = table.Elements<TableRow>().ElementAt(2);
                    cell = row.Elements<TableCell>().ElementAt(1);
                    AddImage(cell, doc.MainDocumentPart, imagen, "imIdg1_1");

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
                    AddImage(cell, doc.MainDocumentPart, imagen, "imIdg1");

                    introvertido = convertScalePoints(puntaje);
                    break;

            }

            puntaje = round(ListResultTemperamentos[1].score).ToString();
            imagen = pathImages + puntaje.Replace(".", "_") + ".png";

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
                    AddImage(cell, doc.MainDocumentPart, imagen, "imIdg2");

                    extrovertido += convertScalePoints(puntaje);
                    break;
                case "4":
                    //Mostrar Barra hacia arriba
                    row = table.Elements<TableRow>().ElementAt(1);
                    cell = row.Elements<TableCell>().ElementAt(2);
                    AddImage(cell, doc.MainDocumentPart, imagen, "imIdg2");

                    //Mostrar Barra hacia abajo
                    row = table.Elements<TableRow>().ElementAt(2);
                    cell = row.Elements<TableCell>().ElementAt(2);
                    AddImage(cell, doc.MainDocumentPart, imagen, "imIdg2_1");
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
                    AddImage(cell, doc.MainDocumentPart, imagen, "imIdg2");

                    introvertido += convertScalePoints(puntaje);
                    break;

            }


            puntaje = round(ListResultTemperamentos[2].score).ToString();
            imagen = pathImages + puntaje.Replace(".", "_") + ".png";

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
                    AddImage(cell, doc.MainDocumentPart, imagen, "imIdg3");

                    extrovertido += convertScalePoints(puntaje);
                    break;
                case "4":
                    //Mostrar Barra hacia arriba
                    row = table.Elements<TableRow>().ElementAt(1);
                    cell = row.Elements<TableCell>().ElementAt(3);
                    AddImage(cell, doc.MainDocumentPart, imagen, "imIdg3");

                    //Mostrar Barra hacia abajo
                    row = table.Elements<TableRow>().ElementAt(2);
                    cell = row.Elements<TableCell>().ElementAt(3);
                    AddImage(cell, doc.MainDocumentPart, imagen, "imIdg3_1");
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
                    AddImage(cell, doc.MainDocumentPart, imagen, "imIdg3");

                    introvertido += convertScalePoints(puntaje);
                    break;

            }

            puntaje = round(ListResultTemperamentos[3].score).ToString();

            imagen = pathImages + puntaje.Replace(".", "_") + ".png";

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
                    AddImage(cell, doc.MainDocumentPart, imagen, "imIdg4");

                    extrovertido += convertScalePoints(puntaje);
                    break;
                case "4":
                    //Mostrar Barra hacia arriba
                    row = table.Elements<TableRow>().ElementAt(1);
                    cell = row.Elements<TableCell>().ElementAt(4);
                    AddImage(cell, doc.MainDocumentPart, imagen, "imIdg4");

                    //Mostrar Barra hacia abajo
                    row = table.Elements<TableRow>().ElementAt(2);
                    cell = row.Elements<TableCell>().ElementAt(4);
                    AddImage(cell, doc.MainDocumentPart, imagen, "imIdg4_1");

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
                    AddImage(cell, doc.MainDocumentPart, imagen, "imIdg4");

                    introvertido += convertScalePoints(puntaje);
                    break;
            }

            ListScoreTemperaments.Add(new GeneralTemperamentosBE { code = "EXTRO", score = Convert.ToDecimal(extrovertido / 4) });
            ListScoreTemperaments.Add(new GeneralTemperamentosBE { code = "INTRO", score = Convert.ToDecimal(introvertido / 4) });


        }

        private void GenerarGrafico_Intuicion_Sensacion(WordprocessingDocument doc, Table table, List<GeneralTemperamentosBE> ListResultTemperamentos)
        {
            string pathImages = Server.MapPath("~/Images/Temperamentos/");
            string puntajeIS = round(ListResultTemperamentos[4].score).ToString();
            string imagen = pathImages + puntajeIS.Replace(".", "_") + ".png";
            double intuitivo = 0;
            double sensorial = 0;


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
                    AddImage(cellIS, doc.MainDocumentPart, imagen, "imIdg5");

                    intuitivo = convertScalePoints(puntajeIS);
                    break;

                case "4":
                    //Mostrar Barra hacia arriba
                    rowIS = table.Elements<TableRow>().ElementAt(1);
                    cellIS = rowIS.Elements<TableCell>().ElementAt(1);
                    AddImage(cellIS, doc.MainDocumentPart, imagen, "imIdg5");

                    //Mostrar Barra hacia abajo
                    rowIS = table.Elements<TableRow>().ElementAt(2);
                    cellIS = rowIS.Elements<TableCell>().ElementAt(1);
                    AddImage(cellIS, doc.MainDocumentPart, imagen, "imIdg5_1");

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
                    AddImage(cellIS, doc.MainDocumentPart, imagen, "imIdg5");

                    sensorial = convertScalePoints(puntajeIS);
                    break;

            }

            puntajeIS = round(ListResultTemperamentos[5].score).ToString();
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
                    cellIS = rowIS.Elements<TableCell>().ElementAt(2);
                    AddImage(cellIS, doc.MainDocumentPart, imagen, "imIdg20");

                    intuitivo += convertScalePoints(puntajeIS);
                    break;
                case "4":
                    //Mostrar Barra hacia arriba
                    rowIS = table.Elements<TableRow>().ElementAt(1);
                    cellIS = rowIS.Elements<TableCell>().ElementAt(2);
                    AddImage(cellIS, doc.MainDocumentPart, imagen, "imIdg20");

                    //Mostrar Barra hacia abajo
                    rowIS = table.Elements<TableRow>().ElementAt(2);
                    cellIS = rowIS.Elements<TableCell>().ElementAt(2);
                    AddImage(cellIS, doc.MainDocumentPart, imagen, "imIdg20_1");

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
                    AddImage(cellIS, doc.MainDocumentPart, imagen, "imIdg20");

                    sensorial += convertScalePoints(puntajeIS);
                    break;

            }


            puntajeIS = round(ListResultTemperamentos[6].score).ToString();
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
                    cellIS = rowIS.Elements<TableCell>().ElementAt(3);
                    AddImage(cellIS, doc.MainDocumentPart, imagen, "imIdg21");

                    intuitivo += convertScalePoints(puntajeIS);
                    break;
                case "4":
                    //Mostrar Barra hacia arriba
                    rowIS = table.Elements<TableRow>().ElementAt(1);
                    cellIS = rowIS.Elements<TableCell>().ElementAt(3);
                    AddImage(cellIS, doc.MainDocumentPart, imagen, "imIdg21");

                    //Mostrar Barra hacia abajo
                    rowIS = table.Elements<TableRow>().ElementAt(2);
                    cellIS = rowIS.Elements<TableCell>().ElementAt(3);
                    AddImage(cellIS, doc.MainDocumentPart, imagen, "imIdg21_1");
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
                    AddImage(cellIS, doc.MainDocumentPart, imagen, "imIdg21");

                    sensorial += convertScalePoints(puntajeIS);
                    break;

            }

            ListScoreTemperaments.Add(new GeneralTemperamentosBE { code = "INT", score = Convert.ToDecimal(intuitivo / 3) });
            ListScoreTemperaments.Add(new GeneralTemperamentosBE { code = "SEN", score = Convert.ToDecimal(sensorial / 3) });


        }

        private void GenerarGrafico_Racional_Emocional(WordprocessingDocument doc, Table table, List<GeneralTemperamentosBE> ListResultTemperamentos)
        {
            string pathImages = Server.MapPath("~/Images/Temperamentos/");
            string puntajeOC = round(ListResultTemperamentos[7].score).ToString();

            string imagen = pathImages + puntajeOC.Replace(".", "_") + ".png";

            double racional = 0;
            double emocional = 0;

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

                    racional = convertScalePoints(puntajeOC);
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

                    emocional = convertScalePoints(puntajeOC);
                    break;

            }

            puntajeOC = round(ListResultTemperamentos[8].score).ToString();
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
                    cell = row.Elements<TableCell>().ElementAt(2);
                    AddImage(cell, doc.MainDocumentPart, imagen, "imId7");

                    racional += convertScalePoints(puntajeOC);
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
                    emocional += convertScalePoints(puntajeOC);
                    break;

            }


            puntajeOC = round(ListResultTemperamentos[9].score).ToString();
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
                    AddImage(cell, doc.MainDocumentPart, imagen, "imId24");

                    racional += convertScalePoints(puntajeOC);
                    break;
                case "4":
                    row = table.Elements<TableRow>().ElementAt(1);
                    cell = row.Elements<TableCell>().ElementAt(3);
                    AddImage(cell, doc.MainDocumentPart, imagen, "imId24");

                    row = table.Elements<TableRow>().ElementAt(2);
                    cell = row.Elements<TableCell>().ElementAt(3);
                    AddImage(cell, doc.MainDocumentPart, imagen, "imId24_1");
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
                    emocional += convertScalePoints(puntajeOC);
                    break;

            }

            ListScoreTemperaments.Add(new GeneralTemperamentosBE { code = "RAC", score = Convert.ToDecimal(racional / 3) });
            ListScoreTemperaments.Add(new GeneralTemperamentosBE { code = "EMO", score = Convert.ToDecimal(emocional / 3) });

        }

        private void GenerarGrafico_Organizado_Casual(WordprocessingDocument doc, Table table, List<GeneralTemperamentosBE> ListResultTemperamentos)
        {
            string pathImages = Server.MapPath("~/Images/Temperamentos/");
            string puntajeOC = round(ListResultTemperamentos[10].score).ToString();

            string imagen = pathImages + puntajeOC.Replace(".", "_") + ".png";

            double organizado = 0;
            double casual = 0;

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
                    AddImage(cell, doc.MainDocumentPart, imagen, "imIdg8");

                    organizado = convertScalePoints(puntajeOC);
                    break;
                case "4":
                    row = table.Elements<TableRow>().ElementAt(1);
                    cell = row.Elements<TableCell>().ElementAt(1);
                    AddImage(cell, doc.MainDocumentPart, imagen, "imIdg8");

                    row = table.Elements<TableRow>().ElementAt(2);
                    cell = row.Elements<TableCell>().ElementAt(1);
                    AddImage(cell, doc.MainDocumentPart, imagen, "imIdg8_1");

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
                    AddImage(cell, doc.MainDocumentPart, imagen, "imIdg8");

                    casual = convertScalePoints(puntajeOC);
                    break;

            }

            puntajeOC = round(ListResultTemperamentos[11].score).ToString();
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
                    cell = row.Elements<TableCell>().ElementAt(2);
                    AddImage(cell, doc.MainDocumentPart, imagen, "imIdg9");

                    organizado += convertScalePoints(puntajeOC);
                    break;
                case "4":
                    row = table.Elements<TableRow>().ElementAt(1);
                    cell = row.Elements<TableCell>().ElementAt(2);
                    AddImage(cell, doc.MainDocumentPart, imagen, "imIdg9");

                    row = table.Elements<TableRow>().ElementAt(2);
                    cell = row.Elements<TableCell>().ElementAt(2);
                    AddImage(cell, doc.MainDocumentPart, imagen, "imIdg9_1");

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
                    AddImage(cell, doc.MainDocumentPart, imagen, "imIdg9");

                    casual += convertScalePoints(puntajeOC);
                    break;

            }

            puntajeOC = round(ListResultTemperamentos[12].score).ToString();
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
                    AddImage(cell, doc.MainDocumentPart, imagen, "imId25");

                    organizado += convertScalePoints(puntajeOC);
                    break;

                case "4":
                    row = table.Elements<TableRow>().ElementAt(1);
                    cell = row.Elements<TableCell>().ElementAt(3);
                    AddImage(cell, doc.MainDocumentPart, imagen, "imId25");

                    row = table.Elements<TableRow>().ElementAt(2);
                    cell = row.Elements<TableCell>().ElementAt(3);
                    AddImage(cell, doc.MainDocumentPart, imagen, "imId25_1");
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

                    casual += convertScalePoints(puntajeOC);
                    break;

            }

            ListScoreTemperaments.Add(new GeneralTemperamentosBE { code = "ORG", score = Convert.ToDecimal(organizado / 3) });
            ListScoreTemperaments.Add(new GeneralTemperamentosBE { code = "CAS", score = Convert.ToDecimal(casual / 3) });

        }


        private void PutScoreTemperaments(Table table, List<GeneralTemperamentosBE> lstScoreTemperaments)
        {
            int lenght = lstScoreTemperaments.Count;
            string strCode = "";
            TableRow row1 = null;
            for (int i = 0; i < lenght; i++)
            {
                strCode = lstScoreTemperaments[i].code;
                switch (strCode)
                {
                    case "EXTRO":
                        row1 = table.Elements<TableRow>().ElementAt(1);
                        break;
                    case "INTRO":
                        row1 = table.Elements<TableRow>().ElementAt(2);
                        break;
                    case "INT":
                        row1 = table.Elements<TableRow>().ElementAt(3);
                        break;
                    case "SEN":
                        row1 = table.Elements<TableRow>().ElementAt(4);
                        break;
                    case "RAC":
                        row1 = table.Elements<TableRow>().ElementAt(5);
                        break;
                    case "EMO":
                        row1 = table.Elements<TableRow>().ElementAt(6);
                        break;
                    case "ORG":
                        row1 = table.Elements<TableRow>().ElementAt(7);
                        break;
                    case "CAS":
                        row1 = table.Elements<TableRow>().ElementAt(8);
                        break;

                }


                TableCell cell1 = row1.Elements<TableCell>().ElementAt(1);
                Paragraph p1 = cell1.Elements<Paragraph>().First();
                Run r1 = p1.Elements<Run>().First();
                Text t1 = r1.Elements<Text>().First();

                t1.Text = lstScoreTemperaments[i].score.ToString("0.##");

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
