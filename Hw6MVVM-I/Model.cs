using System.ComponentModel;
using System.Xml.Linq;

namespace Hw6MVVM_I
{
    class Record 
    {

        private string name;
        private string adress;
        private string phone;

        public string Name
        {
            get
            {
                return name;
            }
            set
            {
                name = value;
                
            }
        }
        public string Adress
        {
            get
            {
                return adress;
            }
            set
            {
                adress = value;
             
            }
        }
        public string Phone
        {
            get
            {
                return phone;
            }
            set
            {
                phone = value;
             
            }
        }

      
        public Record(string n, string a, string p)
        {
            Name = n;
            Adress = a;
            Phone = p;
        }
        public Record()
        {
            Name = "";
            Adress = "";
            Phone = "";
        }
    }
}
