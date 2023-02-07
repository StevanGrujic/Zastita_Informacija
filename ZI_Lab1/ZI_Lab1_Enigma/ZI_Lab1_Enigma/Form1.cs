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

namespace ZI_Lab1_Enigma
{
    public partial class Form1 : Form
    {
        Enigma enigma;
        string key = "";
        string keyFile;
        public Form1()
        {
            InitializeComponent();

            keyFile = System.IO.Path.Combine(System.AppDomain.CurrentDomain.BaseDirectory, "..\\..\\Keys.txt");
            fsw.Path = System.IO.Path.Combine(System.AppDomain.CurrentDomain.BaseDirectory, "..\\..\\PlainTextFiles");
            txbSourceFile.Enabled = false;
            btnSourceFileDialog.Enabled = false;
            btnEncrypt.Enabled = false;
            btnDecrypt.Enabled = false;
            this.enigma = new Enigma(1, 2, 3, 'B', "AAZ", "AAA",null);
        }

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
                using (StreamWriter sw2 = new StreamWriter(keyFile,true)) 
                {
                    if(key == "")
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
                foreach(string s in frm.plugboardSetting)
                {
                    this.key = this.key + s;
                    this.key = this.key + " ";
                }

            }
        }
        #endregion

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

        private void btnDecrypt_Click(object sender, EventArgs e)
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
                    string refl=" ", iState="", rSet="", plugBoard = "";
                    int r1 = 0, r2 = 0, r3 = 0;
                    string a = sr2.ReadLine();
                    while(!sr2.EndOfStream && !a.Contains("Target file: " + txbSourceFile.Text))
                    {
                        a = sr2.ReadLine();
                    }
                    if(sr2.EndOfStream)
                    {
                        MessageBox.Show("Source file is not the encryption of any file");
                        return;
                    }
                        string key = sr2.ReadLine();
                        sr2.Close(); //ovo sam dodao
                        string[] niz = key.Split(' ');
                        for(int i = 0;i<niz.Length;i++)
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

        private void fsw_Created(object sender, System.IO.FileSystemEventArgs e)
        {
             using (StreamReader sr = new StreamReader(e.FullPath))
             {
                string st = sr.ReadToEnd();
                //sr.Close();
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
                using (StreamWriter sw2 = new StreamWriter(keyFile, true))
                {
                    if (key == "")
                    {
                        key = "1 2 3 B AAZ AAA ";
                        sw2.WriteLine("Target file: " + txbTargetFile.Text);
                        sw2.WriteLine(key);
                        sw2.WriteLine();
                        key = "";
                        MessageBox.Show("File successfuly encrypted!");
                    }
                    else
                    {
                        sw2.WriteLine("Target file: " + txbTargetFile.Text);
                        sw2.WriteLine(key);
                        sw2.WriteLine();
                        key = "";
                        MessageBox.Show("File successfuly encrypted!");
                    }
                    sw2.Close();
                }

            }
        }
    }
}
