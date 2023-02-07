using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZI_Lab2
{
    class Enigma
    {
        static char[] RotorI = { 'E', 'K', 'M', 'F', 'L', 'G', 'D', 'Q', 'V', 'Z', 'N', 'T', 'O', 'W', 'Y', 'H', 'X', 'U', 'S', 'P', 'A', 'I', 'B', 'R', 'C', 'J' };
        static char[] RotorII = { 'A', 'J', 'D', 'K', 'S', 'I', 'R', 'U', 'X', 'B', 'L', 'H', 'W', 'T', 'M', 'C', 'Q', 'G', 'Z', 'N', 'P', 'Y', 'F', 'V', 'O', 'E' };
        static char[] RotorIII = { 'B', 'D', 'F', 'H', 'J', 'L', 'C', 'P', 'R', 'T', 'X', 'V', 'Z', 'N', 'Y', 'E', 'I', 'W', 'G', 'A', 'K', 'M', 'U', 'S', 'Q', 'O' };
        static char[] ReflectorA = { 'E', 'J', 'M', 'Z', 'A', 'L', 'Y', 'X', 'V', 'B', 'W', 'F', 'C', 'R', 'Q', 'U', 'O', 'N', 'T', 'S', 'P', 'I', 'K', 'H', 'G', 'D' };
        static char[] ReflectorB = { 'Y', 'R', 'U', 'H', 'Q', 'S', 'L', 'D', 'P', 'X', 'N', 'G', 'O', 'K', 'M', 'I', 'E', 'B', 'F', 'Z', 'C', 'W', 'V', 'J', 'A', 'T' };


        char[,] _rotors = new char[3, 26];
        char[] _reflector = new char[26];
        string _initialState;
        string _ringSetting;
        string[] plugboard;
        public ushort Rotor1 
        {
            set
            {
                if(value<=8 && value>0)
                {
                    switch (value)
                    {
                        case 1:
                            for (int i = 0; i < 26; i++)
                            {
                                _rotors[0, i] = RotorI[i];
                            }
                            break;
                        case 2:
                            for (int i = 0; i < 26; i++)
                            {
                                _rotors[0, i] = RotorII[i];
                            }
                            break;
                        case 3:
                            for (int i = 0; i < 26; i++)
                            {
                                _rotors[0, i] = RotorIII[i];
                            }
                            break;
                        default:
                            break;
                    }
                }
            } 
        }
        public ushort Rotor2
        {
            set
            {
                if (value <= 8 && value > 0)
                {
                    switch (value)
                    {
                        case 1:
                            for (int i = 0; i < 26; i++)
                            {
                                _rotors[1, i] = RotorI[i];
                            }
                            break;
                        case 2:
                            for (int i = 0; i < 26; i++)
                            {
                                _rotors[1, i] = RotorII[i];
                            }
                            break;
                        case 3:
                            for (int i = 0; i < 26; i++)
                            {
                                _rotors[1, i] = RotorIII[i];
                            }
                            break;
                        default:
                            break;
                    }
                }
            }
        }
        public ushort Rotor3
        {
            set
            {
                if (value <= 8 && value > 0)
                {
                    switch (value)
                    {
                        case 1:
                            for (int i = 0; i < 26; i++)
                            {
                                _rotors[2, i] = RotorI[i];
                            }
                            break;
                        case 2:
                            for (int i = 0; i < 26; i++)
                            {
                                _rotors[2, i] = RotorII[i];
                            }
                            break;
                        case 3:
                            for (int i = 0; i < 26; i++)
                            {
                                _rotors[2, i] = RotorIII[i];
                            }
                            break;
                        default:
                            break;
                    }
                }
            }
        }
        public Enigma(ushort prviRotor, ushort drugiRotor, ushort treciRotor, char reflektor, string initialState, string ringSetting,string[]plg)
        {
            if (prviRotor > 8 || drugiRotor > 8 || treciRotor > 8)
            {
                throw new Exception("Rotori moraju da imaju vrednosti manje od 8");
            }

            if (reflektor != 'A' && reflektor != 'B' && reflektor != 'a' && reflektor != 'b')
            {
                throw new Exception("Reflektor moze da bude samo tipa A ili tipa B");
            }

            if (initialState.Length != 3)
            {
                throw new Exception("Inicijalno stanje mora da ima 3 slova");
            }

            if (ringSetting.Length != 3)
            {
                throw new Exception("Inicijalno stanje mora da ima 3 slova");
            }
            initialState = initialState.ToUpper();
            ringSetting = ringSetting.ToUpper();
            this._initialState = initialState;
            this._ringSetting = ringSetting;
            switch (prviRotor)
            {
                case 1:
                    for (int i = 0; i < 26; i++)
                    {
                        _rotors[0, i] = RotorI[i];
                    }
                    break;
                case 2:
                    for (int i = 0; i < 26; i++)
                    {
                        _rotors[0, i] = RotorII[i];
                    }
                    break;
                case 3:
                    for (int i = 0; i < 26; i++)
                    {
                        _rotors[0, i] = RotorIII[i];
                    }
                    break;
                default:
                    break;
            }
            switch (drugiRotor)
            {
                case 1:
                    for (int i = 0; i < 26; i++)
                    {
                        _rotors[1, i] = RotorI[i];
                    }
                    break;
                case 2:
                    for (int i = 0; i < 26; i++)
                    {
                        _rotors[1, i] = RotorII[i];
                    }
                    break;
                case 3:
                    for (int i = 0; i < 26; i++)
                    {
                        _rotors[1, i] = RotorIII[i];
                    }
                    break;
                default:
                    break;
            }
            switch (treciRotor)
            {
                case 1:
                    for (int i = 0; i < 26; i++)
                    {
                        _rotors[2, i] = RotorI[i];
                    }
                    break;
                case 2:
                    for (int i = 0; i < 26; i++)
                    {
                        _rotors[2, i] = RotorII[i];
                    }
                    break;
                case 3:
                    for (int i = 0; i < 26; i++)
                    {
                        _rotors[2, i] = RotorIII[i];
                    }
                    break;
                default:
                    break;
            }
            switch (reflektor)
            {
                case 'A':
                    for (int i = 0; i < 26; i++)
                    {
                        _reflector[i] = ReflectorA[i];
                    }
                    break;
                case 'B':
                    for (int i = 0; i < 26; i++)
                    {
                        _reflector[i] = ReflectorB[i];
                    }
                    break;
                default:
                    break;

            }

            /* if (plg != null)
             {
                 char pom;
                 foreach (string p in plg)
                 {
                     for (int i = 0; i < 3; i++)
                     {
                         pom = _rotors[i, (int)(p[0] - 'A')];
                         _rotors[i, (int)(p[0] - 'A')] = _rotors[i, (int)(p[1] - 'A')];
                         _rotors[i, (int)(p[1] - 'A')] = pom;
                     }

                     pom = _reflector[(int)(p[0] - 'A')];
                     _reflector[(int)(p[0] - 'A')] = _reflector[(int)(p[1] - 'A')];
                     _reflector[(int)(p[1] - 'A')] = pom;
                 }
             }*/
            this.plugboard = plg;

            char[] initialStateArray = initialState.ToCharArray();
            int[] nizInt = new int[3]; //niz pomeraja za svaki od rotora
            for (int i = 0; i < 3; i++)
            {
                nizInt[i] = initialStateArray[i] - 'A';
            }
            char[] pomocni = new char[26];
            for (int i = 0; i < 3; i++)
            {
                if (nizInt[i] == 0)
                    continue;
                for (int j = 0; j < 26; j++)
                {
                    _rotors[i, j] = (char)((_rotors[i, j] - 'A' - nizInt[i] + 26) % 26 + 'A');
                }
                for (int k = 0; k < 26; k++)
                {
                    pomocni[k] = _rotors[i, k];
                }
                rotateLeft(ref pomocni, nizInt[i]);
                for (int l = 0; l < 26; l++)
                {
                    _rotors[i, l] = pomocni[l];
                }
            }

            char[] RingSettingArray = ringSetting.ToCharArray();
            int[] nizIntRing = new int[3]; //niz pomeraja za svaki od rotora
            for (int i = 0; i < 3; i++)
            {
                nizIntRing[i] = RingSettingArray[i] - 'A';
            }
            pomocni = new char[26];
            for (int i = 0; i < 3; i++)
            {
                if (nizIntRing[i] == 0)
                    continue;
                for (int j = 0; j < 26; j++)
                {
                    _rotors[i, j] = (char)((_rotors[i, j] - 'A' + nizIntRing[i]) % 26 + 'A');
                }
                for (int k = 0; k < 26; k++)
                {
                    pomocni[k] = _rotors[i, k];
                }
                rotateRight(ref pomocni, nizIntRing[i]);
                for (int l = 0; l < 26; l++)
                {
                    _rotors[i, l] = pomocni[l];
                }
            }

        }
        private void rotateLeft(ref char[] niz, int brojRotacija)
        {
            for (int i = 0; i < brojRotacija; i++)
            {
                char pom = niz[0];
                for (int j = 0; j < niz.Length - 1; j++)
                {
                    niz[j] = niz[j + 1];
                }
                niz[niz.Length - 1] = pom;
            }
        }

        private void rotateRight(ref char[] niz, int brojRotacija)
        {
            for (int i = 0; i < brojRotacija; i++)
            {
                char pom = niz[niz.Length - 1];
                for (int j = niz.Length - 1; j > 0; j--)
                {
                    niz[j] = niz[j - 1];
                }
                niz[0] = pom;
            }
        }

        private int nadjiSlovo(char a, char[] niz)
        {
            for (int i = 0; i < niz.Length; i++)
            {
                if (niz[i] == a)
                    return i;
            }
            return -1;
        }

        private char[] getRow(int row, char[,] niz)
        {
            char[] pom = new char[26];
            for (int i = 0; i < 26; i++)
            {
                pom[i] = niz[row, i];
            }
            return pom;
        }

        private void inkrementirajRotor()
        {
            char[] pom = this._initialState.ToCharArray();
            pom[2] = (char)(((pom[2] - 'A' + 1) % 26) + 'A');
            for (int i = 0; i < 26; i++)
            {
                _rotors[2, i] = (char)((_rotors[2, i] - 'A' - 1 + 26) % 26 + 'A');
            }
            char[] pomocni = new char[26];
            for (int k = 0; k < 26; k++)
            {
                pomocni[k] = _rotors[2, k];
            }
            rotateLeft(ref pomocni, 1);
            for (int i = 0; i < 26; i++)
            {
                _rotors[2, i] = pomocni[i];
            }

            if (pom[2] == 'W')
            {
                //pom[1] = (char)(((pom[1] - 'A' + 1) % 26 + 26) + 'A');
                pom[1] = (char)((pom[1] - 'A' + 1) % 26 + 'A');
                for (int i = 0; i < 26; i++)
                {
                    _rotors[1, i] = (char)((_rotors[1, i] - 'A' - 1 + 26) % 26 + 'A');
                }
                pomocni = new char[26];
                for (int k = 0; k < 26; k++)
                {
                    pomocni[k] = _rotors[1, k];
                }
                rotateLeft(ref pomocni, 1);
                for (int i = 0; i < 26; i++)
                {
                    _rotors[1, i] = pomocni[i];
                }
                if (pom[1] == 'F')
                {
                    pom[0] = (char)((pom[0] - 'A' + 1) % 26 + 'A');
                    for (int i = 0; i < 26; i++)
                    {
                        _rotors[0, i] = (char)((_rotors[0, i] - 'A' - 1 + 26) % 26 + 'A');
                    }
                    pomocni = new char[26];
                    for (int k = 0; k < 26; k++)
                    {
                        pomocni[k] = _rotors[0, k];
                    }
                    rotateLeft(ref pomocni, 1);
                    for (int i = 0; i < 26; i++)
                    {
                        _rotors[0, i] = pomocni[i];
                    }
                }
            }
            string s = new string(pom);
            this._initialState = s;

        }

        public string Encrypt(String s)
        {
            s = s.ToUpper();
            if(this.plugboard != null)
            this.transform(ref s);
            char[] niz = s.ToCharArray();
            char izlazIzRotora3, izlazIzRotora2, izlazIzRotora1, izlazIzReflektora;
            char[] pom = new char[niz.Length];
            int index;
            for (int i = 0; i < niz.Length; i++)
            {
                if (niz[i] < 65 || (niz[i] > 90 && niz[i] < 97) || niz[i] > 122)
                {
                    pom[i] = ' ';
                    
                    continue;
                }
                inkrementirajRotor();
                izlazIzRotora3 = _rotors[2, niz[i] - 'A'];
                izlazIzRotora2 = _rotors[1, izlazIzRotora3 - 'A'];
                izlazIzRotora1 = _rotors[0, izlazIzRotora2 - 'A'];
                izlazIzReflektora = _reflector[izlazIzRotora1 - 'A'];

                index = nadjiSlovo(izlazIzReflektora, getRow(0, _rotors));
                izlazIzRotora1 = (char)('A' + index);

                index = nadjiSlovo(izlazIzRotora1, getRow(1, _rotors));
                izlazIzRotora2 = (char)('A' + index);

                index = nadjiSlovo(izlazIzRotora2, getRow(2, _rotors));
                izlazIzRotora3 = (char)('A' + index);
                pom[i] = izlazIzRotora3;
            }
            string enkriptovaniString = new string(pom);
            if (this.plugboard != null)
                this.transform(ref enkriptovaniString);
            return enkriptovaniString;
        }

        private void transform(ref string s)
        {
            char[] niz = s.ToCharArray();
            string ss = join(this.plugboard);
            int ind;
            for(int i=0; i<niz.Length; i++)
            {
                if(ss.Contains(niz[i]))
                {
                    ind = ss.IndexOf(niz[i]);
                    if(ind!=niz.Length-1 && ss[ind+1] == ' ')
                    {
                        niz[i] = ss[ind - 1];
                    }
                    else
                    {
                        if(ss[ind+1] != ' ')
                        {
                            niz[i] = ss[ind + 1];
                        }
                    }
                }
            }
            s = new string(niz);
        }
        private string join(string[] ss)
        {
            string s="";
            foreach(string a in ss)
            {
                s += a;
                s += " ";
            }
            return s;
        }
    }
}
