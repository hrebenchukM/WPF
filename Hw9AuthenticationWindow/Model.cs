using System.ComponentModel;
using System.Xml.Linq;

namespace Hw9AuthenticationWindow
{
    class Account
    {

        private string login;
        private string password;
       

        public string Login
        {
            get
            {
                return login;
            }
            set
            {
                login = value;

            }
        }
        public string Password
        {
            get
            {
                return password;
            }
            set
            {
                password = value;

            }
        }
     


        public Account(string l, string p)
        {
            Login = l;
            Password = p;
        }
        public Account()
        {
            Login = "";
            Password = "";
        }
    }
}
