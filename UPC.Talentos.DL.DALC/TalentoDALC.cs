using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UPC.Talentos.BL.BE;
using System.Data;
using System.Data.SqlClient;

namespace UPC.Talentos.DL.DALC
{
    public class TalentoDALC
    {
        private String pathImages = "/images/talentos/images/";
        private String pathExample = "/images/talentos/example/";
        
        //private String[] IMAGES = {"image12.png", "image20.png", "image29.png", "image31.png", "image33.png", "image34.png", "image39.png", //ANARANJADO
        //                           "image1.png", "image5.png", "image15.png", "image25.png", "image26.png", "image35.png", "image41.png", //AZUL
        //                           "image4.png", "image13.png", "image14.png", "image16.png", "image23.png", "image24.png", "image32.png", //AMARILLO
        //                           "image2.png", "image9.png", "image10.png", "image11.png", "image22.png", "image37.png", "image42.png", //GUINDA
        //                           "image3.png", "image6.png", "image7.png", "image8.png", "image19.png", "image38.png", "image40.png", //ROJO
        //                           "image17.png", "image18.png", "image21.png", "image27.png", "image28.png", "image30.png", "image36.png"}; //VERDE};


        //private String[] EXAMPLES = {"image12a.png", "image20a.png", "image29a.png", "image31a.png", "image33a.png", "image34a.png", "image39a.png", //ANARANJADO
        //                           "image1a.png", "image5a.png", "image15a.png", "image25a.png", "image26a.png", "image35a.png", "image41a.png", //AZUL
        //                           "image4a.png", "image13a.png", "image14a.png", "image16a.png", "image23a.png", "image24a.png", "image32a.png", //AMARILLO
        //                           "image2a.png", "image9a.png", "image10a.png", "image11a.png", "image22a.png", "image37a.png", "image42a.png", //GUINDA
        //                           "image3a.png", "image6a.png", "image7a.png", "image8a.png", "image19a.png", "image38a.png", "image40a.png", //ROJO
        //                           "image17a.png", "image18a.png", "image21a.png", "image27a.png", "image28a.png", "image30a.png", "image36a.png"}; //VERDE};

        //private int[] cod = { 0, 0, 0, 0, 0, 0, 0,
        //                      1, 1, 1, 1, 1, 1, 1,
        //                      2, 2, 2, 2, 2, 2, 2,
        //                      3, 3, 3, 3, 3, 3, 3,
        //                      4, 4, 4, 4, 4, 4, 4,
        //                      5, 5, 5, 5, 5, 5, 5
        //                    };

        //private String[] IMAGES = { "image1.png", "image2.png", "image3.png", "image4.png", "image5.png", "image6.png", "image7.png", "image8.png", "image9.png", "image10.png", "image11.png", "image12.png", "image13.png", "image14.png", "image15.png", "image16.png", "image17.png", "image18.png", "image19.png", "image20.png" };
        //private int[] cod = { 0, 1, 2, 3, 4, 5, 0, 1, 2, 3, 4, 5, 0, 1, 2, 3, 4, 5, 0, 1 };

        //Esta función permite obtener los 42 talentos del juego
        //public List<TalentoBE> ObtenerTalentos()
        //{
        //    List<TalentoBE> lstTalentoBE = null;
        //    Random rnd = new Random();

        //    try
        //    {
        //        lstTalentoBE = new List<TalentoBE>();
        //        int cont = 0;
        //        string sin_png;
        //        for (int i = 0; i < IMAGES.Length; i++)
        //        {
        //            sin_png = IMAGES[i].Substring(0, IMAGES[i].Length - 4);
        //            cont = Convert.ToInt32(sin_png.Substring(5, sin_png.Length - 5));
        //            TalentoBE objTalentoBE = new TalentoBE("Descripcion " + cont.ToString(), cont, pathExample + EXAMPLES[i], pathImages + IMAGES[i], cod[i]);
        //            lstTalentoBE.Add(objTalentoBE);
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        throw;
        //    }

        //    return lstTalentoBE;
        //}

        public List<TalentoBE> ListarTalentos()
        {
            SqlConnection Conn = null;
            SqlCommand cmdTalentosListar = null;
            SqlDataReader drTalentos;
            String sCadenaConexion;
            String sqlTalentosListar;

            try
            {
                sCadenaConexion = Utilities.GetConnectionStringTalentos2();

                sqlTalentosListar = "uspTalentosListar";

                Conn = new SqlConnection(sCadenaConexion);

                cmdTalentosListar = Conn.CreateCommand();
                cmdTalentosListar.CommandType = CommandType.StoredProcedure;
                cmdTalentosListar.CommandText = sqlTalentosListar;

                List<TalentoBE> lstTalentos;
                TalentoBE objTalentoBE;

                cmdTalentosListar.Connection.Open();
                drTalentos = cmdTalentosListar.ExecuteReader();

                lstTalentos = new List<TalentoBE>();
                string imagen = "";
                string imagenEspalda = "";

                while (drTalentos.Read())
                {
                    objTalentoBE = new TalentoBE();
                    objTalentoBE.IdTalento = drTalentos.GetInt32(drTalentos.GetOrdinal("Talento_id"));
                    objTalentoBE.IdTendencia = drTalentos.GetInt32(drTalentos.GetOrdinal("Tendencia_id"));
                    objTalentoBE.Nombre = drTalentos.GetString(drTalentos.GetOrdinal("Nombre"));
                    objTalentoBE.Descripcion = drTalentos.GetString(drTalentos.GetOrdinal("Descripcion"));
                    objTalentoBE.NombreTendencia = drTalentos.GetString(drTalentos.GetOrdinal("NombreTendencia"));
                    objTalentoBE.ColorTendencia = drTalentos.GetString(drTalentos.GetOrdinal("Color"));
                    objTalentoBE.TipoTalento = drTalentos.GetInt32(drTalentos.GetOrdinal("TipoTalento_id"));
                    imagen = drTalentos.GetString(drTalentos.GetOrdinal("Imagen"));
                    imagenEspalda = drTalentos.GetString(drTalentos.GetOrdinal("ImagenEspalda"));
                    objTalentoBE.Image = pathImages + imagen;
                    objTalentoBE.Example = pathExample + imagenEspalda;
                    objTalentoBE.IdColor = drTalentos.GetInt32(drTalentos.GetOrdinal("Tendencia_id"));
                    lstTalentos.Add(objTalentoBE);
                }

                cmdTalentosListar.Connection.Close();
                cmdTalentosListar.Dispose();
                Conn.Dispose();
                return lstTalentos;
            }
            catch (Exception ex)
            {
                Conn.Dispose();
                cmdTalentosListar.Dispose();

                throw;
            }            
        }

        public List<TalentoBE> ListarTalentosReporteTodos()
        {
            SqlConnection Conn = null;
            SqlCommand cmdTalentosListar = null;
            SqlDataReader drTalentos;
            String sCadenaConexion;
            String sqlTalentosListar;

            try
            {
                sCadenaConexion = Utilities.GetConnectionStringTalentos2();

                sqlTalentosListar = "usp_TalTalentoReporteListarTodos";

                Conn = new SqlConnection(sCadenaConexion);

                cmdTalentosListar = Conn.CreateCommand();
                cmdTalentosListar.CommandType = CommandType.StoredProcedure;
                cmdTalentosListar.CommandText = sqlTalentosListar;

                List<TalentoBE> lstTalentos;
                TalentoBE objTalentoBE;

                cmdTalentosListar.Connection.Open();
                drTalentos = cmdTalentosListar.ExecuteReader();

                lstTalentos = new List<TalentoBE>();

                while (drTalentos.Read())
                {
                    objTalentoBE = new TalentoBE();
                    objTalentoBE.IdTendencia = drTalentos.GetInt32(drTalentos.GetOrdinal("Tendencia_id"));
                    objTalentoBE.Nombre = drTalentos.GetString(drTalentos.GetOrdinal("Nombre"));
                    lstTalentos.Add(objTalentoBE);
                }

                cmdTalentosListar.Connection.Close();
                cmdTalentosListar.Dispose();
                Conn.Dispose();
                return lstTalentos;
            }
            catch (Exception ex)
            {
                Conn.Dispose();
                cmdTalentosListar.Dispose();

                throw;
            }
        }

        public List<TalentoComplexBE> ListarTalentosComplex(int idTalento)
        {
            SqlConnection Conn = null;
            SqlCommand cmdTalentosListar = null;
            SqlDataReader drTalentos;
            SqlParameter prmIdTalento;
            String sCadenaConexion;
            String sqlTalentosListar;

            try
            {
                sCadenaConexion = Utilities.GetConnectionStringTalentos2();

                sqlTalentosListar = "usp_TalentoComplexListar";

                Conn = new SqlConnection(sCadenaConexion);

                prmIdTalento = new SqlParameter("@idTalento", idTalento);
                prmIdTalento.SqlDbType = SqlDbType.Int;

                cmdTalentosListar = Conn.CreateCommand();
                cmdTalentosListar.CommandType = CommandType.StoredProcedure;
                cmdTalentosListar.CommandText = sqlTalentosListar;

                cmdTalentosListar.Parameters.Add(prmIdTalento);

                List<TalentoComplexBE> lstTalentos;
                TalentoComplexBE objTalentoBE;

                cmdTalentosListar.Connection.Open();
                drTalentos = cmdTalentosListar.ExecuteReader();

                lstTalentos = new List<TalentoComplexBE>();

                while (drTalentos.Read())
                {
                    objTalentoBE = new TalentoComplexBE();
                    objTalentoBE.idTalento = drTalentos.GetInt32(drTalentos.GetOrdinal("Talento_id"));
                    objTalentoBE.idTendencia = drTalentos.GetInt32(drTalentos.GetOrdinal("Tendencia_id"));
                    objTalentoBE.idTipoTalento = drTalentos.GetInt32(drTalentos.GetOrdinal("TipoTalento_id"));
                    objTalentoBE.nombre = drTalentos.GetString(drTalentos.GetOrdinal("Nombre"));
                    objTalentoBE.descripcion = drTalentos.GetString(drTalentos.GetOrdinal("Descripcion"));
                    objTalentoBE.tendencia = drTalentos.GetString(drTalentos.GetOrdinal("Tendencia"));
                    objTalentoBE.tipoTalento = drTalentos.GetString(drTalentos.GetOrdinal("TipoTalento"));
                    objTalentoBE.image = pathImages + drTalentos.GetString(drTalentos.GetOrdinal("Imagen"));
                    
                    lstTalentos.Add(objTalentoBE);
                }

                cmdTalentosListar.Connection.Close();
                cmdTalentosListar.Dispose();
                Conn.Dispose();

                return lstTalentos;
            }
            catch (Exception ex)
            {
                Conn.Dispose();
                cmdTalentosListar.Dispose();

                throw;
            }
        }

        public bool ActualizarTalento(TalentoComplexBE objTalento)
        {
            SqlConnection Conn = null;
            SqlCommand cmdTalentosActualizar = null;
            SqlParameter prmIdTalento;
            SqlParameter prmIdTendencia;
            SqlParameter prmNombre;
            SqlParameter prmDescripcion;
            String sCadenaConexion;
            String sqlTalentosActualizar;

            try
            {
                sCadenaConexion = Utilities.GetConnectionStringTalentos2();

                sqlTalentosActualizar = "usp_TalentoActualizar";

                Conn = new SqlConnection(sCadenaConexion);

                cmdTalentosActualizar = Conn.CreateCommand();
                cmdTalentosActualizar.CommandType = CommandType.StoredProcedure;
                cmdTalentosActualizar.CommandText = sqlTalentosActualizar;

                prmIdTalento = new SqlParameter("@Talento_id", objTalento.idTalento);
                prmIdTalento.SqlDbType = SqlDbType.Int;

                prmIdTendencia = new SqlParameter("@Tendencia_id", objTalento.idTendencia);
                prmIdTendencia.SqlDbType = SqlDbType.Int;

                prmNombre = new SqlParameter("@Nombre", objTalento.nombre);
                prmNombre.SqlDbType = SqlDbType.VarChar;
                prmNombre.Size = 60;

                prmDescripcion = new SqlParameter("@Descripcion", objTalento.descripcion);
                prmDescripcion.SqlDbType = SqlDbType.VarChar;
                prmDescripcion.Size = 5000;

                cmdTalentosActualizar.Parameters.Add(prmIdTalento);
                cmdTalentosActualizar.Parameters.Add(prmIdTendencia);
                cmdTalentosActualizar.Parameters.Add(prmNombre);
                cmdTalentosActualizar.Parameters.Add(prmDescripcion);

                cmdTalentosActualizar.Connection.Open();
                cmdTalentosActualizar.ExecuteNonQuery();

                cmdTalentosActualizar.Connection.Close();
                cmdTalentosActualizar.Dispose();
                Conn.Dispose();

                return true;
            }
            catch (Exception ex)
            {
                Conn.Dispose();
                cmdTalentosActualizar.Dispose();

                return false;
            }
        }

        public List<RecomendacionBE> ObtenerDescripcionesTalentosSeleccionados(int idTalentoMas1, int idTalentoMas2, int idTalentoMas3, 
            int idTalentoMas4, int idTalentoMas5, int idTalentoMas6, int idTalentoMas7, int idTalentoMas8, int idTalentoMas9,
            int idTalentoMas10, int idTalentoMas11, int idTalentoMas12, int idTalentoMenos1, int idTalentoMenos2, int idTalentoMenos3,
            int idTalentoMenos4, int idTalentoMenos5, int idTalentoMenos6, int tipo)
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
            SqlParameter prmTipo;

            try
            {
                sCadenaConexion = Utilities.GetConnectionStringTalentos2();

                sqlTalentosListar = "usp_TalDescripcionesTalentosSeleccionados_v2";

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

                prmTipo = new SqlParameter("@Tipo", tipo);
                prmTipo.SqlDbType = SqlDbType.Int;

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
                cmdDescripcionTalentosObtener.Parameters.Add(prmTipo);


                List<RecomendacionBE> lstTalentosMasDesarrollados = new List<RecomendacionBE>();

                cmdDescripcionTalentosObtener.Connection.Open();
                drTalentos = cmdDescripcionTalentosObtener.ExecuteReader();

                while (drTalentos.Read())
                {
                    //Re-uso de objeto RecomendacionBE para Obtener la descripcion de talentos.
                    RecomendacionBE objRecomendacionBE = new RecomendacionBE();

                    objRecomendacionBE.IdRecomendacion = drTalentos.GetInt32(drTalentos.GetOrdinal("Descripcion_Id"));
                    objRecomendacionBE.Tipo = drTalentos.GetInt32(drTalentos.GetOrdinal("Tipo"));
                    objRecomendacionBE.Descripcion = drTalentos.GetString(drTalentos.GetOrdinal("Descripcion"));
                    objRecomendacionBE.Talento = drTalentos.GetString(drTalentos.GetOrdinal("talento"));
                    objRecomendacionBE.IdTalento = drTalentos.GetInt32(drTalentos.GetOrdinal("Talento_id"));

                    lstTalentosMasDesarrollados.Add(objRecomendacionBE);
                }

                

                cmdDescripcionTalentosObtener.Connection.Close();
                cmdDescripcionTalentosObtener.Dispose();
                Conn.Dispose();

                return lstTalentosMasDesarrollados;
            }
            catch (Exception ex)
            {
                Conn.Dispose();
                cmdDescripcionTalentosObtener.Dispose();

                throw;
            }
        }



        public List<RecomendacionBE> ObtenerDescripcionesAdultoTalentosSeleccionados(int idTalentoMas1, int idTalentoMas2, int idTalentoMas3,
            int idTalentoMas4, int idTalentoMas5, int idTalentoMas6, int idTalentoMas7, int idTalentoMas8, int idTalentoMas9,
            int idTalentoMas10, int idTalentoMas11, int idTalentoMas12, int idTalentoMenos1, int idTalentoMenos2, int idTalentoMenos3,
            int idTalentoMenos4, int idTalentoMenos5, int idTalentoMenos6, int tipo)
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
            SqlParameter prmTipo;

            try
            {
                sCadenaConexion = Utilities.GetConnectionStringTalentos2();

                sqlTalentosListar = "usp_TalDescripcionesTalentosSeleccionados_Adulto";

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

                prmTipo = new SqlParameter("@Tipo", tipo);
                prmTipo.SqlDbType = SqlDbType.Int;

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
                cmdDescripcionTalentosObtener.Parameters.Add(prmTipo);


                List<RecomendacionBE> lstTalentosMasDesarrollados = new List<RecomendacionBE>();

                cmdDescripcionTalentosObtener.Connection.Open();
                drTalentos = cmdDescripcionTalentosObtener.ExecuteReader();

                while (drTalentos.Read())
                {
                    //Re-uso de objeto RecomendacionBE para Obtener la descripcion de talentos.
                    RecomendacionBE objRecomendacionBE = new RecomendacionBE();

                    objRecomendacionBE.IdRecomendacion = drTalentos.GetInt32(drTalentos.GetOrdinal("Descripcion_Id"));
                    objRecomendacionBE.Tipo = drTalentos.GetInt32(drTalentos.GetOrdinal("Tipo"));
                    objRecomendacionBE.Descripcion = drTalentos.GetString(drTalentos.GetOrdinal("Descripcion"));
                    objRecomendacionBE.Talento = drTalentos.GetString(drTalentos.GetOrdinal("talento"));
                    objRecomendacionBE.IdTalento = drTalentos.GetInt32(drTalentos.GetOrdinal("Talento_id"));

                    lstTalentosMasDesarrollados.Add(objRecomendacionBE);
                }



                cmdDescripcionTalentosObtener.Connection.Close();
                cmdDescripcionTalentosObtener.Dispose();
                Conn.Dispose();

                return lstTalentosMasDesarrollados;
            }
            catch (Exception ex)
            {
                Conn.Dispose();
                cmdDescripcionTalentosObtener.Dispose();

                throw;
            }
        }

        /// <summary>
        /// Devuelve los codigos de los talentos separados por comas
        /// </summary>
        /// <param name="nombres">Nombre de los talentos separados por comas</param>
        /// <returns></returns>
        public string ObtenerCodigos(string nombres)
        {

            SqlConnection conn = null;
            SqlCommand cmdTendenciaObtener = null;
            SqlParameter prmNombresTendencia = null;

            String sCadenaConexion;
            String sqlTendenciaObtener;
            SqlDataReader drTendencia;
            string codigos = "";

            try
            {

                sCadenaConexion = Utilities.GetConnectionStringTalentos2();
                conn = new SqlConnection(sCadenaConexion);

                sqlTendenciaObtener = "usp_TalTalentoObtenerCodigos";
                cmdTendenciaObtener = conn.CreateCommand();
                cmdTendenciaObtener.CommandText = sqlTendenciaObtener;
                cmdTendenciaObtener.CommandType = CommandType.StoredProcedure;

                prmNombresTendencia = cmdTendenciaObtener.CreateParameter();
                prmNombresTendencia.ParameterName = "@NombresTalentos";
                prmNombresTendencia.SqlDbType = SqlDbType.VarChar;
                prmNombresTendencia.Size = 800;
                prmNombresTendencia.Value = nombres;

                cmdTendenciaObtener.Parameters.Add(prmNombresTendencia);
                cmdTendenciaObtener.Connection.Open();
                drTendencia = cmdTendenciaObtener.ExecuteReader();

                if (drTendencia.Read())
                {
                    codigos = drTendencia.GetString(drTendencia.GetOrdinal("Codigos"));
                }

                cmdTendenciaObtener.Connection.Close();
                cmdTendenciaObtener.Dispose();
                conn.Dispose();

                return codigos;
            }
            catch (Exception ex)
            {
                cmdTendenciaObtener.Connection.Close();
                cmdTendenciaObtener.Dispose();
                conn.Dispose();
                throw;
            }
        }
    }
}