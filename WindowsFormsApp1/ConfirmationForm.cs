using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApp1
{
    public partial class ConfirmationForm : Form
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
        public ConfirmationForm()
        {
            InitializeComponent();
        }      
        private void bunifuButton22_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void bunifuButton23_Click(object sender, EventArgs e)
        {
            this.Hide();
            //f1.Show();
            StartForm frmVod = new StartForm();
            frmVod.ShowDialog();
        }
    }
}
