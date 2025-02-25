using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;

namespace Tamagotchit
{
    [Serializable]
    public class Ilpo : Tamagotchi
    {
        public int vasymys = 30;
        public int kyllaisyys = 10;
        private bool sairaus;
        public string tunnetila;
        public bool nukkua;

        public Ilpo(string nimi) : base(nimi)
        {

        }


        public void Herata()
        {
            syo3kertaa = 0;
            vasymys = 30;
            nukkua = false;
        }

        public void Laake()
        {
            sairaus = false;
            syo3kertaa = 0;
        }

        public void Herkku()
        {
            syo3kertaa += 1;
        }

        public override void Syo()
        {
            base.Syo();

            if (syo3kertaa == 3)
            {
                onnellisuusIndeksi -= 10;
                kyllaisyys = 10;
                syo3kertaa = 0;

            }
            else
            {
                onnellisuusIndeksi += 3;
                kyllaisyys = 10;
            }

        }

        public override void Nuku()
        {
            base.Nuku();
            onnellisuusIndeksi -= 10;
            nukkua = true;
        }

        ~Ilpo()
        {

        }

        public override string ToString()
        {

            return $"{base.GetType().Name}:{base.nimi}, reagointitahti {base.Tahti}, onnellisuusindeksi {base.onnellisuusIndeksi}";
        }


        public void Serialize()
        {
            string path = AppDomain.CurrentDomain.BaseDirectory + $"tallennus/{this.nimi}.dat";

            if (this.kuollut == false)
            {

                FileStream fs = File.OpenWrite(path);
                BinaryFormatter bf = new BinaryFormatter();
                bf.Serialize(fs, this);
                fs.Flush();
                fs.Close();

            }
            else
            {
                File.Delete(path);
            }
        }

        public Ilpo Deserialize()
        {
            try
            {
                string path = AppDomain.CurrentDomain.BaseDirectory + $"tallennus/{this.nimi}.dat";
                FileStream fs = File.OpenRead(path);
                BinaryFormatter bf = new BinaryFormatter();
                Ilpo tamagotchi = (Ilpo)bf.Deserialize(fs);
                fs.Close();
                return tamagotchi;
            }
            catch (FileNotFoundException)
            {

                return null;

            }

        }

    }


}


