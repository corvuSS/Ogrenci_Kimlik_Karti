using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Printing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace proje
{
    public partial class FrmId : Form
    {
        public FrmId()
        {
            InitializeComponent();
        }

        private void FrmId_Load(object sender, EventArgs e)
        {
            textBox1.Text = Kayit.no.ToUpper();
            textBox2.Text = Kayit.ad.ToUpper();
            textBox3.Text =  Kayit.soyad.ToUpper();
            textBox4.Text =  Kayit.dtarih.ToUpper();
            textBox5.Text =  Kayit.tel.ToUpper();

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }
        private void Yazdir()
        {
            PrintDocument pd = new PrintDocument();
            pd.PrintPage += new PrintPageEventHandler(this.YazdirmaIslemi);

            PrintDialog pdialog = new PrintDialog();
            pdialog.Document = pd;

            if (pdialog.ShowDialog() == DialogResult.OK)
            {
                pd.Print();
            }
        }
        private void button1_Click(object sender, EventArgs e)
        {
            Yazdir();
        }
        private void YazdirmaIslemi(object sender, PrintPageEventArgs e)
        {
            Bitmap bmp = new Bitmap(this.Width, this.Height);
            this.DrawToBitmap(bmp, new Rectangle(0, 0, this.Width, this.Height));
            e.Graphics.DrawImage(bmp, 0, 0);
        }
    }
}
