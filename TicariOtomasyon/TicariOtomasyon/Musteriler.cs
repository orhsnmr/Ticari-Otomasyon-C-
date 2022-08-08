using System;
using System.Data;
using System.Windows.Forms;
using MySql.Data.MySqlClient;

namespace TicariOtomasyon
{
    public partial class Musteriler : Form
    {
        public Musteriler()
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
            da = new MySqlDataAdapter("SELECT *FROM tbl_musteriler", con.bag);
            da.Fill(dt);
            dgvMusteriVeri.DataSource = dt;
            con.bag.Close();
        }
        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
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
        private void Musteriler_Load(object sender, EventArgs e)
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
            txtAd.Text = ""; txtSoyad.Text = ""; txtMail.Text = ""; txtAdres.Text = ""; txtTC.Text = ""; txtVergiD.Text = "";
            txtTel1.Text = ""; txtAdres.Text = ""; cbil.Text = ""; cbilce.Text = ""; txtTel2.Text = "";
        }
        private void btnSave_Click(object sender, EventArgs e)
        {

            if (txtAd.Text == "" || txtSoyad.Text == "" || txtMail.Text == "" || txtAdres.Text == "" || txtTC.Text == "" || txtVergiD.Text == "" || txtTel1.Text == "" || txtAdres.Text == "" || cbil.Text == "" || cbilce.Text == "")
            {
                this.alert("Boş Alan Bırakmayınız", FormAlert.enumType.warning);
            }
            else
            {
                con.bag.Open();
                command = new MySqlCommand("INSERT INTO tbl_musteriler(AD,SOYAD,TELEFON,TELEFON2,TC,MAIL,IL,ILCE,ADRES,VERGIDAIRE) VALUES(@AD,@SOYAD,@TELEFON,@TELEFON2,@TC,@MAIL,@IL,@ILCE,@ADRES,@VERGIDAIRE)", con.bag);
                command.Parameters.AddWithValue("@AD", txtAd.Text);
                command.Parameters.AddWithValue("@SOYAD", txtSoyad.Text);
                command.Parameters.AddWithValue("@TELEFON", txtTel1.Text);
                command.Parameters.AddWithValue("@TELEFON2", txtTel2.Text);
                command.Parameters.AddWithValue("@TC", txtTC.Text);
                command.Parameters.AddWithValue("@MAIL", txtMail.Text);
                command.Parameters.AddWithValue("@IL", cbil.Text);
                command.Parameters.AddWithValue("@ILCE", cbilce.Text);
                command.Parameters.AddWithValue("@ADRES", txtAdres.Text);
                command.Parameters.AddWithValue("@VERGIDAIRE", txtVergiD.Text);
                command.ExecuteNonQuery();
                con.bag.Close();
                this.alert("Kayıt Başarılı", FormAlert.enumType.success);
                listele();
                txtTemizle();
            }
        }

        private void cbil_SelectedIndexChanged(object sender, EventArgs e)
        {
            con.bag.Open();
            command = new MySqlCommand("SELECT ILCEAD FROM tbl_ilceler WHERE ILID=@ILID", con.bag);
            command.Parameters.AddWithValue("@ILID", cbil.SelectedIndex + 1);
            MySqlDataReader dr = command.ExecuteReader();
            cbilce.Items.Clear();
            while (dr.Read())
            {
                cbilce.Items.Add(dr[0]);
            }
            con.bag.Close();
        }
        void KayıtSil(int numara)
        {
            string sql = "DELETE FROM tbl_musteriler where ID=@ID";
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
                foreach (DataGridViewRow drow in dgvMusteriVeri.SelectedRows)
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
                command = new MySqlCommand("SELECT *FROM tbl_musteriler where AD LIKE @AD OR TC LIKE @TC", con.bag);
                command.Parameters.AddWithValue("@AD", txtAd.Text);
                command.Parameters.AddWithValue("@TC", txtTC.Text);
                da = new MySqlDataAdapter(command);
                DataSet ds = new DataSet();
                da.Fill(ds);
                dgvMusteriVeri.DataSource = ds.Tables[0];
                con.bag.Close();
            }
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            if (txtAd.Text == "" || txtSoyad.Text == "" || txtMail.Text == "" || txtAdres.Text == "" || txtTC.Text == "" || txtVergiD.Text == "" || txtTel1.Text == "" ||  cbil.Text == "" || cbilce.Text == "")
            {
                this.alert("Boş Alan Bırakmayınız", FormAlert.enumType.warning);
            }
            else
            {
                con.bag.Open();
                command = new MySqlCommand("UPDATE tbl_musteriler SET AD=@AD,SOYAD = @SOYAD,TELEFON=@TELEFON,TELEFON2=@TELEFON2,TC=@TC,MAIL=@MAIL,IL=@IL,ILCE=@ILCE,ADRES=@ADRES,VERGIDAIRE=@VERGIDAIRE", con.bag);
                command.Parameters.AddWithValue("@AD", txtAd.Text);
                command.Parameters.AddWithValue("@SOYAD", txtSoyad.Text);
                command.Parameters.AddWithValue("@TELEFON", txtTel1.Text);
                command.Parameters.AddWithValue("@TELEFON2", txtTel2.Text);
                command.Parameters.AddWithValue("@TC", txtTC.Text);
                command.Parameters.AddWithValue("@MAIL", txtMail.Text);
                command.Parameters.AddWithValue("@IL", cbil.Text);
                command.Parameters.AddWithValue("@ILCE", cbilce.Text);
                command.Parameters.AddWithValue("@ADRES", txtAdres.Text);
                command.Parameters.AddWithValue("@VERGIDAIRE", txtVergiD.Text);
                command.ExecuteNonQuery();
                con.bag.Close();
                this.alert("Kayıt Güncelleme Başarılı", FormAlert.enumType.success);
                listele();
                txtTemizle();
                txtTC.Enabled = true;
            }
        }
     
        private void dgvMusteriVeri_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            string AD, SOYAD, TELEFON, TELEFON2, TC, MAIL, IL, ILCE, ADRES, VERGIDAIRE;
            int secilenalan = dgvMusteriVeri.SelectedCells[0].RowIndex;
            AD = dgvMusteriVeri.Rows[secilenalan].Cells[1].Value.ToString();
            SOYAD = dgvMusteriVeri.Rows[secilenalan].Cells[2].Value.ToString();
            TELEFON = dgvMusteriVeri.Rows[secilenalan].Cells[3].Value.ToString();
            TELEFON2 = dgvMusteriVeri.Rows[secilenalan].Cells[4].Value.ToString();
            TC = dgvMusteriVeri.Rows[secilenalan].Cells[5].Value.ToString();
            MAIL = dgvMusteriVeri.Rows[secilenalan].Cells[6].Value.ToString();
            IL = dgvMusteriVeri.Rows[secilenalan].Cells[7].Value.ToString();
            ILCE = dgvMusteriVeri.Rows[secilenalan].Cells[8].Value.ToString();
            ADRES = dgvMusteriVeri.Rows[secilenalan].Cells[9].Value.ToString();
            VERGIDAIRE = dgvMusteriVeri.Rows[secilenalan].Cells[10].Value.ToString();
            txtAd.Text = AD; txtSoyad.Text = SOYAD; txtTel1.Text = TELEFON; txtTel2.Text = TELEFON2; ; txtTC.Text = TC;
            txtMail.Text = MAIL; cbil.Text = IL; cbilce.Text = ILCE; txtAdres.Text = ADRES; txtVergiD.Text = VERGIDAIRE;
            txtTC.Enabled = false;
        }
    }
}
