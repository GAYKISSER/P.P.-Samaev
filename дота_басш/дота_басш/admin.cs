using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace дота_басш
{
    public partial class admin : Form
    {
        BD bd = new BD();
        public admin()
        {
            InitializeComponent();
        }

        private void admin_Load(object sender, EventArgs e)
        {
            grid1();
            grid2();
            grid3();
        }
        private void pictureBox_help_test_Click(object sender, EventArgs e)
        {
            MessageBox.Show("В этой вкладке реализовано удаление тестов", "Система");
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            MessageBox.Show("В этой вкладке реализовано изменение предметов", "Система");
        }
        private void pictureBox_help_polzov_Click(object sender, EventArgs e)
        {
            MessageBox.Show("В этой вкладке реализовано взаимодействие с данными пользователей", "Система");
        }
        Point lastpoint;
        // изменение положения окна
        private void admin_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                this.Left += e.X - lastpoint.X;
                this.Top += e.Y - lastpoint.Y;
            }
        }

        private void admin_MouseDown(object sender, MouseEventArgs e)
        {
            lastpoint = new Point(e.X, e.Y);
        }
        private void grid1()
        {
            SqlConnection con = new SqlConnection(bd.sqlCon);
            con.Open();
            SqlCommand com = new SqlCommand(@"SELECT id_test as 'id теста',id_polzovatel as 'id пользователя',levell as 'уровень',
            yron as 'урон',speed as 'скор. атк.',sila as 'сила', lovcost as 'ловкость',intelect as 'интелект',slot_1 as 'слот 1',slot_2 as 'слот 2',
slot_3 as 'слот 3', slot_4 as 'слот 4', slot_5 as 'слот 5', slot_6 as 'слот 6',kol_bash as 'кол-во басшей',pr_bash as '% басша',kol_ydar as 'кол-во ударов',
itog_damage as 'итоговый урон',time_test as 'время теста' FROM test_bash", con);
            SqlDataAdapter adapter = new SqlDataAdapter(com);
            DataSet ds = new DataSet();
            adapter.Fill(ds, "test_bash");
            dataGridView1.DataSource = ds.Tables[0];
            con.Close();
        }
        private void grid2()
        {
            SqlConnection con = new SqlConnection(bd.sqlCon);
            con.Open();
            SqlCommand com = new SqlCommand(@"SELECT namme as 'название',opisanie as 'описание',id_baf as 'id умений',
            cena as 'цена',uron_up as 'урон',speed_up as 'скор. атк.',
lovcost as 'ловкость',sila as 'сила',intelect as 'интелект' FROM predmetu", con);
            SqlDataAdapter adapter = new SqlDataAdapter(com);
            DataSet ds = new DataSet();
            adapter.Fill(ds, "predmetu");
            dataGridView2.DataSource = ds.Tables[0];
            con.Close();
        }
        private void grid3()
        {
            SqlConnection con = new SqlConnection(bd.sqlCon);
            con.Open();
            SqlCommand com = new SqlCommand(@"SELECT id as 'id пользователя',loginn as 'логин',pasword as 'пароль',
            dopusk_lvl as 'уровень допуска' FROM polzov", con);
            SqlDataAdapter adapter = new SqlDataAdapter(com);
            DataSet ds = new DataSet();
            adapter.Fill(ds, "polzov");
            dataGridView3.DataSource = ds.Tables[0];
            con.Close();
        }
        private void button1_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }


        private void button_ocno_polzov_Click(object sender, EventArgs e)
        {
            polzov a = new polzov();
            this.Close();
            a.Show();
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            int a = e.RowIndex;

            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = dataGridView1.Rows[a];
                //////////////////
                textBox1.Text = row.Cells[0].Value.ToString();
            }
        }

        private void dataGridView2_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            int a = e.RowIndex;
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = dataGridView2.Rows[a];
                //////////////////
                textBox_name_predm.Text = row.Cells[0].Value.ToString();
                textBox_opis_predm.Text = row.Cells[1].Value.ToString();
                textBox_id_baff.Text = row.Cells[2].Value.ToString();
                textBox_cena.Text = row.Cells[3].Value.ToString();
                textBox_yron_predm.Text = row.Cells[4].Value.ToString();
                textBox_speed_atc_predm.Text = row.Cells[5].Value.ToString();
                textBox_lovc_predm.Text = row.Cells[6].Value.ToString();
                textBox_sila_predm.Text = row.Cells[7].Value.ToString();
                textBox_intel_predm.Text = row.Cells[8].Value.ToString();
            }
        }

        private void dataGridView3_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            int a = e.RowIndex;

            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = dataGridView3.Rows[a];
                //////////////////
                textBox_id_p.Text = row.Cells[0].Value.ToString();
                textBox_login.Text = row.Cells[1].Value.ToString();
                textBox_pasword.Text = row.Cells[2].Value.ToString();
                textBox_lvl_dopuska.Text = row.Cells[3].Value.ToString();
            }
        }


        //
        private void button_dell_test_Click(object sender, EventArgs e)
        {
            var a = $"delete test_bash where id_test='{textBox1.Text}'";
            bd.queryEx(a);
            grid1();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            textBox_name_predm.Text = "";
            textBox_opis_predm.Text = "";
            textBox_id_baff.Text = "";
            textBox_cena.Text = "";
            textBox_yron_predm.Text = "";
            textBox_speed_atc_predm.Text = "";
            textBox_lovc_predm.Text = "";
            textBox_sila_predm.Text = "";
            textBox_intel_predm.Text = "";
        }

        private void button_del_predmet_Click(object sender, EventArgs e)
        {

                var a = $"delete predmetu where namme='{textBox_name_predm.Text}'";
                bd.queryEx(a);
            grid2();
        }

        private void button_add_predmet_Click(object sender, EventArgs e)
        {
            var a = $"insert into predmetu values" +
                $"('{textBox_name_predm.Text}','{textBox_opis_predm.Text}','" +
                $"{textBox_id_baff.Text}','{textBox_cena.Text}','{textBox_yron_predm.Text}','" +
                $"{textBox_speed_atc_predm.Text}','{textBox_lovc_predm.Text}','{textBox_sila_predm.Text}','" +
                $"{textBox_intel_predm.Text}')";
            bd.queryEx(a);
            grid2();
        }

        private void button_izm_predmet_Click(object sender, EventArgs e)
        {
            var a = $"update predmetu set opisanie='{textBox_opis_predm.Text}'," +
               $"id_baf='{textBox_id_baff.Text}',cena='{textBox_cena.Text}',uron_up='{textBox_yron_predm.Text}'," +
               $"speed_up='{textBox_speed_atc_predm.Text}',lovcost='{textBox_lovc_predm.Text}',sila='{textBox_sila_predm.Text}'," +
               $"intelect='{textBox_intel_predm.Text}' where namme = '{textBox_name_predm.Text}'";
            bd.queryEx(a);
            grid2();
        }


        private Boolean Check()
        {
            BD bd = new BD();
            SqlConnection con = new SqlConnection(bd.sqlCon);
            var log = textBox_login.Text;
            SqlDataAdapter ad = new SqlDataAdapter();
            DataTable tab = new DataTable();
            string a = $"select * from polzov where loginn = '{log}'";
            SqlCommand command = new SqlCommand(a, con);
            ad.SelectCommand = command;
            ad.Fill(tab);
            if (tab.Rows.Count > 0)
            {
                MessageBox.Show("такой пользователь уже есть", "Ошибка");
                return true;
            }
            else
            {
                return false;
            }

        }
        private void button_clear_Click(object sender, EventArgs e)
        {
            textBox_id_p.Text = "";
            textBox_login.Text = "";
            textBox_pasword.Text = "";
            textBox_lvl_dopuska.Text = "";
        }

        private void button_del_polz_Click(object sender, EventArgs e)
        {
            var a = $"delete polzov where id='{textBox_id_p.Text}'";
            bd.queryEx(a);
            grid3();
        }

        private void button_add_polz_Click(object sender, EventArgs e)
        {
            var a = $"insert into polzov(loginn,pasword,dopusk_lvl) values('{textBox_login.Text}','{textBox_pasword.Text}','" +
                $"1')";
            bd.queryEx(a);
            grid3();
        }

        private void button_izm_polz_Click(object sender, EventArgs e)
        {
            var a = $"update  polzov set loginn='{textBox_login.Text}'," +
                $"pasword='{textBox_pasword.Text}',dopusk_lvl='{textBox_lvl_dopuska.Text}' where id ='{textBox_id_p.Text}'";
            if (Check())
            {
                return;
            }
            bd.queryEx(a);
            grid3();
        }

        private void button_ban_Click(object sender, EventArgs e)
        {
            var a = $"update  polzov set dopusk_lvl='0' where id ='{textBox_id_p.Text}'";
            bd.queryEx(a);
            grid3();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            var a = $"update  polzov set dopusk_lvl= '1' where id ='{textBox_id_p.Text}'";
            bd.queryEx(a);
            grid3();
        }
    }
}
