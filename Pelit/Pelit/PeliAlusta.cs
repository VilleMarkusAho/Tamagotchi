using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;

namespace Pelit
{
    [Serializable]
    class PeliAlusta
    {
        private bool[] ruudut = new bool[9];

        public bool OnkoOsuma(int ruutu)
        {
            ruutu--;

            if (ruudut[ruutu] == true)
            {
                return true;
            }
            
            else
            {
                return false;
            }
        }

        public void Siirto(int ruutu)
        {
            ruutu--;
            for (int i = 0; i < ruudut.Length; i++)
            {
                ruudut[i] = false;
            }
            ruudut[ruutu] = true;

        }

        public void Init()
        {


            for (int i = 0; i < ruudut.Length; i++)
            {
                ruudut[i] = false;
            }
         

        }




    }
}
