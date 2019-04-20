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
    public partial class FrmRezervacija : Form
    {
        List<Korisnik> Kupci;
        List<Automobil> Automobili;
        List<Rezervacija> Rezervacije;
        List<Ponuda> Ponude;
        FileStream fajl;
        StreamReader sreader;
        DateTime D_od, D_do;
        string putanja;
        int i, ID, cena;
        public FrmRezervacija()
        {
            InitializeComponent();
            try
            {
                putanja = "Rezervacija.txt";
                Osvezi();
                formiraj("Automobil.txt");
                Automobili = Automobil.Procitaj_Automobil(sreader);
                dataGridView3.DataSource = Automobili;
                foreach (Automobil A in Automobili)
                {
                    cbAutomobil.Items.Add(A.Id_auto + "-" + A.Marka + " " + A.Model);
                }
                formiraj("Kupac.txt");
                Kupci = Korisnik.Procitaj_korisnike(sreader);
                foreach (Korisnik k in Kupci)
                {
                    cbKupac.Items.Add(k.Id_korisnik + "-" + k.Ime + " " + k.Prezime);
                }
                formiraj("Ponuda.txt");
                Ponude = Ponuda.Procitaj_Ponude(sreader);
                fajl.Close(); i = Rezervacije.Count;
            }
            catch (Exception ex)
            {
                MessageBox.Show("" + ex.Message, "грешка", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1);
            }
        }
        void formiraj(string putanja)
        {
            fajl = new FileStream(putanja, FileMode.Open);
            sreader = new StreamReader(fajl);
        }
        void Osvezi()
        {
            formiraj(putanja);
            Rezervacije = Rezervacija.Procitaj_Rezervacije(sreader);
        }
        private void FrmRezervacija_Load(object sender, EventArgs e)
        {

        }
        int Rukujcenom()
        {
            D_od = Convert.ToDateTime(monthOD.SelectionStart.ToString("MM/dd/yyyy"));
            D_do = Convert.ToDateTime(monthDo.SelectionStart.ToString("MM/dd/yyyy"));
            try
            {
                ID = Convert.ToInt32(cbAutomobil.Text.Split('-')[0]);
            }
            catch
            {

            }
            foreach (Rezervacija Rez in Rezervacije)
            {
                if (!(D_od < Rez.Datum_od && D_do < Rez.Datum_od) && !(Rez.Datum_do < D_od && Rez.Datum_do < D_do) && (Rez.Datum_od < Rez.Datum_do) && (D_od < D_do) && Rez.Id_kupac + "" == cbKupac.Text.Split('-')[0] && Rez.Id_automobil + "" == cbAutomobil.Text.Split('-')[0])
                {
                    return -1;
                }
            }
            if (cbAutomobil.Text.Length > 0 && cbKupac.Text.Length > 0 && ispitaj_datume())
            {
                cena = Rezervacija.Racunaj_cenu(ID, D_od, D_do, Ponude);
                if (cena < 0) { MessageBox.Show("Нема понуде о изнајмљивању кола у датом опсегу!", "Обавештење", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1); return -1; }
                txtCena.Text = cena.ToString();
            }
            else
            {
                MessageBox.Show("Обавезно попунити сва поља!\n Поведите рачуна о датуму!\n Датум од никада не може бити већи од датума до,као ни датум до мањи од датума од!", "Обавештење", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1); return -1;
            }
            return 1;
        }
        private void button1_Click(object sender, EventArgs e)
        {
            int ishod = Rukujcenom();
            try
            {
                Rezervacija Nova_rezervacija = new Rezervacija(Convert.ToInt32(cbAutomobil.Text.Split('-')[0]), Convert.ToInt32(cbKupac.Text.Split('-')[0]), Convert.ToDateTime(D_od.ToString("MM/dd/yyyy")), Convert.ToDateTime(D_do.ToString("MM/dd/yyyy")), cena);
                fajl = new FileStream(putanja, FileMode.Append);
                StreamWriter w = new StreamWriter(fajl, Encoding.UTF8);
                int broj_upisanih = 0;
                if (ishod > 0)
                { broj_upisanih = Rezervacija.Rezervisi(w, Rezervacije, Nova_rezervacija); w.Close(); fajl.Close(); }
                else
                    MessageBox.Show("Безуспешно уписивање резервације на информациони систем \n не можете имати више резервација у овом опсегу за једног корисника!", "Упозорење!", MessageBoxButtons.OK, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button1);
                if (broj_upisanih > 0)
                {
                    MessageBox.Show("Успешно сте унели нову резервацију у информациони систем за издавање возила!\n", "Обавештење", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
                    brisi_polja();
                }

                else if (broj_upisanih == -1)
                {
                    MessageBox.Show("Безуспешно уписивање резервације на информациони систем \n резервација датим ID_ем,термином и возилом постоји у бази података!", "Упозорење!", MessageBoxButtons.OK, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button1);
                    brisi_polja();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("" + ex.Message, "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1);
            }
            sreader.Close();
            fajl.Close();
            Osvezi();
        }
        void brisi_polja()
        {
            cbAutomobil.Text = cbKupac.Text = txtCena.Text = "";
        }
        private bool ispitaj_datume()
        {
            if (D_od > D_do) return false;
            else return true;
        }
        private void cbPonuda_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                txtCena.Clear();
                List<Ponuda> SELEKTOVAN = new List<Ponuda>();
                foreach (Ponuda p in Ponude)
                {
                    if (cbAutomobil.Text[0] + "" == p.Id_automobila + "")
                    {
                        SELEKTOVAN.Add(p);
                    }
                }
                dataGridView1.DataSource = SELEKTOVAN;
                List<Rezervacija> SELEKTOVAN2 = new List<Rezervacija>();
                foreach (Rezervacija R in Rezervacije)
                {
                    if ((cbKupac.Text[0] + "" == R.Id_kupac + "") && cbAutomobil.Text[0] + "" == R.Id_automobil + "")
                    {
                        SELEKTOVAN2.Add(R);
                    }
                }
                foreach (Rezervacija REZ in SELEKTOVAN2)
                {
                    txtCena.Text = "" + REZ.Cena;
                }
            }
            catch (Exception xe)
            {

            }
        }
        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                if (txtCena.Text.Length > 0 && cbAutomobil.Text.Length > 0 && cbKupac.Text.Length > 0)
                {
                    int br = Rezervacija.Brisi_Rezervaciju(cbAutomobil.Text.Split('-')[0], cbKupac.Text.Split('-')[0], putanja);
                    if (br > 0)
                    {
                        MessageBox.Show("Успешно обрисан запис!", "Информација", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
                    }
                    brisi_polja();
                    Osvezi();
                }
                else
                {
                    MessageBox.Show("Бришете непостојећу резервацију!\n", "Обавештење", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
                }


            }
            catch (Exception ex)
            {

            }
        }
        private void button3_Click(object sender, EventArgs e)
        {
            try
            {
                Rukujcenom();
                if (cbAutomobil.Text.Length > 0 && cbKupac.Text.Length > 0 && ispitaj_datume())
                {
                    int br_izmenjenih = Rezervacija.izmeni(putanja, ID + "", cbKupac.Text.Split('-')[0], D_od + "", D_do + "", cena + "");
                    if (br_izmenjenih > 0)
                    {
                        MessageBox.Show("Успешно измењен запис!", "Информација", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
                    }
                    brisi_polja();
                    Osvezi();
                }
                else
                {
                    MessageBox.Show("Безуспешно измењен запис!\n", "Обавештење", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
                }

            }
            catch (Exception ex)
            {

            }
        }
        private void cbKupac_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                List<Rezervacija> SELEKTOVAN = new List<Rezervacija>();
                foreach (Rezervacija R in Rezervacije)
                {
                    if (cbKupac.Text[0] + "" == R.Id_kupac + "")
                    {
                        SELEKTOVAN.Add(R);
                    }
                }
                dataGridView2.DataSource = SELEKTOVAN;

            }
            catch (Exception xe)
            {

            }
        }
    }
}
