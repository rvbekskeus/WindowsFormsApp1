using Bunifu.UI.WinForms;
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
    public partial class Form2 : Form
    {
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
        public Form2()
        {
            InitializeComponent();
            textBox3.UseSystemPasswordChar = false;
        
            this.FormBorderStyle = FormBorderStyle.None;
            Region = System.Drawing.Region.FromHrgn(CreateRoundRectRgn(0, 0, Width, Height, 20, 20));
        }



        private void Form2_Load(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {

                   }

        private void Form2_Load_1(object sender, EventArgs e)
        {
           

   

            
        }   
            private void pic1_Click(object sender, EventArgs e)
        {

        }

        private void materialLabel3_Click(object sender, EventArgs e)
        {

        }

        private void bunifuButton21_Click(object sender, EventArgs e)
        {
            string username = textBox1.Text;
            string password = textBox3.Text;

            using (SqlConnection connection = new SqlConnection("Data Source = (localdb)\\MSSQLLocalDB;AttachDbFilename = C:\\Users\\User\\AppData\\Local\\Microsoft\\Microsoft SQL Server Local DB\\Instances\\MSSQLLocalDB\\курсовая_внатуре5.mdf;Integrated Security = True"))
            {
                connection.Open();

                string query = "SELECT COUNT(*) FROM Users WHERE Пользователь = @Пользователь AND Пароль = @Пароль";

                using (SqlCommand cmd = new SqlCommand(query, connection))
                {
                    // Исправлено имя параметра
                    cmd.Parameters.AddWithValue("@Пользователь", username);
                    cmd.Parameters.AddWithValue("@Пароль", password);

                    int count = (int)cmd.ExecuteScalar();

                    if (count > 0)
                    {
                        
                        this.Hide();
                        AuthDialog frmAuthDialog = new AuthDialog();
                        frmAuthDialog.ShowDialog();
                    }
                    else
                    {
                        bunifuSnackbar1.Show(this, "Неверный логин или пароль.");
                    }
                }
            }
        

        //if (textBox1.Text == "Alexey" && textBox3.Text == "123")
        //{
        //    //bunifuSnackbar1.Show(this, "Добро пожаловать, Alexey");
        //    ////Расписание f1 = new Расписание();
        //    //this.Hide();
        //    ////f1.Show();
        //    //AuthDialog frmAuthDialog = new AuthDialog();
        //    //frmAuthDialog.ShowDialog();

        //}
    }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void bunifuButton22_Click(object sender, EventArgs e)
        {
            this.Hide();
            //f1.Show();
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
