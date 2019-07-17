using System;

namespace UPC.Seguridad.Talentos.BL.BE
{
    public class SesionBE
    {
        private int _Participante_id;
        private String _Password;

        public String Password
        {
            get { return _Password; }
            set { _Password = value; }
        }

        public int Participante_id
        {
            get { return _Participante_id; }
            set { _Participante_id = value; }
        }
    }
}
