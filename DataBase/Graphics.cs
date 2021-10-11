using Npgsql;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;

namespace DataBase
{
    public partial class Graphics : Form
    {
        private NpgsqlConnection npgSqlConnection;
        NpgsqlCommand sqlCommand;
        DataTable dt;
        NpgsqlDataReader dataReader;
        string connectionString = "";
        List<string> city = new List<string> { }; List<string> value = new List<string> { };
        List<string> family = new List<string> { }; List<string> count = new List<string> { };
        public Graphics(string usersurname, string userpassword)
        {
            InitializeComponent();
            this.usersurname = usersurname;
            this.userpassword = userpassword; firstGraph();
        }
        private string usersurname;
        private string userpassword;
        void firstGraph()
        {
            try
            {
                connectionString = "Server = localhost;" + "Port = 5432;" + "Database = Дидур;" + "User Id = '" + usersurname + "';" + "Password = '" + userpassword + "';";
                npgSqlConnection = new NpgsqlConnection(connectionString);
                npgSqlConnection.Open();
                sqlCommand = new NpgsqlCommand("SELECT  public.\"Group\".\"group\",  Count(public.\"Child\".surname)  FROM  public.\"Child\"  INNER JOIN public.\"Group\" ON public.\"Child\".id_group = public.\"Group\".id_group  GROUP BY  public.\"Group\".\"group\"  ", npgSqlConnection);
                dataReader = sqlCommand.ExecuteReader();
                dt = new DataTable();
                dt.Load(dataReader);
                dataGridView1.DataSource = dt;
                dataGridView1.AutoResizeColumns();

                npgSqlConnection.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                npgSqlConnection.Close();
            }
            foreach (DataGridViewRow row in dataGridView1.Rows)
            {
                if (row.Cells[1].Value != null)
                {
                    family.Add(row.Cells[0].Value.ToString());
                }
            }
            foreach (DataGridViewRow row in dataGridView1.Rows)
            {
                if (row.Cells[1].Value != null)
                {
                    count.Add(row.Cells[1].Value.ToString());
                }
            }
            chart1.Titles.Add("Диаграмма количества детей в группах");
            chart1.ChartAreas[0].AxisX.Title = "Групы детей";
            chart1.ChartAreas[0].AxisY.Title = "Количество детей";
        }
        void BuildChart(string stroka, string stroka2)
        {
            chart1.Series[0].Points.AddXY(stroka, stroka2);
        }
       
        void build1()
        {
            for (int i = 0; i < family.Count; i++)
                chart1.Invoke(new Action<string, string>(BuildChart), family[i], count[i]);
        }
        
        private void Graphics_Load(object sender, EventArgs e)
        {
            build1();
        }
    }
}
