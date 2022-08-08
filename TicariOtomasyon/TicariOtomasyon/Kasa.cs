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
    public partial class Kasa : Form
    {
        public Kasa()
        {
            InitializeComponent();
        }
        MySqlDataAdapter da = new MySqlDataAdapter();
        DataTable dt = new DataTable();
        MySQLConnection con = new MySQLConnection();
        void Musterilistele()
        {
            con.bag.Open();
            dt.Clear();
            da = new MySqlDataAdapter("SELECT a.ID,c.URUNAD,a.ADET,b.AD, b.SOYAD, d.AD,d.SOYAD,d.ADRES,a.FIYAT,a.FATURAID,a.TOPLAM,a.TARIH,a.NOTLAR " +
                "from tbl_musteriharaket  a INNER JOIN tbl_personeller b on  a.PERSONEL = b.ID INNER JOIN tbl_urunler c on" +
                " a.URUNID= c.ID INNER JOIN tbl_musteriler d on a.Musteri = d.ID", con.bag);
            da.Fill(dt);
            dgvKasaGiris.DataSource = dt;
            con.bag.Close();
        }
        void listele()
        {
            DataTable dt = new DataTable();
            con.bag.Open();
            dt.Clear();
            MySqlDataAdapter da1 = new MySqlDataAdapter("SELECT a.HARAKETID,c.URUNAD,a.ADET,b.AD, b.SOYAD, d.AD,a.FIYAT,a.FATURAID,a.TOPLAM,a.TARIH,a.NOTLAR from tbl_firmaharaketler" +
                "  a INNER JOIN tbl_personeller b" +
                " on  a.PERSONEL = b.ID INNER JOIN tbl_urunler c on a.URUNID= c.ID INNER JOIN tbl_firma d on a.FIRMA = d.ID", con.bag);
            da1.Fill(dt);
            dataGridView1.DataSource = dt;
            con.bag.Close();
        }

        void kasacikis()
        {
            DataTable dt = new DataTable();
            con.bag.Open();
            dt.Clear();
            MySqlDataAdapter da1 = new MySqlDataAdapter("SELECT * FROM tbl_giderler", con.bag);
            da1.Fill(dt);
            dgvKasacikis.DataSource = dt;
            con.bag.Close();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        MySqlCommand command;
        MySqlDataReader dr;
        #region  LİSTELEEEEME
        void odeme()
        {
            con.bag.Open();
            command = new MySqlCommand("SELECT (ELEKTRIK+SU+DOGALGAZ+EKSTRA) FROM tbl_giderler ORDER BY ID asc", con.bag);
            dr = command.ExecuteReader();
            while (dr.Read())
            {
                lblOdeme.Text = dr[0].ToString() + " TL";
            }
            con.bag.Close();
        }
        void personelmaas()
        {
            con.bag.Open();
            command = new MySqlCommand("SELECT MAASLAR FROM tbl_giderler ORDER BY ID asc", con.bag);
            dr = command.ExecuteReader();
            while (dr.Read())
            {
                lblPersonelMaas.Text = dr[0].ToString() + " TL";
            }
            con.bag.Close();
        }
        void toplamtutar()
        {
            con.bag.Open();
            command = new MySqlCommand("SELECT SUM(TUTAR) FROM tbl_faturadetay", con.bag);
            dr = command.ExecuteReader();
            while (dr.Read())
            {
                lblToplamTutar.Text = dr[0].ToString() + " TL";
            }
            con.bag.Close();
        }
        void toplammusteri()
        {
            con.bag.Open();
            command = new MySqlCommand("SELECT COUNT(*) FROM tbl_musteriler", con.bag);
            dr = command.ExecuteReader();
            while (dr.Read())
            {
                lblMusteris.Text = dr[0].ToString();
            }
            con.bag.Close();
        }
        void toplamFirma()
        {
            con.bag.Open();
            command = new MySqlCommand("SELECT COUNT(*) FROM tbl_firma", con.bag);
            dr = command.ExecuteReader();
            while (dr.Read())
            {
                lblfirmas.Text = dr[0].ToString();
            }
            con.bag.Close();
        }
        void Firmasehir()
        {
            con.bag.Open();
            command = new MySqlCommand("SELECT COUNT(Distinct(IL)) FROM tbl_firma", con.bag);
            dr = command.ExecuteReader();
            while (dr.Read())
            {
                lblsehirs.Text = dr[0].ToString();
            }
            con.bag.Close();
        }
        void Musterisehir()
        {
            con.bag.Open();
            command = new MySqlCommand("SELECT COUNT(Distinct(IL)) FROM tbl_musteriler", con.bag);
            dr = command.ExecuteReader();
            while (dr.Read())
            {
                lblMusteriSehir.Text = dr[0].ToString();
            }
            con.bag.Close();
        }
        void personel()
        {
            con.bag.Open();
            command = new MySqlCommand("SELECT COUNT(*) FROM tbl_personeller", con.bag);
            dr = command.ExecuteReader();
            while (dr.Read())
            {
                lblpersonels.Text = dr[0].ToString();
            }
            con.bag.Close();
        }
        void stok()
        {
            con.bag.Open();
            command = new MySqlCommand("SELECT sum(ADET) FROM tbl_urunler", con.bag);
            dr = command.ExecuteReader();
            while (dr.Read())
            {
                lblstoks.Text = dr[0].ToString();
            }
            con.bag.Close();
        }
        void CHART1()
        {
            con.bag.Open();
            command = new MySqlCommand("SELECT AY,ELEKTRIK FROM tbl_giderler ORDER BY ID DESC LIMIT 4", con.bag);
            dr = command.ExecuteReader();
            while (dr.Read())
            {
                chartControl1.Series["Aylar"].Points.Add(new DevExpress.XtraCharts.SeriesPoint(dr[0], dr[1]));
            }
            con.bag.Close();
        }
        void CHART2()
        {
            con.bag.Open();
            command = new MySqlCommand("SELECT AY,SU FROM tbl_giderler ORDER BY ID DESC LIMIT 4", con.bag);
            dr = command.ExecuteReader();
            while (dr.Read())
            {
                chartControl2.Series["Aylar"].Points.Add(new DevExpress.XtraCharts.SeriesPoint(dr[0], dr[1]));
            }
            con.bag.Close();
        }
        #endregion

        private void Kasa_Load(object sender, EventArgs e)
        {
            listele();
            Musterilistele();
            //kasa toplam tutar
            toplamtutar();
            //ÖDEME
            odeme();
            //personelmaas
            personelmaas();
            //toplammusteri
            toplammusteri();
            //toplam fşrma
            toplamFirma();
            //firma şehir
            Firmasehir();
            //musteri sehir
            Musterisehir();
            //personel
            personel();
            //stok
            stok();
            //chart 
            CHART1();
            CHART2();
            //kasa cikis
            kasacikis();
        }
    }
}
