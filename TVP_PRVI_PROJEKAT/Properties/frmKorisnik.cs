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
    public partial class frmKorisnik : Form
    {
        int tajmer = 60;
        List<Rezervacija> Rezervacije;
        string putanja, id_kupca;
        public frmKorisnik():base()
        {
            InitializeComponent();
            putanja = "Rezervacija.txt";
        }
        public frmKorisnik(string ime_kupca,string prezime_kupca,string id_korisnika):this()
        {
            tsime_prezime_odjava.Text = ime_kupca + " " + prezime_kupca + "(одјави се)";
            id_kupca = id_korisnika;
        }
        void Osvezi()
        {
            listBox1.Items.Clear();
            StreamReader citanje = new StreamReader(putanja);
            Rezervacije = Rezervacija.Procitaj_Rezervacije(citanje);
            foreach (Rezervacija rez in Rezervacije)
            {
                if (id_kupca.Contains(rez.Id_kupac.ToString()))
                {
                    listBox1.Items.Add(rez.Id_automobil + "|" + rez.Datum_od + "|" + rez.Datum_do + "|" + rez.Cena);
                }
            }
        }
        private void frmKorisnik_Load(object sender, EventArgs e)
        {
            try
            {
                Osvezi();
            }
            catch (Exception ex)
            {
                MessageBox.Show("" + ex.Message, "грешка", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1);
            }

        }

        private void излазToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void ts_vreme_Click(object sender, EventArgs e)
        {

        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            ts_vreme.Text="Трајања сесије:" + (tajmer--) + "s";
            if (tajmer < 1)
            {
                Close();
            }
        }

        private void frmKorisnik_Activated(object sender, EventArgs e)
        {
            timer1.Enabled = true;
        }

        private void frmKorisnik_Deactivate(object sender, EventArgs e)
        {
            timer1.Enabled = false;
        }

        private void frmKorisnik_Resize(object sender, EventArgs e)
        {
            if (WindowState == FormWindowState.Minimized)
                timer1.Enabled = true;
           
        }

        private void button1_Click(object sender, EventArgs e)
        {
            frmRezervacijaKorisnik frmrezkorisnik = new frmRezervacijaKorisnik(id_kupca);
            frmrezkorisnik.Show();
            frmrezkorisnik.FormClosed += OSVEZI;
        }

        private void OSVEZI(object sender, FormClosedEventArgs e)
        {
            Osvezi();
        }

        private void button2_Click(object sender, EventArgs e)
        {
                int br_redova = 0;
                FileStream f = new FileStream(putanja, FileMode.Open);
                StreamReader r = new StreamReader(f);
                string text = "", ostali = "";
                while (!r.EndOfStream)
                {
                    text = r.ReadLine();
                    if (listBox1.SelectedIndex!=br_redova)
                    {
                        ostali += (text + "\r\n");
                    }
                    br_redova++;
                }

                r.Close();
                f.Close();
                f = new FileStream(putanja, FileMode.Create);
                StreamWriter w = new StreamWriter(f);
                w.Write(ostali);
                w.Close();
                f.Close();
                Osvezi();
        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
                
           
        }
    }
}
