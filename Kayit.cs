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
using System.Drawing.Printing;
using System.Drawing;

namespace proje
{
    public partial class Kayit : Form
    {
        SqlConnection baglanti;
        SqlCommand komut;
        SqlDataAdapter da;
        public Kayit()
        {
            InitializeComponent();
        }

        private void dateTimePicker1_ValueChanged(object sender, EventArgs e)
        {

        }

        void OgrenciGetir()
        {
            baglanti = new SqlConnection("Data Source=DESKTOP-9KVF81C\\SQLEXPRESS;Initial Catalog=adminpanel;Integrated Security=True");
            baglanti.Open();
            da = new SqlDataAdapter("SELECT * FROM ogrenci", baglanti);
            DataTable tablo = new DataTable();
            da.Fill(tablo);
            dataGridView1.DataSource = tablo;
            baglanti.Close();

        }
        private void Kayit_Load(object sender, EventArgs e)
        {
            OgrenciGetir();
        }

        private void dataGridView1_CellEnter(object sender, DataGridViewCellEventArgs e)
        {
            txtNo.Text = dataGridView1.CurrentRow.Cells[0].Value.ToString();
            txtAd.Text = dataGridView1.CurrentRow.Cells[1].Value.ToString();
            txtSoyad.Text = dataGridView1.CurrentRow.Cells[2].Value.ToString();
            string tarihStr = dataGridView1.CurrentRow.Cells[3].Value.ToString();
            DateTime tarih;

            if (DateTime.TryParse(tarihStr, out tarih))
            {
                dateTimePicker1.Value = tarih;
            }
            else
            {

                dateTimePicker1.Value = DateTime.Now;
            }
            txtTel.Text = dataGridView1.CurrentRow.Cells[4].Value.ToString();



        }

        private void btnEkle_Click(object sender, EventArgs e)
        {
            string sorgu = "INSERT INTO ogrenci(ad, soyad, dtarih, tel) VALUES (@ad, @soyad, @dtarih, @tel)";
            komut = new SqlCommand(sorgu, baglanti);

            komut.Parameters.AddWithValue("@ad", txtAd.Text);
            komut.Parameters.AddWithValue("@soyad", txtSoyad.Text);
            komut.Parameters.AddWithValue("@dtarih", dateTimePicker1.Value);
            komut.Parameters.AddWithValue("@tel", txtTel.Text);
            baglanti.Open();
            komut.ExecuteNonQuery();
            baglanti.Close();
            OgrenciGetir();


        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            // DataGridView'nin boyutlandırılması ve görünürlüğünü ayarlama
            dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
            dataGridView1.AutoResizeColumns(DataGridViewAutoSizeColumnsMode.AllCells);
            dataGridView1.AutoResizeRows(DataGridViewAutoSizeRowsMode.AllCells);

            // DataGridView'nin kaydırma çubukları eklemesini sağlama
            dataGridView1.ScrollBars = ScrollBars.Both;
        }

        private void btnSil_Click(object sender, EventArgs e)
        {
            string sorgu = "DELETE FROM ogrenci WHERE mno=@mno";
            komut = new SqlCommand(sorgu, baglanti);
            komut.Parameters.AddWithValue("@mno",Convert.ToInt32(txtNo.Text));
            baglanti.Open() ;
            komut.ExecuteNonQuery();
            baglanti.Close() ;
            OgrenciGetir() ;
        }

        private void btnGüncelle_Click(object sender, EventArgs e)
        {
            string sorgu = "UPDATE ogrenci SET ad=@ad, soyad=@soyad, dtarih=@dtarih, tel=@tel WHERE mno=@mno";
            komut = new SqlCommand (sorgu, baglanti);
            komut.Parameters.AddWithValue("@mno",Convert.ToInt32(txtNo.Text));
            komut.Parameters.AddWithValue("@ad",txtAd.Text);
            komut.Parameters.AddWithValue("@soyad",txtSoyad.Text);
            komut.Parameters.AddWithValue("@dtarih", dateTimePicker1.Value);
            komut.Parameters.AddWithValue("@tel",txtTel.Text);
            baglanti.Open();
            komut.ExecuteNonQuery();
            baglanti.Close();
            OgrenciGetir() ;
        }

        private void btnYazdir_Click(object sender, EventArgs e)
        {
            printPreviewDialog1.Document = printDocument1; 
            printPreviewDialog1.PrintPreviewControl.Zoom = 1;
            printPreviewDialog1.ShowDialog();
        }

        private void printDocument1_PrintPage(object sender, PrintPageEventArgs e)
        {
            Bitmap imagebmp = new Bitmap(dataGridView1.Width, dataGridView1.Height);
            dataGridView1.DrawToBitmap(imagebmp, new Rectangle(0, 0, dataGridView1.Width, dataGridView1.Height));
            e.Graphics.DrawImage(imagebmp, 120, 20);
        }

        private void button1_Click(object sender, EventArgs e)
        {

        }
        public static string no, ad, soyad, dtarih, tel;
        private void button1_Click_1(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtNo.Text) || string.IsNullOrWhiteSpace(txtAd.Text) || string.IsNullOrWhiteSpace(txtSoyad.Text) || dateTimePicker1.Value == null)
            {
                // Eksik alanlar var, kullanıcıya bir uyarı verilebilir veya gerekli işlemler yapılabilir.
                MessageBox.Show("Lütfen tüm alanları doldurun.","Hata",MessageBoxButtons.OK,MessageBoxIcon.Exclamation);
            }
            else
            {
                no = txtNo.Text;
                ad = txtAd.Text;
                soyad = txtSoyad.Text;
                dtarih = dateTimePicker1.Text;
                tel = txtTel.Text;
                new FrmId().ShowDialog();

            }

        }
    }
}
