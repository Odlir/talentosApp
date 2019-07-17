using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UPC.Seguridad.Talentos.DL.DALC;
using UPC.Seguridad.Talentos.BL.BE;

namespace UPC.Seguridad.Talentos.BL.BC
{
    public class LogBC
    {
        public int LogInsertar(LogBE be)
        {
            LogDALC dalc = new LogDALC();
            return dalc.LogInsertar(be);
        }
    }
}