using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TVP_PRVI_PROJEKAT
{
    public partial class frmPrijava : Form
    {
        frmPocetna poc;
        frmPrijava f;
        List<Korisnik> Korisnici;
        List<Administrator> Admini;
        List<Kupac> Kupci;
        FileStream fajl;
        StreamReader sreader;
        string putanja;
        public frmPrijava() : base()
        {
            InitializeComponent();
            try
            {
                putanja = "Kupac.txt";
                poc = new frmPocetna();
                Inicijalizuj_sve_liste();
                Korisnici = Korisnik.Procitaj_korisnike(sreader);
                Inicijalizuj_sve_liste();
                Admini = Administrator.Procitaj_Admine(sreader);
                Inicijalizuj_sve_liste();
                Kupci = Kupac.Procitaj_Kupce(sreader);
                fajl.Close();
                sreader.Close();
            }
            catch(Exception ex)
            {
                MessageBox.Show("" + ex.Message, "грешка", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1);
            }
        }
        void Inicijalizuj_sve_liste()
        {
                fajl = new FileStream(putanja, FileMode.Open);
                sreader = new StreamReader(fajl);
        }
        public frmPrijava(string Admin) : this()
        {
            if ("Admin" == Admin)
            {
                label7.Visible = linkLabel1.Visible = false;
                Text = "Пријава администратор";
                try
                {
                    fajl.Close();

                    fajl.Close();
                }
                catch (Exception ew)
                {
                    MessageBox.Show(ew.Message);

                }
            }
        }


        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Close();
            podesi();

        }
        public void podesi()
        {


            f = new frmPrijava();
            f.Show();
            f.Text = "Регистрација";
            f.button1.Text = "Региструј се!";
            f.pictureBox1.Image = Properties.Resources.kontakt;
            f.label7.Visible = f.linkLabel1.Visible = false;
            f.tbIme.Visible = f.tbPrezime.Visible = f.tbJMBG.Visible = f.tbDatumRodj.Visible = f.tbTelefon.Visible = true;
            f.label1.Visible = f.label2.Visible = f.label3.Visible = f.label4.Visible = f.label5.Visible = f.label6.Visible = true;
            f.label8.Visible = f.label9.Visible = false;
            f.tbKorisnickoIme.Visible = f.tbLozinka.Visible = false;
        }
    
            
             
        
        private void button1_Click(object sender, EventArgs e)
        {
            string ime="", prezime="";
            if (button1.Text.Contains("Пријави се") && Text.Contains("Пријава администратор") )
            {
                bool postoji = true;
                foreach (Administrator A in Admini)
                {
                    if (tbKorisnickoIme.Text.Contains(A.Kor_ime_administratora)&&tbLozinka.Text.Contains(A.Sifra_adminstratora) )
                    {
                        postoji = false; prezime = A.Prezime;
                        ime = A.Ime;
                    }
                } tbKorisnickoIme.Text =  tbLozinka.Text = "";
                  if(postoji)
                    {
                    MessageBox.Show("Нетачна лозинка или корисничко име покушајте поново!", "Упозорење", MessageBoxButtons.OK, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button3);
                    }
                else if(!postoji)
                {
                    MessageBox.Show(ime + " " + prezime + "\n" + "Успешно сте се улоговали на информациони систем!", "Добродошли", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
                    Close();
                    frmAdministrator frmadmin = new frmAdministrator( ime,prezime);
                    frmadmin.Show();
                }
            }
            else if(!(button1.Text.Contains("Пријави се") && Text.Contains("Пријава")))
            {
                try
                {
                    fajl.Close();
                    Korisnik Novi_korisnik = new Korisnik((Korisnici.Count+1), tbIme.Text, tbPrezime.Text, tbJMBG.Text,tbDatumRodj.Text, tbTelefon.Text);
                    fajl = new FileStream(putanja, FileMode.Append);
                    StreamWriter w = new StreamWriter(fajl);
                    int broj_upisanih = Korisnik.UpsiNovogKorisnika(w, Novi_korisnik, Korisnici);fajl.Close();
                    if (broj_upisanih > 0) { MessageBox.Show("Успешно сте се пријавили на информациони систем за издавање возила!\n", "Обавештење", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1); Close();}
                    else { MessageBox.Show("Безуспешно пријављивање на информациони систем \n корисник са датим именом презименом и матичним бројем постоји у систему!", "Упозорење!", MessageBoxButtons.OK, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button1);
                        tbDatumRodj.Text = tbIme.Text = tbJMBG.Text = tbPrezime.Text = tbTelefon.Text = tbDatumRodj.Text = "";
                        
                    }
                }
               catch(Exception ex)
               {
                    MessageBox.Show(""+ex.Message, "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1);
                }
             }
            else if((button1.Text.Contains("Пријави се") && Text.Contains("Пријава")))
            {
                bool postoji=true;
                string ime_kupca ="",prezime_kupca="",id_kupca="";
                foreach(Kupac k in Kupci)
                {
                    if(tbKorisnickoIme.Text.Contains(k.Kor_ime_kupca)&& tbLozinka.Text.Contains(k.Sifra_kupca))
                    {
                        ime_kupca = k.Ime;
                        prezime_kupca = k.Prezime;
                        id_kupca = k.Id_korisnik+"";
                        postoji = false;
                    }
                    tbKorisnickoIme.Text = tbLozinka.Text = "";
                }
                if(postoji)
                {
                    MessageBox.Show("Нетачна лозинка или корисничко име покушајте поново!", "Упозорење", MessageBoxButtons.OK, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button3);
                }
                else
                {
                    MessageBox.Show(ime_kupca + " " + prezime_kupca + "\n" + "Успешно сте се улоговали на информациони систем!", "Добродошли", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
                    Close();
                    frmKorisnik frm_korisnik_kupac = new frmKorisnik(ime_kupca, prezime_kupca,id_kupca);
                    frm_korisnik_kupac.Show();
                }
            }
        }

        private void frmPrijava_Load(object sender, EventArgs e)
        {

        }
    }
}
