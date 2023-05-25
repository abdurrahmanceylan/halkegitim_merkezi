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
    public partial class sinifislem : Form
    {
        SqlConnection baglanti;
        SqlCommand komut;
        SqlCommand komut2;
        SqlDataAdapter da;
        public sinifislem()
        {
            InitializeComponent();
        }
        void sinifgetir()
        {
            baglanti = new SqlConnection("Data Source=ABDURRAHMAN\\SQLEXPRESS;Initial Catalog=halkegitim;Integrated Security=True");
            baglanti.Open();
            da = new SqlDataAdapter("select * from sinif", baglanti);
            DataTable tablo = new DataTable();
            da.Fill(tablo);
            dataGridView1.DataSource = tablo;
            // tablo.Clear();
            baglanti.Close();
        }
            private void sinif_Load(object sender, EventArgs e)
        {
            sinif_id.Enabled = false;

            sinifgetir();
            ///bu kısım 
            baglanti.Open();
            SqlCommand komut = new SqlCommand("Select * from kurslar", baglanti);
            SqlDataAdapter da = new SqlDataAdapter(komut);
            DataTable dt = new DataTable();
            da.Fill(dt);
            KursCombo.ValueMember = "kurs_id";
            KursCombo.DisplayMember = "kurs_adi";
            KursCombo.DataSource = dt;
            baglanti.Close();
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            sinif_id.Text = dataGridView1.CurrentRow.Cells[0].Value.ToString();
            KursCombo.Text = dataGridView1.CurrentRow.Cells[1].Value.ToString();
            sinifadi.Text = dataGridView1.CurrentRow.Cells[2].Value.ToString();
           

        }

        private void btnEkle_Click(object sender, EventArgs e)
        {
            string sorgu = "insert into sinif (kurs_id,sinifadi) values (@kurs_id,@sinifadi)";
            komut = new SqlCommand(sorgu, baglanti);
            komut.Parameters.AddWithValue("@kurs_id", KursCombo.SelectedValue);
            komut.Parameters.AddWithValue("@sinifadi", sinifadi.Text);
           
            baglanti.Open();
            komut.ExecuteNonQuery();
            baglanti.Close();
            sinifgetir();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            string sorgu = "delete from sinif where sinif_id=@sinif_id ";
            komut = new SqlCommand(sorgu, baglanti);
            komut.Parameters.AddWithValue("@sinif_id", Convert.ToInt32(sinif_id.Text));
            baglanti.Open();
            komut.ExecuteNonQuery();
            baglanti.Close();
            sinifgetir();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            string sorgu = "UPDATE sinif SET kurs_id=@kurs_id,sinifadi=@sinifadi where sinif_id=@sinif_id ";
            komut = new SqlCommand(sorgu, baglanti);
            komut.Parameters.AddWithValue("@sinif_id", Convert.ToInt32(sinif_id.Text));
            komut.Parameters.AddWithValue("@kurs_id", KursCombo.SelectedValue);
            komut.Parameters.AddWithValue("@sinifadi", sinifadi.Text);
        
            baglanti.Open();
            komut.ExecuteNonQuery();
            baglanti.Close();
           sinifgetir();
        }

       
    }
}
