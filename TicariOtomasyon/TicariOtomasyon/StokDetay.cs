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
    public partial class StokDetay : Form
    {
        public StokDetay()
        {
            InitializeComponent();
        }
        MySqlDataAdapter da = new MySqlDataAdapter();
        DataTable dt = new DataTable();
        MySQLConnection con = new MySQLConnection();
        MySqlCommand command;
        void StokDetay1()
        {
            con.bag.Open();
            dt.Clear();
            da = new MySqlDataAdapter("SELECT *FROM tbl_urunler WHERE URUNAD='"+AD+"'", con.bag);
            da.Fill(dt);
            dgvStokveri.DataSource = dt;
            con.bag.Close();
        }
        public string AD;
        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void StokDetay_Load(object sender, EventArgs e)
        {
            StokDetay1();
        }
    }
}
