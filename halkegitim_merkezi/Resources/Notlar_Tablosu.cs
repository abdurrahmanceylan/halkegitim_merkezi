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
    public partial class Notlar_Tablosu : Form
    {
        SqlConnection baglanti;
        SqlCommand komut;
        SqlCommand komut2;
        SqlDataAdapter da;
        public Notlar_Tablosu()
        {
            InitializeComponent();
        }
        void notgetir()
        {
            //okul_adi.Text = " ";
            // okul_id.Text = " ";

            baglanti = new SqlConnection("Data Source=ABDURRAHMAN\\SQLEXPRESS;Initial Catalog=halkegitim;Integrated Security=True");
            baglanti.Open();
            da = new SqlDataAdapter("select * from notdurumu ", baglanti);
            DataTable tablo = new DataTable();
            da.Fill(tablo);
            dataGridView1.DataSource = tablo;
            // tablo.Clear();
            baglanti.Close();
        }

        void kursiyerdersnotgetir()
        {
            //okul_adi.Text = " ";
            // okul_id.Text = " ";

            baglanti = new SqlConnection("Data Source=ABDURRAHMAN\\SQLEXPRESS;Initial Catalog=halkegitim;Integrated Security=True");
            baglanti.Open();
            da = new SqlDataAdapter("select * from kursiyer_ders_not ", baglanti);
            DataTable tablo = new DataTable();
            da.Fill(tablo);
            
            // tablo.Clear();
            baglanti.Close();
        }
        private void Not_Load(object sender, EventArgs e)
        {
            //notid.Enabled = false;
            //KursiyerCombo.Items.Clear();

            notgetir();
            baglanti.Open();
            SqlCommand komut = new SqlCommand("Select * from kurslar", baglanti);
            SqlDataAdapter da = new SqlDataAdapter(komut);
            DataTable dt = new DataTable();
            da.Fill(dt);
            KursCombo.ValueMember = "kurs_id";
            KursCombo.DisplayMember = "kurs_adi";
            KursCombo.DataSource = dt;
            baglanti.Close();

            //baglanti.Open();
            //SqlCommand komut1 = new SqlCommand("Select * from kursiyer", baglanti);
            //SqlDataAdapter daa = new SqlDataAdapter(komut1);
            //DataTable dtt = new DataTable();
            //daa.Fill(dtt);
            //KursiyerCombo.ValueMember = "kursiyer_id";
            //KursiyerCombo.DisplayMember = "adi";
            //KursiyerCombo.DataSource = dtt;
            //baglanti.Close();
            KursiyerCombo.Items.Clear();
            SqlDataReader oku;
            notgetir();
            baglanti.Open();
            komut = new SqlCommand();
            komut.Connection = baglanti;
            komut.CommandText = ("Select * from kursiyer_ders_not");
            oku = komut.ExecuteReader();
            while (oku.Read())
            {
                KursiyerCombo.Items.Add(oku["kursiyer_id"].ToString());
            }
            baglanti.Close();

            //comboBox1.Items.Clear();
            //SqlDataReader oku1;
            //kursiyerdersnotgetir();
            //baglanti.Open();
            //komut = new SqlCommand();
            //komut.Connection = baglanti;
            //komut.CommandText = ("Select * from kursiyer_ders_not");
            //oku1 = komut.ExecuteReader();
            //while (oku1.Read())
            //{
            //    comboBox1.Items.Clear();
            //        comboBox1.Items.Add(oku1["id"].ToString());
            //}
            //baglanti.Close();
        }

        private void dataGridView1_CellEnter(object sender, DataGridViewCellEventArgs e)
        {
            

        }

        private void button4_Click(object sender, EventArgs e)
        {
            double y1, y2, ortalamasonuc;
            y1 = Convert.ToDouble(sinav1.Text);
            y2 = Convert.ToDouble(sinav2.Text);
            
            ortalamasonuc = (y1 + y2) / 2;
            ortalama.Text = ortalamasonuc.ToString();

            if (ortalamasonuc >= 70) 
            { durum.Text = "Geçti"; }
            else
            { durum.Text = "Kaldı"; }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string sorgu = "insert into notdurumu (sinav1,sinav2,ort,durum) values (@sinav1,@sinav2,@ort,@durum)";
            //   
            komut = new SqlCommand(sorgu, baglanti);
           // komut.Parameters.AddWithValue("@kursiyer_id", KursiyerCombo.Text);


            komut.Parameters.AddWithValue("@sinav1", Convert.ToInt16( sinav1.Text));
            komut.Parameters.AddWithValue("@sinav2", Convert.ToInt16(sinav2.Text));
            komut.Parameters.AddWithValue("@ort", Convert.ToDouble(ortalama.Text));
            komut.Parameters.AddWithValue("@durum", durum.Text);
            
            baglanti.Open();
            komut.ExecuteNonQuery();
            baglanti.Close();
            notgetir();

            dataGridView1.Sort(dataGridView1.Columns["not_id"], ListSortDirection.Descending);
            notid.Text = dataGridView1.CurrentRow.Cells[0].Value.ToString();
            string sorgu2 = "update kursiyer_ders_not set not_id=@not_id where kursiyer_id=@kursiyer_id";
            komut2 = new SqlCommand(sorgu2, baglanti);
            komut2.Parameters.AddWithValue("@kursiyer_id", Convert.ToInt32(KursiyerCombo.Text));
            komut2.Parameters.AddWithValue("@not_id", Convert.ToInt32 (notid.Text));
            baglanti.Open();
            komut2.ExecuteNonQuery();
            baglanti.Close();

            kursiyerdersnotgetir();
        }

        private void label7_Click(object sender, EventArgs e)
        {

        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            string sorgu = "delete from notdurumu where not_id=@not_id ";
            komut = new SqlCommand(sorgu, baglanti);
            komut.Parameters.AddWithValue("@not_id", Convert.ToInt32(notid.Text));
            baglanti.Open();
            komut.ExecuteNonQuery();
            baglanti.Close();
            notgetir();
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            notid.Text = dataGridView1.CurrentRow.Cells[0].Value.ToString();
            sinav1.Text = dataGridView1.CurrentRow.Cells[1].Value.ToString();
            sinav2.Text = dataGridView1.CurrentRow.Cells[2].Value.ToString();
            ortalama.Text = dataGridView1.CurrentRow.Cells[3].Value.ToString();
            durum.Text = dataGridView1.CurrentRow.Cells[4].Value.ToString();
            //  comboBox1.Text = dataGridView1.CurrentRow.Cells[1].Value.ToString();

        }

        private void button3_Click(object sender, EventArgs e)
        {
            string sorgu = "UPDATE notdurumu SET sinav1=@sinav1,sinav2=@sinav2,ort=@ort,durum=@durum where not_id=@not_id ";
            komut = new SqlCommand(sorgu, baglanti);
            komut.Parameters.AddWithValue("@not_id", Convert.ToInt32(notid.Text));
            komut.Parameters.AddWithValue("@sinav1", Convert.ToInt32(sinav1.Text));
            komut.Parameters.AddWithValue("@sinav2", Convert.ToInt32(sinav2.Text));
            komut.Parameters.AddWithValue("@ort", Convert.ToInt32(ortalama.Text));
            komut.Parameters.AddWithValue("@durum",(durum.Text));
            baglanti.Open();
            komut.ExecuteNonQuery();
            baglanti.Close();
            notgetir();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            this.Hide();
            formana ana = new formana();
            ana.ShowDialog();
        }
    }
}
