using System;
using System.Collections.Generic;

namespace UPC.Seguridad.Talentos.SI.WS
{
    public static class Blindaje
    {
    public static Boolean InvalidString(string str)
            { 
                Boolean flag = false;

                int pos = 0;

                string resul = "";
                if (str != null)
                {
                    resul = str.ToUpper();

                    List<String> invalidCharacters = new List<String>();

                    invalidCharacters.Add("^");
                    invalidCharacters.Add("*");
                    invalidCharacters.Add("=");
                    invalidCharacters.Add("+");
                    invalidCharacters.Add("`");
                    invalidCharacters.Add("~");
                    invalidCharacters.Add(";");
                    invalidCharacters.Add(":");
                    invalidCharacters.Add(@"""");
                    invalidCharacters.Add("/");
                    invalidCharacters.Add(@"\");
                    invalidCharacters.Add("|");
                    invalidCharacters.Add("[");
                    invalidCharacters.Add("{");
                    invalidCharacters.Add("]");
                    invalidCharacters.Add("}");
                    invalidCharacters.Add("?");
                    invalidCharacters.Add("¿");
                    invalidCharacters.Add("¡");
                    invalidCharacters.Add("SELECT ");
                    invalidCharacters.Add("INSERT ");
                    invalidCharacters.Add("DELETE ");
                    invalidCharacters.Add("TABLE ");
                    invalidCharacters.Add("CREATE ");
                    invalidCharacters.Add("DROP ");
                    invalidCharacters.Add("UPDATE ");
                    invalidCharacters.Add("ALTER ");
                    invalidCharacters.Add("WFXSSProbe");
                    invalidCharacters.Add("wfxssprobe");
                    invalidCharacters.Add("exec ");
                    invalidCharacters.Add("EXEC ");
                    invalidCharacters.Add("alert(");
                    invalidCharacters.Add("ALERT(");
                    invalidCharacters.Add("alert (");
                    invalidCharacters.Add("ALERT (");
                    invalidCharacters.Add("AVAK$(RETURN_CODE)OS");
                    invalidCharacters.Add("avak$(return_code)os");
                    invalidCharacters.Add("avak$ (");
                    invalidCharacters.Add("AVAK$ (");
                    invalidCharacters.Add("RETURN_CODE");
                    invalidCharacters.Add("return_code");
                    invalidCharacters.Add("&&vol");
                    invalidCharacters.Add("$_REQUEST");
                    invalidCharacters.Add("$_request");
                    invalidCharacters.Add("$sql");
                    invalidCharacters.Add("$SQL");
                    invalidCharacters.Add("$POST");
                    invalidCharacters.Add("$post");
                    invalidCharacters.Add("require_once(");
                    invalidCharacters.Add("require_once (");

                    while ((!flag) && (pos < invalidCharacters.Count))
                    {
                        flag = resul.Contains(invalidCharacters[pos]);
                        pos++;
                    }
                }

                return flag;
            }

        }
}