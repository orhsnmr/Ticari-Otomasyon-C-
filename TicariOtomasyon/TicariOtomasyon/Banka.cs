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
    public partial class Banka : Form
    {
        public Banka()
        {
            InitializeComponent();
        }
        MySqlDataAdapter da = new MySqlDataAdapter();
        DataTable dt = new DataTable();
        MySQLConnection con = new MySQLConnection();
        MySqlCommand command;


        public void alert(string msg, FormAlert.enumType type)
        {
            FormAlert frm = new FormAlert();
            frm.showAlert(msg, type);
        }
        void listele()
        {
            con.bag.Open();
            dt.Clear();
            da = new MySqlDataAdapter("SELECT  a.ID ,a.BANKAADI ,a.IL ,a.ILCE ,a.SUBE " +
                ",a.IBAN ,a.HESAPNO ,a.YETKILI ,a.TELEFON ,a.TARIH ,a.HESAPTURU ,b.AD FROM tbl_bankalar a " +
                "JOIN tbl_firma b on a.FIRMAID =b.ID", con.bag);
            da.Fill(dt);
            dgvBankaVeri.DataSource = dt;
            con.bag.Close();
        }
        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void FirmaListele()
        {
            DataTable dt1 = new DataTable();
            con.bag.Open();
            dt1.Clear();
            da = new MySqlDataAdapter("SELECT ID,AD FROM tbl_firma", con.bag);
            da.Fill(dt1);
            cbFirma.ValueMember = "ID";
            cbFirma.DisplayMember = "AD";
            cbFirma.DataSource = dt1;
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
        private void Banka_Load(object sender, EventArgs e)
        {
            listele();
            ilListele();
            FirmaListele();
        }
        void txtTemizle()
        {
            txtBankaAd.Text = ""; cbil.Text = ""; cbilce.Text = ""; txtSube.Text = ""; txtIBAN.Text = "";
            txtHesapNo.Text = ""; txtYetkili.Text = ""; txtTel.Text = ""; dtpTarih.Text = ""; txtHesapTur.Text = "";
            cbFirma.Text = "";
        }
        private void btnSave_Click(object sender, EventArgs e)
        {

            if (txtBankaAd.Text == "" || cbil.Text == "" || cbil.Text == "" || txtSube.Text == "" || txtIBAN.Text == "" || txtHesapNo.Text == "" || txtYetkili.Text == "" || txtTel.Text == "" || dtpTarih.Text == "" || txtHesapTur.Text == "")
            {
                this.alert("Boş Alan Bırakmayınız", FormAlert.enumType.warning);
            }
            else
            {
                con.bag.Open();
                command = new MySqlCommand("INSERT INTO  tbl_bankalar (BANKAADI,IL,ILCE,SUBE,IBAN ,HESAPNO,YETKILI ,TELEFON ,TARIH ,HESAPTURU ,FIRMAID) VALUES(@BANKAADI,@IL,@ILCE,@SUBE,@IBAN ,@HESAPNO,@YETKILI ,@TELEFON ,@TARIH ,@HESAPTURU ,@FIRMAID)", con.bag);
                command.Parameters.AddWithValue("@BANKAADI", txtBankaAd.Text);
                command.Parameters.AddWithValue("@IL", cbil.Text);
                command.Parameters.AddWithValue("@ILCE", cbilce.Text);
                command.Parameters.AddWithValue("@SUBE", txtSube.Text);
                command.Parameters.AddWithValue("@IBAN", txtIBAN.Text);
                command.Parameters.AddWithValue("@HESAPNO", txtHesapNo.Text);
                command.Parameters.AddWithValue("@YETKILI", txtYetkili.Text);
                command.Parameters.AddWithValue("@TELEFON", txtTel.Text);
                command.Parameters.AddWithValue("@TARIH", dtpTarih.Value);
                command.Parameters.AddWithValue("@HESAPTURU", txtHesapTur.Text);
                command.Parameters.AddWithValue("@FIRMAID", cbFirma.SelectedValue);
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
                cbilce.Items.Add(dr["ILCEAD"]);
            }
            con.bag.Close();
        }

        private void dgvBankaVeri_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            string ID,BANKAADI, IL, ILCE, SUBE, IBAN, HESAPNO, YETKILI, TELEFON, TARIH, HESAPTURU, FIRMAID;
            int secilenalan = dgvBankaVeri.SelectedCells[0].RowIndex;
            ID = dgvBankaVeri.Rows[secilenalan].Cells[0].Value.ToString();
            BANKAADI = dgvBankaVeri.Rows[secilenalan].Cells[1].Value.ToString();
            IL = dgvBankaVeri.Rows[secilenalan].Cells[2].Value.ToString();
            ILCE = dgvBankaVeri.Rows[secilenalan].Cells[3].Value.ToString();
            SUBE = dgvBankaVeri.Rows[secilenalan].Cells[4].Value.ToString();
            IBAN = dgvBankaVeri.Rows[secilenalan].Cells[5].Value.ToString();
            HESAPNO = dgvBankaVeri.Rows[secilenalan].Cells[6].Value.ToString();
            YETKILI = dgvBankaVeri.Rows[secilenalan].Cells[7].Value.ToString();
            TELEFON = dgvBankaVeri.Rows[secilenalan].Cells[8].Value.ToString();
            TARIH = dgvBankaVeri.Rows[secilenalan].Cells[9].Value.ToString();
            HESAPTURU = dgvBankaVeri.Rows[secilenalan].Cells[10].Value.ToString();
            FIRMAID = dgvBankaVeri.Rows[secilenalan].Cells[11].Value.ToString();
            txtBankaAd.Text = BANKAADI; txtSube.Text = SUBE; txtTel.Text = TELEFON; txtIBAN.Text = IBAN; ; txtHesapNo.Text = HESAPNO;
            dtpTarih.Text = TARIH; cbil.Text = IL; cbilce.Text = ILCE; txtYetkili.Text = YETKILI; txtHesapTur.Text = HESAPTURU;
            cbFirma.Text = FIRMAID; txtID.Text = ID;
        }

        void KayıtSil(int numara)
        {
            string sql = "DELETE FROM tbl_bankalar where ID=@ID";
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
                foreach (DataGridViewRow drow in dgvBankaVeri.SelectedRows)
                {
                    int numara = Convert.ToInt32(drow.Cells[0].Value);
                    KayıtSil(numara);
                }
                this.alert("Silme Başarılı", FormAlert.enumType.success);
                listele();
                txtTemizle();
            };
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            if (txtBankaAd.Text == "" || cbil.Text == "" || cbil.Text == "" || txtSube.Text == "" || txtIBAN.Text == "" || txtHesapNo.Text == "" || txtYetkili.Text == "" || txtTel.Text == "" || dtpTarih.Text == "" || txtHesapTur.Text == "")
            {
                this.alert("Boş Alan Bırakmayınız", FormAlert.enumType.warning);
            }
            else
            {
                con.bag.Open();
                command = new MySqlCommand("UPDATE tbl_bankalar SET  BANKAADI=@BANKAADI, IL =@IL,ILCE =@ILCE,SUBE=@SUBE,IBAN=@IBAN,HESAPNO=@HESAPNO,YETKILI=@YETKILI,TELEFON=@TELEFON ,TARIH=@TARIH ,HESAPTURU=@HESAPTURU ,FIRMAID =@FIRMAID WHERE ID=@ID", con.bag);
                command.Parameters.AddWithValue("@ID", txtID.Text);
                command.Parameters.AddWithValue("@BANKAADI", txtBankaAd.Text);
                command.Parameters.AddWithValue("@IL", cbil.Text);
                command.Parameters.AddWithValue("@ILCE", cbilce.Text);
                command.Parameters.AddWithValue("@SUBE", txtSube.Text);
                command.Parameters.AddWithValue("@IBAN", txtIBAN.Text);
                command.Parameters.AddWithValue("@HESAPNO", txtHesapNo.Text);
                command.Parameters.AddWithValue("@YETKILI", txtYetkili.Text);
                command.Parameters.AddWithValue("@TELEFON", txtTel.Text);
                command.Parameters.AddWithValue("@TARIH", dtpTarih.Value);
                command.Parameters.AddWithValue("@HESAPTURU", txtHesapTur.Text);
                command.Parameters.AddWithValue("@FIRMAID", cbFirma.SelectedValue);
                command.ExecuteNonQuery();
                con.bag.Close();
                this.alert("Kayıt Güncelleme Başarılı", FormAlert.enumType.success);
                listele();
                txtTemizle();
            }
        }

        private void btnInfo_Click(object sender, EventArgs e)
        {

            if (txtBankaAd.Text == "" && txtIBAN.Text == "")
            {
                this.alert("BANKA ADI VEYA IBAN'A ARATIN", FormAlert.enumType.info);
            }
            else
            {
                con.bag.Open();
                command = new MySqlCommand("SELECT *FROM tbl_bankalar where BANKAADI LIKE @BANKAADI OR IBAN LIKE @IBAN", con.bag);
                command.Parameters.AddWithValue("@BANKAADI", txtBankaAd.Text);
                command.Parameters.AddWithValue("@IBAN", txtIBAN.Text);
                da = new MySqlDataAdapter(command);
                DataSet ds = new DataSet();
                da.Fill(ds);
                dgvBankaVeri.DataSource = ds.Tables[0];
                con.bag.Close();
            }
        }
    }
}
