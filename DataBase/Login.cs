using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Npgsql;
using System.IO;

namespace DataBase
{
    public partial class Login : Form
    {
        private NpgsqlConnection npgSqlConnection;
        NpgsqlCommand sqlCommand;
        private string sql = "";
        public Login()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string connectionString = "Server = localhost;" + "Port = 5432;" + "Database = Дидур;" + "User Id = '" + textBox1.Text + "';" + "Password = '" + textBox2.Text + "';";
            try
            {
                if (npgSqlConnection != null && npgSqlConnection.State != ConnectionState.Closed)
                {
                    npgSqlConnection.Close();
                }
                npgSqlConnection = new NpgsqlConnection(connectionString);
                npgSqlConnection.Open();

                sql = "select * from u_login(:_username)";
                sqlCommand = new NpgsqlCommand(sql, npgSqlConnection);
                sqlCommand.Parameters.AddWithValue("_username", textBox1.Text);

                int result = (int)sqlCommand.ExecuteScalar();
                npgSqlConnection.Close();

                if (result == 1)
                {
                    Hide();
                    new Admin(textBox1.Text, textBox2.Text).Show();
                }
                else if (result == 0)
                {
                    Hide();
                    new Comp_employee(textBox1.Text, textBox2.Text).Show();
                }
                else if (result == 2)
                {
                    Hide();
                    new Kind_employee(textBox1.Text, textBox2.Text).Show();
                }
                else
                {
                    MessageBox.Show("Ошибка!!! Проверьте вводимы данные или свяжитесь с администратором");
                    return;
                }

            }
            catch (Exception)
            {
                MessageBox.Show("Ошибка!!!");
                npgSqlConnection.Close();
            }
        }

        private void Login_FormClosing(object sender, FormClosingEventArgs e)
        {
            Application.Exit();
        }
    }
}
