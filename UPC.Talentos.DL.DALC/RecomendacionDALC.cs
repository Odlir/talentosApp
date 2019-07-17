using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UPC.Talentos.BL.BE;
using System.Data.SqlClient;
using System.Data;

namespace UPC.Talentos.DL.DALC
{
    public class RecomendacionDALC
    {
        public List<RecomendacionBE> ObtenerRecomedacion(int idTalento)
        {

            SqlConnection conn = null;
            SqlCommand cmdRecomendacionObtener = null;
            SqlParameter prmCodTalento = null;

            String sCadenaConexion;
            String sqlRecomendacionObtener;
            SqlDataReader drRecomendacion;
            try
            {

                sCadenaConexion = Utilities.GetConnectionStringTalentos2();
                conn = new SqlConnection(sCadenaConexion);

                sqlRecomendacionObtener = "usps_TalRecomendacion";
                cmdRecomendacionObtener = conn.CreateCommand();
                cmdRecomendacionObtener.CommandText = sqlRecomendacionObtener;
                cmdRecomendacionObtener.CommandType = CommandType.StoredProcedure;

                prmCodTalento = cmdRecomendacionObtener.CreateParameter();
                prmCodTalento.ParameterName = "@Talento_id";
                prmCodTalento.SqlDbType = SqlDbType.Int;
                prmCodTalento.Value = idTalento;

                cmdRecomendacionObtener.Parameters.Add(prmCodTalento);
                cmdRecomendacionObtener.Connection.Open();
                drRecomendacion = cmdRecomendacionObtener.ExecuteReader();

                RecomendacionBE objRecomendacionBE;
                
                List<RecomendacionBE> list = new List<RecomendacionBE>();
                while (drRecomendacion.Read())
                {
                    objRecomendacionBE = new RecomendacionBE();
                    objRecomendacionBE.IdRecomendacion = drRecomendacion.GetInt32(drRecomendacion.GetOrdinal("Recomendacion_id"));
                    objRecomendacionBE.IdTalento = drRecomendacion.GetInt32(drRecomendacion.GetOrdinal("Talento_id"));
                    objRecomendacionBE.Descripcion = drRecomendacion.GetString(drRecomendacion.GetOrdinal("Descripcion"));                    
                    
                    list.Add(objRecomendacionBE);
                }

                cmdRecomendacionObtener.Connection.Close();
                cmdRecomendacionObtener.Dispose();
                conn.Dispose();

                return list;
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public List<RecomendacionBE> ObtenerSugerenciasTalentosSeleccionados(int idTalentoMas1, int idTalentoMas2, int idTalentoMas3,
            int idTalentoMas4, int idTalentoMas5, int idTalentoMas6, int idTalentoMas7, int idTalentoMas8, int idTalentoMas9,
            int idTalentoMas10, int idTalentoMas11, int idTalentoMas12, int idTalentoMenos1, int idTalentoMenos2, int idTalentoMenos3,
            int idTalentoMenos4, int idTalentoMenos5, int idTalentoMenos6)
        {
            SqlConnection Conn = null;
            SqlCommand cmdDescripcionTalentosObtener = null;
            SqlDataReader drTalentos;
            String sCadenaConexion;
            String sqlTalentosListar;
            SqlParameter prmTdTalentoMas1;
            SqlParameter prmTdTalentoMas2;
            SqlParameter prmTdTalentoMas3;
            SqlParameter prmTdTalentoMas4;
            SqlParameter prmTdTalentoMas5;
            SqlParameter prmTdTalentoMas6;
            SqlParameter prmTdTalentoMas7;
            SqlParameter prmTdTalentoMas8;
            SqlParameter prmTdTalentoMas9;
            SqlParameter prmTdTalentoMas10;
            SqlParameter prmTdTalentoMas11;
            SqlParameter prmTdTalentoMas12;
            SqlParameter prmTdTalentoMenos1;
            SqlParameter prmTdTalentoMenos2;
            SqlParameter prmTdTalentoMenos3;
            SqlParameter prmTdTalentoMenos4;
            SqlParameter prmTdTalentoMenos5;
            SqlParameter prmTdTalentoMenos6;

            try
            {
                sCadenaConexion = Utilities.GetConnectionStringTalentos2();

                sqlTalentosListar = "usp_TalSugerenciasDescripcionTalentos";

                Conn = new SqlConnection(sCadenaConexion);

                prmTdTalentoMas1 = new SqlParameter("@TalentoMas1", idTalentoMas1);
                prmTdTalentoMas1.SqlDbType = SqlDbType.Int;

                prmTdTalentoMas2 = new SqlParameter("@TalentoMas2", idTalentoMas2);
                prmTdTalentoMas2.SqlDbType = SqlDbType.Int;

                prmTdTalentoMas3 = new SqlParameter("@TalentoMas3", idTalentoMas3);
                prmTdTalentoMas3.SqlDbType = SqlDbType.Int;

                prmTdTalentoMas4 = new SqlParameter("@TalentoMas4", idTalentoMas4);
                prmTdTalentoMas4.SqlDbType = SqlDbType.Int;

                prmTdTalentoMas5 = new SqlParameter("@TalentoMas5", idTalentoMas5);
                prmTdTalentoMas5.SqlDbType = SqlDbType.Int;

                prmTdTalentoMas6 = new SqlParameter("@TalentoMas6", idTalentoMas6);
                prmTdTalentoMas6.SqlDbType = SqlDbType.Int;

                prmTdTalentoMas7 = new SqlParameter("@TalentoMas7", idTalentoMas7);
                prmTdTalentoMas7.SqlDbType = SqlDbType.Int;

                prmTdTalentoMas8 = new SqlParameter("@TalentoMas8", idTalentoMas8);
                prmTdTalentoMas8.SqlDbType = SqlDbType.Int;

                prmTdTalentoMas9 = new SqlParameter("@TalentoMas9", idTalentoMas9);
                prmTdTalentoMas9.SqlDbType = SqlDbType.Int;

                prmTdTalentoMas10 = new SqlParameter("@TalentoMas10", idTalentoMas10);
                prmTdTalentoMas10.SqlDbType = SqlDbType.Int;

                prmTdTalentoMas11 = new SqlParameter("@TalentoMas11", idTalentoMas11);
                prmTdTalentoMas11.SqlDbType = SqlDbType.Int;

                prmTdTalentoMas12 = new SqlParameter("@TalentoMas12", idTalentoMas12);
                prmTdTalentoMas12.SqlDbType = SqlDbType.Int;

                prmTdTalentoMenos1 = new SqlParameter("@TalentoMenos1", idTalentoMenos1);
                prmTdTalentoMenos1.SqlDbType = SqlDbType.Int;

                prmTdTalentoMenos2 = new SqlParameter("@TalentoMenos2", idTalentoMenos2);
                prmTdTalentoMenos2.SqlDbType = SqlDbType.Int;

                prmTdTalentoMenos3 = new SqlParameter("@TalentoMenos3", idTalentoMenos3);
                prmTdTalentoMenos3.SqlDbType = SqlDbType.Int;

                prmTdTalentoMenos4 = new SqlParameter("@TalentoMenos4", idTalentoMenos4);
                prmTdTalentoMenos4.SqlDbType = SqlDbType.Int;

                prmTdTalentoMenos5 = new SqlParameter("@TalentoMenos5", idTalentoMenos5);
                prmTdTalentoMenos5.SqlDbType = SqlDbType.Int;

                prmTdTalentoMenos6 = new SqlParameter("@TalentoMenos6", idTalentoMenos6);
                prmTdTalentoMenos6.SqlDbType = SqlDbType.Int;

                cmdDescripcionTalentosObtener = Conn.CreateCommand();
                cmdDescripcionTalentosObtener.CommandType = CommandType.StoredProcedure;
                cmdDescripcionTalentosObtener.CommandText = sqlTalentosListar;

                cmdDescripcionTalentosObtener.Parameters.Add(prmTdTalentoMas1);
                cmdDescripcionTalentosObtener.Parameters.Add(prmTdTalentoMas2);
                cmdDescripcionTalentosObtener.Parameters.Add(prmTdTalentoMas3);
                cmdDescripcionTalentosObtener.Parameters.Add(prmTdTalentoMas4);
                cmdDescripcionTalentosObtener.Parameters.Add(prmTdTalentoMas5);
                cmdDescripcionTalentosObtener.Parameters.Add(prmTdTalentoMas6);
                cmdDescripcionTalentosObtener.Parameters.Add(prmTdTalentoMas7);
                cmdDescripcionTalentosObtener.Parameters.Add(prmTdTalentoMas8);
                cmdDescripcionTalentosObtener.Parameters.Add(prmTdTalentoMas9);
                cmdDescripcionTalentosObtener.Parameters.Add(prmTdTalentoMas10);
                cmdDescripcionTalentosObtener.Parameters.Add(prmTdTalentoMas11);
                cmdDescripcionTalentosObtener.Parameters.Add(prmTdTalentoMas12);
                cmdDescripcionTalentosObtener.Parameters.Add(prmTdTalentoMenos1);
                cmdDescripcionTalentosObtener.Parameters.Add(prmTdTalentoMenos2);
                cmdDescripcionTalentosObtener.Parameters.Add(prmTdTalentoMenos3);
                cmdDescripcionTalentosObtener.Parameters.Add(prmTdTalentoMenos4);
                cmdDescripcionTalentosObtener.Parameters.Add(prmTdTalentoMenos5);
                cmdDescripcionTalentosObtener.Parameters.Add(prmTdTalentoMenos6);

                List<RecomendacionBE> lstRecomendaciones = new List<RecomendacionBE>();

                cmdDescripcionTalentosObtener.Connection.Open();
                drTalentos = cmdDescripcionTalentosObtener.ExecuteReader();

                while (drTalentos.Read())
                {
                    RecomendacionBE objRecomendacionBE = new RecomendacionBE();

                    objRecomendacionBE.IdRecomendacion = drTalentos.GetInt32(drTalentos.GetOrdinal("Recomendacion_Id"));
                    objRecomendacionBE.Tipo = drTalentos.GetInt32(drTalentos.GetOrdinal("Tipo"));
                    objRecomendacionBE.Descripcion = drTalentos.GetString(drTalentos.GetOrdinal("Descripcion"));
                    objRecomendacionBE.Talento = drTalentos.GetString(drTalentos.GetOrdinal("Talento"));
                    objRecomendacionBE.IdTalento = drTalentos.GetInt32(drTalentos.GetOrdinal("Talento_Id"));

                    lstRecomendaciones.Add(objRecomendacionBE);
                }

                cmdDescripcionTalentosObtener.Connection.Close();
                cmdDescripcionTalentosObtener.Dispose();
                Conn.Dispose();

                return lstRecomendaciones;
            }
            catch (Exception ex)
            {
                Conn.Dispose();
                cmdDescripcionTalentosObtener.Dispose();

                throw;
            }
        }

        public List<RecomendacionBE> ObtenerSugerenciasTalentosAdultoSeleccionados(int idTalentoMas1, int idTalentoMas2, int idTalentoMas3,
            int idTalentoMas4, int idTalentoMas5, int idTalentoMas6, int idTalentoMas7, int idTalentoMas8, int idTalentoMas9,
            int idTalentoMas10, int idTalentoMas11, int idTalentoMas12, int idTalentoMenos1, int idTalentoMenos2, int idTalentoMenos3,
            int idTalentoMenos4, int idTalentoMenos5, int idTalentoMenos6)
        {
            SqlConnection Conn = null;
            SqlCommand cmdDescripcionTalentosObtener = null;
            SqlDataReader drTalentos;
            String sCadenaConexion;
            String sqlTalentosListar;
            SqlParameter prmTdTalentoMas1;
            SqlParameter prmTdTalentoMas2;
            SqlParameter prmTdTalentoMas3;
            SqlParameter prmTdTalentoMas4;
            SqlParameter prmTdTalentoMas5;
            SqlParameter prmTdTalentoMas6;
            SqlParameter prmTdTalentoMas7;
            SqlParameter prmTdTalentoMas8;
            SqlParameter prmTdTalentoMas9;
            SqlParameter prmTdTalentoMas10;
            SqlParameter prmTdTalentoMas11;
            SqlParameter prmTdTalentoMas12;
            SqlParameter prmTdTalentoMenos1;
            SqlParameter prmTdTalentoMenos2;
            SqlParameter prmTdTalentoMenos3;
            SqlParameter prmTdTalentoMenos4;
            SqlParameter prmTdTalentoMenos5;
            SqlParameter prmTdTalentoMenos6;

            try
            {
                sCadenaConexion = Utilities.GetConnectionStringTalentos2();

                sqlTalentosListar = "usp_TalSugerenciasDescripcionTalentosAdulto";

                Conn = new SqlConnection(sCadenaConexion);

                prmTdTalentoMas1 = new SqlParameter("@TalentoMas1", idTalentoMas1);
                prmTdTalentoMas1.SqlDbType = SqlDbType.Int;

                prmTdTalentoMas2 = new SqlParameter("@TalentoMas2", idTalentoMas2);
                prmTdTalentoMas2.SqlDbType = SqlDbType.Int;

                prmTdTalentoMas3 = new SqlParameter("@TalentoMas3", idTalentoMas3);
                prmTdTalentoMas3.SqlDbType = SqlDbType.Int;

                prmTdTalentoMas4 = new SqlParameter("@TalentoMas4", idTalentoMas4);
                prmTdTalentoMas4.SqlDbType = SqlDbType.Int;

                prmTdTalentoMas5 = new SqlParameter("@TalentoMas5", idTalentoMas5);
                prmTdTalentoMas5.SqlDbType = SqlDbType.Int;

                prmTdTalentoMas6 = new SqlParameter("@TalentoMas6", idTalentoMas6);
                prmTdTalentoMas6.SqlDbType = SqlDbType.Int;

                prmTdTalentoMas7 = new SqlParameter("@TalentoMas7", idTalentoMas7);
                prmTdTalentoMas7.SqlDbType = SqlDbType.Int;

                prmTdTalentoMas8 = new SqlParameter("@TalentoMas8", idTalentoMas8);
                prmTdTalentoMas8.SqlDbType = SqlDbType.Int;

                prmTdTalentoMas9 = new SqlParameter("@TalentoMas9", idTalentoMas9);
                prmTdTalentoMas9.SqlDbType = SqlDbType.Int;

                prmTdTalentoMas10 = new SqlParameter("@TalentoMas10", idTalentoMas10);
                prmTdTalentoMas10.SqlDbType = SqlDbType.Int;

                prmTdTalentoMas11 = new SqlParameter("@TalentoMas11", idTalentoMas11);
                prmTdTalentoMas11.SqlDbType = SqlDbType.Int;

                prmTdTalentoMas12 = new SqlParameter("@TalentoMas12", idTalentoMas12);
                prmTdTalentoMas12.SqlDbType = SqlDbType.Int;

                prmTdTalentoMenos1 = new SqlParameter("@TalentoMenos1", idTalentoMenos1);
                prmTdTalentoMenos1.SqlDbType = SqlDbType.Int;

                prmTdTalentoMenos2 = new SqlParameter("@TalentoMenos2", idTalentoMenos2);
                prmTdTalentoMenos2.SqlDbType = SqlDbType.Int;

                prmTdTalentoMenos3 = new SqlParameter("@TalentoMenos3", idTalentoMenos3);
                prmTdTalentoMenos3.SqlDbType = SqlDbType.Int;

                prmTdTalentoMenos4 = new SqlParameter("@TalentoMenos4", idTalentoMenos4);
                prmTdTalentoMenos4.SqlDbType = SqlDbType.Int;

                prmTdTalentoMenos5 = new SqlParameter("@TalentoMenos5", idTalentoMenos5);
                prmTdTalentoMenos5.SqlDbType = SqlDbType.Int;

                prmTdTalentoMenos6 = new SqlParameter("@TalentoMenos6", idTalentoMenos6);
                prmTdTalentoMenos6.SqlDbType = SqlDbType.Int;

                cmdDescripcionTalentosObtener = Conn.CreateCommand();
                cmdDescripcionTalentosObtener.CommandType = CommandType.StoredProcedure;
                cmdDescripcionTalentosObtener.CommandText = sqlTalentosListar;

                cmdDescripcionTalentosObtener.Parameters.Add(prmTdTalentoMas1);
                cmdDescripcionTalentosObtener.Parameters.Add(prmTdTalentoMas2);
                cmdDescripcionTalentosObtener.Parameters.Add(prmTdTalentoMas3);
                cmdDescripcionTalentosObtener.Parameters.Add(prmTdTalentoMas4);
                cmdDescripcionTalentosObtener.Parameters.Add(prmTdTalentoMas5);
                cmdDescripcionTalentosObtener.Parameters.Add(prmTdTalentoMas6);
                cmdDescripcionTalentosObtener.Parameters.Add(prmTdTalentoMas7);
                cmdDescripcionTalentosObtener.Parameters.Add(prmTdTalentoMas8);
                cmdDescripcionTalentosObtener.Parameters.Add(prmTdTalentoMas9);
                cmdDescripcionTalentosObtener.Parameters.Add(prmTdTalentoMas10);
                cmdDescripcionTalentosObtener.Parameters.Add(prmTdTalentoMas11);
                cmdDescripcionTalentosObtener.Parameters.Add(prmTdTalentoMas12);
                cmdDescripcionTalentosObtener.Parameters.Add(prmTdTalentoMenos1);
                cmdDescripcionTalentosObtener.Parameters.Add(prmTdTalentoMenos2);
                cmdDescripcionTalentosObtener.Parameters.Add(prmTdTalentoMenos3);
                cmdDescripcionTalentosObtener.Parameters.Add(prmTdTalentoMenos4);
                cmdDescripcionTalentosObtener.Parameters.Add(prmTdTalentoMenos5);
                cmdDescripcionTalentosObtener.Parameters.Add(prmTdTalentoMenos6);

                List<RecomendacionBE> lstRecomendaciones = new List<RecomendacionBE>();

                cmdDescripcionTalentosObtener.Connection.Open();
                drTalentos = cmdDescripcionTalentosObtener.ExecuteReader();

                while (drTalentos.Read())
                {
                    RecomendacionBE objRecomendacionBE = new RecomendacionBE();

                    objRecomendacionBE.IdRecomendacion = drTalentos.GetInt32(drTalentos.GetOrdinal("Recomendacion_Id"));
                    objRecomendacionBE.Tipo = drTalentos.GetInt32(drTalentos.GetOrdinal("Tipo"));
                    objRecomendacionBE.Descripcion = drTalentos.GetString(drTalentos.GetOrdinal("Descripcion"));
                    objRecomendacionBE.Talento = drTalentos.GetString(drTalentos.GetOrdinal("Talento"));
                    objRecomendacionBE.IdTalento = drTalentos.GetInt32(drTalentos.GetOrdinal("Talento_Id"));

                    lstRecomendaciones.Add(objRecomendacionBE);
                }

                cmdDescripcionTalentosObtener.Connection.Close();
                cmdDescripcionTalentosObtener.Dispose();
                Conn.Dispose();

                return lstRecomendaciones;
            }
            catch (Exception ex)
            {
                Conn.Dispose();
                cmdDescripcionTalentosObtener.Dispose();

                throw;
            }
        }
    }
}
