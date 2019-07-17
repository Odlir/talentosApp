using System;

namespace UPC.Seguridad.Talentos.BL.BE
{
    public class DepartamentoBE
    {
        string _DepartamentoId;

        public string DepartamentoId
        {
            get { return _DepartamentoId; }
            set { _DepartamentoId = value; }
        }
        String _Departamento;

        public String Departamento
        {
            get { return _Departamento; }
            set { _Departamento = value; }
        }

        int _PaisId;

        public int PaisId
        {
            get { return _PaisId; }
            set { _PaisId = value; }
        }

    }
}
