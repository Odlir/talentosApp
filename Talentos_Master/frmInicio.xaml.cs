using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using System.Windows.Browser;
using UPC.Talentos.BL.BE;
using System.Net.Browser;

///Esta es la página inicial

namespace Talentos_Master
{
    public partial class frmInicio : IPaginaContenida
    {
        Session sesion = null;

        public frmInicio()
        {
            InitializeComponent();

            BusyWindow.IsBusy = true;

            sesion = Session.getInstance();

            if (HtmlPage.Document.QueryString.Count > 0)
            {
                string token = HtmlPage.Document.QueryString["token"].ToString();
                string id = HtmlPage.Document.QueryString["id"].ToString();

                sesion.Token = token;

                TalentosReference.WSTalentosSoapClient ws = new Talentos_Master.TalentosReference.WSTalentosSoapClient();
                ws.DevolverParticipanteCompleted+=new EventHandler<Talentos_Master.TalentosReference.DevolverParticipanteCompletedEventArgs>(ws_DevolverParticipanteCompleted);
                ws.DevolverParticipanteAsync(id, token);
            }
            else
            {
                BusyWindow.IsBusy = false;
                btnIniciarSession.Visibility = Visibility.Collapsed;
                txtSinSesion.Visibility = Visibility.Visible;
                sesion.EsMasivo = false;
            } 
        }

        private void ws_DevolverParticipanteCompleted(object sender, Talentos_Master.TalentosReference.DevolverParticipanteCompletedEventArgs e)
        {
            if (e.Error == null)
            {
                string json = e.Result;

                UsuarioJson obj = JsonHelper.DeserializeToPerson(json);

                if (json != "" && obj.user != null)
                {
                    Talentos_Master.TalentosReference.ParticipanteBE objParticipanteBE = new Talentos_Master.TalentosReference.ParticipanteBE();
                    objParticipanteBE.Nombres = obj.user.names;
                    objParticipanteBE.ApellidoPaterno = (obj.user.surname == null) ? "" : obj.user.surname;
                    objParticipanteBE.ApellidoMaterno = (obj.user.lastname == null) ? "" : obj.user.lastname;
                    objParticipanteBE.Sexo = (obj.user.sex == null) ? "" : obj.user.sex;
                    objParticipanteBE.CorreoElectronico = (obj.user.email == null) ? "" : obj.user.email;
                    objParticipanteBE.Cargo = (obj.user.charge == null) ? "" : obj.user.charge;
                    objParticipanteBE.NivelInstruccion = (obj.user.charge == null) ? "" : obj.user.charge;
                    objParticipanteBE.Institucion = (obj.user.company == null) ? "" : obj.user.company;
                    objParticipanteBE.DNI = obj.user.numberidentity.ToString();
                    objParticipanteBE.CodigoEvaluacion = obj.user.id.ToString();
                    objParticipanteBE.FechaNacimiento = (obj.user.birthday == null) ? "" : obj.user.birthday.date;

                    sesion.participante.NickName = obj.user.names + " " + obj.user.surname;
                    sesion.CorreoParticipanteMasivo = obj.user.email;
                    sesion.CodEvaluacion = obj.user.id.ToString();
                    sesion.DNI = obj.user.numberidentity;
                    sesion.participante.Nombres = obj.user.names + " " + obj.user.surname;
                    sesion.participante.Sexo = (obj.user.sex.ToUpper().Equals("F")) ? 1 : 2;

                    TalentosReference.WSTalentosSoapClient ws = new Talentos_Master.TalentosReference.WSTalentosSoapClient();

                    ws.InsertarParticipanteUnicoCompleted += new EventHandler<Talentos_Master.TalentosReference.InsertarParticipanteUnicoCompletedEventArgs>(InsertarParticipanteUnicoCompleted);
                    ws.InsertarParticipanteUnicoAsync(objParticipanteBE);
                }
                else
                {
                    BusyWindow.IsBusy = false;
                    btnIniciarSession.Visibility = Visibility.Collapsed;
                    txtSinSesion.Visibility = Visibility.Visible;
                    txtSinSesion.Text = "No se encontraron datos para el participante solicitado. Favor de revisar si los parametros suministrados son los correctos.";
                }
            }
            else
            {
                BusyWindow.IsBusy = false;
                btnIniciarSession.Visibility = Visibility.Collapsed;
                txtSinSesion.Visibility = Visibility.Visible;
                txtSinSesion.Text = "No se encontraron datos para el participante solicitado. Favor de revisar si los parametros suministrados son los correctos.";
            }
        }

        [ScriptableMember]
        public void PassData(string data)
        {
            
        }

        private void syncClient_DownloadStringCompleted(object sender, DownloadStringCompletedEventArgs e)
        {
            if (e.Error == null)
            {
                string json = e.Result;

                UsuarioJson obj = JsonHelper.DeserializeToPerson(json);
            }
        }

        private void InsertarParticipanteUnicoCompleted(object sender, Talentos_Master.TalentosReference.InsertarParticipanteUnicoCompletedEventArgs e)
        {
            sesion.participante.UsuarioId = Convert.ToInt32(e.Result);
            BusyWindow.IsBusy = false;
        }

        private void btnIniciarSession_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            //_cambiarContenido(Enumerador.Pagina.Login);
            _cambiarContenido(Enumerador.Pagina.MasterRueda);
        }

        private void Image_MouseLeftButtonDown_1(object sender, MouseButtonEventArgs e)
        {

        }
    }
}
