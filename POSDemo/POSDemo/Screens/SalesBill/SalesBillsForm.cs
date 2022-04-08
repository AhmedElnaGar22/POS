using POSDemo.DB;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace POSDemo.Screens.SalesBill
{
    public partial class SalesBillsForm : Form
    {
        POSTutEntities db = new POSTutEntities();
        List<POSDemo.DB.Product> products;
        public SalesBillsForm()
        {
            InitializeComponent();

            comboBox1.DataSource = db.Customers.Where(x => x.isActive == true).ToList();
            comboBox1.ValueMember = "Id";
            comboBox1.DisplayMember = "Name";

            products = db.Products.ToList();
            imageList1.ImageSize = new Size(70, 70);

            lblUser.Text = POSDemo.Users.Name;
        }

        private void SalesBillsForm_Load(object sender, EventArgs e)
        {
            for (int i = 0; i < products.Count(); i++)
            {
                if (products[i].Image != null)
                {
                    imageList1.Images.Add(Image.FromFile(products[i].Image));
                }
                else
                {
                    Bitmap btm = new Bitmap(70, 70);
                    imageList1.Images.Add(btm);
                }

                ListViewItem item = new ListViewItem();
                item.Text = products[i].Name;
                item.ImageIndex = i;
                item.Tag = products[i];

                listView1.Items.Add(item);
            }
        }

        void CalculateTotal()
        {
            try 
            { 
                decimal total = 0;

                for (int i = 0; i < dataGridView1.RowCount; i++)
                {
                    total += decimal.Parse(dataGridView1.Rows[i].Cells["totalprice"].Value.ToString());
                }

                lblTotal.Text = total.ToString();

                decimal disc = decimal.Parse(txtdiscount.Text);

                lblDiscount.Text = (total - disc).ToString();
            }
            catch { }

        }

        private void listView1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (listView1.SelectedItems.Count > 0)
            {
                var product = (POSDemo.DB.Product)listView1.SelectedItems[0].Tag;

                for (int i = 0; i < dataGridView1.RowCount; i++)
                {
                    if (dataGridView1.Rows[i].Cells["id"].Value.ToString() == product.id.ToString())
                    {
                        dataGridView1.Rows[i].Cells["quantity"].Value = int.Parse(dataGridView1.Rows[i].Cells["quantity"].Value.ToString()) + 1;
                        dataGridView1.Rows[i].Cells["totalprice"].Value = int.Parse(dataGridView1.Rows[i].Cells["quantity"].Value.ToString()) * decimal.Parse(dataGridView1.Rows[i].Cells["price"].Value.ToString());
                        CalculateTotal();
                        return;
                    }
                }

                dataGridView1.Rows.Add(product.id, product.Name, product.Price, 1, product.Price * 1, product.Image == null ? new Bitmap(40,40) : Image.FromFile(product.Image));
                CalculateTotal();
            }
        }

        private void txtdiscount_TextChanged(object sender, EventArgs e)
        {
            CalculateTotal();
        }

        private void txtdiscount_KeyPress(object sender, KeyPressEventArgs e)
        {

        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            POSDemo.DB.SalesBill result = new POSDemo.DB.SalesBill()
            {
                Date = (DateTime)dateTimePicker1.Value.Date,
                discount = decimal.Parse(txtdiscount.Text),
                Total = decimal.Parse(lblTotal.Text),
                TotalAfterDiscount = decimal.Parse(lblDiscount.Text),
                Notes = txtNotes.Text,
                Customerid = int.Parse(comboBox1.SelectedValue.ToString()),
                UserId = POSDemo.Users.id
            };

            List<SalesBillDetail> list = new List<SalesBillDetail>();
            for (int i = 0; i < dataGridView1.RowCount; i++)
            {
                list.Add(new SalesBillDetail
                {
                    Productid = int.Parse(dataGridView1.Rows[i].Cells["id"].Value.ToString()),
                    quantity = int.Parse(dataGridView1.Rows[i].Cells["quantity"].Value.ToString()),
                    Price = decimal.Parse(dataGridView1.Rows[i].Cells["price"].Value.ToString()),
                    totalPrice = int.Parse(dataGridView1.Rows[i].Cells["quantity"].Value.ToString()) * decimal.Parse(dataGridView1.Rows[i].Cells["price"].Value.ToString())
                });
            }

            result.SalesBillDetails = list;

            db.SalesBills.Add(result);
            db.SaveChanges();

            MessageBox.Show("تم الحفظ" + result.Id);
        }

        private void txtQty_TextChanged(object sender, EventArgs e)
        {

        }

        private void label7_Click(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void dateTimePicker1_ValueChanged(object sender, EventArgs e)
        {

        }

        private void txtNotes_TextChanged(object sender, EventArgs e)
        {

        }

        private void label6_Click(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        private void lblUser_Click(object sender, EventArgs e)
        {

        }

        private void groupBox2_Enter(object sender, EventArgs e)
        {

        }

        private void groupBox3_Enter(object sender, EventArgs e)
        {

        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void label5_Click(object sender, EventArgs e)
        {

        }

        private void lblTotal_Click(object sender, EventArgs e)
        {

        }

        private void lblDiscount_Click(object sender, EventArgs e)
        {

        }

        private void groupBox4_Enter(object sender, EventArgs e)
        {

        }

        private void dataGridView1_RowsRemoved(object sender, DataGridViewRowsRemovedEventArgs e)
        {
            CalculateTotal();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            SalesBillListData frm = new SalesBillListData();
            frm.Show();
        }
    }
}
