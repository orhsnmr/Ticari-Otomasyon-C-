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
    public partial class AdminGiris : Form
    {
        public AdminGiris()
        {
            InitializeComponent();
        }
        MySQLConnection con = new MySQLConnection();
        MySqlCommand command;
        private void btnClose_Click_1(object sender, EventArgs e)
        {
            Application.Exit();
        }
        public void alert(string msg, FormAlert.enumType type)
        {
            FormAlert frm = new FormAlert();
            frm.showAlert(msg, type);
        }
        public string email,ad,soyad,adres,telefon;

        private void AdminGiris_Load(object sender, EventArgs e)
        {
            this.ActiveControl = txtKulad;
        }

        private void btnGiris_Click(object sender, EventArgs e)
        {
            con.bag.Open();
            command = new MySqlCommand("SELECT * FROM tbl_admin WHERE KULAD=@KULAD AND SIFRE =@SIFRE", con.bag);
            command.Parameters.AddWithValue("@KULAD", txtKulad.Text);
            command.Parameters.AddWithValue("@SIFRE", txtSifre.Text);
            MySqlDataReader dr = command.ExecuteReader();
            if (dr.Read())
            {
                AnaSayfa ana = new AnaSayfa();
                email = dr["EPOSTA"].ToString();
                ad = dr["AD"].ToString();
                soyad = dr["SOYAD"].ToString();
                adres = dr["ADRESS"].ToString();
                telefon = dr["TELEFON"].ToString();
                ana.kulad = txtKulad.Text;
                ana.ad = ad;
                ana.soyad = soyad;
                ana.eposta = email;
                ana.adres = adres;
                ana.telefon = telefon;
                ana.Show();
                this.alert("GİRİŞ BAŞARILI", FormAlert.enumType.success);
                this.Hide();
            }
            else
            {
                this.alert("HATALI KULLANICI ADI VEYA ŞİFRE", FormAlert.enumType.error);
            }
            con.bag.Close();
        }
    }
}
