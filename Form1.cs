using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.Sql;
using System.Data.SqlClient;

namespace proje
{
    public partial class Form1 : Form
    {
        SqlConnection con;
        SqlDataReader dr;
        SqlCommand com;
        public Form1()
        {
            InitializeComponent();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            string user = textBox1.Text;
            string password = textBox2.Text;
            con = new SqlConnection("Data Source=DESKTOP-9KVF81C\\SQLEXPRESS;Initial Catalog=adminpanel;Integrated Security=True");
            com = new SqlCommand();
            con.Open();
            com.Connection = con;
            com.CommandText = "Select * From adminpaneli where kullanici_adi='" + textBox1.Text +
                "'And sifre='" + textBox2.Text + "'";
            dr = com.ExecuteReader();
            if (dr.Read())
            {
                MessageBox.Show("Giriş başarılı.");
                Kayit gecis = new Kayit();
                gecis.Show();
                this.Hide();
            }
            else
            {
                MessageBox.Show("Kullanıcı adı veya şifre hatalı.");
            }
            con.Close();

        }
    }
}
