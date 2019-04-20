using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TVP_PRVI_PROJEKAT
{
    public partial class frmStatistika : Form
    {
        Graphics g;
        Bitmap b;
        List<Rezervacija> Rezervacije;
        List<Automobil> Automobili;
        List<Ponuda> Ponude;
        StreamReader r;
        string ID;
        int automobili_u_listi;
        float br;
        float zauzati_proc;
        int ostali;
        int ukupan_broj_automobila;
        public frmStatistika()
        {
            
            InitializeComponent();
            crtaj_koordinantni_sistem();
            string[] MESECI = new string[] {"","Јануар","Фебруар","Март","Април","Мај","Јун","Јул","Август","Септембар","Октобар","Новембар","Децембар"}; 
            r = new StreamReader("Rezervacija.txt");
            Rezervacije = Rezervacija.Procitaj_Rezervacije(r);
            r.Close();
            r = new StreamReader("Automobil.txt");
            Automobili = Automobil.Procitaj_Automobil(r);
            r.Close();
            r = new StreamReader("Ponuda.txt");
            Ponude = Ponuda.Procitaj_Ponude(r);
            foreach(Rezervacija rezervacija in Rezervacije)
            {
                if (!cbMesec.Items.Contains(MESECI[rezervacija.Datum_od.Month] + "-" + rezervacija.Datum_od.Year))
                {
                    
                    cbMesec.Items.Add(MESECI[rezervacija.Datum_od.Month] + "-" + rezervacija.Datum_od.Year);
                    cbMesec.ValueMember+= rezervacija.Datum_od.Month+"";
                }
                if (!cbMesec.Items.Contains(MESECI[rezervacija.Datum_do.Month] + "-" + rezervacija.Datum_do.Year))
                {
                    cbMesec.Items.Add(MESECI[rezervacija.Datum_do.Month] + "-" + rezervacija.Datum_do.Year);
                    cbMesec.ValueMember += rezervacija.Datum_do.Month + "";
                }
            }
            pictureBox1.Invalidate();
        }
        void crtaj_koordinantni_sistem()
        {

            b = new Bitmap(pictureBox1.Width, pictureBox1.Height);
            g = Graphics.FromImage(b);
            Pen olovka = new Pen(Color.WhiteSmoke, 3);
            for (int i = 0; i < pictureBox1.Width - 10; i += 1)
                g.DrawLine(Pens.Lime, 2 * i, 0, 2 * i, pictureBox1.Height);
            g.DrawLine(olovka, 1, 0, 1, pictureBox1.Height - 5);
            g.DrawString("Просечна цена по наруџбини за аутомобил", new Font("Times New Roman", 13, FontStyle.Italic), Brushes.Black, 2,25);
            g.DrawLine(olovka, 0, pictureBox1.Height - 5, pictureBox1.Width-5, pictureBox1.Height - 5);
            g.DrawLine(olovka, 0, 0, pictureBox1.Width-5,0);
            g.DrawLine(olovka, pictureBox1.Width-5, 0, pictureBox1.Width-5, pictureBox1.Height-5);
            pictureBox1.Image = b;
        }

        private float Mesec_broj_dana(DateTime Datum_od,DateTime Datum_do,int cena)
        {
            double prosecna_cena_po_danu = cena/((Datum_do - Datum_od).Days + 1);
            double broj_obuhvacenih_dana=0;
            if (Datum_od.Month == 1 || Datum_od.Month == 3 || Datum_od.Month == 5 || Datum_od.Month == 7 || Datum_od.Month == 8 || Datum_od.Month == 10 || Datum_od.Month == 12)
            {
                if (Datum_do.Month != Datum_od.Month)
                {
                    broj_obuhvacenih_dana = ((31 - Datum_od.Day) + 1) * prosecna_cena_po_danu;
                }
                else
                    broj_obuhvacenih_dana = ((Datum_do - Datum_od).Days + 1) * prosecna_cena_po_danu;
            }
            else if (Datum_od.Month == 4 || Datum_od.Month == 6 || Datum_od.Month == 9 || Datum_od.Month == 11)
            {
                if (Datum_do.Month != Datum_od.Month)
                    broj_obuhvacenih_dana = ((30 - Datum_od.Day) + 1) * prosecna_cena_po_danu;
                else
                    broj_obuhvacenih_dana = ((Datum_do - Datum_od).Days + 1) * prosecna_cena_po_danu;
            }
            else if (Datum_od.Month == 2 || Datum_od.Year % 4 == 0 && Datum_od.Year % 100 == 0 && Datum_od.Year % 400 == 0)
            {
                if (Datum_do.Month != Datum_od.Month)
                    broj_obuhvacenih_dana = ((29 - Datum_od.Day) + 1) * prosecna_cena_po_danu;
                else
                    broj_obuhvacenih_dana = ((Datum_do - Datum_od).Days + 1) * prosecna_cena_po_danu;
            }
            else
            {
                if (Datum_do.Month != Datum_od.Month)
                    broj_obuhvacenih_dana = ((28 - Datum_od.Day) + 1) * prosecna_cena_po_danu;
                else
                    broj_obuhvacenih_dana = ((Datum_do - Datum_od).Days + 1) *prosecna_cena_po_danu;
            }
            return (float)broj_obuhvacenih_dana;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                int brkola = 0; int j = 0;
                int[] sifra_kola = new int[Rezervacije.Count];
                float[] cena_po_automobilu_u_mesecu = new float[Rezervacije.Count];
                foreach (Rezervacija R in Rezervacije)
                {
                    if (ID == R.Datum_od.Month + "" && R.Datum_od.Year.ToString() == cbMesec.Text.Split('-')[1] && R.Id_automobil.ToString() == cbAutomobil.Text.Split('-')[0])
                    {
                        j++;
                    }

                }
                cena_po_automobilu_u_mesecu = new float[j];
                foreach (Rezervacija R in Rezervacije)
                    if (ID == R.Datum_od.Month + "" && R.Datum_od.Year.ToString() == cbMesec.Text.Split('-')[1] && R.Id_automobil.ToString() == cbAutomobil.Text.Split('-')[0])
                    {
                        cena_po_automobilu_u_mesecu[brkola] = Mesec_broj_dana(R.Datum_od, R.Datum_do, R.Cena);
                        brkola++;
                    }
                crtaj_statistiku(brkola, cena_po_automobilu_u_mesecu);
            }
            catch (Exception EX) { }
        }
        void crtaj_statistiku(int br,float []cena)
        {
            crtaj_koordinantni_sistem();
            int j = br;
            Brush[] cetka = new Brush[] { Brushes.SpringGreen,Brushes.Green,Brushes.SteelBlue,Brushes.Chocolate,Brushes.Chartreuse};
            Random r = new Random();
            Matrix m = new Matrix();
            m.Translate(-145,pictureBox1.Height);
            m.Rotate(-90);
            Array.Sort(cena);
            for (int i=0;i<br;i++)
            {     g.Transform = m;
                int BOJA = r.Next(0, 5);
                Rectangle[] R = new Rectangle[br];
                R[i] = new Rectangle(50*i+10,150 ,35,Convert.ToInt32(cena[i]/29));
               g.FillRectangles(cetka[BOJA],R);
                g.DrawString(" "+(j--) + " ", new Font("Times New Roman", 20, FontStyle.Bold), Brushes.Black, 50*i+10, 180);
            }
            j = br;
            m.Translate(0,0);
            m.Rotate(90);
            for(int i=0;i<br;i++)
            {
                g.Transform = m;
              g.DrawString((j--)+".     "+cena[i],new Font("Times New Roman",14,FontStyle.Italic), Brushes.Black, 320, -180+i*20);
            }
           
            pictureBox1.Invalidate();
        }

        private void cbMesec_SelectedIndexChanged(object sender, EventArgs e)
        {
            automobili_u_listi = 0;
            cbAutomobil.Items.Clear();
            ukupan_broj_automobila= Automobili.GroupBy(a => a.Id_auto).Count();
             
            ID = cbMesec.ValueMember[cbMesec.SelectedIndex] + "";
            foreach (Rezervacija R in Rezervacije)
                  {
                    foreach (Automobil Auto in Automobili)
                    {  if (R.Id_automobil==Auto.Id_auto && ID == R.Datum_od.Month + "" && R.Datum_od.Year.ToString() == cbMesec.Text.Split('-')[1] )
                       {
                        if (!cbAutomobil.Items.Contains(Auto.Id_auto + "-" + Auto.Marka + " " + Auto.Model))
                        {
                            cbAutomobil.Items.Add(Auto.Id_auto + "-" + Auto.Marka + " " + Auto.Model);
                            automobili_u_listi++;
                           ostali= ukupan_broj_automobila - automobili_u_listi;
                        }                        
                        }   
                   }
            }
             Invalidate();
        }

        private void frmStatistika_Paint(object sender, PaintEventArgs e)
        {
          
            e.Graphics.ResetTransform();
            if(automobili_u_listi!=0)
            {
              br=((ostali)*360F/ukupan_broj_automobila);
            }
            e.Graphics.DrawString("1.Број возила у %",new Font("Times New Roman",12,FontStyle.Italic),Brushes.Black,Width-200,270);
            e.Graphics.FillEllipse(Brushes.Lime, new Rectangle(Width-200,120,150,150));
            e.Graphics.FillPie(Brushes.AliceBlue, new Rectangle(Width - 200, 120, 150, 150), -90,br );
            e.Graphics.FillPie(Brushes.AliceBlue, new Rectangle(Width - 200, 290, 150, 150), -90, br);
            zauzati_proc = ((float)ostali / ukupan_broj_automobila)*100;
            e.Graphics.DrawString(zauzati_proc.ToString("0.00")+"%", new Font("Times New Roman", 12, FontStyle.Underline), Brushes.Black, Width - 100, 150);
            e.Graphics.DrawString("2.Некориштенa возила:" + ostali, new Font("Times New Roman", 12, FontStyle.Italic), Brushes.Black, Width - 200, 450);
            e.Graphics.FillPie(Brushes.Lime, new Rectangle(Width - 200, 480, 150, 150), -90+br,360-br);
            e.Graphics.DrawString((100F-zauzati_proc).ToString("0.00") + "%", new Font("Times New Roman", 12, FontStyle.Underline), Brushes.Black, Width - 210, 130);
            e.Graphics.DrawString("3.Kориштенa возила:" + automobili_u_listi, new Font("Times New Roman", 12, FontStyle.Italic), Brushes.Black, Width - 200, 650);
        }
    }
}
