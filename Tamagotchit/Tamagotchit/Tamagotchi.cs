using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;

namespace Tamagotchit
{
    [Serializable]
    public abstract class Tamagotchi
    {

        public uint onnellisuusIndeksi = 30;
        public readonly string nimi;
        private int tahti;
        protected int syo3kertaa;
        public bool kuollut;

        protected Tamagotchi(string nimi)
        {
            this.nimi = nimi;
        }

        public int Tahti
        {
            get
            {
                return tahti;
            }

            set
            {
                tahti = value;
            }

        }
        /// <summary>
        /// Jos tahti on oikein ja on toiminnan aika 
        /// </summary>
        public virtual bool Toimi(uint kertoja)
        {
            bool tulos = false;
            if (kertoja % tahti == 0)
            {
                Syo();
                Nuku();
                Pelaa();
                tulos = true;
            }
            return tulos;

        }
        

        public virtual void Syo()
        {
            syo3kertaa += 1;

        }

        public virtual void Nuku()
        {
            syo3kertaa = 0;

        }

        public virtual void Pelaa()
        {
            syo3kertaa = 0;
        }

    }
}
