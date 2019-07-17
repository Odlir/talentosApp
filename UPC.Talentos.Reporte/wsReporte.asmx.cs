using System;
using System.Collections.Generic;
using System.Linq;
using System.Configuration;
using System.Web.Services;
using UPC.Talentos.BL.BE;
using UPC.Talentos.DL.DALC;
using UPC.Talentos.BL.BC;
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
    /// Summary description for wsReporte
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    // [System.Web.Script.Services.ScriptService]
    public class wsReporte : System.Web.Services.WebService
    {
        [WebMethod]
        public void Resultado(ResultadoFinalBE obj, TalentoComplexBE tal)
        {
        }

        [WebMethod]
        public void CargarResultadosMasivos(List<ResultadoFinalBE> lstResultados)
        {
            ResultadoBC objResultadoBC = new ResultadoBC();

            try
            {
                objResultadoBC.CargarResultadosMasivos(lstResultados);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [WebMethod]
        public void EnviarMailResultado(string CodEvaluacion, string token)
        {
            ParticipanteBC objParticipanteBC = new ParticipanteBC();

            objParticipanteBC.EnviarMail(CodEvaluacion, token);
        }

        [WebMethod]
        public string CalcularReporte(string DNI, bool administrador)
        {
            //CreatPDFwithItextSharp(DNI, administrador);
            bool usekey = Convert.ToBoolean(ConfigurationManager.AppSettings["USEKEY_TAL"]);
            int[] orientaciones = new int[6] { 0, 0, 0, 0, 0, 0 }; //Ejecucion: 0, Pensamiento: 1, Innovacion: 2, Liderazgo: 3
            // Personas: 4, Estructura: 5

            ResultadoBC objResultadoBC = new ResultadoBC();
            TalentoBC objTalentoBC = new TalentoBC();

            List<RecomendacionBE> lstDescripcionesMasDesarrolladas = new List<RecomendacionBE>();
            List<RecomendacionBE> lstDescripcionesMenosDesarrolladas = new List<RecomendacionBE>();
            List<RecomendacionBE> lstSugerencias = null;

            // Define an object to pass to the word API for missing parameters
            string nombreWord = "";

            //object fileName = null;
            string fileName = "";
            if (administrador)
                fileName = Server.MapPath(@"~/Reporte/Plantilla/Plantilla_Reporte_Talentos_Admin.docx");
            else
                fileName = Server.MapPath(@"~/Reporte/Plantilla/Plantilla_Reporte_Talentos_Alum.docx");

            try
            {
                ResultadoFinalBE objResultadoBE = objResultadoBC.ObtenerResultadoParticipante(DNI, ref orientaciones);
                List<TalentoBE> lstTalentosTotales = objTalentoBC.ListarTalentosReporteTodos();

                string nombreWord_Aux = objResultadoBE.NombreParticipante.ToUpper();

                nombreWord_Aux = nombreWord_Aux.Replace("Ó", "O");
                nombreWord_Aux = nombreWord_Aux.Replace("Í", "I");
                nombreWord_Aux = nombreWord_Aux.Replace("Á", "A");
                nombreWord_Aux = nombreWord_Aux.Replace("É", "E");
                nombreWord_Aux = nombreWord_Aux.Replace("Ú", "U");
                nombreWord_Aux = nombreWord_Aux.Replace("Ñ", "N");



                nombreWord = "Reporte_Talentos_" + nombreWord_Aux.Replace(" ", "") + ".docx";
                //nombreWord = "Reporte_Talentos_.docx";
                string nombrePDF = nombreWord.Replace(".docx", ".pdf");
                string pathPDF = Server.MapPath("~/Reporte/");
                string archivoNuevo = Server.MapPath("~/Reporte/" + nombreWord);

                EliminarArchivosTemporales(archivoNuevo, "");
                EliminarArchivosTemporales(pathPDF + nombrePDF, "");

                File.Copy(fileName, archivoNuevo);

                RecomendacionBC objRecomenracionBC = new RecomendacionBC();
                lstDescripcionesMasDesarrolladas = objTalentoBC.ObtenerDescripcionesTalentosMasDesarrollados(objResultadoBE.lstTalentosMasDesarrollados, 1);

                if (administrador)
                    lstDescripcionesMenosDesarrolladas = objTalentoBC.ObtenerDescripcionesTalentosMenosDesarrollados(objResultadoBE.lstTalentosMenosDesarrollados, 2);

                lstSugerencias = objRecomenracionBC.ObtenerSugerenciasTalentosSeleccionados(objResultadoBE.lstTalentosMasDesarrollados, objResultadoBE.lstTalentosMenosDesarrollados, administrador);

                using (WordprocessingDocument doc = WordprocessingDocument.Open(archivoNuevo, true))
                {
                    // Find the first table in the document.
                    List<Table> lstTables = doc.MainDocumentPart.Document.Body.Elements<Table>().ToList();

                    //FormatearTablaTalentosGeneral(lstTables[1], lstTalentosTotales);
                    FormatearTablaResultadoChart(lstTables[2], orientaciones);
                    FormatearTablaTalentosMasDesarrollados(lstTables[3], objResultadoBE.lstTalentosMasDesarrollados, objResultadoBE.lstTEMasDesarrollados);
                    if (administrador)
                        FormatearTablaTalentosMenosDesarrollados(lstTables[4], objResultadoBE.lstTalentosMenosDesarrollados, objResultadoBE.lstTEMenosDesarrollados);

                    ModifyChartSimplified(doc.MainDocumentPart, "B", 2, (100 * orientaciones[0] / 12).ToString(), false);
                    ModifyChartSimplified(doc.MainDocumentPart, "B", 3, (100 * orientaciones[1] / 12).ToString(), false);
                    ModifyChartSimplified(doc.MainDocumentPart, "B", 4, (100 * orientaciones[2] / 12).ToString(), false);
                    ModifyChartSimplified(doc.MainDocumentPart, "B", 5, (100 * orientaciones[3] / 12).ToString(), false);
                    ModifyChartSimplified(doc.MainDocumentPart, "B", 6, (100 * orientaciones[4] / 12).ToString(), false);
                    ModifyChartSimplified(doc.MainDocumentPart, "B", 7, (100 * orientaciones[5] / 12).ToString(), false);


                    if (administrador)
                    {
                        MostrarDescripcionesTalentos(lstTables[5], archivoNuevo, lstDescripcionesMasDesarrolladas, administrador, doc);
                        MostrarDescripcionesTalentosMenosDes(lstTables[6], archivoNuevo, lstDescripcionesMenosDesarrolladas, administrador, doc);
                    }
                    else
                        //estab comentado esto
                        MostrarDescripcionesTalentos(lstTables[4], archivoNuevo, lstDescripcionesMasDesarrolladas, administrador, doc);

                    MostrarSugerenciasTalentos(lstTables, archivoNuevo, lstSugerencias, administrador, doc);


                }


                //Mostrar fecha test participante
                SearchAndReplace(archivoNuevo, "#FECHA_TEST#", objResultadoBE.FechaTest);

                // Mostrar Nombre Participante
                SearchAndReplace(archivoNuevo, "#NOMBRE_PERSONA#", objResultadoBE.NombreParticipante);

                if (administrador)
                    SearchAndReplace(archivoNuevo, "#NOMBRE_PERSONA#", objResultadoBE.NombreParticipante);
                else
                    SearchAndReplace(archivoNuevo, "#NOMBRE_PERSONA#", objResultadoBE.NombreParticipante.Split(' ')[0]);


                if (usekey)
                {
                    //API online
                    ConvertirWordToPDF(archivoNuevo, nombreWord, pathPDF, nombrePDF);
                }
                else
                {
                    // TODO: Generación del reporte con Interop
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
                    catch (Exception ex2)
                    {
                        throw ex2;
                    }
                    finally
                    {
                        object save = false;

                        ((Microsoft.Office.Interop.Word._Document)document).Close(ref save,
                                       ref missing,
                                       ref missing);
                        /*
                        document.Close(ref save,
                                       ref missing,
                                       ref missing);
                        */
                        /*
                        word.Application.Quit(ref missing,
                                              ref missing,
                                              ref missing);
                        */
                        ((Microsoft.Office.Interop.Word._Application)word).Quit();


                    }

                }

                EliminarArchivosTemporales(archivoNuevo, "");

                return nombrePDF;
            }
            catch (Exception ex)
            {
                LogDALC objLogDALC = new LogDALC();
                string mensaje = "Metodo: CalcularReporte. Mensaje: " + ex.Message;
                objLogDALC.InsertarLog(mensaje);

                throw ex;
            }
        }

       

        private void ConvertirWordToPDF2(string archivoNuevo, string nombreWord)
        {
            // convert a Word document and save the PDF to a file            
            //string baseDirectory = AppDomain.CurrentDomain.BaseDirectory;
            //string inFile = Path.GetFullPath(Path.Combine(baseDirectory, @"..\..\..\..\test-files\test.docx"));
            FileStream intStream = new FileStream(archivoNuevo, FileMode.Open);
            //string outFile = Path.Combine(archivoNuevo.Replace(".docx", ".pdf"));
            FileStream outStream = new FileStream(archivoNuevo.Replace(".docx", ".pdf"), FileMode.Create);
            try
            {
                Word2Pdf convertApi = new Word2Pdf();
                convertApi.ConvertFile(intStream, nombreWord, outStream, false);
            }
            catch (Exception ex)
            {
                LogDALC objLogDALC = new LogDALC();
                string mensaje = "Metodo: ConvertirWordToPDF2. Mensaje: " + ex.Message;
                objLogDALC.InsertarLog(mensaje);

                throw ex;
            }
            finally
            {
                outStream.Close();
                intStream.Close();
            }
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

        [WebMethod]
        public List<CuadroResultadoBE> CuadroResultadoListar()
        {
            ResultadoDALC objResultadoDALC = new ResultadoDALC();

            return objResultadoDALC.CuadroResultadoListar();
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

        private string replaceTildes(string str)
        {
            str = str.Replace("ó", "o");
            str = str.Replace("í", "i");
            str = str.Replace("á", "a");
            str = str.Replace("é", "e");

            return str;
        }

        private void MostrarSugerenciasTalentos(List<Table> lstTables, string fileName, List<RecomendacionBE> lstSugerencias, bool administrador, WordprocessingDocument doc)
        {
            try
            {

                if (lstSugerencias.Count > 0)
                {
                    Table table = null;

                    if (administrador)
                        table = lstTables[7];
                    else
                        table = lstTables[5];

                    TableRow rt1 = table.Elements<TableRow>().ElementAt(0);
                    Table table1 = rt1.Elements<TableCell>().ElementAt(1).Elements<Table>().First();

                    TableRow rt2 = table.Elements<TableRow>().ElementAt(1);
                    Table table2 = rt2.Elements<TableCell>().ElementAt(1).Elements<Table>().First();

                    TableRow rt3 = table.Elements<TableRow>().ElementAt(2);
                    Table table3 = rt3.Elements<TableCell>().ElementAt(1).Elements<Table>().First();

                    TableRow rt4 = table.Elements<TableRow>().ElementAt(3);
                    Table table4 = rt4.Elements<TableCell>().ElementAt(1).Elements<Table>().First();

                    TableRow rt5 = table.Elements<TableRow>().ElementAt(4);
                    Table table5 = rt5.Elements<TableCell>().ElementAt(1).Elements<Table>().First();

                    TableRow rt6 = table.Elements<TableRow>().ElementAt(5);
                    Table table6 = rt6.Elements<TableCell>().ElementAt(1).Elements<Table>().First();

                    TableRow rt7 = table.Elements<TableRow>().ElementAt(6);
                    Table table7 = rt7.Elements<TableCell>().ElementAt(1).Elements<Table>().First();

                    TableRow rt8 = table.Elements<TableRow>().ElementAt(7);
                    Table table8 = rt8.Elements<TableCell>().ElementAt(1).Elements<Table>().First();

                    TableRow rt9 = table.Elements<TableRow>().ElementAt(8);
                    Table table9 = rt9.Elements<TableCell>().ElementAt(1).Elements<Table>().First();

                    TableRow rt10 = table.Elements<TableRow>().ElementAt(9);
                    Table table10 = rt10.Elements<TableCell>().ElementAt(1).Elements<Table>().First();

                    TableRow rt11 = table.Elements<TableRow>().ElementAt(10);
                    Table table11 = rt11.Elements<TableCell>().ElementAt(1).Elements<Table>().First();

                    TableRow rt12 = table.Elements<TableRow>().ElementAt(11);
                    Table table12 = rt12.Elements<TableCell>().ElementAt(1).Elements<Table>().First();

                    int idTalentoAnterior = 0;
                    int cont = 1;
                    int contTal = 1;
                    int fila = 0;
                    int tope = 13;

                    string pathImages = Server.MapPath("~/Images/Talentos/");
                    string imagen = pathImages + lstSugerencias[0].Talento + ".png";
                    imagen = replaceTildes(imagen);


                    idTalentoAnterior = lstSugerencias[0].IdTalento;
                    contTal = 1;

                    TableRow row1 = table.Elements<TableRow>().ElementAt(0);
                    TableCell cell1 = row1.Elements<TableCell>().ElementAt(0);
                    AddImage(cell1, doc.MainDocumentPart, imagen, "imId20");

                    /*
                    Paragraph p1 = cell1_0.Elements<Paragraph>().First();
                    Run r1 = p1.Elements<Run>().First();
                    Text t1 = r1.Elements<Text>().First();
                    t1.Text = "¿Cómo desarrollar tu talento:\"" + lstSugerencias[0].Talento + "\"?";
                    */



                    for (int i = 0; i < lstSugerencias.Count; i++)
                    {
                        if (!lstSugerencias[i].IdTalento.Equals(idTalentoAnterior))
                        {
                            break;
                        }
                        TableRow row = table1.Elements<TableRow>().ElementAt(fila);
                        TableCell cell = row.Elements<TableCell>().ElementAt(0);
                        Paragraph p = cell.Elements<Paragraph>().First();
                        Run r = p.Elements<Run>().First();
                        Text t = r.Elements<Text>().First();
                        t.Text = lstSugerencias[i].Descripcion;
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

                        if (t.Text.Contains("RECOMEND"))
                        {
                            cell.RemoveAllChildren();
                            cell.Remove();
                            row.RemoveAllChildren();
                            row.Remove();
                        }
                    }

                    idTalentoAnterior = lstSugerencias[cont - 1].IdTalento;
                    contTal = 1;
                    fila = 0;
                    imagen = pathImages + lstSugerencias[cont - 1].Talento + ".png";
                    imagen = replaceTildes(imagen);

                    TableRow row2 = table.Elements<TableRow>().ElementAt(1);
                    TableCell cell2 = row2.Elements<TableCell>().ElementAt(0);
                    AddImage(cell2, doc.MainDocumentPart, imagen, "imId21");

                    /*
                    Paragraph p2 = cell2.Elements<Paragraph>().First();
                    Run r2 = p2.Elements<Run>().First();
                    Text t2 = r2.Elements<Text>().First();
                    t2.Text = "¿Cómo desarrollar tu talento:\"" + lstSugerencias[cont - 1].Talento + "\"?";
                    */

                    for (int i = cont - 1; i < lstSugerencias.Count; i++)
                    {
                        if (!lstSugerencias[i].IdTalento.Equals(idTalentoAnterior))
                        {
                            break;
                        }

                        TableRow row = table2.Elements<TableRow>().ElementAt(fila);
                        TableCell cell = row.Elements<TableCell>().ElementAt(0);
                        Paragraph p = cell.Elements<Paragraph>().First();
                        Run r = p.Elements<Run>().First();
                        Text t = r.Elements<Text>().First();
                        t.Text = lstSugerencias[i].Descripcion;
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

                        if (t.Text.Contains("RECOMEND"))
                        {
                            cell.RemoveAllChildren();
                            cell.Remove();
                            row.RemoveAllChildren();
                            row.Remove();
                        }
                    }

                    idTalentoAnterior = lstSugerencias[cont - 1].IdTalento;
                    contTal = 1;
                    fila = 0;
                    imagen = pathImages + lstSugerencias[cont - 1].Talento + ".png";
                    imagen = replaceTildes(imagen);

                    TableRow row3 = table.Elements<TableRow>().ElementAt(2);
                    TableCell cell3 = row3.Elements<TableCell>().ElementAt(0);

                    AddImage(cell3, doc.MainDocumentPart, imagen, "imId22");

                    /*
                    Paragraph p3 = cell3.Elements<Paragraph>().First();
                    Run r3 = p3.Elements<Run>().First();
                    Text t3 = r3.Elements<Text>().First();
                    t3.Text = "¿Cómo desarrollar tu talento:\"" + lstSugerencias[cont - 1].Talento + "\"?";
                    */
                    for (int i = cont - 1; i < lstSugerencias.Count; i++)
                    {
                        if (!lstSugerencias[i].IdTalento.Equals(idTalentoAnterior))
                        {
                            break;
                        }

                        TableRow row = table3.Elements<TableRow>().ElementAt(fila);
                        TableCell cell = row.Elements<TableCell>().ElementAt(0);
                        Paragraph p = cell.Elements<Paragraph>().First();
                        Run r = p.Elements<Run>().First();
                        Text t = r.Elements<Text>().First();
                        t.Text = lstSugerencias[i].Descripcion;
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

                        if (t.Text.Contains("RECOMEND"))
                        {
                            cell.RemoveAllChildren();
                            cell.Remove();
                            row.RemoveAllChildren();
                            row.Remove();
                        }
                    }

                    idTalentoAnterior = lstSugerencias[cont - 1].IdTalento;
                    contTal = 1;
                    fila = 0;
                    imagen = pathImages + lstSugerencias[cont - 1].Talento + ".png";
                    imagen = replaceTildes(imagen);

                    TableRow row4 = table.Elements<TableRow>().ElementAt(3);
                    TableCell cell4 = row4.Elements<TableCell>().ElementAt(0);
                    AddImage(cell4, doc.MainDocumentPart, imagen, "imId23");
                    /*
                    Paragraph p4 = cell4.Elements<Paragraph>().First();
                    Run r4 = p4.Elements<Run>().First();
                    Text t4 = r4.Elements<Text>().First();
                    t4.Text = "¿Cómo desarrollar tu talento:\"" + lstSugerencias[cont - 1].Talento + "\"?";
                    */
                    for (int i = cont - 1; i < lstSugerencias.Count; i++)
                    {
                        if (!lstSugerencias[i].IdTalento.Equals(idTalentoAnterior))
                        {
                            break;
                        }

                        TableRow row = table4.Elements<TableRow>().ElementAt(fila);
                        TableCell cell = row.Elements<TableCell>().ElementAt(0);
                        Paragraph p = cell.Elements<Paragraph>().First();
                        Run r = p.Elements<Run>().First();
                        Text t = r.Elements<Text>().First();
                        t.Text = lstSugerencias[i].Descripcion;
                        cont++;
                        contTal++;
                        fila++;
                    }

                    for (int i = fila; i < tope; i++)
                    {
                        TableRow row = table4.Elements<TableRow>().Last();
                        TableCell cell = row.Elements<TableCell>().ElementAt(0);
                        Paragraph p = cell.Elements<Paragraph>().First();
                        Run r = p.Elements<Run>().First();
                        Text t = r.Elements<Text>().First();

                        if (t.Text.Contains("RECOMEND"))
                        {
                            cell.RemoveAllChildren();
                            cell.Remove();
                            row.RemoveAllChildren();
                            row.Remove();
                        }
                    }


                    idTalentoAnterior = lstSugerencias[cont - 1].IdTalento;
                    contTal = 1;
                    fila = 0;
                    imagen = pathImages + lstSugerencias[cont - 1].Talento + ".png";
                    imagen = replaceTildes(imagen);

                    TableRow row5 = table.Elements<TableRow>().ElementAt(4);
                    TableCell cell5 = row5.Elements<TableCell>().ElementAt(0);
                    AddImage(cell5, doc.MainDocumentPart, imagen, "imId24");

                    /*
                    Paragraph p5 = cell5.Elements<Paragraph>().First();
                    Run r5 = p5.Elements<Run>().First();
                    Text t5 = r5.Elements<Text>().First();
                    t5.Text = "¿Cómo desarrollar tu talento:\"" + lstSugerencias[cont - 1].Talento + "\"?";
                    */

                    for (int i = cont - 1; i < lstSugerencias.Count; i++)
                    {
                        if (!lstSugerencias[i].IdTalento.Equals(idTalentoAnterior))
                        {
                            break;
                        }

                        TableRow row = table5.Elements<TableRow>().ElementAt(fila);
                        TableCell cell = row.Elements<TableCell>().ElementAt(0);
                        Paragraph p = cell.Elements<Paragraph>().First();
                        Run r = p.Elements<Run>().First();
                        Text t = r.Elements<Text>().First();
                        t.Text = lstSugerencias[i].Descripcion;
                        cont++;
                        contTal++;
                        fila++;
                    }

                    for (int i = fila; i < tope; i++)
                    {
                        TableRow row = table5.Elements<TableRow>().Last();
                        TableCell cell = row.Elements<TableCell>().ElementAt(0);
                        Paragraph p = cell.Elements<Paragraph>().First();
                        Run r = p.Elements<Run>().First();
                        Text t = r.Elements<Text>().First();

                        if (t.Text.Contains("RECOMEND"))
                        {
                            cell.RemoveAllChildren();
                            cell.Remove();
                            row.RemoveAllChildren();
                            row.Remove();
                        }
                    }

                    idTalentoAnterior = lstSugerencias[cont - 1].IdTalento;
                    contTal = 1;
                    fila = 0;

                    TableRow row6 = table.Elements<TableRow>().ElementAt(5);
                    TableCell cell6 = row6.Elements<TableCell>().ElementAt(0);

                    imagen = pathImages + lstSugerencias[cont - 1].Talento + ".png";
                    imagen = replaceTildes(imagen);
                    AddImage(cell6, doc.MainDocumentPart, imagen, "imId25");

                    /*
                    Paragraph p6 = cell6.Elements<Paragraph>().First();
                    Run r6 = p6.Elements<Run>().First();
                    Text t6 = r6.Elements<Text>().First();
                    t6.Text = "¿Cómo desarrollar tu talento:\"" + lstSugerencias[cont - 1].Talento + "\"?";
                    */
                    for (int i = cont - 1; i < lstSugerencias.Count; i++)
                    {
                        if (!lstSugerencias[i].IdTalento.Equals(idTalentoAnterior))
                        {
                            break;
                        }

                        TableRow row = table6.Elements<TableRow>().ElementAt(fila);
                        TableCell cell = row.Elements<TableCell>().ElementAt(0);
                        Paragraph p = cell.Elements<Paragraph>().First();
                        Run r = p.Elements<Run>().First();
                        Text t = r.Elements<Text>().First();
                        t.Text = lstSugerencias[i].Descripcion;
                        cont++;
                        contTal++;
                        fila++;
                    }

                    for (int i = fila; i < tope; i++)
                    {
                        TableRow row = table6.Elements<TableRow>().Last();
                        TableCell cell = row.Elements<TableCell>().ElementAt(0);
                        Paragraph p = cell.Elements<Paragraph>().First();
                        Run r = p.Elements<Run>().First();
                        Text t = r.Elements<Text>().First();

                        if (t.Text.Contains("RECOMEND"))
                        {
                            cell.RemoveAllChildren();
                            cell.Remove();
                            row.RemoveAllChildren();
                            row.Remove();
                        }
                    }

                    idTalentoAnterior = lstSugerencias[cont - 1].IdTalento;
                    contTal = 1;
                    fila = 0;

                    TableRow row7 = table.Elements<TableRow>().ElementAt(6);
                    TableCell cell7 = row7.Elements<TableCell>().ElementAt(0);
                    imagen = pathImages + lstSugerencias[cont - 1].Talento + ".png";
                    imagen = replaceTildes(imagen);
                    AddImage(cell7, doc.MainDocumentPart, imagen, "imId26");

                    /*
                    Paragraph p7 = cell7.Elements<Paragraph>().First();
                    Run r7 = p7.Elements<Run>().First();
                    Text t7 = r7.Elements<Text>().First();
                    t7.Text = "¿Cómo desarrollar tu talento:\"" + lstSugerencias[cont - 1].Talento + "\"?";
                    */
                    for (int i = cont - 1; i < lstSugerencias.Count; i++)
                    {
                        if (!lstSugerencias[i].IdTalento.Equals(idTalentoAnterior))
                        {
                            break;
                        }

                        TableRow row = table7.Elements<TableRow>().ElementAt(fila);
                        TableCell cell = row.Elements<TableCell>().ElementAt(0);
                        Paragraph p = cell.Elements<Paragraph>().First();
                        Run r = p.Elements<Run>().First();
                        Text t = r.Elements<Text>().First();
                        t.Text = lstSugerencias[i].Descripcion;
                        cont++;
                        contTal++;
                        fila++;
                    }

                    for (int i = fila; i < tope; i++)
                    {
                        TableRow row = table7.Elements<TableRow>().Last();
                        TableCell cell = row.Elements<TableCell>().ElementAt(0);
                        Paragraph p = cell.Elements<Paragraph>().First();
                        Run r = p.Elements<Run>().First();
                        Text t = r.Elements<Text>().First();

                        if (t.Text.Contains("RECOMEND"))
                        {
                            cell.RemoveAllChildren();
                            cell.Remove();
                            row.RemoveAllChildren();
                            row.Remove();
                        }
                    }

                    idTalentoAnterior = lstSugerencias[cont - 1].IdTalento;
                    contTal = 1;
                    fila = 0;

                    TableRow row8 = table.Elements<TableRow>().ElementAt(7);
                    TableCell cell8 = row8.Elements<TableCell>().ElementAt(0);
                    imagen = pathImages + lstSugerencias[cont - 1].Talento + ".png";
                    imagen = replaceTildes(imagen);
                    AddImage(cell8, doc.MainDocumentPart, imagen, "imId27");
                    /*
                    Paragraph p8 = cell8.Elements<Paragraph>().First();
                    Run r8 = p8.Elements<Run>().First();
                    Text t8 = r8.Elements<Text>().First();
                    t8.Text = "¿Cómo desarrollar tu talento:\"" + lstSugerencias[cont - 1].Talento + "\"?";
                     * */
                    for (int i = cont - 1; i < lstSugerencias.Count; i++)
                    {
                        if (!lstSugerencias[i].IdTalento.Equals(idTalentoAnterior))
                        {
                            break;
                        }

                        TableRow row = table8.Elements<TableRow>().ElementAt(fila);
                        TableCell cell = row.Elements<TableCell>().ElementAt(0);
                        Paragraph p = cell.Elements<Paragraph>().First();
                        Run r = p.Elements<Run>().First();
                        Text t = r.Elements<Text>().First();
                        t.Text = lstSugerencias[i].Descripcion;
                        cont++;
                        contTal++;
                        fila++;
                    }

                    for (int i = fila; i < tope; i++)
                    {
                        TableRow row = table8.Elements<TableRow>().Last();
                        TableCell cell = row.Elements<TableCell>().ElementAt(0);
                        Paragraph p = cell.Elements<Paragraph>().First();
                        Run r = p.Elements<Run>().First();
                        Text t = r.Elements<Text>().First();

                        if (t.Text.Contains("RECOMEND"))
                        {
                            cell.RemoveAllChildren();
                            cell.Remove();
                            row.RemoveAllChildren();
                            row.Remove();
                        }
                    }

                    idTalentoAnterior = lstSugerencias[cont - 1].IdTalento;
                    contTal = 1;
                    fila = 0;

                    TableRow row9 = table.Elements<TableRow>().ElementAt(8);
                    TableCell cell9 = row9.Elements<TableCell>().ElementAt(0);
                    imagen = pathImages + lstSugerencias[cont - 1].Talento + ".png";
                    imagen = replaceTildes(imagen);
                    AddImage(cell9, doc.MainDocumentPart, imagen, "imId28");

                    /*
                    Paragraph p9 = cell9.Elements<Paragraph>().First();
                    Run r9 = p9.Elements<Run>().First();
                    Text t9 = r9.Elements<Text>().First();
                    t9.Text = "¿Cómo desarrollar tu talento:\"" + lstSugerencias[cont - 1].Talento + "\"?";
                    */
                    for (int i = cont - 1; i < lstSugerencias.Count; i++)
                    {
                        if (!lstSugerencias[i].IdTalento.Equals(idTalentoAnterior))
                        {
                            break;
                        }

                        TableRow row = table9.Elements<TableRow>().ElementAt(fila);
                        TableCell cell = row.Elements<TableCell>().ElementAt(0);
                        Paragraph p = cell.Elements<Paragraph>().First();
                        Run r = p.Elements<Run>().First();
                        Text t = r.Elements<Text>().First();
                        t.Text = lstSugerencias[i].Descripcion;
                        cont++;
                        contTal++;
                        fila++;
                    }

                    for (int i = fila; i < tope; i++)
                    {
                        TableRow row = table9.Elements<TableRow>().Last();
                        TableCell cell = row.Elements<TableCell>().ElementAt(0);
                        Paragraph p = cell.Elements<Paragraph>().First();
                        Run r = p.Elements<Run>().First();
                        Text t = r.Elements<Text>().First();

                        if (t.Text.Contains("RECOMEND"))
                        {
                            cell.RemoveAllChildren();
                            cell.Remove();
                            row.RemoveAllChildren();
                            row.Remove();
                        }
                    }

                    idTalentoAnterior = lstSugerencias[cont - 1].IdTalento;
                    contTal = 1;
                    fila = 0;

                    TableRow row10 = table.Elements<TableRow>().ElementAt(9);
                    TableCell cell10 = row10.Elements<TableCell>().ElementAt(0);
                    imagen = pathImages + lstSugerencias[cont - 1].Talento + ".png";
                    imagen = replaceTildes(imagen);
                    AddImage(cell10, doc.MainDocumentPart, imagen, "imId29");

                    /*
                    Paragraph p10 = cell10.Elements<Paragraph>().First();
                    Run r10 = p10.Elements<Run>().First();
                    Text t10 = r10.Elements<Text>().First();
                    t10.Text = "¿Cómo desarrollar tu talento:\"" + lstSugerencias[cont - 1].Talento + "\"?";
                    */
                    for (int i = cont - 1; i < lstSugerencias.Count; i++)
                    {
                        if (!lstSugerencias[i].IdTalento.Equals(idTalentoAnterior))
                        {
                            break;
                        }

                        TableRow row = table10.Elements<TableRow>().ElementAt(fila);
                        TableCell cell = row.Elements<TableCell>().ElementAt(0);
                        Paragraph p = cell.Elements<Paragraph>().First();
                        Run r = p.Elements<Run>().First();
                        Text t = r.Elements<Text>().First();
                        t.Text = lstSugerencias[i].Descripcion;
                        cont++;
                        contTal++;
                        fila++;
                    }

                    for (int i = fila; i < tope; i++)
                    {
                        TableRow row = table10.Elements<TableRow>().Last();
                        TableCell cell = row.Elements<TableCell>().ElementAt(0);
                        Paragraph p = cell.Elements<Paragraph>().First();
                        Run r = p.Elements<Run>().First();
                        Text t = r.Elements<Text>().First();

                        if (t.Text.Contains("RECOMEND"))
                        {
                            cell.RemoveAllChildren();
                            cell.Remove();
                            row.RemoveAllChildren();
                            row.Remove();
                        }
                    }

                    idTalentoAnterior = lstSugerencias[cont - 1].IdTalento;
                    contTal = 1;
                    fila = 0;

                    TableRow row11 = table.Elements<TableRow>().ElementAt(10);
                    TableCell cell11 = row11.Elements<TableCell>().ElementAt(0);
                    imagen = pathImages + lstSugerencias[cont - 1].Talento + ".png";
                    imagen = replaceTildes(imagen);
                    AddImage(cell11, doc.MainDocumentPart, imagen, "imId30");
                    /*
                    Paragraph p11 = cell11.Elements<Paragraph>().First();
                    Run r11 = p11.Elements<Run>().First();
                    Text t11 = r11.Elements<Text>().First();
                    t11.Text = "¿Cómo desarrollar tu talento:\"" + lstSugerencias[cont - 1].Talento + "\"?";
                    */
                    for (int i = cont - 1; i < lstSugerencias.Count; i++)
                    {
                        if (!lstSugerencias[i].IdTalento.Equals(idTalentoAnterior))
                        {
                            break;
                        }

                        TableRow row = table11.Elements<TableRow>().ElementAt(fila);
                        TableCell cell = row.Elements<TableCell>().ElementAt(0);
                        Paragraph p = cell.Elements<Paragraph>().First();
                        Run r = p.Elements<Run>().First();
                        Text t = r.Elements<Text>().First();
                        t.Text = lstSugerencias[i].Descripcion;
                        cont++;
                        contTal++;
                        fila++;
                    }

                    for (int i = fila; i < tope; i++)
                    {
                        TableRow row = table11.Elements<TableRow>().Last();
                        TableCell cell = row.Elements<TableCell>().ElementAt(0);
                        Paragraph p = cell.Elements<Paragraph>().First();
                        Run r = p.Elements<Run>().First();
                        Text t = r.Elements<Text>().First();

                        if (t.Text.Contains("RECOMEND"))
                        {
                            cell.RemoveAllChildren();
                            cell.Remove();
                            row.RemoveAllChildren();
                            row.Remove();
                        }
                    }

                    contTal = 1;
                    fila = 0;
                    if (lstSugerencias.Count > (cont - 1))
                    {
                        idTalentoAnterior = lstSugerencias[cont - 1].IdTalento;

                        TableRow row12 = table.Elements<TableRow>().ElementAt(11);
                        TableCell cell12 = row12.Elements<TableCell>().ElementAt(0);
                        imagen = pathImages + lstSugerencias[cont - 1].Talento + ".png";
                        imagen = replaceTildes(imagen);
                        AddImage(cell12, doc.MainDocumentPart, imagen, "imId31");
                        /*
                        Paragraph p12 = cell12.Elements<Paragraph>().First();
                        Run r12 = p12.Elements<Run>().First();
                        Text t12 = r12.Elements<Text>().First();
                        t12.Text = "¿Cómo desarrollar tu talento:\"" + lstSugerencias[cont - 1].Talento + "\"?";
                        */
                        for (int i = cont - 1; i < lstSugerencias.Count; i++)
                        {
                            if (!lstSugerencias[i].IdTalento.Equals(idTalentoAnterior))
                            {
                                break;
                            }

                            TableRow row = table12.Elements<TableRow>().ElementAt(fila);
                            TableCell cell = row.Elements<TableCell>().ElementAt(0);
                            Paragraph p = cell.Elements<Paragraph>().First();
                            Run r = p.Elements<Run>().First();
                            Text t = r.Elements<Text>().First();
                            t.Text = lstSugerencias[i].Descripcion;
                            cont++;
                            contTal++;
                            fila++;
                        }

                        for (int i = fila; i < tope; i++)
                        {
                            TableRow row = table12.Elements<TableRow>().Last();
                            TableCell cell = row.Elements<TableCell>().ElementAt(0);
                            Paragraph p = cell.Elements<Paragraph>().First();
                            Run r = p.Elements<Run>().First();
                            Text t = r.Elements<Text>().First();

                            if (t.Text.Contains("RECOMEND"))
                            {
                                cell.RemoveAllChildren();
                                cell.Remove();
                                row.RemoveAllChildren();
                                row.Remove();
                            }
                        }
                    }
                    else
                    {
                        TableRow rowTal = table.Elements<TableRow>().ElementAt(11);
                        TableCell cellTal = rowTal.Elements<TableCell>().ElementAt(0);
                        Paragraph pTal = cellTal.Elements<Paragraph>().First();
                        Run rTal = pTal.Elements<Run>().First();
                        Text tTal = rTal.Elements<Text>().First();

                        TableCell cellDes = rowTal.Elements<TableCell>().ElementAt(1);

                        if (tTal.Text.Contains("Preg"))
                        {
                            cellTal.RemoveAllChildren();
                            cellTal.Remove();
                            cellDes.RemoveAllChildren();
                            cellDes.Remove();
                            rowTal.RemoveAllChildren();
                            rowTal.Remove();
                        }
                    }

                    if (administrador)
                    {
                        TableRow rt13 = table.Elements<TableRow>().ElementAt(12);
                        Table table13 = rt13.Elements<TableCell>().ElementAt(1).Elements<Table>().First();

                        TableRow rt14 = table.Elements<TableRow>().ElementAt(13);
                        Table table14 = rt14.Elements<TableCell>().ElementAt(1).Elements<Table>().First();

                        TableRow rt15 = table.Elements<TableRow>().ElementAt(14);
                        Table table15 = rt15.Elements<TableCell>().ElementAt(1).Elements<Table>().First();

                        TableRow rt16 = table.Elements<TableRow>().ElementAt(15);
                        Table table16 = rt16.Elements<TableCell>().ElementAt(1).Elements<Table>().First();

                        TableRow rt17 = table.Elements<TableRow>().ElementAt(16);
                        Table table17 = rt17.Elements<TableCell>().ElementAt(1).Elements<Table>().First();

                        TableRow rt18 = table.Elements<TableRow>().ElementAt(17);
                        Table table18 = rt18.Elements<TableCell>().ElementAt(1).Elements<Table>().First();

                        idTalentoAnterior = lstSugerencias[cont - 1].IdTalento;
                        contTal = 1;
                        fila = 0;

                        TableRow row13 = table.Elements<TableRow>().ElementAt(12);
                        TableCell cell13 = row13.Elements<TableCell>().ElementAt(0);

                        imagen = pathImages + lstSugerencias[cont - 1].Talento + ".png";
                        imagen = replaceTildes(imagen);
                        AddImage(cell13, doc.MainDocumentPart, imagen, "imId32");
                        /*
                        Paragraph p13 = cell13.Elements<Paragraph>().First();
                        Run r13 = p13.Elements<Run>().First();
                        Text t13 = r13.Elements<Text>().First();
                        t13.Text = "¿Cómo desarrollar tu talento:\"" + lstSugerencias[cont - 1].Talento + "\"?";
                        */
                        for (int i = cont - 1; i < lstSugerencias.Count; i++)
                        {
                            if (!lstSugerencias[i].IdTalento.Equals(idTalentoAnterior))
                            {
                                break;
                            }

                            TableRow row = table13.Elements<TableRow>().ElementAt(fila);
                            TableCell cell = row.Elements<TableCell>().ElementAt(0);
                            Paragraph p = cell.Elements<Paragraph>().First();
                            Run r = p.Elements<Run>().First();
                            Text t = r.Elements<Text>().First();
                            t.Text = lstSugerencias[i].Descripcion;
                            cont++;
                            contTal++;
                            fila++;
                        }

                        for (int i = fila; i < tope; i++)
                        {
                            TableRow row = table13.Elements<TableRow>().Last();
                            TableCell cell = row.Elements<TableCell>().ElementAt(0);
                            Paragraph p = cell.Elements<Paragraph>().First();
                            Run r = p.Elements<Run>().First();
                            Text t = r.Elements<Text>().First();

                            if (t.Text.Contains("RECOMEND"))
                            {
                                cell.RemoveAllChildren();
                                cell.Remove();
                                row.RemoveAllChildren();
                                row.Remove();
                            }
                        }

                        idTalentoAnterior = lstSugerencias[cont - 1].IdTalento;
                        contTal = 1;
                        fila = 0;

                        TableRow row14 = table.Elements<TableRow>().ElementAt(13);
                        TableCell cell14 = row14.Elements<TableCell>().ElementAt(0);
                        imagen = pathImages + lstSugerencias[cont - 1].Talento + ".png";
                        imagen = replaceTildes(imagen);
                        AddImage(cell14, doc.MainDocumentPart, imagen, "imId33");

                        /*
                        Paragraph p14 = cell14.Elements<Paragraph>().First();
                        Run r14 = p14.Elements<Run>().First();
                        Text t14 = r14.Elements<Text>().First();
                        t14.Text = "¿Cómo desarrollar tu talento:\"" + lstSugerencias[cont - 1].Talento + "\"?";
                        */
                        for (int i = cont - 1; i < lstSugerencias.Count; i++)
                        {
                            if (!lstSugerencias[i].IdTalento.Equals(idTalentoAnterior))
                            {
                                break;
                            }

                            TableRow row = table14.Elements<TableRow>().ElementAt(fila);
                            TableCell cell = row.Elements<TableCell>().ElementAt(0);
                            Paragraph p = cell.Elements<Paragraph>().First();
                            Run r = p.Elements<Run>().First();
                            Text t = r.Elements<Text>().First();
                            t.Text = lstSugerencias[i].Descripcion;
                            cont++;
                            contTal++;
                            fila++;
                        }

                        for (int i = fila; i < tope; i++)
                        {
                            TableRow row = table14.Elements<TableRow>().Last();
                            TableCell cell = row.Elements<TableCell>().ElementAt(0);
                            Paragraph p = cell.Elements<Paragraph>().First();
                            Run r = p.Elements<Run>().First();
                            Text t = r.Elements<Text>().First();

                            if (t.Text.Contains("RECOMEND"))
                            {
                                cell.RemoveAllChildren();
                                cell.Remove();
                                row.RemoveAllChildren();
                                row.Remove();
                            }
                        }

                        idTalentoAnterior = lstSugerencias[cont - 1].IdTalento;
                        contTal = 1;
                        fila = 0;

                        TableRow row15 = table.Elements<TableRow>().ElementAt(14);
                        TableCell cell15 = row15.Elements<TableCell>().ElementAt(0);
                        imagen = pathImages + lstSugerencias[cont - 1].Talento + ".png";
                        imagen = replaceTildes(imagen);
                        AddImage(cell15, doc.MainDocumentPart, imagen, "imId34");

                        /*
                        Paragraph p15 = cell15.Elements<Paragraph>().First();
                        Run r15 = p15.Elements<Run>().First();
                        Text t15 = r15.Elements<Text>().First();
                        t15.Text = "¿Cómo desarrollar tu talento:\"" + lstSugerencias[cont - 1].Talento + "\"?";
                        */
                        for (int i = cont - 1; i < lstSugerencias.Count; i++)
                        {
                            if (!lstSugerencias[i].IdTalento.Equals(idTalentoAnterior))
                            {
                                break;
                            }

                            TableRow row = table15.Elements<TableRow>().ElementAt(fila);
                            TableCell cell = row.Elements<TableCell>().ElementAt(0);
                            Paragraph p = cell.Elements<Paragraph>().First();
                            Run r = p.Elements<Run>().First();
                            Text t = r.Elements<Text>().First();
                            t.Text = lstSugerencias[i].Descripcion;
                            cont++;
                            contTal++;
                            fila++;
                        }

                        for (int i = fila; i < tope; i++)
                        {
                            TableRow row = table15.Elements<TableRow>().Last();
                            TableCell cell = row.Elements<TableCell>().ElementAt(0);
                            Paragraph p = cell.Elements<Paragraph>().First();
                            Run r = p.Elements<Run>().First();
                            Text t = r.Elements<Text>().First();

                            if (t.Text.Contains("RECOMEND"))
                            {
                                cell.RemoveAllChildren();
                                cell.Remove();
                                row.RemoveAllChildren();
                                row.Remove();
                            }
                        }

                        idTalentoAnterior = lstSugerencias[cont - 1].IdTalento;
                        contTal = 1;
                        fila = 0;

                        TableRow row16 = table.Elements<TableRow>().ElementAt(15);
                        TableCell cell16 = row16.Elements<TableCell>().ElementAt(0);
                        imagen = pathImages + lstSugerencias[cont - 1].Talento + ".png";
                        imagen = replaceTildes(imagen);
                        AddImage(cell16, doc.MainDocumentPart, imagen, "imId35");

                        /*
                        Paragraph p16 = cell16.Elements<Paragraph>().First();
                        Run r16 = p16.Elements<Run>().First();
                        Text t16 = r16.Elements<Text>().First();
                        t16.Text = "¿Cómo desarrollar tu talento:\"" + lstSugerencias[cont - 1].Talento + "\"?";
                        */
                        for (int i = cont - 1; i < lstSugerencias.Count; i++)
                        {
                            if (!lstSugerencias[i].IdTalento.Equals(idTalentoAnterior))
                            {
                                break;
                            }

                            TableRow row = table16.Elements<TableRow>().ElementAt(fila);
                            TableCell cell = row.Elements<TableCell>().ElementAt(0);
                            Paragraph p = cell.Elements<Paragraph>().First();
                            Run r = p.Elements<Run>().First();
                            Text t = r.Elements<Text>().First();
                            t.Text = lstSugerencias[i].Descripcion;
                            cont++;
                            contTal++;
                            fila++;
                        }

                        for (int i = fila; i < tope; i++)
                        {
                            TableRow row = table16.Elements<TableRow>().Last();
                            TableCell cell = row.Elements<TableCell>().ElementAt(0);
                            Paragraph p = cell.Elements<Paragraph>().First();
                            Run r = p.Elements<Run>().First();
                            Text t = r.Elements<Text>().First();

                            if (t.Text.Contains("RECOMEND"))
                            {
                                cell.RemoveAllChildren();
                                cell.Remove();
                                row.RemoveAllChildren();
                                row.Remove();
                            }
                        }

                        idTalentoAnterior = lstSugerencias[cont - 1].IdTalento;
                        contTal = 1;
                        fila = 0;

                        TableRow row17 = table.Elements<TableRow>().ElementAt(16);
                        TableCell cell17 = row17.Elements<TableCell>().ElementAt(0);
                        imagen = pathImages + lstSugerencias[cont - 1].Talento + ".png";
                        imagen = replaceTildes(imagen);
                        AddImage(cell17, doc.MainDocumentPart, imagen, "imId36");

                        /*
                        Paragraph p17 = cell17.Elements<Paragraph>().First();
                        Run r17 = p17.Elements<Run>().First();
                        Text t17 = r17.Elements<Text>().First();
                        t17.Text = "¿Cómo desarrollar tu talento:\"" + lstSugerencias[cont - 1].Talento + "\"?";
                        */
                        for (int i = cont - 1; i < lstSugerencias.Count; i++)
                        {
                            if (!lstSugerencias[i].IdTalento.Equals(idTalentoAnterior))
                            {
                                break;
                            }

                            TableRow row = table17.Elements<TableRow>().ElementAt(fila);
                            TableCell cell = row.Elements<TableCell>().ElementAt(0);
                            Paragraph p = cell.Elements<Paragraph>().First();
                            Run r = p.Elements<Run>().First();
                            Text t = r.Elements<Text>().First();
                            t.Text = lstSugerencias[i].Descripcion;
                            cont++;
                            contTal++;
                            fila++;
                        }

                        for (int i = fila; i < tope; i++)
                        {
                            TableRow row = table17.Elements<TableRow>().Last();
                            TableCell cell = row.Elements<TableCell>().ElementAt(0);
                            Paragraph p = cell.Elements<Paragraph>().First();
                            Run r = p.Elements<Run>().First();
                            Text t = r.Elements<Text>().First();

                            if (t.Text.Contains("RECOMEND"))
                            {
                                cell.RemoveAllChildren();
                                cell.Remove();
                                row.RemoveAllChildren();
                                row.Remove();
                            }
                        }

                        contTal = 1;
                        fila = 0;
                        if (lstSugerencias.Count > (cont - 1))
                        {
                            idTalentoAnterior = lstSugerencias[cont - 1].IdTalento;

                            TableRow row18 = table.Elements<TableRow>().ElementAt(17);
                            TableCell cell18 = row18.Elements<TableCell>().ElementAt(0);
                            imagen = pathImages + lstSugerencias[cont - 1].Talento + ".png";
                            imagen = replaceTildes(imagen);
                            AddImage(cell18, doc.MainDocumentPart, imagen, "imId37");

                            /*
                            Paragraph p18 = cell18.Elements<Paragraph>().First();
                            Run r18 = p18.Elements<Run>().First();
                            Text t18 = r18.Elements<Text>().First();
                            t18.Text = "¿Cómo desarrollar tu talento:\"" + lstSugerencias[cont - 1].Talento + "\"?";
                            */
                            for (int i = cont - 1; i < lstSugerencias.Count; i++)
                            {
                                if (!lstSugerencias[i].IdTalento.Equals(idTalentoAnterior))
                                {
                                    break;
                                }

                                TableRow row = table18.Elements<TableRow>().ElementAt(fila);
                                TableCell cell = row.Elements<TableCell>().ElementAt(0);
                                Paragraph p = cell.Elements<Paragraph>().First();
                                Run r = p.Elements<Run>().First();
                                Text t = r.Elements<Text>().First();
                                t.Text = lstSugerencias[i].Descripcion;
                                cont++;
                                contTal++;
                                fila++;
                            }

                            for (int i = fila; i < tope; i++)
                            {
                                TableRow row = table18.Elements<TableRow>().Last();
                                TableCell cell = row.Elements<TableCell>().ElementAt(0);
                                Paragraph p = cell.Elements<Paragraph>().First();
                                Run r = p.Elements<Run>().First();
                                Text t = r.Elements<Text>().First();

                                if (t.Text.Contains("RECOMEND"))
                                {
                                    cell.RemoveAllChildren();
                                    cell.Remove();
                                    row.RemoveAllChildren();
                                    row.Remove();
                                }
                            }
                        }
                        else
                        {
                            TableRow rowTal = table.Elements<TableRow>().ElementAt(17);
                            TableCell cellTal = rowTal.Elements<TableCell>().ElementAt(0);
                            Paragraph pTal = cellTal.Elements<Paragraph>().First();
                            Run rTal = pTal.Elements<Run>().First();
                            Text tTal = rTal.Elements<Text>().First();

                            TableCell cellDes = rowTal.Elements<TableCell>().ElementAt(1);

                            if (tTal.Text.Contains("Preg"))
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
                }
            }
            catch (Exception ex)
            {

                LogDALC objLogDALC = new LogDALC();
                string mensaje = "Metodo: sugerencias :  Mensaje: " + ex.Message;
                objLogDALC.InsertarLog(mensaje);

                throw ex;
            }
            //try
            //{
            //    idTalentoAnterior = 0;
            //    recomendacionTalento = recomendacionTalento + lstSugerencias[0].Descripcion + "\r\n";
            //    talentoSugerencia = lstSugerencias[0].Talento;
            //    for (int i = 0; i < lstSugerencias.Count; i++)
            //    {
            //        if (contTal == 12)
            //        {
            //            int f = 0;

            //        }
            //        if (!lstSugerencias[i].IdTalento.Equals(idTalentoAnterior))
            //        {
            //            if (!primeraVez)
            //            {
            //                for (int j = cont - 1; j <= 13; j++)
            //                {
            //                    string etiquetaEliminar = etiquetaRecomendacionAux1.Replace("{1}", j.ToString());
            //                    RemoverParrafo(fileName, etiquetaEliminar);
            //                }
            //            }

            //            primeraVez = false;

            //            recomendacionTalento = recomendacionTalento.Substring(0, recomendacionTalento.Length - 2);

            //            SearchAndReplace(fileName, etiquetaTalento, talentoSugerencia);
            //            recomendacionTalento = "";
            //            cont = 1;

            //            etiquetaTalento = etiquetaTalento.Replace((contTal - 1).ToString(), contTal.ToString());
            //            etiquetaRecomendacionAux1 = etiquetaRecomendacion.Replace("{0}", contTal.ToString());
            //            contTal++;
            //            idTalentoAnterior = lstSugerencias[i].IdTalento;
            //        }

            //        recomendacionTalento = lstSugerencias[i].Descripcion;
            //        etiquetaRecomendacionAux2 = etiquetaRecomendacionAux1.Replace("{1}", cont.ToString());
            //        SearchAndReplace(fileName, etiquetaRecomendacionAux2, recomendacionTalento);
            //        cont++;

            //        talentoSugerencia = lstSugerencias[i].Talento;
            //    }

            //    recomendacionTalento = recomendacionTalento.Substring(0, recomendacionTalento.Length - 2);
            //    SearchAndReplace(fileName, etiquetaRecomendacion, recomendacionTalento);
            //    SearchAndReplace(fileName, etiquetaTalento, talentoSugerencia);

            //    for (int j = cont; j <= 13; j++)
            //    {
            //        string etiquetaEliminar = etiquetaRecomendacionAux1.Replace("{1}", j.ToString());
            //        RemoverParrafo(fileName, etiquetaEliminar);
            //    }

            //    RemoverParrafo(fileName, "#RECOMENDACION_11#");
            //    RemoverParrafo(fileName, "#RECOMENDACION_21#");
            //    RemoverParrafo(fileName, "#RECOMENDACION_31#");
            //    RemoverParrafo(fileName, "#RECOMENDACION_41#");
            //    RemoverParrafo(fileName, "#RECOMENDACION_51#");
            //    RemoverParrafo(fileName, "#RECOMENDACION_61#");
            //    RemoverParrafo(fileName, "#RECOMENDACION_71#");
            //    RemoverParrafo(fileName, "#RECOMENDACION_81#");
            //    RemoverParrafo(fileName, "#RECOMENDACION_91#");
            //    RemoverParrafo(fileName, "#RECOMENDACION_101#");
            //    RemoverParrafo(fileName, "#RECOMENDACION_111#");
            //    RemoverParrafo(fileName, "#RECOMENDACION_121#");
            //    RemoverParrafo(fileName, "#RECOMENDACION_131#");
            //    RemoverParrafo(fileName, "#RECOMENDACION_141#");
            //    RemoverParrafo(fileName, "#RECOMENDACION_151#");
            //    RemoverParrafo(fileName, "#RECOMENDACION_161#");
            //    RemoverParrafo(fileName, "#RECOMENDACION_171#");
            //    RemoverParrafo(fileName, "#RECOMENDACION_181#");
            //}
            //catch (Exception ex)
            //{
            //    throw ex;
            //}
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

        private void MostrarDescripcionesTalentosMenosDes(Table table, string fileName, List<RecomendacionBE> lstDescripciones, bool administrador, WordprocessingDocument doc)
        {

            TableRow rt1 = table.Elements<TableRow>().ElementAt(0);
            // Table table1_0 = rt1.Elements<TableCell>().ElementAt(0).Elements<Table>().First();
            Table table1 = rt1.Elements<TableCell>().ElementAt(1).Elements<Table>().First();

            TableRow rt2 = table.Elements<TableRow>().ElementAt(1);
            //  Table table2_0 = rt2.Elements<TableCell>().ElementAt(0).Elements<Table>().First();
            Table table2 = rt2.Elements<TableCell>().ElementAt(1).Elements<Table>().First();

            TableRow rt3 = table.Elements<TableRow>().ElementAt(2);
            //  Table table3_0 = rt3.Elements<TableCell>().ElementAt(0).Elements<Table>().First();
            Table table3 = rt3.Elements<TableCell>().ElementAt(1).Elements<Table>().First();

            TableRow rt4 = table.Elements<TableRow>().ElementAt(3);
            //  Table table4_0 = rt4.Elements<TableCell>().ElementAt(0).Elements<Table>().First();
            Table table4 = rt4.Elements<TableCell>().ElementAt(1).Elements<Table>().First();

            TableRow rt5 = table.Elements<TableRow>().ElementAt(4);
            //  Table table5_0 = rt5.Elements<TableCell>().ElementAt(0).Elements<Table>().First();
            Table table5 = rt5.Elements<TableCell>().ElementAt(1).Elements<Table>().First();

            TableRow rt6 = table.Elements<TableRow>().ElementAt(5);
            //  Table table6_0 = rt6.Elements<TableCell>().ElementAt(0).Elements<Table>().First();
            Table table6 = rt6.Elements<TableCell>().ElementAt(1).Elements<Table>().First();


            int idTalentoAnterior = 0;
            int cont = 1;
            int contTal = 1;
            int fila = 0;
            int tope = 13;

            idTalentoAnterior = lstDescripciones[0].IdTalento;
            contTal = 1;
            string pathImages = Server.MapPath("~/Images/Talentos/");
            string imagen = pathImages + lstDescripciones[0].Talento + ".png";
            imagen = replaceTildes(imagen);

            TableRow row1 = table.Elements<TableRow>().ElementAt(0);
            TableCell cell1 = row1.Elements<TableCell>().ElementAt(0);
            AddImage(cell1, doc.MainDocumentPart, imagen, "imId13");



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
            AddImage(cell2, doc.MainDocumentPart, imagen, "imId14");

            /*
            Paragraph p2 = cell2.Elements<Paragraph>().First();
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
            AddImage(cell3, doc.MainDocumentPart, imagen, "imId15");

            /*
            Paragraph p3 = cell3.Elements<Paragraph>().First();
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
            AddImage(cell4, doc.MainDocumentPart, imagen, "imId16");

            /*
            Paragraph p4 = cell4.Elements<Paragraph>().First();
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
            AddImage(cell5, doc.MainDocumentPart, imagen, "imId17");

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
            AddImage(cell6, doc.MainDocumentPart, imagen, "imId18");

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
        }

        private void MostrarDescripcionesTalentos(Table table, string fileName, List<RecomendacionBE> lstDescripciones, bool administrador, WordprocessingDocument doc)
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
            AddImage(cell1, doc.MainDocumentPart, imagen, "imId1");

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
            AddImage(cell2, doc.MainDocumentPart, imagen, "imId2");

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
            AddImage(cell3, doc.MainDocumentPart, imagen, "imId3");

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
            AddImage(cell4, doc.MainDocumentPart, imagen, "imId4");

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
            AddImage(cell5, doc.MainDocumentPart, imagen, "imId5");
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
            AddImage(cell6, doc.MainDocumentPart, imagen, "imId6");
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
            AddImage(cell7, doc.MainDocumentPart, imagen, "imId7");

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
            AddImage(cell8, doc.MainDocumentPart, imagen, "imId8");

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
            AddImage(cell9, doc.MainDocumentPart, imagen, "imId9");

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
            AddImage(cell10, doc.MainDocumentPart, imagen, "imId10");

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
            AddImage(cell11, doc.MainDocumentPart, imagen, "imId11");

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
            AddImage(cell12, doc.MainDocumentPart, imagen, "imId12");

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



            /*
            string etiquetaMasDesarrollado = "#DESCRIPCION_MAS_DESARROLLADOS_{0}#";
            string etiquetaMenosDesarrollado = "#DESCRIPCION_MENOS_DESARROLLADOS_{0}#";
            string descripcionMas = "";
            string descripcionMenos = "";
            string etiquetaMasDesarrolladoAux = "";
            string etiquetaMenosDesarrolladoAux = "";
            int contMas = 1;
            int contMenos = 0;
            int tope = 60;

            for (int i = 1; i < lstDescripciones.Count; i++)
            {
                TableRow row = table.Elements<TableRow>().ElementAt(i - 1);
                TableCell cell = row.Elements<TableCell>().ElementAt(0);
                Paragraph p = cell.Elements<Paragraph>().First();
                Run r = p.Elements<Run>().First();
                Text t = r.Elements<Text>().First();
                t.Text = lstDescripciones[i];
                contMas++;
            }

            for (int i = contMas; i <= tope; i++)
            {
                TableRow row = table.Elements<TableRow>().Last();//.ElementAt(i - 1);
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
            */

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

        private void FormatearTablaTalentosGeneral(Table table, List<TalentoBE> lstTalentos)
        {
            int countPersonas = 2;
            int countEmprendimiento = 2;
            int countInnovacion = 2;
            int countEstructuras = 2;
            int countPersuacion = 2;
            int countCognicion = 2;
            int countEspecifico = 2;

            for (int i = 0; i < lstTalentos.Count; i++)
            {
                switch (lstTalentos[i].IdTendencia)
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
                        t1.Text = lstTalentos[i].Nombre;

                        countPersonas += 2;
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
                        t2.Text = lstTalentos[i].Nombre;

                        countEmprendimiento += 2;
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
                        t3.Text = lstTalentos[i].Nombre;

                        countInnovacion += 2;
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
                        t4.Text = lstTalentos[i].Nombre;

                        countEstructuras += 2;
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
                        t5.Text = lstTalentos[i].Nombre;

                        countPersuacion += 2;
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
                        t6.Text = lstTalentos[i].Nombre;

                        countCognicion += 2;
                        break;
                    case 7:
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
                        t7.Text = lstTalentos[i].Nombre;

                        countEspecifico += 2;
                        break;
                }
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

        private void FormatearTablaTalentosMenosDesarrollados(Table table, List<TalentoComplexBE> lstTalentos, List<TalentoComplexBE> lstTalentosTE)
        {
            int countPersonas = 2;
            int countEmprendimiento = 2;
            int countInnovacion = 2;
            int countEstructuras = 2;
            int countPersuacion = 2;
            int countCognicion = 2;
            int countEspecifico = 2;

            try
            {
                for (int i = 0; i < lstTalentos.Count; i++)
                {
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
                            t1.Text = lstTalentos[i].nombre;

                            countPersonas += 2;
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
                            t2.Text = lstTalentos[i].nombre;

                            countEmprendimiento += 2;
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
                            t3.Text = lstTalentos[i].nombre;

                            countInnovacion += 2;
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
                            t4.Text = lstTalentos[i].nombre;

                            countEstructuras += 2;
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
                            t5.Text = lstTalentos[i].nombre;

                            countPersuacion += 2;
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
                            t6.Text = lstTalentos[i].nombre;

                            countCognicion += 2;
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

                    countEspecifico += 2;
                }

                // Blanquear celdas vacias
                for (int i = countPersonas; i <= 18; i += 2)
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
                    cell.TableCellProperties.TableCellBorders.BottomBorder.Color = "F9F9F9";
                    cell.TableCellProperties.TableCellBorders.BottomBorder.Val = new EnumValue<BorderValues>(BorderValues.Thick);
                    cell.TableCellProperties.TableCellBorders.LeftBorder.Color = "F9F9F9";
                    cell.TableCellProperties.TableCellBorders.LeftBorder.Val = new EnumValue<BorderValues>(BorderValues.Thick);
                    cell.TableCellProperties.TableCellBorders.RightBorder.Color = "F9F9F9";
                    cell.TableCellProperties.TableCellBorders.RightBorder.Val = new EnumValue<BorderValues>(BorderValues.Thick);
                }

                for (int i = countEmprendimiento; i <= 18; i += 2)
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
                    cell.TableCellProperties.TableCellBorders.BottomBorder.Color = "F9F9F9";
                    cell.TableCellProperties.TableCellBorders.BottomBorder.Val = new EnumValue<BorderValues>(BorderValues.Thick);
                    cell.TableCellProperties.TableCellBorders.LeftBorder.Color = "F9F9F9";
                    cell.TableCellProperties.TableCellBorders.LeftBorder.Val = new EnumValue<BorderValues>(BorderValues.Thick);
                    cell.TableCellProperties.TableCellBorders.RightBorder.Color = "F9F9F9";
                    cell.TableCellProperties.TableCellBorders.RightBorder.Val = new EnumValue<BorderValues>(BorderValues.Thick);
                }

                for (int i = countInnovacion; i <= 18; i += 2)
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
                    cell.TableCellProperties.TableCellBorders.BottomBorder.Color = "F9F9F9";
                    cell.TableCellProperties.TableCellBorders.BottomBorder.Val = new EnumValue<BorderValues>(BorderValues.Thick);
                    cell.TableCellProperties.TableCellBorders.LeftBorder.Color = "F9F9F9";
                    cell.TableCellProperties.TableCellBorders.LeftBorder.Val = new EnumValue<BorderValues>(BorderValues.Thick);
                    cell.TableCellProperties.TableCellBorders.RightBorder.Color = "F9F9F9";
                    cell.TableCellProperties.TableCellBorders.RightBorder.Val = new EnumValue<BorderValues>(BorderValues.Thick);
                }

                for (int i = countEstructuras; i <= 18; i += 2)
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
                    cell.TableCellProperties.TableCellBorders.BottomBorder.Color = "F9F9F9";
                    cell.TableCellProperties.TableCellBorders.BottomBorder.Val = new EnumValue<BorderValues>(BorderValues.Thick);
                    cell.TableCellProperties.TableCellBorders.LeftBorder.Color = "F9F9F9";
                    cell.TableCellProperties.TableCellBorders.LeftBorder.Val = new EnumValue<BorderValues>(BorderValues.Thick);
                    cell.TableCellProperties.TableCellBorders.RightBorder.Color = "F9F9F9";
                    cell.TableCellProperties.TableCellBorders.RightBorder.Val = new EnumValue<BorderValues>(BorderValues.Thick);
                }

                for (int i = countPersuacion; i <= 18; i += 2)
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
                    cell.TableCellProperties.TableCellBorders.BottomBorder.Color = "F9F9F9";
                    cell.TableCellProperties.TableCellBorders.BottomBorder.Val = new EnumValue<BorderValues>(BorderValues.Thick);
                    cell.TableCellProperties.TableCellBorders.LeftBorder.Color = "F9F9F9";
                    cell.TableCellProperties.TableCellBorders.LeftBorder.Val = new EnumValue<BorderValues>(BorderValues.Thick);
                    cell.TableCellProperties.TableCellBorders.RightBorder.Color = "F9F9F9";
                    cell.TableCellProperties.TableCellBorders.RightBorder.Val = new EnumValue<BorderValues>(BorderValues.Thick);
                }

                for (int i = countCognicion; i <= 18; i += 2)
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
                    cell.TableCellProperties.TableCellBorders.BottomBorder.Color = "F9F9F9";
                    cell.TableCellProperties.TableCellBorders.BottomBorder.Val = new EnumValue<BorderValues>(BorderValues.Thick);
                    cell.TableCellProperties.TableCellBorders.LeftBorder.Color = "F9F9F9";
                    cell.TableCellProperties.TableCellBorders.LeftBorder.Val = new EnumValue<BorderValues>(BorderValues.Thick);
                    cell.TableCellProperties.TableCellBorders.RightBorder.Color = "F9F9F9";
                    cell.TableCellProperties.TableCellBorders.RightBorder.Val = new EnumValue<BorderValues>(BorderValues.Thick);
                }

                for (int i = countEspecifico; i <= 18; i += 2)
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
                    cell.TableCellProperties.TableCellBorders.BottomBorder.Color = "F9F9F9";
                    cell.TableCellProperties.TableCellBorders.BottomBorder.Val = new EnumValue<BorderValues>(BorderValues.Thick);
                    cell.TableCellProperties.TableCellBorders.LeftBorder.Color = "F9F9F9";
                    cell.TableCellProperties.TableCellBorders.LeftBorder.Val = new EnumValue<BorderValues>(BorderValues.Thick);
                    cell.TableCellProperties.TableCellBorders.RightBorder.Color = "F9F9F9";
                    cell.TableCellProperties.TableCellBorders.RightBorder.Val = new EnumValue<BorderValues>(BorderValues.Thick);

                    countEspecifico++;
                }
            }
            catch (Exception ex)
            {
                LogDALC objLogDALC = new LogDALC();
                string mensaje = "Metodo: FormatearTablaTalentosMenosDesarrollados (WS Reporte). Mensaje: " + ex.Message;
                objLogDALC.InsertarLog(mensaje);

                throw ex;
            }
        }

        private void GenerarChartPie2(int cantidadOrientadoPersonas, int cantidadOrientadoPensamiento, int cantidadOrientadoInnovacion,
            int cantidadOrientadoEstructura, int cantidadOrientadoEjecucion, int cantidadOrientadoLiderazgo,
            string rutaExcelChart, string rutaImagenChart)
        {
            string rutaPlantilla = Server.MapPath("~/Reporte/Plantilla/Plantilla Chart.xlsx");
            FileInfo plantilla = new FileInfo(rutaPlantilla);
            FileInfo newFile = new FileInfo(rutaExcelChart);
            ExcelPackage package = new ExcelPackage(plantilla);//, templateFile);
            //package.Workbook.Worksheets.Add("Chart");
            ExcelWorksheet worksheet = package.Workbook.Worksheets[1];

            worksheet.Cells["A1"].Value = cantidadOrientadoPersonas;
            worksheet.Cells["A2"].Value = cantidadOrientadoPensamiento;
            worksheet.Cells["A3"].Value = cantidadOrientadoInnovacion;
            worksheet.Cells["A4"].Value = cantidadOrientadoEstructura;
            worksheet.Cells["A5"].Value = cantidadOrientadoEjecucion;
            worksheet.Cells["A6"].Value = cantidadOrientadoLiderazgo;

            var chart = (worksheet.Drawings.AddChart("PieChart", OfficeOpenXml.Drawing.Chart.eChartType.Pie) as ExcelPieChart);
            chart.Title.Text = "Total";
            chart.SetPosition(0, 0, 5, 5);
            chart.SetSize(600, 300);
            ExcelAddress valueAddress = new ExcelAddress(2, 5, 6, 5);
            var ser = (chart.Series.Add("A1:A6", "B1:B6") as ExcelPieChartSerie);

            // To show the Product name within the Pie Chart along with value
            chart.DataLabel.ShowCategory = false;
            // To show the value in form of percentage
            chart.DataLabel.ShowPercent = false;
            chart.DataLabel.ShowLegendKey = false;

            package.SaveAs(newFile);

            //ConvertChartToImage(rutaExcelChart, rutaImagenChart);
        }

        private static void releaseObject(object obj)
        {
            try
            {
                System.Runtime.InteropServices.Marshal.ReleaseComObject(obj);
                obj = null;
            }
            catch (Exception ex)
            {
                throw;
            }
            finally
            {
                GC.Collect();
            }
        }

        [WebMethod]
        public bool ResultadosParticipantesImportar(string FechaInicio, string FechaFin, string Empresa)
        {
            FileInfo excel = new FileInfo(Server.MapPath(@"~/Reporte/Plantilla/Datos Usuarios Resultado.xlsx"));
            ExcelPackage package = new ExcelPackage(excel);
            ExcelWorksheet sheet = package.Workbook.Worksheets["Resultado"];
            int row = 2;
            int rowAnterior = 2;
            string dniAnterior;
            string dniActual = "";

            string seleccionDNI;
            string seleccionNombre;
            string seleccionAPaterno;
            string seleccionAMaterno;
            string seleccionSexo;
            string seleccionFechaNacimiento;
            string seleccionNivelInstruccion;
            string seleccionCargoEmpresa;
            string seleccionCorreo;

            ResultadoBC objResultadoBC = new ResultadoBC();
            List<ResultadoParaleloBE> lstResultado = objResultadoBC.ResultadosParticipantesImportar(FechaInicio, FechaFin, Empresa);

            dniAnterior = lstResultado[0].DNI;
            foreach (var item in lstResultado)
            {
                dniActual = item.DNI;
                sheet.Cells[row, 1].Value = item.DNI;
                sheet.Cells[row, 2].Value = item.Nombres;
                sheet.Cells[row, 3].Value = item.ApellidoPaterno;
                sheet.Cells[row, 4].Value = item.ApellidoMaterno;
                sheet.Cells[row, 5].Value = item.Sexo;
                sheet.Cells[row, 6].Value = item.FechaNacimiento;
                sheet.Cells[row, 7].Value = item.NivelInstruccion;
                sheet.Cells[row, 8].Value = item.CargoEmpresa;
                sheet.Cells[row, 9].Value = item.CorreoElectronico;
                sheet.Cells[row, 10].Value = item.MasDesarrollados;
                sheet.Cells[row, 11].Value = item.MenosDesarrollados;
                sheet.Cells[row, 12].Value = item.TalentosEspecificos;
                sheet.Cells[row, 13].Value = item.Virtudes;

                if (!dniAnterior.Equals(dniActual))
                {
                    dniAnterior = dniActual;
                    seleccionDNI = "A" + rowAnterior.ToString() + ":A" + row.ToString();
                    seleccionNombre = "B" + rowAnterior.ToString() + ":B" + row.ToString();
                    seleccionAPaterno = "C" + rowAnterior.ToString() + ":C" + row.ToString();
                    seleccionAMaterno = "D" + rowAnterior.ToString() + ":D" + row.ToString();
                    seleccionSexo = "E" + rowAnterior.ToString() + ":E" + row.ToString();
                    seleccionFechaNacimiento = "F" + rowAnterior.ToString() + ":F" + row.ToString();
                    seleccionNivelInstruccion = "G" + rowAnterior.ToString() + ":G" + row.ToString();
                    seleccionCargoEmpresa = "H" + rowAnterior.ToString() + ":H" + row.ToString();
                    seleccionCorreo = "I" + rowAnterior.ToString() + ":I" + row.ToString();

                    sheet.Cells[seleccionDNI].Merge = true;
                    sheet.Cells[seleccionNombre].Merge = true;
                    sheet.Cells[seleccionAPaterno].Merge = true;
                    sheet.Cells[seleccionAMaterno].Merge = true;
                    sheet.Cells[seleccionSexo].Merge = true;
                    sheet.Cells[seleccionFechaNacimiento].Merge = true;
                    sheet.Cells[seleccionNivelInstruccion].Merge = true;
                    sheet.Cells[seleccionCargoEmpresa].Merge = true;
                    sheet.Cells[seleccionCorreo].Merge = true;

                    sheet.Cells[seleccionDNI].Style.VerticalAlignment = OfficeOpenXml.Style.ExcelVerticalAlignment.Center;
                    sheet.Cells[seleccionNombre].Style.VerticalAlignment = OfficeOpenXml.Style.ExcelVerticalAlignment.Center;
                    sheet.Cells[seleccionAPaterno].Style.VerticalAlignment = OfficeOpenXml.Style.ExcelVerticalAlignment.Center;
                    sheet.Cells[seleccionAMaterno].Style.VerticalAlignment = OfficeOpenXml.Style.ExcelVerticalAlignment.Center;
                    sheet.Cells[seleccionSexo].Style.VerticalAlignment = OfficeOpenXml.Style.ExcelVerticalAlignment.Center;
                    sheet.Cells[seleccionFechaNacimiento].Style.VerticalAlignment = OfficeOpenXml.Style.ExcelVerticalAlignment.Center;
                    sheet.Cells[seleccionNivelInstruccion].Style.VerticalAlignment = OfficeOpenXml.Style.ExcelVerticalAlignment.Center;
                    sheet.Cells[seleccionCargoEmpresa].Style.VerticalAlignment = OfficeOpenXml.Style.ExcelVerticalAlignment.Center;
                    sheet.Cells[seleccionCorreo].Style.VerticalAlignment = OfficeOpenXml.Style.ExcelVerticalAlignment.Center;

                    rowAnterior = row + 1;
                }

                row++;
            }

            row--;
            seleccionDNI = "A" + rowAnterior.ToString() + ":A" + row.ToString();
            seleccionNombre = "B" + rowAnterior.ToString() + ":B" + row.ToString();
            seleccionAPaterno = "C" + rowAnterior.ToString() + ":C" + row.ToString();
            seleccionAMaterno = "D" + rowAnterior.ToString() + ":D" + row.ToString();
            seleccionSexo = "E" + rowAnterior.ToString() + ":E" + row.ToString();
            seleccionFechaNacimiento = "F" + rowAnterior.ToString() + ":F" + row.ToString();
            seleccionNivelInstruccion = "G" + rowAnterior.ToString() + ":G" + row.ToString();
            seleccionCargoEmpresa = "H" + rowAnterior.ToString() + ":H" + row.ToString();
            seleccionCorreo = "I" + rowAnterior.ToString() + ":I" + row.ToString();

            sheet.Cells[seleccionDNI].Merge = true;
            sheet.Cells[seleccionNombre].Merge = true;
            sheet.Cells[seleccionAPaterno].Merge = true;
            sheet.Cells[seleccionAMaterno].Merge = true;
            sheet.Cells[seleccionSexo].Merge = true;
            sheet.Cells[seleccionFechaNacimiento].Merge = true;
            sheet.Cells[seleccionNivelInstruccion].Merge = true;
            sheet.Cells[seleccionCargoEmpresa].Merge = true;
            sheet.Cells[seleccionCorreo].Merge = true;

            sheet.Cells[seleccionDNI].Style.VerticalAlignment = OfficeOpenXml.Style.ExcelVerticalAlignment.Center;
            sheet.Cells[seleccionNombre].Style.VerticalAlignment = OfficeOpenXml.Style.ExcelVerticalAlignment.Center;
            sheet.Cells[seleccionAPaterno].Style.VerticalAlignment = OfficeOpenXml.Style.ExcelVerticalAlignment.Center;
            sheet.Cells[seleccionAMaterno].Style.VerticalAlignment = OfficeOpenXml.Style.ExcelVerticalAlignment.Center;
            sheet.Cells[seleccionSexo].Style.VerticalAlignment = OfficeOpenXml.Style.ExcelVerticalAlignment.Center;
            sheet.Cells[seleccionFechaNacimiento].Style.VerticalAlignment = OfficeOpenXml.Style.ExcelVerticalAlignment.Center;
            sheet.Cells[seleccionNivelInstruccion].Style.VerticalAlignment = OfficeOpenXml.Style.ExcelVerticalAlignment.Center;
            sheet.Cells[seleccionCargoEmpresa].Style.VerticalAlignment = OfficeOpenXml.Style.ExcelVerticalAlignment.Center;
            sheet.Cells[seleccionCorreo].Style.VerticalAlignment = OfficeOpenXml.Style.ExcelVerticalAlignment.Center;

            FileInfo excelFinal = new FileInfo(Server.MapPath(@"~/Reporte/Datos Usuarios Resultado.xlsx"));
            package.SaveAs(excelFinal);

            return true;
        }
    }
}
