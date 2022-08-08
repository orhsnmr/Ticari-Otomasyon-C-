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
    public partial class Notlar : Form
    {
        public Notlar()
        {
            InitializeComponent();
        }
        MySqlDataAdapter da = new MySqlDataAdapter();
        DataTable dt = new DataTable();
        MySQLConnection con = new MySQLConnection();
        MySqlCommand command;
        void listele()
        {
            con.bag.Open();
            dt.Clear();
            da = new MySqlDataAdapter("SELECT *FROM tbl_notlar", con.bag);
            da.Fill(dt);
            dgvNotVeri.DataSource = dt;
            con.bag.Close();
        }
        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        public void alert(string msg, FormAlert.enumType type)
        {
            FormAlert frm = new FormAlert();
            frm.showAlert(msg, type);
        }
        void txttemizle()
        {
            dtpTarih.Text = ""; txtSaat.Text = ""; txtBaslik.Text = ""; txtDetay.Text = ""; txtOlusturan.Text = ""; txtHitap.Text = "";
        }
        private void btnSave_Click(object sender, EventArgs e)
        {
            con.bag.Open();
            command = new MySqlCommand("INSERT INTO tbl_notlar(TARIH,SAAT,BASLIK,DETAY,OLUSTURAN,HITAP) VALUES(@TARIH,@SAAT,@BASLIK,@DETAY,@OLUSTURAN,@HITAP)", con.bag);
            command.Parameters.AddWithValue("@TARIH", dtpTarih.Value);
            command.Parameters.AddWithValue("@SAAT", txtSaat.Text);
            command.Parameters.AddWithValue("@BASLIK", txtBaslik.Text);
            command.Parameters.AddWithValue("@DETAY", txtDetay.Text);
            command.Parameters.AddWithValue("@OLUSTURAN", txtOlusturan.Text);
            command.Parameters.AddWithValue("@HITAP", txtHitap.Text);
            command.ExecuteNonQuery();
            con.bag.Close();
            this.alert("Kayıt başarılı", FormAlert.enumType.success);
            listele();
            txttemizle();
        }

        private void Notlar_Load(object sender, EventArgs e)
        {
            listele();
            this.alert("Tarih,oluşturan kişi ve hitap'a göre arama yapınız.", FormAlert.enumType.info);
        }

        private void dgvNotVeri_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            string TARIH, SAAT, BASLIK, DETAY, OLUSTURAN, HITAP, ID;
            int secilenalan = dgvNotVeri.SelectedCells[0].RowIndex;
            ID = dgvNotVeri.Rows[secilenalan].Cells[0].Value.ToString();
            TARIH = dgvNotVeri.Rows[secilenalan].Cells[1].Value.ToString();
            SAAT = dgvNotVeri.Rows[secilenalan].Cells[2].Value.ToString();
            BASLIK = dgvNotVeri.Rows[secilenalan].Cells[3].Value.ToString();
            DETAY = dgvNotVeri.Rows[secilenalan].Cells[4].Value.ToString();
            OLUSTURAN = dgvNotVeri.Rows[secilenalan].Cells[5].Value.ToString();
            HITAP = dgvNotVeri.Rows[secilenalan].Cells[6].Value.ToString();
            dtpTarih.Text = TARIH; txtSaat.Text = SAAT; txtBaslik.Text = BASLIK; txtOlusturan.Text = OLUSTURAN;
            txtHitap.Text = HITAP; txtDetay.Text = DETAY; txtID.Text = ID;
        }
        void KayıtSil(int numara)
        {
            string sql = "DELETE FROM tbl_notlar where NOTID=@ID";
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
                foreach (DataGridViewRow drow in dgvNotVeri.SelectedRows)
                {
                    int numara = Convert.ToInt32(drow.Cells[0].Value);
                    KayıtSil(numara);
                }
                this.alert("Silme Başarılı", FormAlert.enumType.success);
                listele();
                txttemizle();
            }
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            con.bag.Open();
            command = new MySqlCommand("UPDATE tbl_notlar SET TARIH=@TARIH,SAAT=@SAAT,BASLIK=@BASLIK,DETAY=@DETAY,OLUSTURAN=@OLUSTURAN,HITAP=@HITAP WHERE NOTID=@ID", con.bag);
            command.Parameters.AddWithValue("@ID", txtID.Text);
            command.Parameters.AddWithValue("@TARIH", dtpTarih.Value);
            command.Parameters.AddWithValue("@SAAT", txtSaat.Text);
            command.Parameters.AddWithValue("@BASLIK", txtBaslik.Text);
            command.Parameters.AddWithValue("@DETAY", txtDetay.Text);
            command.Parameters.AddWithValue("@OLUSTURAN", txtOlusturan.Text);
            command.Parameters.AddWithValue("@HITAP", txtHitap.Text);
            command.ExecuteNonQuery();
            con.bag.Close();
            this.alert("Kayıt güncelleme başarılı", FormAlert.enumType.success);
            listele();
            txttemizle();
        }

        private void btnInfo_Click(object sender, EventArgs e)
        {
            con.bag.Open();
            command = new MySqlCommand("SELECT *FROM tbl_notlar where TARIH =@TARIH OR OLUSTURAN LIKE @OLUSTURAN OR HITAP LIKE @HITAP", con.bag);
            command.Parameters.AddWithValue("@TARIH", dtpTarih.Value.Date);
            command.Parameters.AddWithValue("@OLUSTURAN", txtOlusturan.Text);
            command.Parameters.AddWithValue("@HITAP", txtHitap.Text);
            da = new MySqlDataAdapter(command);
            DataSet ds = new DataSet();
            da.Fill(ds);
            dgvNotVeri.DataSource = ds.Tables[0];
            con.bag.Close();
        }
        Notdetay notdetay;
        private void dgvNotVeri_DoubleClick(object sender, EventArgs e)
        {
            notdetay = new Notdetay();
            int secilenalan = dgvNotVeri.SelectedCells[0].RowIndex;
            notdetay.detay = dgvNotVeri.Rows[secilenalan].Cells[4].Value.ToString();
            notdetay.Show();
        }
    }
}
