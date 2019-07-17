using System;
using System.Collections.Generic;
using System.Web.Services;
using UPC.Talentos.BL.BC;
using UPC.Talentos.BL.BE;
using System.Net;
using System.Configuration;

namespace UPC.Talentos.BL.SI
{
    /// <summary>
    /// Summary description for WSTalentos
    /// </summary>
    //[WebService(Namespace = "http://tempuri.org/")]
    //[WebService(Namespace = "http://localhost/WSJuegoTalentos/")]
    [WebService(Namespace = "http://microsoft.com/webservices/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    // [System.Web.Script.Services.ScriptService]
    public class WSTalentos : System.Web.Services.WebService
    {
        [WebMethod]
        public string DevolverParticipante(string id, string token)
        {
            ParticipanteBC objParticipanteBC = new ParticipanteBC();
            bool existe = false;

            try
            {

                if (id.Substring(0, 1).Equals("0"))
                    return "";

                long entero = 0;
                bool canConvert = long.TryParse(id, out entero);

                if (canConvert == false)
                    return "";

            }

            catch (Exception)
            {
                return "";
            }

            try
            {

                existe = objParticipanteBC.VerificaParticipanteActivo(id);
            }
            catch (Exception ex)
            {
                LogBC objLogBC = new LogBC();
                string mensaje = "Metodo: DevolverParticipante (Verificar Existencia Participante). Mensaje: " + ex.Message;
                objLogBC.InsertarLog(mensaje);

                return "";
            }

            if (existe)
            {
                string vacio = "";
                return vacio;
            }
            else
            {
                string json = "";
                // hosting: sdch-upc.davidfischman.com
                // produccion: 66.135.63.156
                string url = ConfigurationManager.AppSettings["JSON_PERSONA"] + id + "/person?token=" + token;
                try
                {
                    var syncClient = new WebClient();
                    json = syncClient.DownloadString(url);
                }
                catch (Exception ex)
                {
                    LogBC objLogBC = new LogBC();
                    string mensaje = "Metodo: DevolverParticipante. Mensaje: " + ex.Message;
                    objLogBC.InsertarLog(mensaje);

                    return "";
                }

                return json;
            }
        }


        [WebMethod]
        public string DevolverParticipanteAdulto(string id, string token)
        {
            ParticipanteBC objParticipanteBC = new ParticipanteBC();
            bool existe = false;

            try
            {

                if (id.Substring(0, 1).Equals("0"))
                    return "";

                long entero = 0;
                bool canConvert = long.TryParse(id, out entero);

                if (canConvert == false)
                    return "";

            }

            catch (Exception)
            {
                return "";
            }

            try
            {

                existe = objParticipanteBC.VerificaParticipanteAdultoActivo(id);
            }
            catch (Exception ex)
            {
                LogBC objLogBC = new LogBC();
                string mensaje = "Metodo: DevolverParticipanteAdulto (Verificar Existencia Participante). Mensaje: " + ex.Message;
                objLogBC.InsertarLog(mensaje);

                return "";
            }

            if (existe)
            {
                string vacio = "";
                return vacio;
            }
            else
            {
                string json = "";
                // hosting: sdch-upc.davidfischman.com
                // produccion: 66.135.63.156
                string url = ConfigurationManager.AppSettings["JSON_PERSONA_ADULTO"] + id + "/person?token=" + token;
                try
                {
                    var syncClient = new WebClient();
                    json = syncClient.DownloadString(url);
                }
                catch (Exception ex)
                {
                    LogBC objLogBC = new LogBC();
                    string mensaje = "Metodo: DevolverParticipante. Mensaje: " + ex.Message;
                    objLogBC.InsertarLog(mensaje);

                    return "";
                }

                return json;
            }
        }

        [WebMethod]
        public int JuegoInsertar(JuegoBE objJuego)
        {
            JuegoBC objBC = new JuegoBC();
            return (objBC.JuegoInsertar(objJuego));
        }

        [WebMethod]
        public ParametroBE obtenerTemaActual()
        {
            ParametroBC objParametroBC = new ParametroBC();
            ParametroBE objParametroBE = new ParametroBE();
            objParametroBE = objParametroBC.obtenerTemaActual();
            return (objParametroBE);
        }

        [WebMethod]
        public void actualizarTemaActual(ParametroBE objParametroBE)
        {
            ParametroBC objParametroBC = new ParametroBC();
            objParametroBC.actualizarTemaActual(objParametroBE);
        }

        [WebMethod]
        public List<TalentoBE> ListarTalentos()
        {
            TalentoBC bc = new TalentoBC();
            return bc.ListarTalentos();
        }

        [WebMethod]
        public SkinBE SkinObtener(string descSkin)
        {
            SkinBC bc = new SkinBC();
            return bc.SkinObtener(descSkin);
        }

        [WebMethod]
        public SkinBE SkinActivoObtener()
        {
            SkinBC bc = new SkinBC();
            return bc.SkinActivoObtener();
        }

        [WebMethod]
        public bool SkinActualizar(SkinBE skin)
        {
            SkinBC bc = new SkinBC();
            return bc.SkinActualizar(skin);
        }

        [WebMethod]
        public int InsertarJuego(JuegoBE juego)
        {
            JuegoBC bc = new JuegoBC();
            return bc.InsertarJuego(juego);
        }

        [WebMethod]
        public int InsertarResultado(ResultadoBE resultado)
        {
            ResultadoBC bc = new ResultadoBC();

            try
            {
                return bc.InsertarResultado(resultado);
            }
            catch (Exception ex)
            {
                LogBC objLogBC = new LogBC();
                string mensaje = "Metodo: InsertarResultado. Mensaje: " + ex.Message;
                objLogBC.InsertarLog(mensaje);

                throw ex;
            }

        }

        [WebMethod]
        public int InsertarResultadoAdulto(ResultadoBE resultado)
        {
            ResultadoBC bc = new ResultadoBC();

            try
            {
                return bc.InsertarResultadoAdulto(resultado);
            }
            catch (Exception ex)
            {
                LogBC objLogBC = new LogBC();
                string mensaje = "Metodo: InsertarResultadoAdulto. Mensaje: " + ex.Message;
                objLogBC.InsertarLog(mensaje);

                throw ex;
            }

        }

        [WebMethod]
        public int ResultadoActualizar(ResultadoBE resultado)
        {
            ResultadoBC bc = new ResultadoBC();
            return bc.ResultadoActualizar(resultado);
        }

        [WebMethod]
        public List<RecomendacionBE> ObtenerRecomedacion(int idTalento)
        {
            RecomendacionBC bc = new RecomendacionBC();
            return bc.ObtenerRecomedacion(idTalento);
        }

        [WebMethod]
        public List<TalentoComplexBE> ListarTalentosComplex(int idTalento)
        {
            TalentoBC bc = new TalentoBC();
            return bc.ListarTalentosComplex(idTalento);
        }

        [WebMethod]
        public List<TendenciaBE> ListarTendencias()
        {
            TendenciaBC bc = new TendenciaBC();
            return bc.ListarTendencias();
        }

        [WebMethod]
        public bool ActualizarTalento(TalentoComplexBE objTalento)
        {
            TalentoBC bc = new TalentoBC();
            return bc.ActualizarTalento(objTalento);
        }

        [WebMethod]
        public List<TipoTalentoBE> ListarTipoTalento()
        {
            TipoTalentoBC bc = new TipoTalentoBC();
            return bc.ListarTipoTalento();
        }

        [WebMethod]
        public bool ActualizarTendencia(TendenciaBE objTendencia)
        {
            TendenciaBC bc = new TendenciaBC();
            return bc.ActualizarTendencia(objTendencia);
        }

        [WebMethod]
        public TendenciaBE ObtenerTendencia(int idTendencia)
        {
            TendenciaBC bc = new TendenciaBC();
            return bc.TendenciaObtener(idTendencia);
        }

        [WebMethod]
        public List<TalentoComplexBE> ListarVirtud(int idVirtud)
        {
            VirtudBC objVirtudBC = new VirtudBC();
            return objVirtudBC.ListarVirtudes(idVirtud);
        }

        [WebMethod]
        public bool ActualizarVirtud(TalentoComplexBE objVirtud)
        {
            VirtudBC objVirtudBC = new VirtudBC();
            return objVirtudBC.ActualizarVirtud(objVirtud);
        }

        [WebMethod]
        public bool InsertarParticipante(List<ParticipanteBE> lstParticipantes)
        {
            ParticipanteBC objParticipanteBC = new ParticipanteBC();
            bool resultado;

            try
            {
                resultado = objParticipanteBC.InsertarParticipante(lstParticipantes);
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return resultado;
        }

        [WebMethod]
        public int InsertarParticipanteUnico(ParticipanteBE objParticipanteBE)
        {
            int codigoParticipante = 0;
            ParticipanteBC objParticipanteBC = new ParticipanteBC();

            try
            {
                codigoParticipante = objParticipanteBC.InsertarParticipante(objParticipanteBE);
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return codigoParticipante;
        }

        [WebMethod]
        public int InsertarParticipanteAdultoUnico(ParticipanteBE objParticipanteBE)
        {
            int codigoParticipante = 0;
            ParticipanteBC objParticipanteBC = new ParticipanteBC();

            try
            {
                codigoParticipante = objParticipanteBC.InsertarParticipanteAdulto(objParticipanteBE);
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return codigoParticipante;
        }

        [WebMethod]
        public List<ParticipanteBE> ListarParticipantesMasivos()
        {
            ParticipanteBC objParticipanteBC = new ParticipanteBC();

            return objParticipanteBC.ListarParticipantesMasivos();
        }

        [WebMethod]
        public bool EnviarEmail(List<ParticipanteBE> lstParticipantes)
        {
            ParticipanteBC objParticipanteBC = new ParticipanteBC();

            return objParticipanteBC.EnviarEmail(lstParticipantes);
        }

        [WebMethod]
        public List<ResultadoParaleloBE> ResultadosParticipantesListar(string FechaInicio, string FechaFin, string Empresa)
        {
            ResultadoBC objResultadoBC = new ResultadoBC();

            return objResultadoBC.ResultadosParticipantesListar(FechaInicio, FechaFin, Empresa);
        }

        [WebMethod]
        public UsuarioBE InstanciaUsuario()
        {
            UsuarioBE objUsuarioBE = new UsuarioBE();

            return objUsuarioBE;
        }


        [WebMethod]
        public int InsertarResultadoTemporal(int idParticipante, string lstIDTalentos, int idBuzon)
        {
            ResultadoBC bc = new ResultadoBC();

            try
            {
                bc.InsertaResultadoTemp(idParticipante, lstIDTalentos, idBuzon);

            }
            catch (Exception ex)
            {
                LogBC objLogBC = new LogBC();
                string mensaje = "Metodo: InsertarResultado. Mensaje: " + ex.Message;
                objLogBC.InsertarLog(mensaje);

                throw ex;
            }

            return 0;
        }
    }
}
