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
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using System.Data.Common;

namespace дота_басш
{
    public partial class polzov : Form
    {
        BD bd = new BD();

        public polzov()
        {
            InitializeComponent();
        }

        private void polz_Load(object sender, EventArgs e)
        {
            stats_vivod();
            grid1();
        }
        // обновление страницы
        private void polzov_MouseClick(object sender, MouseEventArgs e)
        {
           
        }
        private void exit_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
        Point lastpoint;
        // изменение положения окна
        private void polz_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                this.Left += e.X - lastpoint.X;
                this.Top += e.Y - lastpoint.Y;
            }
        }

        private void polz_MouseDown(object sender, MouseEventArgs e)
        {
            lastpoint = new Point(e.X, e.Y);
        }
        // взаимодействие с кнопкой выход
        private void label1_MouseEnter(object sender, EventArgs e)
        {
            label1.ForeColor = Color.Blue;
        }

        private void label1_MouseLeave(object sender, EventArgs e)
        {
            label1.ForeColor = Color.Black;
        }
        public int up;
        //public static int time_test = Convert.ToInt32(textBox_time_test.Text);
        public static double sila_up = 0;
        public static double lovkost_up = 0;
        public static double intelect_up = 0;
        public static double itog_lovk;
        public double sila = 20;
        public static double lovkost = 19;
        public double intelect = 15;
        public int bash = 12;
        public int bash_kol = 0;
        public double bash_pr = 0;
        public int lvl = 1;
        public double yron = 50;
        public double speed_up = 0;
        public int kol_ydar = 0;
        /// <summary>
        /// /
        /// </summary>
        public static double yca = 0 + lovkost;
        public double scor_atc = ((100 + yca) * 0.01) / 1.7;

        //
        public double uron_up_slotu = 0;
        public double speed_up_slotu = 0;
        public double lovcost_slotu = 0;
        public double sila_slotu = 0;
        public double intelect_slotu = 0;


        private async void slotu(string slot)
        {
            try
            {
                if (slot == "")
                {
                    return;
                }

                SqlConnection con = new SqlConnection(bd.sqlCon);
                con.Open();

                ////sila
                //SqlCommand com = new SqlCommand($"select sila from predmetu where namme='{slot}'", con);
                //sila_slotu += ((double)com.ExecuteScalar());
                ////lovcost
                //SqlCommand com_lovk = new SqlCommand($"select lovcost from predmetu where namme='{slot}'", con);
                //lovcost_slotu += ((double)com_lovk.ExecuteScalar());
                ////intelect
                //SqlCommand com_intel = new SqlCommand($"select intelect from predmetu where namme='{slot}'", con);
                //intelect_slotu += ((double)com_intel.ExecuteScalar());

                SqlCommand com = new SqlCommand($"select sila,lovcost,intelect,id_baf,uron_up,speed_up from predmetu where namme='{slot}'", con);
                SqlDataAdapter adapter = new SqlDataAdapter(com);
                SqlDataReader sqlReader = null;
                sqlReader = await com.ExecuteReaderAsync();
                while (await sqlReader.ReadAsync())
                {
                    //listBox1.Items.Add(Convert.ToString(sqlReader) + "  " + Convert.ToString(sqlReader["p_firm"])
                    //+ "  " + Convert.ToString(sqlReader["p_type"]) + "  " + Convert.ToString(sqlReader["p_price"]));
                    sila_slotu += Convert.ToDouble(sqlReader["sila"]);
                    lovcost_slotu += Convert.ToDouble(sqlReader["lovcost"]);
                    intelect_slotu+= Convert.ToDouble(sqlReader["intelect"]);
                    uron_up_slotu+= Convert.ToDouble(sqlReader["uron_up"]);
                    speed_up_slotu+= Convert.ToDouble(sqlReader["speed_up"]);
                    
                    //slot2.Text = lovcost_slotu.ToString(); //lovcost_slotu.ToString();
                }
                stats_vivod();




                //using (DbDataReader reader = com.ExecuteReader())
                //    while (reader.Read())
                //    {
                //        sila_slotu += reader.GetInt32(0);
                //        lovcost_slotu += reader.GetInt32(1);
                //        intelect_slotu += reader.GetInt32(2);
                //        uron_up_slotu += reader.GetInt32(4);
                //        speed_up_slotu += reader.GetInt32(5);
                //    }
                con.Close();            

            }
            catch
            {
                MessageBox.Show("ошибка", "ошибка");
            }
        }


        public void stats_vivod()
        {
            itog_lovk = lovkost + lovkost_up + lovcost_slotu;
            yca = speed_up + itog_lovk + speed_up_slotu;
            yron = 50 + itog_lovk ;
            scor_atc = ((100 + yca) * 0.01) / 1.7;
            //update_speed_atc();
            textBox_sila.Text = Convert.ToString(sila+ sila_up + sila_slotu);
            textBox_lovk.Text = Convert.ToString(itog_lovk);
            textBox_intelect.Text = Convert.ToString(intelect + intelect_up + intelect_slotu);
            textBox_speed_atc.Text = Convert.ToString(scor_atc);
            textBox_yron.Text = Convert.ToString(yron + uron_up_slotu);
        }
        //
        public void update_stats_lvl()
        {
            switch (up)
            {
                case 1: sila_up += 2.6; lovkost_up += 3.3; intelect_up += 1.5; break;
                case 10: sila_up += 2.6*10; lovkost_up += 3.3 * 10; intelect_up += 1.5 * 10; break;
                case -1: sila_up -= 2.6; lovkost_up -= 3.3; intelect_up -= 1.5; break;
                case -10: sila_up -= 2.6 * 10; lovkost_up -= 3.3 * 10; intelect_up -= 1.5 * 10; break;                   
            }
            if (sila_up > 75.4) {sila_up = 75.4;}
            if (sila_up < 0) {sila_up = 0;}
            if (lovkost_up > 95.7) { lovkost_up = 95.7; }
            if (lovkost_up < 0) { lovkost_up = 0; }
            if (intelect_up > 43.5) { intelect_up = 43.5; }
            if (intelect_up < 0) { intelect_up = 0; }
            stats_vivod();
        }
        // пределы левела
        public void update_lvl_bash()
        {
            if (lvl > 30)
            {
                lvl = 30;
            }
            if (lvl < 1)
            {
                lvl = 1;
            }
            switch (lvl)
            {
                case 1: bash = 12; break;
                case 2: bash = 16; break;
                case 3: bash = 20; break;
                default: bash = 24; break;
            }
            label2.Text = $"Ур. персонажа: {lvl}";
            label_bash.Text = $"Шанс басша: {bash}%";
        }
        // изменение левела
        private void button_up1_Click(object sender, EventArgs e)
        {
            lvl += 1;
            update_lvl_bash();
            up = 1;
            update_stats_lvl();
        }

        private void button_up10_Click(object sender, EventArgs e)
        {
            lvl += 10;
            update_lvl_bash();
            up = 10;
            update_stats_lvl();
        }

        private void button_down1_Click(object sender, EventArgs e)
        {
            lvl -= 1;
            update_lvl_bash();
            up = -1;
            update_stats_lvl();
        }

        private void button_down10_Click(object sender, EventArgs e)
        {
            lvl -= 10;
            update_lvl_bash();
            up = -10;
            update_stats_lvl();
        }
        // очистка данных теста
        private void button_del_Click(object sender, EventArgs e)
        {
            textBox_time_test.Text = "";
            lvl = 1;
             uron_up_slotu = 0;
            speed_up_slotu = 0;
            lovcost_slotu = 0;
            sila_slotu = 0;
            intelect_slotu = 0;
            update_lvl_bash();
            update_stats_lvl();
            slot1.Text = "";
            slot2.Text = "";
            slot3.Text = "";
            slot4.Text = "";
            slot5.Text = "";
            slot6.Text = "";

            label_kol_ydar.Text = "0";
            label_kol_bash.Text = "0";
            label_pr_bash.Text = "0";
            label_itog_eron.Text = "0";
            
        }

        private void button_add_Click(object sender, EventArgs e)
        {
            slot_add a = new slot_add();
            a.Show();
            button3.Enabled = true;
        }
        // таблицы 
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
            SqlCommand com = new SqlCommand($@"SELECT id_test as 'id теста',id_polzovatel as 'id пользователя',levell as 'уровень',
            yron as 'урон',speed as 'скор. атк.',sila as 'сила', lovcost as 'ловкость',intelect as 'интелект',slot_1 as 'слот 1',slot_2 as 'слот 2',
slot_3 as 'слот 3', slot_4 as 'слот 4', slot_5 as 'слот 5', slot_6 as 'слот 6',kol_bash as 'кол-во басшей',pr_bash as '% басша',kol_ydar as 'кол-во ударов',
itog_damage as 'итоговый урон',time_test as 'время теста' FROM test_bash WHERE id_polzovatel ='{avtoriz.id_polzov}' ", con);
            SqlDataAdapter adapter = new SqlDataAdapter(com);
            DataSet ds = new DataSet();
            adapter.Fill(ds, "test_bash");
            dataGridView1.DataSource = ds.Tables[0];
            con.Close();
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

        private void button2_Click(object sender, EventArgs e)
        {
            grid1();
            button_del_me.Enabled = false;
        }

        private void button_sort_me_Click(object sender, EventArgs e)
        {
            grid2();
            button_del_me.Enabled = true;
        }

        private void label1_Click(object sender, EventArgs e)
        {
            registration a = new registration();
            this.Close();
            a.Show();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            slot1.Text = "";
            slot2.Text = "";
            slot3.Text = "";
            slot4.Text = "";
            slot5.Text = "";
            slot6.Text = "";
        }

        private void button_del_me_Click(object sender, EventArgs e)
        {

                var a = $"delete test_bash where id_test = '{textBox1.Text}'";

            
        }

        private void button3_Click_1(object sender, EventArgs e)
        {
            try
            {
                slot1.Text = slot_add.slot1;
                slot2.Text = slot_add.slot2;
                slot3.Text = slot_add.slot3;
                slot4.Text = slot_add.slot4;
                slot5.Text = slot_add.slot5;
                slot6.Text = slot_add.slot6;



                slotu(slot_add.slot1);
                slotu(slot_add.slot2);
                slotu(slot_add.slot3);
                slotu(slot_add.slot4);
                slotu(slot_add.slot5);
                slotu(slot_add.slot6);
                

                button3.Enabled = false;
            }
            catch 
            {
                return;
            }

        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            MessageBox.Show($"Данная программа ещё не совершенна и пока может быть\n не так удобна в использовании как хотелось","Объяснение");
        }
        public double itog_yron = 0;
        private void button_start_test_Click(object sender, EventArgs e)
        {
            try
            {
                yca = speed_up + itog_lovk + speed_up_slotu;
                scor_atc = ((100 + yca) * 0.01) / 1.7;
                int bash_t=0;
                int bash_izm = bash;
                Random rnd = new Random();
                int time = Convert.ToInt32(textBox_time_test.Text);
                for (double i=scor_atc; i <= time; i++)
                {
                    bash_t = rnd.Next(1, 101);
                    if(bash_izm >= bash_t)
                    {
                        bash_izm = bash;
                        bash_kol += 1;
                        kol_ydar += 1;
                        bash_t = rnd.Next(1, 101);
                        if (bash_izm >= bash_t)
                        {
                            bash_kol += 1;
                        }
                        else
                        {
                            bash_izm += 4;
                            kol_ydar += 1;
                        }
                        

                    }
                    else
                    {
                        bash_izm += 4;
                        kol_ydar += 1;
                    }
                }
                //
                if(bash_kol ==0)
            {
                bash_kol = 1;
            }
                if(kol_ydar == 0)
            {
                kol_ydar = 1;
            }
                bash_pr = kol_ydar/ bash_kol;
                 itog_yron = (kol_ydar * yron) + (bash_kol * (yron + 25));
                label_itog_eron.Text=itog_yron.ToString();
                label_kol_bash.Text=bash_kol.ToString();
                label_kol_ydar.Text=kol_ydar.ToString();
                label_pr_bash.Text=bash_pr.ToString();

                kol_ydar = 0;
                bash_kol = 0;
                bash_pr = 0;
            }
            catch
            {
                MessageBox.Show("Ошибка, неверные данные ", "Система");
            }

            button_save.Enabled = true;
        }

        private void button_save_Click(object sender, EventArgs e)
        {
            double itog_sila = sila + sila_up + sila_slotu;
            
            if (avtoriz.lvl_dopusc_polzov != 0)
            {
                var aa = $"insert into test_bash(id_polzovatel,levell,yron," +
                    $"speed,sila,lovcost,intelect,slot_1,slot_2,slot_3,slot_4," +
                    $"slot_5,slot_6,kol_bash,pr_bash,kol_ydar,itog_damage,time_test)" +
                    $" values ('{avtoriz.id_polzov}','{lvl}','{Convert.ToInt32(itog_yron)}','{Convert.ToInt32(scor_atc)}','{Convert.ToInt32(itog_sila)}','{Convert.ToInt32(itog_lovk)}'," +
                    $"'{Convert.ToInt32(intelect + intelect_up + intelect_slotu)}','{slot1.Text}','{slot2.Text}','{slot3.Text}','{slot4.Text}','{slot5.Text}','{slot6.Text}'," +
                    $"'{label_kol_bash.Text}','{label_pr_bash.Text}','{label_kol_ydar.Text}','{Convert.ToInt32(itog_yron)}','{textBox_time_test.Text}')";
                bd.queryEx(aa);
            }
            else
            {
                MessageBox.Show("Вы забанены и не можете сохронять тесты ", "Система");
            }
        }
    }
}
