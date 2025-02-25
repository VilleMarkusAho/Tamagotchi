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
using Tamagotchit;
using Polttopallo;
using System.Windows.Threading;
using System.Threading;
using System.IO;
using Microsoft.Win32;

namespace UI
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        Ilpo tamagotchi;
        DispatcherTimer timer2;
        DispatcherTimer timer;

        public MainWindow(Ilpo tamagotchi, ImageSource kuva)
        {
            InitializeComponent();

            this.tamagotchi = tamagotchi;

            Kuva.Source = kuva;

            Nimi.Content = tamagotchi.nimi;

            Onnellisuus.Content = $"Onnellisuus: {tamagotchi.onnellisuusIndeksi}";

            tamagotchi.Tahti = 2;

            this.timer = new DispatcherTimer();
            timer.Tick += new EventHandler(Timer_Tick);
            timer.Interval = new TimeSpan(0, 0, tamagotchi.Tahti);
            timer.Start();

            this.timer2 = new DispatcherTimer { Interval = TimeSpan.FromSeconds(3) };
            timer2.Tick += new EventHandler(Timer2_Tick);



        }

        private void Timer2_Tick(object sender, EventArgs e)
        {
            timer2.Stop();
            ToimintoKuva.Visibility = Visibility.Collapsed;

            Syo.IsEnabled = true;
            Nuku.IsEnabled = true;
            Pelaa.IsEnabled = true;
            Herata.IsEnabled = true;
        }

        private bool TarkistaOnnellisuus()
        {
            if (420000000 < tamagotchi.onnellisuusIndeksi || tamagotchi.onnellisuusIndeksi == 0)
            {

                timer.Stop();
                tamagotchi.onnellisuusIndeksi = 0;
                Onnellisuus.Content = "Onnellisuus: 0";
                Kuva.Source = new BitmapImage(new Uri("pack://application:,,,/UI;component/kuva/tombstone.png"));
                ToimintoKuva.Visibility = Visibility.Collapsed;

                Syo.IsEnabled = true;
                Nuku.IsEnabled = true;
                Pelaa.IsEnabled = true;
                Herata.IsEnabled = true;

                return tamagotchi.kuollut = true;

            }

            else return tamagotchi.kuollut = false;
        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            TarkistaOnnellisuus();

            if (tamagotchi.kyllaisyys != 0)
            {
                tamagotchi.kyllaisyys--;
            }
            if (tamagotchi.vasymys != 0)
            {
                tamagotchi.vasymys--;
            }
            

            if (tamagotchi.kyllaisyys == 0)
            {
                MessageBox.Show("On aika syödä");
            }

            if (tamagotchi.vasymys == 0)
            {
                MessageBox.Show("On aika Nukkua");
            }

            
            TarkistaOnnellisuus();

        }

        private void Syo_Click(object sender, RoutedEventArgs e)
        {
            
            TarkistaOnnellisuus();

            if (tamagotchi.kuollut == true)
            {
                return;
            }
            else
            {
                tamagotchi.Syo();
                Syo.IsEnabled = false;
                Nuku.IsEnabled = false;
                Pelaa.IsEnabled = false;
                Herata.IsEnabled = false;

                ToimintoKuva.Source = new BitmapImage(new Uri("pack://application:,,,/UI;component/kuva/eating.png")); 
                ToimintoKuva.Visibility = Visibility.Visible;
                timer2.Start();
                Onnellisuus.Content = $"Onnellisuus: {tamagotchi.onnellisuusIndeksi}";
                TarkistaOnnellisuus();

            }
           
        }

        private void Nuku_Click(object sender, RoutedEventArgs e)
        {
   
            TarkistaOnnellisuus();

            if (tamagotchi.kuollut == true)
            {
                return;
            }
            else
            {
                tamagotchi.Nuku();
                ToimintoKuva.Source = new BitmapImage(new Uri("pack://application:,,,/UI;component/kuva/sleeping.png"));
                ToimintoKuva.Visibility = Visibility.Visible;

                Onnellisuus.Content = $"Onnellisuus: {tamagotchi.onnellisuusIndeksi}";
                Syo.IsEnabled = false;
                Nuku.IsEnabled = false;
                Pelaa.IsEnabled = false;
                TarkistaOnnellisuus();
            }
        }

        private void Herata_Click(object sender, RoutedEventArgs e)
        {
            
            TarkistaOnnellisuus();

            if (tamagotchi.kuollut == true)
            {
                return;
            }
            else
            {
                tamagotchi.Herata();
                Syo.IsEnabled = true;
                Nuku.IsEnabled = true;
                Pelaa.IsEnabled = true;

                TarkistaOnnellisuus();
                ToimintoKuva.Visibility = Visibility.Collapsed;
            }

        }

        private void Pelaa_Click(object sender, RoutedEventArgs e)
        {
            timer.Stop();
            TarkistaOnnellisuus();

            if (tamagotchi.kuollut == true)
            {
                return;
            }
            else
            {
                
                PolttoPallo polttoPallo = new PolttoPallo(tamagotchi, Kuva.Source);
                

                polttoPallo.Owner = this;

                if (polttoPallo.ShowDialog() == false)
                {
                    Onnellisuus.Content = $"Onnellisuus: {tamagotchi.onnellisuusIndeksi}";
                    timer.Start();
                    TarkistaOnnellisuus();
                    if (polttoPallo.peli.kaynnissa == true)
                    {
                        polttoPallo.peli.Serialization(tamagotchi.nimi);

                    }
                    else
                    {
                        File.Delete(AppDomain.CurrentDomain.BaseDirectory + $"tallennus/{tamagotchi.nimi}Peli.dat");
                    }
                }
                
            }
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            timer.Stop();
        }

    }
}
