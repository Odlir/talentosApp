using System;
using System.Collections.Generic;
using UPC.Talentos.BL.BE;
using System.Data.SqlClient;
using System.Data;

namespace UPC.Talentos.DL.DALC
{
    public class TipoTalentoDALC
    {
        public List<TipoTalentoBE> ListarTipoTalento()
        {
            SqlConnection Conn = null;
            SqlCommand cmdTipoTalentoListar = null;
            SqlDataReader drTipoTalentos;
            String sCadenaConexion;
            String sqlTipoTalentoListar;

            try
            {
                sCadenaConexion = Utilities.GetConnectionStringTalentos2();

                sqlTipoTalentoListar = "usp_TipoTalentoListar";

                Conn = new SqlConnection(sCadenaConexion);

                cmdTipoTalentoListar = Conn.CreateCommand();
                cmdTipoTalentoListar.CommandType = CommandType.StoredProcedure;
                cmdTipoTalentoListar.CommandText = sqlTipoTalentoListar;

                List<TipoTalentoBE> lstTipoTalentos;
                TipoTalentoBE objTipoTalentoBE;

                cmdTipoTalentoListar.Connection.Open();
                drTipoTalentos = cmdTipoTalentoListar.ExecuteReader();

                lstTipoTalentos = new List<TipoTalentoBE>();

                while (drTipoTalentos.Read())
                {
                    objTipoTalentoBE = new TipoTalentoBE();
                    objTipoTalentoBE.idTipoTalento = drTipoTalentos.GetInt32(drTipoTalentos.GetOrdinal("TipoTalento_id"));
                    objTipoTalentoBE.nombre = drTipoTalentos.GetString(drTipoTalentos.GetOrdinal("Nombre"));

                    lstTipoTalentos.Add(objTipoTalentoBE);
                }

                cmdTipoTalentoListar.Connection.Close();
                cmdTipoTalentoListar.Dispose();
                Conn.Dispose();

                return lstTipoTalentos;
            }
            catch (Exception ex)
            {
                Conn.Dispose();
                cmdTipoTalentoListar.Dispose();

                throw;
            }
        }
    }
}
