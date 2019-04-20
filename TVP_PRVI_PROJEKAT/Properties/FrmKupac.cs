using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TVP_PRVI_PROJEKAT
{
    public partial class FrmKupac : Form
    {
        string putanja;
        FileStream fajl;
        StreamReader sreader;
        List<Korisnik> Korisnici;
        int i;
        public FrmKupac()
        {
            InitializeComponent();
            try
            {
                Osvezi();
                fajl.Close(); txtID.Text = "1";
               button1.Enabled = false; 
            }
            catch (Exception ex)
            {
                MessageBox.Show("" + ex.Message, "грешка", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1);
            }
        }
        void brisi_polja()
        {
            txtID.Text = txtIme.Text = txtPrezime.Text = txtMaticni.Text = tbDatumRodj.Text = tbTelefon.Text = "";
        }
        private void button1_Click(object sender, EventArgs e)
        {
            Regex rgxmaticni=new Regex("[0-9]{13}");

            txtID.Enabled = true;
            button2.Enabled = button3.Enabled = button4.Enabled = button5.Enabled = true; button1.Enabled = false;
            if ( txtIme.Text.Length>0 && txtPrezime.Text.Length>0 && tbDatumRodj.Text.Length > 0 && tbTelefon.Text.Length > 0 && rgxmaticni.IsMatch(txtMaticni.Text) )
            {
                try
                {

                    Korisnik Novi_korisnik = new Korisnik(Korisnici.Count+1, txtIme.Text, txtPrezime.Text, txtMaticni.Text, tbDatumRodj.Text, tbTelefon.Text);
                    fajl = new FileStream(putanja, FileMode.Append);
                    StreamWriter w = new StreamWriter(fajl,Encoding.UTF8);
                    int broj_upisanih = Korisnik.UpsiNovogKorisnika(w, Novi_korisnik, Korisnici); w.Close(); fajl.Close();
                    if (broj_upisanih > 0)
                    {
                        MessageBox.Show("Успешно сте унели купца у информациони систем за издавање возила!\n", "Обавештење", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
                        brisi_polja();
                    }
                    else if (broj_upisanih == 1)
                    {
                        MessageBox.Show("Безуспешно пријављивање на информациони систем \n корисник са датим именом презименом и матичним бројем постоји у систему!", "Упозорење!", MessageBoxButtons.OK, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button1);
                        brisi_polja();

                    }
                    else
                    {
                        MessageBox.Show("Безуспешно пријављивање на информациони систем \n корисник са датим ID_ем постоји у бази података!", "Упозорење!", MessageBoxButtons.OK, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button1);
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
                MessageBox.Show("Обавезно попунити сва поља водити рачуна о формату уноса!\n", "Обавештење", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
            }
            sreader.Close();
            fajl.Close();
            Osvezi();
           
            brisi_polja();
        }
        public void Osvezi()
        {
            putanja = "Kupac.txt";
            fajl = new FileStream(putanja, FileMode.Open);
            sreader = new StreamReader(fajl);
            Korisnici = Korisnik.Procitaj_korisnike(sreader);
        }
        private void FrmKupac_Load(object sender, EventArgs e)
        {

        }

        private void tbDatumRodj_MaskInputRejected(object sender, MaskInputRejectedEventArgs e)
        {

        }

        private void tbTelefon_MaskInputRejected(object sender, MaskInputRejectedEventArgs e)
        {

        }

        private void button4_Click(object sender, EventArgs e)
        {
            i-=1;
            if (i >=0)
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

        private void button3_Click(object sender, EventArgs e)
        {
            if (txtID.Text.Length > 0 && txtIme.Text.Length > 0 && txtPrezime.Text.Length > 0 && txtMaticni.Text.Length > 0 && tbDatumRodj.Text.Length > 0 && tbTelefon.Text.Length > 0)
            {
                int br_izmenjenih = Korisnik.izmeni(putanja,txtID.Text,txtIme.Text,txtPrezime.Text,txtMaticni.Text,tbDatumRodj.Text,tbTelefon.Text);
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
                txtID.Text = Korisnici[i].Id_korisnik.ToString();
                txtIme.Text = Korisnici[i].Ime;
                txtPrezime.Text = Korisnici[i].Prezime;
                txtMaticni.Text = Korisnici[i].Jmbg;
                tbDatumRodj.Text = Korisnici[i].Datum_rodjenja + "";
                tbTelefon.Text = Korisnici[i].Telefon;
        }
        private void txtID_TextChanged(object sender, EventArgs e)
        {
            List<Korisnik> pretraga_kupca = new List<Korisnik>();
            if (txtID.Text != "")
            {

                foreach (Korisnik Korisnik in Korisnici)
                {
                    if (Convert.ToInt32(txtID.Text) == Korisnik.Id_korisnik)
                    {
                        pretraga_kupca.Add(Korisnik);
                       
                    }
                        else
                        tbDatumRodj.Text = tbTelefon.Text = txtIme.Text = txtPrezime.Text = txtMaticni.Text = "";

                }
                foreach (Korisnik x in pretraga_kupca)
                {


                    txtID.Text = x.Id_korisnik.ToString();
                    txtIme.Text = x.Ime;
                    txtPrezime.Text = x.Prezime;
                    txtMaticni.Text = x.Jmbg;
                    tbDatumRodj.Text = x.Datum_rodjenja + "";
                    tbTelefon.Text = x.Telefon;
                }

            }

           
            
        }

        private void button5_Click(object sender, EventArgs e)
        {

               i++;
          
            if (i<Korisnici.Count)
            {
                Prethodni_sledeci(i);
            }
            else
            {
                MessageBox.Show("Нема следбеника!", "Информација", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button2);
                i =Korisnici.Count;
                brisi_polja();
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {

            if (txtID.Text.Length > 0 && txtIme.Text.Length > 0 && txtPrezime.Text.Length > 0 && txtMaticni.Text.Length > 0 && tbDatumRodj.Text.Length > 0 && tbTelefon.Text.Length > 0)
            {
                int br = Korisnik.Brisi_Korisnika(int.Parse(txtID.Text), putanja);
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
    }
}
