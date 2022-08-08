using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TicariOtomasyon
{
    public partial class Haraketler : Form
    {
        public Haraketler()
        {
            InitializeComponent();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        MySqlDataAdapter da = new MySqlDataAdapter();
        DataTable dt = new DataTable();
        MySQLConnection con = new MySQLConnection();
  
        void listele()
        {
            DataTable dt = new DataTable();
            con.bag.Open();
            dt.Clear();
            MySqlDataAdapter da1 = new MySqlDataAdapter("SELECT a.HARAKETID,c.URUNAD,a.ADET,b.AD, b.SOYAD, d.AD AS 'FİRMA AD',a.FIYAT,a.FATURAID,a.TOPLAM,a.TARIH,a.NOTLAR from tbl_firmaharaketler" +
                "  a INNER JOIN tbl_personeller b" +
                " on  a.PERSONEL = b.ID INNER JOIN tbl_urunler c on a.URUNID= c.ID INNER JOIN tbl_firma d on a.FIRMA = d.ID", con.bag);
            da1.Fill(dt);
            dgvFirmaveri.DataSource = dt;
            con.bag.Close();
        }
        void Musterilistele()
        {
            con.bag.Open();
            dt.Clear();
            da = new MySqlDataAdapter("SELECT a.ID,c.URUNAD,a.ADET,b.AD, b.SOYAD, d.AD AS 'MUSTERİ AD',d.SOYAD AS 'MUSTERİ SOYAD',d.ADRES,a.FIYAT,a.FATURAID,a.TOPLAM,a.TARIH,a.NOTLAR " +
                "from tbl_musteriharaket  a INNER JOIN tbl_personeller b on  a.PERSONEL = b.ID INNER JOIN tbl_urunler c on" +
                " a.URUNID= c.ID INNER JOIN tbl_musteriler d on a.Musteri = d.ID", con.bag);
            da.Fill(dt);
            dgvMusteriVeri.DataSource = dt;
            con.bag.Close();
        }
        void Stok()
        {
            DataTable dt = new DataTable();
            con.bag.Open();
            dt.Clear();
            da = new MySqlDataAdapter("SELECT ID,STOKTUR, STOKADET, TARIH, SAAT,VERGIDAIRE FROM tbl_stoklar", con.bag);
            da.Fill(dt);
            dgvStokHaraket.DataSource = dt;
            con.bag.Close();
        }

        private void Haraketler_Load(object sender, EventArgs e)
        {
            Stok();
            Musterilistele();
            listele();

        }

        private void pnlMenu_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}
