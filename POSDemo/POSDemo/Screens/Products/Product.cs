using POSDemo.DB;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace POSDemo.Screens.Products
{
    public partial class Product : Form
    {
        POSTutEntities db = new POSTutEntities();
        string imagePath = "";
        public Product()
        {
            InitializeComponent();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (txtName.Text == "" || txtBarcode.Text == "" || txtPrice.Text == "" || comboBox1.SelectedValue == null)
            {
                MessageBox.Show("برجاء اكمال البيانات المطلوبه اولا");
            }
            else
            {
                POSDemo.DB.Product product = new POSDemo.DB.Product();
                product.Name = txtName.Text;
                product.Code = txtBarcode.Text;
                product.Notes = txtNotes.Text;
                product.CategoryId = int.Parse(comboBox1.SelectedValue.ToString());

                int qty, price;
                int.TryParse(txtQty.Text, out qty);
                int.TryParse(txtPrice.Text, out price);
                product.Quantity = qty;
                product.Price = price;

                db.Products.Add(product);
                db.SaveChanges();



                if (imagePath != "")
                {
                    string newPath = Environment.CurrentDirectory + "\\images\\Products\\" + product.id + ".jpg";
                    File.Copy(imagePath, newPath);

                    product.Image = newPath;
                    db.SaveChanges();
                }
                MessageBox.Show("تم حفظ المنتج");

                txtBarcode.Text = "";
                txtName.Text = "";
                txtQty.Text = "";
                txtPrice.Text = "";
                txtNotes.Text = "";
                pictureBox1.ImageLocation = "";
            }

        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            OpenFileDialog dialog = new OpenFileDialog();
            if (dialog.ShowDialog() == DialogResult.OK)
            {
                imagePath = dialog.FileName;
                pictureBox1.ImageLocation = dialog.FileName;
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            ProductList p = new ProductList();
            p.Show();
        }

        private void Product_Load(object sender, EventArgs e)
        {
            // TODO: This line of code loads data into the 'pOSTutDataSet.Category' table. You can move, or remove it, as needed.
            this.categoryTableAdapter.Fill(this.pOSTutDataSet.Category);

        }
    }
}
