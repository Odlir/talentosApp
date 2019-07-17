using System;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;

namespace UPC.Seguridad.Talentos.BL.BE
{
    public class RespuestaBE
    {
        UsuarioBE _UsuarioLogueado;

        public UsuarioBE UsuarioLogueado
        {
            get { return _UsuarioLogueado; }
            set { _UsuarioLogueado = value; }
        }
        int _Valor;

        public int Valor
        {
            get { return _Valor; }
            set { _Valor = value; }
        }
        String _Mensaje;

        public String Mensaje
        {
            get { return _Mensaje; }
            set { _Mensaje = value; }
        }

    }
}
