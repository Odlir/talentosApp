using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Configuration;

namespace UPC.Talentos.DL.DALC
{
    internal sealed class Utilities
    {
        public static String GetConnectionStringTalentos()
        {
            return ConfigurationSettings.AppSettings["strCadenaConexion"];
        }

        public static String GetConnectionStringTalentos2()
        {
            return ConfigurationSettings.AppSettings["strCadenaConexion2"];
        }


    }
}
