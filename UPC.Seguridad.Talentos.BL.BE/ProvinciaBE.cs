using System;

namespace UPC.Seguridad.Talentos.BL.BE
{
    public class ProvinciaBE
    {
        string _ProvinciaId;

        public string ProvinciaId
        {
            get { return _ProvinciaId; }
            set { _ProvinciaId = value; }
        }
        int _DepartamentoId;

        public int DepartamentoId
        {
            get { return _DepartamentoId; }
            set { _DepartamentoId = value; }
        }
        String _Provincia;

        public String Provincia
        {
            get { return _Provincia; }
            set { _Provincia = value; }
        }
    }
}
