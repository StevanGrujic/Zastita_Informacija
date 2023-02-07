using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZI_Lab2
{
    class XTEA
    {
        public XTEA()
        {
        }
        public static void Encipher(ref UInt32[] v, UInt32[] key, uint num_rounds = 64)
        {
            uint i;
            UInt32 v0 = v[0], v1 = v[1], sum = 0, delta = 0x9E3779B9;
            for (i = 0; i < num_rounds; i++)
            {
                v0 += (((v1 << 4) ^ (v1 >> 5)) + v1) ^ (sum + key[sum & 3]);
                sum += delta;
                v1 += (((v0 << 4) ^ (v0 >> 5)) + v0) ^ (sum + key[(sum >> 11) & 3]);
            }
            v[0] = v0;
            v[1] = v1;
        }

        public static void Decipher(ref UInt32[] v, UInt32[] key, uint num_rounds = 64)
        {
            uint i;
            UInt32 v0 = v[0], v1 = v[1], delta = 0x9E3779B9, sum = delta * num_rounds;
            for (i = 0; i < num_rounds; i++)
            {
                v1 -= (((v0 << 4) ^ (v0 >> 5)) + v0) ^ (sum + key[(sum >> 11) & 3]);
                sum -= delta;
                v0 -= (((v1 << 4) ^ (v1 >> 5)) + v1) ^ (sum + key[sum & 3]);
            }
            v[0] = v0;
            v[1] = v1;
        }
    }
}
