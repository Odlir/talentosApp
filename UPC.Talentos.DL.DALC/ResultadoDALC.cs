using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using UPC.Talentos.BL.BE;
using System.Data;
using System.Data.SqlClient;

namespace UPC.Talentos.DL.DALC
{
    public class ResultadoDALC
    {
        //Esta clase permite almacenar el resultado de cada juego
        public int InsertarResultado(ResultadoBE objResultado)
        {
            String SqlInsertarResultado;
            SqlConnection Conn = null;
            string sCadena;
            SqlParameter prmParticipanteId;
            SqlParameter prmEsMasivo;
            SqlParameter prmNombreParticipante;
            SqlParameter prmDNI;
            SqlParameter prmCodEvaluacion;
            SqlParameter prmCorreo;
            SqlParameter prmCodigoResultado;
            SqlCommand cmdResultadoInsertar = null;
            int codigoResultado = 0;

            try
            {
                sCadena = Utilities.GetConnectionStringTalentos2();
                Conn = new SqlConnection(sCadena);

                SqlInsertarResultado = "uspi_TalResultado";

                cmdResultadoInsertar = Conn.CreateCommand();
                cmdResultadoInsertar.CommandType = CommandType.StoredProcedure;
                cmdResultadoInsertar.CommandText = SqlInsertarResultado;

                prmParticipanteId = new SqlParameter();
                prmParticipanteId.ParameterName = "@Participante_id";
                prmParticipanteId.SqlDbType = SqlDbType.Int;
                prmParticipanteId.Value = objResultado.Participante_id;

                prmEsMasivo = new SqlParameter();
                prmEsMasivo.ParameterName = "@EsMasivo";
                prmEsMasivo.SqlDbType = SqlDbType.Bit;
                prmEsMasivo.Value = objResultado.EsMasivo;

                prmNombreParticipante = new SqlParameter();
                prmNombreParticipante.ParameterName = "@NombreParticipante";
                prmNombreParticipante.SqlDbType = SqlDbType.VarChar;
                prmNombreParticipante.Size = 150;
                prmNombreParticipante.Value = objResultado.NombreParticipante;

                prmDNI = new SqlParameter();
                prmDNI.ParameterName = "@DNI";
                prmDNI.SqlDbType = SqlDbType.VarChar;
                prmDNI.Size = 8;
                prmDNI.Value = objResultado.DNI;

                prmCodEvaluacion = new SqlParameter();
                prmCodEvaluacion.ParameterName = "@CodEvaluacion";
                prmCodEvaluacion.SqlDbType = SqlDbType.VarChar;
                prmCodEvaluacion.Size = 10;
                prmCodEvaluacion.Value = objResultado.CodEvaluacion;

                prmCorreo = new SqlParameter();
                prmCorreo.ParameterName = "@Correo";
                prmCorreo.SqlDbType = SqlDbType.VarChar;
                prmCorreo.Size = 150;
                prmCorreo.Value = objResultado.CorreoElectronico;

                prmCodigoResultado = new SqlParameter();
                prmCodigoResultado.SqlDbType = SqlDbType.Int;
                prmCodigoResultado.Direction = ParameterDirection.ReturnValue;

                cmdResultadoInsertar.Parameters.Add(prmParticipanteId);
                cmdResultadoInsertar.Parameters.Add(prmEsMasivo);
                cmdResultadoInsertar.Parameters.Add(prmNombreParticipante);
                cmdResultadoInsertar.Parameters.Add(prmDNI);
                cmdResultadoInsertar.Parameters.Add(prmCodEvaluacion);
                cmdResultadoInsertar.Parameters.Add(prmCorreo);
                cmdResultadoInsertar.Parameters.Add(prmCodigoResultado);

                cmdResultadoInsertar.Connection.Open();
                cmdResultadoInsertar.ExecuteNonQuery();

                //SqlHelper.ExecuteNonQuery(sCadena, CommandType.StoredProcedure, SqlInsertarResultado, arrParameter);
                codigoResultado = Convert.ToInt32(prmCodigoResultado.Value);

                return codigoResultado;
            }
            catch (Exception ex)
            {
                cmdResultadoInsertar.Connection.Close();
                cmdResultadoInsertar.Dispose();
                throw;
            }
        }

        public int InsertarResultadoAdulto(ResultadoBE objResultado)
        {
            String SqlInsertarResultado;
            SqlConnection Conn = null;
            string sCadena;
            SqlParameter prmParticipanteId;
            SqlParameter prmEsMasivo;
            SqlParameter prmNombreParticipante;
            SqlParameter prmDNI;
            SqlParameter prmCodEvaluacion;
            SqlParameter prmCorreo;
            SqlParameter prmCodigoResultado;
            SqlCommand cmdResultadoInsertar = null;
            int codigoResultado = 0;

            try
            {
                sCadena = Utilities.GetConnectionStringTalentos2();
                Conn = new SqlConnection(sCadena);

                SqlInsertarResultado = "uspi_TalResultadoAdulto";

                cmdResultadoInsertar = Conn.CreateCommand();
                cmdResultadoInsertar.CommandType = CommandType.StoredProcedure;
                cmdResultadoInsertar.CommandText = SqlInsertarResultado;

                prmParticipanteId = new SqlParameter();
                prmParticipanteId.ParameterName = "@Participante_id";
                prmParticipanteId.SqlDbType = SqlDbType.Int;
                prmParticipanteId.Value = objResultado.Participante_id;

                prmEsMasivo = new SqlParameter();
                prmEsMasivo.ParameterName = "@EsMasivo";
                prmEsMasivo.SqlDbType = SqlDbType.Bit;
                prmEsMasivo.Value = objResultado.EsMasivo;

                prmNombreParticipante = new SqlParameter();
                prmNombreParticipante.ParameterName = "@NombreParticipante";
                prmNombreParticipante.SqlDbType = SqlDbType.VarChar;
                prmNombreParticipante.Size = 150;
                prmNombreParticipante.Value = objResultado.NombreParticipante;

                prmDNI = new SqlParameter();
                prmDNI.ParameterName = "@DNI";
                prmDNI.SqlDbType = SqlDbType.VarChar;
                prmDNI.Size = 8;
                prmDNI.Value = objResultado.DNI;

                prmCodEvaluacion = new SqlParameter();
                prmCodEvaluacion.ParameterName = "@CodEvaluacion";
                prmCodEvaluacion.SqlDbType = SqlDbType.VarChar;
                prmCodEvaluacion.Size = 10;
                prmCodEvaluacion.Value = objResultado.CodEvaluacion;

                prmCorreo = new SqlParameter();
                prmCorreo.ParameterName = "@Correo";
                prmCorreo.SqlDbType = SqlDbType.VarChar;
                prmCorreo.Size = 150;
                prmCorreo.Value = objResultado.CorreoElectronico;

                prmCodigoResultado = new SqlParameter();
                prmCodigoResultado.SqlDbType = SqlDbType.Int;
                prmCodigoResultado.Direction = ParameterDirection.ReturnValue;

                cmdResultadoInsertar.Parameters.Add(prmParticipanteId);
                cmdResultadoInsertar.Parameters.Add(prmEsMasivo);
                cmdResultadoInsertar.Parameters.Add(prmNombreParticipante);
                cmdResultadoInsertar.Parameters.Add(prmDNI);
                cmdResultadoInsertar.Parameters.Add(prmCodEvaluacion);
                cmdResultadoInsertar.Parameters.Add(prmCorreo);
                cmdResultadoInsertar.Parameters.Add(prmCodigoResultado);

                cmdResultadoInsertar.Connection.Open();
                cmdResultadoInsertar.ExecuteNonQuery();

                //SqlHelper.ExecuteNonQuery(sCadena, CommandType.StoredProcedure, SqlInsertarResultado, arrParameter);
                codigoResultado = Convert.ToInt32(prmCodigoResultado.Value);

                return codigoResultado;
            }
            catch (Exception ex)
            {
                cmdResultadoInsertar.Connection.Close();
                cmdResultadoInsertar.Dispose();
                throw;
            }
        }

        /*
        public int ResultadoActualizar(ResultadoBE objResultado)
        {
            
            SqlParameter prmResultadoId;
            SqlParameter prmTalentoId;
            SqlParameter prmBuzonId;
            SqlParameter prmSeleccionado;
            SqlParameter prmPuntaje;
            SqlParameter prmFecha;
            SqlParameter prmParticipanteId;
            SqlDataReader drResultado;
            SqlCommand cmdResultadoActualizar = null;
            SqlConnection Conn = null;
            
            int success = 0;
            String SqlActualizarResultado;
            string sCadena;
            string err = "";
            try
            {
                sCadena = Utilities.GetConnectionStringTalentos2();

                SqlActualizarResultado = "uspu_TalResultado";

                Conn = new SqlConnection(sCadena);

                cmdResultadoActualizar = Conn.CreateCommand();
                cmdResultadoActualizar.CommandType = CommandType.StoredProcedure;
                cmdResultadoActualizar.CommandText = SqlActualizarResultado;


                prmResultadoId = new SqlParameter();
                prmResultadoId.ParameterName = "@Resultado_id";
                prmResultadoId.SqlDbType = SqlDbType.Int;
                prmResultadoId.Value = objResultado.Resultado_id;

                prmTalentoId = new SqlParameter();
                prmTalentoId.ParameterName = "@Talento_id";
                prmTalentoId.SqlDbType = SqlDbType.NVarChar;
                prmTalentoId.Size = 280;
                prmTalentoId.Value = objResultado.TalentoId;

                prmBuzonId = new SqlParameter();
                prmBuzonId.ParameterName = "@Buzon_id";
                prmBuzonId.SqlDbType = SqlDbType.NVarChar;
                prmBuzonId.Size = 250;
                prmBuzonId.Value = objResultado.BuzonId;

                prmSeleccionado = new SqlParameter();
                prmSeleccionado.ParameterName = "@Seleccionado";
                prmSeleccionado.SqlDbType = SqlDbType.NVarChar;
                prmSeleccionado.Size = 250;
                prmSeleccionado.Value = objResultado.Seleccionado;

                prmPuntaje = new SqlParameter();
                prmPuntaje.ParameterName = "@Puntaje";
                prmPuntaje.SqlDbType = SqlDbType.NVarChar;
                prmPuntaje.Size = 150;
                prmPuntaje.Value = objResultado.Puntaje;

                prmFecha = new SqlParameter();
                prmFecha.ParameterName = "@Fecha";
                prmFecha.SqlDbType = SqlDbType.DateTime;
                prmFecha.Value = objResultado.Fecha;

                prmParticipanteId = new SqlParameter();
                prmParticipanteId.ParameterName = "@Participante_id";
                prmParticipanteId.SqlDbType = SqlDbType.Int;
                prmParticipanteId.Value = objResultado.Participante_id;

                cmdResultadoActualizar.Parameters.Add(prmResultadoId);
                cmdResultadoActualizar.Parameters.Add(prmTalentoId);
                cmdResultadoActualizar.Parameters.Add(prmBuzonId);
                cmdResultadoActualizar.Parameters.Add(prmSeleccionado);
                cmdResultadoActualizar.Parameters.Add(prmPuntaje);
                cmdResultadoActualizar.Parameters.Add(prmFecha);
                cmdResultadoActualizar.Parameters.Add(prmParticipanteId);


                cmdResultadoActualizar.Connection.Open();
                drResultado = cmdResultadoActualizar.ExecuteReader();

                if(drResultado.Read())
                {
                    success = drResultado.GetInt32(0);
                }

                cmdResultadoActualizar.Connection.Close();
                cmdResultadoActualizar.Dispose();
                Conn.Dispose();
                return success;

            }
            catch (Exception ex)
            {
                
                cmdResultadoActualizar.Dispose();
                Conn.Dispose();

                throw;
            }

            
            
        }
         */


        public int ResultadoActualizar(ResultadoBE objResultado)
        {
            String SqlActualizarResultado;
            string sCadena;


            try
            {
                sCadena = Utilities.GetConnectionStringTalentos2();

                SqlActualizarResultado = "uspu_TalResultado";

                SqlParameter[] arrParameter = new SqlParameter[8];

                arrParameter[0] = new SqlParameter();
                arrParameter[0].ParameterName = "@Buzon_id";
                arrParameter[0].SqlDbType = SqlDbType.NVarChar;
                arrParameter[0].Size = 250;
                arrParameter[0].Value = objResultado.BuzonId;

                arrParameter[1] = new SqlParameter();
                arrParameter[1].ParameterName = "@Talento_id";
                arrParameter[1].SqlDbType = SqlDbType.NVarChar;
                arrParameter[1].Size = 280;
                arrParameter[1].Value = objResultado.TalentoId;

                arrParameter[2] = new SqlParameter();
                arrParameter[2].ParameterName = "@Seleccionado";
                arrParameter[2].SqlDbType = SqlDbType.NVarChar;
                arrParameter[2].Size = 250;
                arrParameter[2].Value = objResultado.Seleccionado;

                arrParameter[3] = new SqlParameter();
                arrParameter[3].ParameterName = "@Puntaje";
                arrParameter[3].SqlDbType = SqlDbType.NVarChar;
                arrParameter[3].Size = 150;
                arrParameter[3].Value = objResultado.Puntaje;

                arrParameter[4] = new SqlParameter();
                arrParameter[4].ParameterName = "@Fecha";
                arrParameter[4].SqlDbType = SqlDbType.DateTime;
                arrParameter[4].Value = objResultado.Fecha;

                arrParameter[5] = new SqlParameter();
                arrParameter[5].ParameterName = "@Participante_id";
                arrParameter[5].SqlDbType = SqlDbType.Int;
                arrParameter[5].Value = objResultado.Participante_id;

                arrParameter[6] = new SqlParameter();
                arrParameter[6].ParameterName = "@Resultado_id";
                arrParameter[6].SqlDbType = SqlDbType.Int;
                arrParameter[6].Value = objResultado.Resultado_id;

                arrParameter[7] = new SqlParameter();
                arrParameter[7].SqlDbType = SqlDbType.Int;
                arrParameter[7].Direction = ParameterDirection.ReturnValue;

                SqlHelper.ExecuteNonQuery(sCadena, CommandType.StoredProcedure, SqlActualizarResultado, arrParameter);
                return (int)arrParameter[7].Value;
            }
            catch (Exception ex)
            {
                throw;
            }
        }


        public int ResultadoActualizarAdulto(ResultadoBE objResultado)
        {
            int success = 0;
            String SqlActualizarResultado;
            string sCadena;
            string err = "";

            try
            {
                sCadena = Utilities.GetConnectionStringTalentos2();

                SqlActualizarResultado = "uspu_TalResultadoAdulto";

                SqlParameter[] arrParameter = new SqlParameter[8];

                arrParameter[0] = new SqlParameter();
                arrParameter[0].ParameterName = "@Buzon_id";
                arrParameter[0].SqlDbType = SqlDbType.NVarChar;
                arrParameter[0].Size = 250;
                arrParameter[0].Value = objResultado.BuzonId;

                arrParameter[1] = new SqlParameter();
                arrParameter[1].ParameterName = "@Talento_id";
                arrParameter[1].SqlDbType = SqlDbType.NVarChar;
                arrParameter[1].Size = 280;
                arrParameter[1].Value = objResultado.TalentoId;

                arrParameter[2] = new SqlParameter();
                arrParameter[2].ParameterName = "@Seleccionado";
                arrParameter[2].SqlDbType = SqlDbType.NVarChar;
                arrParameter[2].Size = 250;
                arrParameter[2].Value = objResultado.Seleccionado;

                arrParameter[3] = new SqlParameter();
                arrParameter[3].ParameterName = "@Puntaje";
                arrParameter[3].SqlDbType = SqlDbType.NVarChar;
                arrParameter[3].Size = 150;
                arrParameter[3].Value = objResultado.Puntaje;

                arrParameter[4] = new SqlParameter();
                arrParameter[4].ParameterName = "@Fecha";
                arrParameter[4].SqlDbType = SqlDbType.DateTime;
                arrParameter[4].Value = objResultado.Fecha;

                arrParameter[5] = new SqlParameter();
                arrParameter[5].ParameterName = "@Participante_id";
                arrParameter[5].SqlDbType = SqlDbType.Int;
                arrParameter[5].Value = objResultado.Participante_id;

                arrParameter[6] = new SqlParameter();
                arrParameter[6].ParameterName = "@Resultado_id";
                arrParameter[6].SqlDbType = SqlDbType.Int;
                arrParameter[6].Value = objResultado.Resultado_id;

                /*
                SqlHelper.ExecuteNonQuery(sCadena, CommandType.StoredProcedure, SqlActualizarResultado, arrParameter);
                return (int)arrParameter[6].Value;
                */
                arrParameter[7] = new SqlParameter();
                arrParameter[7].SqlDbType = SqlDbType.Int;
                arrParameter[7].Direction = ParameterDirection.ReturnValue;

                SqlHelper.ExecuteNonQuery(sCadena, CommandType.StoredProcedure, SqlActualizarResultado, arrParameter);
                //return (int)arrParameter[6].Value;
                success = Convert.ToInt32(arrParameter[7].Value);
                return success;
            }
            catch (Exception ex)
            {
                err = "" + ex.Message;
                return success;
            }
        }

        public List<CuadroResultadoBE> CuadroResultadoListar()
        {
            SqlConnection Conn = null;
            SqlCommand cmdCuadroResultadoListar = null;
            SqlDataReader drCuadroResultado;
            String sCadenaConexion;
            String sqlCuadroResultadoListar;

            try
            {
                sCadenaConexion = Utilities.GetConnectionStringTalentos2();

                sqlCuadroResultadoListar = "usp_TalCuadroReporteListar";

                Conn = new SqlConnection(sCadenaConexion);

                cmdCuadroResultadoListar = Conn.CreateCommand();
                cmdCuadroResultadoListar.CommandType = CommandType.StoredProcedure;
                cmdCuadroResultadoListar.CommandText = sqlCuadroResultadoListar;

                List<CuadroResultadoBE> lstCuadroResultado;
                CuadroResultadoBE objCuadroResultadoBE;

                cmdCuadroResultadoListar.Connection.Open();
                drCuadroResultado = cmdCuadroResultadoListar.ExecuteReader();

                lstCuadroResultado = new List<CuadroResultadoBE>();

                while (drCuadroResultado.Read())
                {
                    objCuadroResultadoBE = new CuadroResultadoBE();
                    objCuadroResultadoBE.CuadroResultado_Id = drCuadroResultado.GetInt32(drCuadroResultado.GetOrdinal("CuadroReporte_Id"));
                    objCuadroResultadoBE.Texto = drCuadroResultado.GetString(drCuadroResultado.GetOrdinal("Texto"));
                    objCuadroResultadoBE.Talento_Id = drCuadroResultado.GetInt32(drCuadroResultado.GetOrdinal("Talento_Id"));
                    objCuadroResultadoBE.Talento = drCuadroResultado.GetString(drCuadroResultado.GetOrdinal("Talento"));
                    objCuadroResultadoBE.Tendencia_Id = drCuadroResultado.GetInt32(drCuadroResultado.GetOrdinal("Tendencia_Id"));
                    objCuadroResultadoBE.Tendencia = drCuadroResultado.GetString(drCuadroResultado.GetOrdinal("Tendencia"));
                    objCuadroResultadoBE.TipoTalento_Id = drCuadroResultado.GetInt32(drCuadroResultado.GetOrdinal("TipoTalento_Id"));

                    lstCuadroResultado.Add(objCuadroResultadoBE);
                }

                cmdCuadroResultadoListar.Connection.Close();
                cmdCuadroResultadoListar.Dispose();
                Conn.Dispose();

                return lstCuadroResultado;
            }
            catch (Exception ex)
            {
                Conn.Dispose();
                cmdCuadroResultadoListar.Dispose();

                throw;
            }
        }

        public List<ResultadoParaleloBE> ResultadosParticipantesListar(string FechaInicio, string FechaFin, string Empresa)
        {
            SqlConnection Conn = null;
            SqlCommand cmdCuadroResultadoListar = null;
            SqlDataReader drResultado;
            String sCadenaConexion;
            String sqlCuadroResultadoListar;
            SqlParameter prmFechaInicio;
            SqlParameter prmFechaFin;
            SqlParameter prmEmpresa;

            try
            {
                sCadenaConexion = Utilities.GetConnectionStringTalentos2();

                sqlCuadroResultadoListar = "usp_TalResultadoListar";

                Conn = new SqlConnection(sCadenaConexion);

                cmdCuadroResultadoListar = Conn.CreateCommand();
                cmdCuadroResultadoListar.CommandType = CommandType.StoredProcedure;
                cmdCuadroResultadoListar.CommandText = sqlCuadroResultadoListar;

                prmFechaInicio = new SqlParameter("@FechaInicio", FechaInicio);
                prmFechaInicio.SqlDbType = SqlDbType.VarChar;
                prmFechaInicio.Size = 8;

                prmFechaFin = new SqlParameter("@FechaFin", FechaFin);
                prmFechaFin.SqlDbType = SqlDbType.VarChar;
                prmFechaFin.Size = 8;

                prmEmpresa = new SqlParameter("@Empresa", Empresa);
                prmEmpresa.SqlDbType = SqlDbType.VarChar;
                prmEmpresa.Size = 150;

                cmdCuadroResultadoListar.Parameters.Add(prmFechaInicio);
                cmdCuadroResultadoListar.Parameters.Add(prmFechaFin);
                cmdCuadroResultadoListar.Parameters.Add(prmEmpresa);

                cmdCuadroResultadoListar.Connection.Open();
                drResultado = cmdCuadroResultadoListar.ExecuteReader();

                List<ResultadoParaleloBE> lstResultado;
                ResultadoParaleloBE objResultadoParaleloBE;

                lstResultado = new List<ResultadoParaleloBE>();

                while (drResultado.Read())
                {
                    objResultadoParaleloBE = new ResultadoParaleloBE();
                    objResultadoParaleloBE.Resultado_Id = drResultado.GetInt32(drResultado.GetOrdinal("Resultado_Id"));
                    objResultadoParaleloBE.Participante_Id = drResultado.GetInt32(drResultado.GetOrdinal("Participante_Id"));
                    objResultadoParaleloBE.Nombres = drResultado.GetString(drResultado.GetOrdinal("Nombres"));
                    objResultadoParaleloBE.ApellidoPaterno = drResultado.GetString(drResultado.GetOrdinal("ApellidoPaterno"));
                    objResultadoParaleloBE.ApellidoMaterno = drResultado.GetString(drResultado.GetOrdinal("ApellidoMaterno"));
                    objResultadoParaleloBE.DNI = drResultado.GetString(drResultado.GetOrdinal("DNI"));
                    objResultadoParaleloBE.CorreoElectronico = drResultado.GetString(drResultado.GetOrdinal("CorreoElectronico"));
                    objResultadoParaleloBE.CodigoEvaluacion = drResultado.GetString(drResultado.GetOrdinal("CodEvaluacion"));
                    objResultadoParaleloBE.Fecha = drResultado.GetString(drResultado.GetOrdinal("Fecha"));
                    objResultadoParaleloBE.EsMasivo = drResultado.GetBoolean(drResultado.GetOrdinal("EsMasivo"));
                    objResultadoParaleloBE.Empresa = drResultado.GetString(drResultado.GetOrdinal("Empresa"));

                    lstResultado.Add(objResultadoParaleloBE);
                }

                cmdCuadroResultadoListar.Connection.Close();
                cmdCuadroResultadoListar.Dispose();
                Conn.Dispose();

                return lstResultado;
            }
            catch (Exception ex)
            {
                Conn.Dispose();
                cmdCuadroResultadoListar.Dispose();

                throw;
            }
        }

        public List<ResultadoParaleloBE> ResultadosParticipantesImportar(string FechaInicio, string FechaFin, string Empresa)
        {
            SqlConnection Conn = null;
            SqlCommand cmdCuadroResultadoListar = null;
            SqlDataReader drResultado;
            String sCadenaConexion;
            String sqlCuadroResultadoListar;
            SqlParameter prmFechaInicio;
            SqlParameter prmFechaFin;
            SqlParameter prmEmpresa;

            try
            {
                sCadenaConexion = Utilities.GetConnectionStringTalentos2();

                sqlCuadroResultadoListar = "usp_TalResultadoImportar";

                Conn = new SqlConnection(sCadenaConexion);

                cmdCuadroResultadoListar = Conn.CreateCommand();
                cmdCuadroResultadoListar.CommandType = CommandType.StoredProcedure;
                cmdCuadroResultadoListar.CommandText = sqlCuadroResultadoListar;

                prmFechaInicio = new SqlParameter("@FechaInicio", FechaInicio);
                prmFechaInicio.SqlDbType = SqlDbType.VarChar;
                prmFechaInicio.Size = 8;

                prmFechaFin = new SqlParameter("@FechaFin", FechaFin);
                prmFechaFin.SqlDbType = SqlDbType.VarChar;
                prmFechaFin.Size = 8;

                prmEmpresa = new SqlParameter("@Empresa", Empresa);
                prmEmpresa.SqlDbType = SqlDbType.VarChar;
                prmEmpresa.Size = 150;

                cmdCuadroResultadoListar.Parameters.Add(prmFechaInicio);
                cmdCuadroResultadoListar.Parameters.Add(prmFechaFin);
                cmdCuadroResultadoListar.Parameters.Add(prmEmpresa);

                cmdCuadroResultadoListar.Connection.Open();
                drResultado = cmdCuadroResultadoListar.ExecuteReader();

                List<ResultadoParaleloBE> lstResultado;
                ResultadoParaleloBE objResultadoParaleloBE;

                lstResultado = new List<ResultadoParaleloBE>();

                while (drResultado.Read())
                {
                    objResultadoParaleloBE = new ResultadoParaleloBE();
                    objResultadoParaleloBE.Nombres = drResultado.GetString(drResultado.GetOrdinal("Nombres"));
                    objResultadoParaleloBE.ApellidoPaterno = drResultado.GetString(drResultado.GetOrdinal("ApellidoPaterno"));
                    objResultadoParaleloBE.ApellidoMaterno = drResultado.GetString(drResultado.GetOrdinal("ApellidoMaterno"));
                    objResultadoParaleloBE.DNI = drResultado.GetString(drResultado.GetOrdinal("DNI"));
                    objResultadoParaleloBE.Sexo = drResultado.GetString(drResultado.GetOrdinal("Sexo"));
                    objResultadoParaleloBE.FechaNacimiento = drResultado.GetString(drResultado.GetOrdinal("FechaNacimiento"));
                    objResultadoParaleloBE.NivelInstruccion = drResultado.GetString(drResultado.GetOrdinal("NivelInstruccion"));
                    objResultadoParaleloBE.CargoEmpresa = drResultado.GetString(drResultado.GetOrdinal("CargoEmpresa"));
                    objResultadoParaleloBE.Empresa = drResultado.GetString(drResultado.GetOrdinal("Empresa"));
                    objResultadoParaleloBE.CorreoElectronico = drResultado.GetString(drResultado.GetOrdinal("CorreoElectronico"));
                    objResultadoParaleloBE.MasDesarrollados = drResultado.GetString(drResultado.GetOrdinal("MasDesarrollados"));
                    objResultadoParaleloBE.MenosDesarrollados = drResultado.GetString(drResultado.GetOrdinal("MenosDesarrollados"));
                    objResultadoParaleloBE.TalentosEspecificos = drResultado.GetString(drResultado.GetOrdinal("TalentosEspecificos"));
                    objResultadoParaleloBE.Virtudes = drResultado.GetString(drResultado.GetOrdinal("Virtudes"));
                    objResultadoParaleloBE.Buzon_Id = drResultado.GetInt32(drResultado.GetOrdinal("Buzon_Id"));

                    lstResultado.Add(objResultadoParaleloBE);
                }

                cmdCuadroResultadoListar.Connection.Close();
                cmdCuadroResultadoListar.Dispose();
                Conn.Dispose();

                return lstResultado;
            }
            catch (Exception ex)
            {
                Conn.Dispose();
                cmdCuadroResultadoListar.Dispose();

                throw;
            }
        }

        public List<TalentoComplexBE> ObtenerResultadoParticipante(string DNI)
        {
            SqlConnection Conn = null;
            SqlCommand cmdResultadoListar = null;
            SqlDataReader drResultado;
            String sCadenaConexion;
            String sqlResultadoListar;
            SqlParameter prmDNI;

            try
            {
                sCadenaConexion = Utilities.GetConnectionStringTalentos2();

                sqlResultadoListar = "usp_TalResultadoReporte";

                Conn = new SqlConnection(sCadenaConexion);

                cmdResultadoListar = Conn.CreateCommand();
                cmdResultadoListar.CommandType = CommandType.StoredProcedure;
                cmdResultadoListar.CommandText = sqlResultadoListar;

                prmDNI = new SqlParameter("@DNI", DNI);
                prmDNI.SqlDbType = SqlDbType.VarChar;
                prmDNI.Size = 10;

                cmdResultadoListar.Parameters.Add(prmDNI);

                cmdResultadoListar.Connection.Open();
                drResultado = cmdResultadoListar.ExecuteReader();

                List<TalentoComplexBE> lstResultado;
                TalentoComplexBE objTalentoComplejoBE;

                lstResultado = new List<TalentoComplexBE>();

                while (drResultado.Read())
                {
                    objTalentoComplejoBE = new TalentoComplexBE();
                    objTalentoComplejoBE.idTalento = drResultado.GetInt32(drResultado.GetOrdinal("Talento_Id"));
                    objTalentoComplejoBE.nombre = drResultado.GetString(drResultado.GetOrdinal("Talento"));
                    objTalentoComplejoBE.Buzon_Id = drResultado.GetInt32(drResultado.GetOrdinal("Buzon_Id"));
                    objTalentoComplejoBE.Seleccionado = drResultado.GetBoolean(drResultado.GetOrdinal("Seleccionado"));
                    objTalentoComplejoBE.idTendencia = drResultado.GetInt32(drResultado.GetOrdinal("Tendencia_Id"));
                    objTalentoComplejoBE.tendencia = drResultado.GetString(drResultado.GetOrdinal("Tendencia"));
                    objTalentoComplejoBE.idTipoTalento = drResultado.GetInt32(drResultado.GetOrdinal("TipoTalento_Id"));

                    lstResultado.Add(objTalentoComplejoBE);
                }

                cmdResultadoListar.Connection.Close();
                cmdResultadoListar.Dispose();
                Conn.Dispose();

                return lstResultado;
            }
            catch (Exception ex)
            {
                Conn.Dispose();
                cmdResultadoListar.Dispose();

                throw;
            }
        }


        public List<TalentoComplexBE> ObtenerResultadoParticipanteAdulto(string DNI)
        {
            SqlConnection Conn = null;
            SqlCommand cmdResultadoListar = null;
            SqlDataReader drResultado;
            String sCadenaConexion;
            String sqlResultadoListar;
            SqlParameter prmDNI;

            try
            {
                sCadenaConexion = Utilities.GetConnectionStringTalentos2();

                sqlResultadoListar = "usp_TalResultadoAdultoReporte"; 

                Conn = new SqlConnection(sCadenaConexion);

                cmdResultadoListar = Conn.CreateCommand();
                cmdResultadoListar.CommandType = CommandType.StoredProcedure;
                cmdResultadoListar.CommandText = sqlResultadoListar;

                prmDNI = new SqlParameter("@DNI", DNI);
                prmDNI.SqlDbType = SqlDbType.VarChar;
                prmDNI.Size = 10;

                cmdResultadoListar.Parameters.Add(prmDNI);

                cmdResultadoListar.Connection.Open();
                drResultado = cmdResultadoListar.ExecuteReader();

                List<TalentoComplexBE> lstResultado;
                TalentoComplexBE objTalentoComplejoBE;

                lstResultado = new List<TalentoComplexBE>();

                while (drResultado.Read())
                {
                    objTalentoComplejoBE = new TalentoComplexBE();
                    objTalentoComplejoBE.idTalento = drResultado.GetInt32(drResultado.GetOrdinal("Talento_Id"));
                    objTalentoComplejoBE.nombre = drResultado.GetString(drResultado.GetOrdinal("Talento"));
                    objTalentoComplejoBE.Buzon_Id = drResultado.GetInt32(drResultado.GetOrdinal("Buzon_Id"));
                    objTalentoComplejoBE.Seleccionado = drResultado.GetBoolean(drResultado.GetOrdinal("Seleccionado"));
                    objTalentoComplejoBE.idTendencia = drResultado.GetInt32(drResultado.GetOrdinal("Tendencia_Id"));
                    objTalentoComplejoBE.tendencia = drResultado.GetString(drResultado.GetOrdinal("Tendencia"));
                    objTalentoComplejoBE.idTipoTalento = drResultado.GetInt32(drResultado.GetOrdinal("TipoTalento_Id"));

                    lstResultado.Add(objTalentoComplejoBE);
                }

                cmdResultadoListar.Connection.Close();
                cmdResultadoListar.Dispose();
                Conn.Dispose();

                return lstResultado;
            }
            catch (Exception ex)
            {
                Conn.Dispose();
                cmdResultadoListar.Dispose();

                throw;
            }
        }

        public int InsertaResultadoTemp(int idParticipante, string lstIdTalento, int idBuzon)
        {
            String SqlInsertarResultado;
            SqlConnection Conn = null;
            string sCadena;

            SqlParameter prmIdParticipante;
            SqlParameter prmIdTalento;
            SqlParameter prmIdBuzon;

            SqlCommand cmdResultadoInsertar = null;
            int codigoResultado = 0;

            try
            {
                sCadena = Utilities.GetConnectionStringTalentos2();
                Conn = new SqlConnection(sCadena);

                SqlInsertarResultado = "USPI_TALENTOS_SELECCIONADOS";

                cmdResultadoInsertar = Conn.CreateCommand();
                cmdResultadoInsertar.CommandType = CommandType.StoredProcedure;
                cmdResultadoInsertar.CommandText = SqlInsertarResultado;

                prmIdParticipante = new SqlParameter();
                prmIdParticipante.ParameterName = "@id_participante";
                prmIdParticipante.SqlDbType = SqlDbType.Int;
                prmIdParticipante.Value = idParticipante;

                prmIdTalento = new SqlParameter();
                prmIdTalento.ParameterName = "@talento_id";
                prmIdTalento.SqlDbType = SqlDbType.VarChar;
                prmIdTalento.Size = 50;
                prmIdTalento.Value = lstIdTalento;

                prmIdBuzon = new SqlParameter();
                prmIdBuzon.ParameterName = "@buzon_id";
                prmIdBuzon.SqlDbType = SqlDbType.Int;
                prmIdBuzon.Value = idBuzon;


                cmdResultadoInsertar.Parameters.Add(prmIdParticipante);
                cmdResultadoInsertar.Parameters.Add(prmIdTalento);
                cmdResultadoInsertar.Parameters.Add(prmIdBuzon);

                cmdResultadoInsertar.Connection.Open();
                cmdResultadoInsertar.ExecuteNonQuery();

                return codigoResultado;
            }
            catch (Exception ex)
            {
                cmdResultadoInsertar.Connection.Close();
                cmdResultadoInsertar.Dispose();
                throw;
            }
        }


        public string ObtenerFechaTestxCodigo(string codigoEvaluacion)
        {
            SqlConnection Conn = null;
            SqlCommand cmdresultado = null;
            SqlDataReader rdResultado;
            String sCadenaConexion;
            String sqlResultado;
            SqlParameter prmCodigoEvaluacion;
            string fecha = "";

            try
            {
                sCadenaConexion = Utilities.GetConnectionStringTalentos2();

                sqlResultado = "usp_TalObtenerFechaTestxCodigo";

                Conn = new SqlConnection(sCadenaConexion);

                cmdresultado = Conn.CreateCommand();
                cmdresultado.CommandType = CommandType.StoredProcedure;
                cmdresultado.CommandText = sqlResultado;

                prmCodigoEvaluacion = new SqlParameter();
                prmCodigoEvaluacion.ParameterName = "@CodEvaluacion";
                prmCodigoEvaluacion.SqlDbType = System.Data.SqlDbType.VarChar;
                prmCodigoEvaluacion.Size = 10;
                prmCodigoEvaluacion.Value = codigoEvaluacion;

                cmdresultado.Parameters.Add(prmCodigoEvaluacion);

                cmdresultado.Connection.Open();
                rdResultado = cmdresultado.ExecuteReader();

                if (rdResultado.Read())
                {
                    fecha = rdResultado.GetString(rdResultado.GetOrdinal("fecha_test"));
                }

                cmdresultado.Connection.Close();
                cmdresultado.Dispose();
                Conn.Dispose();
                return fecha;
            }
            catch (Exception ex)
            {
                Conn.Dispose();
                cmdresultado.Dispose();

                throw;
            }
        }


        public string ObtenerFechaTestAdultoxCodigo(string codigoEvaluacion)
        {
            SqlConnection Conn = null;
            SqlCommand cmdresultado = null;
            SqlDataReader rdResultado;
            String sCadenaConexion;
            String sqlResultado;
            SqlParameter prmCodigoEvaluacion;
            string fecha = "";

            try
            {
                sCadenaConexion = Utilities.GetConnectionStringTalentos2();

                sqlResultado = "usp_TalObtenerFechaTestAdultoxCodigo";

                Conn = new SqlConnection(sCadenaConexion);

                cmdresultado = Conn.CreateCommand();
                cmdresultado.CommandType = CommandType.StoredProcedure;
                cmdresultado.CommandText = sqlResultado;

                prmCodigoEvaluacion = new SqlParameter();
                prmCodigoEvaluacion.ParameterName = "@CodEvaluacion";
                prmCodigoEvaluacion.SqlDbType = System.Data.SqlDbType.VarChar;
                prmCodigoEvaluacion.Size = 10;
                prmCodigoEvaluacion.Value = codigoEvaluacion;

                cmdresultado.Parameters.Add(prmCodigoEvaluacion);

                cmdresultado.Connection.Open();
                rdResultado = cmdresultado.ExecuteReader();

                if (rdResultado.Read())
                {
                    fecha = rdResultado.GetString(rdResultado.GetOrdinal("fecha_test"));
                }

                cmdresultado.Connection.Close();
                cmdresultado.Dispose();
                Conn.Dispose();
                return fecha;
            }
            catch (Exception ex)
            {
                Conn.Dispose();
                cmdresultado.Dispose();

                throw;
            }
        }


        /** Reporte Compatibilidad **/
        public List<ResultadoComFacultadBE> ObtenerResultadoCompatibilidadXFacultad(string codUser, double ptsExtroIntro, double ptsIntuicion_Sensacion, double ptsRacional_Emotivo, double ptsOrganizadoCasual)
        {
            SqlConnection Conn = null;
            SqlCommand cmdResultadoListar = null;
            SqlDataReader drResultado;
            String sCadenaConexion;
            String sqlStoredProcedure;
            SqlParameter prmCodUser;
            SqlParameter prmPtsExtroIntro;
            SqlParameter prmPtsIntuicion_Sensacion;
            SqlParameter prmPtsRacional_Emotivo;
            SqlParameter prmPtsOrganizadoCasual;

            try
            {

                /*
                 @CodEvaluacion VARCHAR(50),
                @PtsExtroIntro DECIMAL(5,2),
                @PtsRacionalEmocional DECIMAL(5,2),
                @PtsOrganizadoCasual DECIMAL(5,2),
                @PtsIntuicionSensorial DECIMAL(5,2) 
                 */
                sCadenaConexion = Utilities.GetConnectionStringTalentos2();

                sqlStoredProcedure = "uspGetCompatibilidadxFacultad";

                Conn = new SqlConnection(sCadenaConexion);

                cmdResultadoListar = Conn.CreateCommand();
                cmdResultadoListar.CommandType = CommandType.StoredProcedure;
                cmdResultadoListar.CommandText = sqlStoredProcedure;

                prmCodUser = new SqlParameter("@CodEvaluacion", codUser);
                prmCodUser.SqlDbType = SqlDbType.VarChar;
                prmCodUser.Size = 50;

                prmPtsExtroIntro = new SqlParameter("@PtsExtroIntro", ptsExtroIntro);
                prmPtsExtroIntro.SqlDbType = SqlDbType.Decimal;
                prmPtsExtroIntro.Precision = 5;
                prmPtsExtroIntro.Scale = 2;

                prmPtsRacional_Emotivo = new SqlParameter("@PtsRacionalEmocional", ptsRacional_Emotivo);
                prmPtsRacional_Emotivo.SqlDbType = SqlDbType.Decimal;
                prmPtsRacional_Emotivo.Precision = 5;
                prmPtsRacional_Emotivo.Scale = 2;

                prmPtsOrganizadoCasual = new SqlParameter("@PtsOrganizadoCasual", ptsOrganizadoCasual);
                prmPtsOrganizadoCasual.SqlDbType = SqlDbType.Decimal;
                prmPtsOrganizadoCasual.Precision = 5;
                prmPtsOrganizadoCasual.Scale = 2;

                prmPtsIntuicion_Sensacion = new SqlParameter("@PtsIntuicionSensorial", ptsIntuicion_Sensacion);
                prmPtsIntuicion_Sensacion.SqlDbType = SqlDbType.Decimal;
                prmPtsIntuicion_Sensacion.Precision = 5;
                prmPtsIntuicion_Sensacion.Scale = 2;


                cmdResultadoListar.Parameters.Add(prmCodUser);
                cmdResultadoListar.Parameters.Add(prmPtsExtroIntro);
                cmdResultadoListar.Parameters.Add(prmPtsRacional_Emotivo);
                cmdResultadoListar.Parameters.Add(prmPtsOrganizadoCasual);
                cmdResultadoListar.Parameters.Add(prmPtsIntuicion_Sensacion);

                cmdResultadoListar.Connection.Open();
                drResultado = cmdResultadoListar.ExecuteReader();

                List<ResultadoComFacultadBE> lstResultado;
                ResultadoComFacultadBE objResultadoComFacultadBE;

                lstResultado = new List<ResultadoComFacultadBE>();

                while (drResultado.Read())
                {
                    objResultadoComFacultadBE = new ResultadoComFacultadBE();
                    objResultadoComFacultadBE.idFacultad = drResultado.GetInt32(drResultado.GetOrdinal("id_facultad"));
                    objResultadoComFacultadBE.nombreFacultad = drResultado.GetString(drResultado.GetOrdinal("nombre"));
                    objResultadoComFacultadBE.coincidencia = drResultado.GetInt32(drResultado.GetOrdinal("coincidencia"));
                    lstResultado.Add(objResultadoComFacultadBE);
                }

                cmdResultadoListar.Connection.Close();
                cmdResultadoListar.Dispose();
                Conn.Dispose();

                return lstResultado;
            }
            catch (Exception ex)
            {
                Conn.Dispose();
                cmdResultadoListar.Dispose();

                throw;
            }
        }


        public List<ResultadoComCampoLaboralBE> ObtenerResultadoCompatibilidadXCampoLaboral(string codUser, double ptsExtroIntro, double ptsIntuicion_Sensacion, double ptsRacional_Emotivo, double ptsOrganizadoCasual)
        {
            SqlConnection Conn = null;
            SqlCommand cmdResultadoListar = null;
            SqlDataReader drResultado;
            String sCadenaConexion;
            String sqlStoredProcedure;
            SqlParameter prmCodUser;
            SqlParameter prmPtsExtroIntro;
            SqlParameter prmPtsIntuicion_Sensacion;
            SqlParameter prmPtsRacional_Emotivo;
            SqlParameter prmPtsOrganizadoCasual;

            try
            {

                /*
                 @CodEvaluacion VARCHAR(50),
                @PtsExtroIntro DECIMAL(5,2),
                @PtsRacionalEmocional DECIMAL(5,2),
                @PtsOrganizadoCasual DECIMAL(5,2),
                @PtsIntuicionSensorial DECIMAL(5,2) 
                 */
                sCadenaConexion = Utilities.GetConnectionStringTalentos2();

                sqlStoredProcedure = "uspGetCompatibilidadxCampoLaboral";

                Conn = new SqlConnection(sCadenaConexion);

                cmdResultadoListar = Conn.CreateCommand();
                cmdResultadoListar.CommandType = CommandType.StoredProcedure;
                cmdResultadoListar.CommandText = sqlStoredProcedure;

                prmCodUser = new SqlParameter("@CodEvaluacion", codUser);
                prmCodUser.SqlDbType = SqlDbType.VarChar;
                prmCodUser.Size = 50;

                prmPtsExtroIntro = new SqlParameter("@PtsExtroIntro", ptsExtroIntro);
                prmPtsExtroIntro.SqlDbType = SqlDbType.Decimal;
                prmPtsExtroIntro.Precision = 5;
                prmPtsExtroIntro.Scale = 2;

                prmPtsRacional_Emotivo = new SqlParameter("@PtsRacionalEmocional", ptsRacional_Emotivo);
                prmPtsRacional_Emotivo.SqlDbType = SqlDbType.Decimal;
                prmPtsRacional_Emotivo.Precision = 5;
                prmPtsRacional_Emotivo.Scale = 2;

                prmPtsOrganizadoCasual = new SqlParameter("@PtsOrganizadoCasual", ptsOrganizadoCasual);
                prmPtsOrganizadoCasual.SqlDbType = SqlDbType.Decimal;
                prmPtsOrganizadoCasual.Precision = 5;
                prmPtsOrganizadoCasual.Scale = 2;

                prmPtsIntuicion_Sensacion = new SqlParameter("@PtsIntuicionSensorial", ptsIntuicion_Sensacion);
                prmPtsIntuicion_Sensacion.SqlDbType = SqlDbType.Decimal;
                prmPtsIntuicion_Sensacion.Precision = 5;
                prmPtsIntuicion_Sensacion.Scale = 2;


                cmdResultadoListar.Parameters.Add(prmCodUser);
                cmdResultadoListar.Parameters.Add(prmPtsExtroIntro);
                cmdResultadoListar.Parameters.Add(prmPtsRacional_Emotivo);
                cmdResultadoListar.Parameters.Add(prmPtsOrganizadoCasual);
                cmdResultadoListar.Parameters.Add(prmPtsIntuicion_Sensacion);

                cmdResultadoListar.Connection.Open();
                drResultado = cmdResultadoListar.ExecuteReader();

                List<ResultadoComCampoLaboralBE> lstResultado;
                ResultadoComCampoLaboralBE objResultadoComCampoLaboral;

                lstResultado = new List<ResultadoComCampoLaboralBE>();
                string carreras = "";
                string[] arrCarreras = null;
                List<String> lstCarreras = null;

                while (drResultado.Read())
                {
                    objResultadoComCampoLaboral = new ResultadoComCampoLaboralBE();
                    objResultadoComCampoLaboral.idCampoLaboral = drResultado.GetInt32(drResultado.GetOrdinal("id_campolaboral"));
                    objResultadoComCampoLaboral.nombreCamboLaboral = drResultado.GetString(drResultado.GetOrdinal("nombre"));
                    objResultadoComCampoLaboral.coincidencia = drResultado.GetInt32(drResultado.GetOrdinal("coincidencia"));

                    carreras = drResultado.GetString(drResultado.GetOrdinal("carreras"));
                    arrCarreras = carreras.Split(';');
                    lstCarreras = new List<string>();
                    for (int i = 0; i < arrCarreras.Length; i++)
                    {
                        lstCarreras.Add(arrCarreras[i]);
                    }

                    objResultadoComCampoLaboral.lstCarreras = lstCarreras;
                    lstResultado.Add(objResultadoComCampoLaboral);
                }

                cmdResultadoListar.Connection.Close();
                cmdResultadoListar.Dispose();
                Conn.Dispose();

                return lstResultado;
            }
            catch (Exception ex)
            {
                Conn.Dispose();
                cmdResultadoListar.Dispose();

                throw;
            }
        }



        //General-Report
        public List<GeneralReportCategoryBE> getResultGrlReportTalentCategory(int idEncuesta)
        {
            SqlConnection Conn = null;
            SqlCommand cmdResultadoListar = null;
            SqlDataReader drResultado;
            String sCadenaConexion;
            String sqlStoredProcedure;
            SqlParameter prmEncuesta;

            try
            {

                sCadenaConexion = Utilities.GetConnectionStringTalentos2();

                sqlStoredProcedure = "uspGetGrAlReportCategoryTalents";

                Conn = new SqlConnection(sCadenaConexion);

                cmdResultadoListar = Conn.CreateCommand();
                cmdResultadoListar.CommandType = CommandType.StoredProcedure;
                cmdResultadoListar.CommandText = sqlStoredProcedure;

                prmEncuesta = new SqlParameter("@idEncuesta", idEncuesta);
                prmEncuesta.SqlDbType = SqlDbType.Int;
                //prmEncuesta.Size = 50;

                cmdResultadoListar.Parameters.Add(prmEncuesta);

                cmdResultadoListar.Connection.Open();
                drResultado = cmdResultadoListar.ExecuteReader();

                List<GeneralReportCategoryBE> lstResultado;
                GeneralReportCategoryBE objectResult;

                lstResultado = new List<GeneralReportCategoryBE>();

                while (drResultado.Read())
                {
                    objectResult = new GeneralReportCategoryBE();
                    objectResult.TendenciaId = drResultado.GetInt32(drResultado.GetOrdinal("idTendencia"));
                    objectResult.tendenciaDesc = drResultado.GetString(drResultado.GetOrdinal("descTendencia"));
                    objectResult.percentCategory = drResultado.GetInt32(drResultado.GetOrdinal("percentCategory"));
                    objectResult.countTalents = drResultado.GetInt32(drResultado.GetOrdinal("countTalents"));
                    objectResult.TotalTalents = drResultado.GetInt32(drResultado.GetOrdinal("totalTalents"));

                    lstResultado.Add(objectResult);
                }

                cmdResultadoListar.Connection.Close();
                cmdResultadoListar.Dispose();
                Conn.Dispose();

                return lstResultado;
            }
            catch (Exception ex)
            {
                Conn.Dispose();
                cmdResultadoListar.Dispose();

                throw;
            }
        }


        public List<GeneralTalentBE> getlstGeneralTalents(int idEncuesta)
        {
            SqlConnection Conn = null;
            SqlCommand cmdResultadoListar = null;
            SqlDataReader drResultado;
            String sCadenaConexion;
            String sqlStoredProcedure;
            SqlParameter prmEncuesta;

            try
            {

                sCadenaConexion = Utilities.GetConnectionStringTalentos2();

                sqlStoredProcedure = "uspGetGrAlReport_LstTalents";

                Conn = new SqlConnection(sCadenaConexion);

                cmdResultadoListar = Conn.CreateCommand();
                cmdResultadoListar.CommandType = CommandType.StoredProcedure;
                cmdResultadoListar.CommandText = sqlStoredProcedure;

                prmEncuesta = new SqlParameter("@idEncuesta", idEncuesta);
                prmEncuesta.SqlDbType = SqlDbType.Int;
                //prmEncuesta.Size = 50;

                cmdResultadoListar.Parameters.Add(prmEncuesta);

                cmdResultadoListar.Connection.Open();
                drResultado = cmdResultadoListar.ExecuteReader();

                List<GeneralTalentBE> lstResultado;
                GeneralTalentBE objectResult;

                lstResultado = new List<GeneralTalentBE>();

                while (drResultado.Read())
                {
                    objectResult = new GeneralTalentBE();
                    objectResult.idTalento = drResultado.GetInt32(drResultado.GetOrdinal("Talento_Id"));
                    objectResult.cantSelect = drResultado.GetInt32(drResultado.GetOrdinal("Cant_Select"));
                    objectResult.nombre = drResultado.GetString(drResultado.GetOrdinal("Talento"));
                    objectResult.idTendencia = drResultado.GetInt32(drResultado.GetOrdinal("Tendencia_Id"));
                    objectResult.tendencia = drResultado.GetString(drResultado.GetOrdinal("Tendencia"));
                    objectResult.idTipoTalento = drResultado.GetInt32(drResultado.GetOrdinal("TipoTalento_Id"));

                    lstResultado.Add(objectResult);
                }

                cmdResultadoListar.Connection.Close();
                cmdResultadoListar.Dispose();
                Conn.Dispose();

                return lstResultado;
            }
            catch (Exception ex)
            {
                Conn.Dispose();
                cmdResultadoListar.Dispose();

                throw;
            }
        }


        public List<GeneralInteresesBE> getResultGeneralIntereses(int idEncuesta)
        {
            SqlConnection Conn = null;
            SqlCommand cmdResultadoListar = null;
            SqlDataReader drResultado;
            String sCadenaConexion;
            String sqlStoredProcedure;
            SqlParameter prmEncuesta;

            try
            {

                sCadenaConexion = Utilities.GetConnectionStringTalentos2();

                sqlStoredProcedure = "uspGetGrAlReport_resultIntereses";

                Conn = new SqlConnection(sCadenaConexion);

                cmdResultadoListar = Conn.CreateCommand();
                cmdResultadoListar.CommandType = CommandType.StoredProcedure;
                cmdResultadoListar.CommandText = sqlStoredProcedure;

                prmEncuesta = new SqlParameter("@idEncuesta", idEncuesta);
                prmEncuesta.SqlDbType = SqlDbType.Int;
                //prmEncuesta.Size = 50;

                cmdResultadoListar.Parameters.Add(prmEncuesta);

                cmdResultadoListar.Connection.Open();
                drResultado = cmdResultadoListar.ExecuteReader();

                List<GeneralInteresesBE> lstResultado;
                GeneralInteresesBE objectResult;

                lstResultado = new List<GeneralInteresesBE>();

                while (drResultado.Read())
                {
                    objectResult = new GeneralInteresesBE();
                    objectResult.code = drResultado.GetString(drResultado.GetOrdinal("indicatorname"));
                    objectResult.score = drResultado.GetInt32(drResultado.GetOrdinal("indicatorvalue"));

                    lstResultado.Add(objectResult);
                }

                cmdResultadoListar.Connection.Close();
                cmdResultadoListar.Dispose();
                Conn.Dispose();

                return lstResultado;
            }
            catch (Exception ex)
            {
                Conn.Dispose();
                cmdResultadoListar.Dispose();

                throw;
            }
        }

        public List<GeneralTemperamentosBE> getResultGeneralTemperamentos(int idEncuesta)
        {
            SqlConnection Conn = null;
            SqlCommand cmdResultadoListar = null;
            SqlDataReader drResultado;
            String sCadenaConexion;
            String sqlStoredProcedure;
            SqlParameter prmEncuesta;

            try
            {

                sCadenaConexion = Utilities.GetConnectionStringTalentos2();

                sqlStoredProcedure = "uspGetGrAlReport_resultTemperament";

                Conn = new SqlConnection(sCadenaConexion);

                cmdResultadoListar = Conn.CreateCommand();
                cmdResultadoListar.CommandType = CommandType.StoredProcedure;
                cmdResultadoListar.CommandText = sqlStoredProcedure;

                prmEncuesta = new SqlParameter("@idEncuesta", idEncuesta);
                prmEncuesta.SqlDbType = SqlDbType.Int;
                //prmEncuesta.Size = 50;

                cmdResultadoListar.Parameters.Add(prmEncuesta);

                cmdResultadoListar.Connection.Open();
                drResultado = cmdResultadoListar.ExecuteReader();

                List<GeneralTemperamentosBE> lstResultado;
                GeneralTemperamentosBE objectResult;

                lstResultado = new List<GeneralTemperamentosBE>();

                while (drResultado.Read())
                {
                    objectResult = new GeneralTemperamentosBE();
                    objectResult.code = drResultado.GetString(drResultado.GetOrdinal("indicatorname"));
                    objectResult.score = drResultado.GetDecimal(drResultado.GetOrdinal("indicatorvalue"));

                    lstResultado.Add(objectResult);
                }

                cmdResultadoListar.Connection.Close();
                cmdResultadoListar.Dispose();
                Conn.Dispose();

                return lstResultado;
            }
            catch (Exception ex)
            {
                Conn.Dispose();
                cmdResultadoListar.Dispose();

                throw;
            }
        }

        public void insertInterestsResult(string strUserId, int intSchedullingId, int intADM, int intARG, int intART, int intCOM,
                                         int intCON, int intCUL, int intDEP, int intDIS, int intFIN, int intINF, int intJUR,
                                         int intMAR, int intMEC, int intMIN, int intPED, int intSAL, int intSOC, int intTRA,
                                         int intTUR)
        {
            String SqlInsertarResultado;
            SqlConnection Conn = null;
            string sCadena;
            SqlParameter prmStrUserId;
            SqlParameter prmIntSchedullingId;
            SqlParameter prmIntADM;
            SqlParameter prmIntARG;
            SqlParameter prmIntART;
            SqlParameter prmIntCOM;
            SqlParameter prmIntCON;
            SqlParameter prmIntCUL;
            SqlParameter prmIntDEP;
            SqlParameter prmIntDIS;
            SqlParameter prmIntFIN;
            SqlParameter prmIntINF;
            SqlParameter prmIntJUR;
            SqlParameter prmIntMAR;
            SqlParameter prmIntMEC;
            SqlParameter prmIntMIN;
            SqlParameter prmIntPED;
            SqlParameter prmIntSAL;
            SqlParameter prmIntSOC;
            SqlParameter prmIntTRA;
            SqlParameter prmIntTUR;

            SqlCommand cmdResultadoInsertar = null;

            try
            {
                sCadena = Utilities.GetConnectionStringTalentos2();
                Conn = new SqlConnection(sCadena);

                SqlInsertarResultado = "uspiResultadoIntereses";

                cmdResultadoInsertar = Conn.CreateCommand();
                cmdResultadoInsertar.CommandType = CommandType.StoredProcedure;
                cmdResultadoInsertar.CommandText = SqlInsertarResultado;

                prmStrUserId = new SqlParameter();
                prmStrUserId.ParameterName = "@CodEvaluacion";
                prmStrUserId.SqlDbType = SqlDbType.VarChar;
                prmStrUserId.Size = 50;
                prmStrUserId.Value = strUserId;

                prmIntSchedullingId = new SqlParameter();
                prmIntSchedullingId.ParameterName = "@CodEncuesta_Intereses";
                prmIntSchedullingId.SqlDbType = SqlDbType.Int;
                prmIntSchedullingId.Value = intSchedullingId;

                prmIntADM = new SqlParameter();
                prmIntADM.ParameterName = "@Administracion";
                prmIntADM.SqlDbType = SqlDbType.Int;
                prmIntADM.Value = intADM;

                prmIntARG = new SqlParameter();
                prmIntARG.ParameterName = "@Agraria";
                prmIntARG.SqlDbType = SqlDbType.Int;
                prmIntARG.Value = intARG;

                prmIntART = new SqlParameter();
                prmIntART.ParameterName = "@Artistica";
                prmIntART.SqlDbType = SqlDbType.Int;
                prmIntART.Value = intART;

                prmIntCOM = new SqlParameter();
                prmIntCOM.ParameterName = "@Comunicacion";
                prmIntCOM.SqlDbType = SqlDbType.Int;
                prmIntCOM.Value = intCOM;

                prmIntCON = new SqlParameter();
                prmIntCON.ParameterName = "@Construccion";
                prmIntCON.SqlDbType = SqlDbType.Int;
                prmIntCON.Value = intCON;

                prmIntCUL = new SqlParameter();
                prmIntCUL.ParameterName = "@Culinaria";
                prmIntCUL.SqlDbType = SqlDbType.Int;
                prmIntCUL.Value = intCUL;

                prmIntDEP = new SqlParameter();
                prmIntDEP.ParameterName = "@Deportiva";
                prmIntDEP.SqlDbType = SqlDbType.Int;
                prmIntDEP.Value = intDEP;

                prmIntDIS = new SqlParameter();
                prmIntDIS.ParameterName = "@Diseno";
                prmIntDIS.SqlDbType = SqlDbType.Int;
                prmIntDIS.Value = intDIS;

                prmIntFIN = new SqlParameter();
                prmIntFIN.ParameterName = "@Financiera";
                prmIntFIN.SqlDbType = SqlDbType.Int;
                prmIntFIN.Value = intFIN;

                prmIntINF = new SqlParameter();
                prmIntINF.ParameterName = "@Informatica";
                prmIntINF.SqlDbType = SqlDbType.Int;
                prmIntINF.Value = intINF;

                prmIntJUR = new SqlParameter();
                prmIntJUR.ParameterName = "@Juridico";
                prmIntJUR.SqlDbType = SqlDbType.Int;
                prmIntJUR.Value = intJUR;

                prmIntMAR = new SqlParameter();
                prmIntMAR.ParameterName = "@Marketing";
                prmIntMAR.SqlDbType = SqlDbType.Int;
                prmIntMAR.Value = intMAR;

                prmIntMEC = new SqlParameter();
                prmIntMEC.ParameterName = "@Mecanico_Electronica";
                prmIntMEC.SqlDbType = SqlDbType.Int;
                prmIntMEC.Value = intMEC;

                prmIntMIN = new SqlParameter();
                prmIntMIN.ParameterName = "@Minera";
                prmIntMIN.SqlDbType = SqlDbType.Int;
                prmIntMIN.Value = intMIN;

                prmIntPED = new SqlParameter();
                prmIntPED.ParameterName = "@Pedagogia";
                prmIntPED.SqlDbType = SqlDbType.Int;
                prmIntPED.Value = intPED;

                prmIntSAL = new SqlParameter();
                prmIntSAL.ParameterName = "@Salud";
                prmIntSAL.SqlDbType = SqlDbType.Int;
                prmIntSAL.Value = intSAL;

                prmIntSOC = new SqlParameter();
                prmIntSOC.ParameterName = "@Social";
                prmIntSOC.SqlDbType = SqlDbType.Int;
                prmIntSOC.Value = intSOC;

                prmIntTRA = new SqlParameter();
                prmIntTRA.ParameterName = "@Traduccion";
                prmIntTRA.SqlDbType = SqlDbType.Int;
                prmIntTRA.Value = intTRA;

                prmIntTUR = new SqlParameter();
                prmIntTUR.ParameterName = "@Turismo";
                prmIntTUR.SqlDbType = SqlDbType.Int;
                prmIntTUR.Value = intTUR;

                cmdResultadoInsertar.Parameters.Add(prmStrUserId);
                cmdResultadoInsertar.Parameters.Add(prmIntSchedullingId);
                cmdResultadoInsertar.Parameters.Add(prmIntADM);
                cmdResultadoInsertar.Parameters.Add(prmIntARG);
                cmdResultadoInsertar.Parameters.Add(prmIntART);
                cmdResultadoInsertar.Parameters.Add(prmIntCOM);
                cmdResultadoInsertar.Parameters.Add(prmIntCON);
                cmdResultadoInsertar.Parameters.Add(prmIntCUL);
                cmdResultadoInsertar.Parameters.Add(prmIntDEP);
                cmdResultadoInsertar.Parameters.Add(prmIntDIS);
                cmdResultadoInsertar.Parameters.Add(prmIntFIN);
                cmdResultadoInsertar.Parameters.Add(prmIntINF);
                cmdResultadoInsertar.Parameters.Add(prmIntJUR);
                cmdResultadoInsertar.Parameters.Add(prmIntMAR);
                cmdResultadoInsertar.Parameters.Add(prmIntMEC);
                cmdResultadoInsertar.Parameters.Add(prmIntMIN);
                cmdResultadoInsertar.Parameters.Add(prmIntPED);
                cmdResultadoInsertar.Parameters.Add(prmIntSAL);
                cmdResultadoInsertar.Parameters.Add(prmIntSOC);
                cmdResultadoInsertar.Parameters.Add(prmIntTRA);
                cmdResultadoInsertar.Parameters.Add(prmIntTUR);


                cmdResultadoInsertar.Connection.Open();
                cmdResultadoInsertar.ExecuteNonQuery();

            }
            catch (Exception ex)
            {
                cmdResultadoInsertar.Connection.Close();
                cmdResultadoInsertar.Dispose();
                throw;
            }
        }




        public void insertTemperamentsResult(string strUserId, int intSchedullingTemperamentId, int intSchedullingInterestsId,
                                             double dblAmbDinamic_AmbTranquilo, double dblSociable_Intimo, double dblEntusiasta_Calmado,
                                             double dblComunicativo_Reservado, double dblInstintivo_Esceptico, double dblOriginal_Tradicional,
                                             double dblCreativo_Realista, double dblObjetivo_Compasivo, double dblDistante_Susceptible,
                                             double dblDirecto_Empatico, double dblPlanificado_Espontaneo, double dblMetodico_Eventual,
                                             double dblEstructurado_Flexible)
        {
            String SqlInsertarResultado;
            SqlConnection Conn = null;
            string sCadena;
            SqlParameter prmStrUserId;
            SqlParameter prmIntSchedullingTemperamentId;
            SqlParameter prmIntSchedullingInterestsId;
            SqlParameter prmDblAmbDinamic_AmbTranquilo;
            SqlParameter prmDblSociable_Intimo;
            SqlParameter prmDblEntusiasta_Calmado;
            SqlParameter prmDblComunicativo_Reservado;

            SqlParameter prmDblInstintivo_Esceptico;
            SqlParameter prmDblOriginal_Tradicional;
            SqlParameter prmDblCreativo_Realista;

            SqlParameter prmDblObjetivo_Compasivo;
            SqlParameter prmDblDistante_Susceptible;
            SqlParameter prmDblDirecto_Empatico;

            SqlParameter prmDblPlanificado_Espontaneo;
            SqlParameter prmDblMetodico_Eventual;
            SqlParameter prmDblEstructurado_Flexible;


            SqlCommand cmdResultadoInsertar = null;

            try
            {
                sCadena = Utilities.GetConnectionStringTalentos2();
                Conn = new SqlConnection(sCadena);

                SqlInsertarResultado = "uspiResultadoTemperamentos";

                cmdResultadoInsertar = Conn.CreateCommand();
                cmdResultadoInsertar.CommandType = CommandType.StoredProcedure;
                cmdResultadoInsertar.CommandText = SqlInsertarResultado;

                prmStrUserId = new SqlParameter();
                prmStrUserId.ParameterName = "@CodEvaluacion";
                prmStrUserId.SqlDbType = SqlDbType.VarChar;
                prmStrUserId.Size = 50;
                prmStrUserId.Value = strUserId;

                prmIntSchedullingTemperamentId = new SqlParameter();
                prmIntSchedullingTemperamentId.ParameterName = "@CodEncuesta_Temperamentos";
                prmIntSchedullingTemperamentId.SqlDbType = SqlDbType.Int;
                prmIntSchedullingTemperamentId.Value = intSchedullingTemperamentId;

                prmIntSchedullingInterestsId = new SqlParameter();
                prmIntSchedullingInterestsId.ParameterName = "@CodEncuesta_Intereses";
                prmIntSchedullingInterestsId.SqlDbType = SqlDbType.Int;
                prmIntSchedullingInterestsId.Value = intSchedullingInterestsId;

                prmDblAmbDinamic_AmbTranquilo = new SqlParameter("@EI_AmbDinamic_AmbTranquilo", dblAmbDinamic_AmbTranquilo);
                prmDblAmbDinamic_AmbTranquilo.SqlDbType = SqlDbType.Decimal;
                prmDblAmbDinamic_AmbTranquilo.Precision = 5;
                prmDblAmbDinamic_AmbTranquilo.Scale = 2;

                prmDblSociable_Intimo = new SqlParameter("@EI_Sociable_Intimo", dblSociable_Intimo);
                prmDblSociable_Intimo.SqlDbType = SqlDbType.Decimal;
                prmDblSociable_Intimo.Precision = 5;
                prmDblSociable_Intimo.Scale = 2;

                prmDblEntusiasta_Calmado = new SqlParameter("@EI_Entusiasta_Calmado", dblEntusiasta_Calmado);
                prmDblEntusiasta_Calmado.SqlDbType = SqlDbType.Decimal;
                prmDblEntusiasta_Calmado.Precision = 5;
                prmDblEntusiasta_Calmado.Scale = 2;

                prmDblComunicativo_Reservado = new SqlParameter("@EI_Comunicativo_Reservado", dblComunicativo_Reservado);
                prmDblComunicativo_Reservado.SqlDbType = SqlDbType.Decimal;
                prmDblComunicativo_Reservado.Precision = 5;
                prmDblComunicativo_Reservado.Scale = 2;

                prmDblInstintivo_Esceptico = new SqlParameter("@IS_Instintivo_Esceptico", dblInstintivo_Esceptico);
                prmDblInstintivo_Esceptico.SqlDbType = SqlDbType.Decimal;
                prmDblInstintivo_Esceptico.Precision = 5;
                prmDblInstintivo_Esceptico.Scale = 2;

                prmDblOriginal_Tradicional = new SqlParameter("@IS_Original_Tradicional", dblOriginal_Tradicional);
                prmDblOriginal_Tradicional.SqlDbType = SqlDbType.Decimal;
                prmDblOriginal_Tradicional.Precision = 5;
                prmDblOriginal_Tradicional.Scale = 2;

                prmDblCreativo_Realista = new SqlParameter("@IS_Creativo_Realista", dblCreativo_Realista);
                prmDblCreativo_Realista.SqlDbType = SqlDbType.Decimal;
                prmDblCreativo_Realista.Precision = 5;
                prmDblCreativo_Realista.Scale = 2;

                prmDblObjetivo_Compasivo = new SqlParameter("@RE_Objetivo_Compasivo", dblObjetivo_Compasivo);
                prmDblObjetivo_Compasivo.SqlDbType = SqlDbType.Decimal;
                prmDblObjetivo_Compasivo.Precision = 5;
                prmDblObjetivo_Compasivo.Scale = 2;

                prmDblDistante_Susceptible = new SqlParameter("@RE_Distante_Susceptible", dblDistante_Susceptible);
                prmDblDistante_Susceptible.SqlDbType = SqlDbType.Decimal;
                prmDblDistante_Susceptible.Precision = 5;
                prmDblDistante_Susceptible.Scale = 2;

                prmDblDirecto_Empatico = new SqlParameter("@RE_Directo_Empatico", dblDirecto_Empatico);
                prmDblDirecto_Empatico.SqlDbType = SqlDbType.Decimal;
                prmDblDirecto_Empatico.Precision = 5;
                prmDblDirecto_Empatico.Scale = 2;

                prmDblPlanificado_Espontaneo = new SqlParameter("@OC_Planificado_Espontaneo", dblPlanificado_Espontaneo);
                prmDblPlanificado_Espontaneo.SqlDbType = SqlDbType.Decimal;
                prmDblPlanificado_Espontaneo.Precision = 5;
                prmDblPlanificado_Espontaneo.Scale = 2;

                prmDblMetodico_Eventual = new SqlParameter("@OC_Metodico_Eventual", dblMetodico_Eventual);
                prmDblMetodico_Eventual.SqlDbType = SqlDbType.Decimal;
                prmDblMetodico_Eventual.Precision = 5;
                prmDblMetodico_Eventual.Scale = 2;

                prmDblEstructurado_Flexible = new SqlParameter("@OC_Estructurado_Flexible", dblEstructurado_Flexible);
                prmDblEstructurado_Flexible.SqlDbType = SqlDbType.Decimal;
                prmDblEstructurado_Flexible.Precision = 5;
                prmDblEstructurado_Flexible.Scale = 2;

                cmdResultadoInsertar.Parameters.Add(prmStrUserId);
                cmdResultadoInsertar.Parameters.Add(prmIntSchedullingTemperamentId);
                cmdResultadoInsertar.Parameters.Add(prmIntSchedullingInterestsId);
                cmdResultadoInsertar.Parameters.Add(prmDblAmbDinamic_AmbTranquilo);
                cmdResultadoInsertar.Parameters.Add(prmDblSociable_Intimo);
                cmdResultadoInsertar.Parameters.Add(prmDblEntusiasta_Calmado);
                cmdResultadoInsertar.Parameters.Add(prmDblComunicativo_Reservado);
                cmdResultadoInsertar.Parameters.Add(prmDblInstintivo_Esceptico);
                cmdResultadoInsertar.Parameters.Add(prmDblOriginal_Tradicional);
                cmdResultadoInsertar.Parameters.Add(prmDblCreativo_Realista);
                cmdResultadoInsertar.Parameters.Add(prmDblObjetivo_Compasivo);
                cmdResultadoInsertar.Parameters.Add(prmDblDistante_Susceptible);
                cmdResultadoInsertar.Parameters.Add(prmDblDirecto_Empatico);
                cmdResultadoInsertar.Parameters.Add(prmDblPlanificado_Espontaneo);
                cmdResultadoInsertar.Parameters.Add(prmDblMetodico_Eventual);
                cmdResultadoInsertar.Parameters.Add(prmDblEstructurado_Flexible);

                cmdResultadoInsertar.Connection.Open();
                cmdResultadoInsertar.ExecuteNonQuery();

            }
            catch (Exception ex)
            {
                cmdResultadoInsertar.Connection.Close();
                cmdResultadoInsertar.Dispose();
                throw;
            }
        }


        public string getDatePoll(int intPollId)
        {
            SqlConnection Conn = null;
            SqlCommand cmdresultado = null;
            SqlDataReader rdResultado;
            String sCadenaConexion;
            String sqlResultado;
            SqlParameter prmPollId;
            string date = "";

            try
            {
                sCadenaConexion = Utilities.GetConnectionStringTalentos2();

                sqlResultado = "uspGetDatePoll";

                Conn = new SqlConnection(sCadenaConexion);

                cmdresultado = Conn.CreateCommand();
                cmdresultado.CommandType = CommandType.StoredProcedure;
                cmdresultado.CommandText = sqlResultado;

                prmPollId = new SqlParameter();
                prmPollId.ParameterName = "@PollId";
                prmPollId.SqlDbType = System.Data.SqlDbType.Int;
                prmPollId.Value = intPollId;

                cmdresultado.Parameters.Add(prmPollId);

                cmdresultado.Connection.Open();
                rdResultado = cmdresultado.ExecuteReader();

                if (rdResultado.Read())
                {
                    date = rdResultado.GetString(rdResultado.GetOrdinal("Month_Year"));
                }

                cmdresultado.Connection.Close();
                cmdresultado.Dispose();
                Conn.Dispose();
                return date;
            }
            catch (Exception ex)
            {
                Conn.Dispose();
                cmdresultado.Dispose();

                throw;
            }
        }


        public string getCompanyName(int intPollId)
        {
            SqlConnection Conn = null;
            SqlCommand cmdresultado = null;
            SqlDataReader rdResultado;
            String sCadenaConexion;
            String sqlResultado;
            SqlParameter prmPollId;
            string strCompanyName = "";

            try
            {
                sCadenaConexion = Utilities.GetConnectionStringTalentos2();

                sqlResultado = "uspGetCompanyNameForGeneralReport";

                Conn = new SqlConnection(sCadenaConexion);

                cmdresultado = Conn.CreateCommand();
                cmdresultado.CommandType = CommandType.StoredProcedure;
                cmdresultado.CommandText = sqlResultado;

                prmPollId = new SqlParameter();
                prmPollId.ParameterName = "@PollId";
                prmPollId.SqlDbType = System.Data.SqlDbType.Int;
                prmPollId.Value = intPollId;

                cmdresultado.Parameters.Add(prmPollId);

                cmdresultado.Connection.Open();
                rdResultado = cmdresultado.ExecuteReader();

                if (rdResultado.Read())
                {
                    strCompanyName = rdResultado.GetString(rdResultado.GetOrdinal("Institucion"));
                }

                cmdresultado.Connection.Close();
                cmdresultado.Dispose();
                Conn.Dispose();
                return strCompanyName;
            }
            catch (Exception ex)
            {
                Conn.Dispose();
                cmdresultado.Dispose();

                throw;
            }
        }
        


    }
}

