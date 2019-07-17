using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UPC.Talentos.DL.DALC;

namespace UPC.Talentos.BL.BC
{
    public class LogBC
    {
        public void InsertarLog(string mensaje)
        {
            LogDALC objLogDALC = new LogDALC();

            objLogDALC.InsertarLog(mensaje);
        }
    }
}
