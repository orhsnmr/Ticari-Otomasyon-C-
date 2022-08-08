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
    public partial class Ayarlar : Form
    {
        public Ayarlar()
        {
            InitializeComponent();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        MySqlDataAdapter da = new MySqlDataAdapter();
        MySQLConnection con = new MySQLConnection();
        DataTable dt = new DataTable();
        MySqlCommand command;
        private void Ayarlar_Load(object sender, EventArgs e)
        {
            con.bag.Open();
            dt.Clear();
            da = new MySqlDataAdapter("SELECT AD,SOYAD,KULAD,YETKI FROM tbl_admin", con.bag);
            da.Fill(dt);
            dgvAdminveri.DataSource = dt;
            con.bag.Close();
        }


        public void alert(string msg, FormAlert.enumType type)
        {
            FormAlert frm = new FormAlert();
            frm.showAlert(msg, type);
        }
        int h = 547;
        int h1 = 279;
        AnaSayfa ana = new AnaSayfa();
        private void btnHesapDuzenle_Click(object sender, EventArgs e)
        {
            ana = new AnaSayfa();
            panleHesap.Height = h;
            txtEposta.Text = lblEposta.Text;
            txtKulad.Text = lblKulad.Text;
            panelYetki.Hide();
            btnAdresDuzenle.Enabled = false;
        }

        private void label10_Click(object sender, EventArgs e)
        {
            panleHesap.Height = h1;
            panelYetki.Show();
            btnAdresDuzenle.Enabled = true;

        }

        void adreslistele()
        {
            if (con.bag.State == ConnectionState.Open)
            {
                con.bag.Close();
                con.bag.Open();
                command = new MySqlCommand("SELECT * FROM tbl_admin", con.bag);
                MySqlDataReader dr = command.ExecuteReader();
                if (dr.Read())
                {
                    lblAdres.Text = dr["ADRESS"].ToString();
                    lblTel.Text = dr["TELEFON"].ToString();
                }
                con.bag.Close();
            }
        }
        string ad, soyad;
        void hesaplistele()
        {
            if (con.bag.State == ConnectionState.Open)
            {
                con.bag.Close();
                con.bag.Open();
                command = new MySqlCommand("SELECT * FROM tbl_admin", con.bag);
                MySqlDataReader dr = command.ExecuteReader();
                if (dr.Read())
                {
                    ad = dr["AD"].ToString();
                    soyad = dr["SOYAD"].ToString();
                    lblKulad.Text = dr["KULAD"].ToString();
                    lblEposta.Text = dr["EPOSTA"].ToString();
                    lblAd.Text = ad + " " + soyad;
                }
                con.bag.Close();
            }
        }
        private void btnHesapGuncelle_Click(object sender, EventArgs e)
        {
            if (con.bag.State == ConnectionState.Closed)
            {
                con.bag.Open();
                command = new MySqlCommand("UPDATE tbl_admin SET AD=@AD,SOYAD=@SOYAD,EPOSTA=@EPOSTA WHERE KULAD=@KULAD", con.bag);
                command.Parameters.AddWithValue("@AD", txtAd.Text);
                command.Parameters.AddWithValue("@SOYAD", txtSoyad.Text);
                command.Parameters.AddWithValue("@EPOSTA", txtEposta.Text);
                command.Parameters.AddWithValue("@KULAD", txtKulad.Text);
                command.ExecuteNonQuery();
                con.bag.Close();
                this.alert("Kayıt Güncelleme Başarılı", FormAlert.enumType.success);
                hesaplistele();
            }
        }

        private void btnAdresDuzenle_Click(object sender, EventArgs e)
        {
            panelAdres.Height = h;
            txtAdres.Text = lblAdres.Text;
            txtTel.Text = lblTel.Text;
            panelYetki.Hide();
            btnHesapDuzenle.Enabled = false;

        }

        private void label11_Click(object sender, EventArgs e)
        {
            panelAdres.Height = h1;
            panelYetki.Show();
            btnHesapDuzenle.Enabled = true;
        }

        private void dgvMusteriveri_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            string ad, soyad, kulad, yetki;
            int secilenalan = dgvAdminveri.SelectedCells[0].RowIndex;
            ad = dgvAdminveri.Rows[secilenalan].Cells[0].Value.ToString();
            soyad = dgvAdminveri.Rows[secilenalan].Cells[1].Value.ToString();
            kulad = dgvAdminveri.Rows[secilenalan].Cells[2].Value.ToString();
            yetki = dgvAdminveri.Rows[secilenalan].Cells[3].Value.ToString();
            adlbl.Text = ad; soyadlbl.Text = soyad; lblKullanici.Text = kulad;
            cbYetki.Text = yetki;
        }

        private void btnAdresGuncelle_Click(object sender, EventArgs e)
        {
            if (con.bag.State == ConnectionState.Closed)
            {
                con.bag.Open();
                command = new MySqlCommand("UPDATE tbl_admin SET ADRESS=@ADRES,TELEFON=@TELEFON WHERE KULAD=@KULAD", con.bag);
                command.Parameters.AddWithValue("@ADRES", txtAdres.Text);
                command.Parameters.AddWithValue("@TELEFON", txtTel.Text);
                command.Parameters.AddWithValue("@KULAD", lblKulad.Text);
                command.ExecuteNonQuery();
                this.alert("Kayıt Güncelleme Başarılı", FormAlert.enumType.success);
                adreslistele();
                con.bag.Close();
            }
        }
        //void adminGuncelle()
        //{
        //    if (con.bag.State == ConnectionState.Open)
        //    {
        //        con.bag.Close();
        //        con.bag.Open();
        //        command = new MySqlCommand("UPDATE tbl_admin SET AD=@AD,SOYAD=@SOYAD,SIFRE=@SIFRE,EPOSTA=@EPOSTA,YETKI=@YETKI WHERE KULAD=@KULAD", con.bag);
        //        command.Parameters.AddWithValue("@AD", txtad.Text);
        //        command.Parameters.AddWithValue("@SOYAD", txtSOYAD.Text);
        //        command.Parameters.AddWithValue("@SIFRE", txtSifre.Text);
        //        command.Parameters.AddWithValue("@EPOSTA", txtEposta.Text);
        //        command.Parameters.AddWithValue("@YETKI", txtYetki.Text);
        //        command.Parameters.AddWithValue("@KULAD", txtKulad.Text);
        //        command.ExecuteNonQuery();
        //        con.bag.Close();
        //        listele();
        //        this.alert("Kayıt Güncelleme Başarılı", FormAlert.enumType.success);
        //    }

        //}
        //void kullanici()
        //{
        //    if (con.bag.State == ConnectionState.Open)
        //    {
        //        con.bag.Close();
        //        con.bag.Open();
        //        command = new MySqlCommand("UPDATE tbl_admin SET AD=@AD,SOYAD=@SOYAD,SIFRE=@SIFRE,EPOSTA=@EPOSTA WHERE KULAD=@KULAD", con.bag);
        //        command.Parameters.AddWithValue("@AD", txtad.Text);
        //        command.Parameters.AddWithValue("@SOYAD", txtSOYAD.Text);
        //        command.Parameters.AddWithValue("@SIFRE", txtSifre.Text);
        //        command.Parameters.AddWithValue("@EPOSTA", txtEposta.Text);
        //        command.Parameters.AddWithValue("@KULAD", txtKulad.Text);
        //        command.ExecuteNonQuery();
        //        con.bag.Close();
        //        listele();
        //        this.alert("Kayıt Güncelleme Başarılı", FormAlert.enumType.success);
        //    }
        //}


        ////void Ekle()
        ////{
        ////    if (con.bag.State == ConnectionState.Open)
        ////    {
        ////        con.bag.Close();
        ////        con.bag.Open();
        ////        command = new MySqlCommand("INSERT INTO tbl_admin(AD,SOYAD,KULAD,SIFRE,EPOSTA,YETKI) VALUES(@AD,@SOYAD,@KULAD,@SIFRE,@EPOSTA,@YETKI)", con.bag);
        ////        command.Parameters.AddWithValue("@AD", txtad.Text);
        ////        command.Parameters.AddWithValue("@SOYAD", txtSOYAD.Text);
        ////        command.Parameters.AddWithValue("@SIFRE", txtSifre.Text);
        ////        command.Parameters.AddWithValue("@EPOSTA", txtEposta.Text);
        ////        command.Parameters.AddWithValue("@KULAD", txtKulad.Text);
        ////        command.Parameters.AddWithValue("@YETKI", txtYetki.Text);
        ////        command.ExecuteNonQuery();
        ////        this.alert("Kayıt Ekleme Başarılı", FormAlert.enumType.success);
        ////        listele();
        ////        con.bag.Close();
        ////    }
        ////}
        //con.bag.Open();
        //command = new MySqlCommand("SELECT * FROM tbl_admin", con.bag);
        //MySqlDataReader dr1 = command.ExecuteReader();
        //dr1.Read();
        //if (dr1["YETKI"].ToString() == "admin")
        //{
        //    Ekle();
        //}
        //con.bag.Close();
    }
}
