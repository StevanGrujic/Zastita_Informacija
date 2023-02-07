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
        #endregion

        #region Constructors
        public Form1()
        {
            InitializeComponent();

            keyFile = System.IO.Path.Combine(System.AppDomain.CurrentDomain.BaseDirectory, "..\\..\\Keys.txt");
            keyFile2 = System.IO.Path.Combine(System.AppDomain.CurrentDomain.BaseDirectory, "..\\..\\KeysXtea.txt");
            fsw.Path = System.IO.Path.Combine(System.AppDomain.CurrentDomain.BaseDirectory, "..\\..\\PlainTextFiles");
            txbSourceFile.Enabled = false;
            btnSourceFileDialog.Enabled = false;
            btnEncrypt.Enabled = false;
            btnDecrypt.Enabled = false;
            txbKey.Enabled = false;
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
            if(cmbIzbor.SelectedIndex == 0)
            {
                if (txbSourceFile.Text == "")
                {
                    MessageBox.Show("Source file not selected!");
                    return;
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
                    using (StreamWriter sw = new StreamWriter(txbTargetFile.Text))
                    {
                        string cipher;
                        cipher = enigma.Encrypt(s);
                        sw.Write(cipher);
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
                                if(key == "")
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
            else if(cmbIzbor.SelectedIndex == 1)
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
                List<byte> listaBajtova = Encoding.ASCII.GetBytes(File.ReadAllText(txbSourceFile.Text)).ToList();
                rtbPlain.Text = File.ReadAllText(txbSourceFile.Text);
                while(listaBajtova.Count % 8 != 0)
                {
                    listaBajtova.Add((byte)'.');
                }

                byte[] nizBajtova = new byte[listaBajtova.Count];
                nizBajtova = listaBajtova.ToArray();

                UInt32[] blokPodataka = new UInt32[2];
                byte[] kljuc = new byte[16];
                kljuc = Encoding.ASCII.GetBytes(txbKey.Text);
                List<byte> kljucList = new List<byte>();
                kljucList = kljuc.ToList();
                int k = kljuc.Length;
                while(k<16)
                {
                    kljucList.Add(0);
                    k++;
                }
                kljucList.Reverse();
                kljuc = kljucList.ToArray();
                int s = 0;
                for(int i = 0; i<kljuc.Length; i++)
                {
                    if (s == kljuc.Length - i)
                        break;
                    if (kljuc[i]!=0)
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
                for (int i=0; i<nizBajtova.Length; i+=8)
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

        private void btnDecrypt_Click(object sender, EventArgs e)
        {
            if (cmbIzbor.SelectedIndex == 0)
            {
                if (txbSourceFile.Text == "")
                {
                    MessageBox.Show("Source file not selected!");
                    return;
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
                        sr2.Close(); //ovo sam dodao
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
                        string[] ss;
                        if (plugBoard.StartsWith(" "))
                            ss = null;
                        else
                            ss = plugBoard.Split(' ');
                        Enigma en = new Enigma((ushort)r1, (ushort)r2, (ushort)r3, refl[0], iState, rSet, ss);
                        using (StreamWriter sw = new StreamWriter(txbTargetFile.Text))
                        {
                            string cipher;
                            cipher = en.Encrypt(s);
                            sw.Write(cipher);
                            sw.Close();
                            MessageBox.Show("File successfuly decrypted!");
                            sw.Close();
                        }
                    }
                }
            }
            else if (cmbIzbor.SelectedIndex == 1)
            {
                if (txbSourceFile.Text == "")
                {
                    MessageBox.Show("Source file not selected!");
                    return;
                }
                List<byte> listaBajtova = File.ReadAllBytes(txbSourceFile.Text).ToList();
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
                File.WriteAllBytes(txbTargetFile.Text, nizBajtovaZaUpis.ToArray());
                MessageBox.Show("File succesfully decrypted!");
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

                this.key += frm.rotor1 + " " + frm.rotor2 + " " + frm.rotor3 +
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
                //using (StreamWriter sw = new StreamWriter(keyFile2, true))
                //{
                //    if (txbKey.Text == "")
                //    {
                //        key2 = "abcdefg";
                //        sw.WriteLine("Target file: " + txbTargetFile.Text);
                //        sw.WriteLine(key2);
                //        sw.WriteLine();
                //        key2 = "";
                //        MessageBox.Show("File successfuly encrypted!");
                //    }
                //    else
                //    {
                //        sw.WriteLine("Target file: " + txbTargetFile.Text);
                //        sw.WriteLine(txbKey.Text);
                //        sw.WriteLine();
                //        key2 = "";
                //        MessageBox.Show("File successfuly encrypted!");
                //    }
                //    sw.Close();
                //}
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
        #endregion
    }
}
