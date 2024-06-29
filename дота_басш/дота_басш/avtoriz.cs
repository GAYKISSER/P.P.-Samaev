using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.Reflection.Emit;

namespace дота_басш
{
    public partial class avtoriz : Form
    {
        BD bd = new BD();
        public avtoriz()
        {
            InitializeComponent();
        }
        // метод для нахождения id
        public static int id_polzov;
        private void id()
        {
            SqlConnection con = new SqlConnection(bd.sqlCon);
            con.Open();
            SqlCommand com = new SqlCommand($"select id from polzov where loginn = '{textBox1.Text}' and pasword = '{textBox2.Text}'", con);
            id_polzov = ((int)com.ExecuteScalar());
            con.Close();
        }
        public static int lvl_dopusc_polzov;
        private void lvl_dopiscc()
        {
            SqlConnection con = new SqlConnection(bd.sqlCon);
            con.Open();
            SqlCommand com = new SqlCommand($"select dopusk_lvl from polzov where loginn = '{textBox1.Text}' and pasword = '{textBox2.Text}'", con);
            lvl_dopusc_polzov = ((int)com.ExecuteScalar());
            con.Close();
        }
        // кнопка вход
        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                // переделать запросы
                var loginUs = textBox1.Text;
                var paswordUs = textBox2.Text;
                int aa = 1;
                int bb = 2;
                int cc = 0;

                SqlConnection con = new SqlConnection(bd.sqlCon);
                string a = $"select * from polzov where loginn = '{loginUs}' and pasword = '{paswordUs}' and dopusk_lvl = '{aa}'";
                string b = $"select * from polzov where loginn = '{loginUs}' and pasword = '{paswordUs}' and dopusk_lvl = '{bb}'";
                string c = $"select * from polzov where loginn = '{loginUs}' and pasword = '{paswordUs}' and dopusk_lvl = '{cc}'";


                SqlCommand command = new SqlCommand(a, con);
                SqlCommand command1 = new SqlCommand(b, con);
                SqlCommand command2 = new SqlCommand(c, con);
                DataTable table = new DataTable();
                DataTable table1 = new DataTable();
                DataTable table2 = new DataTable();
                SqlDataAdapter adapter = new SqlDataAdapter();
                SqlDataAdapter adapter1 = new SqlDataAdapter();
                SqlDataAdapter adapter2 = new SqlDataAdapter();

               
                adapter.SelectCommand = command;
                adapter.Fill(table);
                adapter1.SelectCommand = command1;
                adapter1.Fill(table1);
                adapter.SelectCommand = command2;
                adapter.Fill(table2);


                id();
                lvl_dopiscc();


                if (table1.Rows.Count == 1)
                {
                    MessageBox.Show("приветствую администратор", "система");
                    this.Hide();
                    admin user = new admin();
                    user.Show();
                }

                else if (table.Rows.Count == 1)
                {
                    MessageBox.Show("вы успешно вошли", "система");
                    this.Hide();
                    polzov ad = new polzov();
                    ad.Show();
                }
                else if (table2.Rows.Count == 1)
                {
                    MessageBox.Show("вы успешно вошли", "система");
                    this.Hide();
                    polzov ad = new polzov();
                    ad.Show();
                }
                else
                {
                    MessageBox.Show("неверные данные", "Ошибка");
                }
                con.Close();
            }
            catch
            {
                MessageBox.Show("Такого пользователя нет", "Система");
            }
        }

        private void exit_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
        Point lastpoint;
        // изменение положения окна
        private void avtorization_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                this.Left += e.X - lastpoint.X;
                this.Top += e.Y - lastpoint.Y;
            }
        }

        private void avtorization_MouseDown(object sender, MouseEventArgs e)
        {
            lastpoint = new Point(e.X, e.Y);
        }
        // взаимодействие с кнопкой выход
        private void label4_MouseEnter(object sender, EventArgs e)
        {
            label2.ForeColor = Color.Blue;
        }

        private void label4_MouseLeave(object sender, EventArgs e)
        {
            label2.ForeColor = Color.Black;
        }
        // переход на другое окно
        private void label4_Click(object sender, EventArgs e)
        {
            this.Hide();
            registration reg = new registration();
            reg.Show();
        }
    }
}
