using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;
using System.Configuration;
using System.Data.OleDb;
using System.Data;

public partial class CargarResultados : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }

    protected void btnCargar_Click(object sender, EventArgs e)
    {
        uploadExcel();

        string institucion = txtInstitucion.Text.Trim();

        LeerExcel(institucion);

        deleteExcel();

        txtInstitucion.Text = "";
    }

    private void LeerExcel(string institucion)
    {
        string conStr = "";
        string filename = ViewState["nombreExcel"].ToString();
        string path = Server.MapPath("~/Temp") + "/" + filename;
        string tieneCabecera = "YES";

        conStr = ConfigurationManager.ConnectionStrings["ConnectionExcel"]
                          .ConnectionString;

        conStr = String.Format(conStr, path, tieneCabecera);
        OleDbConnection connExcel = new OleDbConnection(conStr);
        OleDbCommand cmdExcel = new OleDbCommand();
        OleDbDataAdapter oda = new OleDbDataAdapter();
        DataTable dt = new DataTable();
        cmdExcel.Connection = connExcel;

        //Get the name of First Sheet
        connExcel.Open();
        DataTable dtExcelSchema;
        dtExcelSchema = connExcel.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, null);
        string SheetName = dtExcelSchema.Rows[0]["TABLE_NAME"].ToString();
        connExcel.Close();

        //Read Data from First Sheet
        connExcel.Open();
        cmdExcel.CommandText = "SELECT * From [" + SheetName + "]";
        oda.SelectCommand = cmdExcel;
        oda.Fill(dt);
        connExcel.Close();

        List<DataRow> list = new List<DataRow>(dt.Select());
        List<TalentosReference.ResultadoParaleloBE> lstParticipantes;

        lstParticipantes = (from DataRow row in dt.Rows
                            //where row["DNI"].ToString() != ""
                            select new TalentosReference.ResultadoParaleloBE
                            {
                                Nombres = row["NOMBRES"].ToString().Trim(),
                                ApellidoPaterno = row["APELLIDO PATERNO"].ToString().Trim(),
                                ApellidoMaterno = row["APELLIDO MATERNO"].ToString().Trim(),
                                DNI = row["DNI"].ToString().Trim(),
                                CargoEmpresa = row["CARGO EN LA EMPRESA"].ToString().Trim(),
                                FechaNacimiento = row["FECHA NACIMIENTO"].ToString().Trim(),
                                Empresa = institucion.Trim(),
                                NivelInstruccion = row["NIVEL DE INSTRUCCIÓN"].ToString().Trim(),
                                CorreoElectronico = row["CORREO ELECTRÓNICO"].ToString().Trim(),
                                Sexo = row["SEXO"].ToString().Trim(),
                                CodigoEvaluacion = row["Código Evaluación"].ToString().Trim(),
                                MasDesarrollados = row["Talentos más desarrollados"].ToString().Trim(),
                                MenosDesarrollados = row["Talentos menos desarrollados"].ToString().Trim(),
                                TalentosEspecificosMas = row["Talentos Especificos Más Desarrollados"].ToString().Trim(),
                                TalentosEspecificosMenos = row["Talentos Especificos Menos Desarrollados"].ToString().Trim(),
                                Virtudes = row["Virtudes"].ToString().Trim()
                            }).ToList();

        ReportesReference.ResultadoFinalBE objResultadoBE = null;
        List<ReportesReference.ResultadoFinalBE> lstResultados = new List<ReportesReference.ResultadoFinalBE>();
        List<ReportesReference.TalentoComplexBE> lstTalentosIntermedioDesarrollados = null;
        List<ReportesReference.TalentoComplexBE> lstTalentosMasDesarrollados = null;
        List<ReportesReference.TalentoComplexBE> lstTalentosMenosDesarrollados = null;
        List<ReportesReference.TalentoComplexBE> lstTEMasDesarrollados = null;
        List<ReportesReference.TalentoComplexBE> lstTEMenosDesarrollados = null;
        List<ReportesReference.TalentoComplexBE> lstVirtudes = null;
        string anteriorDNI = lstParticipantes[0].DNI;

        foreach (var item in lstParticipantes)
        {
            if (!(string.IsNullOrEmpty(item.MasDesarrollados.Trim()) && string.IsNullOrEmpty(item.MenosDesarrollados.Trim()) &&
                string.IsNullOrEmpty(item.TalentosEspecificosMas.Trim()) && string.IsNullOrEmpty(item.TalentosEspecificosMenos.Trim()) &&
                string.IsNullOrEmpty(item.Virtudes.Trim())))
            {
                if (!string.IsNullOrEmpty(item.DNI) && anteriorDNI != item.DNI)
                {
                    objResultadoBE.lstTalentosMasDesarrollados = lstTalentosMasDesarrollados.ToArray();
                    objResultadoBE.lstTalentosMenosDesarrollados = lstTalentosMenosDesarrollados.ToArray();
                    objResultadoBE.lstTEMasDesarrollados = lstTEMasDesarrollados.ToArray();
                    objResultadoBE.lstTEMenosDesarrollados = lstTEMenosDesarrollados.ToArray();
                    objResultadoBE.lstVirtudes = lstVirtudes.ToArray();
                    lstResultados.Add(objResultadoBE);
                    anteriorDNI = item.DNI;
                }

                if (!string.IsNullOrEmpty(item.DNI))
                {
                    objResultadoBE = new ReportesReference.ResultadoFinalBE();
                    lstTalentosIntermedioDesarrollados = new List<ReportesReference.TalentoComplexBE>();
                    lstTalentosMasDesarrollados = new List<ReportesReference.TalentoComplexBE>();
                    lstTalentosMenosDesarrollados = new List<ReportesReference.TalentoComplexBE>();
                    lstTEMasDesarrollados = new List<ReportesReference.TalentoComplexBE>();
                    lstTEMenosDesarrollados = new List<ReportesReference.TalentoComplexBE>();
                    lstVirtudes = new List<ReportesReference.TalentoComplexBE>();

                    objResultadoBE.DNI = item.DNI;
                    objResultadoBE.NombreParticipante = item.Nombres;
                    objResultadoBE.ApellidoPaterno = item.ApellidoPaterno;
                    objResultadoBE.ApellidoMaterno = item.ApellidoMaterno;
                    objResultadoBE.Sexo = item.Sexo;
                    objResultadoBE.CorreoElectronico = item.CorreoElectronico;
                    objResultadoBE.CargoEmpresa = item.CargoEmpresa;
                    objResultadoBE.NivelInstruccion = item.NivelInstruccion;
                    objResultadoBE.Institucion = item.Empresa;
                    objResultadoBE.FechaNacimiento = item.FechaNacimiento;
                    objResultadoBE.CodigoEvaluacion = item.CodigoEvaluacion;
                    
                    if (!string.IsNullOrEmpty(item.MasDesarrollados))
                    {
                        ReportesReference.TalentoComplexBE objTalentoTalentoMasBE = new ReportesReference.TalentoComplexBE();
                        objTalentoTalentoMasBE.nombre = item.MasDesarrollados;
                        objTalentoTalentoMasBE.Seleccionado = true;
                        objTalentoTalentoMasBE.Buzon_Id = 1;
                        lstTalentosMasDesarrollados.Add(objTalentoTalentoMasBE);
                    }

                    if (!string.IsNullOrEmpty(item.MenosDesarrollados))
                    {
                        ReportesReference.TalentoComplexBE objTalentoTalentoMenosBE = new ReportesReference.TalentoComplexBE();
                        objTalentoTalentoMenosBE.nombre = item.MenosDesarrollados;
                        objTalentoTalentoMenosBE.Seleccionado = true;
                        objTalentoTalentoMenosBE.Buzon_Id = 3;
                        lstTalentosMenosDesarrollados.Add(objTalentoTalentoMenosBE);
                    }

                    if (!string.IsNullOrEmpty(item.TalentosEspecificosMas))
                    {
                        ReportesReference.TalentoComplexBE objTalentoEspecificoMasBE = new ReportesReference.TalentoComplexBE();
                        objTalentoEspecificoMasBE.nombre = item.TalentosEspecificosMas;
                        objTalentoEspecificoMasBE.Seleccionado = true;
                        objTalentoEspecificoMasBE.Buzon_Id = 4;
                        lstTEMasDesarrollados.Add(objTalentoEspecificoMasBE);
                    }

                    if (!string.IsNullOrEmpty(item.TalentosEspecificosMenos))
                    {
                        ReportesReference.TalentoComplexBE objTalentoEspecificoMenosBE = new ReportesReference.TalentoComplexBE();
                        objTalentoEspecificoMenosBE.nombre = item.TalentosEspecificosMenos;
                        objTalentoEspecificoMenosBE.Seleccionado = true;
                        objTalentoEspecificoMenosBE.Buzon_Id = 6;
                        lstTEMenosDesarrollados.Add(objTalentoEspecificoMenosBE);
                    }

                    if (!string.IsNullOrEmpty(item.Virtudes))
                    {
                        ReportesReference.TalentoComplexBE objVirtudBE = new ReportesReference.TalentoComplexBE();
                        objVirtudBE.nombre = item.Virtudes;
                        objVirtudBE.Seleccionado = true;
                        objVirtudBE.Buzon_Id = 7;
                        lstVirtudes.Add(objVirtudBE);
                    }
                }
                else
                {
                    if (!string.IsNullOrEmpty(item.MasDesarrollados))
                    {
                        ReportesReference.TalentoComplexBE objTalentoTalentoMasBE = new ReportesReference.TalentoComplexBE();
                        objTalentoTalentoMasBE.nombre = item.MasDesarrollados;
                        objTalentoTalentoMasBE.Seleccionado = true;
                        objTalentoTalentoMasBE.Buzon_Id = 1;
                        lstTalentosMasDesarrollados.Add(objTalentoTalentoMasBE);
                    }

                    if (!string.IsNullOrEmpty(item.MenosDesarrollados))
                    {
                        ReportesReference.TalentoComplexBE objTalentoTalentoMenosBE = new ReportesReference.TalentoComplexBE();
                        objTalentoTalentoMenosBE.nombre = item.MenosDesarrollados;
                        objTalentoTalentoMenosBE.Seleccionado = true;
                        objTalentoTalentoMenosBE.Buzon_Id = 3;
                        lstTalentosMenosDesarrollados.Add(objTalentoTalentoMenosBE);
                    }

                    if (!string.IsNullOrEmpty(item.TalentosEspecificosMas))
                    {
                        ReportesReference.TalentoComplexBE objTalentoEspecificoMasBE = new ReportesReference.TalentoComplexBE();
                        objTalentoEspecificoMasBE.nombre = item.TalentosEspecificosMas;
                        objTalentoEspecificoMasBE.Seleccionado = true;
                        objTalentoEspecificoMasBE.Buzon_Id = 4;
                        lstTEMasDesarrollados.Add(objTalentoEspecificoMasBE);
                    }

                    if (!string.IsNullOrEmpty(item.TalentosEspecificosMenos))
                    {
                        ReportesReference.TalentoComplexBE objTalentoEspecificoMenosBE = new ReportesReference.TalentoComplexBE();
                        objTalentoEspecificoMenosBE.nombre = item.TalentosEspecificosMenos;
                        objTalentoEspecificoMenosBE.Seleccionado = true;
                        objTalentoEspecificoMenosBE.Buzon_Id = 6;
                        lstTEMenosDesarrollados.Add(objTalentoEspecificoMenosBE);
                    }

                    if (!string.IsNullOrEmpty(item.Virtudes))
                    {
                        ReportesReference.TalentoComplexBE objVirtudBE = new ReportesReference.TalentoComplexBE();
                        objVirtudBE.nombre = item.Virtudes;
                        objVirtudBE.Seleccionado = true;
                        objVirtudBE.Buzon_Id = 7;
                        lstVirtudes.Add(objVirtudBE);
                    }
                }
            }
        }

        objResultadoBE.lstTalentosMasDesarrollados = lstTalentosMasDesarrollados.ToArray();
        objResultadoBE.lstTalentosMenosDesarrollados = lstTalentosMenosDesarrollados.ToArray();
        objResultadoBE.lstTEMasDesarrollados = lstTEMasDesarrollados.ToArray();
        objResultadoBE.lstTEMenosDesarrollados = lstTEMenosDesarrollados.ToArray();
        objResultadoBE.lstVirtudes = lstVirtudes.ToArray();
        
        lstResultados.Add(objResultadoBE);

        ReportesReference.wsReporte reporte = new ReportesReference.wsReporte();

        try
        {
            reporte.CargarResultadosMasivos(lstResultados.ToArray());

            lblStatusOk.Visible = true;
            lblStatusOk.Text = "Se cargaron los resultados de los participantes en el sistema.";
        }
        catch (Exception ex)
        {
            lblStatus.Visible = true;
            lblStatus.Text = "El archivo no pudo ser cargado. favor de comunicarse con el administrador del sistema.\r\n" +
                "Un correo fue enviado a cada participante para que visualicen sus resultados.";
            throw ex;
        }
    }

    private void uploadExcel()
    {
        lblStatus.Visible = false;
        lblStatusOk.Visible = false;

        if (fuExcel.HasFile)
        {
            try
            {
                if (fuExcel.PostedFile.ContentType == "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet"
                    || fuExcel.PostedFile.ContentType == "application/octet-stream")
                {
                    if (fuExcel.PostedFile.ContentLength < 2097152)
                    {
                        string filename = Path.GetFileName(fuExcel.FileName);
                        string path = Server.MapPath("~/Temp") + "/" + filename;
                        fuExcel.SaveAs(path);
                        ViewState["nombreExcel"] = filename;
                        //StatusLabel.Text = "Upload status: File uploaded!";
                    }
                    else
                    {
                        lblStatus.Visible = true;
                        lblStatus.Text = "El archivo pesa mas de 2 MB";
                    }
                }
                //else
                //    StatusLabel.Text = "Upload status: Only JPEG files are accepted!";
            }
            catch (Exception ex)
            {
                lblStatus.Visible = true;
                lblStatus.Text = "El archivo no pudo ser cargado. favor de comunicarse con el administrador del sistema.";
                throw ex;
            }
        }
    }

    private void deleteExcel()
    {
        string filename = ViewState["nombreExcel"].ToString();
        string path = Server.MapPath("~/Temp") + "/" + filename;

        if (System.IO.File.Exists(path))
        {
            System.IO.File.Delete(path);
        }
    }
}
