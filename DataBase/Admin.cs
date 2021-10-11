using System;
using System.Collections.Generic;
using System.Data;
using Npgsql;
using System.Windows.Forms;

namespace DataBase
{
    public partial class Admin : Form
    {

        //string connectionString = "Server = localhost;" + "Port = 5432;" + "Database = Дидур;" + "User Id = postgres;" + "Password = vadim;";
        string connectionString = "";
        private string usersurname;
        private string userpassword;
        private NpgsqlConnection npgSqlConnection;
        NpgsqlCommand sqlCommand;
        DataTable dt;
        NpgsqlDataReader dataReader;
        private string sql = "";
        private string deletesql1 = "";
        String id = "";
        public Admin(string usersurname, string userpassword)
        {
            InitializeComponent();
            this.usersurname = usersurname;
            this.userpassword = userpassword;
            button2.Visible = false;
            connectionString = "Server = localhost;" + "Port = 5432;" + "Database = Дидур;" + "User Id = '" + usersurname + "';" + "Password = '" + userpassword + "';";
            label2.Text = "Обновить данные";
        }

        /// <summary>
        /// Команды для загрузки таблицы из бд
        /// </summary>
        public void command_sql_select()
        {
            if (label1.Text == "район")
            {
                sql = "SELECT \"District\".id_district AS \"Id\",  \"District\".district AS \"Район\"  FROM \"District\"";
            }
            else if (label1.Text == "форма собственности")
            {
                sql = "SELECT \"Type of ownership\".id_type AS \"Id\",  \"Type of ownership\".type AS \"Тип собсвенности\"  FROM \"Type of ownership\"";
            }
            else if(label1.Text == "детский сад")
            {
                sql = "SELECT  public.\"Kindergarten\".id_kindergarten as \"ID\",  public.\"Kindergarten\".caption as \"Название\",  public.\"District\".district as \"Район\",  public.\"Type of property\".\"type\" as \"Тип собственности\",  public.\"Kindergarten\".year_of_open as \"Год открытия\",  public.\"Kindergarten\".phone as \"Телефон\"  FROM  public.\"Kindergarten\"  INNER JOIN public.\"District\" ON public.\"Kindergarten\".id_district = public.\"District\".id_district  INNER JOIN public.\"Type of property\" ON public.\"Kindergarten\".id_type = public.\"Type of property\".id_type  ";
            }
            else if (label1.Text == "предприятие")
            {
                sql = "SELECT  public.\"Company\".id_company as \"ID\",  public.\"Company\".\"name\" as \"Название\",  public.\"Type of ownership\".\"type\" as \"Форма собственности\",  public.\"Company\".phone as \"Телефон\",  public.\"Company\".\"year\" as \"Год открытия\",  public.\"Company\".employees as \"Кол-во сотрудников\"  FROM  public.\"Company\"  INNER JOIN public.\"Type of ownership\" ON public.\"Company\".id_type = public.\"Type of ownership\".id_type  ";
            }
            else if (label1.Text == "сотрудник предприятия")
            {
                sql = "SELECT  public.\"Employee of the company\".\"login\" as \"Логин\",  public.\"Company\".\"name\" as \"Предприятие\"  FROM  public.\"Employee of the company\"  INNER JOIN public.\"Company\" ON public.\"Employee of the company\".id_company = public.\"Company\".id_company  ";
            }
            else if (label1.Text == "сотрудник сада")
            {
                sql = "SELECT  public.\"Employee of the kindergarten\".\"login\" as \"Логин\",  public.\"Kindergarten\".caption as \"Детсад\"  FROM  public.\"Employee of the kindergarten\"  INNER JOIN public.\"Kindergarten\" ON public.\"Employee of the kindergarten\".id_kindergarten = public.\"Kindergarten\".id_kindergarten  ";
            }
            else if (label1.Text == "тип собственности")
            {
                sql = "SELECT  public.\"Type of property\".id_type as \"ID\",  public.\"Type of property\".\"type\" as \"Тип собственности\"  FROM  public.\"Type of property\"  ";
            }
            else if (label1.Text == "вид помощи садам")
            {
                sql = "SELECT  public.\"Type of garden assistance\".id_type as \"ID\",  public.\"Type of garden assistance\".\"type\" as \"Вид помощи садам\"  FROM  public.\"Type of garden assistance\"  ";
            }
            else if (label1.Text == "группа")
            {
                sql = "SELECT  public.\"Group\".id_group as \"ID\",  public.\"Group\".\"group\" as \"Группа\"  FROM  public.\"Group\"  ";
            }
            else if (label1.Text == "вид помощи детям")
            {
                sql = "SELECT  public.\"Type of children assistance\".id_type as \"ID\",  public.\"Type of children assistance\".\"type\" as \"Вид помощи детям\"  FROM  public.\"Type of children assistance\"  ";
            }
        }

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

        private void районToolStripMenuItem_Click(object sender, EventArgs e)
        {
            label1.Text = "район";
            command_sql_select();
            Load_dataGridView();
            button4.Enabled = false;
            button2.Text = "Добавить район";
            button2.Visible = true;
        }
        
        private void формаСобственностиToolStripMenuItem_Click(object sender, EventArgs e)
        {
            label1.Text = "форма собственности";
            command_sql_select();
            Load_dataGridView();
            button4.Enabled = false;
            button2.Text = "Добавить форму собственности";
            button2.Visible = true;
        }
        
        private void детскийСадToolStripMenuItem_Click(object sender, EventArgs e)
        {
            label1.Text = "детский сад";
            command_sql_select();
            Load_dataGridView();
            button4.Enabled = true;
            button2.Text = "Добавить детский сад";
            button2.Visible = true;
        }

        private void предприятиеToolStripMenuItem_Click(object sender, EventArgs e)
        {
            label1.Text = "предприятие";
            command_sql_select();
            Load_dataGridView();
            button4.Enabled = true;
            button2.Text = "Добавить предприятие";
            button2.Visible = true;
        }

        private void сотрудникиПредприятияToolStripMenuItem_Click(object sender, EventArgs e)
        {
            label1.Text = "сотрудник предприятия";
            command_sql_select();
            Load_dataGridView();
            button4.Enabled = true;
            button2.Text = "Добавить сотрудника предприятия";
            button2.Visible = true;
        }

        private void сотрудникиДетсогоСадаToolStripMenuItem_Click(object sender, EventArgs e)
        {
            label1.Text = "сотрудник сада";
            command_sql_select();
            Load_dataGridView();
            button4.Enabled = true;
            button2.Text = "Добавить сотрудника сада";
            button2.Visible = true;
        }

        private void типСобственностиToolStripMenuItem_Click(object sender, EventArgs e)
        {
            label1.Text = "тип собственности";
            command_sql_select();
            Load_dataGridView();
            button4.Enabled = false;
            button2.Text = "Добавить тип собственности";
            button2.Visible = true;
        }

        private void видПомощиСадамToolStripMenuItem_Click(object sender, EventArgs e)
        {
            label1.Text = "вид помощи садам";
            command_sql_select();
            Load_dataGridView();
            button4.Enabled = false;
            button2.Text = "Добавить вид помощи";
            button2.Visible = true;
        }

        private void группаToolStripMenuItem_Click(object sender, EventArgs e)
        {
            label1.Text = "группа";
            command_sql_select();
            Load_dataGridView();
            button4.Enabled = false;
            button2.Text = "Добавить группу";
            button2.Visible = true;
        }

        private void видПомощиДетямToolStripMenuItem_Click(object sender, EventArgs e)
        {
            label1.Text = "вид помощи детям";
            command_sql_select();
            Load_dataGridView();
            button4.Enabled = false;
            button2.Text = "Добавить вид помощи";
            button2.Visible = true;
        }

        private void Admin_FormClosing(object sender, FormClosingEventArgs e)
        {
            Hide();
            new Login().Show();
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
                Add_information add_information = new Add_information(Name, id);
                add_information.ShowDialog();
            }
            catch (Exception) { MessageBox.Show("Вы не выбрали таблицу"); }
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
                sqlCommand = new NpgsqlCommand(deletesql1, npgSqlConnection);
                sqlCommand.ExecuteNonQuery();
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
        /// Команды для удаления из БД по ID
        /// </summary>
        private void command_sql_delete()
        {
            if (label1.Text == "район")
            {
                sql = "DELETE FROM \"District\" WHERE id_district in (" + delete + ");";
                deletesql1 = "";
            }
            else if (label1.Text == "тип собственности")
            {
                sql = "DELETE FROM \"Type of property\" WHERE id_type in (" + delete + ");";
                deletesql1 = "";
            }
            else if (label1.Text == "вид помощи садам")
            {
                sql = "DELETE FROM \"Type of garden assistance\" WHERE id_type in (" + delete + ");";
                deletesql1 = "";
            }
            else if (label1.Text == "детский сад")
            {
                sql = "DELETE FROM \"Kindergarten\" WHERE id_kindergarten in (" + delete + ");";
                deletesql1 = "";
            }
            else if (label1.Text == "форма собственности")
            {
                sql = "DELETE FROM \"Type of ownership\" WHERE id_type in (" + delete + ");";
                deletesql1 = "";
            }
            else if (label1.Text == "предприятие")
            {
                sql = "DELETE FROM \"Company\" WHERE id_company in (" + delete + ");";
                deletesql1 = "";
            }
            else if (label1.Text == "группа")
            {
                sql = "DELETE FROM \"Group\" WHERE id_group in (" + delete + ");";
                deletesql1 = "";
            }
            else if (label1.Text == "вид помощи детям")
            {
                sql = "DELETE FROM \"Type of children assistance\" WHERE id_type in (" + delete + ");";
                deletesql1 = "";
            }
            else if (label1.Text == "сотрудник сада")
            {
                sql = "DELETE FROM \"Employee of the kindergarten\" WHERE id_employee in ('" + delete + "');";
                deletesql1 = "DROP OWNED BY \"'" + dataGridView1.CurrentRow.Cells[0].Value.ToString() + "'\";  DROP ROLE \"'" + dataGridView1.CurrentRow.Cells[0].Value.ToString() + "'\";";
            }
            else if (label1.Text == "сотрудник предприятия")
            {
                sql = "DELETE FROM \"Employee of the company\" WHERE login in ('" + delete + "');";
                deletesql1 = "DROP OWNED BY \"'" + dataGridView1.CurrentRow.Cells[0].Value.ToString() + "'\";  DROP ROLE \"'" + dataGridView1.CurrentRow.Cells[0].Value.ToString() + "'\";";
            }
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
            Add_information add_information = new Add_information(Name, id);
            
            if (Name == "район")
            {
                add_information.ID.Text = dataGridView1.CurrentRow.Cells[0].Value.ToString();
                add_information.TextBox1.Text = dataGridView1.CurrentRow.Cells[1].Value.ToString();
            }
            else if (Name == "тип собственности")
            {
                add_information.ID.Text = dataGridView1.CurrentRow.Cells[0].Value.ToString();
                add_information.TextBox1.Text = dataGridView1.CurrentRow.Cells[1].Value.ToString();
            }
            else if (Name == "вид помощи садам")
            {
                add_information.ID.Text = dataGridView1.CurrentRow.Cells[0].Value.ToString();
                add_information.TextBox1.Text = dataGridView1.CurrentRow.Cells[1].Value.ToString();
            }
            else if (Name == "детский сад")
            {
                add_information.ID.Text = dataGridView1.CurrentRow.Cells[0].Value.ToString();
                add_information.TextBox1.Text = dataGridView1.CurrentRow.Cells[1].Value.ToString();
                add_information.ComboBox1.Text = dataGridView1.CurrentRow.Cells[2].Value.ToString();
                add_information.ComboBox2.Text = dataGridView1.CurrentRow.Cells[3].Value.ToString();
                add_information.YearBox.Text = dataGridView1.CurrentRow.Cells[4].Value.ToString();
                add_information.TextBox2.Text = dataGridView1.CurrentRow.Cells[5].Value.ToString();
            }
            else if (Name == "форма собственности")
            {
                add_information.ID.Text = dataGridView1.CurrentRow.Cells[0].Value.ToString();
                add_information.TextBox1.Text = dataGridView1.CurrentRow.Cells[1].Value.ToString();
            }
            else if (Name == "предприятие")
            {
                add_information.ID.Text = dataGridView1.CurrentRow.Cells[0].Value.ToString();
                add_information.TextBox1.Text = dataGridView1.CurrentRow.Cells[1].Value.ToString();
                add_information.ComboBox1.Text = dataGridView1.CurrentRow.Cells[2].Value.ToString();
                add_information.TextBox2.Text = dataGridView1.CurrentRow.Cells[3].Value.ToString();
                add_information.YearBox.Text = dataGridView1.CurrentRow.Cells[4].Value.ToString();
                add_information.TextBox3.Text = dataGridView1.CurrentRow.Cells[5].Value.ToString();
            }
            else if (Name == "группа")
            {
                add_information.ID.Text = dataGridView1.CurrentRow.Cells[0].Value.ToString();
                add_information.TextBox1.Text = dataGridView1.CurrentRow.Cells[1].Value.ToString();
            }
            else if (Name == "вид помощи детям")
            {
                add_information.ID.Text = dataGridView1.CurrentRow.Cells[0].Value.ToString();
                add_information.TextBox1.Text = dataGridView1.CurrentRow.Cells[1].Value.ToString();
            }
            else if (Name == "сотрудник сада")
            {
                add_information.TextBox1.Text = dataGridView1.CurrentRow.Cells[0].Value.ToString();
                add_information.ComboBox1.Text = dataGridView1.CurrentRow.Cells[1].Value.ToString();
            }
            else if (Name == "сотрудник предприятия")
            {
                add_information.TextBox1.Text = dataGridView1.CurrentRow.Cells[0].Value.ToString();
                add_information.ComboBox1.Text = dataGridView1.CurrentRow.Cells[1].Value.ToString();
            }
            if (dataGridView1.CurrentRow.Cells[0].Value.ToString() != "")
            {
                add_information.button1.Text = "Изменить";
            }
            add_information.ShowDialog();
        }

        private void label1_Click(object sender, EventArgs e)
        {
            command_sql_select();
            Load_dataGridView();
        }

        /// <summary>
        /// Кнопка для поиска
        /// </summary>
        private void button4_Click(object sender, EventArgs e)
        {
            string value_for_search = textBox3.Text;
            if (label1.Text == "детский сад")
            {
                sql = "SELECT  public.\"Kindergarten\".id_kindergarten as \"ID\",  public.\"Kindergarten\".caption as \"Название\",  public.\"District\".district as \"Район\",  public.\"Type of property\".\"type\" as \"Тип собственности\",  public.\"Kindergarten\".year_of_open as \"Год открытия\",  public.\"Kindergarten\".phone as \"Телефон\"  FROM  public.\"Kindergarten\"  INNER JOIN public.\"District\" ON public.\"Kindergarten\".id_district = public.\"District\".id_district  INNER JOIN public.\"Type of property\" ON public.\"Kindergarten\".id_type = public.\"Type of property\".id_type  WHERE \"type\" = '" + value_for_search + "' or district = '" + value_for_search + "' or caption = '" + value_for_search + "' or phone = '" + value_for_search + "' ";
            }
            else if (label1.Text == "предприятие")
            {
                sql = "SELECT  public.\"Company\".id_company as \"ID\",  public.\"Company\".\"name\" as \"Название\",  public.\"Type of ownership\".\"type\" as \"Форма собственности\",  public.\"Company\".phone as \"Телефон\",  public.\"Company\".\"year\" as \"Год открытия\",  public.\"Company\".employees as \"Кол-во сотрудников\"  FROM  public.\"Company\"  INNER JOIN public.\"Type of ownership\" ON public.\"Company\".id_type = public.\"Type of ownership\".id_type  WHERE \"type\" = '" + value_for_search + "' or \"Company\".\"name\" = '" + value_for_search + "' or phone = '" + value_for_search + "' ";
            }
            else if (label1.Text == "сотрудник предприятия")
            {
                sql = "SELECT  public.\"Employee of the company\".\"login\" as \"Логин\",  public.\"Company\".\"name\" as \"Предприятие\"  FROM  public.\"Employee of the company\"  INNER JOIN public.\"Company\" ON public.\"Employee of the company\".id_company = public.\"Company\".id_company WHERE \"Company\".\"name\" = '" + value_for_search + "' ";
            }
            else if (label1.Text == "сотрудник сада")
            {
                sql = "SELECT  public.\"Employee of the kindergarten\".\"login\" as \"Логин\",  public.\"Kindergarten\".caption as \"Детсад\"  FROM  public.\"Employee of the kindergarten\"  INNER JOIN public.\"Kindergarten\" ON public.\"Employee of the kindergarten\".id_kindergarten = public.\"Kindergarten\".id_kindergarten WHERE \"Kindergarten\".caption = '" + value_for_search + "' ";
            }
            Load_dataGridView();
            if (label5.Text == "Всего заполненно 0 строк")
            {
                MessageBox.Show("Поиск не дал результата");
                command_sql_select();
                Load_dataGridView();
            }
            textBox3.Clear();
        }
        string Query = "";//СДЕЛАТЬ ЗАПРОСЫ ВСЕ

        private void вывестиДетейВУказаннойГруппеToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Query = "1";
            Querys querys = new Querys(Query, usersurname, userpassword);
            querys.ShowDialog();
        }

        private void вывестиСадыВУказанномРайонеToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Query = "2";
            Querys querys = new Querys(Query, usersurname, userpassword);
            querys.ShowDialog();
        }

        private void садыОткрытыеВУказанномГодуToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Query = "3";
            Querys querys = new Querys(Query, usersurname, userpassword);
            querys.ShowDialog();
        }

        private void компанииОткрытыеВУказанномГодуToolStripMenuItem_Click_1(object sender, EventArgs e)
        {
            Query = "4";
            Querys querys = new Querys(Query, usersurname, userpassword);
            querys.ShowDialog();
        }

        private void краткаяИнформацияОПомощиДетямToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Query = "5";
            Querys querys = new Querys(Query, usersurname, userpassword);
            querys.ShowDialog();
        }

        private void краткаяИнформацияОДетяхToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Query = "6";
            Querys querys = new Querys(Query, usersurname, userpassword);
            querys.ShowDialog();
        }

        private void краткаяИнформацияОПредприятияхToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Query = "7";
            Querys querys = new Querys(Query, usersurname, userpassword);
            querys.ShowDialog();
        }

        private void районыВКоторыхНетДетсадовToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Query = "8";
            Querys querys = new Querys(Query, usersurname, userpassword);
            querys.ShowDialog();
        }

        private void всеДетсадыИРайоныToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Query = "9";
            Querys querys = new Querys(Query, usersurname, userpassword);
            querys.ShowDialog();
        }

        private void информацияОПомощиДетямКромеУказанногоГодаToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Query = "10";
            Querys querys = new Querys(Query, usersurname, userpassword);
            querys.ShowDialog();
        }

        private void колвоДетейПоГруппамToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Query = "11";
            Querys querys = new Querys(Query, usersurname, userpassword);
            querys.ShowDialog();
        }

        private void всегоСуммаВыплатСоставилаВТомЧислеСредняяСуммаВыплатToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Query = "12";
            Querys querys = new Querys(Query, usersurname, userpassword);
            querys.ShowDialog();
        }

        private void вывестиКолвоДетейПоСадамРодившихсяВУказанномГодуToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Query = "13";
            Querys querys = new Querys(Query, usersurname, userpassword);
            querys.ShowDialog();
        }

        private void колвоДетейВИмениКоторыхЕстьДанныеБуквыToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Query = "14";
            Querys querys = new Querys(Query, usersurname, userpassword);
            querys.ShowDialog();
        }

        private void колвоЗаявокНаПомощьСадамСУказаннымТипомПомощиToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Query = "15";
            Querys querys = new Querys(Query, usersurname, userpassword);
            querys.ShowDialog();
        }

        private void колвоИТипЗаявокГдеСуммаВыплатБольшеВведённогоЧислаToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Query = "16";
            Querys querys = new Querys(Query, usersurname, userpassword);
            querys.ShowDialog();
        }

        private void количествоСадовСУказаннымТипомСобственностиToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Query = "17";
            Querys querys = new Querys(Query, usersurname, userpassword);
            querys.ShowDialog();
        }
        private void количествоСадовСУказаннымТипомСобственностиОткрывшиесяВУказанномГодуToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Query = "18";
            Querys querys = new Querys(Query, usersurname, userpassword);
            querys.ShowDialog();
        }

        private void вывестиСреднююСтоимостьВыплатПоТипамПомощиСадамToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Query = "19";
            Querys querys = new Querys(Query, usersurname, userpassword);
            querys.ShowDialog();
        }

        private void вывод4ёхТаблицТипСобственностиВидПомощиДетсадамВидПомощиДетямФормаСобственностиToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Query = "20";
            Querys querys = new Querys(Query, usersurname, userpassword);
            querys.ShowDialog();
        }

        private void детиУКоторыхУказаннаяГруппа2ШтукиToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Query = "21";
            Querys querys = new Querys(Query, usersurname, userpassword);
            querys.ShowDialog();
        }

        private void детиКоторыеНеВходятВУказанныеГруппы2ШтукиToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Query = "22";
            Querys querys = new Querys(Query, usersurname, userpassword);
            querys.ShowDialog();
        }

        private void вывестиДетейИИхГруппыToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Query = "23";
            Querys querys = new Querys(Query, usersurname, userpassword);
            querys.ShowDialog();
        }

        private void label2_Click(object sender, EventArgs e)
        {
            command_sql_select();
            Load_dataGridView();
        }
    }
}
