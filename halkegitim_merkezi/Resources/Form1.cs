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
    public partial class Form1 : Form
    {
        SqlConnection baglanti;
        SqlCommand komut;
        SqlCommand komut2;
        SqlDataAdapter da;
        public Form1()
        {
            InitializeComponent();
        }
        void saatgetir()
        {
            baglanti = new SqlConnection("Data Source=ABDURRAHMAN\\SQLEXPRESS;Initial Catalog=halkegitim;Integrated Security=True");
            baglanti.Open();
            da = new SqlDataAdapter("select * from kurs_saat", baglanti);
            DataTable tablo = new DataTable();
            da.Fill(tablo);
            dataGridView1.DataSource = tablo;
            // tablo.Clear();
            baglanti.Close();
        }
        private void Form1_Load(object sender, EventArgs e)
        {
            kurs_saat_id.Enabled = false;

            saatgetir();
            ///bu kısım 
            baglanti.Open();
            SqlCommand komut = new SqlCommand("Select * from sinif", baglanti);
            SqlDataAdapter da = new SqlDataAdapter(komut);
            DataTable dt = new DataTable();
            da.Fill(dt);
            sinifCombo.ValueMember = "sinif_id";
            sinifCombo.DisplayMember = "sinifadi";
            sinifCombo.DataSource = dt;
            baglanti.Close();
        }

        private void btnEkle_Click(object sender, EventArgs e)
        {
            string sorgu = "insert into kurs_saat (sinif_id,kurs_tarih,kurs_saat) values (@sinif_id,@kurs_tarih,@kurs_saat)";
            komut = new SqlCommand(sorgu, baglanti);
           // komut.Parameters.AddWithValue("@kurs_saat_id", Convert.ToInt32 ( kurs_saat_id.Text));
            komut.Parameters.AddWithValue("@sinif_id", sinifCombo.SelectedValue);
            komut.Parameters.AddWithValue("@kurs_tarih", dateTimePicker1.Value);
            komut.Parameters.AddWithValue("@kurs_saat", maskedTextBox2.Text);


            baglanti.Open();
            komut.ExecuteNonQuery();
            baglanti.Close();
            saatgetir();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            string sorgu = "delete from kurs_saat where kurs_saat_id=@kurs_saat_id ";
            komut = new SqlCommand(sorgu, baglanti);
            komut.Parameters.AddWithValue("@kurs_saat_id", Convert.ToInt32(kurs_saat_id.Text));
            baglanti.Open();
            komut.ExecuteNonQuery();
            baglanti.Close();
            saatgetir();
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            kurs_saat_id.Text = dataGridView1.CurrentRow.Cells[0].Value.ToString();
            sinifCombo.Text = dataGridView1.CurrentRow.Cells[1].Value.ToString();
            dateTimePicker1.Text = dataGridView1.CurrentRow.Cells[2].Value.ToString();
            maskedTextBox2.Text = dataGridView1.CurrentRow.Cells[3].Value.ToString();


        }

        private void button3_Click(object sender, EventArgs e)
        {
            string sorgu = "UPDATE kurs_saat SET sinif_id=@sinif_id,kurs_tarih=@kurs_tarih,kurs_saat=@kurs_saat where kurs_saat_id=@kurs_saat_id ";
            komut = new SqlCommand(sorgu, baglanti);
            komut.Parameters.AddWithValue("@kurs_saat_id", Convert.ToInt32(kurs_saat_id.Text));
            komut.Parameters.AddWithValue("@sinif_id", sinifCombo.SelectedValue);
            komut.Parameters.AddWithValue("@kurs_tarih", dateTimePicker1.Value);
            komut.Parameters.AddWithValue("@kurs_saat", maskedTextBox2.Text);

            baglanti.Open();
            komut.ExecuteNonQuery();
            baglanti.Close();
            saatgetir();
        }
    }
}
