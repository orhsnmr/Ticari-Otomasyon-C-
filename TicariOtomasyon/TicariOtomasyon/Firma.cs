using MySql.Data.MySqlClient;
using System;
using System.Data;
using System.Windows.Forms;

namespace TicariOtomasyon
{
    public partial class Firma : Form
    {
        public Firma()
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
            da = new MySqlDataAdapter("SELECT *FROM tbl_firma", con.bag);
            da.Fill(dt);
            dgvFirmaVeri.DataSource = dt;
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
        void kodCagir()
        {
            con.bag.Open();
            command = new MySqlCommand("SELECT FIRMAKOD FROM tbl_firmakod", con.bag);
            MySqlDataReader dr = command.ExecuteReader();
            while (dr.Read())
            {
                txtKod1.Text = dr[0].ToString();
            }
            con.bag.Close();
        }
        private void Firma_Load(object sender, EventArgs e)
        {
            listele();
            ilListele();
            kodCagir();
        }

        private void cbil_SelectedIndexChanged(object sender, EventArgs e)
        {
            con.bag.Open();
            command = new MySqlCommand("SELECT ILCEAD FROM tbl_ilceler WHERE ILID=@ILID", con.bag);
            command.Parameters.AddWithValue("@ILID", cbil.SelectedIndex + 1);
            MySqlDataReader dr = command.ExecuteReader();
            while (dr.Read())
            {
                cbilce.Items.Add(dr[0]);
            }
            con.bag.Close();
        }
     
        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        void Temizle()
        {
            txtAd.Text = ""; txtSoyad.Text = ""; txtSektor.Text = ""; txtGorev.Text = ""; txtTC.Text = ""; txtTel1.Text = ""; txtTel2.Text = ""; txtTel3.Text = "";
            txtMail.Text = ""; cbil.Text = ""; cbilce.Text = ""; txtFax.Text = ""; txtVergiD.Text = ""; txtAdres.Text = ""; txtOK1.Text = ""; txtOK2.Text = "";
            txtOK3.Text = "";
        }
        private void dgvFirmaVeri_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            string ID,AD, YETKILISTATU, YETKILIADSOYAD, YETKILITC, SEKTOR, TELEFON, TELEFON2, TELEFON3, MAIL, FAX, IL, ILCE, VERGIDAIRE, ADRES, OZELKOD1, OZELKOD2, OZELKOD3;
            int secilenalan = dgvFirmaVeri.SelectedCells[0].RowIndex;
            ID = dgvFirmaVeri.Rows[secilenalan].Cells[0].Value.ToString();
            AD = dgvFirmaVeri.Rows[secilenalan].Cells[1].Value.ToString();
            YETKILISTATU = dgvFirmaVeri.Rows[secilenalan].Cells[2].Value.ToString();
            YETKILIADSOYAD = dgvFirmaVeri.Rows[secilenalan].Cells[3].Value.ToString();
            YETKILITC = dgvFirmaVeri.Rows[secilenalan].Cells[4].Value.ToString();
            SEKTOR = dgvFirmaVeri.Rows[secilenalan].Cells[5].Value.ToString();
            TELEFON = dgvFirmaVeri.Rows[secilenalan].Cells[6].Value.ToString();
            TELEFON2 = dgvFirmaVeri.Rows[secilenalan].Cells[7].Value.ToString();
            TELEFON3 = dgvFirmaVeri.Rows[secilenalan].Cells[8].Value.ToString();
            MAIL = dgvFirmaVeri.Rows[secilenalan].Cells[9].Value.ToString();
            FAX = dgvFirmaVeri.Rows[secilenalan].Cells[10].Value.ToString();
            IL = dgvFirmaVeri.Rows[secilenalan].Cells[11].Value.ToString();
            ILCE = dgvFirmaVeri.Rows[secilenalan].Cells[12].Value.ToString();
            VERGIDAIRE = dgvFirmaVeri.Rows[secilenalan].Cells[13].Value.ToString();
            ADRES = dgvFirmaVeri.Rows[secilenalan].Cells[14].Value.ToString();
            OZELKOD1 = dgvFirmaVeri.Rows[secilenalan].Cells[15].Value.ToString();
            OZELKOD2 = dgvFirmaVeri.Rows[secilenalan].Cells[16].Value.ToString();
            OZELKOD3 = dgvFirmaVeri.Rows[secilenalan].Cells[17].Value.ToString();
            txtAd.Text = AD; txtSektor.Text = SEKTOR; txtGorev.Text = YETKILISTATU; txtSoyad.Text = YETKILIADSOYAD; txtTC.Text = YETKILITC;
            txtTel1.Text = TELEFON; txtTel2.Text = TELEFON2; txtTel3.Text = TELEFON3; txtMail.Text = MAIL; txtFax.Text = FAX; cbil.Text = IL;
            cbilce.Text = ILCE; txtVergiD.Text = VERGIDAIRE; txtAdres.Text = ADRES; txtOK1.Text = OZELKOD1; txtOK2.Text = OZELKOD2; txtOK3.Text = OZELKOD3;
            txtID.Text = ID; txtTC.Enabled = false;
        }
        public void alert(string msg, FormAlert.enumType type)
        {
            FormAlert frm = new FormAlert();
            frm.showAlert(msg, type);
        }
        private void btnSave_Click(object sender, EventArgs e)
        {
            if (txtAd.Text == "" || txtSoyad.Text == "" || txtSektor.Text == "" || txtGorev.Text == "" || txtTC.Text == "" || txtTel1.Text == "" || txtMail.Text == "" || cbil.Text == "" || cbilce.Text == "" || txtFax.Text == "" || txtVergiD.Text == "" || txtAdres.Text == "")
            {
                this.alert("Boş Alan Bırakmayınız", FormAlert.enumType.warning);
            }
            else
            {
                con.bag.Open();
                command = new MySqlCommand("INSERT INTO tbl_firma(AD, YETKILISTATU, YETKILIADSOYAD, YETKILITC, SEKTOR, TELEFON, TELEFON2, TELEFON3, MAIL, FAX, IL, ILCE, VERGIDAIRE, ADRES, OZELKOD1, OZELKOD2, OZELKOD3) VALUES" +
                    "(@AD, @YETKILISTATU, @YETKILIADSOYAD, @YETKILITC, @SEKTOR, @TELEFON, @TELEFON2, @TELEFON3,@MAIL, @FAX, @IL, @ILCE, @VERGIDAIRE, @ADRES, @OZELKOD1, @OZELKOD2, @OZELKOD3)", con.bag);
                command.Parameters.AddWithValue("@AD", txtAd.Text);
                command.Parameters.AddWithValue("@YETKILISTATU", txtGorev.Text);
                command.Parameters.AddWithValue("@SEKTOR", txtSektor.Text);
                command.Parameters.AddWithValue("@YETKILIADSOYAD", txtSoyad.Text);
                command.Parameters.AddWithValue("@YETKILITC", txtTC.Text);
                command.Parameters.AddWithValue("@TELEFON", txtTel1.Text);
                command.Parameters.AddWithValue("@TELEFON2", txtTel2.Text);
                command.Parameters.AddWithValue("@TELEFON3", txtTel3.Text);
                command.Parameters.AddWithValue("@FAX", txtFax.Text);
                command.Parameters.AddWithValue("@MAIL", txtMail.Text);
                command.Parameters.AddWithValue("@IL", cbil.Text);
                command.Parameters.AddWithValue("@ILCE", cbilce.Text);
                command.Parameters.AddWithValue("@ADRES", txtAdres.Text);
                command.Parameters.AddWithValue("@VERGIDAIRE", txtVergiD.Text);
                command.Parameters.AddWithValue("@OZELKOD1", txtOK1.Text);
                command.Parameters.AddWithValue("@OZELKOD2", txtOK2.Text);
                command.Parameters.AddWithValue("@OZELKOD3", txtOK3.Text);
                command.ExecuteNonQuery();
                con.bag.Close();
                this.alert("Kayıt Başarılı", FormAlert.enumType.success);
                listele();
                Temizle();
            }
        }

        void KayıtSil(int numara)
        {
            string sql = "DELETE FROM tbl_firma where ID=@ID";
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
                foreach (DataGridViewRow drow in dgvFirmaVeri.SelectedRows)
                {
                    int numara = Convert.ToInt32(drow.Cells[0].Value);
                    KayıtSil(numara);
                }
                this.alert("Silme Başarılı", FormAlert.enumType.success);
                listele();
                Temizle();
            }

        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            if (txtAd.Text == "" || txtSoyad.Text == "" || txtSektor.Text == "" || txtGorev.Text == "" || txtTC.Text == "" || txtTel1.Text == "" || txtMail.Text == "" || cbil.Text == "" || cbilce.Text == "" || txtFax.Text == "" || txtVergiD.Text == "" || txtAdres.Text == "")
            {
                this.alert("Boş Alan Bırakmayınız", FormAlert.enumType.warning);
            }
            else
            {
                con.bag.Open();
                command = new MySqlCommand("UPDATE tbl_firma SET AD =@AD, YETKILISTATU =@YETKILISTATU, YETKILIADSOYAD =@YETKILIADSOYAD, SEKTOR=@SEKTOR, TELEFON=@TELEFON, TELEFON2 =@TELEFON2, TELEFON3=@TELEFON3, MAIL =@MAIL, FAX = @FAX, IL=@IL, ILCE=@ILCE, VERGIDAIRE=@VERGIDAIRE, ADRES=@ADRES, OZELKOD1 =@OZELKOD1, OZELKOD2=@OZELKOD2, OZELKOD3=@OZELKOD3 WHERE  YETKILITC=@YETKILITC", con.bag);
                command.Parameters.AddWithValue("@AD", txtAd.Text);
                command.Parameters.AddWithValue("@YETKILISTATU", txtGorev.Text);
                command.Parameters.AddWithValue("@SEKTOR", txtSektor.Text);
                command.Parameters.AddWithValue("@YETKILIADSOYAD", txtSoyad.Text);
                command.Parameters.AddWithValue("@YETKILITC", txtTC.Text);
                command.Parameters.AddWithValue("@TELEFON", txtTel1.Text);
                command.Parameters.AddWithValue("@TELEFON2", txtTel2.Text);
                command.Parameters.AddWithValue("@TELEFON3", txtTel3.Text);
                command.Parameters.AddWithValue("@FAX", txtFax.Text);
                command.Parameters.AddWithValue("@MAIL", txtMail.Text);
                command.Parameters.AddWithValue("@IL", cbil.Text);
                command.Parameters.AddWithValue("@ILCE", cbilce.Text);
                command.Parameters.AddWithValue("@ADRES", txtAdres.Text);
                command.Parameters.AddWithValue("@VERGIDAIRE", txtVergiD.Text);
                command.Parameters.AddWithValue("@OZELKOD1", txtOK1.Text);
                command.Parameters.AddWithValue("@OZELKOD2", txtOK2.Text);
                command.Parameters.AddWithValue("@OZELKOD3", txtOK3.Text);
                command.ExecuteNonQuery();
                con.bag.Close();
                this.alert("Kayıt Güncelleme Başarılı", FormAlert.enumType.success);
                listele();
                Temizle();
                txtTC.Enabled = true;
            }
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
                command = new MySqlCommand("SELECT *FROM tbl_firma where AD LIKE @AD OR YETKILITC LIKE @TC OR OZELKOD1 LIKE @OZELKOD1 OR OZELKOD2 LIKE @OZELKOD1 OR OZELKOD1 LIKE @OZELKOD1  ", con.bag);
                command.Parameters.AddWithValue("@AD", txtAd.Text);
                command.Parameters.AddWithValue("@TC", txtTC.Text);
                command.Parameters.AddWithValue("@OZELKOD1", txtOK1.Text);
                command.Parameters.AddWithValue("@OZELKOD2", txtOK2.Text);
                command.Parameters.AddWithValue("@OZELKOD3", txtOK3.Text);
                da = new MySqlDataAdapter(command);
                DataSet ds = new DataSet();
                da.Fill(ds);
                dgvFirmaVeri.DataSource = ds.Tables[0];
                con.bag.Close();
            }
        }
    }
}
