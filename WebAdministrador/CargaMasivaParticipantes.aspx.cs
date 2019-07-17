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

public partial class CargaMasivaParticipantes : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!this.IsPostBack)
        {
            cargarParticipantesMasivos();
        }
    }

    private void uploadExcel()
    {
        if (fuExcel.HasFile)
        {
            try
            {
                if (fuExcel.PostedFile.ContentType == "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet"
                    || fuExcel.PostedFile.ContentType == "application/octet-stream")
                {
                    if (fuExcel.PostedFile.ContentLength < 102400)
                    {
                        string filename = Path.GetFileName(fuExcel.FileName);
                        string path = Server.MapPath("~/Temp") + "/" + filename;
                        fuExcel.SaveAs(path);
                        ViewState["nombreExcel"] = filename;
                        //StatusLabel.Text = "Upload status: File uploaded!";
                    }
                    //else
                    //    StatusLabel.Text = "Upload status: The file has to be less than 100 kb!";
                }
                //else
                //    StatusLabel.Text = "Upload status: Only JPEG files are accepted!";
            }
            catch (Exception ex)
            {
                //StatusLabel.Text = "Upload status: The file could not be uploaded. The following error occured: " + ex.Message;
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

        //switch (Extension)
        //{
        //    case ".xls": //Excel 97-03
        //        conStr = ConfigurationManager.ConnectionStrings["Excel03ConString"]
        //                 .ConnectionString;
        //        break;
        //    case ".xlsx": //Excel 07
        conStr = ConfigurationManager.ConnectionStrings["ConnectionExcel"]
                          .ConnectionString;
        //        break;
        //}
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
        List<TalentosReference.ParticipanteBE> lstParticipantes;

        lstParticipantes = (from DataRow row in dt.Rows
                            where row["DNI"].ToString() != ""
                            select new TalentosReference.ParticipanteBE
                            {
                                Nombres = row["NOMBRES"].ToString().Trim(),
                                ApellidoPaterno = row["APELLIDO PATERNO"].ToString().Trim(),
                                ApellidoMaterno = row["APELLIDO MATERNO"].ToString().Trim(),
                                DNI = row["DNI"].ToString().Trim(),
                                Cargo = row["CARGO EN LA EMPRESA"].ToString().Trim(),
                                FechaNacimiento = row["FECHA NACIMIENTO"].ToString().Trim(),
                                Institucion = institucion.Trim(),
                                NivelInstruccion = row["NIVEL DE INSTRUCCIÓN"].ToString().Trim(),
                                CorreoElectronico = row["CORREO ELECTRÓNICO"].ToString().Trim(),
                                Sexo = row["SEXO"].ToString().Trim(),
                            }).ToList();

        TalentosReference.WSTalentos objService = new TalentosReference.WSTalentos();

        objService.InsertarParticipante(lstParticipantes.ToArray());

        cargarParticipantesMasivos();

        //objService.EnviarEmail(lstParticipantes.ToArray());
        objService.EnviarEmailCompleted += new TalentosReference.EnviarEmailCompletedEventHandler(objService_EnviarEmailCompleted);
        objService.EnviarEmailAsync(lstParticipantes.ToArray());

        //Bind Data to GridView
        //gvParticipantes.Caption = Path.GetFileName(path);
        //gvParticipantes.DataSource = dt;
        //gvParticipantes.DataBind();
    }

    private void objService_EnviarEmailCompleted(object sender, TalentosReference.EnviarEmailCompletedEventArgs e)
    {
        ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(),
              "Informacion",
              "alert('Se enviaron los correos a los participantes cargados masivamente');",
              true);
    }

    private void cargarParticipantesMasivos()
    {
        TalentosReference.WSTalentos objService = new TalentosReference.WSTalentos();
        List<TalentosReference.ParticipanteBE> lstParticipantes = new List<TalentosReference.ParticipanteBE>();

        lstParticipantes = objService.ListarParticipantesMasivos().ToList();

        gvParticipantes.DataSource = lstParticipantes;
        gvParticipantes.DataBind();
    }
}
