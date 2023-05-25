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
    public partial class ogretmen : Form
    {
        SqlConnection baglanti;
        SqlCommand komut;
        SqlDataAdapter da;
        public ogretmen()
        {
            InitializeComponent();
        }
        void ogretmengetir()
        {
            //okul_adi.Text = " ";
            // okul_id.Text = " ";

            baglanti = new SqlConnection("Data Source=ABDURRAHMAN\\SQLEXPRESS;Initial Catalog=halkegitim;Integrated Security=True");
            baglanti.Open();
            da = new SqlDataAdapter("select * from ogretmen", baglanti);
            DataTable tablo = new DataTable();
            da.Fill(tablo);
            dataGridView1.DataSource = tablo;
            // tablo.Clear();
            baglanti.Close();
        }
        private void ogretmen_Load(object sender, EventArgs e)
        {
            ogretmen_id.Enabled = false;

            ogretmengetir();
        }

        private void dataGridView1_CellEnter(object sender, DataGridViewCellEventArgs e)
        {
            ogretmen_id.Text = dataGridView1.CurrentRow.Cells[0].Value.ToString();
            adi.Text = dataGridView1.CurrentRow.Cells[1].Value.ToString();
            soyadi.Text = dataGridView1.CurrentRow.Cells[2].Value.ToString();
            kimlik.Text = dataGridView1.CurrentRow.Cells[3].Value.ToString();
            comboBox1.Text = dataGridView1.CurrentRow.Cells[4].Value.ToString();
            dateTimePicker1.Text = dataGridView1.CurrentRow.Cells[5].Value.ToString();


        }

        private void button1_Click(object sender, EventArgs e)
        {
            string sorgu = "insert into ogretmen (adi,soyadi,kimlik,d_yeri,d_tarihi) values (@adi,@soyadi,@kimlik,@d_yeri,@d_tarihi)";
            komut = new SqlCommand(sorgu, baglanti);
            komut.Parameters.AddWithValue("@adi", adi.Text);
            komut.Parameters.AddWithValue("@soyadi", soyadi.Text);
            komut.Parameters.AddWithValue("@kimlik", kimlik.Text);
            komut.Parameters.AddWithValue("@d_yeri", comboBox1.Text);
            komut.Parameters.AddWithValue("@d_tarihi", dateTimePicker1.Value);
            baglanti.Open();
            komut.ExecuteNonQuery();
            baglanti.Close();
            ogretmengetir();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            string sorgu = "delete from ogretmen where ogretmen_id=@ogretmen_id ";
            komut = new SqlCommand(sorgu, baglanti);
            komut.Parameters.AddWithValue("@ogretmen_id", Convert.ToInt32(ogretmen_id.Text));
            baglanti.Open();
            komut.ExecuteNonQuery();
            baglanti.Close();
            ogretmengetir();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            string sorgu = "UPDATE ogretmen SET adi=@adi,soyadi=@soyadi,kimlik=@kimlik,d_yeri=@d_yeri,d_tarihi=@d_tarihi where ogretmen_id=@ogretmen_id ";
            komut = new SqlCommand(sorgu, baglanti);
            komut.Parameters.AddWithValue("@ogretmen_id", Convert.ToInt32(ogretmen_id.Text));
            komut.Parameters.AddWithValue("@adi", adi.Text);
            komut.Parameters.AddWithValue("@soyadi", soyadi.Text);
            komut.Parameters.AddWithValue("@kimlik", kimlik.Text);
            komut.Parameters.AddWithValue("@d_yeri", (comboBox1.Text));
            komut.Parameters.AddWithValue("@d_tarihi", dateTimePicker1.Value);
            baglanti.Open();
            komut.ExecuteNonQuery();
            baglanti.Close();
            ogretmengetir();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            this.Hide();
            formana ana = new formana();
            ana.ShowDialog();
        }
    }
}
