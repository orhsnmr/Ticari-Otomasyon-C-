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
    public partial class Urunler : Form
    {
        public Urunler()
        {
            InitializeComponent();
        }
        private void btnClose_Click_1(object sender, EventArgs e)
        {
            this.Close();
        }
        MySqlDataAdapter da = new MySqlDataAdapter();
        DataTable dt = new DataTable();
        MySQLConnection con = new MySQLConnection();
        void listele()
        {
            con.bag.Open();
            dt.Clear();
            da = new MySqlDataAdapter("SELECT *FROM tbl_urunler", con.bag);
            da.Fill(dt);
            dgvUrunveri.DataSource = dt;
            con.bag.Close();
        }
        private void Urunler_Load(object sender, EventArgs e)
        {
            listele();
        }
        MySqlCommand command;
        public void alert(string msg, FormAlert.enumType type)
        {
            FormAlert frm = new FormAlert();
            frm.showAlert(msg, type);
        }
        private void btnSave_Click(object sender, EventArgs e)
        {
            con.bag.Open();
            if (txtAd.Text == "" || txtMarka.Text == "" || txtModel.Text == "" || txtYil.Text == "" || nudAdet.Text == "" || txtAlisF.Text == "" || txtSatisF.Text == "" || txtDetay.Text == "")
            {
                this.alert("Boş Alan Bırakmayınız", FormAlert.enumType.warning);
            }
            else
            {
                command = new MySqlCommand("INSERT INTO tbl_urunler(URUNAD,MARKA,MODEL,YIL,ADET,ALISFIYAT,SATISFIYAT,DETAY) VALUES(@URUNAD,@MARKA,@MODEL,@YIL,@ADET,@ALISFIYAT,@SATISFIYAT,@DETAY)", con.bag);
                command.Parameters.AddWithValue("@URUNAD", txtAd.Text);
                command.Parameters.AddWithValue("@MARKA", txtMarka.Text);
                command.Parameters.AddWithValue("@MODEL", txtModel.Text);
                command.Parameters.AddWithValue("@YIL", txtYil.Text);
                command.Parameters.AddWithValue("@ADET", int.Parse((nudAdet.Value).ToString()));
                command.Parameters.AddWithValue("@ALISFIYAT", decimal.Parse(txtAlisF.Text));
                command.Parameters.AddWithValue("@SATISFIYAT", decimal.Parse(txtSatisF.Text));
                command.Parameters.AddWithValue("@DETAY", txtDetay.Text);
                command.ExecuteNonQuery();
                con.bag.Close();
                this.alert("Kayıt Başarılı", FormAlert.enumType.success);
                listele();
                TxtTemizle();
            }
        }

        private void TxtTemizle()
        {
            txtAd.Text = ""; txtAlisF.Text = ""; txtDetay.Text = ""; txtMarka.Text = ""; txtModel.Text = ""; txtSatisF.Text = "";
            txtYil.Text = ""; nudAdet.Text = "";
        }
        void KayıtSil(int numara)
        {
            string sql = "DELETE FROM tbl_urunler where ID=@ID";
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
                foreach (DataGridViewRow drow in dgvUrunveri.SelectedRows)
                {
                    int numara = Convert.ToInt32(drow.Cells[0].Value);
                    KayıtSil(numara);
                }
                this.alert("Silme Başarılı", FormAlert.enumType.success);
                listele();
                TxtTemizle();
            }
            
           


        }

        private void dgvUrunveri_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            string ad, marka, model, yil, adet, alisF, satisF, detay;
            int secilenalan = dgvUrunveri.SelectedCells[0].RowIndex;
            ad = dgvUrunveri.Rows[secilenalan].Cells[1].Value.ToString();
            marka = dgvUrunveri.Rows[secilenalan].Cells[2].Value.ToString();
            model = dgvUrunveri.Rows[secilenalan].Cells[3].Value.ToString();
            yil = dgvUrunveri.Rows[secilenalan].Cells[4].Value.ToString();
            adet = dgvUrunveri.Rows[secilenalan].Cells[5].Value.ToString();
            alisF = dgvUrunveri.Rows[secilenalan].Cells[6].Value.ToString();
            satisF = dgvUrunveri.Rows[secilenalan].Cells[7].Value.ToString();
            detay = dgvUrunveri.Rows[secilenalan].Cells[8].Value.ToString();
            txtAd.Text = ad; txtModel.Text = model; txtMarka.Text = marka; txtYil.Text = yil;
            nudAdet.Text = adet; txtAlisF.Text = alisF; txtSatisF.Text = satisF; txtDetay.Text = detay;
        }

        private void btnInfo_Click(object sender, EventArgs e)
        {
            if (txtAd.Text == "" && txtMarka.Text == "" && txtModel.Text == "")
            {
                this.alert("Ürün adı,marka veya modeline göre arama yapınız.", FormAlert.enumType.info);
            }
            else
            {
                con.bag.Open();
                command = new MySqlCommand("SELECT *FROM tbl_urunler where URUNAD LIKE @URUNAD OR MARKA LIKE @MARKA OR MODEL LIKE @MODEL    ", con.bag);
                command.Parameters.AddWithValue("@URUNAD", txtAd.Text);
                command.Parameters.AddWithValue("@MARKA", txtMarka.Text);
                command.Parameters.AddWithValue("@MODEL", txtModel.Text);
                da = new MySqlDataAdapter(command);
                DataSet ds = new DataSet();
                da.Fill(ds);
                dgvUrunveri.DataSource = ds.Tables[0];
                con.bag.Close();
            }

        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            if (txtAd.Text == "" || txtMarka.Text == "" || txtModel.Text == "" || txtYil.Text == "" || nudAdet.Text == "" || txtAlisF.Text == "" || txtSatisF.Text == "" || txtDetay.Text == "")
            {
                this.alert("Boş Alan Bırakmayınız", FormAlert.enumType.warning);
            }
            else
            {
                con.bag.Open();
                command = new MySqlCommand("UPDATE tbl_urunler SET MARKA =@MARKA,MODEL=@MODEL,YIL=@YIL,ADET=@ADET,ALISFIYAT=@ALISFIYAT,SATISFIYAT=@SATISFIYAT,DETAY=@DETAY WHERE URUNAD = @URUNAD ", con.bag);
                command.Parameters.AddWithValue("@URUNAD", txtAd.Text);
                command.Parameters.AddWithValue("@MARKA", txtMarka.Text);
                command.Parameters.AddWithValue("@MODEL", txtModel.Text);
                command.Parameters.AddWithValue("@YIL", txtYil.Text);
                command.Parameters.AddWithValue("@ADET", int.Parse((nudAdet.Value).ToString()));
                command.Parameters.AddWithValue("@ALISFIYAT", decimal.Parse(txtAlisF.Text));
                command.Parameters.AddWithValue("@SATISFIYAT", decimal.Parse(txtSatisF.Text));
                command.Parameters.AddWithValue("@DETAY", txtDetay.Text);
                command.ExecuteNonQuery();
                con.bag.Close();
                this.alert("Kayıt Güncelleme Başarılı", FormAlert.enumType.success);
                listele();
                TxtTemizle();
            }
        }
    }
}
