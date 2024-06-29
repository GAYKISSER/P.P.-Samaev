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

namespace дота_басш
{
    public partial class slot_add : Form
    {
        BD bd = new BD();
        public slot_add()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        public static string slot1;
        public static string slot2;
        public static string slot3;
        public static string slot4;
        public static string slot5;
        public static string slot6;
        private void button2_Click(object sender, EventArgs e)
        {
            //comboBox1.SelectedItem.ToString();
            try
            {
                switch (comboBox1.SelectedIndex)
                {
                    case 0: slot1= textBox1.Text;  break;
                    case 1: slot2 = textBox1.ToString(); break;
                    case 2: slot3 = textBox1.ToString(); break;
                    case 3: slot4 = textBox1.ToString(); break;
                    case 4: slot5 = textBox1.ToString(); break;
                    case 5: slot6 = textBox1.ToString(); break;

                }
                MessageBox.Show("действие выполнено", "Система");
            }
            catch
            {
                MessageBox.Show("ошибка", "ошибка");
            }
        }


        private void slot_add_Load(object sender, EventArgs e)
        {
            comboBox1.SelectedIndex = 0;
            grid();
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            comboBox1.DropDownStyle = ComboBoxStyle.DropDownList;
        }
        private void grid()
        {
            SqlConnection con = new SqlConnection(bd.sqlCon);
            con.Open();
            SqlCommand com = new SqlCommand(@"SELECT namme as 'название',opisanie as 'описание',id_baf as 'id умений',
            cena as 'цена',uron_up as 'урон',speed_up as 'скор. атк.',
lovcost as 'ловкость',sila as 'сила',intelect as 'интелект' FROM predmetu", con);
            SqlDataAdapter adapter = new SqlDataAdapter(com);
            DataSet ds = new DataSet();
            adapter.Fill(ds, "predmetu");
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
    }
}
