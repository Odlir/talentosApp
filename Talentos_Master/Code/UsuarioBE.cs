
namespace Talentos_Master
{
    public class UsuarioJson
    {
        public Usuario user = new Usuario();
    }

    public class Usuario
    {
        public int id;
        public string code;
        public string numberidentity;
        public string names;
        public string surname;
        public string lastname;
        public string sex;
        public string email;
        public Birthday birthday = new Birthday();
        public string email_information;
        public string company;
        public string headquarter;
        public string organizationalunit;
        public string charge;
        public string faculty;
        public string career;
        public string course;
        public string section;
    }

    public class Birthday
    {
        public string date;
        public string timezone_type;
        public string timezone;
    }
}
