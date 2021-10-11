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
    public partial class Add_information : Form
    {
        private NpgsqlConnection npgSqlConnection;
        NpgsqlCommand sqlCommand;
        DataTable dt;
        NpgsqlDataReader dataReader;
        string connectionString = "";
        private string sql = "";
        private string addsql1 = "";
        string Name1 = "";
        string id = ""; string first = ""; string second = ""; string third = ""; string fourth = ""; string fifth = ""; string sixth = ""; string seventh = ""; string eighth = ""; string ninth = ""; string ten = "";
        //сделать его айдишником, чтобы не высвечивалось
        public TextBox ID = new TextBox(); public TextBox TextBox1 = new TextBox(); public TextBox TextBox2 = new TextBox(); public TextBox TextBox3 = new TextBox(); public TextBox TextBox4 = new TextBox(); public TextBox TextBox5 = new TextBox(); public TextBox TextBox6 = new TextBox();

        Label Label1 = new Label(); Label Label2 = new Label(); Label Label3 = new Label(); Label Label4 = new Label(); Label Label5 = new Label(); Label Label6 = new Label(); Label Label7 = new Label(); Label Label8 = new Label(); Label Label9 = new Label(); Label Label10 = new Label(); Label Label11 = new Label();

        public ComboBox ComboBox1 = new ComboBox(); public ComboBox ComboBox2 = new ComboBox(); public ComboBox ComboBox3 = new ComboBox(); public ComboBox ComboBox4 = new ComboBox();

        public DateTimePicker DateTimePicker1 = new DateTimePicker(); public DateTimePicker DateTimePicker2 = new DateTimePicker();

        Button Button2 = new Button();

        public PictureBox PictureBox1 = new PictureBox();

        CheckBox CheckBox1 = new CheckBox(); CheckBox CheckBox2 = new CheckBox();

        public NumericUpDown YearBox = new NumericUpDown();

        string ideshka = "";
        /// <summary>
        /// Это загрузка всего
        /// </summary>
        public Add_information(string Name, string id, string usersurname, string userpassword)
        {
            InitializeComponent();
            Name1 = Name;
            ideshka = id;
            this.usersurname = usersurname;
            this.userpassword = userpassword;
            connectionString = "Server = localhost;" + "Port = 5432;" + "Database = Дидур;" + "User Id = '" + usersurname + "';" + "Password = '" + userpassword + "';";
            //connectionString = "Server = localhost;" + "Port = 5432;" + "Database = Дидур;" + "User Id = Сотрудник_сада_1;" + "Password = 12345;";
            //connectionString = "Server = localhost;" + "Port = 5432;" + "Database = Дидур;" + "User Id = Сотрудник_предприятия_1;" + "Password = 12345;";
            load_dataGridView();
        }
        public Add_information(string Name, string id)
        {
            InitializeComponent();
            Name1 = Name;
            ideshka = id;
            connectionString = "Server = localhost;" + "Port = 5432;" + "Database = Дидур;" + "User Id = postgres;" + "Password = postgres;";
            load_dataGridView();
        }
        private string usersurname;
        private string userpassword;
        /// <summary>
        /// комманды и переменные для вставки в таблицу
        /// </summary>
        private void command_sql_insert()
        {
            if (Name1 == "район")
            {
                if (TextBox1.Text == "")
                {
                    sql = "INSERT INTO \"Company name\"(company_name) VALUES(" + first + ")";
                }
                else
                {
                    first = TextBox1.Text;
                    sql = "INSERT INTO \"District\"(district) VALUES('" + first + "');";
                }
            }
            else if (Name1 == "тип собственности")
            {
                if (TextBox1.Text == "")
                {
                    sql = "INSERT INTO \"Company name\"(company_name) VALUES(" + first + ")";
                }
                else
                {
                    first = TextBox1.Text;
                    sql = "INSERT INTO \"Type of property\"(type) VALUES('" + first + "');";
                }
            }
            else if (Name1 == "вид помощи садам")
            {
                if (TextBox1.Text == "")
                {
                    sql = "INSERT INTO \"Company name\"(company_name) VALUES(" + first + ")";
                }
                else
                {
                    first = TextBox1.Text;
                    sql = "INSERT INTO \"Type of garden assistance\"(type) VALUES('" + first + "');";
                }
            }
            else if (Name1 == "детский сад")
            {
                if (TextBox1.Text == "" || ComboBox1.Text == "" || ComboBox2.Text == "" || TextBox2.Text == "" || YearBox.Text == "")
                {
                    sql = "INSERT INTO \"Company name\"(company_name) VALUES(" + first + ")";
                }
                else
                {
                    first = TextBox1.Text;
                    second = ComboBox1.SelectedValue.ToString();
                    third = ComboBox2.SelectedValue.ToString();
                    fourth = YearBox.Text;
                    fifth = TextBox2.Text;
                    sql = "INSERT INTO \"Kindergarten\"(caption, id_district, id_type, year_of_open, phone) VALUES('" + first + "', " + second + ", " + third + ", " + fourth + ", '" + fifth + "');";
                }
            }
            else if (Name1 == "форма собственности")
            {
                if (TextBox1.Text == "")
                {
                    sql = "INSERT INTO \"Company name\"(company_name) VALUES(" + first + ")";
                }
                else
                {
                    first = TextBox1.Text;
                    sql = "INSERT INTO \"Type of ownership\"(type) VALUES('" + first + "');";
                }
            }
            else if (Name1 == "предприятие")
            {
                if (TextBox1.Text == "" || ComboBox1.Text == "" || TextBox3.Text == "" || TextBox2.Text == "" || YearBox.Text == "")
                {
                    sql = "INSERT INTO \"Company name\"(company_name) VALUES(" + first + ")";
                }
                else
                {
                    first = TextBox1.Text;
                    second = ComboBox1.SelectedValue.ToString();
                    third = TextBox2.Text; 
                    fourth = YearBox.Text;
                    fifth = TextBox3.Text;
                    sql = "INSERT INTO \"Company\"(name, id_type, phone, year, employees) VALUES('" + first + "', " + second + ", '" + third + "', " + fourth + ", " + fifth + ");";
                }
            }
            else if (Name1 == "группа")
            {
                if (TextBox1.Text == "")
                {
                    sql = "INSERT INTO \"Company name\"(company_name) VALUES(" + first + ")";
                }
                else
                {
                    first = TextBox1.Text;
                    sql = "INSERT INTO \"Group\"(group) VALUES('" + first + "');";
                }
            }
            else if (Name1 == "вид помощи детям")
            {
                if (TextBox1.Text == "")
                {
                    sql = "INSERT INTO \"Company name\"(company_name) VALUES(" + first + ")";
                }
                else
                {
                    first = TextBox1.Text;
                    sql = "INSERT INTO \"Type of children assistance\"(type) VALUES('" + first + "');";
                }
            }
            else if (Name1 == "сотрудник сада")
            {
                if (TextBox1.Text == "" || ComboBox1.Text == "" || ComboBox2.Text == "" || TextBox2.Text == "")
                {
                    sql = "INSERT INTO \"Company name\"(company_name) VALUES(" + first + ")";
                }
                else
                {
                    first = TextBox1.Text;//логин
                    second = ComboBox1.SelectedValue.ToString();//предприятие-сад
                    third = ComboBox2.Text;//роль(addmin, cemployee, kemployee)
                    fourth = TextBox2.Text;//пароль
                    if (third == "админ")
                    {
                        addsql1 = "CREATE ROLE \"" + first + "\" login PASSWORD '" + fourth + "'; GRANT \"addmin\" TO \"" + first + "\" WITH ADMIN OPTION;";
                    }
                    else if (third == "сотрудник")
                    {
                        sql = "INSERT INTO \"Employee of the kindergarten\"(login, id_kindergarten) VALUES('" + first + "', " + second + ");";
                        addsql1 = "CREATE ROLE \"" + first + "\" login PASSWORD '" + fourth + "'; GRANT \"kemployee\" TO \"" + first + "\" WITH ADMIN OPTION;";
                    }
                }
            }
            else if (Name1 == "сотрудник предприятия")
            {
                if (TextBox1.Text == "" || ComboBox1.Text == "" || ComboBox2.Text == "" || TextBox2.Text == "")
                {
                    sql = "INSERT INTO \"Company name\"(company_name) VALUES(" + first + ")";
                }
                else
                {
                    first = TextBox1.Text;//логин
                    second = ComboBox1.SelectedValue.ToString();//предприятие-сад
                    third = ComboBox2.Text;//роль(addmin, cemployee, kemployee)
                    fourth = TextBox2.Text;//пароль
                    if (third == "админ")
                    {
                        addsql1 = "CREATE ROLE \"" + first + "\" login PASSWORD '" + fourth + "'; GRANT \"addmin\" TO \"" + first + "\" WITH ADMIN OPTION;";
                    }
                    else if (third == "сотрудник")
                    {
                        sql = "INSERT INTO \"Employee of the company\"(login, id_company) VALUES('" + first + "', " + second + ");";
                        addsql1 = "CREATE ROLE \"" + first + "\" login PASSWORD '" + fourth + "'; GRANT \"cemployee\" TO \"" + first + "\" WITH ADMIN OPTION;";
                    }
                }
            }
            else if (Name1 == "ребёнок")
            {
                if (TextBox1.Text == "" || ComboBox1.Text == "" || ComboBox2.Text == "" || TextBox3.Text == "" || TextBox2.Text == "" || DateTimePicker1.Value.ToShortDateString() == "")
                {
                    sql = "INSERT INTO \"Company name\"(company_name) VALUES(" + first + ")";
                }
                else
                {
                    first = TextBox1.Text;
                    second = TextBox2.Text;
                    third = TextBox3.Text;
                    fourth = DateTimePicker1.Value.ToShortDateString();
                    fifth = ComboBox1.SelectedValue.ToString();
                    sixth = ComboBox2.SelectedValue.ToString();
                    sql = "INSERT INTO\"Child\" (surname, name, patronymic, birthday, id_group, id_kindergarten) VALUES('" + first + "', '" + second + "', '" + third + "', '" + fourth + "', " + fifth + ", " + sixth + ");";                   
                }
            }
            else if (Name1 == "помощь детским садам")
            {
                if (TextBox1.Text == "" || ComboBox1.Text == "" || ComboBox2.Text == "" )
                {
                    sql = "INSERT INTO \"Company name\"(company_name) VALUES(" + first + ")";
                }
                else
                {
                    first = ComboBox1.SelectedValue.ToString();
                    second = ComboBox2.SelectedValue.ToString();
                    third = TextBox1.Text;
                    fourth = DateTime.Now.ToString("dd-MM-yyyy");
                    fifth = ComboBox3.SelectedValue.ToString();//сделать, чтобы не было видно
                    sql = "INSERT INTO \"Assistance to kindergarten\"(id_kindergarten, id_type, cost, date, id_company ) VALUES(" + first + ", " + second + ", " + third + ", '" + fourth + "', " + fifth + ");";
                }
            }
            else if (Name1 == "помощь детям")
            {
                if (TextBox1.Text == "" || ComboBox1.Text == "" || ComboBox2.Text == "" )
                {
                    sql = "INSERT INTO \"Company name\"(company_name) VALUES(" + first + ")";
                }
                else
                {
                    first = ComboBox1.SelectedValue.ToString();
                    second = ComboBox2.SelectedValue.ToString();
                    third = TextBox1.Text;
                    fourth = DateTime.Now.ToString("dd-MM-yyyy");
                    fifth = ComboBox3.SelectedValue.ToString();//сделать, чтобы не было видно
                    sql = "INSERT INTO \"Assistance to children\"(id_child, id_type, cost, date, id_company ) VALUES(" + first + ", " + second + ", " + third + ", '" + fourth + "', " + fifth + ");";
                }
            }
        }
        /// <summary>
        /// комманды и переменные для изменения данных в таблице
        /// </summary>
        private void command_sql_update()
        {
            if (Name1 == "район")
            {
                if (TextBox1.Text == "")
                {
                    sql = "INSERT INTO \"Company name\"(company_name) VALUES(" + first + ")";
                }
                else
                {
                    id = ID.Text;
                    first = TextBox1.Text;
                    sql = "UPDATE \"District\" SET (district) =('" + first + "') WHERE id_district='" + id + "';";
                }
            }
            else if (Name1 == "тип собственности")
            {
                if (TextBox1.Text == "")
                {
                    sql = "INSERT INTO \"Company name\"(company_name) VALUES(" + first + ")";
                }
                else
                {
                    id = ID.Text;
                    first = TextBox1.Text;
                    sql = "UPDATE \"Type of property\" SET (type) =('" + first + "') WHERE id_type='" + id + "';";
                }
            }
            else if (Name1 == "вид помощи садам")
            {
                if (TextBox1.Text == "")
                {
                    sql = "INSERT INTO \"Company name\"(company_name) VALUES(" + first + ")";
                }
                else
                {
                    id = ID.Text;
                    first = TextBox1.Text;
                    sql = "UPDATE \"Type of garden assistance\" SET (type) =('" + first + "') WHERE id_type='" + id + "';";
                }
            }
            else if (Name1 == "детский сад")
            {
                if (TextBox1.Text == "" || ComboBox1.Text == "" || ComboBox2.Text == "" || TextBox2.Text == "" || YearBox.Text == "")
                {
                    sql = "INSERT INTO \"Company name\"(company_name) VALUES(" + first + ")";
                }
                else
                {
                    id = ID.Text;
                    first = TextBox1.Text;
                    second = ComboBox1.SelectedValue.ToString();
                    third = ComboBox2.SelectedValue.ToString();
                    fourth = YearBox.Text;
                    fifth = TextBox2.Text;
                    sql = "UPDATE \"Kindergarten\" SET (caption, id_district, id_type, year_of_open, phone) =('" + first + "', " + second + ", " + third + ", " + fourth + ", '" + fifth + "') WHERE id_kindergarten='" + id + "';";
                }
            }
            else if (Name1 == "форма собственности")
            {
                if (TextBox1.Text == "")
                {
                    sql = "INSERT INTO \"Company name\"(company_name) VALUES(" + first + ")";
                }
                else
                {
                    id = ID.Text;
                    first = TextBox1.Text;
                    sql = "UPDATE \"Type of ownership\" SET (type) =('" + first + "') WHERE id_type='" + id + "';";
                }
            }
            else if (Name1 == "предприятие")
            {
                if (TextBox1.Text == "" || ComboBox1.Text == "" || TextBox3.Text == "" || TextBox2.Text == "" || YearBox.Text == "")
                {
                    sql = "INSERT INTO \"Company name\"(company_name) VALUES(" + first + ")";
                }
                else
                {
                    id = ID.Text;
                    first = TextBox1.Text;
                    second = ComboBox1.SelectedValue.ToString();
                    third = TextBox2.Text;
                    fourth = YearBox.Text;
                    fifth = TextBox3.Text;
                    sql = "UPDATE \"Company\" SET (name, id_type, phone, year, employees) =('" + first + "', " + second + ", '" + third + "', " + fourth + ", " + fifth + ") WHERE id_company='" + id + "';";
                }
            }
            else if (Name1 == "группа")
            {
                if (TextBox1.Text == "")
                {
                    sql = "INSERT INTO \"Company name\"(company_name) VALUES(" + first + ")";
                }
                else
                {
                    id = ID.Text;
                    first = TextBox1.Text;
                    sql = "UPDATE \"Group\" SET (group) =('" + first + "') WHERE id_group='" + id + "';";
                }
            }
            else if (Name1 == "вид помощи детям")
            {
                if (TextBox1.Text == "")
                {
                    sql = "INSERT INTO \"Company name\"(company_name) VALUES(" + first + ")";
                }
                else
                {
                    id = ID.Text;
                    first = TextBox1.Text;
                    sql = "UPDATE \"Type of children assistance\" SET (type) =('" + first + "') WHERE id_type='" + id + "';"; ;
                }
            }
            else if (Name1 == "сотрудник сада")
            {
                if (TextBox1.Text == "" || ComboBox1.Text == "" || ComboBox2.Text == "" || TextBox2.Text == "")
                {
                    sql = "INSERT INTO \"Company name\"(company_name) VALUES(" + first + ")";
                }
                else
                {
                    first = TextBox1.Text;//логин
                    second = ComboBox1.SelectedValue.ToString();//предприятие-сад
                    third = ComboBox2.Text;//роль(addmin, cemployee, kemployee)
                    fourth = TextBox2.Text;//пароль
                    if (third == "админ")
                    {
                        addsql1 = "REVOKE \"addmin\" FROM \"" + first + "\"; REVOKE \"kemployee\" FROM \"" + first + "\";  GRANT \"addmin\" TO \"" + first + "\" WITH ADMIN OPTION;  ALTER ROLE \"" + first + "\" LOGIN PASSWORD '" + fourth + "'; ";
                    }
                    else if (third == "сотрудник")
                    {
                        sql = "UPDATE \"Employee of the kindergarten\" SET (login, id_kindergarten) =('" + first + "', " + second + ") WHERE login ='" + first + "';";
                        addsql1 = "REVOKE \"addmin\" FROM \"" + first + "\"; REVOKE \"kemployee\" FROM \"" + first + "\";  GRANT \"kemployee\" TO \"" + first + "\" WITH ADMIN OPTION;  ALTER ROLE \"" + first + "\" LOGIN PASSWORD '" + fourth + "'; ";
                    }
                }
            }
            else if (Name1 == "сотрудник предприятия")
            {
                if (TextBox1.Text == "" || ComboBox1.Text == "" || ComboBox2.Text == "" || TextBox2.Text == "")
                {
                    sql = "INSERT INTO \"Company name\"(company_name) VALUES(" + first + ")";
                }
                else
                {
                    first = TextBox1.Text;//логин
                    second = ComboBox1.SelectedValue.ToString();//предприятие-сад
                    third = ComboBox2.Text;//роль(addmin, cemployee, kemployee)
                    fourth = TextBox2.Text;//пароль
                    if (third == "админ")
                    {
                        addsql1 = "REVOKE \"addmin\" FROM \"" + first + "\"; REVOKE \"cemployee\" FROM \"" + first + "\";  GRANT \"addmin\" TO \"" + first + "\" WITH ADMIN OPTION;  ALTER ROLE \"" + first + "\" LOGIN PASSWORD '" + fourth + "'; ";
                    }
                    else if (third == "сотрудник")
                    {
                        sql = "UPDATE \"Employee of the company\" SET (login, id_company) =('" + first + "', " + second + ") WHERE login ='" + first + "';";
                        addsql1 = "REVOKE \"addmin\" FROM \"" + first + "\"; REVOKE \"cemployee\" FROM \"" + first + "\";  GRANT \"cemployee\" TO \"" + first + "\" WITH ADMIN OPTION;  ALTER ROLE \"" + first + "\" LOGIN PASSWORD '" + fourth + "'; ";
                    }
                }
            }
            else if (Name1 == "ребёнок")
            {
                if (TextBox1.Text == "" || ComboBox1.Text == "" || ComboBox2.Text == "" || TextBox3.Text == "" || TextBox2.Text == "" || DateTimePicker1.Value.ToShortDateString() == "")
                {
                    sql = "INSERT INTO \"Company name\"(company_name) VALUES(" + first + ")";
                }
                else
                {
                    id = ID.Text;
                    first = TextBox1.Text;
                    second = TextBox2.Text;
                    third = TextBox3.Text;
                    fourth = DateTimePicker1.Value.ToShortDateString();
                    fifth = ComboBox1.SelectedValue.ToString();
                    sixth = ComboBox2.SelectedValue.ToString();
                    sql = "INSERT INTO\"Child\" (surname, name, patronymic, birthday, id_group, id_kindergarten) VALUES('" + first + "', '" + second + "', '" + third + "', '" + fourth + "', " + fifth + ", " + sixth + ");";
                }
            }
            else if (Name1 == "помощь детским садам")
            {
                if (TextBox1.Text == "" || ComboBox1.Text == "" || ComboBox2.Text == "")
                {
                    sql = "INSERT INTO \"Company name\"(company_name) VALUES(" + first + ")";
                }
                else
                {
                    id = ID.Text;
                    first = ComboBox1.SelectedValue.ToString();
                    second = ComboBox2.SelectedValue.ToString();
                    third = TextBox1.Text;
                    fourth = DateTime.Now.ToString("dd-MM-yyyy");
                    fifth = ComboBox3.SelectedValue.ToString();//сделать, чтобы не было видно
                    sql = "UPDATE \"Assistance to kindergarten\" SET (id_kindergarten, id_type, cost, date, id_company ) =(" + first + ", " + second + ", " + third + ", '" + fourth + "', " + fifth + ") WHERE id_assistance=" + id + ";";
                }
            }
            else if (Name1 == "помощь детям")
            {
                if (TextBox1.Text == "" || ComboBox1.Text == "" || ComboBox2.Text == "")
                {
                    sql = "INSERT INTO \"Company name\"(company_name) VALUES(" + first + ")";
                }
                else
                {
                    id = ID.Text;
                    first = ComboBox1.SelectedValue.ToString();
                    second = ComboBox2.SelectedValue.ToString();
                    third = TextBox1.Text;
                    fourth = DateTime.Now.ToString("dd-MM-yyyy");
                    fifth = ComboBox3.SelectedValue.ToString();//сделать, чтобы не было видно
                    sql = "UPDATE \"Assistance to children\" SET (id_child, id_type, cost, date, id_company ) =(" + first + ", " + second + ", " + third + ", '" + fourth + "', " + fifth + ") WHERE id_assistance=" + id + ";";
                }
            }
        }

        /// <summary>
        /// метод для определения элементов, которые необходимы для каждой таблицы
        /// </summary>
        private void load_dataGridView()
        {
            if (Name1 == "район")
            {
                ClientSize = new Size(200, 120);
                MaximumSize = new Size(216, 158);
                MinimumSize = new Size(216, 158);
                button1.Location = new Point(12, 60);

                Label1.Location = new Point(45, 10);
                Label1.Size = new Size(150, 20);
                Label1.Text = "Название района";
                Controls.Add(Label1);

                TextBox1.Location = new Point(25, 30);
                TextBox1.Size = new Size(150, 20);
                Controls.Add(TextBox1);
            }
            else if (Name1 == "тип собственности")
            {
                ClientSize = new Size(200, 120);
                MaximumSize = new Size(216, 158);
                MinimumSize = new Size(216, 158);
                button1.Location = new Point(12, 60);

                Label1.Location = new Point(45, 10);
                Label1.Size = new Size(150, 20);
                Label1.Text = "тип собственности";
                Controls.Add(Label1);

                TextBox1.Location = new Point(25, 30);
                TextBox1.Size = new Size(150, 20);
                Controls.Add(TextBox1);
            }
            else if (Name1 == "вид помощи садам")
            {
                ClientSize = new Size(200, 120);
                MaximumSize = new Size(216, 158);
                MinimumSize = new Size(216, 158);
                button1.Location = new Point(12, 60);

                Label1.Location = new Point(45, 10);
                Label1.Size = new Size(150, 20);
                Label1.Text = "вид помощи садам";
                Controls.Add(Label1);

                TextBox1.Location = new Point(25, 30);
                TextBox1.Size = new Size(150, 20);
                Controls.Add(TextBox1);
            }
            else if (Name1 == "вид помощи детям")
            {
                ClientSize = new Size(200, 120);
                MaximumSize = new Size(216, 158);
                MinimumSize = new Size(216, 158);
                button1.Location = new Point(12, 60);

                Label1.Location = new Point(45, 10);
                Label1.Size = new Size(150, 20);
                Label1.Text = "вид помощи детям";
                Controls.Add(Label1);

                TextBox1.Location = new Point(25, 30);
                TextBox1.Size = new Size(150, 20);
                Controls.Add(TextBox1);
            }
            else if (Name1 == "форма собственности")
            {
                ClientSize = new Size(200, 120);
                MaximumSize = new Size(216, 158);
                MinimumSize = new Size(216, 158);
                button1.Location = new Point(12, 60);

                Label1.Location = new Point(45, 10);
                Label1.Size = new Size(150, 20);
                Label1.Text = "Форма собственности";
                Controls.Add(Label1);

                TextBox1.Location = new Point(25, 30);
                TextBox1.Size = new Size(150, 20);
                Controls.Add(TextBox1);
            }
            else if (Name1 == "группа")
            {
                ClientSize = new Size(200, 120);
                MaximumSize = new Size(216, 158);
                MinimumSize = new Size(216, 158);
                button1.Location = new Point(12, 60);

                Label1.Location = new Point(45, 10);
                Label1.Size = new Size(150, 20);
                Label1.Text = "Название группы";
                Controls.Add(Label1);

                TextBox1.Location = new Point(25, 30);
                TextBox1.Size = new Size(150, 20);
                Controls.Add(TextBox1);
            }
            else if (Name1 == "ребёнок")
            {
                ClientSize = new Size(200, 289);
                MaximumSize = new Size(216, 317);
                MinimumSize = new Size(216, 317);
                button1.Location = new Point(15, 220);

                Label1.Location = new Point(60, 10);
                Label1.Size = new Size(100, 20);
                Label1.Text = "Фамилия";
                Controls.Add(Label1);

                TextBox1.Location = new Point(35, 30);
                TextBox1.Size = new Size(130, 20);
                Controls.Add(TextBox1);

                Label2.Location = new Point(60, 50);
                Label2.Size = new Size(100, 20);
                Label2.Text = "Имя";
                Controls.Add(Label2);

                TextBox2.Location = new Point(35, 70);
                TextBox2.Size = new Size(130, 20);
                Controls.Add(TextBox2);

                Label3.Location = new Point(60, 90);
                Label3.Size = new Size(100, 20);
                Label3.Text = "Отчество";
                Controls.Add(Label3);

                TextBox3.Location = new Point(35, 110);
                TextBox3.Size = new Size(130, 20);
                Controls.Add(TextBox3);

                Label4.Location = new Point(60, 130);
                Label4.Size = new Size(100, 20);
                Label4.Text = "День рождения";
                Controls.Add(Label4);

                DateTimePicker1.Location = new Point(35, 150);
                DateTimePicker1.Size = new Size(130, 20);
                DateTimePicker1.Format = DateTimePickerFormat.Custom;
                DateTimePicker1.CustomFormat = " ";
                DateTimePicker1.ValueChanged += new EventHandler(dateTimePicker3_ValueChanged);
                Controls.Add(DateTimePicker1);

                Label5.Location = new Point(60, 175);
                Label5.Size = new Size(100, 20);
                Label5.Text = "Группа";
                Controls.Add(Label5);

                ComboBox1.Location = new Point(35, 195);
                ComboBox1.Size = new Size(130, 20);
                ComboBox1.DropDownStyle = ComboBoxStyle.DropDownList;
                Controls.Add(ComboBox1);
                try
                {
                    npgSqlConnection = new NpgsqlConnection(connectionString);
                    npgSqlConnection.Open();
                    sqlCommand = new NpgsqlCommand("SELECT  * FROM  public.\"Group\"", npgSqlConnection);

                    dataReader = sqlCommand.ExecuteReader();
                    dt = new DataTable();
                    dt.Load(dataReader);
                    ComboBox1.DataSource = dt;
                    ComboBox1.DisplayMember = "group";
                    ComboBox1.ValueMember = "id_group";
                    npgSqlConnection.Close();
                }
                catch (Exception ex) { MessageBox.Show(ex.Message); npgSqlConnection.Close(); }
                ComboBox1.SelectedIndex = -1;

                ComboBox2.Location = new Point(60, 220);
                ComboBox2.Size = new Size(10, 10);
                ComboBox2.DropDownStyle = ComboBoxStyle.DropDownList;
                Controls.Add(ComboBox2);
                try
                {
                    npgSqlConnection = new NpgsqlConnection(connectionString);
                    npgSqlConnection.Open();
                    sqlCommand = new NpgsqlCommand("SELECT  \"Kindergarten\".id_kindergarten,  caption  FROM  \"Employee of the kindergarten\"  INNER JOIN \"Kindergarten\" ON \"Employee of the kindergarten\".id_kindergarten = \"Kindergarten\".id_kindergarten  WHERE \"login\" = (SELECT CURRENT_USER)  ", npgSqlConnection);

                    dataReader = sqlCommand.ExecuteReader();
                    dt = new DataTable();
                    dt.Load(dataReader);
                    ComboBox2.DataSource = dt;
                    ComboBox2.DisplayMember = "caption";
                    ComboBox2.ValueMember = "id_kindergarten";
                    npgSqlConnection.Close();
                }
                catch (Exception ex) { MessageBox.Show(ex.Message); npgSqlConnection.Close(); }
            }
            else if (Name1 == "помощь детским садам")
            {
                ClientSize = new Size(200, 219);
                MaximumSize = new Size(216, 247);
                MinimumSize = new Size(216, 247);
                button1.Location = new Point(15, 150);

                Label1.Location = new Point(60, 10);
                Label1.Size = new Size(130, 20);
                Label1.Text = "Детский сад";
                Controls.Add(Label1);

                ComboBox1.Location = new Point(35, 30);
                ComboBox1.Size = new Size(130, 18);
                ComboBox1.DropDownStyle = ComboBoxStyle.DropDownList;
                Controls.Add(ComboBox1);
                try
                {
                    npgSqlConnection = new NpgsqlConnection(connectionString);
                    npgSqlConnection.Open();
                    sqlCommand = new NpgsqlCommand("SELECT  * FROM  public.\"Kindergarten\"", npgSqlConnection);

                    dataReader = sqlCommand.ExecuteReader();
                    dt = new DataTable();
                    dt.Load(dataReader);
                    ComboBox1.DataSource = dt;
                    ComboBox1.DisplayMember = "caption";
                    ComboBox1.ValueMember = "id_kindergarten";
                    npgSqlConnection.Close();
                }
                catch (Exception ex) { MessageBox.Show(ex.Message); npgSqlConnection.Close(); }
                ComboBox1.SelectedIndex = -1;

                Label2.Location = new Point(60, 53);
                Label2.Size = new Size(130, 20);
                Label2.Text = "Вид помощи";
                Controls.Add(Label2);

                ComboBox2.Location = new Point(35, 73);
                ComboBox2.Size = new Size(130, 18);
                ComboBox2.DropDownStyle = ComboBoxStyle.DropDownList;
                Controls.Add(ComboBox2);
                try
                {
                    npgSqlConnection = new NpgsqlConnection(connectionString);
                    npgSqlConnection.Open();
                    sqlCommand = new NpgsqlCommand("SELECT * FROM  \"Type of garden assistance\" ", npgSqlConnection);

                    dataReader = sqlCommand.ExecuteReader();
                    dt = new DataTable();
                    dt.Load(dataReader);
                    ComboBox2.DataSource = dt;
                    ComboBox2.DisplayMember = "type";
                    ComboBox2.ValueMember = "id_type";
                    npgSqlConnection.Close();
                }
                catch (Exception ex) { MessageBox.Show(ex.Message); npgSqlConnection.Close(); }
                ComboBox2.SelectedIndex = -1;

                Label3.Location = new Point(60, 96);
                Label3.Size = new Size(130, 20);
                Label3.Text = "Стоимость";
                Controls.Add(Label3);

                TextBox1.Location = new Point(35, 116);
                TextBox1.Size = new Size(130, 20);
                TextBox1.KeyPress += new KeyPressEventHandler(TextBox1KeyPress);
                Controls.Add(TextBox1);

                ComboBox3.Location = new Point(60, 150);
                ComboBox3.Size = new Size(10, 10);
                ComboBox3.DropDownStyle = ComboBoxStyle.DropDownList;
                Controls.Add(ComboBox3);
                try
                {
                    npgSqlConnection = new NpgsqlConnection(connectionString);
                    npgSqlConnection.Open();
                    sqlCommand = new NpgsqlCommand("SELECT  \"Company\".id_company,  \"name\"  FROM  \"Employee of the company\"  INNER JOIN \"Company\" ON \"Employee of the company\".id_company = \"Company\".id_company WHERE \"login\" = (SELECT CURRENT_USER)  ", npgSqlConnection);

                    dataReader = sqlCommand.ExecuteReader();
                    dt = new DataTable();
                    dt.Load(dataReader);
                    ComboBox3.DataSource = dt;
                    ComboBox3.DisplayMember = "\"name\"";
                    ComboBox3.ValueMember = "id_company";
                    npgSqlConnection.Close();
                }
                catch (Exception ex) { MessageBox.Show(ex.Message); npgSqlConnection.Close(); }
            }
            else if (Name1 == "помощь детям")
            {
                ClientSize = new Size(200, 219);
                MaximumSize = new Size(216, 247);
                MinimumSize = new Size(216, 247);
                button1.Location = new Point(15, 150);

                Label1.Location = new Point(60, 10);
                Label1.Size = new Size(130, 20);
                Label1.Text = "Ребёнок";
                Controls.Add(Label1);

                ComboBox1.Location = new Point(1, 30);
                ComboBox1.Size = new Size(195, 18);
                ComboBox1.DropDownStyle = ComboBoxStyle.DropDownList;
                Controls.Add(ComboBox1);
                try
                {
                    npgSqlConnection = new NpgsqlConnection(connectionString);
                    npgSqlConnection.Open();
                    sqlCommand = new NpgsqlCommand("SELECT  id_child as \"id\",  concat_ws(' ',surname,\"name\",patronymic) as \"ФИО\"  FROM  public.\"Child\"  ", npgSqlConnection);

                    dataReader = sqlCommand.ExecuteReader();
                    dt = new DataTable();
                    dt.Load(dataReader);
                    ComboBox1.DataSource = dt;
                    ComboBox1.DisplayMember = "ФИО";
                    ComboBox1.ValueMember = "id";
                    npgSqlConnection.Close();
                }
                catch (Exception ex) { MessageBox.Show(ex.Message); npgSqlConnection.Close(); }
                ComboBox1.SelectedIndex = -1;

                Label2.Location = new Point(60, 53);
                Label2.Size = new Size(130, 20);
                Label2.Text = "Вид помощи";
                Controls.Add(Label2);

                ComboBox2.Location = new Point(35, 73);
                ComboBox2.Size = new Size(130, 18);
                ComboBox2.DropDownStyle = ComboBoxStyle.DropDownList;
                Controls.Add(ComboBox2);
                try
                {
                    npgSqlConnection = new NpgsqlConnection(connectionString);
                    npgSqlConnection.Open();
                    sqlCommand = new NpgsqlCommand("SELECT * FROM  \"Type of children assistance\" ", npgSqlConnection);

                    dataReader = sqlCommand.ExecuteReader();
                    dt = new DataTable();
                    dt.Load(dataReader);
                    ComboBox2.DataSource = dt;
                    ComboBox2.DisplayMember = "type";
                    ComboBox2.ValueMember = "id_type";
                    npgSqlConnection.Close();
                }
                catch (Exception ex) { MessageBox.Show(ex.Message); npgSqlConnection.Close(); }
                ComboBox2.SelectedIndex = -1;

                Label3.Location = new Point(60, 96);
                Label3.Size = new Size(130, 20);
                Label3.Text = "Стоимость";
                Controls.Add(Label3);

                TextBox1.Location = new Point(35, 116);
                TextBox1.Size = new Size(130, 20);
                TextBox1.KeyPress += new KeyPressEventHandler(TextBox1KeyPress);
                Controls.Add(TextBox1);

                ComboBox3.Location = new Point(60, 150);
                ComboBox3.Size = new Size(10, 10);
                ComboBox3.DropDownStyle = ComboBoxStyle.DropDownList;
                Controls.Add(ComboBox3);
                try
                {
                    npgSqlConnection = new NpgsqlConnection(connectionString);
                    npgSqlConnection.Open();
                    sqlCommand = new NpgsqlCommand("SELECT  \"Company\".id_company,  \"name\"  FROM  \"Employee of the company\"  INNER JOIN \"Company\" ON \"Employee of the company\".id_company = \"Company\".id_company WHERE \"login\" = (SELECT CURRENT_USER)  ", npgSqlConnection);

                    dataReader = sqlCommand.ExecuteReader();
                    dt = new DataTable();
                    dt.Load(dataReader);
                    ComboBox3.DataSource = dt;
                    ComboBox3.DisplayMember = "\"name\"";
                    ComboBox3.ValueMember = "id_company";
                    npgSqlConnection.Close();
                }
                catch (Exception ex) { MessageBox.Show(ex.Message); npgSqlConnection.Close(); }
            }
            else if (Name1 == "сотрудник сада")
            {
                ClientSize = new Size(200, 259);
                MaximumSize = new Size(216, 287);
                MinimumSize = new Size(216, 287);
                button1.Location = new Point(15, 190);

                Label1.Location = new Point(60, 10);
                Label1.Size = new Size(130, 20);
                Label1.Text = "Где работает";
                Controls.Add(Label1);

                ComboBox1.Location = new Point(35, 30);
                ComboBox1.Size = new Size(130, 18);
                ComboBox1.DropDownStyle = ComboBoxStyle.DropDownList;
                Controls.Add(ComboBox1);
                try
                {
                    npgSqlConnection = new NpgsqlConnection(connectionString);
                    npgSqlConnection.Open();
                    sqlCommand = new NpgsqlCommand("SELECT  * FROM  public.\"Kindergarten\"", npgSqlConnection);

                    dataReader = sqlCommand.ExecuteReader();
                    dt = new DataTable();
                    dt.Load(dataReader);
                    ComboBox1.DataSource = dt;
                    ComboBox1.DisplayMember = "caption";
                    ComboBox1.ValueMember = "id_kindergarten";
                    npgSqlConnection.Close();
                }
                catch (Exception ex) { MessageBox.Show(ex.Message); npgSqlConnection.Close(); }
                ComboBox1.SelectedIndex = -1;

                Label2.Location = new Point(60, 53);
                Label2.Size = new Size(130, 20);
                Label2.Text = "Роль";
                Controls.Add(Label2);

                ComboBox2.Location = new Point(35, 73);
                ComboBox2.Size = new Size(130, 18);
                ComboBox2.Items.AddRange(new string[] { "сотрудник", "админ" });
                ComboBox2.DropDownStyle = ComboBoxStyle.DropDownList;
                Controls.Add(ComboBox2);
                ComboBox2.SelectedIndex = -1;

                Label3.Location = new Point(60, 96);
                Label3.Size = new Size(130, 20);
                Label3.Text = "Логин";
                Controls.Add(Label3);

                TextBox1.Location = new Point(25, 116);
                TextBox1.Size = new Size(160, 20);
                Controls.Add(TextBox1);

                Label4.Location = new Point(60, 136);
                Label4.Size = new Size(130, 20);
                Label4.Text = "Пароль";
                Controls.Add(Label4);

                TextBox2.Location = new Point(35, 156);
                TextBox2.Size = new Size(130, 20);
                Controls.Add(TextBox2);
            }
            else if (Name1 == "сотрудник предприятия")
            {
                ClientSize = new Size(200, 259);
                MaximumSize = new Size(216, 287);
                MinimumSize = new Size(216, 287);
                button1.Location = new Point(15, 190);

                Label1.Location = new Point(60, 10);
                Label1.Size = new Size(130, 20);
                Label1.Text = "Где работает";
                Controls.Add(Label1);

                ComboBox1.Location = new Point(35, 30);
                ComboBox1.Size = new Size(130, 18);
                ComboBox1.DropDownStyle = ComboBoxStyle.DropDownList;
                Controls.Add(ComboBox1);
                try
                {
                    npgSqlConnection = new NpgsqlConnection(connectionString);
                    npgSqlConnection.Open();
                    sqlCommand = new NpgsqlCommand("SELECT  * FROM  public.\"Company\"", npgSqlConnection);

                    dataReader = sqlCommand.ExecuteReader();
                    dt = new DataTable();
                    dt.Load(dataReader);
                    ComboBox1.DataSource = dt;
                    ComboBox1.DisplayMember = "name";
                    ComboBox1.ValueMember = "id_company";
                    npgSqlConnection.Close();
                }
                catch (Exception ex) { MessageBox.Show(ex.Message); npgSqlConnection.Close(); }
                ComboBox1.SelectedIndex = -1;

                Label2.Location = new Point(60, 53);
                Label2.Size = new Size(130, 20);
                Label2.Text = "Роль";
                Controls.Add(Label2);

                ComboBox2.Location = new Point(35, 73);
                ComboBox2.Size = new Size(130, 18);
                ComboBox2.Items.AddRange(new string[] { "сотрудник", "админ" });
                ComboBox2.DropDownStyle = ComboBoxStyle.DropDownList;
                Controls.Add(ComboBox2);
                ComboBox2.SelectedIndex = -1;

                Label3.Location = new Point(60, 96);
                Label3.Size = new Size(130, 20);
                Label3.Text = "Логин";
                Controls.Add(Label3);

                TextBox1.Location = new Point(25, 116);
                TextBox1.Size = new Size(160, 20);
                Controls.Add(TextBox1);

                Label4.Location = new Point(60, 136);
                Label4.Size = new Size(130, 20);
                Label4.Text = "Пароль";
                Controls.Add(Label4);

                TextBox2.Location = new Point(35, 156);
                TextBox2.Size = new Size(130, 20);
                Controls.Add(TextBox2);
            }
            else if (Name1 == "предприятие")
            {
                ClientSize = new Size(270, 239);
                MaximumSize = new Size(286, 267);
                MinimumSize = new Size(286, 267);
                button1.Location = new Point(45, 170);

                Label1.Location = new Point(5, 19);
                Label1.Size = new Size(120, 20);
                Label1.Text = "Название преприятия";
                Controls.Add(Label1);

                TextBox1.Location = new Point(5, 39);
                TextBox1.Size = new Size(120, 18);
                Controls.Add(TextBox1);

                Label2.Location = new Point(130, 19);
                Label2.Size = new Size(130, 20);
                Label2.Text = "Форма собственности";
                Controls.Add(Label2);

                ComboBox1.Location = new Point(130, 39);
                ComboBox1.Size = new Size(120, 18);
                ComboBox1.DropDownStyle = ComboBoxStyle.DropDownList;
                Controls.Add(ComboBox1);
                try
                {
                    npgSqlConnection = new NpgsqlConnection(connectionString);
                    npgSqlConnection.Open();
                    sqlCommand = new NpgsqlCommand("SELECT  * FROM  public.\"Type of ownership\"", npgSqlConnection);

                    dataReader = sqlCommand.ExecuteReader();
                    dt = new DataTable();
                    dt.Load(dataReader);
                    ComboBox1.DataSource = dt;
                    ComboBox1.DisplayMember = "type";
                    ComboBox1.ValueMember = "id_type";
                    npgSqlConnection.Close();
                }
                catch (Exception ex) { MessageBox.Show(ex.Message); npgSqlConnection.Close(); }
                ComboBox1.SelectedIndex = -1;

                Label3.Location = new Point(160, 63);
                Label3.Size = new Size(100, 20);
                Label3.Text = "Телефон";
                Controls.Add(Label3);

                TextBox2.Location = new Point(130, 83);
                TextBox2.Size = new Size(120, 20);
                Controls.Add(TextBox2);

                Label4.Location = new Point(25, 65);
                Label4.Size = new Size(100, 20);
                Label4.Text = "Год открытия";
                Controls.Add(Label4);

                YearBox.Location = new Point(15, 85);
                YearBox.Size = new Size(100, 20);
                YearBox.Maximum = new decimal(new int[] { 2020, 0, 0, 0 });
                YearBox.Minimum = new decimal(new int[] { 1970, 0, 0, 0 });
                Controls.Add(YearBox);

                Label5.Location = new Point(130, 103);
                Label5.Size = new Size(130, 20);
                Label5.Text = "Количество работников";
                Controls.Add(Label5);

                TextBox3.Location = new Point(130, 123);
                TextBox3.Size = new Size(120, 20);
                TextBox3.KeyPress += new KeyPressEventHandler(TextBox1KeyPress);
                Controls.Add(TextBox3);
            }
            else if (Name1 == "детский сад")
            {
                ClientSize = new Size(270, 239);
                MaximumSize = new Size(286, 267);
                MinimumSize = new Size(286, 267);
                button1.Location = new Point(45, 170);

                Label1.Location = new Point(5, 19);
                Label1.Size = new Size(120, 20);
                Label1.Text = "Название детсада";
                Controls.Add(Label1);

                TextBox1.Location = new Point(5, 39);
                TextBox1.Size = new Size(120, 18);
                Controls.Add(TextBox1);

                Label2.Location = new Point(130, 19);
                Label2.Size = new Size(130, 20);
                Label2.Text = "Район";
                Controls.Add(Label2);

                ComboBox1.Location = new Point(130, 39);
                ComboBox1.Size = new Size(120, 18);
                ComboBox1.DropDownStyle = ComboBoxStyle.DropDownList;
                Controls.Add(ComboBox1);
                try
                {
                    npgSqlConnection = new NpgsqlConnection(connectionString);
                    npgSqlConnection.Open();
                    sqlCommand = new NpgsqlCommand("SELECT  * FROM  public.\"District\"", npgSqlConnection);

                    dataReader = sqlCommand.ExecuteReader();
                    dt = new DataTable();
                    dt.Load(dataReader);
                    ComboBox1.DataSource = dt;
                    ComboBox1.DisplayMember = "district";
                    ComboBox1.ValueMember = "id_district";
                    npgSqlConnection.Close();
                }
                catch (Exception ex) { MessageBox.Show(ex.Message); npgSqlConnection.Close(); }
                ComboBox1.SelectedIndex = -1;

                Label3.Location = new Point(160, 63);
                Label3.Size = new Size(100, 20);
                Label3.Text = "Телефон";
                Controls.Add(Label3);

                TextBox2.Location = new Point(130, 83);
                TextBox2.Size = new Size(120, 20);
                Controls.Add(TextBox2);

                Label4.Location = new Point(25, 35);
                Label4.Size = new Size(100, 20);
                Label4.Text = "Год открытия";
                Controls.Add(Label4);

                YearBox.Location = new Point(15, 85);
                YearBox.Size = new Size(100, 20);
                YearBox.Maximum = new decimal(new int[] { 2020, 0, 0, 0 });
                YearBox.Minimum = new decimal(new int[] { 1970, 0, 0, 0 });
                Controls.Add(YearBox);

                Label5.Location = new Point(130, 103);
                Label5.Size = new Size(130, 20);
                Label5.Text = "Тип собственности";
                Controls.Add(Label5);
                
                ComboBox2.Location = new Point(130, 123);
                ComboBox2.Size = new Size(120, 18);
                ComboBox2.DropDownStyle = ComboBoxStyle.DropDownList;
                Controls.Add(ComboBox2);
                try
                {
                    npgSqlConnection = new NpgsqlConnection(connectionString);
                    npgSqlConnection.Open();
                    sqlCommand = new NpgsqlCommand("SELECT  * FROM  public.\"Type of property\"", npgSqlConnection);

                    dataReader = sqlCommand.ExecuteReader();
                    dt = new DataTable();
                    dt.Load(dataReader);
                    ComboBox2.DataSource = dt;
                    ComboBox2.DisplayMember = "type";
                    ComboBox2.ValueMember = "id_type";
                    npgSqlConnection.Close();
                }
                catch (Exception ex) { MessageBox.Show(ex.Message); npgSqlConnection.Close(); }
                ComboBox2.SelectedIndex = -1;
            }
        }
        /// <summary>
        /// Метод вставки значений в БД
        /// </summary>
        private void button1_Click(object sender, EventArgs e)
        {
            if (ID.Text == "")
            {
                command_sql_insert();
                try
                {
                    npgSqlConnection = new NpgsqlConnection(connectionString);
                    npgSqlConnection.Open();
                    sqlCommand = new NpgsqlCommand(sql, npgSqlConnection);
                    sqlCommand.ExecuteNonQuery();
                    sqlCommand = new NpgsqlCommand(addsql1, npgSqlConnection);
                    sqlCommand.ExecuteNonQuery();
                    npgSqlConnection.Close();
                    MessageBox.Show("Добавленно");
                }
                catch (Exception)
                {
                    MessageBox.Show("Проверьте вводимые данные");
                    npgSqlConnection.Close();
                }
            }
            else if (ID.Text != "")
            {
                command_sql_update();
                try
                {
                    npgSqlConnection = new NpgsqlConnection(connectionString);
                    npgSqlConnection.Open();
                    sqlCommand = new NpgsqlCommand(sql, npgSqlConnection);
                    sqlCommand.ExecuteNonQuery();
                    sqlCommand = new NpgsqlCommand(addsql1, npgSqlConnection);
                    sqlCommand.ExecuteNonQuery();
                    npgSqlConnection.Close();
                    MessageBox.Show("Измененно");
                }
                catch (Exception)
                {
                    MessageBox.Show("Проверьте вводимые данные");
                    npgSqlConnection.Close();
                }
            }
        }
        void TextBox1KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!(Char.IsDigit(e.KeyChar)) && (e.KeyChar != (char)Keys.Back) && (e.KeyChar != (char)Keys.Delete))
            {
                e.Handled = true;
            }
        }

        private void dateTimePicker3_ValueChanged(object sender, EventArgs e)
        {
            DateTimePicker1.Format = DateTimePickerFormat.Short;
            DateTimePicker2.Format = DateTimePickerFormat.Short;
        }
    }
}