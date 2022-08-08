using System;
using System.Net.Mail;
using System.Windows.Forms;
using Google.Apis.Auth.OAuth2;
using Google.Apis.Gmail.v1;
using Google.Apis.Gmail.v1.Data;
using Google.Apis.Services;
using Google.Apis.Util.Store;

namespace TicariOtomasyon
{
    public partial class Mail : Form
    {
        public Mail()
        {
            InitializeComponent();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        public void alert(string msg, FormAlert.enumType type)
        {
            FormAlert frm = new FormAlert();
            frm.showAlert(msg, type);
        }
//smtpClient.Credentials = new System.Net.NetworkCredential("orhansm89@mail.com", "1?9_0*7!198.644");
        private void btnSend_Click(object sender, EventArgs e)
        {
            MailMessage mail = new MailMessage();            
            mail.From = new MailAddress("orhansm89@mail.com");
            mail.To.Add(txtKime.Text);
            mail.Subject = txtKonu.Text;
            mail.Body = txtMesaj.Text;

            SmtpClient SmtpServer = new SmtpClient();
            SmtpServer.UseDefaultCredentials = false;
            SmtpServer.Credentials = new System.Net.NetworkCredential("orhansm89@mail.com", "Gara4ever.");            
            SmtpServer.DeliveryMethod = SmtpDeliveryMethod.Network;
            SmtpServer.Host = "smtp.gmail.com";            
            SmtpServer.Port = 587;            
            SmtpServer.EnableSsl = true;
            this.alert("Mail Gönderildi", FormAlert.enumType.success);
        }
    }
}
