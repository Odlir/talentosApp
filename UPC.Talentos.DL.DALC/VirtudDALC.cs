using System;
using System.Collections.Generic;
using UPC.Talentos.BL.BE;
using System.Data.SqlClient;
using System.Data;

namespace UPC.Talentos.DL.DALC
{
    public class VirtudDALC
    {
        private String pathImages = "./images/talentos/images/";
        private String pathExample = "./images/talentos/example/";

        public List<TalentoComplexBE> ListarVirtudes(int idVirtud)
        {
            SqlConnection Conn = null;
            SqlCommand cmdVirtudListar = null;
            SqlDataReader drVirtudes;
            SqlParameter prmIdVirtud;
            String sCadenaConexion;
            String sqlVirtudListar;

            try
            {
                sCadenaConexion = Utilities.GetConnectionStringTalentos2();

                sqlVirtudListar = "usp_TalVirtudListar";

                Conn = new SqlConnection(sCadenaConexion);

                prmIdVirtud = new SqlParameter("@idVirtud", idVirtud);
                prmIdVirtud.SqlDbType = SqlDbType.Int;

                cmdVirtudListar = Conn.CreateCommand();
                cmdVirtudListar.CommandType = CommandType.StoredProcedure;
                cmdVirtudListar.CommandText = sqlVirtudListar;

                cmdVirtudListar.Parameters.Add(prmIdVirtud);

                List<TalentoComplexBE> lstVirtudes;
                TalentoComplexBE objVirtudBE;

                cmdVirtudListar.Connection.Open();
                drVirtudes = cmdVirtudListar.ExecuteReader();

                lstVirtudes = new List<TalentoComplexBE>();

                while (drVirtudes.Read())
                {
                    objVirtudBE = new TalentoComplexBE();
                    objVirtudBE.idTalento = drVirtudes.GetInt32(drVirtudes.GetOrdinal("Talento_id"));
                    objVirtudBE.idTendencia = drVirtudes.GetInt32(drVirtudes.GetOrdinal("Tendencia_id"));
                    objVirtudBE.nombre = drVirtudes.GetString(drVirtudes.GetOrdinal("Nombre"));
                    objVirtudBE.descripcion = drVirtudes.GetString(drVirtudes.GetOrdinal("Descripcion"));
                    objVirtudBE.tendencia = drVirtudes.GetString(drVirtudes.GetOrdinal("Tendencia"));
                    objVirtudBE.image = pathImages + drVirtudes.GetString(drVirtudes.GetOrdinal("Imagen"));

                    lstVirtudes.Add(objVirtudBE);
                }

                cmdVirtudListar.Connection.Close();
                cmdVirtudListar.Dispose();
                Conn.Dispose();

                return lstVirtudes;
            }
            catch (Exception ex)
            {
                Conn.Dispose();
                cmdVirtudListar.Dispose();

                throw;
            }
        }

        public bool ActualizarVirtud(TalentoComplexBE objVirtud)
        {
            SqlConnection Conn = null;
            SqlCommand cmdVirtudActualizar = null;
            SqlParameter prmIdVirtud;
            SqlParameter prmIdTendencia;
            SqlParameter prmNombre;
            SqlParameter prmDescripcion;
            String sCadenaConexion;
            String sqlVirtudActualizar;

            try
            {
                sCadenaConexion = Utilities.GetConnectionStringTalentos2();

                sqlVirtudActualizar = "usp_VirtudActualizar";

                Conn = new SqlConnection(sCadenaConexion);

                cmdVirtudActualizar = Conn.CreateCommand();
                cmdVirtudActualizar.CommandType = CommandType.StoredProcedure;
                cmdVirtudActualizar.CommandText = sqlVirtudActualizar;

                prmIdVirtud = new SqlParameter("@Virtud_id", objVirtud.idTalento);
                prmIdVirtud.SqlDbType = SqlDbType.Int;

                prmIdTendencia = new SqlParameter("@Tendencia_id", objVirtud.idTendencia);
                prmIdTendencia.SqlDbType = SqlDbType.Int;

                prmNombre = new SqlParameter("@Nombre", objVirtud.nombre);
                prmNombre.SqlDbType = SqlDbType.VarChar;
                prmNombre.Size = 60;

                prmDescripcion = new SqlParameter("@Descripcion", objVirtud.descripcion);
                prmDescripcion.SqlDbType = SqlDbType.VarChar;
                prmDescripcion.Size = 5000;

                cmdVirtudActualizar.Parameters.Add(prmIdVirtud);
                cmdVirtudActualizar.Parameters.Add(prmIdTendencia);
                cmdVirtudActualizar.Parameters.Add(prmNombre);
                cmdVirtudActualizar.Parameters.Add(prmDescripcion);

                cmdVirtudActualizar.Connection.Open();
                cmdVirtudActualizar.ExecuteNonQuery();

                cmdVirtudActualizar.Connection.Close();
                cmdVirtudActualizar.Dispose();
                Conn.Dispose();

                return true;
            }
            catch (Exception ex)
            {
                Conn.Dispose();
                cmdVirtudActualizar.Dispose();

                return false;
            }
        }
    }
}
