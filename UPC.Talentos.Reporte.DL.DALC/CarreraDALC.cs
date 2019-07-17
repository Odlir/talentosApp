using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UPC.Talentos.Reporte.BL.BE;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;

namespace UPC.Talentos.Reporte.DL.DALC
{
    public class CarreraDALC
    {
        public List<CarreraBE> ListarCarreras(string Codigo)
        {
            SqlConnection Conn = null;
            SqlCommand cmdCarreraListar = null;
            SqlDataReader drCarrera;
            SqlParameter prmCodigo;
            String sCadenaConexion;
            String sqlTalentosListar;

            try
            {
                sCadenaConexion = ConfigurationManager.AppSettings["strCadenaConexion2"].ToString();

                sqlTalentosListar = "usp_TalCarreraAreaListar";

                Conn = new SqlConnection(sCadenaConexion);

                prmCodigo = new SqlParameter("@CodigoArea", Codigo);
                prmCodigo.SqlDbType = SqlDbType.VarChar;
                prmCodigo.Size = 3;

                cmdCarreraListar = Conn.CreateCommand();
                cmdCarreraListar.CommandType = CommandType.StoredProcedure;
                cmdCarreraListar.CommandText = sqlTalentosListar;

                cmdCarreraListar.Parameters.Add(prmCodigo);

                List<CarreraBE> lstCarreras;
                CarreraBE objCarreraBE;

                cmdCarreraListar.Connection.Open();
                drCarrera = cmdCarreraListar.ExecuteReader();

                lstCarreras = new List<CarreraBE>();

                while (drCarrera.Read())
                {
                    objCarreraBE = new CarreraBE();
                    objCarreraBE.Carrera_Id = drCarrera.GetInt32(drCarrera.GetOrdinal("Carrera_Id"));
                    objCarreraBE.Nombre = drCarrera.GetString(drCarrera.GetOrdinal("Nombre"));

                    lstCarreras.Add(objCarreraBE);
                }

                cmdCarreraListar.Connection.Close();
                cmdCarreraListar.Dispose();
                Conn.Dispose();

                return lstCarreras;
            }
            catch (Exception ex)
            {
                Conn.Dispose();
                cmdCarreraListar.Dispose();

                throw;
            }
        }

        public List<string> ListarCarreras(string condicion1, string condicion2, string condicion3, int tipo)
        {
            SqlConnection Conn = null;
            SqlCommand cmdCarreraListar = null;
            SqlDataReader drCarrera;
            SqlParameter prmCondicion1;
            SqlParameter prmCondicion2;
            SqlParameter prmCondicion3;
            SqlParameter prmTipo;
            String sCadenaConexion;
            String sqlTalentosListar;

            try
            {
                sCadenaConexion = ConfigurationManager.AppSettings["strCadenaConexion2"].ToString();
                //sCadenaConexion = "server=localhost; database=UPCTalentos; User Id=sa; Password=sql";
                sqlTalentosListar = "usp_TalCarreraListar";

                Conn = new SqlConnection(sCadenaConexion);

                prmCondicion1 = new SqlParameter("@Area1", condicion1);
                prmCondicion1.SqlDbType = SqlDbType.VarChar;
                prmCondicion1.Size = 3;

                prmCondicion2 = new SqlParameter("@Area2", condicion2);
                prmCondicion2.SqlDbType = SqlDbType.VarChar;
                prmCondicion2.Size = 3;

                prmCondicion3 = new SqlParameter("@Area3", condicion3);
                prmCondicion3.SqlDbType = SqlDbType.VarChar;
                prmCondicion3.Size = 3;

                prmTipo = new SqlParameter("@Tipo", tipo);
                prmTipo.SqlDbType = SqlDbType.SmallInt;

                cmdCarreraListar = Conn.CreateCommand();
                cmdCarreraListar.CommandType = CommandType.StoredProcedure;
                cmdCarreraListar.CommandText = sqlTalentosListar;

                cmdCarreraListar.Parameters.Add(prmCondicion1);
                cmdCarreraListar.Parameters.Add(prmCondicion2);
                cmdCarreraListar.Parameters.Add(prmCondicion3);
                cmdCarreraListar.Parameters.Add(prmTipo);

                List<string> lstCarreras = new List<string>(); ;

                cmdCarreraListar.Connection.Open();
                drCarrera = cmdCarreraListar.ExecuteReader();

                while (drCarrera.Read())
                {
                    string carrera = "";

                    carrera = drCarrera.GetString(drCarrera.GetOrdinal("Nombre"));

                    lstCarreras.Add(carrera);
                }

                cmdCarreraListar.Connection.Close();
                cmdCarreraListar.Dispose();
                Conn.Dispose();

                return lstCarreras;
            }
            catch (Exception ex)
            {
                Conn.Dispose();
                cmdCarreraListar.Dispose();

                throw;
            }
        }


        //Temperamentos
        public List<ElementoTemperamentoBE> ListarElementos()
        {
            SqlConnection Conn = null;
            SqlCommand cmdElementoListar = null;
            SqlDataReader drElemento;
            String sCadenaConexion;
            String sqlTalentosListar;

            try
            {
                sCadenaConexion = ConfigurationManager.AppSettings["strCadenaConexion2"].ToString();

                sqlTalentosListar = "usp_TalListaElementos";

                Conn = new SqlConnection(sCadenaConexion);

                cmdElementoListar = Conn.CreateCommand();
                cmdElementoListar.CommandType = CommandType.StoredProcedure;
                cmdElementoListar.CommandText = sqlTalentosListar;

                List<ElementoTemperamentoBE> lstElementos;
                ElementoTemperamentoBE objElementoTemperamentoBE;

                cmdElementoListar.Connection.Open();
                drElemento = cmdElementoListar.ExecuteReader();

                lstElementos = new List<ElementoTemperamentoBE>();

                while (drElemento.Read())
                {
                    objElementoTemperamentoBE = new ElementoTemperamentoBE();
                    objElementoTemperamentoBE.nombre_mayus = drElemento.GetString(drElemento.GetOrdinal("nombre_mayus"));
                    objElementoTemperamentoBE.nombre = drElemento.GetString(drElemento.GetOrdinal("nombre"));
                    objElementoTemperamentoBE.descripcion = drElemento.GetString(drElemento.GetOrdinal("descripcion"));

                    lstElementos.Add(objElementoTemperamentoBE);
                }



                cmdElementoListar.Connection.Close();
                cmdElementoListar.Dispose();
                Conn.Dispose();

                return lstElementos;
            }
            catch (Exception ex)
            {
                Conn.Dispose();
                cmdElementoListar.Dispose();

                throw;
            }
        }

        public string ObtenerDescripcionRueda(string condicion1)
        {
            SqlConnection Conn = null;
            SqlCommand cmdCarreraListar = null;
            SqlDataReader drCarrera;
            SqlParameter prmCondicion1;
            String sCadenaConexion;
            String sqlTalentosListar;
            string descripcion = "";
            try
            {
                sCadenaConexion = ConfigurationManager.AppSettings["strCadenaConexion2"].ToString();
                //sCadenaConexion = "server=localhost; database=UPCTalentos; User Id=sa; Password=sql";
                sqlTalentosListar = "usp_TalObtenerDescripcionRueda";

                Conn = new SqlConnection(sCadenaConexion);

                prmCondicion1 = new SqlParameter("@id", condicion1);
                prmCondicion1.SqlDbType = SqlDbType.Char;
                prmCondicion1.Size = 4;

                cmdCarreraListar = Conn.CreateCommand();
                cmdCarreraListar.CommandType = CommandType.StoredProcedure;
                cmdCarreraListar.CommandText = sqlTalentosListar;

                cmdCarreraListar.Parameters.Add(prmCondicion1);


                cmdCarreraListar.Connection.Open();
                drCarrera = cmdCarreraListar.ExecuteReader();

                if (drCarrera.Read())
                {

                    descripcion = drCarrera.GetString(drCarrera.GetOrdinal("descripcion"));

                }

                cmdCarreraListar.Connection.Close();
                cmdCarreraListar.Dispose();
                Conn.Dispose();

                return descripcion;
            }
            catch (Exception ex)
            {
                Conn.Dispose();
                cmdCarreraListar.Dispose();

                throw;
            }
        }

        public List<FortalezaTemperamentoBE> ObtenerFortalezas(string eje1, string eje2, string eje3, string eje4)
        {
            SqlConnection Conn = null;
            SqlCommand cmdCarreraListar = null;
            SqlDataReader drElemento;
            SqlParameter prmCondicion1;
            SqlParameter prmCondicion2;
            SqlParameter prmCondicion3;
            SqlParameter prmCondicion4;
            String sCadenaConexion;
            String sqlListar;
            try
            {
                sCadenaConexion = ConfigurationManager.AppSettings["strCadenaConexion2"].ToString();
                //sCadenaConexion = "server=localhost; database=UPCTalentos; User Id=sa; Password=sql";
                sqlListar = "usp_TalObtenerFortalezas";

                Conn = new SqlConnection(sCadenaConexion);

                prmCondicion1 = new SqlParameter("@eje1", eje1);
                prmCondicion1.SqlDbType = SqlDbType.Char;
                prmCondicion1.Size = 1;

                prmCondicion2 = new SqlParameter("@eje2", eje2);
                prmCondicion2.SqlDbType = SqlDbType.Char;
                prmCondicion2.Size = 1;

                prmCondicion3 = new SqlParameter("@eje3", eje3);
                prmCondicion3.SqlDbType = SqlDbType.Char;
                prmCondicion3.Size = 1;

                prmCondicion4 = new SqlParameter("@eje4", eje4);
                prmCondicion4.SqlDbType = SqlDbType.Char;
                prmCondicion4.Size = 1;

                cmdCarreraListar = Conn.CreateCommand();
                cmdCarreraListar.CommandType = CommandType.StoredProcedure;
                cmdCarreraListar.CommandText = sqlListar;

                cmdCarreraListar.Parameters.Add(prmCondicion1);
                cmdCarreraListar.Parameters.Add(prmCondicion2);
                cmdCarreraListar.Parameters.Add(prmCondicion3);
                cmdCarreraListar.Parameters.Add(prmCondicion4);

                List<FortalezaTemperamentoBE> lstElementos;
                FortalezaTemperamentoBE objFortalizaTemperamentoBE;
                lstElementos = new List<FortalezaTemperamentoBE>();

                cmdCarreraListar.Connection.Open();
                drElemento = cmdCarreraListar.ExecuteReader();

                while (drElemento.Read())
                {

                    objFortalizaTemperamentoBE = new FortalezaTemperamentoBE();
                    objFortalizaTemperamentoBE.eje = drElemento.GetString(drElemento.GetOrdinal("eje"));
                    objFortalizaTemperamentoBE.nombre = drElemento.GetString(drElemento.GetOrdinal("nombre"));
                    objFortalizaTemperamentoBE.descripcion = drElemento.GetString(drElemento.GetOrdinal("descripcion"));

                    lstElementos.Add(objFortalizaTemperamentoBE);

                }

                cmdCarreraListar.Connection.Close();
                cmdCarreraListar.Dispose();
                Conn.Dispose();

                return lstElementos;
            }
            catch (Exception ex)
            {
                Conn.Dispose();
                cmdCarreraListar.Dispose();

                throw;
            }
        }


    }
}
