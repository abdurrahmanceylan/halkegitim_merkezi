using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace halkegitim_merkezi.Resources
{
    public partial class formana : Form
    {
        public formana()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            Form3 frm3 = new Form3();
            frm3.Show();
        }

        private void button8_Click(object sender, EventArgs e)
        {
            sinifislem snf = new sinifislem();
            snf.Show();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Form2 frm2 = new Form2();
            frm2.ShowDialog();  
            this.Hide();
        }

        private void button9_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Kurslar krs = new Kurslar();
            krs.ShowDialog();
            this.Hide();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            Kursiyer krsyr = new Kursiyer();
            krsyr.ShowDialog();
            this.Hide();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            ogretmen ogrtmn = new ogretmen();
           ogrtmn.ShowDialog();
            this.Hide();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            Notlar_Tablosu  nt = new Notlar_Tablosu ();
            nt.ShowDialog();
            this.Hide();
        }

        private void button7_Click(object sender, EventArgs e)
        {
            Form1 frm1 = new Form1();   
            frm1.ShowDialog();  
            this.Hide();
        }
    }
}
 