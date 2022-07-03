using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
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
    public partial class MainWindow : Window
    {
        public List<Osoby> Osoby { get; set; }
        private DataTelContext db = new DataTelContext();

        private void LoadData()
        {
            Button button = new Button();
            var q1 = from o in db.Osoby
                     orderby o.Imie ascending
                     select new
                     {
                         o.Imie,
                         o.Nazwisko,
                         o.NrTel,
                         o.Email,
                         o.Adres,
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
                sortBtn.Visibility = Visibility.Hidden;
                addContact.Content = "Kontakty";
                visable = false;
            }
            else
            {
                data1.Visibility = Visibility.Visible;
                formAdd.Visibility = Visibility.Hidden;
                sortBtn.Visibility = Visibility.Visible;
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
            Regex regex = new Regex(@"^[\w-\.]+@([\w-]+\.)+[\w-]{2,4}$");
            Regex regex1 = new Regex(@"^([0-9]+(-[0-9]+)+)$");
            if (!regex.IsMatch(emailAdd.Text))
            {
                errorMessage.Content = "Błędne dane"; emailAdd.BorderBrush = Brushes.Red;
                if (!regex1.IsMatch(nrtelAdd.Text))
                {
                    errorMessage.Content = "Błędne dane";
                    nrtelAdd.BorderBrush = Brushes.Red;
                }
            }
            else
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
                errorMessage.Content = null;
                emailAdd.BorderBrush = null;
                nrtelAdd.BorderBrush = null;
            }
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            ClearForm();
        }

        private void Button_Click_3(object sender, RoutedEventArgs e)
        {
            var q1 = from o in db.Osoby
                     orderby o.OsobyId descending
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
    }
}