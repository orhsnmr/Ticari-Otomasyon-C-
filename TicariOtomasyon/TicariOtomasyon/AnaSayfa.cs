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
    public partial class AnaSayfa : Form
    {
        public AnaSayfa()
        {
            InitializeComponent();
        }
        public void alert(string msg, FormAlert.enumType type)
        {
            FormAlert frm = new FormAlert();
            frm.showAlert(msg, type);
        }
        Urunler urun;
        private void btnurun_Click_1(object sender, EventArgs e)
        {

            if (urun == null || urun.IsDisposed)
            {
                urun = new Urunler();
                this.IsMdiContainer = true;
                urun.MdiParent = this;
                urun.Show();
                urun.Dock = DockStyle.Fill;
            }
            else
            {
                this.alert("Form Zaten Açık", FormAlert.enumType.error);
            }
          

        }       
        private void btnClose_Click_1(object sender, EventArgs e)
        {
            Application.Exit();
        }
        Musteriler musteri;
        private void btnMuster_Click(object sender, EventArgs e)
        {
            if (musteri == null || musteri.IsDisposed)
            {
                musteri = new Musteriler();
                this.IsMdiContainer = true;
                musteri.MdiParent = this;
                musteri.Show();
                musteri.Dock = DockStyle.Fill;
            }
            else
            {
                this.alert("Form Zaten Açık",FormAlert.enumType.error);
            }          
        }
        Firma firma;
        private void btnFirma_Click(object sender, EventArgs e)
        {
            if (firma == null || firma.IsDisposed)
            {
                firma = new Firma();
                this.IsMdiContainer = true;
                firma.MdiParent = this;
                firma.Show();
                firma.Dock = DockStyle.Fill;
            }
            else
            {
                this.alert("Form Zaten Açık", FormAlert.enumType.error);
            }
        }
        Personeller person;
        private void btnPersonel_Click(object sender, EventArgs e)
        {
            if (person == null || person.IsDisposed)
            {
                person = new Personeller();
                this.IsMdiContainer = true;
                person.MdiParent = this;
                person.Show();
                person.Dock = DockStyle.Fill;
            }
            else
            {
                this.alert("Form Zaten Açık", FormAlert.enumType.error);
            }
        }

        Rehber rhb;
        private void btnTel_Click_1(object sender, EventArgs e)
        {
            if (rhb == null || rhb.IsDisposed)
            {
                rhb = new Rehber();
                this.IsMdiContainer = true;
                rhb.MdiParent = this;
                rhb.Show();
                rhb.Dock = DockStyle.Fill;
            }
            else
            {
                this.alert("Form Zaten Açık", FormAlert.enumType.error);
            }
        }
        Giderler gider;
        private void btnGider_Click(object sender, EventArgs e)
        {
            if (gider == null || gider.IsDisposed)
            {
                gider = new Giderler();
                this.IsMdiContainer = true;
                gider.MdiParent = this;
                gider.Show();
                gider.Dock = DockStyle.Fill;
            }
            else
            {
                this.alert("Form Zaten Açık", FormAlert.enumType.error);
            }
        }
        Banka bank;
        private void btnBanka_Click(object sender, EventArgs e)
        {
            if (bank == null || bank.IsDisposed)
            {
                bank = new Banka();
                this.IsMdiContainer = true;
                bank.MdiParent = this;
                bank.Show();
                bank.Dock = DockStyle.Fill;
            }
            else
            {
                this.alert("Form Zaten Açık", FormAlert.enumType.error);
            }
        }
        Fatura ftr;
        private void btnFatura_Click(object sender, EventArgs e)
        {
            if (ftr == null || ftr.IsDisposed)
            {
                ftr = new Fatura();
                this.IsMdiContainer = true;
                ftr.MdiParent = this;
                ftr.Show();
                ftr.Dock = DockStyle.Fill;
            }
            else
            {
                this.alert("Form Zaten Açık", FormAlert.enumType.error);
            }
        }
        Notlar not;
        private void btnNotes_Click(object sender, EventArgs e)
        {
            if (not == null || not.IsDisposed)
            {
                not = new Notlar();
                this.IsMdiContainer = true;
                not.MdiParent = this;
                not.Show();
                not.Dock = DockStyle.Fill;
            }
            else
            {
                this.alert("Form Zaten Açık", FormAlert.enumType.error);
            }
        }
        Haraketler haraket;
        private void btnIstatistik_Click(object sender, EventArgs e)
        {
            if (haraket == null || haraket.IsDisposed)
            {
                haraket = new Haraketler();
                this.IsMdiContainer = true;
                haraket.MdiParent = this;
                haraket.Show();
                haraket.Dock = DockStyle.Fill;
            }
            else
            {
                this.alert("Form Zaten Açık", FormAlert.enumType.error);
            }
        }

        
        public string kulad, eposta,ad,soyad,adres,telefon;
        private void Form1_Load(object sender, EventArgs e)
        {
            ana = new FormAnaSayfa();
            this.IsMdiContainer = true;
            ana.MdiParent = this;
            ana.Show();
            ana.Dock = DockStyle.Fill;
        }
        Raporlar rapor;

        Kasa kasa;
        private void btnKasa_Click(object sender, EventArgs e)
        {
            if (kasa == null || kasa.IsDisposed)
            {
                kasa = new Kasa();
                this.IsMdiContainer = true;
                kasa.MdiParent = this;
                kasa.Show();
                kasa.Dock = DockStyle.Fill;
            }
            else
            {
                this.alert("Form Zaten Açık", FormAlert.enumType.error);
            }
        }
        FormAnaSayfa ana;
        private void btnHomepage_Click(object sender, EventArgs e)
        {
            if (ana == null || ana.IsDisposed)
            {
                ana = new FormAnaSayfa();
                this.IsMdiContainer = true;
                ana.MdiParent = this;
                ana.Show();
                ana.Dock = DockStyle.Fill;
            }
            else
            {
                this.alert("Form Zaten Açık", FormAlert.enumType.error);
            }
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void btnRapor_Click(object sender, EventArgs e)
        {
            if (rapor == null || rapor.IsDisposed)
            {
                rapor = new Raporlar();
                this.IsMdiContainer = true;
                rapor.MdiParent = this;
                rapor.Show();
                rapor.Dock = DockStyle.Fill;
            }
            else
            {
                this.alert("Form Zaten Açık", FormAlert.enumType.error);
            }
        }
        Stok stok;
        private void btnStok_Click(object sender, EventArgs e)
        {
            if (stok == null || stok.IsDisposed)
            {
                stok = new Stok();
                this.IsMdiContainer = true;
                stok.MdiParent = this;
                stok.Show();
                stok.Dock = DockStyle.Fill;
            }
            else
            {
                this.alert("Form Zaten Açık", FormAlert.enumType.error);
            }
        }
        Ayarlar ayar;

        private void btnAyarlar_Click_1(object sender, EventArgs e)
        {
            if (ayar == null || ayar.IsDisposed)
            {
                ayar = new Ayarlar();
                this.IsMdiContainer = true;
                ayar.MdiParent = this;
                ayar.lblKulad.Text = kulad;
                ayar.lblEposta.Text = eposta;
                ayar.lblAd.Text = ad + " " + soyad;
                ayar.txtAd.Text = ad;
                ayar.txtSoyad.Text = soyad;
                ayar.lblAdres.Text = adres;
                ayar.lblTel.Text = telefon;
                ayar.Show();
                ayar.Dock = DockStyle.Fill;
            }
            else
            {
                this.alert("Form Zaten Açık", FormAlert.enumType.error);
            }
        }
    }
}
