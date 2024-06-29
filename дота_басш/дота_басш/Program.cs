using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace дота_басш
{
    internal static class Program
    {
        /// <summary>
        /// Главная точка входа для приложения.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new avtoriz());
        }
    }

    class BD
    {
        public string sqlCon = @"server=DESTROYER2;Initial Catalog=dota;Integrated Security=true";

        public string StrCon()
        {
            return sqlCon;
        }
        public SqlDataAdapter queryEx(string query)
        {
            try
            {
                SqlConnection myCon = new SqlConnection(StrCon());
                myCon.Open();
                SqlDataAdapter SDA = new SqlDataAdapter(query, myCon);
                SDA.SelectCommand.ExecuteNonQuery();
                MessageBox.Show("действие выполнено", "система");
                return SDA;
            }
            catch
            {
                MessageBox.Show("ошибка", "ошибка");
                return null;
            }
        }
       
    }
}
