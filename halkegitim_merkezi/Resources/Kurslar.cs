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
    public partial class Kurslar : Form
    {
        SqlConnection baglanti;
        SqlCommand komut;
        SqlDataAdapter da;
        public Kurslar()
        {
            InitializeComponent();
        }
        void kursgetir()
        {
            //okul_adi.Text = " ";
           // okul_id.Text = " ";

            baglanti = new SqlConnection("Data Source=ABDURRAHMAN\\SQLEXPRESS;Initial Catalog=halkegitim;Integrated Security=True");
            baglanti.Open();
            da = new SqlDataAdapter("select * from kurslar", baglanti);
            DataTable tablo = new DataTable();
            da.Fill(tablo);
            dataGridView1.DataSource = tablo;
            // tablo.Clear();
            baglanti.Close();
        }
        private void Kurs_islemleri_Load(object sender, EventArgs e)
        {
            kurs_id.Enabled = false;
            comboBox1.Items.Clear();
            SqlDataReader oku;
            kursgetir();
            baglanti.Open();
            komut = new SqlCommand();
            komut.Connection = baglanti;
            komut.CommandText = ("Select * from bolum");
            oku = komut.ExecuteReader();
            while (oku.Read())
            {
                comboBox1.Items.Add(oku["bolum_id"].ToString());
            }
            baglanti.Close();


            comboBox2.Items.Clear();
            SqlDataReader oku1;
            kursgetir();
            baglanti.Open();
            komut = new SqlCommand();
            komut.Connection = baglanti;
            komut.CommandText = ("Select * from ogretmen");
            oku1 = komut.ExecuteReader();
            while (oku1.Read())
            {
                comboBox2.Items.Add(oku1["ogretmen_id"].ToString());
            }
            baglanti.Close();


        }

        private void dataGridView1_CellEnter(object sender, DataGridViewCellEventArgs e)
        {
            kurs_id.Text = dataGridView1.CurrentRow.Cells[0].Value.ToString();
            kursad.Text = dataGridView1.CurrentRow.Cells[1].Value.ToString();
            comboBox1.Text = dataGridView1.CurrentRow.Cells[2].Value.ToString();
            comboBox2.Text = dataGridView1.CurrentRow.Cells[3].Value.ToString();

        }

        private void button1_Click(object sender, EventArgs e)
        {
            string sorgu = "insert into kurslar (kurs_adi,bolum_id,ogretmen_id) values (@kurs_adi,@bolum_id,@ogretmen_id)";
            komut = new SqlCommand(sorgu, baglanti);
            komut.Parameters.AddWithValue("@kurs_adi", kursad.Text);
            komut.Parameters.AddWithValue("@bolum_id", comboBox1.Text);
            komut.Parameters.AddWithValue("@ogretmen_id", comboBox2.Text);
            baglanti.Open();
            komut.ExecuteNonQuery();
            baglanti.Close();
            kursgetir();
        }

        private void button2_Click(object sender, EventArgs e)
        {

            if (String.IsNullOrEmpty(kurs_id.Text))
            {
                MessageBox.Show("Lütfen kursiyer seçiniz");

            }
            else
            {
                string sorgu = "delete from kurslar where kurs_id=@kurs_id ";
                komut = new SqlCommand(sorgu, baglanti);
                komut.Parameters.AddWithValue("@kurs_id", Convert.ToInt32(kurs_id.Text));
                baglanti.Open();
                komut.ExecuteNonQuery();
                baglanti.Close();
                kursgetir();
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            string sorgu = "UPDATE kurslar SET kurs_adi=@kurs_adi,bolum_id=@bolum_id,ogretmen_id=@ogretmen_id where kurs_id=@kurs_id ";
            komut = new SqlCommand(sorgu, baglanti);
            komut.Parameters.AddWithValue("@kurs_id", Convert.ToInt32(kurs_id.Text));
            komut.Parameters.AddWithValue("@kurs_adi", kursad.Text);
            komut.Parameters.AddWithValue("@bolum_id", Convert.ToInt32(comboBox1.Text));
            komut.Parameters.AddWithValue("@ogretmen_id", Convert.ToInt32(comboBox2.Text));
            baglanti.Open();
            komut.ExecuteNonQuery();
            baglanti.Close();
            kursgetir();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            this.Hide();
            formana ana = new formana();
            ana.ShowDialog();
        }
    }
}
