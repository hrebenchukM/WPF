using System.ComponentModel;
using System.Xml.Linq;

namespace Hw6MVVM_I
{
    class Record : INotifyPropertyChanged
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
                OnPropertyChanged(new PropertyChangedEventArgs(nameof(Name)));
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
                OnPropertyChanged(new PropertyChangedEventArgs(nameof(Adress)));
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
                OnPropertyChanged(new PropertyChangedEventArgs(nameof(Phone)));
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public void OnPropertyChanged(PropertyChangedEventArgs e)
        {
            PropertyChanged?.Invoke(this, e);
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
