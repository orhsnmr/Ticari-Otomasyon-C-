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
    public partial class Fatura : Form
    {
        public Fatura()
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
        MySqlCommand command;
        void listele()
        {
            con.bag.Open();
            dt.Clear();
            da = new MySqlDataAdapter("SELECT *FROM tbl_faturabilgi", con.bag);
            da.Fill(dt);
            dgvFaturaveri.DataSource = dt;
            con.bag.Close();
        }     

        private void Fatura_Load(object sender, EventArgs e)
        {
           
            listele();
        }
        public void alert(string msg, FormAlert.enumType type)
        {
            FormAlert frm = new FormAlert();
            frm.showAlert(msg, type);
        }
        void txtTemizle()
        {
            txtSeriNo.Text = ""; txtSıraNo.Text = ""; txtTeslimAlan.Text = ""; txtTeslimEden.Text = ""; dtpTarih.Text = "";
            txtVergiD.Text = ""; txtAlici.Text = ""; txtSaat.Text = "";
            txtUrunAdi.Text = ""; txtTutar.Text = ""; txtFiyat.Text = ""; txtFaturaID.Text = ""; txtMiktar.Text = "";
        }
        private void btnSave_Click(object sender, EventArgs e)
        {
            if (txtSeriNo.Text == "" || txtSıraNo.Text == "" || txtTeslimAlan.Text == "" || txtTeslimEden.Text == "" || dtpTarih.Text == "" || txtVergiD.Text == "" || txtAlici.Text == "" || txtSaat.Text == "")
            {
                this.alert("Boş alan bırakmayınız.", FormAlert.enumType.warning);
            }
            else
            {
                if (txtFaturaID.Text == "")
                {
                    con.bag.Open();
                    command = new MySqlCommand("INSERT INTO tbl_faturabilgi(SERI, SIRANO, TARIH, SAAT, VERGIDAIRE, ALICI, TESLIMEDEN, TESLIMALAN) VALUES(@SERI,@SIRANO,@TARIH,@SAAT,@VERGIDAIRE,@ALICI,@TESLIMEDEN,@TESLIMALAN)", con.bag);
                    command.Parameters.AddWithValue("@SERI", txtSeriNo.Text);
                    command.Parameters.AddWithValue("@SIRANO", txtSıraNo.Text);
                    command.Parameters.AddWithValue("@TARIH", dtpTarih.Value);
                    command.Parameters.AddWithValue("@SAAT", txtSaat.Text);
                    command.Parameters.AddWithValue("@VERGIDAIRE", txtVergiD.Text);
                    command.Parameters.AddWithValue("@ALICI", txtAlici.Text);
                    command.Parameters.AddWithValue("@TESLIMEDEN", txtTeslimEden.Text);
                    command.Parameters.AddWithValue("@TESLIMALAN", txtTeslimAlan.Text);
                    command.ExecuteNonQuery();
                    con.bag.Close();
                    this.alert("Kayıt Başarılı", FormAlert.enumType.success);
                    listele();
                    txtTemizle();
                }
            }

            if (txtFaturaID.Text != "")
            {
                double miktar, tutar, fiyat;
                fiyat = Convert.ToDouble(txtFiyat.Text);
                miktar = Convert.ToDouble(txtMiktar.Text);
                tutar = fiyat * miktar;
                txtTutar.Text = tutar.ToString();
                con.bag.Open();
                MySqlCommand command2 = new MySqlCommand("INSERT INTO tbl_faturadetay(URUNAD,MIKTAR,FIYAT,TUTAR,FATURAID) VALUES(@URUNAD,@MIKTAR,@FIYAT,@TUTAR,@FATURAID)", con.bag);
                command2.Parameters.AddWithValue("@URUNAD", txtUrunAdi.Text);
                command2.Parameters.AddWithValue("@MIKTAR", txtMiktar.Text);
                command2.Parameters.AddWithValue("@FIYAT", txtFiyat.Text);
                command2.Parameters.AddWithValue("@TUTAR", txtTutar.Text);
                command2.Parameters.AddWithValue("@FATURAID", txtFaturaID.Text);
                command2.ExecuteNonQuery();
                con.bag.Close();
                this.alert("Kayıt Başarılı", FormAlert.enumType.success);
                listele();
                txtTemizle();
            }
        }

        private void dgvFaturaveri_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            string FATURAID, SERI, SIRANO, TARIH, SAAT, VERGIDAIRE, ALICI, TESLIMEDEN, TESLIMALAN;
            int secilenalan = dgvFaturaveri.SelectedCells[0].RowIndex;
            FATURAID = dgvFaturaveri.Rows[secilenalan].Cells[0].Value.ToString();
            SERI = dgvFaturaveri.Rows[secilenalan].Cells[1].Value.ToString();
            SIRANO = dgvFaturaveri.Rows[secilenalan].Cells[2].Value.ToString();
            TARIH = dgvFaturaveri.Rows[secilenalan].Cells[3].Value.ToString();
            SAAT = dgvFaturaveri.Rows[secilenalan].Cells[4].Value.ToString();
            VERGIDAIRE = dgvFaturaveri.Rows[secilenalan].Cells[5].Value.ToString();
            ALICI = dgvFaturaveri.Rows[secilenalan].Cells[6].Value.ToString();
            TESLIMEDEN = dgvFaturaveri.Rows[secilenalan].Cells[7].Value.ToString();
            TESLIMALAN = dgvFaturaveri.Rows[secilenalan].Cells[8].Value.ToString();
            txtSeriNo.Text = SERI; txtSıraNo.Text = SIRANO; txtTeslimAlan.Text = TESLIMALAN; txtTeslimEden.Text = TESLIMEDEN; dtpTarih.Text = TARIH;
            txtVergiD.Text = VERGIDAIRE; txtAlici.Text = ALICI; txtSaat.Text = SAAT; txtID.Text = FATURAID;
        }
        void KayıtSil(int numara)
        {
            string sql = "DELETE FROM tbl_faturabilgi where FATURABILGIID=@ID";
            command = new MySqlCommand(sql, con.bag);
            command.Parameters.AddWithValue("@ID", numara);
            con.bag.Open();
            command.ExecuteNonQuery();
            con.bag.Close();
        }
        private void btnDelete_Click(object sender, EventArgs e)
        {

            DialogResult Secim = new DialogResult();

            Secim = MessageBox.Show("Silmeyi Onaylıyor musunuz?", "SİLME İŞLEMİ", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2);

            if (Secim == DialogResult.Yes)
            {
                foreach (DataGridViewRow drow in dgvFaturaveri.SelectedRows)
                {
                    int numara = Convert.ToInt32(drow.Cells[0].Value);
                    KayıtSil(numara);
                }
                this.alert("Silme Başarılı", FormAlert.enumType.success);
                listele();
                txtTemizle();
            }
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            con.bag.Open();
            command = new MySqlCommand("UPDATE tbl_faturabilgi SET SERI=@SERI, SIRANO=@SIRANO, TARIH=@TARIH, SAAT=@SAAT, VERGIDAIRE=@VERGIDAIRE, ALICI=@ALICI, TESLIMEDEN=@TESLIMEDEN, TESLIMALAN=@TESLIMEDEN WHERE FATURABILGIID=@ID", con.bag);
            command.Parameters.AddWithValue("@ID", txtID.Text);
            command.Parameters.AddWithValue("@SERI", txtSeriNo.Text);
            command.Parameters.AddWithValue("@SIRANO", txtSıraNo.Text);
            command.Parameters.AddWithValue("@TARIH", dtpTarih.Value);
            command.Parameters.AddWithValue("@SAAT", txtSaat.Text);
            command.Parameters.AddWithValue("@VERGIDAIRE", txtVergiD.Text);
            command.Parameters.AddWithValue("@ALICI", txtAlici.Text);
            command.Parameters.AddWithValue("@TESLIMEDEN", txtTeslimEden.Text);
            command.Parameters.AddWithValue("@TESLIMALAN", txtTeslimAlan.Text);
            command.ExecuteNonQuery();
            con.bag.Close();
            this.alert("Kayıt Güncelleme Başarılı", FormAlert.enumType.success);
            listele();
            txtTemizle();
            con.bag.Close();
        }
        FaturaUrunDetay fud;
        private void dgvFaturaveri_DoubleClick(object sender, EventArgs e)
        {
            fud = new FaturaUrunDetay();
            fud.id = txtID.Text;
            fud.Show();
            
        }

        private void btnInfo_Click(object sender, EventArgs e)
        {
            con.bag.Open();
            command = new MySqlCommand("SELECT *FROM tbl_faturabilgi where SERI=@SERI OR SIRANO=@SIRANO OR TARIH=@TARIH OR SAAT=@SAAT OR VERGIDAIRE=@VERGIDAIRE OR ALICI=@ALICI OR TESLIMEDEN=@TESLIMEDEN OR TESLIMALAN=@TESLIMEDEN", con.bag);
            command.Parameters.AddWithValue("@SERI", txtSeriNo.Text);
            command.Parameters.AddWithValue("@SIRANO", txtSıraNo.Text);
            command.Parameters.AddWithValue("@TARIH", dtpTarih.Value.Date);
            command.Parameters.AddWithValue("@SAAT", txtSaat.Text);
            command.Parameters.AddWithValue("@VERGIDAIRE", txtVergiD.Text);
            command.Parameters.AddWithValue("@ALICI", txtAlici.Text);
            command.Parameters.AddWithValue("@TESLIMEDEN", txtTeslimEden.Text);
            command.Parameters.AddWithValue("@TESLIMALAN", txtTeslimAlan.Text);
            da = new MySqlDataAdapter(command);
            DataSet ds = new DataSet();
            da.Fill(ds);
            dgvFaturaveri.DataSource = ds.Tables[0];
            con.bag.Close();
        }
    }
}
