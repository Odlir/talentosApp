using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UPC.Talentos.BL.BE;
using System.Data.SqlClient;
using System.Data;

namespace UPC.Talentos.DL.DALC
{
    public class TendenciaDALC
    {
        public TendenciaBE TendenciaObtener(int codTendencia)
        {

            SqlConnection conn = null;
            SqlCommand cmdTendenciaObtener = null;
            SqlParameter prmCodTendencia = null;

            String sCadenaConexion;
            String sqlTendenciaObtener;
            SqlDataReader drTendencia;
            try
            {

                sCadenaConexion = Utilities.GetConnectionStringTalentos2();
                conn = new SqlConnection(sCadenaConexion);

                sqlTendenciaObtener = "usp_TendenciaObtener";
                cmdTendenciaObtener = conn.CreateCommand();
                cmdTendenciaObtener.CommandText = sqlTendenciaObtener;
                cmdTendenciaObtener.CommandType = CommandType.StoredProcedure;

                prmCodTendencia = cmdTendenciaObtener.CreateParameter();
                prmCodTendencia.ParameterName = "@CodTendencia";
                prmCodTendencia.SqlDbType = SqlDbType.Int;
                prmCodTendencia.Value = codTendencia;

                cmdTendenciaObtener.Parameters.Add(prmCodTendencia);
                cmdTendenciaObtener.Connection.Open();
                drTendencia = cmdTendenciaObtener.ExecuteReader();

                TendenciaBE objTendenciaBE;
                objTendenciaBE = new TendenciaBE();
                if (drTendencia.Read())
                {
                    objTendenciaBE.TendenciaId = drTendencia.GetInt32(drTendencia.GetOrdinal("Tendencia_id"));
                    objTendenciaBE.Descripcion = drTendencia.GetString(drTendencia.GetOrdinal("Descripcion"));
                    objTendenciaBE.Nombre = drTendencia.GetString(drTendencia.GetOrdinal("Nombre"));
                    objTendenciaBE.Color = drTendencia.GetString(drTendencia.GetOrdinal("Color"));
                }

                cmdTendenciaObtener.Connection.Close();
                cmdTendenciaObtener.Dispose();
                conn.Dispose();

                return objTendenciaBE;
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public List<TendenciaBE> ListarTendencias()
        {
            SqlConnection Conn = null;
            SqlCommand cmdTalentosListar = null;
            SqlDataReader drTalentos;
            String sCadenaConexion;
            String sqlTalentosListar;

            try
            {
                sCadenaConexion = Utilities.GetConnectionStringTalentos2();

                sqlTalentosListar = "usp_TendenciaListar";

                Conn = new SqlConnection(sCadenaConexion);

                cmdTalentosListar = Conn.CreateCommand();
                cmdTalentosListar.CommandType = CommandType.StoredProcedure;
                cmdTalentosListar.CommandText = sqlTalentosListar;

                List<TendenciaBE> lstTendencias;
                TendenciaBE objTendenciaBE;

                cmdTalentosListar.Connection.Open();
                drTalentos = cmdTalentosListar.ExecuteReader();

                lstTendencias = new List<TendenciaBE>();

                while (drTalentos.Read())
                {
                    objTendenciaBE = new TendenciaBE();
                    objTendenciaBE.TendenciaId = drTalentos.GetInt32(drTalentos.GetOrdinal("Tendencia_id"));
                    objTendenciaBE.Nombre = drTalentos.GetString(drTalentos.GetOrdinal("Nombre"));
                    objTendenciaBE.Descripcion = drTalentos.GetString(drTalentos.GetOrdinal("Descripcion"));
                    objTendenciaBE.Color = drTalentos.GetString(drTalentos.GetOrdinal("Color"));

                    lstTendencias.Add(objTendenciaBE);
                }

                cmdTalentosListar.Connection.Close();
                cmdTalentosListar.Dispose();
                Conn.Dispose();

                return lstTendencias;
            }
            catch (Exception ex)
            {
                Conn.Dispose();
                cmdTalentosListar.Dispose();

                throw;
            }
        }

        public bool ActualizarTendencia(TendenciaBE objTendencia)
        {
            SqlConnection Conn = null;
            SqlCommand cmdTendenciaActualizar = null;
            SqlParameter prmIdTendencia;
            SqlParameter prmNombre;
            SqlParameter prmDescripcion;
            SqlParameter prmColor;
            String sCadenaConexion;
            String sqlTalentosActualizar;

            try
            {
                sCadenaConexion = Utilities.GetConnectionStringTalentos2();

                sqlTalentosActualizar = "usp_TendenciaActualizar";

                Conn = new SqlConnection(sCadenaConexion);

                cmdTendenciaActualizar = Conn.CreateCommand();
                cmdTendenciaActualizar.CommandType = CommandType.StoredProcedure;
                cmdTendenciaActualizar.CommandText = sqlTalentosActualizar;

                prmIdTendencia = new SqlParameter("@TendenciaId", objTendencia.TendenciaId);
                prmIdTendencia.SqlDbType = SqlDbType.Int;

                prmNombre = new SqlParameter("@Nombre", objTendencia.Nombre);
                prmNombre.SqlDbType = SqlDbType.VarChar;
                prmNombre.Size = 150;

                prmDescripcion = new SqlParameter("@Descripcion", objTendencia.Descripcion);
                prmDescripcion.SqlDbType = SqlDbType.VarChar;
                prmDescripcion.Size = 150;

                prmColor = new SqlParameter("@Color", objTendencia.Descripcion);
                prmColor.SqlDbType = SqlDbType.VarChar;
                prmColor.Size = 150;

                cmdTendenciaActualizar.Parameters.Add(prmIdTendencia);
                cmdTendenciaActualizar.Parameters.Add(prmNombre);
                cmdTendenciaActualizar.Parameters.Add(prmDescripcion);
                cmdTendenciaActualizar.Parameters.Add(prmColor);

                cmdTendenciaActualizar.Connection.Open();
                cmdTendenciaActualizar.ExecuteNonQuery();

                cmdTendenciaActualizar.Connection.Close();
                cmdTendenciaActualizar.Dispose();
                Conn.Dispose();

                return true;
            }
            catch (Exception ex)
            {
                Conn.Dispose();
                cmdTendenciaActualizar.Dispose();

                return false;
            }
        }

        
    }
}
