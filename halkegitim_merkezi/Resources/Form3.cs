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
    public partial class Form3 : Form
    {
        SqlConnection baglanti;
        SqlCommand komut;
        SqlDataAdapter da;
        public Form3()
        {
            InitializeComponent();
        }
        void bolumgetir()
        {
            baglanti = new SqlConnection("Data Source=ABDURRAHMAN\\SQLEXPRESS;Initial Catalog=halkegitim;Integrated Security=True");
            baglanti.Open();
            da = new SqlDataAdapter("select * from bolum", baglanti);
            DataTable tablo= new DataTable();
            da.Fill(tablo);
            dataGridView1.DataSource = tablo;
            baglanti.Close();   
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void Form3_Load(object sender, EventArgs e)
        {
            bolumid.Enabled = false;
            comboBox1.Items.Clear();
            SqlDataReader oku;
            bolumgetir();
            baglanti.Open();
            komut = new SqlCommand();
            komut.Connection = baglanti;
            komut.CommandText= ("Select * from okul");
            oku= komut.ExecuteReader();
            while (oku.Read())
            {
                comboBox1.Items.Add(oku["okul_id"].ToString());
            }
            baglanti.Close();

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void dataGridView1_CellEnter(object sender, DataGridViewCellEventArgs e)
        {
            bolumid.Text = dataGridView1.CurrentRow.Cells[0].Value.ToString();
            bolumad.Text = dataGridView1.CurrentRow.Cells[1].Value.ToString();
            comboBox1.Text = dataGridView1.CurrentRow.Cells[2].Value.ToString();
            
        }

        private void okulid_TextChanged(object sender, EventArgs e)
        {

        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            string sorgu = "insert into bolum (bolum_adi,okul_id) values (@bolum_adi,@okul_id)";
            komut=new SqlCommand(sorgu,baglanti);
            komut.Parameters.AddWithValue("@bolum_adi", bolumad.Text);
            komut.Parameters.AddWithValue("@okul_id", comboBox1.Text);
            baglanti.Open();
            komut.ExecuteNonQuery();    
            baglanti.Close() ;
            bolumgetir();
        }

        private void bolumad_TextChanged(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            string sorgu = "delete from bolum where bolum_id=@bolum_id ";
            komut=new SqlCommand(sorgu,baglanti);
            komut.Parameters.AddWithValue("@bolum_id", Convert.ToInt32(bolumid.Text));
            baglanti.Open();
            komut.ExecuteNonQuery();
            baglanti.Close();
            bolumgetir();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            string sorgu = "UPDATE bolum SET bolum_adi=@bolum_adi,okul_id=@okul_id where bolum_id=@bolum_id ";
            komut = new SqlCommand(sorgu, baglanti);
            komut.Parameters.AddWithValue("@bolum_id", Convert.ToInt32(bolumid.Text));
            komut.Parameters.AddWithValue("@bolum_adi", bolumad.Text);
            komut.Parameters.AddWithValue("@okul_id", Convert.ToInt32(comboBox1.Text));
            baglanti.Open();
            komut.ExecuteNonQuery();
            baglanti.Close();
            bolumgetir();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            this.Hide();
            formana ana = new formana();
            ana.ShowDialog();
        }
    }
}
