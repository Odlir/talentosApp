using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UPC.Seguridad.Talentos.BL.BE;
using UPC.Seguridad.Talentos.DL.DALC;

namespace UPC.Seguridad.Talentos.BL.BC
{
    public class UbigeoBC
    {
        public List<PaisBE> ListarPaises()
        {
            UbigeoDALC dalc = new UbigeoDALC();
            return dalc.ListarPaises();
        }

        public List<DepartamentoBE> ListarDepartamento()
        {
            UbigeoDALC dalc = new UbigeoDALC();
            return dalc.ListarDepartamento();
        }

        public List<ProvinciaBE> ListarProvincia(string DepartamentoId)
        {
            UbigeoDALC dalc = new UbigeoDALC();
            return dalc.ListarProvincia(DepartamentoId);


        }


        public List<DistritoBE> ListarDistrito(string ProvinciaId, string DepartamentoId)
        {
            UbigeoDALC dalc = new UbigeoDALC();
            return dalc.ListarDistrito(ProvinciaId, DepartamentoId);
        }



    }
}
