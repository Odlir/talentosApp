using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UPC.Talentos.BL.BE;
using System.Data.SqlClient;
using System.Data;

namespace UPC.Talentos.DL.DALC
{
    public class SkinDALC
    {
        public SkinBE SkinObtener(String descripcion)
        {

            SqlConnection conn = null;
            SqlCommand cmdSkinObtener = null;
            SqlParameter prmCodSkin = null;

            String sCadenaConexion;
            String sqlSkinObtener;
            SqlDataReader drSkin;
            try
            {

                sCadenaConexion = Utilities.GetConnectionStringTalentos();
                conn = new SqlConnection(sCadenaConexion);

                sqlSkinObtener = "usps_TalSkin";
                cmdSkinObtener = conn.CreateCommand();
                cmdSkinObtener.CommandText = sqlSkinObtener;
                cmdSkinObtener.CommandType = CommandType.StoredProcedure;

                prmCodSkin = cmdSkinObtener.CreateParameter();
                prmCodSkin.ParameterName = "@Descripcion";
                prmCodSkin.SqlDbType = SqlDbType.VarChar;
                prmCodSkin.Size = 150;
                prmCodSkin.Value = descripcion;

                cmdSkinObtener.Parameters.Add(prmCodSkin);
                cmdSkinObtener.Connection.Open();
                drSkin = cmdSkinObtener.ExecuteReader();

                SkinBE objSkinBE;
                objSkinBE = new SkinBE();
                if (drSkin.Read())
                {
                    objSkinBE.SkinId = drSkin.GetInt32(drSkin.GetOrdinal("Skin_id"));
                    objSkinBE.Descripcion = drSkin.GetString(drSkin.GetOrdinal("Descripcion"));
                    objSkinBE.RutaTalentos = drSkin.GetString(drSkin.GetOrdinal("RutaTalentos"));
                    objSkinBE.Activo = drSkin.GetInt32(drSkin.GetOrdinal("Activo"));
                    objSkinBE.RutaEjemplos = drSkin.GetString(drSkin.GetOrdinal("RutaEjemplos"));
                   
                }

                cmdSkinObtener.Connection.Close();
                cmdSkinObtener.Dispose();
                conn.Dispose();

                return objSkinBE;
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public SkinBE SkinActivoObtener()
        {

            SqlConnection conn = null;
            SqlCommand cmdSkinObtener = null;


            String sCadenaConexion;
            String sqlSkinObtener;
            SqlDataReader drSkin;
            try
            {

                sCadenaConexion = Utilities.GetConnectionStringTalentos2();
                conn = new SqlConnection(sCadenaConexion);

                sqlSkinObtener = "usps_TalSkinActivo";
                cmdSkinObtener = conn.CreateCommand();
                cmdSkinObtener.CommandText = sqlSkinObtener;
                cmdSkinObtener.CommandType = CommandType.StoredProcedure;

                cmdSkinObtener.Connection.Open();
                drSkin = cmdSkinObtener.ExecuteReader();

                SkinBE objSkinBE;
                objSkinBE = new SkinBE();
                if (drSkin.Read())
                {
                    objSkinBE.SkinId = drSkin.GetInt32(drSkin.GetOrdinal("Skin_id"));
                    objSkinBE.Descripcion = drSkin.GetString(drSkin.GetOrdinal("Descripcion"));
                    objSkinBE.RutaTalentos = drSkin.GetString(drSkin.GetOrdinal("RutaTalentos"));
                    objSkinBE.Activo = drSkin.GetInt32(drSkin.GetOrdinal("Activo"));
                    objSkinBE.RutaEjemplos = drSkin.GetString(drSkin.GetOrdinal("RutaEjemplos"));

                }

                cmdSkinObtener.Connection.Close();
                cmdSkinObtener.Dispose();
                conn.Dispose();

                return objSkinBE;
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public bool ActualizarSkin(SkinBE objSkin)
        {

            SqlConnection conn = null;
            SqlCommand cmdSkinActualizar = null;
            SqlParameter prmDescripSkin = null;

            String sCadenaConexion;
            String sqlSkinActualizar;
           
            try
            {

                sCadenaConexion = Utilities.GetConnectionStringTalentos2();
                conn = new SqlConnection(sCadenaConexion);

                sqlSkinActualizar = "uspu_TalSkin";
                cmdSkinActualizar = conn.CreateCommand();
                cmdSkinActualizar.CommandText = sqlSkinActualizar;
                cmdSkinActualizar.CommandType = CommandType.StoredProcedure;

                prmDescripSkin = cmdSkinActualizar.CreateParameter();
                prmDescripSkin.ParameterName = "@Descripcion";
                prmDescripSkin.SqlDbType = SqlDbType.VarChar;
                prmDescripSkin.Size = 150;
                prmDescripSkin.Value = objSkin.Descripcion;

                cmdSkinActualizar.Parameters.Add(prmDescripSkin);

                //SqlParameter prmRutaTalentos = new SqlParameter();
                //prmRutaTalentos = cmdSkinActualizar.CreateParameter();
                //prmRutaTalentos.ParameterName = "@RutaTalentos";
                //prmRutaTalentos.SqlDbType = SqlDbType.VarChar;
                //prmRutaTalentos.Size = 150;
                //prmRutaTalentos.Value = objSkin.RutaTalentos;

                //cmdSkinActualizar.Parameters.Add(prmRutaTalentos);

                //SqlParameter prmRutaEjemplos = new SqlParameter();

                //prmRutaEjemplos = cmdSkinActualizar.CreateParameter();
                //prmRutaEjemplos.ParameterName = "@RutaEjemplos";
                //prmRutaEjemplos.SqlDbType = SqlDbType.VarChar;
                //prmRutaEjemplos.Size = 150;
                //prmRutaEjemplos.Value = objSkin.RutaEjemplos;

                //cmdSkinActualizar.Parameters.Add(prmRutaEjemplos);

                SqlParameter prmActivo = new SqlParameter();

                prmActivo = cmdSkinActualizar.CreateParameter();
                prmActivo.ParameterName = "@Activo";
                prmActivo.SqlDbType = SqlDbType.Int;
                prmActivo.Value = objSkin.Activo;

                cmdSkinActualizar.Parameters.Add(prmActivo);

                cmdSkinActualizar.Connection.Open();
                cmdSkinActualizar.ExecuteNonQuery();
                cmdSkinActualizar.Connection.Close();

                cmdSkinActualizar.Dispose();
                conn.Dispose();

                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
    }
}
