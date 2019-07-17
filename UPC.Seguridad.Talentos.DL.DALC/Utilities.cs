using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Configuration;

namespace UPC.Seguridad.Talentos.DL.DALC
{
    internal sealed class Utilities
    {
        public static String GetConnectionStringSeguridad()
        {
            return ConfigurationSettings.AppSettings["strCadenaConexionSeguridad"];
        }
    }
}
