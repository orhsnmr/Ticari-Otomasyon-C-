using System;
using System.Windows.Forms;
using MySql.Data.MySqlClient;
using System.Data;

namespace TicariOtomasyon
{
    public partial class FaturaUrunDetay : Form
    {
        public FaturaUrunDetay()
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
        public string id;
        public  void listele()
        {
            
            con.bag.Open();
            dt.Clear();
            da = new MySqlDataAdapter("SELECT *FROM tbl_faturadetay WHERE FATURAID='"+ id +"'", con.bag);
            da.Fill(dt);
            dgvFaturaUrunDetayveri.DataSource = dt;
            con.bag.Close();
        }

        private void FaturaUrunDetay_Load(object sender, EventArgs e)
        {
            listele();
        }
        FaturaUrunDuzenleme faturaud;
        private void dgvFaturaUrunDetayveri_DoubleClick(object sender, EventArgs e)
        {
            faturaud = new FaturaUrunDuzenleme();
            string urunID,urunAdi, Miktar, Fiyat, Tutar;
            int secilenalan = dgvFaturaUrunDetayveri.SelectedCells[0].RowIndex;
            urunID = dgvFaturaUrunDetayveri.Rows[secilenalan].Cells[0].Value.ToString();
            urunAdi = dgvFaturaUrunDetayveri.Rows[secilenalan].Cells[1].Value.ToString();
            Miktar = dgvFaturaUrunDetayveri.Rows[secilenalan].Cells[2].Value.ToString();
            Fiyat = dgvFaturaUrunDetayveri.Rows[secilenalan].Cells[3].Value.ToString();
            Tutar = dgvFaturaUrunDetayveri.Rows[secilenalan].Cells[4].Value.ToString();
            faturaud.urunID = urunID;
            faturaud.urunAdi = urunAdi;
            faturaud.Miktar = Miktar;
            faturaud.Fiyat = Fiyat;
            faturaud.Tutar = Tutar;
            faturaud.Show();
        }
    }
}
