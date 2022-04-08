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
    public partial class SalesBillListData : Form
    {
        POSTutEntities db = new POSTutEntities();
        public SalesBillListData()
        {
            InitializeComponent();

            dataGridView2.DataSource = db.SalesBills.Select(x => new { x.Id, x.Total, x.User.UserName, x.Customer.Name, x.Date }).ToList();
        }

        private void dataGridView2_SelectionChanged(object sender, EventArgs e)
        {
            int id = int.Parse(dataGridView2.CurrentRow.Cells[0].Value.ToString());
            var bill = db.SalesBills.FirstOrDefault(x => x.Id == id);
            txtNumber.Text = bill.Id.ToString();
            txtNotes.Text = bill.Notes;
            lblDiscount.Text = bill.TotalAfterDiscount.ToString();
            lblTotal.Text = bill.Total.ToString();
            dateTimePicker1.Value = (DateTime)bill.Date;
            txtdiscount.Text = bill.discount.ToString();

            dataGridView1.Rows.Clear();

            foreach (var item in bill.SalesBillDetails)
            {
                dataGridView1.Rows.Add(item.Productid, item.Product.Name, item.Price, item.quantity, item.totalPrice, item.Product.Image == null ? new Bitmap(40,40) : Image.FromFile(item.Product.Image));
            }
        }
    }
}
