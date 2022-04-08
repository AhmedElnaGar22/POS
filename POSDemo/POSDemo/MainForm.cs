using POSDemo.Screens.Customer;
using POSDemo.Screens.Products;
using POSDemo.Screens.SalesBill;
using POSDemo.Screens.Suppliers;
using POSDemo.Screens.Users;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace POSDemo
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
        }

        private void fileToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void closeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void addNewUserToolStripMenuItem_Click(object sender, EventArgs e)
        {
            NewUser frm = new NewUser();
            frm.Show();
        }

        private void addProductToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Product p = new Product();
            p.Show();
        }

        private void listProductToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ProductList frm = new ProductList();
            frm.Show();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            ProductList frm = new ProductList();
            frm.Show();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            CustomerManagement frm = new CustomerManagement();
            frm.Show();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Suppliers s = new Suppliers();
            s.Show();
        }

        private void العملاءToolStripMenuItem_Click(object sender, EventArgs e)
        {
            CustomerManagement frm = new CustomerManagement();
            frm.Show();
        }

        private void الموردينToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Suppliers s = new Suppliers();
            s.Show();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            SalesBillsForm frm = new SalesBillsForm();
            frm.Show();
        }
    }
}
