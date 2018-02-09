using System; 
using System.Collections.Generic; 
using System.ComponentModel; 
using System.Data; 
using System.Drawing; 
using System.Linq; 
using System.Text; 
using System.Threading.Tasks; 
using System.Windows.Forms; 
using MySql.Data.MySqlClient; 

namespace project
{
    public partial class Form1 : Form
    {
        MySqlConnection MySqlConnection;
        public Form1()
        {
            InitializeComponent();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void Form1_Load(object sender, EventArgs e)
        {
            string conStr = "server = localhost; user = root; database = mydb; password = root; ";
            MySqlConnection = new MySqlConnection(conStr);
            MySqlConnection.Open();
            MySqlDataReader reader = null;
            MySqlCommand command = new MySqlCommand("SELECT * FROM people", MySqlConnection);
            try
            {
                reader = command.ExecuteReader();
                while (reader.Read())
                {
                    listBox1.Items.Add(Convert.ToString(reader["worker_id"]) + " " + Convert.ToString(reader["f_name"]) + " " + Convert.ToString(reader["l_name"]) + " " + Convert.ToString(reader["position"]));
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString(), ex.Source.ToString(), MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                if (reader != null)
                    reader.Close();
            }
        }
        // Добавление сотрудников 
        private void button1_Click(object sender, EventArgs e)
        {
            MySqlCommand command = new MySqlCommand("INSERT INTO people (f_name, l_name, position) VALUES (@f_name, @l_name, @position)", MySqlConnection);
            command.Parameters.AddWithValue("f_name", textBox2.Text);
            command.Parameters.AddWithValue("l_name", textBox3.Text);
            command.Parameters.AddWithValue("position", textBox4.Text);
            command.ExecuteNonQuery();
            MySqlDataReader reader = null;
            MySqlCommand commands = new MySqlCommand("SELECT *, MAX ('worker id')", MySqlConnection);
            try
            {
                reader = commands.ExecuteReader();
                while (reader.Read())
                {
                    double f, s;
                    f = Convert.ToDouble(reader["worker_id"]);

                    s = f + 1;
                    MySqlCommand com = new MySqlCommand("INSERT INTO people (worker_id) VALUES (@worker_id)");
                    com.Parameters.AddWithValue("@worker_id", s);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString(), ex.Source.ToString(), MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                if (reader != null)
                    reader.Close();
            }


        }

        private void обновитьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            listBox1.Items.Clear();
            MySqlDataReader reader = null;
            MySqlCommand command = new MySqlCommand("SELECT * FROM people", MySqlConnection);
            try
            {
                reader = command.ExecuteReader();
                while (reader.Read())
                {
                    listBox1.Items.Add(Convert.ToString(reader["worker_id"]) + " " + Convert.ToString(reader["f_name"]) + " " + Convert.ToString(reader["l_name"]) + " " + Convert.ToString(reader["position"]));
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString(), ex.Source.ToString(), MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                if (reader != null)
                    reader.Close();
            }
        }
        //Расчет ЗП 

        private void расчетToolStripMenuItem_Click_1(object sender, EventArgs e)
        {
            listBox1.Items.Clear();
            MySqlDataReader reader = null;
            MySqlCommand command = new MySqlCommand("SELECT * FROM zp", MySqlConnection);

            try
            {
                reader = command.ExecuteReader();
                while (reader.Read())
                {
                    double  b, c, d;
                    b = Convert.ToDouble(reader["zp_in_month"]);
                    c = Double.Parse(textBox5.Text);
                    d = b * c;

                    listBox1.Items.Add(Convert.ToString(d));
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString(), ex.Source.ToString(), MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                if (reader != null)
                    reader.Close();
            }
        }
        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (MySqlConnection != null && MySqlConnection.State != ConnectionState.Closed)
                MySqlConnection.Close();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            //double a;
            //MySqlDataReader reader = null;
            //MySqlCommand idzp = new MySqlCommand("SELECT * FROM people , MAX ('worker_id')", MySqlConnection);
            //reader = idzp.ExecuteReader();
            //a = Convert.ToDouble(reader["worker_id"]);
            //MySqlCommand vnos = new MySqlCommand("INSERT INTO zp (people_worker_id) VALUES (@people_worker_id)");
            //vnos.Parameters.AddWithValue("@people_worker_id", a);
            MySqlCommand command = new MySqlCommand("INSERT INTO zp (people_worker_id, pos, zp_in_month) VALUES (@people_worker_id, @pos, @zp_in_month)", MySqlConnection);
            command.Parameters.AddWithValue("people_worker_id", textBox10.Text);
            command.Parameters.AddWithValue("zp_in_month", textBox11.Text);
            command.Parameters.AddWithValue("pos", textBox12.Text);
            command.ExecuteNonQuery();
        }

        private void показатьВсеToolStripMenuItem_Click(object sender, EventArgs e)
        {
            listBox1.Items.Clear();
            MySqlDataReader reader = null;
            MySqlCommand command = new MySqlCommand("SELECT * FROM people", MySqlConnection);
        }

        private async void button3_Click(object sender, EventArgs e)
        {
            MySqlCommand delete = new MySqlCommand("DELETE FROM people WHERE (worker_id = @id)", MySqlConnection);
            delete.Parameters.AddWithValue("id", textBox9.Text);
            await delete.ExecuteNonQueryAsync();
        }

        private async void button2_Click(object sender, EventArgs e)
        {
            MySqlCommand update = new MySqlCommand("UPDATE people SET f_name = @name, l_name = @lname, position = @pos WHERE worker_id = @id", MySqlConnection);
            update.Parameters.AddWithValue("id", textBox8.Text);
            update.Parameters.AddWithValue("name", textBox7.Text);
            update.Parameters.AddWithValue("lname", textBox1.Text);
            update.Parameters.AddWithValue("pos", textBox6.Text);
            await update.ExecuteNonQueryAsync();
        }
    }
}