using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using ZI_Lab1_Enigma;

namespace ZI_Lab2
{
    public partial class Form1 : Form
    {
        #region Attributes
        Enigma enigma;
        string key = "";
        string key2 = "";
        string keyFile;
        string keyFile2;
        string initVectorFile1;
        string initVectorFile2;
        string crcFile;
        #endregion

        #region Constructors
        public Form1()
        {
            InitializeComponent();

            keyFile = System.IO.Path.Combine(System.AppDomain.CurrentDomain.BaseDirectory, "..\\..\\Keys.txt");
            keyFile2 = System.IO.Path.Combine(System.AppDomain.CurrentDomain.BaseDirectory, "..\\..\\KeysXtea.txt");
            initVectorFile1 = System.IO.Path.Combine(System.AppDomain.CurrentDomain.BaseDirectory, "..\\..\\InitVectorsXtea.txt");
            initVectorFile2 = System.IO.Path.Combine(System.AppDomain.CurrentDomain.BaseDirectory, "..\\..\\InitVectorsEnigma.txt");
            crcFile = System.IO.Path.Combine(System.AppDomain.CurrentDomain.BaseDirectory, "..\\..\\Crc.txt");


            fsw.Path = System.IO.Path.Combine(System.AppDomain.CurrentDomain.BaseDirectory, "..\\..\\PlainTextFiles");
            txbSourceFile.Enabled = false;
            btnSourceFileDialog.Enabled = false;
            btnEncrypt.Enabled = false;
            btnDecrypt.Enabled = false;
            txbKey.Enabled = false;
            txbIV.Enabled = false;
            cmbIzbor.SelectedIndex = 0;
            this.enigma = new Enigma(1, 2, 3, 'B', "AAZ", "AAA",null);
        }
        #endregion

        #region EventHandlers
        private void btnEnable_Click(object sender, EventArgs e)
        {
            fsw.EnableRaisingEvents = true;
            txbSourceFile.Enabled = false;
            btnSourceFileDialog.Enabled = false;
            btnEncrypt.Enabled = false;
            btnDecrypt.Enabled = false;

        }

        private void btnDisable_Click(object sender, EventArgs e)
        {
            fsw.EnableRaisingEvents = false;
            txbSourceFile.Enabled = true;
            btnSourceFileDialog.Enabled = true;
            btnEncrypt.Enabled = true;
            btnDecrypt.Enabled = true;

        }

        private void btnTargetFileDialog_Click(object sender, EventArgs e)
        {
            if (ofdTargetFile.ShowDialog() == DialogResult.OK)
            {
                txbTargetFile.Text = ofdTargetFile.FileName;
            }

        }

        private void btnEncrypt_Click(object sender, EventArgs e)
        {
            if (cmbIzbor.SelectedIndex == 0) //enigma
            {
                if (txbSourceFile.Text == "")
                {
                    MessageBox.Show("Source file not selected!");
                    return;
                }

                if (!ckbIV.Checked)
                {

                    using (StreamReader sr = new StreamReader(txbSourceFile.Text))
                    {
                        string s = sr.ReadToEnd();
                        sr.Close();
                        rtbPlain.Text = s;
                        s.ToUpper();
                        if (txbTargetFile.Text == "")
                        {
                            MessageBox.Show("Target file not selected!");
                            return;
                        }
                        using (StreamWriter sw = new StreamWriter(txbTargetFile.Text))
                        {
                            string cipher;
                            char[] pom = new char[s.Length];
                            pom = s.ToCharArray();
                            for (int i = 0; i < s.Length; i++)
                            {
                                if (s[i] < 65 || (s[i] > 90 && s[i] < 97) || s[i] > 122)
                                {
                                    pom[i] = ' ';

                                    continue;
                                }
                            }
                            s = new string(pom);
                            s = s.ToUpper();

                            byte crc = CRC.CRC_8(s);
                            cipher = enigma.Encrypt(s);
                            sw.Write(cipher);
                            sw.WriteLine();
                            sw.Write(crc);
                            sw.Close();
                            MessageBox.Show("File successfuly encrypted!");
                        }
                        string sss = File.ReadAllText(initVectorFile2);
                        string[] arrLinee = File.ReadAllLines(initVectorFile2);
                        if (sss.Contains("Target file: " + txbTargetFile.Text))
                        {
                            for (int i = 0; i < arrLinee.Length; i++)
                            {
                                if (arrLinee[i].Contains("Target file: " + txbTargetFile.Text))
                                {
                                    arrLinee[i] = "";
                                    arrLinee[i + 1] = "";
                                    break;
                                }
                            }
                            File.WriteAllLines(initVectorFile2, arrLinee);
                        }
                        string ss = File.ReadAllText(keyFile);
                        string[] arrLine = File.ReadAllLines(keyFile);
                        if (ss.Contains("Target file: " + txbTargetFile.Text))
                        {
                            for (int i = 0; i < arrLine.Length; i++)
                            {
                                if (arrLine[i].Contains("Target file: " + txbTargetFile.Text))
                                {
                                    if (key == "")
                                    {
                                        arrLine[i + 1] = "1 2 3 B AAZ AAA ";
                                        break;
                                    }
                                    else
                                    {
                                        arrLine[i + 1] = key;
                                        break;
                                    }

                                }
                            }
                            File.WriteAllLines(keyFile, arrLine);
                            MessageBox.Show("File successfuly encrypted!");
                        }
                        else
                        {
                            using (StreamWriter sw2 = new StreamWriter(keyFile, true))
                            {
                                if (key == "")
                                {
                                    key = "1 2 3 B AAZ AAA ";
                                    sw2.WriteLine("Target file: " + txbTargetFile.Text);
                                    sw2.WriteLine(key);
                                    sw2.WriteLine();
                                    key = "";
                                }
                                else
                                {
                                    sw2.WriteLine("Target file: " + txbTargetFile.Text);
                                    sw2.WriteLine(key);
                                    sw2.WriteLine();
                                    key = "";
                                }
                                sw2.Close();
                            }

                        }

                    }
                }

                else
                {
                    string str = File.ReadAllText(keyFile);
                    string[] arrLine1 = File.ReadAllLines(keyFile);
                    if (str.Contains("Target file: " + txbTargetFile.Text))
                    {
                        for (int i = 0; i < arrLine1.Length; i++)
                        {
                            if (arrLine1[i].Contains("Target file: " + txbTargetFile.Text))
                            {
                                if (key == "")
                                {
                                    arrLine1[i + 1] = "1 2 3 B AAZ AAA ";
                                    break;
                                }
                                else
                                {
                                    arrLine1[i + 1] = key;
                                    break;
                                }

                            }
                        }
                        File.WriteAllLines(keyFile, arrLine1);
                    }
                    using (StreamReader sr = new StreamReader(txbSourceFile.Text))
                    {
                        string s = sr.ReadToEnd();
                        sr.Close();
                        rtbPlain.Text = s;
                        s.ToUpper();
                        if (txbTargetFile.Text == "")
                        {
                            MessageBox.Show("Target file not selected!");
                            return;
                        }
                        string sss = File.ReadAllText(initVectorFile2);
                        string[] arrLinee = File.ReadAllLines(initVectorFile2);
                        if (sss.Contains("Target file: " + txbTargetFile.Text))
                        {
                            for (int i = 0; i < arrLinee.Length; i++)
                            {
                                if (arrLinee[i].Contains("Target file: " + txbTargetFile.Text))
                                {
                                    arrLinee[i + 1] = txbIV.Text;
                                    break;
                                }
                            }
                            File.WriteAllLines(initVectorFile2, arrLinee);
                        }
                        else
                        {
                            using (StreamWriter sw = new StreamWriter(initVectorFile2, true))
                            {
                                {
                                    sw.WriteLine("Target file: " + txbTargetFile.Text);
                                    sw.WriteLine(txbIV.Text);
                                    sw.WriteLine();
                                }
                                sw.Close();
                            }
                        }
                        using (StreamWriter sw = new StreamWriter(txbTargetFile.Text))
                        {
                            string cipher;
                            char[] pom = new char[s.Length + 8];
                            pom = s.ToCharArray();
                            for (int i = 0; i < s.Length; i++)
                            {
                                if (s[i] < 65 || (s[i] > 90 && s[i] < 97) || s[i] > 122)
                                {
                                    pom[i] = s[i];

                                    continue;
                                }
                            }
                            s = new string(pom);
                            s = s.ToUpper();

                            pom = s.ToCharArray();
                            List<char> pomocna = new List<char>();
                            pomocna = pom.ToList();
                            while (pomocna.Count % 8 != 0)
                            {
                                pomocna.Add(' ');
                            }
                            pom = new char[pomocna.Count];
                            pom = pomocna.ToArray();

                            byte crc = CRC.CRC_8(s);

                            char[] initVector = new char[16];
                            initVector = txbIV.Text.ToCharArray();
                            List<char> initVectorList = new List<char>();
                            initVectorList = initVector.ToList();
                            int n = initVector.Length;
                            if (n == 0) //ako nije unet nikakav IV
                            {
                                for (int i = 0; i < 8; i++)
                                {
                                    initVectorList.Add('\0');
                                    n++;
                                }
                            }
                            else
                            {
                                int ind = 0;
                                while (n < 8)
                                {
                                    initVectorList.Add(initVector[ind++]);
                                    n++;
                                }
                            }

                            initVector = initVectorList.ToArray();

                            char[] xorOperand = new char[8];
                            char[] zaEnkripciju;
                            //ovde podeliti ulazni string na blokove 
                            for (int i = 0; i < pom.Length; i += 8)
                            {
                                if(i == 0)
                                {
                                    for(int j=0; j<8; j++)
                                    {
                                        xorOperand[j] = initVector[j];
                                    }
                                }
                                List<char> lista = new List<char>();
                                lista.Add(pom[i]);
                                lista.Add(pom[i + 1]);
                                lista.Add(pom[i + 2]);
                                lista.Add(pom[i + 3]);
                                lista.Add(pom[i + 4]);
                                lista.Add(pom[i + 5]);
                                lista.Add(pom[i + 6]);
                                lista.Add(pom[i + 7]);

                                char[] p = lista.ToArray();
                                string pp = string.Join("", p);
                                pp.ToUpper();
                                p = pp.ToCharArray();
                                lista = p.ToList();
                                XOR(ref lista, xorOperand);

                                zaEnkripciju = lista.ToArray();

                                cipher = enigma.Encrypt(string.Join("",zaEnkripciju));
                                sw.Write(cipher);

                            }
                            sw.WriteLine();
                            sw.Write(crc);
                            sw.Close();
                            MessageBox.Show("File successfuly encrypted!");
                        }
                        string ss = File.ReadAllText(keyFile);
                        string[] arrLine = File.ReadAllLines(keyFile);
                        if (ss.Contains("Target file: " + txbTargetFile.Text))
                        {
                            for (int i = 0; i < arrLine.Length; i++)
                            {
                                if (arrLine[i].Contains("Target file: " + txbTargetFile.Text))
                                {
                                    if (key == "")
                                    {
                                        arrLine[i + 1] = "1 2 3 B AAZ AAA ";
                                        break;
                                    }
                                    else
                                    {
                                        arrLine[i + 1] = key;
                                        break;
                                    }

                                }
                            }
                            File.WriteAllLines(keyFile, arrLine);
                            MessageBox.Show("File successfuly encrypted!");
                        }
                        else
                        {
                            using (StreamWriter sw2 = new StreamWriter(keyFile, true))
                            {
                                if (key == "")
                                {
                                    key = "1 2 3 B AAZ AAA ";
                                    sw2.WriteLine("Target file: " + txbTargetFile.Text);
                                    sw2.WriteLine(key);
                                    sw2.WriteLine();
                                    key = "";
                                }
                                else
                                {
                                    sw2.WriteLine("Target file: " + txbTargetFile.Text);
                                    sw2.WriteLine(key);
                                    sw2.WriteLine();
                                    key = "";
                                }
                                sw2.Close();
                            }

                        }


                    }
                }
            }
            else if (cmbIzbor.SelectedIndex == 1)//xtea
            {
                if (txbSourceFile.Text == "")
                {
                    MessageBox.Show("Source file not selected!");
                    return;
                }
                if (txbKey.Text == "")
                {
                    MessageBox.Show("You must enter a key");
                    return;
                }
                string ss = File.ReadAllText(keyFile2);
                string[] arrLine = File.ReadAllLines(keyFile2);
                if (ss.Contains("Target file: " + txbTargetFile.Text))
                {
                    for (int i = 0; i < arrLine.Length; i++)
                    {
                        if (arrLine[i].Contains("Target file: " + txbTargetFile.Text))
                        {
                            arrLine[i + 1] = txbKey.Text;
                            break;
                        }
                    }
                    File.WriteAllLines(keyFile2, arrLine);
                    MessageBox.Show("File successfuly encrypted!");
                }
                else
                {
                    using (StreamWriter sw = new StreamWriter(keyFile2, true))
                    {
                        {
                            sw.WriteLine("Target file: " + txbTargetFile.Text);
                            sw.WriteLine(txbKey.Text);
                            sw.WriteLine();
                            key2 = "";
                            MessageBox.Show("File successfuly encrypted!");
                        }
                        sw.Close();
                    }
                }
                //Ubacivanje inicijalizacionog vektora
                if(ckbIV.Checked)
                {
                    ss = File.ReadAllText(initVectorFile1);
                    arrLine = File.ReadAllLines(initVectorFile1);
                    if (ss.Contains("Target file: " + txbTargetFile.Text))
                    {
                        for (int i = 0; i < arrLine.Length; i++)
                        {
                            if (arrLine[i].Contains("Target file: " + txbTargetFile.Text))
                            {
                                arrLine[i + 1] = txbIV.Text;
                                break;
                            }
                        }
                        File.WriteAllLines(initVectorFile1, arrLine);
                        MessageBox.Show("File successfuly encrypted!");
                    }
                    else
                    {
                        using (StreamWriter sw = new StreamWriter(initVectorFile1, true))
                        {
                            {
                                sw.WriteLine("Target file: " + txbTargetFile.Text);
                                sw.WriteLine(txbIV.Text);
                                sw.WriteLine();
                            }
                            sw.Close();
                        }
                    }
                }
                
                List<byte> listaBajtova = Encoding.ASCII.GetBytes(File.ReadAllText(txbSourceFile.Text)).ToList();

                rtbPlain.Text = File.ReadAllText(txbSourceFile.Text);
                while (listaBajtova.Count % 8 != 0)
                {
                    listaBajtova.Add((byte)'.');
                }

                byte crc = CRC.CRC_8(listaBajtova); //racunanje crc

                byte[] nizBajtova = new byte[listaBajtova.Count];
                nizBajtova = listaBajtova.ToArray();

                UInt32[] blokPodataka = new UInt32[2];
                byte[] kljuc = new byte[16];
                kljuc = Encoding.ASCII.GetBytes(txbKey.Text);
                List<byte> kljucList = new List<byte>();
                kljucList = kljuc.ToList();
                int k = kljuc.Length;
                while (k < 16)
                {
                    kljucList.Add(0);
                    k++;
                }
                kljucList.Reverse();
                kljuc = kljucList.ToArray();
                int s = 0;
                for (int i = 0; i < kljuc.Length; i++)
                {
                    if (s == kljuc.Length - i)
                        break;
                    if (kljuc[i] != 0)
                    {
                        byte a = kljuc[i];
                        kljuc[i] = kljuc[kljuc.Length - s - 1];
                        kljuc[kljuc.Length - s - 1] = a;
                        s++;
                    }
                }
                List<UInt32> deloviKljuceva = new List<UInt32>();
                for (int i = 0; i < 16; i += 4)
                {
                    List<byte> pom1 = new List<byte>();
                    pom1.Add(kljuc[i]);
                    pom1.Add(kljuc[i + 1]);
                    pom1.Add(kljuc[i + 2]);
                    pom1.Add(kljuc[i + 3]);
                    pom1.Reverse();
                    deloviKljuceva.Add(BitConverter.ToUInt32(pom1.ToArray(), 0));
                }
                List<byte> nizBajtovaZaUpis = new List<byte>();
                if (!ckbIV.Checked)
                {
                    string sss = File.ReadAllText(initVectorFile1);
                    string[] arrLinee = File.ReadAllLines(initVectorFile1);
                    if (sss.Contains("Target file: " + txbTargetFile.Text))
                    {
                        for (int i = 0; i < arrLinee.Length; i++)
                        {
                            if (arrLinee[i].Contains("Target file: " + txbTargetFile.Text))
                            {
                                arrLinee[i] = "";
                                arrLinee[i + 1] = "";
                                break;
                            }
                        }
                        File.WriteAllLines(initVectorFile1, arrLinee);
                    }
                    for (int i = 0; i < nizBajtova.Length; i += 8)
                    {
                        List<byte> pom1 = new List<byte>();
                        List<byte> pom2 = new List<byte>();
                        pom1.Add(nizBajtova[i]);
                        pom1.Add(nizBajtova[i + 1]);
                        pom1.Add(nizBajtova[i + 2]);
                        pom1.Add(nizBajtova[i + 3]);
                        pom1.Reverse();

                        pom2.Add(nizBajtova[i + 4]);
                        pom2.Add(nizBajtova[i + 5]);
                        pom2.Add(nizBajtova[i + 6]);
                        pom2.Add(nizBajtova[i + 7]);
                        pom2.Reverse();

                        blokPodataka[0] = BitConverter.ToUInt32(pom1.ToArray(), 0);
                        blokPodataka[1] = BitConverter.ToUInt32(pom2.ToArray(), 0);
                        XTEA.Encipher(ref blokPodataka, deloviKljuceva.ToArray());

                        List<byte> pom = new List<byte>();
                        pom = BitConverter.GetBytes(blokPodataka[0]).ToList();
                        pom.Reverse();
                        byte[] niz = new byte[pom.Count];
                        niz = pom.ToArray();
                        foreach (byte b in niz)
                        {
                            nizBajtovaZaUpis.Add(b);
                        }
                        pom = BitConverter.GetBytes(blokPodataka[1]).ToList();
                        pom.Reverse();
                        niz = new byte[pom.Count];
                        niz = pom.ToArray();
                        foreach (byte b in niz)
                        {
                            nizBajtovaZaUpis.Add(b);
                        }
                    }
                    nizBajtovaZaUpis.Add(crc);
                    File.WriteAllBytes(txbTargetFile.Text, nizBajtovaZaUpis.ToArray());
                }
                else
                {
                    byte[] initVector = new byte[16];
                    initVector = Encoding.ASCII.GetBytes(txbIV.Text);
                    List<byte> initVectorList = new List<byte>();
                    initVectorList = initVector.ToList();
                    int n = initVector.Length;
                    if (n == 0) //ako nije unet nikakav IV
                    {
                        for (int i = 0; i < 8; i++)
                        {
                            initVectorList.Add(0);
                            n++;
                        }
                    }
                    else
                    {
                        int ind = 0;
                        while (n < 8)
                        {
                            initVectorList.Add(initVector[ind++]);
                            n++;
                        }
                    }

                    initVectorList.Reverse();
                    initVector = initVectorList.ToArray();
                    byte[] xorOperand = new byte[8];
                    for (int i = 0; i < nizBajtova.Length; i += 8)
                    {
                        if(i == 0) // ako je prva iteracija, potrebno je koristiti initVector
                        {
                            for(int j=0; j<8; j++)
                            {
                                xorOperand[j] = initVector[j];
                            }
                        }
                        List<byte> pom1 = new List<byte>();
                        List<byte> pom2 = new List<byte>();
                        pom1.Add(nizBajtova[i]);
                        pom1.Add(nizBajtova[i + 1]);
                        pom1.Add(nizBajtova[i + 2]);
                        pom1.Add(nizBajtova[i + 3]);
                        pom1.Reverse();

                        pom2.Add(nizBajtova[i + 4]);
                        pom2.Add(nizBajtova[i + 5]);
                        pom2.Add(nizBajtova[i + 6]);
                        pom2.Add(nizBajtova[i + 7]);
                        pom2.Reverse();

                        XOR(ref pom1, ref pom2, xorOperand); //xor ulaznog bloka i xorOperanda

                        blokPodataka[0] = BitConverter.ToUInt32(pom1.ToArray(), 0);
                        blokPodataka[1] = BitConverter.ToUInt32(pom2.ToArray(), 0);

                        List<byte> provera1 = new List<byte>();
                        List<byte> provera2 = new List<byte>();
                        provera1 = BitConverter.GetBytes(blokPodataka[0]).ToList();
                        provera1.Reverse();

                        provera2 = BitConverter.GetBytes(blokPodataka[1]).ToList();
                        provera2.Reverse();

                        XTEA.Encipher(ref blokPodataka, deloviKljuceva.ToArray());
                        List<byte> pom3 = new List<byte>();
                        List<byte> pom4 = new List<byte>();

                        pom3 = BitConverter.GetBytes(blokPodataka[0]).ToList();
                        pom3.Reverse();
                        byte[] niz = new byte[pom3.Count];
                        niz = pom3.ToArray();
                        foreach (byte b in niz)
                        {
                            nizBajtovaZaUpis.Add(b);
                        }
                        pom4 = BitConverter.GetBytes(blokPodataka[1]).ToList();
                        pom4.Reverse();
                        niz = new byte[pom4.Count];
                        niz = pom4.ToArray();
                        foreach (byte b in niz)
                        {
                            nizBajtovaZaUpis.Add(b);
                        }

                        int ind = 0;
                        foreach(byte b in pom4)
                        {
                            pom3.Add(b);
                        }
                        foreach(byte b in pom3)
                        {
                            xorOperand[ind++] = b; // xor operand postaje rezultat prethodnog izvrsavanja algoritma
                        }
                    }
                    nizBajtovaZaUpis.Add(crc);
                    File.WriteAllBytes(txbTargetFile.Text, nizBajtovaZaUpis.ToArray());
                }
            }
            

        }

        private void btnDecrypt_Click(object sender, EventArgs e)
        {
            if (cmbIzbor.SelectedIndex == 0)//enigma
            {
                if (txbSourceFile.Text == "")
                {
                    MessageBox.Show("Source file not selected!");
                    return;
                }

                using (StreamReader sr = new StreamReader(txbSourceFile.Text))
                {
                    string s = sr.ReadToEnd();
                    string[] arrLine = File.ReadAllLines(txbSourceFile.Text);
                    string crcProsledjeni = arrLine[arrLine.Length - 1];
                    string[] pom = new string[arrLine.Length];
                    for(int i=0;i<arrLine.Length-1;i++)
                    {
                        if(arrLine[i] != null)
                        pom[i] = arrLine[i];
                    }
                    s = string.Join("", pom);

                    sr.Close();
                    rtbPlain.Text = s;
                    s.ToUpper();
                    if (txbTargetFile.Text == "")
                    {
                        MessageBox.Show("Target file not selected!");
                        return;
                    }
                    sr.Close();

                    using (StreamReader sr2 = new StreamReader(keyFile, true))
                    {
                        string refl = " ", iState = "", rSet = "", plugBoard = "";
                        int r1 = 0, r2 = 0, r3 = 0;
                        string a = sr2.ReadLine();
                        while (!sr2.EndOfStream && !a.Contains("Target file: " + txbSourceFile.Text))
                        {
                            a = sr2.ReadLine();
                        }
                        if (sr2.EndOfStream)
                        {
                            MessageBox.Show("Source file is not the encryption of any file");
                            return;
                        }
                        string key = sr2.ReadLine();
                        sr2.Close();
                        string[] niz = key.Split(' ');
                        for (int i = 0; i < niz.Length; i++)
                        {
                            switch (i)
                            {
                                case 0:
                                    r1 = Int16.Parse(niz[i]);
                                    break;
                                case 1:
                                    r2 = Int16.Parse(niz[i]);
                                    break;
                                case 2:
                                    r3 = Int16.Parse(niz[i]);
                                    break;
                                case 3:
                                    refl = niz[i];
                                    break;
                                case 4:
                                    iState = niz[i];
                                    break;
                                case 5:
                                    rSet = niz[i];
                                    break;
                                default:
                                    plugBoard = plugBoard + niz[i] + " ";
                                    break;
                            }
                        }
                        string initVectorStr;
                        using (StreamReader srr = new StreamReader(initVectorFile2, true))
                        {
                            string aa = srr.ReadLine();
                            while (!srr.EndOfStream && !aa.Contains("Target file: " + txbSourceFile.Text))
                            {
                                aa = srr.ReadLine();
                            }
                            if (srr.EndOfStream)
                            {
                                initVectorStr = "";
                            }
                            else
                            {
                                initVectorStr = srr.ReadLine();
                            }
                        }
                        string[] ss;
                        if (plugBoard.StartsWith(" "))
                            ss = null;
                        else
                            ss = plugBoard.Split(' ');
                        Enigma en = new Enigma((ushort)r1, (ushort)r2, (ushort)r3, refl[0], iState, rSet, ss);

                        if(initVectorStr == "")
                        {
                            using (StreamWriter sw = new StreamWriter(txbTargetFile.Text))
                            {
                                string plainText;
                                plainText = en.Encrypt(s);
                                byte crc = CRC.CRC_8(plainText);
                                if (crc.ToString() != crcProsledjeni)
                                    MessageBox.Show("Nisu isti");
                                sw.Write(plainText);
                                sw.Close();
                                MessageBox.Show("File successfuly decrypted!");
                                sw.Close();
                            }
                        }
                        else
                        {
                            using (StreamWriter sw = new StreamWriter(txbTargetFile.Text))
                            {
                                char[] initVector = new char[16];
                                initVector = initVectorStr.ToCharArray();
                                List<char> initVectorList = new List<char>();
                                initVectorList = initVector.ToList();
                                int n = initVector.Length;
                                if (n == 0) //ako nije unet nikakav IV
                                {
                                    for (int i = 0; i < 8; i++)
                                    {
                                        initVectorList.Add('\0');
                                        n++;
                                    }
                                }
                                else
                                {
                                    int ind = 0;
                                    while (n < 8)
                                    {
                                        initVectorList.Add(initVector[ind++]);
                                        n++;
                                    }
                                }

                                initVector = initVectorList.ToArray();

                                char[] xorOperand = new char[8];
                                char[] zaDekripciju;
                                //ovde podeliti ulazni string na blokove 
                                string plainText = "";
                                for (int i = 0; i < s.Length; i += 8)
                                {
                                    if (i == 0)
                                    {
                                        for (int j = 0; j < 8; j++)
                                        {
                                            xorOperand[j] = initVector[j];
                                        }
                                    }
                                    List<char> lista = new List<char>();
                                    lista.Add(s[i]);
                                    lista.Add(s[i + 1]);
                                    lista.Add(s[i + 2]);
                                    lista.Add(s[i + 3]);
                                    lista.Add(s[i + 4]);
                                    lista.Add(s[i + 5]);
                                    lista.Add(s[i + 6]);
                                    lista.Add(s[i + 7]);

                                    string plainTextBlock;

                                    zaDekripciju = lista.ToArray();

                                    plainTextBlock = en.Encrypt(string.Join("", zaDekripciju));
                                    List<char> pomocna = plainTextBlock.ToCharArray().ToList();

                                    XOR(ref pomocna, xorOperand);

                                    char[] pomocni = pomocna.ToArray();

                                    plainText += string.Join("", pomocni);

                                }
                                plainText = plainText.ToUpper();
                                int k = plainText.Length - 1;
                                while(plainText[k] == ' ')
                                {
                                    plainText = plainText.Remove(k);
                                    k--;
                                }
                                byte crc = CRC.CRC_8(plainText);
                                if (crc.ToString() != crcProsledjeni)
                                    MessageBox.Show("Nisu isti");
                                sw.Write(plainText);
                                sw.Close();
                                MessageBox.Show("File successfuly decrypted!");
                                sw.Close();
                            }
                        }
                        
                    }
                }
            }
            else if (cmbIzbor.SelectedIndex == 1)//xtea
            {
                if (txbSourceFile.Text == "")
                {
                    MessageBox.Show("Source file not selected!");
                    return;
                }
                List<byte> listaBajtova = File.ReadAllBytes(txbSourceFile.Text).ToList();
                byte crcProsledjen = listaBajtova[listaBajtova.Count - 1]; //poklapa se

                rtbPlain.Text = File.ReadAllText(txbSourceFile.Text);
                while (listaBajtova.Count % 8 != 0)
                {
                    listaBajtova.Add((byte)'.');
                }
                string kljucStr;
                using (StreamReader sr = new StreamReader(keyFile2, true))
                {
                    string a = sr.ReadLine();
                    while (!sr.EndOfStream && !a.Contains("Target file: " + txbSourceFile.Text))
                    {
                        a = sr.ReadLine();
                    }
                    if (sr.EndOfStream)
                    {
                        MessageBox.Show("Source file is not the encryption of any file");
                        return;
                    }
                    kljucStr = sr.ReadLine();
                }

                //uzimanje initVectora iz fajla ako postoji
                string initVectorStr;
                using (StreamReader sr = new StreamReader(initVectorFile1, true))
                {
                    string a = sr.ReadLine();
                    while (!sr.EndOfStream && !a.Contains("Target file: " + txbSourceFile.Text))
                    {
                        a = sr.ReadLine();
                    }
                    if (sr.EndOfStream)
                    {
                        initVectorStr = "";
                    }
                    else
                    {
                        initVectorStr = sr.ReadLine();
                    }
                }

                byte[] nizBajtova = new byte[listaBajtova.Count];
                nizBajtova = listaBajtova.ToArray();

                UInt32[] blokPodataka = new UInt32[2];
                byte[] kljuc = new byte[16];
                kljuc = Encoding.ASCII.GetBytes(kljucStr);
                List<byte> kljucList = new List<byte>();
                kljucList = kljuc.ToList();
                int k = kljuc.Length;
                while (k < 16)
                {
                    kljucList.Add(0);
                    k++;
                }
                kljucList.Reverse();
                kljuc = kljucList.ToArray();
                int s = 0;
                for (int i = 0; i < kljuc.Length; i++)
                {
                    if (s == kljuc.Length - i)
                        break;
                    if (kljuc[i] != 0)
                    {
                        byte a = kljuc[i];
                        kljuc[i] = kljuc[kljuc.Length - s - 1];
                        kljuc[kljuc.Length - s - 1] = a;
                        s++;
                    }
                }
                List<UInt32> deloviKljuceva = new List<UInt32>();
                for (int i = 0; i < 16; i += 4)
                {
                    List<byte> pom1 = new List<byte>();
                    pom1.Add(kljuc[i]);
                    pom1.Add(kljuc[i + 1]);
                    pom1.Add(kljuc[i + 2]);
                    pom1.Add(kljuc[i + 3]);
                    pom1.Reverse();
                    deloviKljuceva.Add(BitConverter.ToUInt32(pom1.ToArray(), 0));
                }
                List<byte> nizBajtovaZaUpis = new List<byte>();
                if(initVectorStr == "")
                {
                    for (int i = 0; i < nizBajtova.Length-8; i += 8)
                    {
                        List<byte> pom1 = new List<byte>();
                        List<byte> pom2 = new List<byte>();
                        pom1.Add(nizBajtova[i]);
                        pom1.Add(nizBajtova[i + 1]);
                        pom1.Add(nizBajtova[i + 2]);
                        pom1.Add(nizBajtova[i + 3]);
                        pom1.Reverse();
                        pom2.Add(nizBajtova[i + 4]);
                        pom2.Add(nizBajtova[i + 5]);
                        pom2.Add(nizBajtova[i + 6]);
                        pom2.Add(nizBajtova[i + 7]);
                        pom2.Reverse();
                        blokPodataka[0] = BitConverter.ToUInt32(pom1.ToArray(), 0);
                        blokPodataka[1] = BitConverter.ToUInt32(pom2.ToArray(), 0);
                        XTEA.Decipher(ref blokPodataka, deloviKljuceva.ToArray());

                        List<byte> pom = new List<byte>();
                        pom = BitConverter.GetBytes(blokPodataka[0]).ToList();
                        pom.Reverse();
                        byte[] niz = new byte[pom.Count];
                        niz = pom.ToArray();
                        foreach (byte b in niz)
                        {
                            nizBajtovaZaUpis.Add(b);
                        }
                        pom = BitConverter.GetBytes(blokPodataka[1]).ToList();
                        pom.Reverse();
                        niz = new byte[pom.Count];
                        niz = pom.ToArray();
                        foreach (byte b in niz)
                        {
                            nizBajtovaZaUpis.Add(b);
                        }
                    }
                    string ss = Encoding.ASCII.GetString(nizBajtovaZaUpis.ToArray());
                    byte crcc = CRC.CRC_8(nizBajtovaZaUpis);
                    if (crcProsledjen != crcc)
                        MessageBox.Show("Nisu isti");
                    File.WriteAllBytes(txbTargetFile.Text, nizBajtovaZaUpis.ToArray());
                    MessageBox.Show("File succesfully decrypted!");
                }
                else
                {
                    byte[] initVector = new byte[16];
                    initVector = Encoding.ASCII.GetBytes(initVectorStr);
                    List<byte> initVectorList = new List<byte>();
                    initVectorList = initVector.ToList();
                    int n = initVector.Length;
                    if (n == 0) //ako nije unet nikakav IV
                    {
                        for (int i = 0; i < 8; i++)
                        {
                            initVectorList.Add(0);
                            n++;
                        }
                    }
                    else
                    {
                        int ind = 0;
                        while (n < 8)
                        {
                            initVectorList.Add(initVector[ind++]);
                            n++;
                        }
                    }

                    initVectorList.Reverse();
                    initVector = initVectorList.ToArray();
                    byte[] xorOperand = new byte[8];

                    for (int i = 0; i < nizBajtova.Length-8; i += 8)
                    {
                        if (i == 0) // ako je prva iteracija, potrebno je koristiti initVector
                        {
                            for (int j = 0; j < 8; j++)
                            {
                                xorOperand[7-j] = initVector[j];
                            }
                        }
                        List<byte> pom1 = new List<byte>();
                        List<byte> pom2 = new List<byte>();
                        List<byte> pom3 = new List<byte>();
                        List<byte> pom4 = new List<byte>();

                        pom1.Add(nizBajtova[i]);
                        pom1.Add(nizBajtova[i + 1]);
                        pom1.Add(nizBajtova[i + 2]);
                        pom1.Add(nizBajtova[i + 3]);
                        pom1.Reverse();

                        pom2.Add(nizBajtova[i + 4]);
                        pom2.Add(nizBajtova[i + 5]);
                        pom2.Add(nizBajtova[i + 6]);
                        pom2.Add(nizBajtova[i + 7]);
                        pom2.Reverse();

                        blokPodataka[0] = BitConverter.ToUInt32(pom1.ToArray(), 0);
                        blokPodataka[1] = BitConverter.ToUInt32(pom2.ToArray(), 0);

                        XTEA.Decipher(ref blokPodataka, deloviKljuceva.ToArray());

                        pom3 = BitConverter.GetBytes(blokPodataka[0]).ToList();
                        pom3.Reverse();

                        pom4 = BitConverter.GetBytes(blokPodataka[1]).ToList();
                        pom4.Reverse();

                        XOR(ref pom3, ref pom4, xorOperand);

                        byte[] niz = new byte[pom3.Count];
                        niz = pom3.ToArray();
                        foreach (byte b in niz)
                        {
                            nizBajtovaZaUpis.Add(b);
                        }
                        niz = new byte[pom4.Count];
                        niz = pom4.ToArray();
                        foreach (byte b in niz)
                        {
                            nizBajtovaZaUpis.Add(b);
                        }

                        int ind = 0;
                        foreach(byte b in pom1)
                        {
                            xorOperand[ind++] = b;
                        }
                        foreach (byte b in pom2)
                        {
                            xorOperand[ind++] = b;
                        }
                    }
                    string ss = Encoding.ASCII.GetString(nizBajtovaZaUpis.ToArray());
                    byte crcc = CRC.CRC_8(nizBajtovaZaUpis);
                    if (crcProsledjen != crcc)
                        MessageBox.Show("Nisu isti");
                    File.WriteAllBytes(txbTargetFile.Text, nizBajtovaZaUpis.ToArray());
                    MessageBox.Show("File succesfully decrypted!");
                }
            }

        }

        private void txbInitialState_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar < 'A' || e.KeyChar > 'Z')
            {
                e.Handled = false;
            }
        }

        private void btnChangeSetting_Click(object sender, EventArgs e)
        {
            
            EnigmaSettings frm = new EnigmaSettings();
            DialogResult dlg = frm.ShowDialog();
            if(dlg == DialogResult.OK)
            {
                this.enigma = new Enigma(frm.rotor1, frm.rotor2, frm.rotor3,
                    frm.reflector, frm.initialState, frm.ringSetting,frm.plugboardSetting);

                this.key = frm.rotor1 + " " + frm.rotor2 + " " + frm.rotor3 +
                    " " + frm.reflector + " " + frm.initialState + " " + frm.ringSetting +
                    " "; 
                if(frm.plugboardSetting != null)
                foreach(string s in frm.plugboardSetting)
                {
                    this.key = this.key + s;
                    this.key = this.key + " ";
                }

            }
        }

        private void btnSourceFileDialog_Click(object sender, EventArgs e)
        {
            if (ofdSourceFile.ShowDialog() == DialogResult.OK)
            {
                txbSourceFile.Text = ofdSourceFile.FileName;
                using (StreamReader sr = new StreamReader(ofdSourceFile.FileName))
                {
                    rtbPlain.Text = sr.ReadToEnd();
                    sr.Close();
                }
            }
        }

        private void fsw_Created(object sender, System.IO.FileSystemEventArgs e)
        {
            if (cmbIzbor.SelectedIndex == 0)
            {
                using (StreamReader sr = new StreamReader(e.FullPath))
                {
                    string st = sr.ReadToEnd();
                    rtbPlain.Text = st;
                    st.ToUpper();
                    if (txbTargetFile.Text == "")
                    {
                        MessageBox.Show("Target file not selected!");
                        return;
                    }
                    using (StreamWriter sw = new StreamWriter(txbTargetFile.Text))
                    {
                        string cipher;
                        cipher = enigma.Encrypt(st);
                        sw.Write(cipher);
                        sw.Close();
                    }
                    string ss = File.ReadAllText(keyFile);
                    string[] arrLine = File.ReadAllLines(keyFile);
                    if (ss.Contains("Target file: " + txbTargetFile.Text))
                    {
                        for (int i = 0; i < arrLine.Length; i++)
                        {
                            if (arrLine[i].Contains("Target file: " + txbTargetFile.Text))
                            {
                                if (key == "")
                                {
                                    arrLine[i + 1] = "1 2 3 B AAZ AAA ";
                                    break;
                                }
                                else
                                {
                                    arrLine[i + 1] = key;
                                    break;
                                }

                            }
                        }
                        File.WriteAllLines(keyFile, arrLine);
                        MessageBox.Show("File successfuly encrypted!");
                    }
                    else
                    {
                        using (StreamWriter sw2 = new StreamWriter(keyFile, true))
                        {
                            if (key == "")
                            {
                                key = "1 2 3 B AAZ AAA ";
                                sw2.WriteLine("Target file: " + txbTargetFile.Text);
                                sw2.WriteLine(key);
                                sw2.WriteLine();
                                key = "";
                            }
                            else
                            {
                                sw2.WriteLine("Target file: " + txbTargetFile.Text);
                                sw2.WriteLine(key);
                                sw2.WriteLine();
                                key = "";
                            }
                            sw2.Close();
                        }

                    }
                }

            }
            else if (cmbIzbor.SelectedIndex == 1)
            {
                if (txbTargetFile.Text == "")
                {
                    MessageBox.Show("Target file not selected!");
                    return;
                }

                List<byte> listaBajtova = Encoding.ASCII.GetBytes(File.ReadAllText(e.FullPath)).ToList();
                rtbPlain.Text = File.ReadAllText(e.FullPath);
                while (listaBajtova.Count % 8 != 0)
                {
                    listaBajtova.Add((byte)'.');
                }
                string ss = File.ReadAllText(keyFile2);
                string[] arrLine = File.ReadAllLines(keyFile2);
                if (ss.Contains("Target file: " + txbTargetFile.Text))
                {
                    for (int i = 0; i < arrLine.Length; i++)
                    {
                        if (arrLine[i].Contains("Target file: " + txbTargetFile.Text))
                        {
                            arrLine[i + 1] = txbKey.Text;
                            break;
                        }
                    }
                    File.WriteAllLines(keyFile2, arrLine);
                    MessageBox.Show("File successfuly encrypted!");
                }
                else
                {
                    using (StreamWriter sw = new StreamWriter(keyFile2, true))
                    {

                        if (txbKey.Text == "")
                        {
                            key2 = "abcdefg";
                            sw.WriteLine("Target file: " + txbTargetFile.Text);
                            sw.WriteLine(key2);
                            sw.WriteLine();
                            key2 = "";
                            MessageBox.Show("File successfuly encrypted!");
                        }
                        else
                        {
                            sw.WriteLine("Target file: " + txbTargetFile.Text);
                            sw.WriteLine(txbKey.Text);
                            sw.WriteLine();
                            key2 = "";
                            MessageBox.Show("File successfuly encrypted!");
                        }
                    }
                }
                byte[] nizBajtova = new byte[listaBajtova.Count];
                nizBajtova = listaBajtova.ToArray();
                UInt32[] blokPodataka = new UInt32[2];
                byte[] kljuc = new byte[16];
                kljuc = Encoding.ASCII.GetBytes(txbKey.Text);
                List<byte> kljucList = new List<byte>();
                kljucList = kljuc.ToList();
                int k = kljuc.Length;
                while (k < 16)
                {
                    kljucList.Add(0);
                    k++;
                }
                kljucList.Reverse();
                kljuc = kljucList.ToArray();
                int s = 0;
                for (int i = 0; i < kljuc.Length; i++)
                {
                    if (s == kljuc.Length - i)
                        break;
                    if (kljuc[i] != 0)
                    {
                        byte a = kljuc[i];
                        kljuc[i] = kljuc[kljuc.Length - s - 1];
                        kljuc[kljuc.Length - s - 1] = a;
                        s++;
                    }
                }
                List<UInt32> deloviKljuceva = new List<UInt32>();
                for (int i = 0; i < 16; i += 4)
                {
                    List<byte> pom1 = new List<byte>();
                    pom1.Add(kljuc[i]);
                    pom1.Add(kljuc[i + 1]);
                    pom1.Add(kljuc[i + 2]);
                    pom1.Add(kljuc[i + 3]);
                    pom1.Reverse();
                    deloviKljuceva.Add(BitConverter.ToUInt32(pom1.ToArray(), 0));
                }
                List<byte> nizBajtovaZaUpis = new List<byte>();
                for (int i = 0; i < nizBajtova.Length; i += 8)
                {
                    List<byte> pom1 = new List<byte>();
                    List<byte> pom2 = new List<byte>();
                    pom1.Add(nizBajtova[i]);
                    pom1.Add(nizBajtova[i + 1]);
                    pom1.Add(nizBajtova[i + 2]);
                    pom1.Add(nizBajtova[i + 3]);
                    pom1.Reverse();
                    pom2.Add(nizBajtova[i + 4]);
                    pom2.Add(nizBajtova[i + 5]);
                    pom2.Add(nizBajtova[i + 6]);
                    pom2.Add(nizBajtova[i + 7]);
                    pom2.Reverse();
                    blokPodataka[0] = BitConverter.ToUInt32(pom1.ToArray(), 0);
                    blokPodataka[1] = BitConverter.ToUInt32(pom2.ToArray(), 0);
                    XTEA.Encipher(ref blokPodataka, deloviKljuceva.ToArray());

                    List<byte> pom = new List<byte>();
                    pom = BitConverter.GetBytes(blokPodataka[0]).ToList();
                    pom.Reverse();
                    byte[] niz = new byte[pom.Count];
                    niz = pom.ToArray();
                    foreach (byte b in niz)
                    {
                        nizBajtovaZaUpis.Add(b);
                    }
                    pom = BitConverter.GetBytes(blokPodataka[1]).ToList();
                    pom.Reverse();
                    niz = new byte[pom.Count];
                    niz = pom.ToArray();
                    foreach (byte b in niz)
                    {
                        nizBajtovaZaUpis.Add(b);
                    }
                }
                File.WriteAllBytes(txbTargetFile.Text, nizBajtovaZaUpis.ToArray());
            }
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            this.DialogResult = DialogResult.Abort;
        }

        private void btnPick_Click(object sender, EventArgs e)
        {
            if (cmbIzbor.SelectedIndex == 0)
            {
                txbKey.Enabled = false;
                btnChangeSetting.Enabled = true;
            }
            else if (cmbIzbor.SelectedIndex == 1)
            {
                btnChangeSetting.Enabled = false;
                txbKey.Enabled = true;
            }
        }

        private void ckbIV_CheckedChanged(object sender, EventArgs e)
        {
            if (ckbIV.Checked)
            {
                txbIV.Enabled = true;
            }
            else
            {
                txbIV.Enabled = false;
            }
        }
        #endregion

        #region Methods

        public void XOR(ref List<byte> pom1, ref List<byte> pom2, byte[] xorOperand)
        {
            if(pom1.Count + pom2.Count == xorOperand.Length)
            {
                for (int i = 0; i < pom1.Count; i++)
                {
                    pom1[i] = (byte)(pom1[i] ^ xorOperand[i]);
                }

                for (int i = 0; i < pom2.Count; i++)
                {
                    pom2[i] = (byte)(pom2[i] ^ xorOperand[i+4]);
                }
            }
        }

        public void XOR(ref List<char> lista, char[] xorOperand)
        {
            if(lista.Count == xorOperand.Length)
            {
                for(int i = 0; i<lista.Count; i++)
                {
                    lista[i] = (char)(lista[i] ^ xorOperand[i]);
                }
            }
        }

        #endregion
    }
}
