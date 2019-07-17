using System;
using UPC.Talentos.BL.BE;
using System.Data.SqlClient;
using System.Data;
using System.Collections.Generic;

namespace UPC.Talentos.DL.DALC
{
    public class ParticipanteDALC
    {
        public int InsertarParticipante(ParticipanteBE objParticipanteBE)
        {
            String SqlInsertarParticipante;
            string sCadena;
            int participanteId = 0;
            SqlConnection Conn;
            SqlCommand cmdInsertarParticipante = null;
            SqlParameter prmParticipanteId;
            SqlParameter prmNombres;
            SqlParameter prmApPaterno;
            SqlParameter prmApMaterno;
            SqlParameter prmSexo;
            SqlParameter prmCorreo;
            SqlParameter prmFechaNac;
            SqlParameter prmNivelInstruccion;
            SqlParameter prmCargoTrabajo;
            SqlParameter prmInstitucion;
            SqlParameter prmDNI;
            SqlParameter prmCodigoEvaluacion;

            try
            {
                sCadena = Utilities.GetConnectionStringTalentos2();
                Conn = new SqlConnection(sCadena);

                SqlInsertarParticipante = "usp_TalInsertarParticipante";
                cmdInsertarParticipante = Conn.CreateCommand();
                cmdInsertarParticipante.CommandType = System.Data.CommandType.StoredProcedure;
                cmdInsertarParticipante.CommandText = SqlInsertarParticipante;

                prmParticipanteId = new SqlParameter();
                prmParticipanteId.SqlDbType = System.Data.SqlDbType.Int;
                prmParticipanteId.Direction = System.Data.ParameterDirection.ReturnValue;

                prmNombres = new SqlParameter();
                prmNombres.ParameterName = "@Nombres";
                prmNombres.SqlDbType = System.Data.SqlDbType.VarChar;
                prmNombres.Size = 100;
                prmNombres.Value = objParticipanteBE.Nombres;

                prmDNI = new SqlParameter();
                prmDNI.ParameterName = "@DNI";
                prmDNI.SqlDbType = System.Data.SqlDbType.VarChar;
                prmDNI.Size = 8;
                prmDNI.Value = objParticipanteBE.DNI;

                prmApPaterno = new SqlParameter();
                prmApPaterno.ParameterName = "@ApPaterno";
                prmApPaterno.SqlDbType = System.Data.SqlDbType.VarChar;
                prmApPaterno.Size = 60;
                prmApPaterno.Value = objParticipanteBE.ApellidoPaterno;

                prmApMaterno = new SqlParameter();
                prmApMaterno.ParameterName = "@ApMaterno";
                prmApMaterno.SqlDbType = System.Data.SqlDbType.VarChar;
                prmApMaterno.Size = 60;
                prmApMaterno.Value = objParticipanteBE.ApellidoMaterno;

                prmSexo = new SqlParameter();
                prmSexo.ParameterName = "@Sexo";
                prmSexo.SqlDbType = System.Data.SqlDbType.VarChar;
                prmSexo.Size = 1;
                prmSexo.Value = objParticipanteBE.Sexo;

                prmCorreo = new SqlParameter();
                prmCorreo.ParameterName = "@Correo";
                prmCorreo.SqlDbType = System.Data.SqlDbType.VarChar;
                prmCorreo.Size = 150;
                prmCorreo.Value = objParticipanteBE.CorreoElectronico;

                prmFechaNac = new SqlParameter();
                prmFechaNac.ParameterName = "@FechaNacimiento";
                prmFechaNac.SqlDbType = System.Data.SqlDbType.VarChar;
                prmFechaNac.Size = 10;
                prmFechaNac.Value = objParticipanteBE.FechaNacimiento;

                prmNivelInstruccion = new SqlParameter();
                prmNivelInstruccion.ParameterName = "@NivelInstruccion";
                prmNivelInstruccion.SqlDbType = System.Data.SqlDbType.VarChar;
                prmNivelInstruccion.Size = 200;
                prmNivelInstruccion.Value = objParticipanteBE.NivelInstruccion;

                prmCargoTrabajo = new SqlParameter();
                prmCargoTrabajo.ParameterName = "@CargoTrabajo";
                prmCargoTrabajo.SqlDbType = System.Data.SqlDbType.VarChar;
                prmCargoTrabajo.Size = 200;
                prmCargoTrabajo.Value = objParticipanteBE.Cargo;

                prmInstitucion = new SqlParameter();
                prmInstitucion.ParameterName = "@CentroTrabajo";
                prmInstitucion.SqlDbType = System.Data.SqlDbType.VarChar;
                prmInstitucion.Size = 150;
                prmInstitucion.Value = objParticipanteBE.Institucion;

                prmCodigoEvaluacion = new SqlParameter();
                prmCodigoEvaluacion.ParameterName = "@CodigoEvaluacion";
                prmCodigoEvaluacion.SqlDbType = System.Data.SqlDbType.VarChar;
                prmCodigoEvaluacion.Size = 10;
                prmCodigoEvaluacion.Value = objParticipanteBE.CodigoEvaluacion;

                cmdInsertarParticipante.Parameters.Add(prmParticipanteId);
                cmdInsertarParticipante.Parameters.Add(prmNombres);
                cmdInsertarParticipante.Parameters.Add(prmApPaterno);
                cmdInsertarParticipante.Parameters.Add(prmApMaterno);
                cmdInsertarParticipante.Parameters.Add(prmSexo);
                cmdInsertarParticipante.Parameters.Add(prmCorreo);
                cmdInsertarParticipante.Parameters.Add(prmFechaNac);
                cmdInsertarParticipante.Parameters.Add(prmNivelInstruccion);
                cmdInsertarParticipante.Parameters.Add(prmInstitucion);
                cmdInsertarParticipante.Parameters.Add(prmCargoTrabajo);
                cmdInsertarParticipante.Parameters.Add(prmDNI);
                cmdInsertarParticipante.Parameters.Add(prmCodigoEvaluacion);

                cmdInsertarParticipante.Connection.Open();
                cmdInsertarParticipante.ExecuteNonQuery();

                participanteId = Convert.ToInt32(prmParticipanteId.Value);
                cmdInsertarParticipante.Connection.Close();
                cmdInsertarParticipante.Dispose();
                Conn.Dispose();

                return participanteId;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        public int InsertarParticipanteAdulto(ParticipanteBE objParticipanteBE)
        {
            String SqlInsertarParticipante;
            string sCadena;
            int participanteId = 0;
            SqlConnection Conn;
            SqlCommand cmdInsertarParticipante = null;
            SqlParameter prmParticipanteId;
            SqlParameter prmNombres;
            SqlParameter prmApPaterno;
            SqlParameter prmApMaterno;
            SqlParameter prmSexo;
            SqlParameter prmCorreo;
            SqlParameter prmFechaNac;
            SqlParameter prmNivelInstruccion;
            SqlParameter prmCargoTrabajo;
            SqlParameter prmInstitucion;
            SqlParameter prmDNI;
            SqlParameter prmCodigoEvaluacion;

            try
            {
                sCadena = Utilities.GetConnectionStringTalentos2();
                Conn = new SqlConnection(sCadena);

                SqlInsertarParticipante = "usp_TalInsertarParticipanteAdulto";
                cmdInsertarParticipante = Conn.CreateCommand();
                cmdInsertarParticipante.CommandType = System.Data.CommandType.StoredProcedure;
                cmdInsertarParticipante.CommandText = SqlInsertarParticipante;

                prmParticipanteId = new SqlParameter();
                prmParticipanteId.SqlDbType = System.Data.SqlDbType.Int;
                prmParticipanteId.Direction = System.Data.ParameterDirection.ReturnValue;

                prmNombres = new SqlParameter();
                prmNombres.ParameterName = "@Nombres";
                prmNombres.SqlDbType = System.Data.SqlDbType.VarChar;
                prmNombres.Size = 100;
                prmNombres.Value = objParticipanteBE.Nombres;

                prmDNI = new SqlParameter();
                prmDNI.ParameterName = "@DNI";
                prmDNI.SqlDbType = System.Data.SqlDbType.VarChar;
                prmDNI.Size = 8;
                prmDNI.Value = objParticipanteBE.DNI;

                prmApPaterno = new SqlParameter();
                prmApPaterno.ParameterName = "@ApPaterno";
                prmApPaterno.SqlDbType = System.Data.SqlDbType.VarChar;
                prmApPaterno.Size = 60;
                prmApPaterno.Value = objParticipanteBE.ApellidoPaterno;

                prmApMaterno = new SqlParameter();
                prmApMaterno.ParameterName = "@ApMaterno";
                prmApMaterno.SqlDbType = System.Data.SqlDbType.VarChar;
                prmApMaterno.Size = 60;
                prmApMaterno.Value = objParticipanteBE.ApellidoMaterno;

                prmSexo = new SqlParameter();
                prmSexo.ParameterName = "@Sexo";
                prmSexo.SqlDbType = System.Data.SqlDbType.VarChar;
                prmSexo.Size = 1;
                prmSexo.Value = objParticipanteBE.Sexo;

                prmCorreo = new SqlParameter();
                prmCorreo.ParameterName = "@Correo";
                prmCorreo.SqlDbType = System.Data.SqlDbType.VarChar;
                prmCorreo.Size = 150;
                prmCorreo.Value = objParticipanteBE.CorreoElectronico;

                prmFechaNac = new SqlParameter();
                prmFechaNac.ParameterName = "@FechaNacimiento";
                prmFechaNac.SqlDbType = System.Data.SqlDbType.VarChar;
                prmFechaNac.Size = 10;
                prmFechaNac.Value = objParticipanteBE.FechaNacimiento;

                prmNivelInstruccion = new SqlParameter();
                prmNivelInstruccion.ParameterName = "@NivelInstruccion";
                prmNivelInstruccion.SqlDbType = System.Data.SqlDbType.VarChar;
                prmNivelInstruccion.Size = 200;
                prmNivelInstruccion.Value = objParticipanteBE.NivelInstruccion;

                prmCargoTrabajo = new SqlParameter();
                prmCargoTrabajo.ParameterName = "@CargoTrabajo";
                prmCargoTrabajo.SqlDbType = System.Data.SqlDbType.VarChar;
                prmCargoTrabajo.Size = 200;
                prmCargoTrabajo.Value = objParticipanteBE.Cargo;

                prmInstitucion = new SqlParameter();
                prmInstitucion.ParameterName = "@CentroTrabajo";
                prmInstitucion.SqlDbType = System.Data.SqlDbType.VarChar;
                prmInstitucion.Size = 150;
                prmInstitucion.Value = objParticipanteBE.Institucion;

                prmCodigoEvaluacion = new SqlParameter();
                prmCodigoEvaluacion.ParameterName = "@CodigoEvaluacion";
                prmCodigoEvaluacion.SqlDbType = System.Data.SqlDbType.VarChar;
                prmCodigoEvaluacion.Size = 10;
                prmCodigoEvaluacion.Value = objParticipanteBE.CodigoEvaluacion;

                cmdInsertarParticipante.Parameters.Add(prmParticipanteId);
                cmdInsertarParticipante.Parameters.Add(prmNombres);
                cmdInsertarParticipante.Parameters.Add(prmApPaterno);
                cmdInsertarParticipante.Parameters.Add(prmApMaterno);
                cmdInsertarParticipante.Parameters.Add(prmSexo);
                cmdInsertarParticipante.Parameters.Add(prmCorreo);
                cmdInsertarParticipante.Parameters.Add(prmFechaNac);
                cmdInsertarParticipante.Parameters.Add(prmNivelInstruccion);
                cmdInsertarParticipante.Parameters.Add(prmInstitucion);
                cmdInsertarParticipante.Parameters.Add(prmCargoTrabajo);
                cmdInsertarParticipante.Parameters.Add(prmDNI);
                cmdInsertarParticipante.Parameters.Add(prmCodigoEvaluacion);

                cmdInsertarParticipante.Connection.Open();
                cmdInsertarParticipante.ExecuteNonQuery();

                participanteId = Convert.ToInt32(prmParticipanteId.Value);
                cmdInsertarParticipante.Connection.Close();
                cmdInsertarParticipante.Dispose();
                Conn.Dispose();

                return participanteId;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public bool VerificaParticipanteActivo(string codEvaluacion)
        {
            SqlConnection Conn = null;
            SqlCommand cmdParticipante = null;
            SqlDataReader drParticipante;
            String sCadenaConexion;
            String sqlParticipante;
            SqlParameter prmCodigoEvaluacion;
            string cod = "";
            bool existe = false;

            try
            {
                sCadenaConexion = Utilities.GetConnectionStringTalentos2();

                sqlParticipante = "usp_TalVerificaParticipanteActivo";

                Conn = new SqlConnection(sCadenaConexion);

                cmdParticipante = Conn.CreateCommand();
                cmdParticipante.CommandType = CommandType.StoredProcedure;
                cmdParticipante.CommandText = sqlParticipante;

                prmCodigoEvaluacion = new SqlParameter();
                prmCodigoEvaluacion.ParameterName = "@CodigoEvaluacion";
                prmCodigoEvaluacion.SqlDbType = System.Data.SqlDbType.VarChar;
                prmCodigoEvaluacion.Size = 10;
                prmCodigoEvaluacion.Value = codEvaluacion;

                cmdParticipante.Parameters.Add(prmCodigoEvaluacion);

                cmdParticipante.Connection.Open();
                drParticipante = cmdParticipante.ExecuteReader();

                while (drParticipante.Read())
                {
                    cod = drParticipante.GetString(drParticipante.GetOrdinal("CodigoEvaluacion"));
                }

                cmdParticipante.Connection.Close();
                cmdParticipante.Dispose();
                Conn.Dispose();

                if (!cod.Equals(""))
                    existe = true;

                return existe;
            }
            catch (Exception ex)
            {
                Conn.Dispose();
                cmdParticipante.Dispose();

                throw;
            }
        }


        public bool VerificaParticipanteAdultoActivo(string codEvaluacion)
        {
            SqlConnection Conn = null;
            SqlCommand cmdParticipante = null;
            SqlDataReader drParticipante;
            String sCadenaConexion;
            String sqlParticipante;
            SqlParameter prmCodigoEvaluacion;
            string cod = "";
            bool existe = false;

            try
            {
                sCadenaConexion = Utilities.GetConnectionStringTalentos2();

                sqlParticipante = "usp_TalVerificaParticipanteAdultoActivo";

                Conn = new SqlConnection(sCadenaConexion);

                cmdParticipante = Conn.CreateCommand();
                cmdParticipante.CommandType = CommandType.StoredProcedure;
                cmdParticipante.CommandText = sqlParticipante;

                prmCodigoEvaluacion = new SqlParameter();
                prmCodigoEvaluacion.ParameterName = "@CodigoEvaluacion";
                prmCodigoEvaluacion.SqlDbType = System.Data.SqlDbType.VarChar;
                prmCodigoEvaluacion.Size = 10;
                prmCodigoEvaluacion.Value = codEvaluacion;

                cmdParticipante.Parameters.Add(prmCodigoEvaluacion);

                cmdParticipante.Connection.Open();
                drParticipante = cmdParticipante.ExecuteReader();

                while (drParticipante.Read())
                {
                    cod = drParticipante.GetString(drParticipante.GetOrdinal("CodigoEvaluacion"));
                }

                cmdParticipante.Connection.Close();
                cmdParticipante.Dispose();
                Conn.Dispose();

                if (!cod.Equals(""))
                    existe = true;

                return existe;
            }
            catch (Exception ex)
            {
                Conn.Dispose();
                cmdParticipante.Dispose();

                throw;
            }
        }

        public List<ParticipanteBE> ListarParticipantesMasivos()
        {
            SqlConnection Conn = null;
            SqlCommand cmdParticipantesListar = null;
            SqlDataReader drParticipantes;
            String sCadenaConexion;
            String sqlParticipantesListar;

            try
            {
                sCadenaConexion = Utilities.GetConnectionStringTalentos2();

                sqlParticipantesListar = "usp_TalListarParticipantes";

                Conn = new SqlConnection(sCadenaConexion);

                cmdParticipantesListar = Conn.CreateCommand();
                cmdParticipantesListar.CommandType = CommandType.StoredProcedure;
                cmdParticipantesListar.CommandText = sqlParticipantesListar;

                List<ParticipanteBE> lstParticipantes;
                ParticipanteBE objParticipanteBE;

                cmdParticipantesListar.Connection.Open();
                drParticipantes = cmdParticipantesListar.ExecuteReader();

                lstParticipantes = new List<ParticipanteBE>();

                while (drParticipantes.Read())
                {
                    objParticipanteBE = new ParticipanteBE();
                    objParticipanteBE.ParticipanteId = drParticipantes.GetInt32(drParticipantes.GetOrdinal("ParticipanteMasivoId"));
                    objParticipanteBE.Nombres = drParticipantes.GetString(drParticipantes.GetOrdinal("Nombres"));
                    objParticipanteBE.ApellidoPaterno = drParticipantes.GetString(drParticipantes.GetOrdinal("ApellidoPaterno"));
                    objParticipanteBE.ApellidoMaterno = drParticipantes.GetString(drParticipantes.GetOrdinal("ApellidoMaterno"));
                    objParticipanteBE.NivelInstruccion = drParticipantes.GetString(drParticipantes.GetOrdinal("NivelInstruccion"));
                    objParticipanteBE.Cargo = drParticipantes.GetString(drParticipantes.GetOrdinal("CargoEmpresa"));
                    objParticipanteBE.FechaNacimiento = drParticipantes.GetString(drParticipantes.GetOrdinal("FechaNacimiento"));
                    objParticipanteBE.Sexo = drParticipantes.GetString(drParticipantes.GetOrdinal("Sexo"));
                    objParticipanteBE.CorreoElectronico = drParticipantes.GetString(drParticipantes.GetOrdinal("CorreoElectronico"));
                    objParticipanteBE.Institucion = drParticipantes.GetString(drParticipantes.GetOrdinal("Institucion"));
                    objParticipanteBE.DNI = drParticipantes.GetString(drParticipantes.GetOrdinal("DNI"));
                    lstParticipantes.Add(objParticipanteBE);
                }

                cmdParticipantesListar.Connection.Close();
                cmdParticipantesListar.Dispose();
                Conn.Dispose();
                return lstParticipantes;
            }
            catch (Exception ex)
            {
                Conn.Dispose();
                cmdParticipantesListar.Dispose();

                throw;
            }
        }

        public string ObtenerPasswordParticipante(string DNI)
        {
            SqlConnection Conn = null;
            SqlCommand cmdParticipante = null;
            SqlDataReader drParticipante;
            String sCadenaConexion;
            String sqlParticipante;
            SqlParameter prmDNI;
            string password = "";

            try
            {
                sCadenaConexion = Utilities.GetConnectionStringTalentos2();

                sqlParticipante = "usp_TalObtenerPasswordParticipante";

                Conn = new SqlConnection(sCadenaConexion);

                cmdParticipante = Conn.CreateCommand();
                cmdParticipante.CommandType = CommandType.StoredProcedure;
                cmdParticipante.CommandText = sqlParticipante;

                prmDNI = new SqlParameter();
                prmDNI.ParameterName = "@DNI";
                prmDNI.SqlDbType = System.Data.SqlDbType.VarChar;
                prmDNI.Size = 8;
                prmDNI.Value = DNI;

                cmdParticipante.Parameters.Add(prmDNI);

                cmdParticipante.Connection.Open();
                drParticipante = cmdParticipante.ExecuteReader();

                while (drParticipante.Read())
                {
                    password = drParticipante.GetString(drParticipante.GetOrdinal("Password"));
                }

                cmdParticipante.Connection.Close();
                cmdParticipante.Dispose();
                Conn.Dispose();
                return password;
            }
            catch (Exception ex)
            {
                Conn.Dispose();
                cmdParticipante.Dispose();

                throw;
            }
        }

        public string ObtenerNombreParticipante(string DNI, ref string correoElectronico, ref string codigoEvaluacion)
        {
            SqlConnection Conn = null;
            SqlCommand cmdParticipante = null;
            SqlDataReader drParticipante;
            String sCadenaConexion;
            String sqlParticipante;
            SqlParameter prmDNI;
            string nombre = "";

            try
            {
                sCadenaConexion = Utilities.GetConnectionStringTalentos2();

                sqlParticipante = "usp_TalObtenerNombreParticipante";

                Conn = new SqlConnection(sCadenaConexion);

                cmdParticipante = Conn.CreateCommand();
                cmdParticipante.CommandType = CommandType.StoredProcedure;
                cmdParticipante.CommandText = sqlParticipante;

                prmDNI = new SqlParameter();
                prmDNI.ParameterName = "@DNI";
                prmDNI.SqlDbType = System.Data.SqlDbType.VarChar;
                prmDNI.Size = 8;
                prmDNI.Value = DNI;

                cmdParticipante.Parameters.Add(prmDNI);

                cmdParticipante.Connection.Open();
                drParticipante = cmdParticipante.ExecuteReader();

                while (drParticipante.Read())
                {
                    nombre = drParticipante.GetString(drParticipante.GetOrdinal("NombreParticipante"));
                    correoElectronico = drParticipante.GetString(drParticipante.GetOrdinal("CorreoElectronico"));
                    codigoEvaluacion = drParticipante.GetString(drParticipante.GetOrdinal("CodEvaluacion"));
                }

                cmdParticipante.Connection.Close();
                cmdParticipante.Dispose();
                Conn.Dispose();
                return nombre;
            }
            catch (Exception ex)
            {
                Conn.Dispose();
                cmdParticipante.Dispose();

                throw;
            }
        }


        public string ObtenerNombreParticipanteAdulto(string DNI, ref string correoElectronico, ref string codigoEvaluacion)
        {
            SqlConnection Conn = null;
            SqlCommand cmdParticipante = null;
            SqlDataReader drParticipante;
            String sCadenaConexion;
            String sqlParticipante;
            SqlParameter prmDNI;
            string nombre = "";

            try
            {
                sCadenaConexion = Utilities.GetConnectionStringTalentos2();

                sqlParticipante = "usp_TalObtenerNombreParticipanteAdulto";

                Conn = new SqlConnection(sCadenaConexion);

                cmdParticipante = Conn.CreateCommand();
                cmdParticipante.CommandType = CommandType.StoredProcedure;
                cmdParticipante.CommandText = sqlParticipante;

                prmDNI = new SqlParameter();
                prmDNI.ParameterName = "@DNI";
                prmDNI.SqlDbType = System.Data.SqlDbType.VarChar;
                prmDNI.Size = 8;
                prmDNI.Value = DNI;

                cmdParticipante.Parameters.Add(prmDNI);

                cmdParticipante.Connection.Open();
                drParticipante = cmdParticipante.ExecuteReader();

                while (drParticipante.Read())
                {
                    nombre = drParticipante.GetString(drParticipante.GetOrdinal("NombreParticipante"));
                    correoElectronico = drParticipante.GetString(drParticipante.GetOrdinal("CorreoElectronico"));
                    codigoEvaluacion = drParticipante.GetString(drParticipante.GetOrdinal("CodEvaluacion"));
                }

                cmdParticipante.Connection.Close();
                cmdParticipante.Dispose();
                Conn.Dispose();
                return nombre;
            }
            catch (Exception ex)
            {
                Conn.Dispose();
                cmdParticipante.Dispose();

                throw;
            }
        }

        public string ObtenerNombreParticipantexCodigo(string codigoEvaluacion)
        {
            SqlConnection Conn = null;
            SqlCommand cmdParticipante = null;
            SqlDataReader drParticipante;
            String sCadenaConexion;
            String sqlParticipante;
            SqlParameter prmCodigoEvaluacion;
            string nombre = "";

            try
            {
                sCadenaConexion = Utilities.GetConnectionStringTalentos2();

                sqlParticipante = "usp_TalObtenerNombreParticipantexCodigo";

                Conn = new SqlConnection(sCadenaConexion);

                cmdParticipante = Conn.CreateCommand();
                cmdParticipante.CommandType = CommandType.StoredProcedure;
                cmdParticipante.CommandText = sqlParticipante;

                prmCodigoEvaluacion = new SqlParameter();
                prmCodigoEvaluacion.ParameterName = "@CodEvaluacion";
                prmCodigoEvaluacion.SqlDbType = System.Data.SqlDbType.VarChar;
                prmCodigoEvaluacion.Size = 10;
                prmCodigoEvaluacion.Value = codigoEvaluacion;

                cmdParticipante.Parameters.Add(prmCodigoEvaluacion);

                cmdParticipante.Connection.Open();
                drParticipante = cmdParticipante.ExecuteReader();

                while (drParticipante.Read())
                {
                    nombre = drParticipante.GetString(drParticipante.GetOrdinal("NombreParticipante"));
                }

                cmdParticipante.Connection.Close();
                cmdParticipante.Dispose();
                Conn.Dispose();
                return nombre;
            }
            catch (Exception ex)
            {
                Conn.Dispose();
                cmdParticipante.Dispose();

                throw;
            }
        }


        public string ObtenerNombreParticipanteAdultoxCodigo(string codigoEvaluacion)
        {
            SqlConnection Conn = null;
            SqlCommand cmdParticipante = null;
            SqlDataReader drParticipante;
            String sCadenaConexion;
            String sqlParticipante;
            SqlParameter prmCodigoEvaluacion;
            string nombre = "";

            try
            {
                sCadenaConexion = Utilities.GetConnectionStringTalentos2();

                sqlParticipante = "usp_TalObtenerNombreParticipanteAdultoxCodigo";

                Conn = new SqlConnection(sCadenaConexion);

                cmdParticipante = Conn.CreateCommand();
                cmdParticipante.CommandType = CommandType.StoredProcedure;
                cmdParticipante.CommandText = sqlParticipante;

                prmCodigoEvaluacion = new SqlParameter();
                prmCodigoEvaluacion.ParameterName = "@CodEvaluacion";
                prmCodigoEvaluacion.SqlDbType = System.Data.SqlDbType.VarChar;
                prmCodigoEvaluacion.Size = 10;
                prmCodigoEvaluacion.Value = codigoEvaluacion;

                cmdParticipante.Parameters.Add(prmCodigoEvaluacion);

                cmdParticipante.Connection.Open();
                drParticipante = cmdParticipante.ExecuteReader();

                while (drParticipante.Read())
                {
                    nombre = drParticipante.GetString(drParticipante.GetOrdinal("NombreParticipante"));
                }

                cmdParticipante.Connection.Close();
                cmdParticipante.Dispose();
                Conn.Dispose();
                return nombre;
            }
            catch (Exception ex)
            {
                Conn.Dispose();
                cmdParticipante.Dispose();

                throw;
            }
        }




    }
}
