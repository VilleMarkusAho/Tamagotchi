using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;



namespace Pelit
{
    [Serializable]
    public class Peli
    {
        private PeliAlusta alusta;
        private uint pisteet;
        private ushort hutit;
        private Random random;
        public bool kaynnissa;
     
        public Peli()
        {
            this.alusta = new PeliAlusta();
            this.random = new Random();

        }
        public bool OnkoOsuma(int ruutu)
        {

            if (alusta.OnkoOsuma(ruutu) == true)
            {
                pisteet++;
                return true;
            }
            else
            {
                hutit++;
                return false;
            }

        }

        public uint Pisteet => pisteet;

        public ushort Hutit => hutit;

        public bool OnkoValmis()
        {
            if (hutit == 3)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public int Siirto()
        {
            int ruutu = random.Next(1, 10);
            alusta.Siirto(ruutu);
            return ruutu;
        }       

        public void Init()
        {
            pisteet = 0;
            hutit = 0;
            alusta.Init();
            Siirto();

        }

        public void Serialization(string tamagotchinNimi)
        {


            string path = AppDomain.CurrentDomain.BaseDirectory + $"tallennus/{tamagotchinNimi}Peli.dat";

            FileStream fs = File.OpenWrite(path);
            BinaryFormatter bf = new BinaryFormatter();
            bf.Serialize(fs, this);
            fs.Flush();
            fs.Close();
            
            

        }


        public Peli Deserialization(string tamagotchinNimi)
        {

            try
            {
                string path = AppDomain.CurrentDomain.BaseDirectory + $"tallennus/{tamagotchinNimi}Peli.dat";
                FileStream fs = File.OpenRead(path);
                BinaryFormatter bf = new BinaryFormatter();
                Peli tilanne = (Peli)bf.Deserialize(fs);
                fs.Close();
                return tilanne;
            }
            catch (FileNotFoundException)
            {

                return null;

            }

        }





    }
}
