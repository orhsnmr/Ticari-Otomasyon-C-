using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySql.Data.MySqlClient;

namespace TicariOtomasyon
{
    public partial class FormAnaSayfa : Form
    {
        public FormAnaSayfa()
        {
            InitializeComponent();
        }
        MySqlDataAdapter da = new MySqlDataAdapter();
        DataTable dt = new DataTable();
        MySQLConnection con = new MySQLConnection();
        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        void Stoklar()
        {
            con.bag.Open();
            da = new MySqlDataAdapter("SELECT URUNAD,SUM(ADET) as 'ADET' From tbl_urunler GROUP BY URUNAD HAVING SUM(ADET)<= 20 ORDER BY SUM(ADET)",con.bag);
            da.Fill(dt);
            dgvAzalanStok.DataSource = dt;
            con.bag.Close();
        }
        void Ajanda()
        {
            DataTable dt1 = new DataTable();
            con.bag.Open();
            da = new MySqlDataAdapter("SELECT TARIH,SAAT,BASLIK FROM tbl_notlar ORDER BY NOTID DESC LIMIT 5", con.bag);
            da.Fill(dt1);
            dgvNote.DataSource = dt1;
            con.bag.Close();
        }
        void FirmaHaraket()
        {
            DataTable dt = new DataTable();
            con.bag.Open();
            MySqlDataAdapter da1 = new MySqlDataAdapter("SELECT c.URUNAD,a.ADET,d.AD AS 'FİRMA AD',a.FIYAT,a.TOPLAM from tbl_firmaharaketler" +
                "  a INNER JOIN tbl_urunler c on a.URUNID= c.ID INNER JOIN tbl_firma d on a.FIRMA = d.ID", con.bag);
            da1.Fill(dt);
            dgvFirmaVeri.DataSource = dt;
            con.bag.Close();
        }
        void Fihrist()
        {
            DataTable dt = new DataTable();
            con.bag.Open();
            MySqlDataAdapter da1 = new MySqlDataAdapter("SELECT AD,TELEFON FROM tbl_firma", con.bag);
            da1.Fill(dt);
            dgvFihrist.DataSource = dt;
            con.bag.Close();
        }
        private void FormAnaSayfa_Load(object sender, EventArgs e)
        {
            Stoklar();
            FirmaHaraket();
            Ajanda();
            Fihrist();
            webBrowser1.Navigate("https://www.tcmb.gov.tr/kurlar/today.xml");
        }
    }
}
