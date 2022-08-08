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
    public partial class FaturaUrunDuzenleme : Form
    {
        public FaturaUrunDuzenleme()
        {
            InitializeComponent();
        }
        MySqlDataAdapter da = new MySqlDataAdapter();
        DataTable dt = new DataTable();
        MySQLConnection con = new MySQLConnection();
        MySqlCommand command;
        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        public string urunID,urunAdi,Miktar,Fiyat,Tutar;

        private void btnDelete_Click(object sender, EventArgs e)
        {
            con.bag.Open();
            command = new MySqlCommand("DELETE FROM tbl_faturadetay WHERE FATURAURUNID =@ID",con.bag);
            command.Parameters.AddWithValue("@ID", txtürünID.Text);
            command.ExecuteNonQuery();
            con.bag.Close();
            this.alert("Kayıt Silme Başarılı", FormAlert.enumType.success);
         
        }

        public void alert(string msg, FormAlert.enumType type)
        {
            FormAlert frm = new FormAlert();
            frm.showAlert(msg, type);
        }
        
        private void btnUpdate_Click(object sender, EventArgs e)
        {
            con.bag.Open();
            command = new MySqlCommand("UPDATE tbl_faturadetay SET URUNAD=@URUNAD,MIKTAR=@MIKTAR,FIYAT=@FIYAT,TUTAR=@TUTAR WHERE FATURAURUNID = @FATURAID", con.bag);
            command.Parameters.AddWithValue("@URUNAD", txtUrunAdi.Text);
            command.Parameters.AddWithValue("@MIKTAR", txtMiktar.Text);
            command.Parameters.AddWithValue("@FIYAT", txtFiyat.Text);
            command.Parameters.AddWithValue("@TUTAR", txtTutar.Text);
            command.Parameters.AddWithValue("@FATURAID", txtürünID.Text);
            command.ExecuteNonQuery();
            con.bag.Close();
            this.alert("Kayıt Güncelleme Başarılı", FormAlert.enumType.success);          
        }

        private void FaturaUrunDuzenleme_Load(object sender, EventArgs e)
        {
            txtürünID.Enabled = false;
            txtürünID.Text = urunID;
            txtUrunAdi.Text = urunAdi;
            txtMiktar.Text = Miktar;
            txtFiyat.Text = Fiyat;
            txtTutar.Text = Tutar;
        }
    }
}
