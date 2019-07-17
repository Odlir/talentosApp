using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UPC.Seguridad.Talentos.BL.BE;
using System.Data;
using System.Data.SqlClient;

namespace UPC.Seguridad.Talentos.DL.DALC
{
    public class UsuarioDALC
    {

        //public UsuarioBE UsuarioValidar(UsuarioBE objUsuarioBE)
        //{
        //    try
        //    {
        //       // String sCadena = "server=(local); database=db_NetFSeguridadTalentos; Integrated Security=true";
        //        String sCadena = Utilities.GetConnectionStringSeguridad();

        //        String sqlUsuarioValidar;

        //        SqlDataReader drUsuarioValidar;

        //        sqlUsuarioValidar = "usps_TalValidarUsuario";
        //        SqlParameter[] ArrSqlParameter = new SqlParameter[2];

        //        ArrSqlParameter[0] = new SqlParameter();
        //        ArrSqlParameter[0].ParameterName = "@NickName";
        //        ArrSqlParameter[0].SqlDbType = SqlDbType.VarChar;
        //        ArrSqlParameter[0].Size = 50;
        //        //ArrSqlParameter[0].Value = objUsuarioBE.NickName;
        //        ArrSqlParameter[0].Value = objUsuarioBE.Correo;

        //        ArrSqlParameter[1] = new SqlParameter();
        //        ArrSqlParameter[1].ParameterName = "@Password";
        //        ArrSqlParameter[1].SqlDbType = SqlDbType.VarChar;
        //        ArrSqlParameter[1].Size = 100;
        //        ArrSqlParameter[1].Value = objUsuarioBE.Password;



        //        drUsuarioValidar = SqlHelper.ExecuteReader(sCadena, CommandType.StoredProcedure, sqlUsuarioValidar, ArrSqlParameter);
        //        //return drUsuarioValidar;
        //        UsuarioBE OBJUSER = new UsuarioBE();
        //        if (drUsuarioValidar.Read())
        //        {

        //            OBJUSER.UsuarioId = Convert.ToInt16(drUsuarioValidar.GetValue(drUsuarioValidar.GetOrdinal("Usuario_id")));
        //            OBJUSER.Nombres = drUsuarioValidar.GetValue(drUsuarioValidar.GetOrdinal("Nombres")).ToString();
        //            OBJUSER.APaterno = drUsuarioValidar.GetValue(drUsuarioValidar.GetOrdinal("Apellidos")).ToString();
        //            OBJUSER.AMaterno = drUsuarioValidar.GetValue(drUsuarioValidar.GetOrdinal("Apellidos")).ToString();
        //            OBJUSER.Carrera = drUsuarioValidar.GetValue(drUsuarioValidar.GetOrdinal("Carrera")).ToString();
        //            OBJUSER.Edad = Convert.ToInt16(drUsuarioValidar.GetValue(drUsuarioValidar.GetOrdinal("Edad")));
        //            OBJUSER.Email = drUsuarioValidar.GetValue(drUsuarioValidar.GetOrdinal("Email")).ToString();
        //            OBJUSER.Telefono = drUsuarioValidar.GetValue(drUsuarioValidar.GetOrdinal("Telefono")).ToString();
        //            OBJUSER.RolID =Convert.ToInt16(drUsuarioValidar.GetValue(drUsuarioValidar.GetOrdinal("Rol_id")));

        //            OBJUSER.NickName = drUsuarioValidar.GetValue(drUsuarioValidar.GetOrdinal("NickName")).ToString();
                    
        //        }
        //        return OBJUSER;

        //    }
        //    catch (Exception ex)
        //    {
        //        throw;
        //    }
        //}

        //public UsuarioBE UsuarioValidarNickName(UsuarioBE objUsuarioBE)
        //{
        //    try
        //    {
        //        //String sCadena = "server=(local); database=db_NetFSeguridadTalentos; Integrated Security=true";
        //        String sCadena = Utilities.GetConnectionStringSeguridad();

        //        String sqlUsuarioValidarNickName;

        //        SqlDataReader drUsuarioValidar;

        //        sqlUsuarioValidarNickName = "usps_TalBuscarUsuarioxNickName";
        //        SqlParameter[] ArrSqlParameter = new SqlParameter[1];

        //        ArrSqlParameter[0] = new SqlParameter();
        //        ArrSqlParameter[0].ParameterName = "@NickName";
        //        ArrSqlParameter[0].SqlDbType = SqlDbType.VarChar;
        //        ArrSqlParameter[0].Size = 50;
        //        ArrSqlParameter[0].Value = objUsuarioBE.NickName;



        //        drUsuarioValidar = SqlHelper.ExecuteReader(sCadena, CommandType.StoredProcedure, sqlUsuarioValidarNickName, ArrSqlParameter);
        //        //return drUsuarioValidar;
        //        UsuarioBE OBJUSER = new UsuarioBE();
        //        if (drUsuarioValidar.Read())
        //        {

        //            OBJUSER.UsuarioID = Convert.ToInt16(drUsuarioValidar.GetValue(drUsuarioValidar.GetOrdinal("Usuario_id")));
        //            OBJUSER.Nombres = drUsuarioValidar.GetValue(drUsuarioValidar.GetOrdinal("Nombres")).ToString();
        //            OBJUSER.Apellidos = drUsuarioValidar.GetValue(drUsuarioValidar.GetOrdinal("Apellidos")).ToString();
        //            OBJUSER.Carrera = drUsuarioValidar.GetValue(drUsuarioValidar.GetOrdinal("Carrera")).ToString();
        //            OBJUSER.Edad = Convert.ToInt16(drUsuarioValidar.GetValue(drUsuarioValidar.GetOrdinal("Edad")));
        //            OBJUSER.Email = drUsuarioValidar.GetValue(drUsuarioValidar.GetOrdinal("Email")).ToString();
        //            OBJUSER.Telefono = drUsuarioValidar.GetValue(drUsuarioValidar.GetOrdinal("Telefono")).ToString();
        //            OBJUSER.RolID = Convert.ToInt16(drUsuarioValidar.GetValue(drUsuarioValidar.GetOrdinal("Rol_id")));

        //            OBJUSER.NickName = drUsuarioValidar.GetValue(drUsuarioValidar.GetOrdinal("NickName")).ToString();

        //        }
        //        return OBJUSER;

        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }
        //}

        //public UsuarioBE UsuarioValidarLog(UsuarioBE objUsuarioBE)
        //{
        //    try
        //    {
        //     //   String sCadena = "server=(local); database=db_NetFSeguridadTalentos; Integrated Security=true";
        //        String sCadena = Utilities.GetConnectionStringSeguridad();

        //        String sqlUsuarioValidarLog;

        //        SqlDataReader drUsuarioValidar;

        //        sqlUsuarioValidarLog = "usps_TalenLogUsuario";
        //        SqlParameter[] ArrSqlParameter = new SqlParameter[2];

        //        ArrSqlParameter[0] = new SqlParameter();
        //        ArrSqlParameter[0].ParameterName = "@NickName";
        //        ArrSqlParameter[0].SqlDbType = SqlDbType.VarChar;
        //        ArrSqlParameter[0].Size = 50;
        //        ArrSqlParameter[0].Value = objUsuarioBE.NickName;

        //        ArrSqlParameter[1] = new SqlParameter();
        //        ArrSqlParameter[1].ParameterName = "@fecha";
        //        ArrSqlParameter[1].SqlDbType = SqlDbType.DateTime;
        //        ArrSqlParameter[1].Value = DateTime.Now;



        //        drUsuarioValidar = SqlHelper.ExecuteReader(sCadena, CommandType.StoredProcedure, sqlUsuarioValidarLog, ArrSqlParameter);
        //        //return drUsuarioValidar;
                
        //        UsuarioBE OBJUSER = new UsuarioBE();
        //        if (drUsuarioValidar.Read())
        //        {

        //            //OBJUSER.UsuarioID = Convert.ToInt16(drUsuarioValidar.GetValue(drUsuarioValidar.GetOrdinal("Usuario_id")));
        //            //OBJUSER.Nombres = drUsuarioValidar.GetValue(drUsuarioValidar.GetOrdinal("Nombres")).ToString();
        //            //OBJUSER.Apellidos = drUsuarioValidar.GetValue(drUsuarioValidar.GetOrdinal("Apellidos")).ToString();
        //            //OBJUSER.Carrera = drUsuarioValidar.GetValue(drUsuarioValidar.GetOrdinal("Carrera")).ToString();
        //            //OBJUSER.Edad = Convert.ToInt16(drUsuarioValidar.GetValue(drUsuarioValidar.GetOrdinal("Edad")).ToString());
        //            //OBJUSER.Email = drUsuarioValidar.GetValue(drUsuarioValidar.GetOrdinal("Email")).ToString();
        //            //OBJUSER.Telefono = drUsuarioValidar.GetValue(drUsuarioValidar.GetOrdinal("Telefono")).ToString();
        //            //OBJUSER.RolID = Convert.ToInt16(drUsuarioValidar.GetValue(drUsuarioValidar.GetOrdinal("Rol_id")).ToString());

        //            OBJUSER.NickName = drUsuarioValidar.GetValue(drUsuarioValidar.GetOrdinal("NickName")).ToString();
                    
        //        }
        //        return OBJUSER;

        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }
        //}

        //public int UsuarioInsertar(UsuarioBE objUsuarioBE)
        //{
        //    try
        //    {
        //        //String sCadena = "server=(local); database=db_NetFSeguridadTalentos; Integrated Security=true";
        //        String sCadena = Utilities.GetConnectionStringSeguridad();

        //        String sqlUsuarioValidar;

        //        sqlUsuarioValidar = "uspi_TalUsuario";
        //        SqlParameter[] ArrSqlParameter = new SqlParameter[10];

        //        ArrSqlParameter[0] = new SqlParameter();
        //        ArrSqlParameter[0].ParameterName = "@Nombres";
        //        ArrSqlParameter[0].SqlDbType = SqlDbType.VarChar;
        //        ArrSqlParameter[0].Size = 100;
        //        ArrSqlParameter[0].Value = objUsuarioBE.Nombres;

        //        ArrSqlParameter[1] = new SqlParameter();
        //        ArrSqlParameter[1].ParameterName = "@Apellidos";
        //        ArrSqlParameter[1].SqlDbType = SqlDbType.VarChar;
        //        ArrSqlParameter[1].Size = 100;
        //        ArrSqlParameter[1].Value = objUsuarioBE.Apellidos;

        //        ArrSqlParameter[2] = new SqlParameter();
        //        ArrSqlParameter[2].ParameterName = "@NickName";
        //        ArrSqlParameter[2].SqlDbType = SqlDbType.VarChar;
        //        ArrSqlParameter[2].Size = 50;
        //        ArrSqlParameter[2].Value = objUsuarioBE.NickName;

        //        ArrSqlParameter[3] = new SqlParameter();
        //        ArrSqlParameter[3].ParameterName = "@Password";
        //        ArrSqlParameter[3].SqlDbType = SqlDbType.VarChar;
        //        ArrSqlParameter[3].Size = 100;
        //        ArrSqlParameter[3].Value = objUsuarioBE.Password;

        //        ArrSqlParameter[4] = new SqlParameter();
        //        ArrSqlParameter[4].ParameterName = "@Carrera";
        //        ArrSqlParameter[4].SqlDbType = SqlDbType.VarChar;
        //        ArrSqlParameter[4].Size = 100;
        //        ArrSqlParameter[4].Value = objUsuarioBE.Carrera;

        //        ArrSqlParameter[5] = new SqlParameter();
        //        ArrSqlParameter[5].ParameterName = "@Email";
        //        ArrSqlParameter[5].SqlDbType = SqlDbType.VarChar;
        //        ArrSqlParameter[5].Size = 100;
        //        ArrSqlParameter[5].Value = objUsuarioBE.Email;

        //        ArrSqlParameter[6] = new SqlParameter();
        //        ArrSqlParameter[6].ParameterName = "@Telefono";
        //        ArrSqlParameter[6].SqlDbType = SqlDbType.VarChar;
        //        ArrSqlParameter[6].Size = 50;
        //        ArrSqlParameter[6].Value = objUsuarioBE.Telefono;

        //        ArrSqlParameter[7] = new SqlParameter();
        //        ArrSqlParameter[7].ParameterName = "@Edad";
        //        ArrSqlParameter[7].SqlDbType = SqlDbType.Int;         
        //        ArrSqlParameter[7].Value = objUsuarioBE.Edad;

        //        ArrSqlParameter[8] = new SqlParameter();
        //        ArrSqlParameter[8].ParameterName = "@Rol_id";
        //        ArrSqlParameter[8].SqlDbType = SqlDbType.Int;         
        //        ArrSqlParameter[8].Value = objUsuarioBE.RolID;

        //        ArrSqlParameter[9]= new SqlParameter();
        //        ArrSqlParameter[9].ParameterName = "@Usuario_id";
        //        ArrSqlParameter[9].SqlDbType = SqlDbType.Int;
        //        ArrSqlParameter[9].Direction = ParameterDirection.ReturnValue;


        //        SqlHelper.ExecuteNonQuery(sCadena, CommandType.StoredProcedure, sqlUsuarioValidar, ArrSqlParameter);

        //        int icod = (int)ArrSqlParameter[9].Value;


        //        return icod;

        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }
        //}

        //public int LogInsertar(UsuarioBE objUsuarioBE)
        //{
        //    try
        //    {
        //        //String sCadena = "server=(local); database=db_NetFSeguridadTalentos; Integrated Security=true";
        //        String sCadena = Utilities.GetConnectionStringSeguridad();

        //        String sqlLogInsertar;

        //        sqlLogInsertar = "uspi_TalGuardarLog";
        //        SqlParameter[] ArrSqlParameter = new SqlParameter[4];


        //        ArrSqlParameter[0] = new SqlParameter();
        //        ArrSqlParameter[0].ParameterName = "@NickName";
        //        ArrSqlParameter[0].SqlDbType = SqlDbType.VarChar;
        //        ArrSqlParameter[0].Size = 50;
        //        ArrSqlParameter[0].Value = objUsuarioBE.NickName;

        //        ArrSqlParameter[1] = new SqlParameter();
        //        ArrSqlParameter[1].ParameterName = "@FechaLog";
        //        ArrSqlParameter[1].SqlDbType = SqlDbType.DateTime;
        //        ArrSqlParameter[1].Value = DateTime.Now;

        //        ArrSqlParameter[2] = new SqlParameter();
        //        ArrSqlParameter[2].ParameterName = "@NextLog";
        //        ArrSqlParameter[2].SqlDbType = SqlDbType.DateTime;
        //        ArrSqlParameter[2].Value = DateTime.Now.AddMonths(6);

        //        ArrSqlParameter[3] = new SqlParameter();
        //        ArrSqlParameter[3].ParameterName = "@Log_id";
        //        ArrSqlParameter[3].SqlDbType = SqlDbType.Int;
        //        ArrSqlParameter[3].Direction = ParameterDirection.ReturnValue;


        //        SqlHelper.ExecuteNonQuery(sCadena, CommandType.StoredProcedure, sqlLogInsertar, ArrSqlParameter);

        //        int icod = (int)ArrSqlParameter[3].Value;


        //        return icod;

        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }
        //}

        public List<UsuarioBE> ObtenerFamosoPorCarrera(int idCarrera)
        {
            try
            {
                // String sCadena = "server=(local); database=db_NetFSeguridadTalentos; Integrated Security=true";
                String sCadena = Utilities.GetConnectionStringSeguridad();

                String sqlUsuarioValidar;

                SqlDataReader drUsuarioValidar;

                sqlUsuarioValidar = "usps_TalParticipantePorCarrera";
                SqlParameter[] ArrSqlParameter = new SqlParameter[2];

                ArrSqlParameter[0] = new SqlParameter();
                ArrSqlParameter[0].ParameterName = "@CarreraId";
                ArrSqlParameter[0].SqlDbType = SqlDbType.Int;
                
                ArrSqlParameter[0].Value = idCarrera;

                drUsuarioValidar = SqlHelper.ExecuteReader(sCadena, CommandType.StoredProcedure, sqlUsuarioValidar, ArrSqlParameter);
                //return drUsuarioValidar;
                UsuarioBE OBJUSER = new UsuarioBE();
                List<UsuarioBE> list = new List<UsuarioBE>();
                while (drUsuarioValidar.Read())
                {
                    OBJUSER = new UsuarioBE();
                    OBJUSER.UsuarioId = Convert.ToInt16(drUsuarioValidar.GetValue(drUsuarioValidar.GetOrdinal("ParticipanteId")));
                    OBJUSER.Nombres = drUsuarioValidar.GetValue(drUsuarioValidar.GetOrdinal("Nombres")).ToString();
                    OBJUSER.APaterno = drUsuarioValidar.GetValue(drUsuarioValidar.GetOrdinal("ApellidoPaterno")).ToString();
                    OBJUSER.AMaterno = drUsuarioValidar.GetValue(drUsuarioValidar.GetOrdinal("ApellidoMaterno")).ToString();
                    OBJUSER.Sexo = Convert.ToInt16(drUsuarioValidar.GetValue(drUsuarioValidar.GetOrdinal("Sexo")));
                    OBJUSER.CarreraId = Convert.ToInt16(drUsuarioValidar.GetValue(drUsuarioValidar.GetOrdinal("CarreraId")));

                    list.Add(OBJUSER);

                }
                return list;

            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public List<UsuarioBE> ObtenerFamosoPorCarrerasId(string idCarreras)
        {
            try
            {
                // String sCadena = "server=(local); database=db_NetFSeguridadTalentos; Integrated Security=true";
                String sCadena = Utilities.GetConnectionStringSeguridad();

                String sqlUsuarioValidar;

                SqlDataReader drUsuarioValidar;

                sqlUsuarioValidar = "usps_TalParticipantePorCarrerasId";
                SqlParameter[] ArrSqlParameter = new SqlParameter[2];

                ArrSqlParameter[0] = new SqlParameter();
                ArrSqlParameter[0].ParameterName = "@CarrerasId";
                ArrSqlParameter[0].SqlDbType = SqlDbType.VarChar;
                ArrSqlParameter[0].Size = 250;

                ArrSqlParameter[0].Value = idCarreras;

                drUsuarioValidar = SqlHelper.ExecuteReader(sCadena, CommandType.StoredProcedure, sqlUsuarioValidar, ArrSqlParameter);
                //return drUsuarioValidar;
                UsuarioBE OBJUSER = new UsuarioBE();
                List<UsuarioBE> list = new List<UsuarioBE>();
                while (drUsuarioValidar.Read())
                {
                    OBJUSER = new UsuarioBE();
                    OBJUSER.UsuarioId = Convert.ToInt16(drUsuarioValidar.GetValue(drUsuarioValidar.GetOrdinal("ParticipanteId")));
                    OBJUSER.Nombres = drUsuarioValidar.GetValue(drUsuarioValidar.GetOrdinal("Nombres")).ToString();
                    OBJUSER.APaterno = drUsuarioValidar.GetValue(drUsuarioValidar.GetOrdinal("ApellidoPaterno")).ToString();
                    OBJUSER.AMaterno = drUsuarioValidar.GetValue(drUsuarioValidar.GetOrdinal("ApellidoMaterno")).ToString();
                    OBJUSER.Sexo = Convert.ToInt16(drUsuarioValidar.GetValue(drUsuarioValidar.GetOrdinal("Sexo")));
                    OBJUSER.CarreraId = Convert.ToInt16(drUsuarioValidar.GetValue(drUsuarioValidar.GetOrdinal("CarreraId")));

                    list.Add(OBJUSER);

                }
                return list;

            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public UsuarioBE ObtenerParticipante(String correo, String password)
        {

            String SqlObtnerParticipante;
            string sCadena;
            SqlConnection Conn;
            SqlCommand cmdObtnerParticipante = null;
            UsuarioBE objParticipante;
            SqlDataReader drObtenerParticipante;

            try
            {
                sCadena = Utilities.GetConnectionStringSeguridad();
                Conn = new SqlConnection(sCadena);

                SqlObtnerParticipante = "usp_TalObtenerParticipante";
                cmdObtnerParticipante = Conn.CreateCommand();
                cmdObtnerParticipante.CommandType = System.Data.CommandType.StoredProcedure;
                cmdObtnerParticipante.CommandText = SqlObtnerParticipante;

                SqlParameter prmCorreo = new SqlParameter();
                prmCorreo.ParameterName = "@correo";
                prmCorreo.SqlDbType = System.Data.SqlDbType.VarChar;
                prmCorreo.Size = 100;
                prmCorreo.Value = correo;


                SqlParameter prmPassword = new SqlParameter();
                prmPassword.ParameterName = "@pwd";
                prmPassword.SqlDbType = System.Data.SqlDbType.VarChar;
                prmPassword.Size = 10;
                prmPassword.Value = password;

                cmdObtnerParticipante.Parameters.Add(prmCorreo);
                cmdObtnerParticipante.Parameters.Add(prmPassword);

                cmdObtnerParticipante.Connection.Open();
                drObtenerParticipante = cmdObtnerParticipante.ExecuteReader();

                objParticipante = new UsuarioBE();

                while (drObtenerParticipante.Read())
                {

                    objParticipante.UsuarioId = drObtenerParticipante.GetInt32(drObtenerParticipante.GetOrdinal("ParticipanteId"));
                    objParticipante.Nombres = drObtenerParticipante.GetString(drObtenerParticipante.GetOrdinal("Nombres"));
                    objParticipante.APaterno = drObtenerParticipante.GetString(drObtenerParticipante.GetOrdinal("ApellidoPaterno"));
                    objParticipante.AMaterno = drObtenerParticipante.GetString(drObtenerParticipante.GetOrdinal("ApellidoMaterno"));
                    objParticipante.TipoParticipanteId = drObtenerParticipante.GetInt32(drObtenerParticipante.GetOrdinal("TipoParticipanteId"));
                    objParticipante.Sexo = drObtenerParticipante.GetInt32(drObtenerParticipante.GetOrdinal("Sexo"));
                }


                cmdObtnerParticipante.Connection.Close();
                cmdObtnerParticipante.Dispose();
                Conn.Dispose();

                return objParticipante;


            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public int InsertarParticipante(UsuarioBE objParticipanteBE)
        {

            String SqlInsertarParticipante;
            string sCadena;
            SqlConnection Conn;
            SqlCommand cmdInsertarParticipante = null;
            int iparticipanteid = 0;

            try
            {
                sCadena = Utilities.GetConnectionStringSeguridad();
                Conn = new SqlConnection(sCadena);

                SqlInsertarParticipante = "usp_TalInsertarParticipante";
                cmdInsertarParticipante = Conn.CreateCommand();
                cmdInsertarParticipante.CommandType = System.Data.CommandType.StoredProcedure;
                cmdInsertarParticipante.CommandText = SqlInsertarParticipante;

                SqlParameter prmParticipanteId = new SqlParameter();
                prmParticipanteId.SqlDbType = System.Data.SqlDbType.Int;
                prmParticipanteId.Direction = System.Data.ParameterDirection.ReturnValue;

                SqlParameter prmNombres = new SqlParameter();
                prmNombres.ParameterName = "@nombres";
                prmNombres.SqlDbType = System.Data.SqlDbType.VarChar;
                prmNombres.Size = 200;
                prmNombres.Value = objParticipanteBE.Nombres;

                SqlParameter prmApPaterno = new SqlParameter();
                prmApPaterno.ParameterName = "@appaterno";
                prmApPaterno.SqlDbType = System.Data.SqlDbType.VarChar;
                prmApPaterno.Size = 200;
                prmApPaterno.Value = objParticipanteBE.APaterno;

                SqlParameter prmApMaterno = new SqlParameter();
                prmApMaterno.ParameterName = "@apmaterno";
                prmApMaterno.SqlDbType = System.Data.SqlDbType.VarChar;
                prmApMaterno.Size = 200;
                prmApMaterno.Value = objParticipanteBE.AMaterno;

                SqlParameter prmSexo = new SqlParameter();
                prmSexo.ParameterName = "@sexo";
                prmSexo.SqlDbType = System.Data.SqlDbType.Int;
                prmSexo.Value = objParticipanteBE.Sexo;

                SqlParameter prmCorreo = new SqlParameter();
                prmCorreo.ParameterName = "@correo";
                prmCorreo.SqlDbType = System.Data.SqlDbType.VarChar;
                prmCorreo.Size = 100;
                prmCorreo.Value = objParticipanteBE.Correo;

                SqlParameter prmPassword = new SqlParameter();
                prmPassword.ParameterName = "@password";
                prmPassword.SqlDbType = System.Data.SqlDbType.VarChar;
                prmPassword.Size = 50;
                prmPassword.Value = objParticipanteBE.Password;

                SqlParameter prmFechaNac = new SqlParameter();
                prmFechaNac.ParameterName = "@fechanacimiento";
                prmFechaNac.SqlDbType = System.Data.SqlDbType.DateTime;
                prmFechaNac.Value = objParticipanteBE.FechaNac;

                SqlParameter prmDireccion = new SqlParameter();
                prmDireccion.ParameterName = "@direccion";
                prmDireccion.SqlDbType = System.Data.SqlDbType.VarChar;
                prmDireccion.Size = 200;
                prmDireccion.Value = objParticipanteBE.Direccion;

                SqlParameter prmTelefonoFijo = new SqlParameter();
                prmTelefonoFijo.ParameterName = "@telefonofijo";
                prmTelefonoFijo.SqlDbType = System.Data.SqlDbType.VarChar;
                prmTelefonoFijo.Size = 15;
                prmTelefonoFijo.Value = objParticipanteBE.TelfFijo;

                SqlParameter prmTelefonoMovil = new SqlParameter();
                prmTelefonoMovil.ParameterName = "@telefonomovil";
                prmTelefonoMovil.SqlDbType = System.Data.SqlDbType.VarChar;
                prmTelefonoMovil.Size = 15;
                prmTelefonoMovil.Value = objParticipanteBE.TelfMovil;


                SqlParameter prmNivelInstruccion = new SqlParameter();
                prmNivelInstruccion.ParameterName = "@NivelInstrucId";
                prmNivelInstruccion.SqlDbType = System.Data.SqlDbType.Int;
                prmNivelInstruccion.Value = objParticipanteBE.NivelInstruccionId;

                SqlParameter prmTipoParticipante = new SqlParameter();
                prmTipoParticipante.ParameterName = "@tipoparticipanteId";
                prmTipoParticipante.SqlDbType = System.Data.SqlDbType.Int;
                prmTipoParticipante.Value = objParticipanteBE.TipoParticipanteId;


                SqlParameter prmCarrera = new SqlParameter();
                prmCarrera.ParameterName = "@carrera";
                prmCarrera.SqlDbType = System.Data.SqlDbType.Int;
                prmCarrera.Value = objParticipanteBE.CarreraId;


                SqlParameter prmCentroEstudio = new SqlParameter();
                prmCentroEstudio.ParameterName = "@centroEstudio";
                prmCentroEstudio.SqlDbType = System.Data.SqlDbType.VarChar;
                prmCentroEstudio.Size = 200;
                prmCentroEstudio.Value = objParticipanteBE.CentroEstudio;

                SqlParameter prmDistritoEstudioId = new SqlParameter();
                prmDistritoEstudioId.ParameterName = "@distritoEstudioId";
                prmDistritoEstudioId.SqlDbType = System.Data.SqlDbType.Int;
                prmDistritoEstudioId.Value = objParticipanteBE.DistritoEstudioId;

                SqlParameter prmCentroTrabajo = new SqlParameter();
                prmCentroTrabajo.ParameterName = "@CentroTrabajo";
                prmCentroTrabajo.SqlDbType = System.Data.SqlDbType.VarChar;
                prmCentroTrabajo.Size = 200;
                prmCentroTrabajo.Value = objParticipanteBE.CentroTrabajo;

                SqlParameter prmDireccionCT = new SqlParameter();
                prmDireccionCT.ParameterName = "@direccionCT";
                prmDireccionCT.SqlDbType = System.Data.SqlDbType.VarChar;
                prmDireccionCT.Size = 200;
                prmDireccionCT.Value = objParticipanteBE.DireccionCentroTrabajo;

                SqlParameter prmDistritoTrabajoId = new SqlParameter();
                prmDistritoTrabajoId.ParameterName = "@distritoTrabajoId";
                prmDistritoTrabajoId.SqlDbType = System.Data.SqlDbType.Int;
                prmDistritoTrabajoId.Value = objParticipanteBE.DistritoTrabajoId;

                SqlParameter prmTelefonoTrabajo = new SqlParameter();
                prmTelefonoTrabajo.ParameterName = "@telfTrabajo";
                prmTelefonoTrabajo.SqlDbType = System.Data.SqlDbType.VarChar;
                prmTelefonoTrabajo.Size = 15;
                prmTelefonoTrabajo.Value = objParticipanteBE.TelfTrabajo;

                SqlParameter prmCorreoTrabajo = new SqlParameter();
                prmCorreoTrabajo.ParameterName = "@correoTrabajo";
                prmCorreoTrabajo.SqlDbType = System.Data.SqlDbType.VarChar;
                prmCorreoTrabajo.Size = 100;
                prmCorreoTrabajo.Value = objParticipanteBE.CorreoTrabajo;

                SqlParameter prmCargoTrabajo = new SqlParameter();
                prmCargoTrabajo.ParameterName = "@cargoTrabajo";
                prmCargoTrabajo.SqlDbType = System.Data.SqlDbType.VarChar;
                prmCargoTrabajo.Size = 200;
                prmCargoTrabajo.Value = objParticipanteBE.CargoTrabajo;

                SqlParameter prmOcupacionId = new SqlParameter();
                prmOcupacionId.ParameterName = "@OcupacionId";
                prmOcupacionId.SqlDbType = System.Data.SqlDbType.Int;
                prmOcupacionId.Value = objParticipanteBE.OcupacionId;

                cmdInsertarParticipante.Parameters.Add(prmParticipanteId);
                cmdInsertarParticipante.Parameters.Add(prmNombres);
                cmdInsertarParticipante.Parameters.Add(prmApPaterno);
                cmdInsertarParticipante.Parameters.Add(prmApMaterno);
                cmdInsertarParticipante.Parameters.Add(prmSexo);
                cmdInsertarParticipante.Parameters.Add(prmCorreo);
                cmdInsertarParticipante.Parameters.Add(prmPassword);
                cmdInsertarParticipante.Parameters.Add(prmFechaNac);
                cmdInsertarParticipante.Parameters.Add(prmDireccion);
                cmdInsertarParticipante.Parameters.Add(prmTelefonoFijo);
                cmdInsertarParticipante.Parameters.Add(prmTelefonoMovil);
                cmdInsertarParticipante.Parameters.Add(prmNivelInstruccion);
                cmdInsertarParticipante.Parameters.Add(prmTipoParticipante);
                cmdInsertarParticipante.Parameters.Add(prmCarrera);
                cmdInsertarParticipante.Parameters.Add(prmCentroEstudio);
                cmdInsertarParticipante.Parameters.Add(prmDistritoEstudioId);
                cmdInsertarParticipante.Parameters.Add(prmCentroTrabajo);
                cmdInsertarParticipante.Parameters.Add(prmDireccionCT);
                cmdInsertarParticipante.Parameters.Add(prmDistritoTrabajoId);
                cmdInsertarParticipante.Parameters.Add(prmTelefonoTrabajo);
                cmdInsertarParticipante.Parameters.Add(prmCorreoTrabajo);
                cmdInsertarParticipante.Parameters.Add(prmCargoTrabajo);
                cmdInsertarParticipante.Parameters.Add(prmOcupacionId);

                cmdInsertarParticipante.Connection.Open();
                cmdInsertarParticipante.ExecuteNonQuery();

                iparticipanteid = Convert.ToInt32(prmParticipanteId.Value);
                cmdInsertarParticipante.Connection.Close();
                cmdInsertarParticipante.Dispose();
                Conn.Dispose();

                return iparticipanteid;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public int ValidarEmailUnico(string mail)
        {
            String SqlObtnerParticipante;
            string sCadena;
            SqlConnection Conn;
            SqlCommand cmdObtnerParticipante = null;
            UsuarioBE objParticipante;
            SqlDataReader drObtenerParticipante;

            try
            {
                sCadena = Utilities.GetConnectionStringSeguridad();
                Conn = new SqlConnection(sCadena);

                SqlObtnerParticipante = "usps_ValidarEmailUnico";
                cmdObtnerParticipante = Conn.CreateCommand();
                cmdObtnerParticipante.CommandType = System.Data.CommandType.StoredProcedure;
                cmdObtnerParticipante.CommandText = SqlObtnerParticipante;

                SqlParameter prmCorreo = new SqlParameter();
                prmCorreo.ParameterName = "@correo";
                prmCorreo.SqlDbType = System.Data.SqlDbType.VarChar;
                prmCorreo.Size = 100;
                prmCorreo.Value = mail;

                cmdObtnerParticipante.Parameters.Add(prmCorreo);
                
                cmdObtnerParticipante.Connection.Open();
                drObtenerParticipante = cmdObtnerParticipante.ExecuteReader();

                int res = 0;
                if (drObtenerParticipante.Read())
                res = drObtenerParticipante.GetInt32(drObtenerParticipante.GetOrdinal("existe"));

                cmdObtnerParticipante.Connection.Close();
                cmdObtnerParticipante.Dispose();
                Conn.Dispose();

                return res;
            }
            catch (Exception ex)
            {
                return -1;
            }
        }

        public UsuarioBE LoginMasivo(string mail, string password)
        {
            String spLogin;
            string sCadena;
            SqlConnection Conn = null;
            SqlCommand cmdObtnerParticipante = null;
            SqlDataReader drObtenerParticipante;
            SqlParameter prmMail;
            SqlParameter prmPassword;
            UsuarioBE objUsuarioBE = null;

            spLogin = "usp_TalObtenerParticipanteMasivo";

            try
            {
                sCadena = Utilities.GetConnectionStringSeguridad();
                Conn = new SqlConnection(sCadena);
                
                cmdObtnerParticipante = Conn.CreateCommand();
                cmdObtnerParticipante.CommandType = System.Data.CommandType.StoredProcedure;
                cmdObtnerParticipante.CommandText = spLogin;

                prmMail = new SqlParameter();
                prmMail.ParameterName = "@mail";
                prmMail.SqlDbType = System.Data.SqlDbType.VarChar;
                prmMail.Size = 150;
                prmMail.Value = mail;

                prmPassword = new SqlParameter();
                prmPassword.ParameterName = "@password";
                prmPassword.SqlDbType = System.Data.SqlDbType.VarChar;
                prmPassword.Size = 50;
                prmPassword.Value = password;

                cmdObtnerParticipante.Parameters.Add(prmMail);
                cmdObtnerParticipante.Parameters.Add(prmPassword);

                cmdObtnerParticipante.Connection.Open();
                drObtenerParticipante = cmdObtnerParticipante.ExecuteReader();

                if (drObtenerParticipante.Read())
                {
                    objUsuarioBE = new UsuarioBE();

                    objUsuarioBE.UsuarioId = drObtenerParticipante.GetInt32(drObtenerParticipante.GetOrdinal("ParticipanteMasivoId"));
                    objUsuarioBE.Nombres = drObtenerParticipante.GetString(drObtenerParticipante.GetOrdinal("Nombres"));
                    objUsuarioBE.APaterno = drObtenerParticipante.GetString(drObtenerParticipante.GetOrdinal("ApellidoPaterno"));
                    objUsuarioBE.AMaterno = drObtenerParticipante.GetString(drObtenerParticipante.GetOrdinal("ApellidoMaterno"));
                    objUsuarioBE.Sexo = drObtenerParticipante.GetInt32(drObtenerParticipante.GetOrdinal("Sexo"));
                    objUsuarioBE.FechaNac = drObtenerParticipante.GetDateTime(drObtenerParticipante.GetOrdinal("FechaNacimiento"));
                    objUsuarioBE.NivelInstruccionId = drObtenerParticipante.GetInt32(drObtenerParticipante.GetOrdinal("NivelInstruccion"));
                    objUsuarioBE.CargoTrabajo = drObtenerParticipante.GetString(drObtenerParticipante.GetOrdinal("CargoEmpresa"));
                    objUsuarioBE.Correo = drObtenerParticipante.GetString(drObtenerParticipante.GetOrdinal("CorreoElectronico"));
                    objUsuarioBE.CentroEstudio = drObtenerParticipante.GetString(drObtenerParticipante.GetOrdinal("Institucion"));
                }

                cmdObtnerParticipante.Connection.Close();
                cmdObtnerParticipante.Dispose();
                Conn.Dispose();
            }
            catch (Exception ex)
            {
                cmdObtnerParticipante.Connection.Close();
                cmdObtnerParticipante.Dispose();
                Conn.Dispose();
            }

            return objUsuarioBE;
        }
    }
}
