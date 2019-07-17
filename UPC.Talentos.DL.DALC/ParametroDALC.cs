using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UPC.Talentos.BL.BE;
using System.Data.SqlClient;
using System.Data;

namespace UPC.Talentos.DL.DALC
{
    public class ParametroDALC
    {
      
        private String sCadena = Utilities.GetConnectionStringTalentos();

        #region Skins
            public ParametroBE obtenerTemaActual()
            {
                ParametroBE objParametroBE = null;
                SqlParameter prmDescripcion = null;
                SqlCommand cmd = null;
                SqlDataReader dr = null;
                SqlConnection conn = null;
                String strUSP = "usps_TalParametro";
                String descripcion = "Tema";
                try
                {
                    conn = new SqlConnection(sCadena);
                    cmd = new SqlCommand(strUSP, conn);
                    cmd.CommandType = CommandType.StoredProcedure;

                    prmDescripcion = new SqlParameter();
                    prmDescripcion.ParameterName = "@Descripcion";
                    prmDescripcion.SqlDbType = SqlDbType.VarChar;
                    prmDescripcion.Size = 100;
                    prmDescripcion.Value = descripcion;

                    cmd.Parameters.Add(prmDescripcion);

                    cmd.Connection.Open();
                    dr = cmd.ExecuteReader();

                    if(dr.HasRows)
                    {
                        dr.Read();
                        objParametroBE = new ParametroBE();
                        objParametroBE.Value = dr.GetString(dr.GetOrdinal("Valor"));
                        objParametroBE.Fecha = dr.GetDateTime(dr.GetOrdinal("Fecha"));
                    }
                    cmd.Connection.Close();
                    return objParametroBE;
                }
                catch (Exception ex)
                {
                    throw;
                }
            }
            public void actualizarTemaActual(ParametroBE objParametroBE)
            {
                SqlParameter prmDescripcion = null;
                SqlParameter prmValue = null;
                SqlParameter prmFecha = null;
                SqlCommand cmd = null;
                SqlConnection conn = null;
                String strUSP = "uspu_TalParametro";
                String descripcion = "Tema";
                try
                {
                    conn = new SqlConnection(sCadena);
                    cmd = new SqlCommand(strUSP, conn);
                    cmd.CommandType = CommandType.StoredProcedure;

                    prmDescripcion = new SqlParameter();
                    prmDescripcion.ParameterName = "@Descripcion";
                    prmDescripcion.SqlDbType = SqlDbType.VarChar;
                    prmDescripcion.Size = 100;
                    prmDescripcion.Value = descripcion;

                    prmValue = new SqlParameter();
                    prmValue.ParameterName = "@Value";
                    prmValue.SqlDbType = SqlDbType.VarChar;
                    prmValue.Size = 100;
                    prmValue.Value = objParametroBE.Value;

                    prmFecha = new SqlParameter();
                    prmFecha.ParameterName = "@Fecha";
                    prmFecha.SqlDbType = SqlDbType.DateTime;
                    prmFecha.Value = DateTime.Now;

                    cmd.Parameters.Add(prmDescripcion);
                    cmd.Parameters.Add(prmValue);
                    cmd.Parameters.Add(prmFecha);

                    cmd.Connection.Open();
                    cmd.ExecuteNonQuery();
                    cmd.Connection.Close();
                }
                catch (Exception ex)
                {
                    throw;
                }
            }
        #endregion
    }
}
