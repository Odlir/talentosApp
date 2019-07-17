using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UPC.Talentos.BL.BE;
using System.Data;
using System.Data.SqlClient;

namespace UPC.Talentos.DL.DALC
{
    
    public class JuegoDALC
    {
        //Almacenar información del Juego actual
        public int JuegoInsertar(JuegoBE objJuego)
        {
            String SqlInsertarJuego;
            string sCadena;

            try
            {
                //sCadena = "server=(local); database=db_NetFTalentos; Integrated Security=true";
                sCadena = Utilities.GetConnectionStringTalentos();

                SqlInsertarJuego = "uspi_TalJuego";

                SqlParameter[] arrParameter = new SqlParameter[3];

                arrParameter[0] = new SqlParameter();
                arrParameter[0].SqlDbType = SqlDbType.Int;
                arrParameter[0].Direction = ParameterDirection.ReturnValue;

                arrParameter[1] = new SqlParameter();
                arrParameter[1].ParameterName = "@NickName";
                arrParameter[1].SqlDbType = SqlDbType.VarChar;
                arrParameter[1].Size = 50;
                arrParameter[1].Value = objJuego.NickName;

                arrParameter[2] = new SqlParameter();
                arrParameter[2].ParameterName = "@Fecha";
                arrParameter[2].SqlDbType = SqlDbType.DateTime;
                arrParameter[2].Value = objJuego.Fecha;

                SqlHelper.ExecuteNonQuery(sCadena, CommandType.StoredProcedure, SqlInsertarJuego, arrParameter);
                return (int)arrParameter[0].Value;

            }
            catch (Exception ex)
            {
                throw ex;
            }


        }

        public int InsertarJuego(JuegoBE objJuego)
        {
            String SqlInsertarJuego;
            string sCadena;

            try
            {
                //sCadena = "server=(local); database=db_NetFTalentos; Integrated Security=true";
                sCadena = Utilities.GetConnectionStringTalentos2();

                SqlInsertarJuego = "uspi_TalJuego";

                SqlParameter[] arrParameter = new SqlParameter[4];

                arrParameter[0] = new SqlParameter();
                arrParameter[0].SqlDbType = SqlDbType.Int;
                arrParameter[0].Direction = ParameterDirection.ReturnValue;

                arrParameter[1] = new SqlParameter();
                arrParameter[1].ParameterName = "@Evento_id";
                arrParameter[1].SqlDbType = SqlDbType.Int;
                arrParameter[1].Value = objJuego.EventoId;


                arrParameter[2] = new SqlParameter();
                arrParameter[2].ParameterName = "@Participante_id";
                arrParameter[2].SqlDbType = SqlDbType.Int;
                arrParameter[2].Value = objJuego.ParticipanteId;

                arrParameter[3] = new SqlParameter();
                arrParameter[3].ParameterName = "@Fecha";
                arrParameter[3].SqlDbType = SqlDbType.DateTime;
                arrParameter[3].Value = objJuego.Fecha;

                SqlHelper.ExecuteNonQuery(sCadena, CommandType.StoredProcedure, SqlInsertarJuego, arrParameter);
                return (int)arrParameter[0].Value;

            }
            catch (Exception ex)
            {
                throw ex;
            }


        }
    }

}
