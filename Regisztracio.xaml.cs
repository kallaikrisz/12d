using MySql.Data.MySqlClient;
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
using System.Windows.Shapes;

namespace ugyfel
{
    /// <summary>
    /// Interaction logic for Regisztracio.xaml
    /// </summary>
    public partial class Regisztracio : Window
    {
        public Regisztracio()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            string nev = felhasznalonev.Text;
            string pass = jelszo.Password;
            string passujra = jelszoujra.Password;
            string emailcim = email.Text;

            if(string.IsNullOrEmpty(nev) || string.IsNullOrEmpty(pass))
            {
                MessageBox.Show("Töltsön ki minden mezőt!", "Hiányzó adat!" ,MessageBoxButton.OK,MessageBoxImage.Error);
                return;
            }
            if(pass!= passujra)
            {
                MessageBox.Show("Nem egyeznek a jelszavak!", "Jelszó hiba!", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            if(!emailcim.Contains('@')|| !emailcim.Contains('.'))
            {
                MessageBox.Show("Nem megfelelő e-mail formátum!", "E-mail hiba!", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            try 
            {
                using (MySqlConnection conn = new MySql.Data.MySqlClient.MySqlConnection(App.ConnString))
                {
                    conn.Open();
                    string sql = "select count(*) from felhasznalo where felhasznalo=@fnev or email=@emil";
                    var check=new MySql.Data.MySqlClient.MySqlCommand(sql, conn);
                    check.Parameters.AddWithValue("@fnev",nev);
                    check.Parameters.AddWithValue("@emil", emailcim);
                    long db=(long)check.ExecuteScalar();
                    if (db > 0)
                    {
                        MessageBox.Show("Már van ilyen felhasználó vagy e-mail cím!", "Hiba!", MessageBoxButton.OK, MessageBoxImage.Error);
                        return;
                    }
                }

                
            }
            catch (Exception ex)
            {
                MessageBox.Show("Hiba történt az adatbáziskapcsolat során:\n" + ex.Message, "Adatbázis hiba", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}
