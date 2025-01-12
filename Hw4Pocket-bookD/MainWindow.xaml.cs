using System.Collections.ObjectModel;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Hw4Pocket_bookD
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            ProgrammingLanguages programminglanguages = Resources["programminglanguages"] as ProgrammingLanguages;
            Language lang = new Language();
            lang.Name = programminglanguages.LanguageName;
            lang.Author = programminglanguages.LanguageAuthor;
            lang.Year = programminglanguages.LanguageYear;
            programminglanguages.Languages.Add(lang);
        }

        private void listBox1_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            try
            {
                ProgrammingLanguages programminglanguages = Resources["programminglanguages"] as ProgrammingLanguages;
                if (programminglanguages.SelectedIndex == -1)
                    return;
                Language lang = programminglanguages.Languages[programminglanguages.SelectedIndex];
                programminglanguages.LanguageName = lang.Name;
                programminglanguages.LanguageAuthor = lang.Author;
                programminglanguages.LanguageYear = lang.Year;
            }
            catch { }
        }

        private void langName_GotFocus(object sender, RoutedEventArgs e)
        {
            TextBox textbox = sender as TextBox;
            textbox.Text = string.Empty;
        }
    }

    public class Language : DependencyObject
    {
        private static readonly DependencyProperty NameProperty;
        private static readonly DependencyProperty AuthorProperty;
        private static readonly DependencyProperty YearProperty;

        static Language()
        {
            NameProperty = DependencyProperty.Register(nameof(Name), typeof(string), typeof(Language));
            AuthorProperty = DependencyProperty.Register(nameof(Author), typeof(string), typeof(Language));
            YearProperty = DependencyProperty.Register(nameof(Year), typeof(int), typeof(Language));
        }

        public string Name
        {
            get { return (string)GetValue(NameProperty); }
            set { SetValue(NameProperty, value); }
        }
        public string Author
        {
            get { return (string)GetValue(AuthorProperty); }
            set { SetValue(AuthorProperty, value); }
        }
        public int Year
        {
            get { return (int)GetValue(YearProperty); }
            set { SetValue(YearProperty, value); }
        }
    }

    public class ProgrammingLanguages : DependencyObject
    {
        public ObservableCollection<Language> Languages { get; set; } = new ObservableCollection<Language>();


        private static readonly DependencyProperty SelectedIndexProperty;
        private static readonly DependencyProperty NameProperty;
        private static readonly DependencyProperty AuthorProperty;
        private static readonly DependencyProperty YearProperty;

        static ProgrammingLanguages()
        {
            SelectedIndexProperty = DependencyProperty.Register(nameof(SelectedIndex), typeof(int), typeof(ProgrammingLanguages));
            NameProperty = DependencyProperty.Register(nameof(LanguageName), typeof(string), typeof(ProgrammingLanguages));
            AuthorProperty = DependencyProperty.Register(nameof(LanguageAuthor), typeof(string), typeof(ProgrammingLanguages));
            YearProperty = DependencyProperty.Register(nameof(LanguageYear), typeof(int), typeof(ProgrammingLanguages));
        }

        public int SelectedIndex
        {
            get { return (int)GetValue(SelectedIndexProperty); }
            set { SetValue(SelectedIndexProperty, value); }
        }

        public string LanguageName
        {
            get { return (string)GetValue(NameProperty); }
            set { SetValue(NameProperty, value); }
        }
        public string LanguageAuthor
        {
            get { return (string)GetValue(AuthorProperty); }
            set { SetValue(AuthorProperty, value); }
        }
        public int LanguageYear
        {
            get { return (int)GetValue(YearProperty); }
            set { SetValue(YearProperty, value); }
        }
    }
}