using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;

namespace UPC.Talentos.DL.DALC
{
    public class LogDALC
    {
        public void InsertarLog(string mensaje)
        {
            String sqlInsertaMensajeLog;
            string sCadena;
            SqlConnection Conn;
            SqlCommand cmdInsertaLog = null;
            SqlParameter prmMensaje;

            try
            {
                sCadena = Utilities.GetConnectionStringTalentos2();
                Conn = new SqlConnection(sCadena);

                sqlInsertaMensajeLog = "usp_TalInsertarLog";
                cmdInsertaLog = Conn.CreateCommand();
                cmdInsertaLog.CommandType = System.Data.CommandType.StoredProcedure;
                cmdInsertaLog.CommandText = sqlInsertaMensajeLog;

                prmMensaje = new SqlParameter();
                prmMensaje.ParameterName = "@Mensaje";
                prmMensaje.SqlDbType = System.Data.SqlDbType.VarChar;
                prmMensaje.Size = 1000;
                prmMensaje.Value = mensaje;

                cmdInsertaLog.Parameters.Add(prmMensaje);

                cmdInsertaLog.Connection.Open();
                cmdInsertaLog.ExecuteNonQuery();

                cmdInsertaLog.Connection.Close();
                cmdInsertaLog.Dispose();
                Conn.Dispose();
            }
            catch (Exception ex)
            {
                cmdInsertaLog.Dispose();
                throw ex;
            }
        }
    }
}
