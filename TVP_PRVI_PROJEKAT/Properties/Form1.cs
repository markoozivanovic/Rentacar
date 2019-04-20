using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TVP_PRVI_PROJEKAT
{
    public partial class frmPocetna : Form
    {
        frmPrijava k;
        public int broj_formi;
        public frmPocetna()
        {
           
            InitializeComponent();
            DateTime datum = DateTime.Now;
            Datum.Text = datum.ToString("Приступљено: \nHH:mm:ss \n dd.MM.yyyy");
        }

        private void администрацијаToolStripMenuItem_Click(object sender, EventArgs e)
        {
            k = new frmPrijava("Admin");
            OtvoriForme(k);
        }
        public void FormeIzlaz(object sender,FormClosedEventArgs e)
        {
            Progress.Value = 0;
            izvestaj.Text= "Форма затворена!";
            broj_formi--;
            brFormi.Text = "Број отворених форми је"+broj_formi+"";
        }

        private void frmPocetna_Load(object sender, EventArgs e)
        {

        }

        private void toolStripMenuItem3_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            WindowState = FormWindowState.Maximized;
            
        }

        private void pictureBox1_DoubleClick(object sender, EventArgs e)
        {
            
            
                WindowState = FormWindowState.Normal;
            
            
        }
        public string IzbvestajOtvorenaForma(Form m)
        {
            return "" + m.Text + " је отворена!";
        }
        public  void OtvoriForme(Form f)
        {
          
            Progress.Value = 100;
            izvestaj.Text = IzbvestajOtvorenaForma(f);
            f.Show();  
            broj_formi++;
            brFormi.Text = "Број отворених форми је" + broj_formi;
            f.FormClosed += FormeIzlaz;
          
        }
        private void toolStripMenuItem1_Click(object sender, EventArgs e)
        {
            k = new frmPrijava();
            OtvoriForme(k);
        }

     
    }
}
