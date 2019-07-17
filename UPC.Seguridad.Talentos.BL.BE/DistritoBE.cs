using System;

namespace UPC.Seguridad.Talentos.BL.BE
{
    public class DistritoBE
    {
        int _DistritoId;

        public int DistritoId
        {
            get { return _DistritoId; }
            set { _DistritoId = value; }
        }
        int _ProvinciaId;

        public int ProvinciaId
        {
            get { return _ProvinciaId; }
            set { _ProvinciaId = value; }
        }
      
        
        String _Distrito;

        public String Distrito
        {
            get { return _Distrito; }
            set { _Distrito = value; }
        }
    }
}
