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
    public partial class frmAdministrator : Form
    {
        frmAutomobil fAuto;
        frmStatistika fStatistika;
        frmPonuda fpon;
        FrmRezervacija frez;
        frmPrijava fpri;
        FrmKupac fkupac;
        int tajmer = 60;
        public frmAdministrator():base()
        {
            InitializeComponent();



            fStatistika = new frmStatistika();
            fpon = new frmPonuda();
            frez = new FrmRezervacija();
            fAuto = new frmAutomobil();
            fkupac = new FrmKupac();
            fpri = new frmPrijava("Admin");
            
            timer1.Enabled = true;
        }
        public frmAdministrator(string ime,string prezime):this()
        {
            toolStripMenuItem1.Text += " " + ime+" "+ prezime + "(Одјави се)";
        }
       

        private void frmAdministrator_Load(object sender, EventArgs e)
        {

        }
       
        private void pictureBox1_Click(object sender, EventArgs e)
        {
            fAuto = new frmAutomobil();
            fAuto.Show();
          

          
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            fkupac = new FrmKupac();
            fkupac.Show();
          
           
        }
       
        private void pictureBox4_Click(object sender, EventArgs e)
        {
            fStatistika = new frmStatistika();
            fStatistika.Show();
           
        }

        private void pictureBox3_Click(object sender, EventArgs e)
        {
            fpon = new frmPonuda();
            fpon.Show();
           
        }

        private void pictureBox5_Click(object sender, EventArgs e)
        {
            frez = new FrmRezervacija();
            frez.Show();
          
        }

        private void label3_Click(object sender, EventArgs e)
        {
           
        }

        private void излазToolStripMenuItem_Click(object sender, EventArgs e)
        {

            if (MessageBox.Show("Да ли желите изаћи из апликације?", "Излаз?", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) == DialogResult.Yes)
            {
                Close();
                fAuto.Close();
                fStatistika.Close();
                fkupac.Close();
                 fpon.Close();
                frez.Close();
            }
        }

        private void toolStripMenuItem2_Click(object sender, EventArgs e)
        {

        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            
            toolStripMenuItem2.Text = "Трајања сесије:"+(tajmer--)+"s";
            if(tajmer<1)
            {
                Close();
            }
        }

        private void frmAdministrator_Activated(object sender, EventArgs e)
        {
            timer1.Enabled = true;
        }

        private void frmAdministrator_Deactivate(object sender, EventArgs e)
        {
            timer1.Enabled = false;
        }
    }
}
