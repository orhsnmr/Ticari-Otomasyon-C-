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
    public partial class Notdetay : Form
    {
        public Notdetay()
        {
            InitializeComponent();
        }
        public string detay;

        private void Notdetay_Load(object sender, EventArgs e)
        {
            txtDetay.Text = detay;
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
