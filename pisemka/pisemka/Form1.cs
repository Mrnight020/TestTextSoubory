using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace pisemka
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                string jmeno = String.Empty;
                OpenFileDialog otevri = new OpenFileDialog();
                otevri.InitialDirectory = Path.GetDirectoryName(Application.ExecutablePath);
                if (otevri.ShowDialog() == DialogResult.OK)
                {
                    jmeno = otevri.FileName;
                }

                StreamReader cti = new StreamReader(jmeno);
                string mzda = String.Empty;
                string nejvetsimzda = String.Empty;
                int pocetzen = 0;
                double nejvetsi = 0;
                double prumernyvek = 0;
                int pocetprumernyvek = 0;

                while (!cti.EndOfStream)
                {
                    string radek = cti.ReadLine();
                    string[] pole = radek.Split(',');
                    double penize = double.Parse(pole[4]);
                    penize = CZK(penize);
                    

                    mzda = pole[0] + "," + pole[1] + "," + pole[2] + "," + pole[3] + "," + penize;

                    if (penize < 17300)
                    {
                        listBox2.Items.Add(mzda);
                    }
                    prumernyvek += double.Parse(pole[3]);
                    pocetprumernyvek++;

                    if (pole[2] == "Female")
                    {
                        pocetzen++;
                    }
                    if (penize > nejvetsi)
                    {
                        nejvetsi = penize;
                        nejvetsimzda = mzda;
                    }

                    listBox1.Items.Add(mzda);

                }
                cti.Close();

                StreamWriter zapis = new StreamWriter("best.txt");
                zapis.WriteLine(nejvetsimzda);
                double vysledek = prumernyvek / pocetprumernyvek;
                zapis.WriteLine("Prumerny vek : " + vysledek + "let");
                zapis.Close();

                MessageBox.Show("Vygenerovalo se :" + pocetzen + " žen");
            }
            catch
            {
                MessageBox.Show("Nekde se stala chyba");
            }       
        }

        public double CZK(double penizky)
        {
            return penizky * 22.33;
        }

    }
}
