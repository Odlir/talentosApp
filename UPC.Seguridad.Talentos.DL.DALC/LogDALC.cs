using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UPC.Seguridad.Talentos.BL.BE;
using System.Data.SqlClient;
using System.Data;

namespace UPC.Seguridad.Talentos.DL.DALC
{
    public class LogDALC
    {
        public int LogInsertar(LogBE be)
        {
            try
            {
                
                String sCadena = Utilities.GetConnectionStringSeguridad();

                String sqlLogInsertar;

                sqlLogInsertar = "uspi_TalLog";
                SqlParameter[] ArrSqlParameter = new SqlParameter[3];

                ArrSqlParameter[0] = new SqlParameter();
                ArrSqlParameter[0].ParameterName = "@Descripcion";
                ArrSqlParameter[0].SqlDbType = SqlDbType.VarChar;
                ArrSqlParameter[0].Size = 900;
                ArrSqlParameter[0].Value = be.Descripcion;

                ArrSqlParameter[1] = new SqlParameter();
                ArrSqlParameter[1].ParameterName = "@UsuarioId";
                ArrSqlParameter[1].SqlDbType = SqlDbType.Int;
                ArrSqlParameter[1].Value = be.UsuarioId;






                SqlHelper.ExecuteNonQuery(sCadena, CommandType.StoredProcedure, sqlLogInsertar, ArrSqlParameter);




                return 1;

            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
    }
}
