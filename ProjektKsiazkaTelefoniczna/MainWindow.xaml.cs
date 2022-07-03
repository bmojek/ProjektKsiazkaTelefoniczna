using Microsoft.Data.SqlClient;
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
        private DataTelContext db = new DataTelContext();

        private void LoadData()
        {
            var q1 = from o in db.Osoby
                     orderby o.Imie ascending
                     select new
                     {
                         o.Imie,
                         o.Nazwisko,
                         o.NrTel,
                         o.Email,
                         o.Adres
                     };

            data1.ItemsSource = q1.ToList();
        }

        public MainWindow()
        {
            InitializeComponent();
            LoadData();
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
            LoadData();
        }

        private void ClearForm()
        {
            imieAdd.Text = "";
            nazwiskoAdd.Text = "";
            nrtelAdd.Text = "";
            emailAdd.Text = "";
            adresAdd.Text = "";
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            try
            {
                Osoby nowaosoba = new Osoby()
                {
                    Imie = imieAdd.Text,
                    Nazwisko = nazwiskoAdd.Text,
                    NrTel = nrtelAdd.Text,
                    Email = emailAdd.Text,
                    Adres = adresAdd.Text
                };
                db.Osoby.Add(nowaosoba);
                db.SaveChanges();
                MessageBox.Show("Kontakt został dodany!");
                ClearForm();
            }
            catch
            {
                MessageBox.Show("Błędne dane");
            }
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            ClearForm();
        }
    }
}