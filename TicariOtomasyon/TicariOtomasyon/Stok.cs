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
    public partial class Stok : Form
    {
        public Stok()
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
            da = new MySqlDataAdapter("SELECT URUNAD,SUM(ADET) AS 'MIKTAR' FROM tbl_urunler GROUP BY URUNAD", con.bag);
            da.Fill(dt);
            dgvStokveri.DataSource = dt;
            con.bag.Close();
        }

        private void Stok_Load(object sender, EventArgs e)
        {
            listele();
            con.bag.Open();
            command = new MySqlCommand("SELECT URUNAD,SUM(ADET) AS 'MIKTAR' FROM tbl_urunler GROUP BY URUNAD", con.bag);
            MySqlDataReader dr = command.ExecuteReader();
            while (dr.Read())
            {
                chartControl1.Series["Series 1"].Points.AddPoint(Convert.ToString(dr[0]), int.Parse(dr[1].ToString()));
            }
            con.bag.Close();
            con.bag.Open();
            MySqlCommand command1 = new MySqlCommand("SELECT IL,COUNT(*) FROM tbl_firma GROUP BY IL", con.bag);
            MySqlDataReader dr1 = command1.ExecuteReader();
            while (dr1.Read())
            {
                chartControl2.Series["Series 1"].Points.AddPoint(Convert.ToString(dr1[0]), int.Parse(dr1[1].ToString()));
            }
            con.bag.Close();
        }
        
        private void dgvStokveri_DoubleClick(object sender, EventArgs e)
        {
            StokDetay stokDetay = new StokDetay();
            int secilenalan = dgvStokveri.SelectedCells[0].RowIndex;
            stokDetay.AD = dgvStokveri.Rows[secilenalan].Cells[0].Value.ToString();
            stokDetay.Show();
        }
    }
}
