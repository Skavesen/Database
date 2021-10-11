using System;
using System.Collections.Generic;
using System.Data;
using System.Windows.Forms;
using Npgsql;

namespace DataBase
{
    public partial class Kind_employee : Form
    {
        private NpgsqlConnection npgSqlConnection;
        NpgsqlCommand sqlCommand;
        DataTable dt;
        NpgsqlDataReader dataReader;
        //string connectionString = "Server = localhost;" + "Port = 5432;" + "Database = Дидур;" + "User Id = postgres;" + "Password = vadim;";

        private string sql = "";
        String id = "";//id для справочников
        string connectionString = "";
        public Kind_employee(string usersurname, string userpassword)
        {
            InitializeComponent();
            this.usersurname = usersurname;
            this.userpassword = userpassword;
            Text = "Пользователь " + usersurname;
            button2.Visible = false;
            label2.Text = "Обновить данные";
            //label1.Text = "помощь детям";
            connectionString = "Server = localhost;" + "Port = 5432;" + "Database = Дидур;" + "User Id = '" + usersurname + "';" + "Password = '" + userpassword + "';";
        }
        private string usersurname;
        private string userpassword;
        /// <summary>
        /// Метод вычисления количества ячеек в таблице
        /// </summary>
        private int GetCountOfFilledCells(int columnIndex)
        {
            int count = 0;
            for (int i = 0; i < dataGridView1.RowCount; i++)
            {
                if (dataGridView1[columnIndex, i].Value != null &&
                    dataGridView1[columnIndex, i].Value.ToString() != String.Empty)
                    count++;
            }
            return count;
        }
        /// <summary>
        /// Метод загрузки таблицы
        /// </summary>
        public void Load_dataGridView()
        {
            try
            {
                if (npgSqlConnection != null && npgSqlConnection.State != ConnectionState.Closed)
                {
                    npgSqlConnection.Close();
                }
                npgSqlConnection = new NpgsqlConnection(connectionString);
                npgSqlConnection.Open();

                sqlCommand = new NpgsqlCommand(sql, npgSqlConnection);
                dataReader = sqlCommand.ExecuteReader();
                dt = new DataTable();
                dt.Load(dataReader);
                dataGridView1.DataSource = dt;
                dataGridView1.AutoResizeColumns();

                npgSqlConnection.Close();
                label5.Text = "Всего заполненно " + GetCountOfFilledCells(0).ToString() + " строк";
                dataGridView1.ReadOnly = true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                npgSqlConnection.Close();
            }
        }

        private void детиToolStripMenuItem_Click(object sender, EventArgs e)
        {
            label1.Text = "ребёнок";
            command_sql_select();
            Load_dataGridView();
            button2.Text = "Добавить ребёнка";
            button2.Visible = true;
        }
        /// <summary>
        /// Команды для загрузки таблицы из бд
        /// </summary>
        public void command_sql_select()
        {
            if (label1.Text == "ребёнок")
            {
                sql = "SELECT  id_child as \"ID\",  surname as \"Фамилия\",  \"name\" as \"Имя\",  patronymic as \"Отчество\",  birthday as \"День рождения\",  \"group\" as \"Группа\"  FROM  \"Child\"  INNER JOIN \"Group\" ON \"Child\".id_group = \"Group\".id_group  INNER JOIN \"Kindergarten\" ON \"Child\".id_kindergarten = \"Kindergarten\".id_kindergarten   ";
            }
        }
        /// <summary>
        /// Команды для удаления из БД по ID
        /// </summary>
        private void command_sql_delete()
        {
            if (label1.Text == "ребёнок")
            {
                sql = "DELETE FROM \"Child\" WHERE id_child in (" + delete + ");";
            }
        }
        /// <summary>
        /// Команды для поиска в БД (сделать, чтобы искало по отделениям(чужие записи не показывать))
        /// </summary>
        string value_for_search = "";
        private void command_sql_search()
        {
            value_for_search = textBox3.Text;
            if (label1.Text == "ребёнок")
            {
                sql = "SELECT  id_child as \"ID\",  surname as \"Фамилия\",  \"name\" as \"Имя\",  patronymic as \"Отчество\",  birthday as \"День рождения\",  \"group\" as \"Группа\"  FROM  \"Child\"  INNER JOIN \"Group\" ON \"Child\".id_group = \"Group\".id_group  INNER JOIN \"Kindergarten\" ON \"Child\".id_kindergarten = \"Kindergarten\".id_kindergarten WHERE group = '" + value_for_search + "' or name = '" + value_for_search + "' or surname = '" + value_for_search + "' or patronymic = '" + value_for_search + "' ";
            }
        }
        /// <summary>
        /// Метод удаления записи по ID
        /// </summary>
        List<int> Selected = new List<int>();
        string temp = ""; string delete = "";
        private void delete_dataGridView()
        {
            foreach (DataGridViewCell cell in dataGridView1.SelectedCells)
            {
                if (cell.ColumnIndex == 0)
                {
                    Selected.Add((int)cell.Value);
                }
            }
            foreach (int temp1 in Selected)
            {
                temp += temp1.ToString() + ",";
            }
            char[] MyChar = { ',' };
            delete = temp.TrimEnd(MyChar);

            command_sql_delete();
            try
            {
                npgSqlConnection = new NpgsqlConnection(connectionString);
                npgSqlConnection.Open();
                sqlCommand = new NpgsqlCommand(sql, npgSqlConnection);
                sqlCommand.ExecuteNonQuery();
                npgSqlConnection.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                npgSqlConnection.Close();
            }
            delete = "";
            command_sql_select();
            Load_dataGridView();
        }
        /// <summary>
        /// Кнопка для поиска
        /// </summary>
        private void button4_Click(object sender, EventArgs e)
        {
            command_sql_search();
            Load_dataGridView();
            if (label5.Text == "Всего заполненно 0 строк")
            {
                MessageBox.Show("Поиск не дал результата");
                command_sql_select();
                Load_dataGridView();
            }
            textBox3.Clear();
        }
        /// <summary>
        /// Метод для добавления в таблицу(открытие новой формы, а там уже вся красота)
        /// </summary>
        private void dataGridView1_DoubleClick(object sender, EventArgs e)
        {
            string Name = label1.Text;
            int selectedrowindex = dataGridView1.SelectedCells[0].RowIndex;
            DataGridViewRow selectedRow = dataGridView1.Rows[selectedrowindex];
            string id = Convert.ToString(selectedRow.Cells[0].Value);
            Add_information add_information = new Add_information(Name, id, usersurname, userpassword);

            if (Name == "ребёнок")
            {
                add_information.ID.Text = dataGridView1.CurrentRow.Cells[0].Value.ToString();
                add_information.TextBox1.Text = dataGridView1.CurrentRow.Cells[1].Value.ToString();
                add_information.TextBox2.Text = dataGridView1.CurrentRow.Cells[2].Value.ToString();
                add_information.TextBox3.Text = dataGridView1.CurrentRow.Cells[3].Value.ToString();
                add_information.DateTimePicker1.Text = dataGridView1.CurrentRow.Cells[4].Value.ToString();
                add_information.ComboBox1.Text = dataGridView1.CurrentRow.Cells[5].Value.ToString();
            }
            if (dataGridView1.CurrentRow.Cells[0].Value.ToString() != "")
            {
                add_information.button1.Text = "Изменить";
            }
            add_information.ShowDialog();
        }
        /// <summary>
        /// Метод обновления базы данных
        /// </summary>
        private void label1_Click(object sender, EventArgs e)
        {
            command_sql_select();
            Load_dataGridView();
        }
        /// <summary>
        /// Удаление через кнопку Delete
        /// </summary>
        private void dataGridView1_UserDeletingRow(object sender, DataGridViewRowCancelEventArgs e)
        {
            if (MessageBox.Show("Вы действительно хотите удалить?", "Удаление", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.Cancel)
            {
                e.Cancel = true;
                command_sql_select();
                Load_dataGridView();
            }
            else
            {
                int selectedrowindex = dataGridView1.SelectedCells[0].RowIndex;
                DataGridViewRow selectedRow = dataGridView1.Rows[selectedrowindex];
                id = Convert.ToString(selectedRow.Cells[0].Value);
                delete_dataGridView();
            }
            command_sql_select();
            Load_dataGridView();
        }
        /// <summary>
        /// Кнопка "Добавить запись в таблицу"(открытие новой формы)
        /// </summary>
        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                string id;
                int selectedrowindex = dataGridView1.SelectedCells[0].RowIndex;
                DataGridViewRow selectedRow = dataGridView1.Rows[selectedrowindex];
                id = Convert.ToString(selectedRow.Cells[0].Value);

                string Name = label1.Text;
                Add_information add_information = new Add_information(Name, id, usersurname, userpassword);
                add_information.ShowDialog();
            }
            catch (Exception) { MessageBox.Show("Вы не выбрали таблицу"); }
        }
       
        private void DB_FormClosing(object sender, FormClosingEventArgs e)
        {
            Hide();
            new Login().Show();
        }

        private void label2_Click(object sender, EventArgs e)
        {
            command_sql_select();
            Load_dataGridView();
        }

        private void статистикаДетейToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Graphics graphics = new Graphics(usersurname, userpassword);
            graphics.ShowDialog();
        }
    }
}