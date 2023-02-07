using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZI_Lab1_Enigma
{
    class CRC
    {
        public CRC() { }

        public static byte CRC_8(List<byte> listaBajtova)
        {

            const byte generator = 0x1D;
            byte crc = 0; 

            foreach (byte currByte in listaBajtova)
            {
                crc ^= currByte;

                for (int i = 0; i < 8; i++)
                {
                    if ((crc & 0x80) != 0)
                    {
                        crc = (byte)((crc << 1) ^ generator);
                    }
                    else
                    {
                        crc <<= 1;
                    }
                }
            }

            return crc;
        }

        public static byte CRC_8(String s)
        {
            const byte generator = 0x1D;
            byte crc = 0;
            List<byte> listaBajtova = new List<byte>();
            listaBajtova = Encoding.ASCII.GetBytes(s).ToList();
            foreach (byte currByte in listaBajtova)
            {
                crc ^= currByte;

                for (int i = 0; i < 8; i++)
                {
                    if ((crc & 0x80) != 0)
                    {
                        crc = (byte)((crc << 1) ^ generator);
                    }
                    else
                    {
                        crc <<= 1;
                    }
                }
            }

            return crc;
        }
    }
}

