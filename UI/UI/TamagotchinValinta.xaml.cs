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
using Tamagotchit;
using UI;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;

namespace Polttopallo
{
    /// <summary>
    /// Interaction logic for TamagotchinValinta.xaml
    /// </summary>
    public partial class TamagotchinValinta : Window
    {
        private Ilpo rex;
        private Ilpo peto;
        private Ilpo mato;

        public TamagotchinValinta()
        {
            Ilpo rex = new Ilpo("Rex");
            Ilpo peto = new Ilpo("Peto");
            Ilpo mato = new Ilpo("Mato");

            if (rex.Deserialize() != null) this.rex = rex.Deserialize();
            else this.rex = new Ilpo("Rex");
            if (peto.Deserialize() != null) this.peto = peto.Deserialize();
            else this.peto = new Ilpo("Peto");
            if (mato.Deserialize() != null) this.mato = mato.Deserialize();
            else this.mato = new Ilpo("Mato");

            InitializeComponent();

            


        }

        private void Tamagotchi1_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            MainWindow window = new MainWindow(rex, Tamagotchi1.Source);
            window.Owner = this;

            if (window.ShowDialog() == false)
            {
                Onnellisuus1.Content = $"Onnellisuus: {rex.onnellisuusIndeksi}";
                if (rex.kuollut == true)
                {
                    Tamagotchi1.Source = new BitmapImage(new Uri("pack://application:,,,/UI;component/kuva/tombstone.png"));
                    File.Delete(AppDomain.CurrentDomain.BaseDirectory + $"tallennus/{rex.nimi}Peli.dat");

                }
            }

        }

        private void Tamagotchi2_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            MainWindow window = new MainWindow(peto, Tamagotchi2.Source);
            window.Owner = this;

            if (window.ShowDialog() == false)
            {
                Onnellisuus2.Content = $"Onnellisuus: {peto.onnellisuusIndeksi}";

                if (peto.kuollut == true)
                {
                    Tamagotchi2.Source = new BitmapImage(new Uri("pack://application:,,,/UI;component/kuva/tombstone.png"));
                    File.Delete(AppDomain.CurrentDomain.BaseDirectory + $"tallennus/{peto.nimi}Peli.dat");
                }


            }


        }

        private void Tamagotchi3_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            MainWindow window = new MainWindow(mato, Tamagotchi3.Source);
            window.Owner = this;

            if (window.ShowDialog() == false)
            {
                Onnellisuus3.Content = $"Onnellisuus: {mato.onnellisuusIndeksi}";
                if (mato.kuollut == true)
                {
                    Tamagotchi3.Source = new BitmapImage(new Uri("pack://application:,,,/UI;component/kuva/tombstone.png"));
                    File.Delete(AppDomain.CurrentDomain.BaseDirectory + $"tallennus/{mato.nimi}Peli.dat");


                }
            }

        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            rex.Serialize();
            peto.Serialize();
            mato.Serialize();

        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            Nimi1.Content = rex.nimi;
            Nimi2.Content = peto.nimi;
            Nimi3.Content = mato.nimi;

            Onnellisuus1.Content = $"Onnellisuus: {rex.onnellisuusIndeksi}";
            Onnellisuus2.Content = $"Onnellisuus: {peto.onnellisuusIndeksi}";
            Onnellisuus3.Content = $"Onnellisuus: {mato.onnellisuusIndeksi}";
        }
    }
}