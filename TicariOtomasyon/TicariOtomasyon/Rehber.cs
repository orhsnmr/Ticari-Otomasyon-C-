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
    public partial class Rehber : Form
    {
        public Rehber()
        {
            InitializeComponent();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        MySqlDataAdapter da, da1;
        DataTable dt = new DataTable();
        DataTable dt1 = new DataTable();
        MySQLConnection con = new MySQLConnection();
        void listele()
        {
            con.bag.Open();
            dt.Clear();
            da = new MySqlDataAdapter("SELECT AD,SOYAD, TELEFON,TELEFON2, MAIL FROM tbl_musteriler", con.bag);
            da.Fill(dt);
            dgvMusteriveri.DataSource = dt;
            con.bag.Close();
        }
        void firmalistele()
        {
            con.bag.Open();
            dt1.Clear();
            da1 = new MySqlDataAdapter("SELECT AD,YETKILIADSOYAD, TELEFON,TELEFON2,TELEFON3, MAIL,FAX FROM tbl_firma", con.bag);
            da1.Fill(dt1);
            dgvFirmaVeri.DataSource = dt1;
            con.bag.Close();
        }
        private void Rehber_Load(object sender, EventArgs e)
        {
            this.alert("Mail göndermek için verilere çift tıklayın.", FormAlert.enumType.info);           
            listele();
            firmalistele();

        }

        Mail frmmail;
        public void alert(string msg, FormAlert.enumType type)
        {
            FormAlert frm = new FormAlert();
            frm.showAlert(msg, type);
        }
        private void dgvFirmaVeri_DoubleClick(object sender, EventArgs e)
        {
            frmmail = new Mail();
            frmmail.Show();
            frmmail.txtKime.Text = dgvFirmaVeri.CurrentRow.Cells[5].Value.ToString();
            
        }

        private void dgvMusteriveri_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void dgvMusteriveri_DoubleClick(object sender, EventArgs e)
        {
            frmmail = new Mail();
            frmmail.Show();
            frmmail.txtKime.Text = dgvMusteriveri.CurrentRow.Cells[4].Value.ToString();
        }

    }
}
