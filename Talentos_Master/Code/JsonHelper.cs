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
using System.IO;
using System.Runtime.Serialization.Json;
using System.Text;

namespace Talentos_Master
{
    public static class JsonHelper
    {
        public static UsuarioJson DeserializeToPerson(string jsonString)
        {
            using (MemoryStream ms = new MemoryStream(Encoding.Unicode.GetBytes(jsonString)))
            {
                DataContractJsonSerializer serializer =
                        new DataContractJsonSerializer(typeof(UsuarioJson));

                return (UsuarioJson)serializer.ReadObject(ms);
            }
        }
    }
}
