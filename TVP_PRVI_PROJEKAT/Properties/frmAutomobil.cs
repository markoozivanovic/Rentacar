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

namespace TVP_PRVI_PROJEKAT
{
    public partial class frmAutomobil : Form
    {
        string putanja;
        FileStream fajl;
        StreamReader sreader;
        List<Automobil> Automobili;
        int i;
        public frmAutomobil()
        {
            InitializeComponent();
            try
            {
                Osvezi();
                fajl.Close(); 
                toolTip1.SetToolTip(txtKubikaza, "Уноси се бројчана вредост за кубикажу,без ознака!");
                txtID.Text = "1";
                button1.Enabled = false;
            }
            catch (Exception ex)
            {
                MessageBox.Show("" + ex.Message, "грешка", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1);
            }

        }
        void brisi_polja()
        {
            txtID.Text = txtMarka.Text = txtModel.Text = txtGodina.Text = txtKubikaza.Text = txtKaroserija.Text=cbGorivo.Text=cbMenjac.Text=cbPogon.Text=cbVrata.Text="";
        }
        public void Osvezi()
        {
            putanja = "Automobil.txt";
            fajl = new FileStream(putanja, FileMode.Open);
            sreader = new StreamReader(fajl);
            Automobili = Automobil.Procitaj_Automobil(sreader);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (txtMarka.Text.Length > 0 && txtModel.Text.Length > 0 && txtGodina.Text.Length > 0 && txtKubikaza.Text.Length > 0 && cbPogon.Text.Length > 0 && cbMenjac.Text.Length > 0 && txtKaroserija.Text.Length > 0 && cbGorivo.Text.Length > 0 && cbVrata.Text.Length > 0)
            {
                int br = Automobil.Brisi_Automobil(int.Parse(txtID.Text), putanja);
                if (br > 0)
                {
                    MessageBox.Show("Успешно обрисан запис!", "Информација", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
                }
                brisi_polja();
                Osvezi();
            }
            else
            {
                MessageBox.Show("Обавезно попунити сва поља!\n", "Обавештење", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
            }
        }

        private void button6_Click(object sender, EventArgs e)
        {
            button1.Enabled = true;
            txtID.Enabled = false;
            brisi_polja();
            button2.Enabled = button3.Enabled = button4.Enabled = button5.Enabled = false;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            txtID.Enabled = true;
            button2.Enabled = button3.Enabled = button4.Enabled = button5.Enabled = true; button1.Enabled = false;
            if (txtMarka.Text.Length > 0 && txtModel.Text.Length > 0 && txtGodina.Text.Length > 0 && txtKubikaza.Text.Length > 0 && cbPogon.Text.Length > 0 && cbMenjac.Text.Length > 0 && txtKaroserija.Text.Length > 0 && cbGorivo.Text.Length > 0 && cbVrata.Text.Length > 0)
            {
                try
                {

                    Automobil Novi_auto = new Automobil((Automobili.Count+1), txtMarka.Text,txtModel.Text, Convert.ToInt32(txtGodina.Text),Convert.ToInt32(txtKubikaza.Text),  cbPogon.Text, cbMenjac.Text, txtKaroserija.Text, cbGorivo.Text,Convert.ToInt32(cbVrata.Text));
                    fajl = new FileStream(putanja, FileMode.Append);
                    StreamWriter w = new StreamWriter(fajl, Encoding.UTF8);
                    int broj_upisanih = Automobil.UpsiNovogAutomobila(w, Novi_auto,Automobili); w.Close(); fajl.Close();
                    if (broj_upisanih > 0)
                    {
                        MessageBox.Show("Успешно сте унели нов аутомобил у информациони систем за издавање возила!\n", "Обавештење", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
                        brisi_polja();
                    }
                    else if (broj_upisanih == 1)
                    {
                        MessageBox.Show("Безуспешно уношење аутомобила на информациони систем \n аутомобил тог модела,годишта,горива и марке постоји у систему!", "Упозорење!", MessageBoxButtons.OK, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button1);
                        brisi_polja();

                    }
                    else
                    {
                        MessageBox.Show("Безуспешно уписивање аутомобила на информациони систем \n аутомобил са датим ID_ем постоји у бази података!", "Упозорење!", MessageBoxButtons.OK, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button1);
                        brisi_polja();
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("" + ex.Message, "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1);
                }
            }
            else
            {
                MessageBox.Show("Обавезно попунити сва поља!\n", "Обавештење", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
            }
            sreader.Close();
            fajl.Close();
            Osvezi();

            brisi_polja();
        }

        private void frmAutomobil_Load(object sender, EventArgs e)
        {

        }

        private void txtID_TextChanged(object sender, EventArgs e)
        {
            List<Automobil> pretraga_automobil = new List<Automobil>();
            if (txtID.Text != "")
            {

                foreach (Automobil Auto in Automobili)
                {
                    if (Auto.Id_auto.ToString().Contains(txtID.Text))
                    {
                        pretraga_automobil.Add(Auto);
                    }
                       
                        
                   
                    else
                        txtMarka.Text = txtModel.Text = txtGodina.Text = txtKubikaza.Text = txtKaroserija.Text = cbGorivo.Text = cbMenjac.Text = cbPogon.Text = cbVrata.Text = "";

                }
                foreach (Automobil p in pretraga_automobil)
                {
                    txtID.Text = p.Id_auto + "";
                    txtMarka.Text = p.Marka;
                    txtModel.Text = p.Model;
                    txtGodina.Text = p.Godiste + "";
                    txtKubikaza.Text = p.Kubikaza + "";
                    txtKaroserija.Text = p.Karoserija;
                    cbGorivo.Text = p.Gorivo;
                    cbMenjac.Text = p.Vrsta_menjaca;
                    cbPogon.Text = p.Pogon;
                    cbVrata.Text = p.Br_vrata + "";
                }
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (txtMarka.Text.Length > 0 && txtModel.Text.Length > 0 && txtGodina.Text.Length > 0 && txtKubikaza.Text.Length > 0 && cbPogon.Text.Length > 0 && cbMenjac.Text.Length > 0 && txtKaroserija.Text.Length > 0 && cbGorivo.Text.Length > 0 && cbVrata.Text.Length > 0)
            {
                int br_izmenjenih = Automobil.izmeni(putanja, txtID.Text, txtMarka.Text, txtModel.Text, txtGodina.Text, txtKubikaza.Text,cbPogon.Text,cbMenjac.Text,txtKaroserija.Text, cbGorivo.Text, cbVrata.Text);
                if (br_izmenjenih > 0)
                {
                    MessageBox.Show("Успешно измењен запис!", "Информација", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
                }
                brisi_polja();
                Osvezi();
            }
            else
            {
                MessageBox.Show("Обавезно попунити сва поља!\n", "Обавештење", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
            }
        }
        void Prethodni_sledeci(int i)
        {
                txtID.Text = Automobili[i].Id_auto + "";
                txtMarka.Text = Automobili[i].Marka;
                txtModel.Text = Automobili[i].Model;
                txtGodina.Text = Automobili[i].Godiste + "";
                txtKubikaza.Text = Automobili[i].Kubikaza + "";
                txtKaroserija.Text = Automobili[i].Karoserija;
                cbGorivo.Text = Automobili[i].Gorivo;
                cbMenjac.Text = Automobili[i].Vrsta_menjaca;
                cbPogon.Text = Automobili[i].Pogon;
                cbVrata.Text = Automobili[i].Br_vrata + "";
        }
        private void button4_Click(object sender, EventArgs e)
        {
            i -= 1;
            if (i >= 0)
            {
                Prethodni_sledeci(i);

            }
            else
            {
                MessageBox.Show("Нема претходника!", "Информација", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button2);
                brisi_polja();
                i = -1;
            }
          
        }

        private void button5_Click(object sender, EventArgs e)
        {
            i++;

            if (i < Automobili.Count)
            {
                Prethodni_sledeci(i);
            }
            else
            {
                MessageBox.Show("Нема следбеника!", "Информација", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button2);
                i = Automobili.Count;
                brisi_polja();
            }
        }

        private void frmAutomobil_Leave(object sender, EventArgs e)
        {

        }
    }
}
