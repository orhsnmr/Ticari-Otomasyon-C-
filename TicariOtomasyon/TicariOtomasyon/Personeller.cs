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
    public partial class Personeller : Form
    {
        public Personeller()
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
            da = new MySqlDataAdapter("SELECT *FROM tbl_personeller", con.bag);
            da.Fill(dt);
            dgvPersonelVeri.DataSource = dt;
            con.bag.Close();
        }
        private void ilListele()
        {
            con.bag.Open();
            command = new MySqlCommand("SELECT IL FROM tbl_iller", con.bag);
            MySqlDataReader dr = command.ExecuteReader();
            while (dr.Read())
            {
                cbil.Items.Add(dr[0]);
            }
            con.bag.Close();
        }
        private void cbil_SelectedIndexChanged(object sender, EventArgs e)
        {
            con.bag.Open();
            command = new MySqlCommand("SELECT ILCEAD FROM tbl_ilceler WHERE ILID=@ILID", con.bag);
            command.Parameters.AddWithValue("@ILID", cbil.SelectedIndex + 1);
            cbilce.Items.Clear();
            MySqlDataReader dr = command.ExecuteReader();
            while (dr.Read())
            {
                cbilce.Items.Add(dr[0]);
            }
            con.bag.Close();
        }

        private void Personeller_Load(object sender, EventArgs e)
        {
            listele();
            ilListele();
        }
        public void alert(string msg, FormAlert.enumType type)
        {
            FormAlert frm = new FormAlert();
            frm.showAlert(msg, type);
        }

        void txtTemizle()
        {
            txtAd.Text = ""; txtSoyad.Text = ""; txtMail.Text = ""; txtAdres.Text = ""; txtTC.Text = ""; txtGorev.Text = "";
            txtTel1.Text = ""; txtAdres.Text = ""; cbil.Text = ""; cbilce.Text = ""; 
        }
        private void btnSave_Click(object sender, EventArgs e)
        {
            if (txtAd.Text == "" || txtSoyad.Text == "" || txtMail.Text == "" || txtAdres.Text == "" || txtTC.Text == "" || txtGorev.Text == "" || txtTel1.Text == "" || txtAdres.Text == "" || cbil.Text == "" || cbilce.Text == "")
            {
                this.alert("Boş Alan Bırakmayınız", FormAlert.enumType.warning);
            }
            else
            {
                con.bag.Open();
                command = new MySqlCommand("INSERT INTO tbl_personeller(AD,SOYAD,TELEFON,TC,MAIL,IL,ILCE,ADRES,GOREV) VALUES(@AD,@SOYAD,@TELEFON,@TC,@MAIL,@IL,@ILCE,@ADRES,@GOREV)", con.bag);
                command.Parameters.AddWithValue("@AD", txtAd.Text);
                command.Parameters.AddWithValue("@SOYAD", txtSoyad.Text);
                command.Parameters.AddWithValue("@TELEFON", txtTel1.Text);
                command.Parameters.AddWithValue("@TC", txtTC.Text);
                command.Parameters.AddWithValue("@MAIL", txtMail.Text);
                command.Parameters.AddWithValue("@IL", cbil.Text);
                command.Parameters.AddWithValue("@ILCE", cbilce.Text);
                command.Parameters.AddWithValue("@ADRES", txtAdres.Text);
                command.Parameters.AddWithValue("@GOREV", txtGorev.Text);
                command.ExecuteNonQuery();
                con.bag.Close();
                this.alert("Kayıt Başarılı", FormAlert.enumType.success);
                listele();
                txtTemizle();
            }
        }

        private void dgvMusteriVeri_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            string AD, SOYAD, TELEFON, TC, MAIL, IL, ILCE, ADRES, GOREV;
            int secilenalan = dgvPersonelVeri.SelectedCells[0].RowIndex;
            AD = dgvPersonelVeri.Rows[secilenalan].Cells[1].Value.ToString();
            SOYAD = dgvPersonelVeri.Rows[secilenalan].Cells[2].Value.ToString();
            TELEFON = dgvPersonelVeri.Rows[secilenalan].Cells[3].Value.ToString();           
            TC = dgvPersonelVeri.Rows[secilenalan].Cells[4].Value.ToString();
            MAIL = dgvPersonelVeri.Rows[secilenalan].Cells[5].Value.ToString();
            IL = dgvPersonelVeri.Rows[secilenalan].Cells[6].Value.ToString();
            ILCE = dgvPersonelVeri.Rows[secilenalan].Cells[7].Value.ToString();
            ADRES = dgvPersonelVeri.Rows[secilenalan].Cells[8].Value.ToString();
            GOREV = dgvPersonelVeri.Rows[secilenalan].Cells[9].Value.ToString();
            txtAd.Text = AD; txtSoyad.Text = SOYAD; txtTel1.Text = TELEFON; txtTC.Text = TC;
            txtMail.Text = MAIL; cbil.Text = IL; cbilce.Text = ILCE; txtAdres.Text = ADRES; txtGorev.Text = GOREV;
            txtTC.Enabled = false;
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            if (txtAd.Text == "" || txtSoyad.Text == "" || txtMail.Text == "" || txtAdres.Text == "" || txtTC.Text == "" || txtGorev.Text == "" || txtTel1.Text == "" || txtAdres.Text == "" || cbil.Text == "" || cbilce.Text == "")
            {
                this.alert("Boş Alan Bırakmayınız", FormAlert.enumType.warning);
            }
            else
            {
                con.bag.Open();
                command = new MySqlCommand("UPDATE tbl_personeller SET AD=@AD,SOYAD = @SOYAD,TELEFON=@TELEFON,TC=@TC,MAIL=@MAIL,IL=@IL,ILCE=@ILCE,ADRES=@ADRES,GOREV=@GOREV", con.bag);
                command.Parameters.AddWithValue("@AD", txtAd.Text);
                command.Parameters.AddWithValue("@SOYAD", txtSoyad.Text);
                command.Parameters.AddWithValue("@TELEFON", txtTel1.Text);
                command.Parameters.AddWithValue("@TC", txtTC.Text);
                command.Parameters.AddWithValue("@MAIL", txtMail.Text);
                command.Parameters.AddWithValue("@IL", cbil.Text);
                command.Parameters.AddWithValue("@ILCE", cbilce.Text);
                command.Parameters.AddWithValue("@ADRES", txtAdres.Text);
                command.Parameters.AddWithValue("@GOREV", txtGorev.Text);
                command.ExecuteNonQuery();
                con.bag.Close();
                this.alert("Kayıt Güncelleme Başarılı", FormAlert.enumType.success);
                listele();
                txtTemizle();
            }
        }
        void KayıtSil(int numara)
        {
            string sql = "DELETE FROM tbl_personeller where ID=@ID";
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
                foreach (DataGridViewRow drow in dgvPersonelVeri.SelectedRows)
                {
                    int numara = Convert.ToInt32(drow.Cells[0].Value);
                    KayıtSil(numara);
                }
                this.alert("Silme Başarılı", FormAlert.enumType.success);
                listele();
                txtTemizle();
            };
        }

        private void btnInfo_Click(object sender, EventArgs e)
        {
            if (txtTC.Text == "" && txtAd.Text == "")
            {
                this.alert("TC veya AD'A göre ARATIN", FormAlert.enumType.info);
            }
            else
            {
                con.bag.Open();
                command = new MySqlCommand("SELECT *FROM tbl_personeller where AD LIKE @AD OR TC LIKE @TC OR GOREV LIKE @GOREV", con.bag);
                command.Parameters.AddWithValue("@AD", txtAd.Text);
                command.Parameters.AddWithValue("@TC", txtTC.Text);
                command.Parameters.AddWithValue("@GOREV", txtGorev.Text);
                da = new MySqlDataAdapter(command);
                DataSet ds = new DataSet();
                da.Fill(ds);
                dgvPersonelVeri.DataSource = ds.Tables[0];
                con.bag.Close();
            }
        }
    }
}
