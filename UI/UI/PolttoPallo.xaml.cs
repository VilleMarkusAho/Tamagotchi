using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using Pelit;
using Tamagotchit;
using System.Windows.Threading;


namespace Polttopallo
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>

    
    public partial class PolttoPallo : Window
    {
        public Peli peli;
        private ImageSource kuva;
        private Image[] koordinaatisto;
        private DispatcherTimer timer;
        private Ilpo tamagotchi;
        private int tahti = 8;
        private int sijainti;

        public PolttoPallo(Ilpo tamagotchi, ImageSource kuva)
        {

            InitializeComponent();

            this.peli = new Peli();
            this.tamagotchi = tamagotchi;

            this.kuva = kuva;
            this.tamagotchi = tamagotchi;

            this.koordinaatisto = new Image[] { K1, K2, K3, K4, K5, K6, K7, K8, K9 };

            for (int i = 0; i < 9; i++)
            {
                koordinaatisto[i].Source = kuva;
            }

            this.timer = new DispatcherTimer();
            timer.Tick += new EventHandler(Timer_Tick);
            timer.Interval = new TimeSpan(0, 0, 8);
            Syote.IsEnabled = false;
            Button.IsEnabled = false;

        }



        private void OnkoOsuma(object sender, RoutedEventArgs e)
        {
            bool osuma = false;
            int.TryParse(Syote.Text, out int ruutu);

            if (ruutu != 0)
            {
                osuma = peli.OnkoOsuma(ruutu);
            }

            Pisteet.Content = peli.Pisteet;
            Hudit.Content = peli.Hutit;

            PeliOhi();

            if (osuma == true)
            {
                
                if (peli.Pisteet % 10 == 0)
                {
                    tamagotchi.onnellisuusIndeksi += 1;

                    if (tahti != 2)
                    {
                        tahti -= 2;
                        timer.Interval = new TimeSpan(0, 0, tahti);
                    }
             
                }

                timer.Stop();
                timer.Start();
                Timer_Tick(sender, e);

            }
            
        }

        private void PeliOhi()
        {

            if (peli.OnkoValmis() == true)
            {
                timer.Stop();
                peli.kaynnissa = false;

                for (int i = 0; i < 9; i++)
                {
                    koordinaatisto[i].Visibility = Visibility.Collapsed;
                }

                MessageBox.Show("Peli on päättynyt");
                peli.Init();

                Pisteet.Content = peli.Pisteet;
                Hudit.Content = peli.Hutit;

                tamagotchi.onnellisuusIndeksi -= 10;
                this.DialogResult = false;
                
            }
            
        }

        private void TamagotchinLiikehdinta(int ruutu)
        {

            for (int i = 0; i < 9; i++)
            {
                koordinaatisto[i].Visibility = Visibility.Collapsed;
            }

            koordinaatisto[ruutu-1].Visibility = Visibility.Visible;
            
        }


        private void Button_Click(object sender, RoutedEventArgs e)
        {
            OnkoOsuma(sender, e);
        }

        private void Syote_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                Button_Click(sender, e);
                Syote.Text = "";
            }
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            Peli peli = new Peli();
            if (peli.Deserialization(tamagotchi.nimi) != null) peli = peli.Deserialization(tamagotchi.nimi);
            this.peli = peli;

            if (peli.kaynnissa == true)
            {
                Timer_Tick(sender, e);
            
            }
            
            Pisteet.Content = peli.Pisteet;
            Hudit.Content = peli.Hutit;

            timer.Start();

            Syote.IsEnabled = true;
            Button.IsEnabled = true;
            Aloita.IsEnabled = false;

            peli.kaynnissa = true;
        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            sijainti = peli.Siirto();
            TamagotchinLiikehdinta(sijainti);
        
            CommandManager.InvalidateRequerySuggested();
        }
    }
}
