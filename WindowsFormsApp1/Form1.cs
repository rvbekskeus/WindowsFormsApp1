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
using System.Windows.Forms.VisualStyles;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.ToolBar;
using ToolTip = System.Windows.Forms.ToolTip;
using System.Runtime.InteropServices;

namespace WindowsFormsApp1
{
    public partial class Расписание : Form
    {
        private string connectionString = "Data Source = (localdb)\\MSSQLLocalDB;AttachDbFilename = C:\\Users\\User\\AppData\\Local\\Microsoft\\Microsoft SQL Server Local DB\\Instances\\MSSQLLocalDB\\курсовая_внатуре5.mdf;Integrated Security = True";

        [DllImport("Gdi32.dll", EntryPoint = "CreateRoundRectRgn")]
        private static extern IntPtr CreateRoundRectRgn
(
   int nLeftRect,     // x-coordinate of upper-left corner
   int nTopRect,      // y-coordinate of upper-left corner
   int nRightRect,    // x-coordinate of lower-right corner
   int nBottomRect,   // y-coordinate of lower-right corner
   int nWidthEllipse, // width of ellipse
   int nHeightEllipse // height of ellipse
);

        public Расписание()
        {
            InitializeComponent();
            ToolTip toolTip = new ToolTip();
            this.FormBorderStyle = FormBorderStyle.None;
            Region = System.Drawing.Region.FromHrgn(CreateRoundRectRgn(0, 0, Width, Height, 20, 20));

            // Установка времени показа и другие параметры
            toolTip.AutoPopDelay = 5000;
            toolTip.InitialDelay = 0;
            toolTip.ReshowDelay = 500;
            toolTip.ShowAlways = true;

            // Привязываем ToolTip к кнопке
            toolTip.SetToolTip(bunifuButton1P, "Поиск");
            toolTip.SetToolTip(bunifuButtonU, "Удалить записи");
            toolTip.SetToolTip(button1, "Поменять местами");
        }
        private void LoadRoutes()
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                SqlDataAdapter dataAdapter = new SqlDataAdapter("SELECT StartPoint, EndPoint, BusNumber, DepartureTime, ArrivalTime FROM Routes", connection);
                DataTable dataTable = new DataTable();
                dataAdapter.Fill(dataTable);
                dataGridView1.DataSource = dataTable;
            }
        }


       
        private void MainForm_Load(object sender, EventArgs e)
        {
            LoadRoutes();
        }

        string[] suggestedWords =
            {
            "Чебоксары", "предложение2", "пример", "автозаполнение", "текст"
            };
        private void Form12_Load(object sender, EventArgs e)
        {
            // Создаем коллекцию для автозавершения
            AutoCompleteStringCollection autoComplete = new AutoCompleteStringCollection();

            autoComplete.AddRange(suggestedWords);

            // Настраиваем TextBox
            textBox1.AutoCompleteCustomSource = autoComplete;
            textBox1.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
            textBox1.AutoCompleteSource = AutoCompleteSource.CustomSource;
        }
        private void Form1_Load(object sender, EventArgs e)
        {
            AutoCompleteStringCollection autoComplete = new AutoCompleteStringCollection();

            // Подключаемся к базе данных и получаем данные
            using (SqlConnection connection = new SqlConnection(connectionString))
            //using (SqlConnection connection = new SqlConnection("Data Source = (localdb)\\\\MSSQLLocalDB;AttachDbFilename = C:\\\\Users\\\\User\\\\AppData\\\\Local\\\\Microsoft\\\\Microsoft SQL Server Local DB\\\\Instances\\\\MSSQLLocalDB\\\\курсовая_внатуре5.mdf;Integrated Security = True\"));
            {
                connection.Open();
                SqlCommand command = new SqlCommand("SELECT StartPoint FROM Routes", connection);
                SqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    if (reader["StartPoint"] != DBNull.Value)
                    {
                        autoComplete.Add(reader["StartPoint"].ToString());
                    }

                }




                textBox1.AutoCompleteCustomSource = autoComplete;
                textBox1.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
                textBox1.AutoCompleteSource = AutoCompleteSource.CustomSource;
            }
            
        }
        private void Form1_Load_1(object sender, EventArgs e)
        {
            AutoCompleteStringCollection autoCompleteData = new AutoCompleteStringCollection();
            autoCompleteData.AddRange(new string[] {
        "Чебоксары",
        "Нискасы",
        "Ядрин",
        "Цивильск",
        "Батырево",
        "Комсомольское",
        "Канаш",
        "Сосновка",
        "Шумерля",
        "Урмары",
        "Ульяновск",
        "Казань",
        "Козмодемьянск",
        "Аликово",
        "Шемурша",
        "Краноармейское",
        "Ибреси",

    });

            textBox1.AutoCompleteCustomSource = autoCompleteData;
            textBox1.AutoCompleteMode = AutoCompleteMode.Suggest;
            textBox1.AutoCompleteSource = AutoCompleteSource.CustomSource;
            textBox2.AutoCompleteCustomSource = autoCompleteData;
            textBox2.AutoCompleteMode = AutoCompleteMode.Suggest;
            textBox2.AutoCompleteSource = AutoCompleteSource.CustomSource;


        }

        private void button1_Click(object sender, EventArgs e)
        {

            // Сохраняем текст из первого TextBox во временную переменную
            string tempText = textBox1.Text;

            // Меняем текст местами
            textBox1.Text = textBox2.Text;
            textBox2.Text = textBox1.Text;
            button1.FlatAppearance.BorderSize = 0;
            button1.FlatStyle = FlatStyle.Flat;
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            string t1 = textBox1.Text;
            string t2 = textBox2.Text;
            textBox1.Text = t2;
            textBox2.Text = t1;
            button1.FlatAppearance.BorderSize = 0;
            button1.FlatStyle = FlatStyle.Flat;
        }

        private void bunifuButton1P_Click(object sender, EventArgs e)
        {
           
            string startPoint = textBox1.Text;
            string endPoint = textBox2.Text;
            string date = bunifuDatePicker1.Value.ToString("yyyy-MM-dd");
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                string query = "SELECT * FROM Routes WHERE StartPoint = @startPoint AND EndPoint = @endPoint";
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@startPoint", startPoint);
                command.Parameters.AddWithValue("@endPoint", endPoint);


                SqlDataAdapter dataAdapter = new SqlDataAdapter(command);
                DataTable dataTable = new DataTable();
                dataAdapter.Fill(dataTable);
                dataGridView1.DataSource = dataTable;
            }
        }

        private void bunifuButtonU_Click(object sender, EventArgs e)
        {

            textBox1.Clear();
            textBox2.Clear();
           
        }

        private void bunifuDatePicker1_ValueChanged(object sender, EventArgs e)
        {

        }

        private void bunifuButton1_Click(object sender, EventArgs e)
        {
            this.Hide();
            //f1.Show();
            ConfirmationForm frmVod = new ConfirmationForm();
            frmVod.ShowDialog();
        }

    }
}


