using System;
using System.Collections.Generic;
using System.Web;
using System.Web.Services;
using UPC.Seguridad.Talentos.BL.BE;
using UPC.Seguridad.Talentos.BL.BC;
using System.Web.Script.Services;
using System.ServiceModel.Activation;
using System.Diagnostics;

namespace UPC.Seguridad.Talentos.SI.WS
{
    /// <summary>
    /// Este Web Service contiene todos los métodos relacionados con la Seguridad del Juego de Talentos
    /// </summary>
    //[WebService(Namespace = "http://tempuri.org/")]
    [ScriptService]
    [WebService(Namespace = "http://microsoft.com/webservices/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    // [System.Web.Script.Services.ScriptService]
    [AspNetCompatibilityRequirements(RequirementsMode = AspNetCompatibilityRequirementsMode.Allowed)]
    public class WebServiceTalentos : System.Web.Services.WebService
    {
        [WebMethod]
        public UsuarioBE LoginPhp(UsuarioBE be)
        {
            LoginReference.miWebServicePortClient ws = new UPC.Seguridad.Talentos.SI.WS.LoginReference.miWebServicePortClient();
            LoginReference.Usuario user = new UPC.Seguridad.Talentos.SI.WS.LoginReference.Usuario();
            user.email = be.Correo;
            user.pwd = be.Password;

            // TODO: Quitar comentarios
            //LoginReference.Usuario logueado = new UPC.Seguridad.Talentos.SI.WS.LoginReference.Usuario();
            //logueado=ws.Login(user);

            UsuarioBE userLogueado = new UsuarioBE();
            userLogueado.Nombres = "Malena Castillo";// logueado.nombre;
            userLogueado.Correo = "malenna@gmail.com";// logueado.email;
            userLogueado.CentroTrabajo = "Interbank";
            userLogueado.Sexo = 1;// logueado.sexo;
            userLogueado.UsuarioId = 7;// logueado.id;

            return userLogueado;
        }

        //[WebMethod]
        //public RespuestaBE  LoginPhp(UsuarioBE be)
        //{
        //    RespuestaBE res = new RespuestaBE();

        //     if (!Blindaje.InvalidString(be.APaterno) && !Blindaje.InvalidString(be.AMaterno) && !Blindaje.InvalidString(be.Carrera) &&
        //        !Blindaje.InvalidString(be.CargoTrabajo) && !Blindaje.InvalidString(be.Correo) && !Blindaje.InvalidString(be.Nombres)
        //         && !Blindaje.InvalidString(be.TipoParticipanteId.ToString()) && !Blindaje.InvalidString(be.Sexo.ToString())
        //        && !Blindaje.InvalidString(be.TelfFijo) && !Blindaje.InvalidString(be.UsuarioId.ToString()) && !Blindaje.InvalidString(be.CarreraId.ToString())
        //        && !Blindaje.InvalidString(be.CentroEstudio) && !Blindaje.InvalidString(be.CentroTrabajo) && !Blindaje.InvalidString(be.CorreoTrabajo)
        //        && !Blindaje.InvalidString(be.Direccion) && !Blindaje.InvalidString(be.DireccionCentroTrabajo) && !Blindaje.InvalidString(be.DistritoEstudioId.ToString())
        //        && !Blindaje.InvalidString(be.DistritoTrabajoId.ToString()) && !Blindaje.InvalidString(be.FechaNac.ToString()) && !Blindaje.InvalidString(be.NivelInstruccionId.ToString())
        //        && !Blindaje.InvalidString(be.Nombres) && !Blindaje.InvalidString(be.OcupacionId.ToString())
        //        && !Blindaje.InvalidString(be.Password) && !Blindaje.InvalidString(be.Sexo.ToString()) && !Blindaje.InvalidString(be.TelfFijo)
        //        && !Blindaje.InvalidString(be.TelfMovil) && !Blindaje.InvalidString(be.TelfTrabajo) && !Blindaje.InvalidString(be.UsuarioId.ToString()) 
        //        && !Blindaje.InvalidString(be.TipoParticipanteId.ToString()))
        //     {
        //        LoginReference.miWebServicePortClient ws = new UPC.Seguridad.Talentos.SI.WS.LoginReference.miWebServicePortClient();
        //        LoginReference.Usuario user = new UPC.Seguridad.Talentos.SI.WS.LoginReference.Usuario();
        //        user.email = be.Correo;
        //        user.pwd = be.Password;

        //        LoginReference.Respuesta logueado = new UPC.Seguridad.Talentos.SI.WS.LoginReference.Respuesta();
        //        logueado = ws.Login(user);

        //        res.Mensaje = logueado.mensaje;
        //        res.Valor = logueado.valor;
        //        UsuarioBE userLogueado = new UsuarioBE();

        //        if (logueado.usuario != null)
        //        {

        //            userLogueado.Nombres = logueado.usuario.nombre;
        //            userLogueado.Correo = logueado.usuario.email;
        //            userLogueado.Sexo = logueado.usuario.sexo;
        //            userLogueado.UsuarioId = Convert.ToInt32(logueado.usuario.id);
        //            userLogueado.APaterno = logueado.usuario.apellido;
        //        }

        //        res.UsuarioLogueado = userLogueado;

        //        return res;
        //     }
        //     else
        //     {
        //         res.Mensaje = " No se permite el uso de caracteres especiales";
        //         res.Valor = 0;
        //         res.UsuarioLogueado = null;
        //         return res;
        //     }
           
        //}

        [WebMethod]
        public UsuarioBE LoginMasivo(string mail, string password)
        {
            UsuarioBC objUsuarioBC = new UsuarioBC();
            UsuarioBE objUsuarioBE = objUsuarioBC.LoginMasivo(mail, password);

            return objUsuarioBE;
        }

        [WebMethod]
        public int ValidarSesion(SesionBE objSesion)
        {
            if (!Blindaje.InvalidString(objSesion.Participante_id.ToString()) )//&& !Blindaje.InvalidString(objSesion.Password.ToString()))
            {
                SesionBC objSesionBC = new SesionBC();
                int activo = objSesionBC.ValidarSesion(objSesion.Participante_id);
                return activo;
            }
            else
                return -1;
        }

        [WebMethod]
        public int GuardarSesion(SesionBE objSesion)
        {
            if (!Blindaje.InvalidString(objSesion.Participante_id.ToString()) )//&& !Blindaje.InvalidString(objSesion.Password.ToString()))
            {
                SesionBC objSesionBC = new SesionBC();
                return objSesionBC.GuardarSesion(objSesion);
            }
            else
                return -2;
        }


        [WebMethod]
        [ScriptMethod]
        public int EliminarSesion(SesionBE objSesion)
        {
            if (!Blindaje.InvalidString(objSesion.Participante_id.ToString()) )//&& !Blindaje.InvalidString(objSesion.Password.ToString()))
            {
                SesionBC objSesionBC = new SesionBC();
                Debug.WriteLine("*** Saving work.");
                System.Threading.Thread.Sleep(3000);

                return objSesionBC.EliminarSesion(objSesion);
            }
            else
                return -1;
        }



        [WebMethod]
        public UsuarioBE EncriptarMd5(UsuarioBE objParticipanteBE)
        {

            UsuarioBC objUsuarioBC = new UsuarioBC();
            if (!Blindaje.InvalidString(objParticipanteBE.APaterno) && !Blindaje.InvalidString(objParticipanteBE.AMaterno) && !Blindaje.InvalidString(objParticipanteBE.Carrera) &&
                 !Blindaje.InvalidString(objParticipanteBE.CargoTrabajo) && !Blindaje.InvalidString(objParticipanteBE.Correo) && !Blindaje.InvalidString(objParticipanteBE.Nombres)
                  && !Blindaje.InvalidString(objParticipanteBE.TipoParticipanteId.ToString()) && !Blindaje.InvalidString(objParticipanteBE.Sexo.ToString())
                 && !Blindaje.InvalidString(objParticipanteBE.TelfFijo) && !Blindaje.InvalidString(objParticipanteBE.UsuarioId.ToString()) && !Blindaje.InvalidString(objParticipanteBE.CarreraId.ToString())
                 && !Blindaje.InvalidString(objParticipanteBE.CentroEstudio) && !Blindaje.InvalidString(objParticipanteBE.CentroTrabajo) && !Blindaje.InvalidString(objParticipanteBE.CorreoTrabajo)
                 && !Blindaje.InvalidString(objParticipanteBE.Direccion) && !Blindaje.InvalidString(objParticipanteBE.DireccionCentroTrabajo) && !Blindaje.InvalidString(objParticipanteBE.DistritoEstudioId.ToString())
                 && !Blindaje.InvalidString(objParticipanteBE.DistritoTrabajoId.ToString())  && !Blindaje.InvalidString(objParticipanteBE.NivelInstruccionId.ToString())
                 && !Blindaje.InvalidString(objParticipanteBE.Nombres) && !Blindaje.InvalidString(objParticipanteBE.OcupacionId.ToString())
                 && !Blindaje.InvalidString(objParticipanteBE.Password) && !Blindaje.InvalidString(objParticipanteBE.Sexo.ToString()) && !Blindaje.InvalidString(objParticipanteBE.TelfFijo)
                 && !Blindaje.InvalidString(objParticipanteBE.TelfMovil) && !Blindaje.InvalidString(objParticipanteBE.TelfTrabajo) && !Blindaje.InvalidString(objParticipanteBE.UsuarioId.ToString())
                 && !Blindaje.InvalidString(objParticipanteBE.TipoParticipanteId.ToString()))
           
                return objUsuarioBC.EncriptarMd5(objParticipanteBE);
            else
                return null;
        }

        [WebMethod]
        public List<UsuarioBE> ObtenerFamososPorCarrera(int idCarrera)
        {
            UsuarioBC bc = new UsuarioBC();

            if (!Blindaje.InvalidString(idCarrera.ToString()))
            {
                return bc.ObtenerFamosoPorCarrera(idCarrera);
            }
            else
            {
                List<UsuarioBE> lista = new List<UsuarioBE>();
                return lista;
            }

        }

        [WebMethod]
        public List<UsuarioBE> ObtenerFamososPorCarrerasId(string idCarreras)
        {
            UsuarioBC bc = new UsuarioBC();

            if (!Blindaje.InvalidString(idCarreras))
            {
                return bc.ObtenerFamosoPorCarrerasId(idCarreras);
            }
            else
            {
                List<UsuarioBE> lista = new List<UsuarioBE>();
                return lista;
            }

        }

        [WebMethod]
        public UsuarioBE ObtenerParticipante(String correo, String password)
        {
            UsuarioBE login = new UsuarioBE();
            login.Password = password;
            login.Correo = correo;

            UsuarioBC objParticipanteBC = new UsuarioBC();

            UsuarioBE desencrypt = new UsuarioBE();
            desencrypt = objParticipanteBC.desencriptar(login);

            char[] caracteres = {'\0'};
            desencrypt.Password = desencrypt.Password.TrimEnd(caracteres);
            UsuarioBE objParticipanteBE = new UsuarioBE();

            if (!Blindaje.InvalidString(desencrypt.Correo) && !Blindaje.InvalidString(desencrypt.Password))
            {
                objParticipanteBE = objParticipanteBC.ObtenerParticipante(desencrypt.Correo, desencrypt.Password);
                return objParticipanteBE;
            }
            else
                return null;
        }

        [WebMethod]
        public List<PaisBE> ListarPaises()
        {
            List<PaisBE> lst = new List<PaisBE>();
            UbigeoBC objDepartamentoBC = new UbigeoBC();
            lst = objDepartamentoBC.ListarPaises();
            return lst;
        }
        
        
        [WebMethod]
        public List<DepartamentoBE> ListarDepartamentos()
        {
            List<DepartamentoBE> lst = new List<DepartamentoBE>();
            UbigeoBC objDepartamentoBC = new UbigeoBC();
            lst = objDepartamentoBC.ListarDepartamento();
            return lst;
            
        }

        [WebMethod]
        public List<ProvinciaBE> ListarProvincias(string DepartamentoId)
        {
            List<ProvinciaBE> lst = new List<ProvinciaBE>();
            UbigeoBC objProvinciaBC = new UbigeoBC();
            if (!Blindaje.InvalidString(DepartamentoId))
            {
                lst = objProvinciaBC.ListarProvincia(DepartamentoId);
             
            }
 
                return lst;

            
        }


        [WebMethod]
        public List<DistritoBE> ListarDistritos(string ProvinciaId, string DepartamentoId)
        {

            List<DistritoBE> lst = new List<DistritoBE>();
            UbigeoBC objDistritoBC = new UbigeoBC();
            if (!Blindaje.InvalidString(ProvinciaId) && !Blindaje.InvalidString(DepartamentoId))
            {
                lst = objDistritoBC.ListarDistrito(ProvinciaId, DepartamentoId);
            }
                return lst;
         
            
        }

        [WebMethod]
        public int InsertarParticipante(UsuarioBE objParticipanteBE)
        {
            UsuarioBC bc = new UsuarioBC();
            if (!Blindaje.InvalidString(objParticipanteBE.APaterno) && !Blindaje.InvalidString(objParticipanteBE.AMaterno) && !Blindaje.InvalidString(objParticipanteBE.Carrera) &&
                !Blindaje.InvalidString(objParticipanteBE.CargoTrabajo) && !Blindaje.InvalidString(objParticipanteBE.Correo) && !Blindaje.InvalidString(objParticipanteBE.Nombres)
                 && !Blindaje.InvalidString(objParticipanteBE.TipoParticipanteId.ToString()) && !Blindaje.InvalidString(objParticipanteBE.Sexo.ToString())
                && !Blindaje.InvalidString(objParticipanteBE.TelfFijo) && !Blindaje.InvalidString(objParticipanteBE.UsuarioId.ToString()) && !Blindaje.InvalidString(objParticipanteBE.CarreraId.ToString())
                && !Blindaje.InvalidString(objParticipanteBE.CentroEstudio) && !Blindaje.InvalidString(objParticipanteBE.CentroTrabajo) && !Blindaje.InvalidString(objParticipanteBE.CorreoTrabajo)
                && !Blindaje.InvalidString(objParticipanteBE.Direccion) && !Blindaje.InvalidString(objParticipanteBE.DireccionCentroTrabajo) && !Blindaje.InvalidString(objParticipanteBE.DistritoEstudioId.ToString())
                && !Blindaje.InvalidString(objParticipanteBE.DistritoTrabajoId.ToString())  && !Blindaje.InvalidString(objParticipanteBE.NivelInstruccionId.ToString())
                && !Blindaje.InvalidString(objParticipanteBE.Nombres) && !Blindaje.InvalidString(objParticipanteBE.OcupacionId.ToString())
                && !Blindaje.InvalidString(objParticipanteBE.Password) && !Blindaje.InvalidString(objParticipanteBE.Sexo.ToString()) && !Blindaje.InvalidString(objParticipanteBE.TelfFijo)
                && !Blindaje.InvalidString(objParticipanteBE.TelfMovil) && !Blindaje.InvalidString(objParticipanteBE.TelfTrabajo) && !Blindaje.InvalidString(objParticipanteBE.UsuarioId.ToString()) 
                && !Blindaje.InvalidString(objParticipanteBE.TipoParticipanteId.ToString()))
                return bc.InsertarParticipante(objParticipanteBE);
            else 
                return -1;
        }

        [WebMethod]
        public int GuardarError(LogBE be)
        {
            LogBC bc = new LogBC();
            if (!Blindaje.InvalidString(be.Descripcion) && !Blindaje.InvalidString(be.Id.ToString()) && !Blindaje.InvalidString(be.UsuarioId.ToString()))
                return bc.LogInsertar(be);
            else
                return -1;
        }

        [WebMethod (EnableSession = true) ]
        public void PutSession(int idUsuario)
        {
           
            System.Web.HttpContext.Current.Session["idUsuario"] = idUsuario.ToString();
        }

        [WebMethod(EnableSession = true)]
        public int GetSession()
        {
            int id =Convert.ToInt32(System.Web.HttpContext.Current.Session["idUsuario"] );
            if (id != null)
                return id;
            else
                return 0;
        }

        [WebMethod(EnableSession = true)]
        public void LimpiarSession()
        {
            Session.Clear();

            
        }

        [WebMethod]
        public int ValidarEmailUnico(string mail)
        {
            UsuarioBC bc = new UsuarioBC();
            if (!Blindaje.InvalidString(mail))
                return bc.ValidarEmailUnico(mail);
            else
                return -1;
        }

        
    }
}
