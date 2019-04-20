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
    public partial class frmRezervacijaKorisnik : Form
    {
        List<Automobil> Automobili;
        List<Ponuda> Ponude;
        List<Rezervacija> Rezervacije;
        StreamReader sreader;
        FileStream fajl;
        DateTime D_od, D_do;
        string putanja,id_kupca,id_automobila;
        int cena;
        public frmRezervacijaKorisnik():base()
        {
            InitializeComponent();
            try
            {
                putanja = "Automobil.txt";
                fajl = new FileStream(putanja, FileMode.Open);
                sreader = new StreamReader(fajl);
                Automobili = Automobil.Procitaj_Automobil(sreader);
                fajl.Close();
                sreader.Close();
                foreach (Automobil MarkaAuta in Automobili)
                {
                    if (!cbMarka.Items.Contains(MarkaAuta.Marka))
                    {
                        cbMarka.Items.Add(MarkaAuta.Marka);
                    }
                }
                fajl = new FileStream("Ponuda.txt", FileMode.Open);
                sreader = new StreamReader(fajl);
                Ponude = Ponuda.Procitaj_Ponude(sreader);
                fajl.Close();
                button2.Enabled = false;
            }
            catch (Exception ex)
            {
                MessageBox.Show("" + ex.Message, "грешка", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1);
            }

        }
        public frmRezervacijaKorisnik(string id_kupca) : this()
        {
            this.id_kupca = id_kupca;
        }
        private void frmRezervacijaKorisnik_Load(object sender, EventArgs e)
        {

        }

        private void cbMarka_SelectedIndexChanged(object sender, EventArgs e)
        {
           
            cbModel.Items.Clear();cbModel.Text = ""; button2.Enabled = false;
            cbGodiste.Items.Clear(); cbGodiste.Text = "";
            foreach (Automobil ModelAuta in Automobili)
            {
                if (ModelAuta.Marka.Contains(cbMarka.SelectedItem.ToString()))
                {
                    if (!cbModel.Items.Contains(ModelAuta.Model))
                    {
                        
                        cbModel.Items.Add(ModelAuta.Model);
                    }
                }
            }
        }
        void brisi_polja()
        {
            cbKubikaza.Text = cbKaroserija.Text = cbVrata.Text = cbGorivo.Text = cbPogon.Text = cbMenjac.Text =txtUkupnaCena.Text= "";
        }
        private void cbGodiste_SelectedIndexChanged(object sender, EventArgs e)
        {
            brisi_polja(); button2.Enabled = false;
            foreach (Automobil Auto in Automobili)
            {
                if (Auto.Godiste.ToString().Contains(cbGodiste.SelectedItem.ToString()))
                {
                    cbKubikaza.Text=Auto.Kubikaza+"";
                    cbKaroserija.Text=Auto.Karoserija;
                    cbVrata.Text=Auto.Br_vrata+"";
                    cbGorivo.Text=Auto.Gorivo;
                    cbPogon.Text=Auto.Pogon;
                    cbMenjac.Text=Auto.Vrsta_menjaca;
                }
            }
        }

        private void cbModel_SelectedIndexChanged(object sender, EventArgs e)
        {
             cbGodiste.Items.Clear(); cbGodiste.Text = ""; button2.Enabled = false;
            foreach (Automobil GodisteAuta in Automobili)
            {
                if (GodisteAuta.Model.Contains(cbModel.SelectedItem.ToString()))
                {
                    if (!cbGodiste.Items.Contains(GodisteAuta.Godiste))
                    {

                        cbGodiste.Items.Add(GodisteAuta.Godiste);
                    }
                }
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
                    txtRezervacije.Clear();
                    foreach(Automobil Auto in Automobili)
                    foreach(Ponuda Ponuda_izlistaj in Ponude)
                    {
                    if (cbMarka.Text.Trim() == Auto.Marka && cbModel.Text.Trim() == Auto.Model)
                    {
                        if (Auto.Id_auto == Ponuda_izlistaj.Id_automobila)
                        {
                            txtRezervacije.Text = Ponuda_izlistaj.Datum_od.ToString("dd.MM.yyyy.").Split(' ')[0] + "-" + Ponuda_izlistaj.Datum_do.ToString("dd.MM.yyyy.").Split(' ')[0] + " Цена: " + Ponuda_izlistaj.Cena_po_danu + "дин по дану";
                            id_automobila = Auto.Id_auto + ""; button2.Enabled = false;
                        }
                        else
                            button2.Enabled = true;;
                    }
                     }
            

        }
        private bool Ispitaj_datume()
        {
            if (D_od > D_do) return false;
            else return true;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            D_od = Convert.ToDateTime(picDatumod.Value.ToString("MM/dd/yyyy"));
            D_do = Convert.ToDateTime(picDatumdo.Value.ToString("MM/dd/yyyy"));
            try
            {
                Osvezi();int ishod = Rukujcenom();
                Rezervacija Nova_rezervacija = new Rezervacija(Convert.ToInt32(id_automobila), Convert.ToInt32(id_kupca), Convert.ToDateTime(D_od.ToString("MM/dd/yyyy")), Convert.ToDateTime(D_do.ToString("MM/dd/yyyy")), cena);
                fajl = new FileStream("Rezervacija.txt", FileMode.Append);
                StreamWriter w = new StreamWriter(fajl, Encoding.UTF8);
                int broj_upisanih = 0;
                
                if (ishod > 0)
                {
                    
                    broj_upisanih = Rezervacija.Rezervisi(w, Rezervacije, Nova_rezervacija); w.Close(); fajl.Close();
                }
                else
                {
                    MessageBox.Show("Безуспешно уписивање резервације на информациони систем \n не можете имати више резервација у овом опсегу!", "Упозорење!", MessageBoxButtons.OK, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button1);
                }
                    if (broj_upisanih > 0)
                    {  txtUkupnaCena.Text = cena.ToString();
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
            button2.Enabled = false;
            
        }
        void Osvezi()
        {
            fajl = new FileStream("Rezervacija.txt", FileMode.Open);
            sreader = new StreamReader(fajl);
            Rezervacije = Rezervacija.Procitaj_Rezervacije(sreader);
        }
        int Rukujcenom()
        {
           
            foreach (Rezervacija Rez in Rezervacije)
            {
                if (!(D_od < Rez.Datum_od && D_do < Rez.Datum_od) && !(Rez.Datum_do < D_od && Rez.Datum_do < D_do) && (Rez.Datum_od < Rez.Datum_do) && (D_od < D_do))
                {
                    return -1;
                }
            }
            if (cbMarka.Text.Trim().Length > 0 && cbGodiste.Text.Trim().Length > 0 && cbModel.Text.Trim().Length > 0 && Ispitaj_datume())
            {
                cena = Rezervacija.Racunaj_cenu(Convert.ToInt32(id_automobila), D_od, D_do, Ponude);
                if (cena < 0) { MessageBox.Show("Нема понуде о изнајмљивању кола у датом опсегу!", "Обавештење", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1); return -1; }
            }
            else
            {
                MessageBox.Show("Обавезно попунити сва поља!\n Поведите рачуна о датуму!\n Датум од никада не може бити већи од датума до,као ни датум до мањи од датума од!", "Обавештење", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1); return -1;
            }
            return 1;
        }
    }
}
