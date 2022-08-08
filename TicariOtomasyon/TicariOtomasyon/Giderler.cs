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
    public partial class Giderler : Form
    {
        public Giderler()
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
            da = new MySqlDataAdapter("SELECT *FROM tbl_giderler", con.bag);
            da.Fill(dt);
            dgvGiderVeri.DataSource = dt;
            con.bag.Close();
        }
        private void Giderler_Load(object sender, EventArgs e)
        {
            listele();
        }

        private void dgvGiderVeri_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            string AY, YIL, ELEKTRIK, SU, DOGALGAZ, INTERNET, MAASLAR, EKSTRA, NOTLAR,ID;
            int secilenalan = dgvGiderVeri.SelectedCells[0].RowIndex;
            ID= dgvGiderVeri.Rows[secilenalan].Cells[0].Value.ToString();
            AY = dgvGiderVeri.Rows[secilenalan].Cells[1].Value.ToString();
            YIL = dgvGiderVeri.Rows[secilenalan].Cells[2].Value.ToString();
            ELEKTRIK = dgvGiderVeri.Rows[secilenalan].Cells[3].Value.ToString();
            SU = dgvGiderVeri.Rows[secilenalan].Cells[4].Value.ToString();
            DOGALGAZ = dgvGiderVeri.Rows[secilenalan].Cells[5].Value.ToString();
            INTERNET = dgvGiderVeri.Rows[secilenalan].Cells[6].Value.ToString();
            MAASLAR = dgvGiderVeri.Rows[secilenalan].Cells[7].Value.ToString();
            EKSTRA = dgvGiderVeri.Rows[secilenalan].Cells[8].Value.ToString();
            NOTLAR = dgvGiderVeri.Rows[secilenalan].Cells[9].Value.ToString();            
            cbAy.Text = AY; cbyil.Text = YIL; txtElektrik.Text = ELEKTRIK; txtSu.Text = SU; ; txtDogalGaz.Text = DOGALGAZ;
            txtnet.Text = INTERNET; txtMaas.Text = MAASLAR; txtExtra.Text = EKSTRA; txtNot.Text = NOTLAR; txtID.Text = ID;
            txtID.Enabled = false;
        }
        public void alert(string msg, FormAlert.enumType type)
        {
            FormAlert frm = new FormAlert();
            frm.showAlert(msg, type);
        }
        void txtTemizle()
        {
            cbAy.Text = ""; cbyil.Text = ""; txtElektrik.Text = ""; txtSu.Text = "";
            txtDogalGaz.Text = ""; txtnet.Text = ""; txtMaas.Text = ""; txtExtra.Text = ""; txtNot.Text = "";
        }
        private void btnSave_Click(object sender, EventArgs e)
        {

            if (cbAy.Text == "" || cbyil.Text == "" || txtElektrik.Text == "" || txtSu.Text == "" || txtDogalGaz.Text == "" || txtnet.Text == "" || txtMaas.Text == "" || txtExtra.Text == "" || txtNot.Text == "")
            {
                this.alert("Boş Alan Bırakmayınız", FormAlert.enumType.warning);
            }
            else
            {
                con.bag.Open();
                command = new MySqlCommand("INSERT INTO tbl_giderler(AY, YIL, ELEKTRIK, SU, DOGALGAZ, INTERNET, MAASLAR, EKSTRA, NOTLAR) VALUES(@AY, @YIL, @ELEKTRIK, @SU, @DOGALGAZ, @INTERNET, @MAASLAR, @EKSTRA, @NOTLAR)", con.bag);
                command.Parameters.AddWithValue("@AY", cbAy.Text);
                command.Parameters.AddWithValue("@YIL", cbyil.Text);
                command.Parameters.AddWithValue("@ELEKTRIK", txtElektrik.Text);
                command.Parameters.AddWithValue("@SU", txtSu.Text);
                command.Parameters.AddWithValue("@DOGALGAZ", txtDogalGaz.Text);
                command.Parameters.AddWithValue("@INTERNET", txtnet.Text);
                command.Parameters.AddWithValue("@MAASLAR", txtMaas.Text);
                command.Parameters.AddWithValue("@EKSTRA", txtExtra.Text);
                command.Parameters.AddWithValue("@NOTLAR", txtNot.Text);
                command.ExecuteNonQuery();
                con.bag.Close();
                this.alert("Kayıt Başarılı", FormAlert.enumType.success);
                listele();
                txtTemizle();
            }
        }
        void KayıtSil(int numara)
        {
            string sql = "DELETE FROM tbl_giderler where ID=@ID";
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
                foreach (DataGridViewRow drow in dgvGiderVeri.SelectedRows)
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
            if (cbAy.Text == "" && cbyil.Text == "")
            {
                this.alert("Ay'a veya Yıl'a göre ARATIN", FormAlert.enumType.info);
            }
            else
            {
                con.bag.Open();
                command = new MySqlCommand("SELECT *FROM tbl_giderler where AY LIKE @AY OR YIL LIKE @YIL", con.bag);
                command.Parameters.AddWithValue("@AY", cbAy.Text);
                command.Parameters.AddWithValue("@YIL", cbyil.Text);
                da = new MySqlDataAdapter(command);
                DataSet ds = new DataSet();
                da.Fill(ds);
                dgvGiderVeri.DataSource = ds.Tables[0];
                con.bag.Close();
            }
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            if (cbAy.Text == "" || cbyil.Text == "" || txtElektrik.Text == "" || txtSu.Text == "" || txtDogalGaz.Text == "" || txtnet.Text == "" || txtMaas.Text == "" || txtExtra.Text == "" || txtNot.Text == "")
            {
                this.alert("Boş Alan Bırakmayınız", FormAlert.enumType.warning);
            }
            else
            {
                con.bag.Open();
                command = new MySqlCommand("UPDATE tbl_giderler SET AY=@AY, YIL=@YIL, ELEKTRIK=@ELEKTRIK, SU=@SU, DOGALGAZ=@DOGALGAZ, INTERNET=@INTERNET, MAASLAR=@MAASLAR, EKSTRA=@EKSTRA, NOTLAR=@NOTLAR WHERE ID=@ID", con.bag);
               
                command.Parameters.AddWithValue("@AY", cbAy.Text);
                command.Parameters.AddWithValue("@YIL", cbyil.Text);
                command.Parameters.AddWithValue("@ELEKTRIK", txtElektrik.Text);
                command.Parameters.AddWithValue("@SU", txtSu.Text);
                command.Parameters.AddWithValue("@DOGALGAZ", txtDogalGaz.Text);
                command.Parameters.AddWithValue("@INTERNET", txtnet.Text);
                command.Parameters.AddWithValue("@MAASLAR", txtMaas.Text);
                command.Parameters.AddWithValue("@EKSTRA", txtExtra.Text);
                command.Parameters.AddWithValue("@NOTLAR", txtNot.Text);
                command.Parameters.AddWithValue("@ID", txtID.Text);
                command.ExecuteNonQuery();
                con.bag.Close();
                this.alert("Kayıt Günceleme Başarılı", FormAlert.enumType.success);
                listele();
                txtTemizle();
            }
        }
    }
}
