using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;

namespace TicariOtomasyon
{
    class MySQLConnection
    {
        public static string conn = ConfigurationManager.ConnectionStrings["MySQL"].ToString();
        public MySqlConnection bag = new MySqlConnection(conn);

        public string baglanti_kontrol()
        {
            try
            {
                bag.Open();
                return "true";
            }
            catch (MySqlException ex)
            {
                return ex.Message;
                throw;
            }
        }
    }
}
