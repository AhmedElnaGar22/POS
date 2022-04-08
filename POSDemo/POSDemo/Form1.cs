using POSDemo.DB;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace POSDemo
{
    public partial class Form1 : Form
    {
        POSTutEntities db = new POSTutEntities();
        public Form1()
        {
            InitializeComponent();
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            var result = db.Users.FirstOrDefault(x => x.UserName == txtUser.Text && x.Password == txtPassword.Text);


            if (result != null)
            {
                this.Close();

                Thread th = new Thread(openForm);
                th.SetApartmentState(ApartmentState.STA);
                th.Start();

                Users.Name = result.UserName;
                Users.id = result.id;
            }
            else
            {
                MessageBox.Show("UserName Or Password Are Invalid");
            }
        }

        void openForm()
        {
            Application.Run(new MainForm());
        }
    }
    static class Users
    {
        static public string Name { get; set; }
        static public int id { get; set; }
    }
}
