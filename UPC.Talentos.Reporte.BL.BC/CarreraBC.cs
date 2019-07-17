using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UPC.Talentos.Reporte.BL.BE;
using UPC.Talentos.Reporte.DL.DALC;

namespace UPC.Talentos.Reporte.BL.BC
{
    public class CarreraBC
    {
        public List<CarreraBE> ListarCarreras(string Codigo)
        {
            CarreraDALC objCarreraDALC = new CarreraDALC();

            return objCarreraDALC.ListarCarreras(Codigo);
        }

        public List<string> ListarCarreras(string condicion1, string condicion2, string condicion3, int tipo)
        {
            CarreraDALC objCarreraDALC = new CarreraDALC();

            return objCarreraDALC.ListarCarreras(condicion1, condicion2, condicion3, tipo);
        }

        public List<ElementoTemperamentoBE> ListarElementos() 
        {
            CarreraDALC objCarreraDALC = new CarreraDALC();

            return objCarreraDALC.ListarElementos();
        }

        public string ObtenerDescripcionRueda(string id) 
        {
            CarreraDALC objCarreraDALC = new CarreraDALC();

            return objCarreraDALC.ObtenerDescripcionRueda(id);
        }

        public List<FortalezaTemperamentoBE> ObtenerFortalezas(string eje1,string eje2,string eje3,string eje4)
        {
            CarreraDALC objCarreraDALC = new CarreraDALC();

            return objCarreraDALC.ObtenerFortalezas(eje1, eje2, eje3, eje4);
        }
    }
}
