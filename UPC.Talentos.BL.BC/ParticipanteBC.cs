using System;
using System.Collections.Generic;
using UPC.Talentos.BL.BE;
using UPC.Talentos.DL.DALC;
using System.Net.Mail;
using System.Net;
using System.Configuration;

namespace UPC.Talentos.BL.BC
{
    public class ParticipanteBC
    {

        public bool VerificaParticipanteActivo(string codEvaluacion)
        {
            ParticipanteDALC objParticipanteDALC = new ParticipanteDALC();
            bool result = objParticipanteDALC.VerificaParticipanteActivo(codEvaluacion);

            return result;
        }

        public bool VerificaParticipanteAdultoActivo(string codEvaluacion)
        {
            ParticipanteDALC objParticipanteDALC = new ParticipanteDALC();
            bool result = objParticipanteDALC.VerificaParticipanteAdultoActivo(codEvaluacion);

            return result;
        }

        public bool InsertarParticipante(List<ParticipanteBE> lstParticipantes)
        {
            ParticipanteDALC objParticipanteDALC = new ParticipanteDALC();
            int id = 0;

            foreach (ParticipanteBE item in lstParticipantes)
            {
                try
                {
                    id = objParticipanteDALC.InsertarParticipante(item);
                }
                catch (Exception)
                {
                    throw;
                }
            }

            return true;
        }

        public int InsertarParticipante(ParticipanteBE objParticipanteBE)
        {
            ParticipanteDALC objParticipanteDALC = new ParticipanteDALC();
            int id = 0;

            try
            {
                id = objParticipanteDALC.InsertarParticipante(objParticipanteBE);
            }
            catch (Exception)
            {
                throw;
            }

            return id;
        }

        public int InsertarParticipanteAdulto(ParticipanteBE objParticipanteBE)
        {
            ParticipanteDALC objParticipanteDALC = new ParticipanteDALC();
            int id = 0;

            try
            {
                id = objParticipanteDALC.InsertarParticipanteAdulto(objParticipanteBE);
            }
            catch (Exception)
            {
                throw;
            }

            return id;
        }

        public List<ParticipanteBE> ListarParticipantesMasivos()
        {
            ParticipanteDALC objParticipanteDALC = new ParticipanteDALC();

            return objParticipanteDALC.ListarParticipantesMasivos();
        }

        public string ObtenerPasswordParticipante(string DNI)
        {
            ParticipanteDALC objParticipanteDALC = new ParticipanteDALC();

            return objParticipanteDALC.ObtenerPasswordParticipante(DNI);
        }


        public bool EnviarMail(string CodEvaluacion, string token)
        {

            ParticipanteDALC objParticipanteDALC = new ParticipanteDALC();

            string correoElectronico = "";

            string codigoEvaluacion = "";


            string nombre = objParticipanteDALC.ObtenerNombreParticipante(CodEvaluacion, ref correoElectronico, ref codigoEvaluacion);


            string smtpAddress = ConfigurationSettings.AppSettings["SMTP"].ToString();


            int portNumber = Convert.ToInt32(ConfigurationSettings.AppSettings["PUERTO"]);


            bool enableSSL = Convert.ToBoolean(ConfigurationSettings.AppSettings["ENABLESSL"]);


            string url = ConfigurationSettings.AppSettings["URL_REPORTE"].ToString();


            string urlIn = ConfigurationSettings.AppSettings["URL_INTERES"].ToString();


            string emailFrom = ConfigurationSettings.AppSettings["FROM"].ToString();


            string emailUserName = ConfigurationSettings.AppSettings["EMAILUSERNAME"].ToString();


            string contrasena = ConfigurationSettings.AppSettings["PASSWORD"].ToString();


            string emailTo = correoElectronico;


            string subject = "Test de Talentos - Reporte de Resultados";


            //string body = @"Estimado(a) #NOMBRE#, gracias por utilizar el Test de Talentos.<br/>Para visualizar el resultado del test haga clic <a href='#URL_TALENTOS#'>aqui</a>: <br/>" +


            // "Para visualizar el resultado de su evaluacn de intereses haga clic <a href='#URL_INTERES#'>aqui</a><br/><br/>Gracias.";


            string body = "<table border='0' cellpadding='0' cellspacing='0' width='100%' style='border:1px #e1e1e1 solid;background:#e9e9e9'>";

            body = body + "<tbody><tr>";

            body = body + "<td align='center' valign='top'>";

            body = body + "<table border='0' cellpadding='0' cellspacing='0' width='100%'>";

            body = body + "<tbody><tr>";

            body = body + "<td>";

            body = body + "<div style='background:#ff1901'>";

            body = body + "<h1 style='font-size:16px;margin:0;display:block;color:#fff;padding:10px;font-weight:normal'>";

            body = body + "<span style='display:block;float:right;font-size:23px;font-weight:bold;padding:10px 3%;text-align:right;width:44%'>";

            body = body + "<img src='https://ci5.googleusercontent.com/proxy/6iu7Um6fyFpLqlyy8MSsXbMI14XYmc07TsSW4D8ZH6PFbGCPU5ia22XpcnMaRnflf7OedIkvE3f5sgM4gCz5__vK1WtdtZ7zxcAKSwz9qx6UsqtTVZXDpSM=s0-d-e1-ft#http://www.upc.edu.pe/sites/all/themes/upc_2013/img/logo_footer.png' title='UPC' alt='UPC'>";

            body = body + "</span>";

            body = body + "<span style='display:block;float:left;font-size:23px;padding:10px 3%;text-align:left;width:44%'>Test de Talentos</span>";

            body = body + "<div style='clear:both'></div>";

            body = body + "</h1>";

            body = body + "</div>";

            body = body + "</td>";

            body = body + "</tr>";

            body = body + "</tbody></table>";

            body = body + "</td>";

            body = body + "</tr>";

            body = body + "<tr>";

            body = body + "<td align='center' valign='top'>";

            body = body + "<table border='0' cellpadding='0' cellspacing='0' width='580'>";

            body = body + "<tbody><tr>";

            body = body + "<td valign='top'>";

            body = body + "<table border='0' cellpadding='0' cellspacing='0' width='100%'>";

            body = body + "<tbody><tr>";

            body = body + "<td valign='top'>";

            body = body + "<div></div>";

            body = body + "<div style='background:#e9e9e9;padding:40px 24px;font-family:helvetica,arial,sans-serif'>";

            body = body + "<p>Estimado(a)&nbsp;#NOMBRE#,</p>";

            body = body + "<p>&nbsp;</p>";

            body = body + "<p>Has completado satisfactoriamente los tests de <strong>Intereses</strong> y <strong>Talentos</strong>. </strong>&nbsp;.";

            body = body + "<p>&nbsp;</p>";

            body = body + "<p>Para ver tus resultados, consulta los siguientes enlaces:&nbsp;</p>";

            body = body + "<p>&nbsp;</p>";

            body = body + "<p><em><a title='Informe Test de Intereses' href='#URL_INTERES#' target='_blank'>Resultados del <strong>Test de Intereses</strong></a></em></p>";

            body = body + "<p><em><a title='Informe Test de Talentos' href='#URL_TALENTOS#' target='_blank'>Resultados del <strong>Test de Talentos</strong></a></em></p>";

            body = body + "<p><em>&nbsp;</em></p>";

            body = body + "<p>Te sugerimos que leas los informes con atención. Mientras lo haces, piensa en cómo te describen estos resultados.</p>";

            body = body + "<p><em>&nbsp;</em></p>";

            body = body + "<p><em>&nbsp;</em></p>";

            body = body + "<p><strong>Lo que viene ahora ...</strong></p>";

            body = body + "<p><em>&nbsp;</em></p>";

            body = body + "<p>Si tienes una <strong>entrevista de admisión</strong> ya programada, recuerda estar en la universidad 10 minutos antes de la hora pactada.</p>";

            body = body + "<p><em>&nbsp;</em></p>";

            body = body + "<p>Si tienes alguna consulta sobre tus resultados o quieres pedir una entrevista con un orientador de UPC, escríbenos a: <a href='oficina_admision@upc.edu.pe'>oficina_admision@upc.edu.pe</a></p>";

            body = body + "<p><em>&nbsp;</em></p>";

            body = body + "<p>Atentamente.</p>";

            body = body + "<p>Universidad Peruana de Ciencias Aplicadas</p>";

            body = body + "</div> ";

            body = body + "</td>";

            body = body + "</tr>";

            body = body + "</tbody></table>";

            body = body + "</td>";

            body = body + "</tr>";

            body = body + "</tbody></table>";

            body = body + "</td>";

            body = body + "</tr>";

            body = body + "</tbody></table>";


            string urlTalentos = url + "?id=" + codigoEvaluacion;


            string urlInteres = urlIn + "?id=" + codigoEvaluacion + "&token=" + token;


            body = body.Replace("#NOMBRE#", nombre);


            body = body.Replace("#URL_TALENTOS#", urlTalentos);


            body = body.Replace("#URL_INTERES#", urlInteres);


            MailMessage mail = new MailMessage();


            SmtpClient SmtpServer = new SmtpClient(smtpAddress);


            mail.From = new MailAddress(emailFrom, "UPC - Notificación");


            mail.To.Add(new System.Net.Mail.MailAddress(emailTo));//emailTo);


            mail.Subject = subject;

            //Evitar que el correo se vaya al SPAM
            mail.Headers.Add("From", "UPC - Notificación <admisiondeltalentoupc@upc.edu.pe>");
            mail.Headers.Add("Return-Path", "admisiondeltalentoupc@upc.edu.pe");
            mail.Headers.Add("Reply-To", "admisiondeltalentoupc@upc.edu.pe");
            mail.Headers.Add("Organization", "UPC");
            mail.Headers.Add("MIME-Version", "1.0");
            //mail.Headers.Add("Content-type", "text/html; charset=iso-8859-1");
            mail.Headers.Add("Content-type", "text/html; charset=UTF-8");
            //mail.Headers.Add("Content-Transfer-Encoding", "uuencode");
            mail.Headers.Add("X-Priority", "2");
            mail.Headers.Add("X-Sender", "admisiondeltalentoupc@upc.edu.pe");
            mail.Headers.Add("X-Mailer", "Microsoft.NET Framework v2.0.50727");


            mail.Body = body;


            mail.IsBodyHtml = true;

            SmtpServer.Credentials = new System.Net.NetworkCredential(emailUserName, contrasena);

            SmtpServer.EnableSsl = enableSSL;

            SmtpServer.Send(mail);


            return true;


        }


        //Solo link de talentos Adultos
        public bool EnviarMailAdulto_Talentos(string CodEvaluacion, string token)
        {

            ParticipanteDALC objParticipanteDALC = new ParticipanteDALC();

            string correoElectronico = "";

            string codigoEvaluacion = "";


            string nombre = objParticipanteDALC.ObtenerNombreParticipanteAdulto(CodEvaluacion, ref correoElectronico, ref codigoEvaluacion);


            string smtpAddress = ConfigurationSettings.AppSettings["SMTP"].ToString();


            int portNumber = Convert.ToInt32(ConfigurationSettings.AppSettings["PUERTO"]);


            bool enableSSL = true;


            string url = ConfigurationSettings.AppSettings["URL_REPORTE_ADULTO"].ToString();


            string emailFrom = ConfigurationSettings.AppSettings["FROM_FISCHMAN"].ToString();


            string contrasena = ConfigurationSettings.AppSettings["PASSWORD_FISCHMAN"].ToString();


            string emailTo = correoElectronico;


            string subject = "Test de Talentos - Reporte de Resultados";


            string body = "<table border='0' cellpadding='0' cellspacing='0' width='100%' style='border:1px #e1e1e1 solid;background:#e9e9e9'>";

            body = body + "<tbody><tr>";

            body = body + "<td align='center' valign='top'>";

            body = body + "<table border='0' cellpadding='0' cellspacing='0' width='100%'>";

            body = body + "<tbody><tr>";

            body = body + "<td>";

            body = body + "<div style='background:#f2900c;'>";

            body = body + "<h1 style='font-size:16px;margin:0;display:block;color:#fff;padding:10px;font-weight:normal'>";

            body = body + "<span style='display:block;float:right;font-size:23px;font-weight:bold;padding:10px 3%;text-align:right;width:44%'>";
            /*
            body = body + "<img src='https://ci5.googleusercontent.com/proxy/6iu7Um6fyFpLqlyy8MSsXbMI14XYmc07TsSW4D8ZH6PFbGCPU5ia22XpcnMaRnflf7OedIkvE3f5sgM4gCz5__vK1WtdtZ7zxcAKSwz9qx6UsqtTVZXDpSM=s0-d-e1-ft#http://www.upc.edu.pe/sites/all/themes/upc_2013/img/logo_footer.png' title='UPC' alt='UPC'>";
            */
            body = body + "</span>";

            body = body + "<span style='display:block;float:left;font-size:23px;padding:10px 3%;text-align:left;width:44%'>Test de Talentos</span>";

            body = body + "<div style='clear:both'></div>";

            body = body + "</h1>";

            body = body + "</div>";

            body = body + "</td>";

            body = body + "</tr>";

            body = body + "</tbody></table>";

            body = body + "</td>";

            body = body + "</tr>";

            body = body + "<tr>";

            body = body + "<td align='center' valign='top'>";

            body = body + "<table border='0' cellpadding='0' cellspacing='0' width='580'>";

            body = body + "<tbody><tr>";

            body = body + "<td valign='top'>";

            body = body + "<table border='0' cellpadding='0' cellspacing='0' width='100%'>";

            body = body + "<tbody><tr>";

            body = body + "<td valign='top'>";

            body = body + "<div></div>";

            body = body + "<div style='background:#e9e9e9;padding:40px 24px;font-family:helvetica,arial,sans-serif'>";

            body = body + "<p>Estimado(a)&nbsp;#NOMBRE#,</p>";

            body = body + "<p>&nbsp;</p>";

            body = body + "<p>Has completado satisfactoriamente el test de <strong>Talentos</strong>. </strong>&nbsp;";

            body = body + "<p>&nbsp;</p>";

            body = body + "<p>Para ver su resultado, consulte el siguiente enlace:&nbsp;</p>";

            body = body + "<p>&nbsp;</p>";

            body = body + "<p><em><a title='Informe Test de Talentos' href='#URL_TALENTOS#' target='_blank'>Resultados del <strong>Test de Talentos</strong></a></em></p>";

            body = body + "<p><em>&nbsp;</em></p>";

            body = body + "<p>Le sugerimos que lea el informe con atención. Mientras lo hace, piense en cómo le describen estos resultados.</p>";

            body = body + "<p><em>&nbsp;</em></p>";

            body = body + "<p>Atentamente.</p>";

            body = body + "<p>Fischman Consultores</p>";

            body = body + "</div> ";

            body = body + "</td>";

            body = body + "</tr>";

            body = body + "</tbody></table>";

            body = body + "</td>";

            body = body + "</tr>";

            body = body + "</tbody></table>";

            body = body + "</td>";

            body = body + "</tr>";

            body = body + "</tbody></table>";


            string urlTalentos = url + "?id=" + codigoEvaluacion;


            body = body.Replace("#NOMBRE#", nombre);


            body = body.Replace("#URL_TALENTOS#", urlTalentos);



            MailMessage mail = new MailMessage();


            SmtpClient SmtpServer = new SmtpClient(smtpAddress);

            mail.From = new MailAddress(emailFrom, "Fischman - Notificación");

            mail.To.Add(new System.Net.Mail.MailAddress(emailTo));//emailTo);


            mail.Subject = subject;

            //Evitar que el correo se vaya al SPAM
            mail.Headers.Add("From", "Fischman - Notificación <fischman.notificacion@gmail.com>");
            mail.Headers.Add("Return-Path", "fischman.notificacion@gmail.com");
            mail.Headers.Add("Reply-To", "fischman.notificacion@gmail.com");
            mail.Headers.Add("Organization", "FyA");
            mail.Headers.Add("MIME-Version", "1.0");
            //mail.Headers.Add("Content-type", "text/html; charset=iso-8859-1");
            mail.Headers.Add("Content-type", "text/html; charset=UTF-8");
            //mail.Headers.Add("Content-Transfer-Encoding", "uuencode");
            mail.Headers.Add("X-Priority", "2");
            mail.Headers.Add("X-Sender", "fischman.notificacion@gmail.com");
            mail.Headers.Add("X-Mailer", "Microsoft.NET Framework v2.0.50727");

            mail.Body = body;



            mail.IsBodyHtml = true;

            SmtpServer.Credentials = new System.Net.NetworkCredential(emailFrom, contrasena);

            SmtpServer.EnableSsl = enableSSL;

            SmtpServer.Send(mail);


            return true;

        }


        public bool EnviarMailAdulto_v2(string CodEvaluacion, string token)
        {

            ParticipanteDALC objParticipanteDALC = new ParticipanteDALC();

            string correoElectronico = "";

            string codigoEvaluacion = "";


            string nombre = objParticipanteDALC.ObtenerNombreParticipanteAdulto(CodEvaluacion, ref correoElectronico, ref codigoEvaluacion);


            string smtpAddress = ConfigurationSettings.AppSettings["SMTP"].ToString();


            int portNumber = Convert.ToInt32(ConfigurationSettings.AppSettings["PUERTO"]);


            bool enableSSL = true;


            string url = ConfigurationSettings.AppSettings["URL_REPORTE_ADULTO"].ToString();

            string urlTemp = ConfigurationSettings.AppSettings["URL_TEMPERAMENTOS_ADULTO"].ToString();

            string emailFrom = ConfigurationSettings.AppSettings["FROM_FISCHMAN"].ToString();

            string contrasena = ConfigurationSettings.AppSettings["PASSWORD_FISCHMAN"].ToString();


            string emailTo = correoElectronico;


            string subject = "Test de Talentos - Reporte de Resultados";


            //string body = @"Estimado(a) #NOMBRE#, gracias por utilizar el Test de Talentos.<br/>Para visualizar el resultado del test haga clic <a href='#URL_TALENTOS#'>aqui</a>: <br/>" +


            // "Para visualizar el resultado de su evaluacn de intereses haga clic <a href='#URL_INTERES#'>aqui</a><br/><br/>Gracias.";


            string body = "<table border='0' cellpadding='0' cellspacing='0' width='100%' style='border:1px #e1e1e1 solid;background:#e9e9e9'>";

            body = body + "<tbody><tr>";

            body = body + "<td align='center' valign='top'>";

            body = body + "<table border='0' cellpadding='0' cellspacing='0' width='100%'>";

            body = body + "<tbody><tr>";

            body = body + "<td>";

            body = body + "<div style='background:#f2900c;'>";

            body = body + "<h1 style='font-size:16px;margin:0;display:block;color:#fff;padding:10px;font-weight:normal'>";

            body = body + "<span style='display:block;float:right;font-size:23px;font-weight:bold;padding:10px 3%;text-align:right;width:44%'>";
            /*
            body = body + "<img src='https://ci5.googleusercontent.com/proxy/6iu7Um6fyFpLqlyy8MSsXbMI14XYmc07TsSW4D8ZH6PFbGCPU5ia22XpcnMaRnflf7OedIkvE3f5sgM4gCz5__vK1WtdtZ7zxcAKSwz9qx6UsqtTVZXDpSM=s0-d-e1-ft#http://www.upc.edu.pe/sites/all/themes/upc_2013/img/logo_footer.png' title='UPC' alt='UPC'>";
            */
            body = body + "</span>";

            body = body + "<span style='display:block;float:left;font-size:23px;padding:10px 3%;text-align:left;width:44%'>Test de Talentos</span>";

            body = body + "<div style='clear:both'></div>";

            body = body + "</h1>";

            body = body + "</div>";

            body = body + "</td>";

            body = body + "</tr>";

            body = body + "</tbody></table>";

            body = body + "</td>";

            body = body + "</tr>";

            body = body + "<tr>";

            body = body + "<td align='center' valign='top'>";

            body = body + "<table border='0' cellpadding='0' cellspacing='0' width='580'>";

            body = body + "<tbody><tr>";

            body = body + "<td valign='top'>";

            body = body + "<table border='0' cellpadding='0' cellspacing='0' width='100%'>";

            body = body + "<tbody><tr>";

            body = body + "<td valign='top'>";

            body = body + "<div></div>";

            body = body + "<div style='background:#e9e9e9;padding:40px 24px;font-family:helvetica,arial,sans-serif'>";

            body = body + "<p>Estimado(a)&nbsp;#NOMBRE#,</p>";

            body = body + "<p>&nbsp;</p>";

            body = body + "<p>Has completado satisfactoriamente los tests de <strong>Temperamentos</strong> y <strong>Talentos</strong>. </strong>&nbsp;.";

            body = body + "<p>&nbsp;</p>";

            body = body + "<p>Para ver sus resultados, consulte los siguientes enlaces:&nbsp;</p>";

            body = body + "<p>&nbsp;</p>";

            body = body + "<p><em><a title='Informe Test de Temperamentos' href='#URL_TEMPERAMENTOS#' target='_blank'>Resultados del <strong>Test de Temperamentos</strong></a></em></p>";

            body = body + "<p><em><a title='Informe Test de Talentos' href='#URL_TALENTOS#' target='_blank'>Resultados del <strong>Test de Talentos</strong></a></em></p>";

            body = body + "<p><em>&nbsp;</em></p>";

            body = body + "<p>Le sugerimos que lea los informes con atención. Mientras lo hace, piense en cómo le describen estos resultados.</p>";

            body = body + "<p><em>&nbsp;</em></p>";

            body = body + "<p>Atentamente.</p>";

            body = body + "<p>Effectus Fischman Consultores</p>";

            body = body + "</div> ";

            body = body + "</td>";

            body = body + "</tr>";

            body = body + "</tbody></table>";

            body = body + "</td>";

            body = body + "</tr>";

            body = body + "</tbody></table>";

            body = body + "</td>";

            body = body + "</tr>";

            body = body + "</tbody></table>";


            string urlTalentos = url + "?id=" + codigoEvaluacion;


            string urlTemperamentos = urlTemp + "?id=" + codigoEvaluacion + "&token=" + token;


            body = body.Replace("#NOMBRE#", nombre);


            body = body.Replace("#URL_TALENTOS#", urlTalentos);


            body = body.Replace("#URL_TEMPERAMENTOS#", urlTemperamentos);


            MailMessage mail = new MailMessage();


            SmtpClient SmtpServer = new SmtpClient(smtpAddress);

            mail.From = new MailAddress(emailFrom, "Fischman - Notificación");

            mail.To.Add(new System.Net.Mail.MailAddress(emailTo));//emailTo);


            mail.Subject = subject;

            //Evitar que el correo se vaya al SPAM
            mail.Headers.Add("From", "Fischman - Notificación <fischman.notificacion@gmail.com>");
            mail.Headers.Add("Return-Path", "fischman.notificacion@gmail.com");
            mail.Headers.Add("Reply-To", "fischman.notificacion@gmail.com");
            mail.Headers.Add("Organization", "FyA");
            mail.Headers.Add("MIME-Version", "1.0");
            //mail.Headers.Add("Content-type", "text/html; charset=iso-8859-1");
            mail.Headers.Add("Content-type", "text/html; charset=UTF-8");
            //mail.Headers.Add("Content-Transfer-Encoding", "uuencode");
            mail.Headers.Add("X-Priority", "2");
            mail.Headers.Add("X-Sender", "fischman.notificacion@gmail.com");
            mail.Headers.Add("X-Mailer", "Microsoft.NET Framework v2.0.50727");

            mail.Body = body;


            mail.IsBodyHtml = true;

            SmtpServer.Credentials = new System.Net.NetworkCredential(emailFrom, contrasena);

            SmtpServer.EnableSsl = enableSSL;

            SmtpServer.Send(mail);


            return true;

        }


        public bool EnviarEmail(List<ParticipanteBE> lstParticipantes)
        {
            string smtpAddress = ConfigurationSettings.AppSettings["SMTP"].ToString();
            int portNumber = Convert.ToInt32(ConfigurationSettings.AppSettings["PUERTO"]);
            bool enableSSL = true;

            string password = "";
            string urlTalentos = ConfigurationSettings.AppSettings["URL_TALENTOS"].ToString();
            string emailFrom = ConfigurationSettings.AppSettings["FROM"].ToString();
            string contrasena = ConfigurationSettings.AppSettings["PASSWORD"].ToString();
            string emailTo = "";
            string subject = "Bienvenido a la Herramienta de Talentos !!!";
            string body = "";
            string bodyTmp = @"Estimado(a) #NOMBRE#, la dirección de correo electrónica para acceder a la herramienta de talentos " +
                @"es: #URL#" + @"<br/><br/>Su password para acceder a la aplicacion es #PASSWORD#<br/><br/>Gracias.";
            string url = "";

            foreach (ParticipanteBE item in lstParticipantes)
            {
                body = bodyTmp;
                url = urlTalentos + "?mail=" + item.CorreoElectronico;

                password = ObtenerPasswordParticipante(item.DNI);
                emailTo = item.CorreoElectronico;

                body = body.Replace("#NOMBRE#", item.Nombres);
                body = body.Replace("#PASSWORD#", password);
                body = body.Replace("#URL#", url);

                MailMessage mail = new MailMessage();
                SmtpClient SmtpServer = new SmtpClient(smtpAddress, portNumber);

                mail.From = new MailAddress(emailFrom);
                mail.To.Add(new System.Net.Mail.MailAddress(emailTo));//emailTo);
                mail.Subject = subject;
                mail.Body = body;
                mail.IsBodyHtml = true;

                SmtpServer.Credentials = new System.Net.NetworkCredential(emailFrom, contrasena);
                SmtpServer.EnableSsl = enableSSL;
                SmtpServer.Send(mail);
            }

            return true;
        }
    }
}
