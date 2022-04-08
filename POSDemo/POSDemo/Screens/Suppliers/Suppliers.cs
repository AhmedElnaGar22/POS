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

namespace POSDemo.Screens.Suppliers
{
    public partial class Suppliers : Form
    {
        POSTutEntities db = new POSTutEntities();
        string imagePath = "";
        int Id;
        POSDemo.DB.Supplier result;
        public Suppliers()
        {
            InitializeComponent();
            checkBox1.Checked = false;
            dataGridView1.DataSource = db.Suppliers.ToList();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            dataGridView1.DataSource = db.Suppliers.ToList();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (txtName.Text == "")
            {
                dataGridView1.DataSource = db.Suppliers.Where(x => x.phone.Contains(txtPhone.Text)).ToList();
            }
            else
            {
                dataGridView1.DataSource = db.Suppliers.Where(x => x.phone.Contains(txtPhone.Text) || x.Name.Contains(txtName.Text)).ToList();
            }
        }

        private void dataGridView1_SelectionChanged(object sender, EventArgs e)
        {
            try
            {
                Id = int.Parse(dataGridView1.CurrentRow.Cells[0].Value.ToString());

                result = db.Suppliers.SingleOrDefault(x => x.Id == Id);

                txtFormName.Text = result.Name;
                txtFormPhone.Text = result.phone;
                txtMail.Text = result.email;
                txtAddress.Text = result.address;
                txtNotes.Text = result.Notes;
                txtCompany.Text = result.Company;
                if (result.isActive == null)
                {
                    result.isActive = false;
                }
                checkBox1.Checked = (bool)result.isActive;
                pictureBox1.ImageLocation = result.Image;
            }
            catch
            {

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

        private void button5_Click(object sender, EventArgs e)
        {
            var r = MessageBox.Show("اضافه عميل جديد", "هل انت متأكد من اضافه هذا العميل", MessageBoxButtons.YesNo);
            if (r == DialogResult.Yes)
            {
                if (txtFormName.Text == "" || txtFormPhone.Text == "")
                {
                    MessageBox.Show("برجاء اكمال البيانات المطلوبه اولا");
                }
                else if (db.Suppliers.Where(x => x.phone == txtFormPhone.Text).Count() > 0)
                {
                    MessageBox.Show("هذا المورد موجود مسبقاً");
                }
                else
                {
                    Supplier s = new Supplier();
                    s.Name = txtFormName.Text;
                    s.phone = txtFormPhone.Text;
                    s.Notes = txtNotes.Text;
                    s.email = txtMail.Text;
                    s.address = txtAddress.Text;
                    s.Company = txtCompany.Text;
                    s.isActive = checkBox1.Checked;

                    db.Suppliers.Add(s);
                    db.SaveChanges();



                    if (imagePath != "")
                    {
                        string newPath = Environment.CurrentDirectory + "\\images\\Suppliers\\" + s.Id + ".jpg";
                        File.Copy(imagePath, newPath);

                        s.Image = newPath;
                        db.SaveChanges();
                    }
                    MessageBox.Show("تم حفظ العميل");

                    txtFormName.Text = "";
                    txtFormPhone.Text = "";
                    txtNotes.Text = "";
                    txtMail.Text = "";
                    txtAddress.Text = "";
                    txtCompany.Text = "";
                    pictureBox1.ImageLocation = "";
                    checkBox1.Checked = false;

                    dataGridView1.DataSource = db.Suppliers.ToList();
                }
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            var r = MessageBox.Show("هل تريد حفظ التعديل ؟", "تأكيد التعديل", MessageBoxButtons.YesNo);
            if (db.Suppliers.Where(x => x.phone == txtFormPhone.Text && x.Id != Id).Count() > 0)
            {
                MessageBox.Show("هذا المورد موجود مسبقاً");
                return;
            }
            if (r == DialogResult.Yes)
            {
                result.Name = txtFormName.Text;
                result.phone = txtFormPhone.Text;
                result.email = txtMail.Text;
                result.address = txtAddress.Text;
                result.Notes = txtNotes.Text;
                result.Company = txtCompany.Text;
                result.isActive = checkBox1.Checked;
                if (imagePath != "")
                {
                    string newPath = Environment.CurrentDirectory + "\\images\\Customers\\" + result.Id + ".jpg";
                    File.Copy(imagePath, newPath, true);

                    result.Image = newPath;
                    db.SaveChanges();
                }
                db.SaveChanges();
                MessageBox.Show("تم الحفظ");

                dataGridView1.DataSource = db.Suppliers.ToList();
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            var r = MessageBox.Show("هل تريد الحذف", "تأكيد الحذف", MessageBoxButtons.YesNo);
            if (r == DialogResult.Yes)
            {
                var result = db.Suppliers.Find(Id);
                db.Suppliers.Remove(result);
                db.SaveChanges();
                MessageBox.Show("تم الحذف");

                dataGridView1.DataSource = db.Suppliers.ToList();
            }
        }
    }
}
