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
    public partial class ProductList : Form
    {
        POSTutEntities db = new POSTutEntities();
        int Id = 0;
        POSDemo.DB.Product result;
        string imagePath ="";
        public ProductList()
        {
            InitializeComponent();

            comboBox1.DataSource = db.Categories.ToList();
            comboBox2.DataSource = db.Categories.ToList();

            dataGridView1.DataSource = db.Products.OrderBy(x => x.Price).ToList();

            //MessageBox.Show(db.Products.Max(x => x.Price).ToString());
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (txtName.Text == "")
            {
                dataGridView1.DataSource = db.Products.Where(x => x.Code == txtBarcode.Text).ToList();
            }
            else
            {
                dataGridView1.DataSource = db.Products.Where(x => x.Code == txtBarcode.Text || x.Name.Contains(txtName.Text)).ToList();
            }

        }

        private void button2_Click(object sender, EventArgs e)
        {
            dataGridView1.DataSource = db.Products.ToList();
        }

        private void dataGridView1_SelectionChanged(object sender, EventArgs e)
        {
            try
            {
                Id = int.Parse(dataGridView1.CurrentRow.Cells[0].Value.ToString());

                result = db.Products.SingleOrDefault(x => x.id == Id);

                txtFormBarcode.Text = result.Code;
                txtFormName.Text = result.Name;
                txtPrice.Text = result.Price.ToString();
                txtQty.Text = result.Quantity.ToString();
                txtNotes.Text = result.Notes;
                pictureBox1.ImageLocation = result.Image;
                comboBox1.SelectedValue = result.CategoryId;
            }
            catch
            {

            }
 
        }

        private void button3_Click(object sender, EventArgs e)
        {

            result.Name = txtFormName.Text;
            result.Code = txtFormBarcode.Text;
            result.Price = decimal.Parse(txtPrice.Text);
            result.Quantity = int.Parse(txtQty.Text);
            result.Notes = txtNotes.Text;
            result.CategoryId = int.Parse(comboBox1.SelectedValue.ToString());

            if (imagePath != "")
            {
                string newPath = Environment.CurrentDirectory + "\\images\\Products\\" + result.id + ".jpg";
                File.Copy(imagePath, newPath,true);

                result.Image = newPath;
                db.SaveChanges();
            }
            db.SaveChanges();
            MessageBox.Show("تم الحفظ");

            dataGridView1.DataSource = db.Products.ToList();

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

        private void button4_Click(object sender, EventArgs e)
        {
            var r = MessageBox.Show("هل تريد الحذف", "تأكيد الحذف", MessageBoxButtons.YesNo);
            if (r == DialogResult.Yes)
            {
                var result = db.Products.Find(Id);
                db.Products.Remove(result);
                db.SaveChanges();
                MessageBox.Show("تم الحذف");

                dataGridView1.DataSource = db.Products.ToList();
            }

        }

        private void button5_Click(object sender, EventArgs e)
        {
            Product p = new Product();
            p.Show();
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void ProductList_Load(object sender, EventArgs e)
        {
            // TODO: This line of code loads data into the 'pOSTutDataSet1.Category' table. You can move, or remove it, as needed.
            this.categoryTableAdapter.Fill(this.pOSTutDataSet1.Category);

        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            int catid = int.Parse(comboBox2.SelectedValue.ToString());
            dataGridView1.DataSource = db.Products.Where(x => x.CategoryId == catid).ToList();
        }
    }
}
