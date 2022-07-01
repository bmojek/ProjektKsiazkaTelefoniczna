using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace ProjektKsiazkaTelefoniczna
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public List<Osoby> Osoby { get; set; }

        public MainWindow()
        {
            InitializeComponent();

            DataTelContext db = new DataTelContext();

            var q1 = from o in db.Osoby
                     orderby o.Imie ascending
                     select new
                     {
                         Imie = o.Imie,
                         Nazwisko = o.Nazwisko,
                         NrTelefonu = o.NrTel,
                         Email = o.Email,
                         Adres = o.Adres
                     };

            data1.ItemsSource = q1.ToList();
        }

        private bool visable = true;

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (visable == true)
            {
                data1.Visibility = Visibility.Hidden;
                formAdd.Visibility = Visibility.Visible;
                addContact.Content = "Kontakty";
                visable = false;
            }
            else
            {
                data1.Visibility = Visibility.Visible;
                formAdd.Visibility = Visibility.Hidden;
                addContact.Content = "Dodaj Kontakt";
                visable = true;
            }
        }
    }
}