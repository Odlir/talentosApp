using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UPC.Seguridad.Talentos.BL.BE;
using UPC.Seguridad.Talentos.DL.DALC;
using System.Security.Cryptography;
using System.IO;

namespace UPC.Seguridad.Talentos.BL.BC
{
    public class UsuarioBC
    {
        //public UsuarioBE UsuarioValidar(UsuarioBE objUsuarioBE)
        //{
        //    UsuarioDALC dalc = new UsuarioDALC();
        //    return dalc.UsuarioValidar(objUsuarioBE);

        //}

        //public UsuarioBE UsuarioValidarNickName(UsuarioBE objUsuarioBE)
        //{
        //    UsuarioDALC dalc = new UsuarioDALC();
        //    return dalc.UsuarioValidarNickName(objUsuarioBE);

        //}

        //public UsuarioBE UsuarioValidarLog(UsuarioBE objUsuarioBE)
        //{
        //    UsuarioDALC dalc = new UsuarioDALC();
        //    return dalc.UsuarioValidarLog(objUsuarioBE);

        //}

        //public int UsuarioInsertar(UsuarioBE objUsuarioBE)
        //{
        //    UsuarioDALC dalc = new UsuarioDALC();
        //    return dalc.UsuarioInsertar(objUsuarioBE);
        //}

        //public int LogInsertar(UsuarioBE objUsuarioBE)
        //{
        //    UsuarioDALC dalc = new UsuarioDALC();
        //    return dalc.LogInsertar(objUsuarioBE);
        //}


        public UsuarioBE EncriptarMd5(UsuarioBE objUsuario)
        {
            String llave = GenerarLlave("talentos");      

            // Convierto la cadena y la clave en arreglos de bytes
            // para poder usarlas en las funciones de encriptacion
            byte[] cadenaBytes = Encoding.UTF8.GetBytes(objUsuario.Password);
            byte[] claveBytes = Encoding.UTF8.GetBytes(llave);

            // Creo un objeto de la clase Rijndael
            RijndaelManaged rij = new RijndaelManaged();

            // Configuro para que utilice el modo ECB
            rij.Mode = CipherMode.ECB;

            // Configuro para que use encriptacion de 256 bits.
            rij.BlockSize = 256;

            // Declaro que si necesitara mas bytes agregue ceros.
            rij.Padding = PaddingMode.Zeros;

            // Declaro un encriptador que use mi clave secreta y un vector
            // de inicializacion aleatorio
            ICryptoTransform encriptador ;
            encriptador = rij.CreateEncryptor(claveBytes, rij.IV);

            // Declaro un stream de memoria para que guarde los datos
            // encriptados a medida que se van calculando
            MemoryStream memStream = new MemoryStream();

            // Declaro un stream de cifrado para que pueda escribir aqui
            // la cadena a encriptar. Esta clase utiliza el encriptador
            // y el stream de memoria para realizar la encriptacion
            // y para almacenarla
            CryptoStream cifradoStream;
            cifradoStream = new CryptoStream(memStream, encriptador, CryptoStreamMode.Write);

            // Escribo los bytes a encriptar. A medida que se va escribiendo
            // se va encriptando la cadena
            cifradoStream.Write(cadenaBytes, 0, cadenaBytes.Length);

            // Aviso que la encriptación se terminó
            cifradoStream.FlushFinalBlock();

            // Convert our encrypted data from a memory stream into a byte array.
            byte[] cipherTextBytes = memStream.ToArray();

            // Cierro los dos streams creados
            memStream.Close();
            cifradoStream.Close();

            // Convierto el resultado en base 64 para que sea legible
            // y devuelvo el resultado
            String resultado = Convert.ToBase64String(cipherTextBytes);

            UsuarioBE obj = new UsuarioBE();
            obj = objUsuario;
            obj.Password = resultado;

            return obj;



        }

        public UsuarioBE desencriptar(UsuarioBE objUsuario)
        {
            String clave = GenerarLlave("talentos");   

            // Convierto la cadena y la clave en arreglos de bytes
            // para poder usarlas en las funciones de encriptacion
            // En este caso la cadena la convierta usando base 64
            // que es la codificacion usada en el metodo encriptar
            byte[] cadenaBytes = Convert.FromBase64String(objUsuario.Password);
            byte[] claveBytes = Encoding.UTF8.GetBytes(clave);

            // Creo un objeto de la clase Rijndael
            RijndaelManaged rij = new RijndaelManaged();

            // Configuro para que utilice el modo ECB
            rij.Mode = CipherMode.ECB;

            // Configuro para que use encriptacion de 256 bits.
            rij.BlockSize = 256;

            // Declaro que si necesitara mas bytes agregue ceros.
            rij.Padding = PaddingMode.Zeros;

            // Declaro un desencriptador que use mi clave secreta y un vector
            // de inicializacion aleatorio
            ICryptoTransform desencriptador;
            desencriptador = rij.CreateDecryptor(claveBytes, rij.IV);

            // Declaro un stream de memoria para que guarde los datos
            // encriptados
            MemoryStream memStream = new MemoryStream(cadenaBytes);

            // Declaro un stream de cifrado para que pueda leer de aqui
            // la cadena a desencriptar. Esta clase utiliza el desencriptador
            // y el stream de memoria para realizar la desencriptacion
            CryptoStream cifradoStream;
            cifradoStream = new CryptoStream(memStream, desencriptador, CryptoStreamMode.Read);

            // Declaro un lector para que lea desde el stream de cifrado.
            // A medida que vaya leyendo se ira desencriptando.
            StreamReader lectorStream = new StreamReader(cifradoStream);

            // Leo todos los bytes y lo almaceno en una cadena
            string resultado = lectorStream.ReadToEnd();

            // Cierro los dos streams creados
            memStream.Close();
            cifradoStream.Close();

            // Devuelvo la cadena
            //return resultado;
            UsuarioBE obj = new UsuarioBE();
            obj = objUsuario;
            obj.Password = resultado;

            return obj;
        }


        static String GenerarLlave(String palabra)
        {
            String llave = "";
            DateTime fecha = DateTime.Today;
            int dia = fecha.Day;
            int mes = fecha.Month;
            int anio = fecha.Year;

            int tam = palabra.Length;

            int particiones = tam / 3;

            if (particiones * 3 < (tam))
                particiones += 1;

            int i = 1;
            int inicio;
            int suma;

            List<String> lst = new List<String>();

            while (i <= particiones)
            {

                inicio = tam - particiones * i;
                suma = particiones;

                if (inicio < 0)
                    inicio = 0;

                suma = particiones;

                lst.Add(palabra.Substring(inicio, suma));
                i++;


            }

            llave += lst[0];
            llave += dia;
            llave += lst[1];
            llave += mes;
            llave += lst[2];
            llave += anio;

            int lactual = llave.Length;

            if (lactual < 32)
            {
                int l = 32 - lactual;


                int j = 1;

                while (llave.Length < 32)
                {
                    llave += "0";
                    j++;
                }
            }


            return llave;

        }

          public List< UsuarioBE> ObtenerFamosoPorCarrera(int idCarrera)
          {
              UsuarioDALC dalc = new UsuarioDALC();
              return dalc.ObtenerFamosoPorCarrera(idCarrera);
          }

          public List<UsuarioBE> ObtenerFamosoPorCarrerasId(string idCarreras)
          {
              UsuarioDALC dalc = new UsuarioDALC();
              return dalc.ObtenerFamosoPorCarrerasId(idCarreras);
          }

          public UsuarioBE ObtenerParticipante(String correo, String password)
          {

              UsuarioDALC objParticipanteDALC = new UsuarioDALC();
              UsuarioBE objParticipanteBE = objParticipanteDALC.ObtenerParticipante(correo, password);
              return objParticipanteBE;

          }

          public int InsertarParticipante(UsuarioBE objParticipanteBE)
          {
              UsuarioDALC dalc = new UsuarioDALC();
              return dalc.InsertarParticipante(objParticipanteBE);
          }

          public int ValidarEmailUnico(string mail)
          {
              UsuarioDALC dalc = new UsuarioDALC();
              return dalc.ValidarEmailUnico(mail);
          }

          public UsuarioBE LoginMasivo(string mail, string password)
          {
              UsuarioDALC objUsuarioDALC = new UsuarioDALC();

              return objUsuarioDALC.LoginMasivo(mail, password);
          }
    }
}
