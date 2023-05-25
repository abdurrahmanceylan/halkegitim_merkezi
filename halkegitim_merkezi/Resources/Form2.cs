using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace halkegitim_merkezi.Resources
{
    public partial class Form2 : Form
    {
        SqlConnection baglanti;
        SqlCommand komut;

        SqlDataAdapter da;
        public Form2()
        {
            InitializeComponent();
        }
        void okulgetir()
        {
            okul_adi.Text = " ";
            okul_id.Text = " ";
           
            baglanti = new SqlConnection("Data Source=ABDURRAHMAN\\SQLEXPRESS;Initial Catalog=halkegitim;Integrated Security=True");
            baglanti.Open();
            da = new SqlDataAdapter("select * from okul", baglanti);
            DataTable tablo = new DataTable();
            da.Fill(tablo);
            dataGridView1.DataSource = tablo;
           // tablo.Clear();
            baglanti.Close();
        }
        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void Form2_Load(object sender, EventArgs e)
        {
         
            okulgetir();
      
        }

        private void dataGridView1_CellEnter(object sender, DataGridViewCellEventArgs e)
        {
           
            okul_id.Text = dataGridView1.CurrentRow.Cells[0].Value.ToString();
            okul_adi.Text = dataGridView1.CurrentRow.Cells[1].Value.ToString();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string sorgu = "insert into okul (okul_adi) values (@okul_adi)";
            komut = new SqlCommand(sorgu, baglanti);
            komut.Parameters.AddWithValue("@okul_adi", okul_adi.Text);
           
            baglanti.Open();
            komut.ExecuteNonQuery();
            baglanti.Close();
            okulgetir();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            string sorgu = "delete from okul where okul_id=@okul_id ";
            komut = new SqlCommand(sorgu, baglanti);
            komut.Parameters.AddWithValue("@okul_id", Convert.ToInt32(okul_id.Text));
            baglanti.Open();
            komut.ExecuteNonQuery();
            baglanti.Close();
            okulgetir();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            string sorgu = "UPDATE okul SET okul_adi=@okul_adi where okul_id=@okul_id ";
            komut = new SqlCommand(sorgu, baglanti);
            komut.Parameters.AddWithValue("@okul_id", Convert.ToInt32(okul_id.Text));
            komut.Parameters.AddWithValue("@okul_adi", okul_adi.Text);
            
            baglanti.Open();
            komut.ExecuteNonQuery();
            baglanti.Close();
            okulgetir();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            this.Hide();
            formana ana = new formana();
            ana.ShowDialog();


        }
    }
}
