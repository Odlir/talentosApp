using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UPC.Seguridad.Talentos.BL.BE;
using System.Data;
using System.Data.SqlClient;

namespace UPC.Seguridad.Talentos.DL.DALC
{
    public class UbigeoDALC
    {
        public List<PaisBE> ListarPaises()
        {
            String SqlListarDepartamento;
            String sCadena;
            SqlConnection Conn;
            SqlCommand cmdListarDepartamento = null;
            SqlDataReader drListarDepartamento = null;



            try
            {
                sCadena = Utilities.GetConnectionStringSeguridad();

                Conn = new SqlConnection(sCadena);

                SqlListarDepartamento = "usp_TalListarPaises";

                cmdListarDepartamento = new SqlCommand();
                cmdListarDepartamento = Conn.CreateCommand();
                cmdListarDepartamento.CommandType = CommandType.StoredProcedure;
                cmdListarDepartamento.CommandText = SqlListarDepartamento;


                cmdListarDepartamento.Connection.Open();
                drListarDepartamento = cmdListarDepartamento.ExecuteReader();

                List<PaisBE> lstDepartamentoBE = new List<PaisBE>();

                PaisBE objDepartamentoBE;

                while (drListarDepartamento.Read())
                {
                    objDepartamentoBE = new PaisBE();
                    
                    objDepartamentoBE.Pais = drListarDepartamento.GetValue(drListarDepartamento.GetOrdinal("Nombre")).ToString();
                    objDepartamentoBE.PaisId = drListarDepartamento.GetInt32(drListarDepartamento.GetOrdinal("UbigeoId"));
                    lstDepartamentoBE.Add(objDepartamentoBE);
                }

                Conn.Close();
                Conn.Dispose();
                cmdListarDepartamento.Dispose();


                return lstDepartamentoBE;

            }
            catch (Exception ex)
            {
                throw;
            }
        }
        
        public List<DepartamentoBE> ListarDepartamento()
        {
            String SqlListarDepartamento;
            String sCadena;
            SqlConnection Conn;
            SqlCommand cmdListarDepartamento = null;
            SqlDataReader drListarDepartamento = null;



            try
            {
                sCadena = Utilities.GetConnectionStringSeguridad();

                Conn = new SqlConnection(sCadena);

                SqlListarDepartamento = "usp_TalListarDepartamentos";

                cmdListarDepartamento = new SqlCommand();
                cmdListarDepartamento = Conn.CreateCommand();
                cmdListarDepartamento.CommandType = CommandType.StoredProcedure;
                cmdListarDepartamento.CommandText = SqlListarDepartamento;

                //SqlParameter prmPaisId = new SqlParameter();
                //prmPaisId.ParameterName = "@paisid";
                //prmPaisId.SqlDbType = SqlDbType.Int;
                //prmPaisId.Value = paisId;

                //cmdListarDepartamento.Parameters.Add(prmPaisId);

                cmdListarDepartamento.Connection.Open();
                drListarDepartamento = cmdListarDepartamento.ExecuteReader();

                List<DepartamentoBE> lstDepartamentoBE = new List<DepartamentoBE>();

                DepartamentoBE objDepartamentoBE;

                while (drListarDepartamento.Read())
                {
                    objDepartamentoBE = new DepartamentoBE();
                    objDepartamentoBE.DepartamentoId = drListarDepartamento.GetValue(drListarDepartamento.GetOrdinal("CODDPTO")).ToString();
                    objDepartamentoBE.Departamento = drListarDepartamento.GetValue(drListarDepartamento.GetOrdinal("Nombre")).ToString();
                    //objDepartamentoBE.PaisId = drListarDepartamento.GetInt32(drListarDepartamento.GetOrdinal("PaisId"));
                    lstDepartamentoBE.Add(objDepartamentoBE);
                }

                Conn.Close();
                Conn.Dispose();
                cmdListarDepartamento.Dispose();


                return lstDepartamentoBE;

            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public List<ProvinciaBE> ListarProvincia(string DepartamentoId)
        {
            String SqlListarProvincia;
            String sCadena;
            SqlConnection Conn;
            SqlCommand cmdListarProvincia = null;
            SqlDataReader drListarProvincia = null;



            try
            {
                sCadena = Utilities.GetConnectionStringSeguridad();
                Conn = new SqlConnection(sCadena);

                SqlListarProvincia = "usp_TalListarProvincias";
                cmdListarProvincia = Conn.CreateCommand();
                cmdListarProvincia.CommandType = CommandType.StoredProcedure;
                cmdListarProvincia.CommandText = SqlListarProvincia;

                SqlParameter prmDepartamentoId = new SqlParameter();
                prmDepartamentoId.ParameterName = "@departamentoid";
                prmDepartamentoId.SqlDbType = SqlDbType.VarChar;
                prmDepartamentoId.Size = 5;
                prmDepartamentoId.Value = DepartamentoId;

                cmdListarProvincia.Parameters.Add(prmDepartamentoId);

                cmdListarProvincia.Connection.Open();
                drListarProvincia = cmdListarProvincia.ExecuteReader();

                List<ProvinciaBE> lstProvinciaBE = new List<ProvinciaBE>();

                ProvinciaBE objProvinciaBE;

                while (drListarProvincia.Read())
                {
                    objProvinciaBE = new ProvinciaBE();
                    objProvinciaBE.ProvinciaId = drListarProvincia.GetString(drListarProvincia.GetOrdinal("CODPROV")); //Convert.ToInt32(drListarProvincia.GetValue(0));
                    //objProvinciaBE.DepartamentoId = drListarProvincia.GetInt32(drListarProvincia.GetOrdinal("DepartamentoId"));// Convert.ToInt32(drListarProvincia.GetValue(1));
                    objProvinciaBE.Provincia = drListarProvincia.GetString(drListarProvincia.GetOrdinal("Nombre"));//drListarProvincia.GetValue(2).ToString();
                    lstProvinciaBE.Add(objProvinciaBE);
                }


                Conn.Close();
                Conn.Dispose();
                cmdListarProvincia.Dispose();
                return lstProvinciaBE;


            }
            catch (Exception ex)
            {
                throw;
            }




        }


        public List<DistritoBE> ListarDistrito(string ProvinciaId, string DepartamentoId)
        {
            String SqlListarDistrito;
            String sCadena;
            SqlConnection Conn;
            SqlCommand cmdListarDistrito = null;
            SqlDataReader drListarDistrito = null;



            try
            {
                sCadena = Utilities.GetConnectionStringSeguridad();
                Conn = new SqlConnection(sCadena);

                SqlListarDistrito = "usp_TalListarDistritos";
                cmdListarDistrito = Conn.CreateCommand();
                cmdListarDistrito.CommandType = CommandType.StoredProcedure;
                cmdListarDistrito.CommandText = SqlListarDistrito;

               

                SqlParameter prmProvinciaId = new SqlParameter();
                prmProvinciaId.ParameterName = "@provinciaid";
                prmProvinciaId.SqlDbType = SqlDbType.VarChar;
                prmProvinciaId.Size = 5;
                prmProvinciaId.Value = ProvinciaId;


                cmdListarDistrito.Parameters.Add(prmProvinciaId);

                SqlParameter prmDepartamentoId = new SqlParameter();
                prmDepartamentoId.ParameterName = "@departamentoid";
                prmDepartamentoId.SqlDbType = SqlDbType.VarChar;
                prmDepartamentoId.Size = 5;
                prmDepartamentoId.Value = DepartamentoId;

                cmdListarDistrito.Parameters.Add(prmDepartamentoId);

                cmdListarDistrito.Connection.Open();
                drListarDistrito = cmdListarDistrito.ExecuteReader();

                List<DistritoBE> lstDistritoBE = new List<DistritoBE>();

                DistritoBE objDistritoBE;

                while (drListarDistrito.Read())
                {
                    objDistritoBE = new DistritoBE();
                    objDistritoBE.DistritoId = drListarDistrito.GetInt32(drListarDistrito.GetOrdinal("UbigeoId"));// Convert.ToInt32(drListarDistrito.GetValue(0));

                    //objDistritoBE.ProvinciaId = drListarDistrito.GetInt32(drListarDistrito.GetOrdinal("ProvinciaId")); //Convert.ToInt32(drListarDistrito.GetValue(2));
                    objDistritoBE.Distrito = drListarDistrito.GetString(drListarDistrito.GetOrdinal("Nombre"));// drListarDistrito.GetValue(3).ToString();
                    lstDistritoBE.Add(objDistritoBE);
                }

                Conn.Close();
                Conn.Dispose();
                cmdListarDistrito.Dispose();
                return lstDistritoBE;




            }
            catch (Exception ex)
            {
                throw;
            }
        }

    }
}
