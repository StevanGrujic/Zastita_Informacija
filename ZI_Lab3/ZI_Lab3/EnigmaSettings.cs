using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ZI_Lab2
{
    public partial class EnigmaSettings : Form
    {
        public ushort rotor1;
        public ushort rotor2;
        public ushort rotor3;
        public char reflector;
        public string initialState;
        public string ringSetting;
        public string[] plugboardSetting;

        public EnigmaSettings()
        {
            rotor1 = 1;
            rotor2 = 2;
            rotor3 = 3;
            reflector = 'B';
            initialState = "AAZ";
            ringSetting = "AAA";
            plugboardSetting = null;
            InitializeComponent();
        }

        private void btnRunSetting_Click(object sender, EventArgs e)
        {
            rotor1 = (ushort)nudR1.Value;
            rotor2 = (ushort)nudR2.Value;
            rotor3 = (ushort)nudR3.Value;
            string s = cmbReflektor.SelectedItem.ToString();
            char[] niz = s.ToCharArray();
            reflector = (char)niz[0];
            initialState = txbInitialState.Text;
            ringSetting = txbRingSetting.Text;

            if(txbPlugboard.Text!="")
            {
                char[] pom = txbPlugboard.Text.ToCharArray();
                for (int i = 0; i < pom.Length; i++)
                {
                    if (pom[i] == ' ')
                    {
                        continue;
                    }
                    if ((pom[i] < 'A' || (pom[i] > 'Z' && pom[i] < 'a') || pom[i] > 'z'))
                    {
                        MessageBox.Show("Mozete uneti samo slova");
                        txbPlugboard.Text = "";
                        break;
                    }
                }
                s = txbPlugboard.Text;
                s.ToUpper();
                plugboardSetting = s.Split(' ');

                for (int i = 0; i < plugboardSetting.Length; i++)
                {
                    if (plugboardSetting[i].Length != 2)
                    {
                        MessageBox.Show("Slova je potrebno pisati u parovima, odvojenim blanko znakom");
                        txbPlugboard.Text = "";
                        plugboardSetting = null;
                        break;
                    }
                }

            }


            this.Close();
            this.DialogResult = DialogResult.OK;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
            this.DialogResult = DialogResult.Cancel;
        }
    }
}
