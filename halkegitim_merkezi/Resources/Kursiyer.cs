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

    public partial class Kursiyer : Form
    {
        SqlConnection baglanti;
        SqlCommand komut;
        SqlCommand komut2;
        SqlDataAdapter da;
        public Kursiyer()
        {
            InitializeComponent();
        }
        void kursiyergetir()
        {
            //okul_adi.Text = "";
            // okul_id.Text = "";

            baglanti = new SqlConnection("Data Source=ABDURRAHMAN\\SQLEXPRESS;Initial Catalog=halkegitim;Integrated Security=True");
            baglanti.Open();
            da = new SqlDataAdapter("select * from kursiyer", baglanti);
            DataTable tablo = new DataTable();
            da.Fill(tablo);
            dataGridView1.DataSource = tablo;
            // tablo.Clear();
            baglanti.Close();
        }

        //void kursdersnotgetir()
        //{
        //    //okul_adi.Text = "";
        //    // okul_id.Text = "";

        //    baglanti = new SqlConnection("Data Source=ABDURRAHMAN\\SQLEXPRESS;Initial Catalog=halkegitim;Integrated Security=True");
        //    baglanti.Open();
        //    da = new SqlDataAdapter("select * from kursiyer_ders_not", baglanti);
        //    DataTable tablo = new DataTable();
        //    da.Fill(tablo);
        //    dataGridView1.DataSource = tablo;
        //    // tablo.Clear();
        //    baglanti.Close();
        //}


        private void Kursiyer_Load(object sender, EventArgs e)
        {
           
                textBox1.Enabled = false;

           
            kursiyergetir();
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

            //dataGridView1.FirstDisplayedScrollingRowIndex = dataGridView1.RowCount - 1;
            //dataGridView1[0, dataGridView1.RowCount - 1].Selected = true;
        }



        private void dataGridView1_CellEnter(object sender, DataGridViewCellEventArgs e)
        {




        }



        private void button2_Click(object sender, EventArgs e)
        {

            if (String.IsNullOrEmpty(textBox1.Text))
            {
                MessageBox.Show("Lütfen kursiyer seçiniz");

            }
            else
            {
                string sorgu = "delete from kursiyer where kursiyer_id=@kursiyer_id ";
                komut = new SqlCommand(sorgu, baglanti);
                komut.Parameters.AddWithValue("@kursiyer_id", Convert.ToInt32(textBox1.Text));
                baglanti.Open();
               var sonuc= komut.ExecuteNonQuery();
                if (sonuc>0)
                {
                    baglanti.Close();
                    kursiyergetir();
                    MessageBox.Show("Silme İşlemi Başarılı");
                }
                else
                {
                    MessageBox.Show("Silme İşlemi Başarısız");
                }
                
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            string sorgu = "UPDATE kursiyer SET adi=@adi,soyadi=@soyadi,kimlik=@kimlik,d_yeri=@d_yeri,d_tarihi=@d_tarihi where kursiyer_id=@kursiyer_id ";
            komut = new SqlCommand(sorgu, baglanti);
            komut.Parameters.AddWithValue("@kursiyer_id", Convert.ToInt32(textBox1.Text));
            komut.Parameters.AddWithValue("@adi", adi.Text);
            komut.Parameters.AddWithValue("@soyadi", soyadi.Text);
            komut.Parameters.AddWithValue("@kimlik", kimlik.Text);
            komut.Parameters.AddWithValue("@d_yeri",(comboBox1.Text));
            komut.Parameters.AddWithValue("@d_tarihi",dateTimePicker1.Value);
            baglanti.Open();
            komut.ExecuteNonQuery();
            baglanti.Close();
            kursiyergetir();

            //dataGridView1.Sort(dataGridView1.Columns["kursiyer_id"], ListSortDirection.Descending);
            //textBox1.Text = dataGridView1.CurrentRow.Cells[0].Value.ToString();

            string sorgu2 = "update  kursiyer_ders_not set kursiyer_id=@kursiyer_id,kurs_id=@kurs_id  where kursiyer_id=@kursiyer_id";
            komut2 = new SqlCommand(sorgu2, baglanti);

            komut2.Parameters.AddWithValue("@kursiyer_id", Convert.ToInt32(textBox1.Text));
            komut2.Parameters.AddWithValue("@kurs_id", KursCombo.SelectedValue);
            baglanti.Open();
            komut2.ExecuteNonQuery();
            baglanti.Close();
        }

        private void btnEkle_Click(object sender, EventArgs e)
        {
           


            string sorgu = "insert into kursiyer (adi,soyadi,kimlik,d_yeri,d_tarihi) values (@adi,@soyadi,@kimlik,@d_yeri,@d_tarihi)";
            komut = new SqlCommand(sorgu, baglanti);
            komut.Parameters.AddWithValue("@adi", adi.Text);
            komut.Parameters.AddWithValue("@soyadi", soyadi.Text);
            komut.Parameters.AddWithValue("@kimlik", kimlik.Text);
            komut.Parameters.AddWithValue("@d_yeri", comboBox1.Text);
            komut.Parameters.AddWithValue("@d_tarihi", dateTimePicker1.Value);
            baglanti.Open();
            komut.ExecuteNonQuery();
            baglanti.Close();
            kursiyergetir();

            //dataGridView1.FirstDisplayedScrollingRowIndex = dataGridView1.RowCount - 1;
            //dataGridView1[0, dataGridView1.RowCount - 1].Selected = true;
            dataGridView1.Sort(dataGridView1.Columns["kursiyer_id"], ListSortDirection.Descending);
            textBox1.Text = dataGridView1.CurrentRow.Cells[0].Value.ToString();
            string sorgu2 = "insert into kursiyer_ders_not (kursiyer_id,kurs_id) values (@kursiyer_id,@kurs_id)";
            komut2 = new SqlCommand(sorgu2, baglanti);
            komut2.Parameters.AddWithValue("@kursiyer_id", textBox1.Text.ToString());
            komut2.Parameters.AddWithValue("@kurs_id", KursCombo.SelectedValue);
            baglanti.Open();
            komut2.ExecuteNonQuery();
            baglanti.Close();
            


            // kursdersnotgetir();


        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            textBox1.Text = dataGridView1.CurrentRow.Cells[0].Value.ToString();
            adi.Text = dataGridView1.CurrentRow.Cells[1].Value.ToString();
            soyadi.Text = dataGridView1.CurrentRow.Cells[2].Value.ToString();
            kimlik.Text = dataGridView1.CurrentRow.Cells[3].Value.ToString();
            comboBox1.Text = dataGridView1.CurrentRow.Cells[4].Value.ToString();
            dateTimePicker1.Text = dataGridView1.CurrentRow.Cells[5].Value.ToString();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            this.Hide();
            formana ana = new formana();
            ana.ShowDialog();

        }
    }
}
