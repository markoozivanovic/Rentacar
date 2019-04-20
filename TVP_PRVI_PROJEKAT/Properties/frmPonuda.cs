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
    public partial class frmPonuda : Form
    {
        List<Automobil> Automobili;
        List<Ponuda> Ponude;
        int i;
        FileStream fajl;
        StreamReader sreader;
        string putanja;
        public frmPonuda()
        {
            InitializeComponent();
            try
            {
                putanja = "Ponuda.txt";
                Osvezi();
                fajl = new FileStream("Automobil.txt", FileMode.Open);
                sreader = new StreamReader(fajl);
                Automobili = Automobil.Procitaj_Automobil(sreader);
                foreach (Automobil auto in Automobili)
                {
                    cbID_IMEAuta.Items.Add(auto.Id_auto + "-" + auto.Marka + " " + auto.Model);
                }
                fajl.Close(); i = 0;


            }
            catch (Exception ex)
            {
                MessageBox.Show("" + ex.Message, "грешка", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1);
            }
        }
        void Osvezi()
        {
            listBox1.Items.Clear();
            fajl = new FileStream(putanja, FileMode.Open);
            sreader = new StreamReader(fajl);
            Ponude = Ponuda.Procitaj_Ponude(sreader);
            int i = 0;
            while (i < Ponude.Count)
            {
                listBox1.Items.Add(Ponude[i].Id_automobila + "\t" + Ponude[i].Datum_od.ToString().Split(' ')[0] + "\t\t" + Ponude[i].Datum_do.ToString().Split(' ')[0] + "\t\t" + Ponude[i].Cena_po_danu);
                i++;
            }
        }

        private void cbID_IMEAuta_SelectedIndexChanged(object sender, EventArgs e)
        {
            List<Automobil> SELEKTOVAN = new List<Automobil>();
            foreach (Automobil auto in Automobili)
            {
                if (cbID_IMEAuta.Text[0] + "" == auto.Id_auto + "")
                {

                    SELEKTOVAN.Add(auto);
                }
            }
            dataGridView1.DataSource = SELEKTOVAN;

        }
        protected bool Poredi_datume()
        {
            DateTime datum_od = Picdatumod.Value;
            DateTime datum_do = Picdatum_do.Value;
            if (datum_od > datum_do)
            {
                return false;
            }
            else
                return true;
        }
        private void button1_Click(object sender, EventArgs e)
        {


            if (cbID_IMEAuta.Text.Length > 0 && txtCenaPoDanu.Text.Length > 0 && Poredi_datume())
            {
                try
                {

                    Ponuda Nova_ponuda = new Ponuda(Convert.ToInt32(cbID_IMEAuta.Text.Split('-')[0]), Convert.ToDateTime(Picdatumod.Value.ToString("MM/dd/yyyy")), Convert.ToDateTime(Picdatum_do.Value.ToString("MM/dd/yyyy")),Convert.ToInt32(txtCenaPoDanu.Text));
                    fajl = new FileStream(putanja, FileMode.Append);
                    StreamWriter w = new StreamWriter(fajl, Encoding.UTF8);
                    int broj_upisanih = Ponuda.NovaPonuda(w, Nova_ponuda, Ponude, Convert.ToDateTime(Picdatumod.Value.ToString("MM/dd/yyyy")), Convert.ToDateTime(Picdatum_do.Value.ToString("MM/dd/yyyy"))); w.Close(); fajl.Close();
                    if (broj_upisanih > 0)
                    {
                        MessageBox.Show("Успешно сте унели нову понуду у информациони систем за издавање возила!\n", "Обавештење", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
                        brisi_polja();
                    }
                   
                    else if(broj_upisanih<=0)
                    {
                        MessageBox.Show("Безуспешно уписивање понуде у информациони систем,могуће да постоји слична или иста понуда за дати аутомобил !", "Упозорење!", MessageBoxButtons.OK, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button1);
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
                MessageBox.Show("Обавезно попунити сва поља!\n Поведите рачуна о датуму!\n Датум од никада не може бити већи од датума до,као ни датум до мањи од датума од!", "Обавештење", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
            }
            sreader.Close();
            fajl.Close();
            Osvezi();
            brisi_polja();
        }
        private void brisi_polja()
        {
            cbID_IMEAuta.Text = Picdatumod.Text = Picdatum_do.Text = txtCenaPoDanu.Text = "";
        }

        private void frmPonuda_Load(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Да би укинули понуду потребно је означити ред у листи понуда", "Информација", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);

          }
        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (MessageBox.Show("Желите да обришете запис?", "Информација", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1) == DialogResult.Yes)
                {
                    int br = Ponuda.Brisi_Ponudu(listBox1.SelectedIndex, putanja);
                    if (br > 0)
                    {
                        MessageBox.Show("Успешно обрисан запис!", "Информација", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
                        brisi_polja();
                    }
                  
                    Osvezi();return;
                } 
                if (MessageBox.Show("Желите да  измените запис?", "Информација", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1) == DialogResult.Yes)
                {
                    int br_izmenjenih = Ponuda.Izmeni(putanja,Convert.ToInt32(cbID_IMEAuta.Text.Split('-')[0]), Convert.ToDateTime(Picdatumod.Value.ToString("MM/dd/yyyy")), Convert.ToDateTime(Picdatum_do.Value.ToString("MM/dd/yyyy")), Convert.ToInt32(txtCenaPoDanu.Text),Ponude);
                    if (br_izmenjenih > 0)
                    {
                        MessageBox.Show("Успешно измењенa цена!", "Информација", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
                        brisi_polja();
                        Osvezi();
                    }
                    else
                    {
                        MessageBox.Show("Неуспешно измењена цена!", "Информација", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
                    }
                   
                }
            }
            catch
            {
                MessageBox.Show("Kaда желите нешто да измените морате пре тога да попуните поља за унос!", "Пажња", MessageBoxButtons.OK, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button1);
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Да би изменули понуду потребно је означити ред у листи понуда", "Информација", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);

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
            if (i < Ponude.Count)
            {
                Prethodni_sledeci(i);
              
            }
            else
            {
                MessageBox.Show("Нема следбеника!", "Информација", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button2);
                i = Ponude.Count;
                brisi_polja();
            }
        }

        private void Prethodni_sledeci(int i)
        {
          
               cbID_IMEAuta.Text = Ponude[i].Id_automobila.ToString();
               Picdatumod.Text = Ponude[i].Datum_od+"";
               Picdatum_do.Text = Ponude[i].Datum_do+"";
               txtCenaPoDanu.Text = Ponude[i].Cena_po_danu+"";
        }
    }
    }
        
    

