using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UPC.Seguridad.Talentos.DL.DALC;
using UPC.Seguridad.Talentos.BL.BE;

namespace UPC.Seguridad.Talentos.BL.BC
{
    public class SesionBC
    {

        public int ValidarSesion(int participante_id)
        {
            SesionDALC objSesionDALC = new SesionDALC();
            List<SesionBE> lstSesionBE = objSesionDALC.SesionVericarActivacion();


            for (int i = 0; i < lstSesionBE.Count; i++)
            {
                if (participante_id == lstSesionBE[i].Participante_id)
                    return 1; // existe otra sesion 
            }


            return 0;  // es correcto
        }

        public int GuardarSesion(SesionBE objSesion)
        {


            SesionDALC objSesionDALC = new SesionDALC();
            return objSesionDALC.SesionInsertar(objSesion);

        }


        public int EliminarSesion(SesionBE objSesion)
        {
            SesionDALC objSesionDALC = new SesionDALC();
            return objSesionDALC.SesionEliminar(objSesion);
        }



    }
}
