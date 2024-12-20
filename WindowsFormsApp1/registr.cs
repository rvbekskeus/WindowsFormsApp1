using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApp1
{
    public partial class registr : Form
    {
        SqlConnection conn = new SqlConnection("Data Source=(localdb)\\MSSQLLocalDB;AttachDbFilename=C:\\Users\\User\\AppData\\Local\\Microsoft\\Microsoft SQL Server Local DB\\Instances\\MSSQLLocalDB\\курсовая_внатуре5.mdf;Integrated Security=True");

        public registr()
        {
            InitializeComponent();
        }

        private void bunifuButton21_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(textBox1.Text)
                || string.IsNullOrWhiteSpace(textBox3.Text)
                || string.IsNullOrWhiteSpace(textBox5.Text))
            {
                bunifuSnackbar1.Show(this, "Пожалуйста, заполните все поля.");
                return;
            }

            // Проверка соответствия паролей
            if (textBox3.Text != textBox5.Text) // Предполагается, что это поле для повторного ввода пароля
            {
                bunifuSnackbar1.Show(this, "Пароли не совпадают.");
                return;
            }

            try
            {
                using (SqlConnection conn = new SqlConnection("Data Source=(localdb)\\MSSQLLocalDB;AttachDbFilename=C:\\Users\\User\\AppData\\Local\\Microsoft\\Microsoft SQL Server Local DB\\Instances\\MSSQLLocalDB\\курсовая_внатуре5.mdf;Integrated Security=True"))
                {
                    conn.Open(); // Открываем соединение

                    // Проверка существования пользователя
                    SqlCommand checkUser = new SqlCommand("SELECT COUNT(*) FROM Users WHERE Пользователь = @Пользователь", conn);
                    checkUser.Parameters.Add(new SqlParameter("@Пользователь", SqlDbType.VarChar) { Value = textBox1.Text });

                    int userExists = (int)checkUser.ExecuteScalar();

                    if (userExists > 0)
                    {
                        bunifuSnackbar2.Show(this, "Пользователь с таким именем уже существует.");
                        return;
                    }

                    // Добавление нового пользователя
                    SqlCommand comm = new SqlCommand("UserAdd", conn) { CommandType = CommandType.StoredProcedure };
                    comm.Parameters.Add(new SqlParameter("@Пользователь", SqlDbType.VarChar) { Value = textBox1.Text });
                    comm.Parameters.Add(new SqlParameter("@Пароль", SqlDbType.VarChar) { Value = textBox3.Text }); // Используем textBox3.Text для пароля

                    comm.ExecuteNonQuery(); // Выполняем команду
                    bunifuSnackbar1.Show(this, "Регистрация прошла успешно!");

                    // Настройка таймера
                    Timer timer = new Timer();
                    timer.Interval = 1000; // 3000 мс = 3 секунды
                    timer.Tick += (s, args) =>
                    {
                        timer.Stop(); // Остановить таймер
                        timer.Dispose(); // Освободить ресурсы
                        Form2 frm2 = new Form2();
                        frm2.Show(); // Открыть новую форму
                        this.Close(); // Закрыть текущую форму
                    };
                    timer.Start(); // Запустить таймер
                }
            }
            catch (SqlException ex)
            {
                MessageBox.Show(ex.Message, "Ошибка!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void bunifuButton22_Click(object sender, EventArgs e)
        {
            this.Hide();
            StartForm frmAuthDialog = new StartForm();
            frmAuthDialog.ShowDialog();
        }

        private void bunifuFormCaptionButton1_Click(object sender, EventArgs e)
        {
            this.Hide();
            //f1.Show();
            ConfirmationForm frmVod = new ConfirmationForm();
            frmVod.ShowDialog();
        }
    }

}


