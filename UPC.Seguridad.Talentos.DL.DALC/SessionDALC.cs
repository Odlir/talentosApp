using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using UPC.Seguridad.Talentos.BL.BE;
using System.Data.SqlClient;

namespace UPC.Seguridad.Talentos.DL.DALC
{
    public class SesionDALC
    {
        public int SesionInsertar(SesionBE objSesion)
        {
            String SqlInsertarSesion;
            string sCadena;
            Int32 res = 0;


            try
            {

                sCadena = Utilities.GetConnectionStringSeguridad();
                SqlInsertarSesion = "usp_TalSesionInsertar";

                SqlParameter[] arrParameter = new SqlParameter[2];

                arrParameter[0] = new SqlParameter();
                arrParameter[0].SqlDbType = SqlDbType.Int;
                arrParameter[0].Value = objSesion.Participante_id;
                arrParameter[0].ParameterName = "@IdUsuario";

                arrParameter[1] = new SqlParameter();
                arrParameter[1].SqlDbType = SqlDbType.Int;
                arrParameter[1].Direction = ParameterDirection.ReturnValue;

                return SqlHelper.ExecuteNonQuery(sCadena, CommandType.StoredProcedure, SqlInsertarSesion, arrParameter);

                //res = Convert.ToInt32(arrParameter[1].Value);


            }
            catch (Exception ex)
            {
                return -2;
            }
            //return res;
        }

        public int SesionEliminar(SesionBE objSesion)
        {
            String SqlEliminarSesion;
            string sCadena;
            Int32 res = 0;
            try
            {

                sCadena = Utilities.GetConnectionStringSeguridad();

                SqlEliminarSesion = "usp_TalSesionEliminar";

                SqlParameter[] arrParameter = new SqlParameter[2];

                arrParameter[0] = new SqlParameter();
                arrParameter[0].SqlDbType = SqlDbType.Int;
                arrParameter[0].Value = objSesion.Participante_id;
                arrParameter[0].ParameterName = "@Participante_id";

                arrParameter[1] = new SqlParameter();
                arrParameter[1].SqlDbType = SqlDbType.Int;
                arrParameter[1].Direction = ParameterDirection.ReturnValue;

                SqlHelper.ExecuteNonQuery(sCadena, CommandType.StoredProcedure, SqlEliminarSesion, arrParameter);

                res = Convert.ToInt32(arrParameter[1].Value);



            }
            catch (Exception ex)
            {
                throw ex;
            }

            return res;
        }

        public List<SesionBE> SesionVericarActivacion()
        {
            String SqlListarSesion;
            string sCadena;

            try
            {

                sCadena = Utilities.GetConnectionStringSeguridad();

                SqlListarSesion = "usp_TalSesionListar";


                SqlDataReader sqlData = SqlHelper.ExecuteReader(sCadena, CommandType.StoredProcedure, SqlListarSesion);

                List<SesionBE> lstSesion;
                lstSesion = new List<SesionBE>();

                while (sqlData.Read())
                {
                    SesionBE objSesionBE = new SesionBE();
                    objSesionBE.Participante_id = sqlData.GetInt32(Convert.ToInt32(sqlData.GetOrdinal("Participante_id")));
                    lstSesion.Add(objSesionBE);
                }


                return lstSesion;

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
