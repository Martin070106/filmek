using MySql.Data.MySqlClient;
using MySqlX.XDevAPI.Relational;
using Org.BouncyCastle.Ocsp;
using Org.BouncyCastle.Pqc.Crypto.Lms;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
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
using System.Xml.Linq;

namespace Filmek
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        MySqlConnection kapcs = new MySqlConnection("server = srv1.tarhely.pro;database = v2labgwj_12a; uid =v2labgwj_12a; password = 'HASnEeKvbDEPGgvTZubG';");
        List<Film> filmek = new List<Film>();
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                kapcs.Open();
                var parancs = new MySqlCommand("SELECT * FROM kameczm_filmek", kapcs).ExecuteReader();
                while (parancs.Read())
                {
                    var film = new Film(parancs["filmazon"].ToString(), parancs["cim"].ToString(), Convert.ToInt32(parancs["ev"]), parancs["szines"].ToString(), parancs["mufaj"].ToString(), Convert.ToInt32(parancs["hossz"]));
                    filmek.Add(film);
                }
                kapcs.Close();
                dgFilm.ItemsSource = filmek;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Hiba történt: " + ex.Message);
            }
        }

        private void dgFilm_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (dgFilm.SelectedItem is Film selectedFilm)
            {
                lbFilmAzon.Content = selectedFilm.filmazon;
                tb1.Text = selectedFilm.cim;
                tb2.Text = selectedFilm.ev.ToString();
                tb3.Text = selectedFilm.szines;
                tb4.Text = selectedFilm.mufaj;
                tb5.Text = selectedFilm.hossz.ToString();
            }
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            foreach (var film in filmek)
            {
                if (film.filmazon == lbFilmAzon.Content.ToString())
                {
                    film.cim = tb1.Text;
                    film.ev = int.Parse(tb2.Text);
                    film.szines = tb3.Text;
                    film.mufaj = tb4.Text;
                    film.hossz = int.Parse(tb5.Text);
                    break;
                }
            }
            kapcs.Open();
            new MySqlCommand($"UPDATE kameczm_filmek SET cim='{tb1.Text}', ev={tb2.Text}, szines='{tb3.Text}', mufaj='{tb4.Text}', hossz={tb5.Text} WHERE filmazon='{lbFilmAzon.Content}'",kapcs).ExecuteNonQuery();
            kapcs.Close();

            dgFilm.Items.Refresh();
        }
    }
}
