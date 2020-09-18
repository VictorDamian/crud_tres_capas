using System;
using System.Windows.Forms;
using Domain.Models;
using Domain.ValuesObjects;

namespace Presentacion.Forms
{
    public partial class FormEmployee : Form
    {
        private EmployeeModel employee = new EmployeeModel();
        public FormEmployee()
        {
            InitializeComponent();
            panel1.Enabled = false;
        }

        private void FormEmployee_Load(object sender, EventArgs e)
        {
            ListEmployee();
        }
        private void ListEmployee()
        {
            try
            {
                dataGridEmp.DataSource = employee.getAll();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void txtSearch_TextChanged(object sender, EventArgs e)
        {
            dataGridEmp.DataSource = employee.FindById(txtSearch.Text);
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            employee.IdNumber = txtINumber.Text;
            employee.Name = txtName.Text;
            employee.Mail = txtMail.Text;
            employee.Birthday = txtDate.Value;

            bool valid = new Helps.DataValidation(employee).Validate();
            if (valid==true)
            {
                string result = employee.saveChange();
                MessageBox.Show(result);
                ListEmployee();
                Restar();
            }
        }

        private void Restar()
        {
            panel1.Enabled = false;
            txtINumber.Clear();
            txtMail.Clear();
            txtName.Clear();
        }

        private void restarText()
        {
            txtINumber.Clear();
            txtMail.Clear();
            txtName.Clear();
        }

        private void btnNew_Click(object sender, EventArgs e)
        {
            panel1.Enabled = true;
            employee.State = EntityState.Added;
            restarText();
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            if (dataGridEmp.SelectedRows.Count > 0)
            {
                panel1.Enabled = true;
                employee.State = EntityState.Modified;
                //el id no puede ser editado, no es necesario pasar a txt
                //pasar datos al txt
                employee.IdPk = Convert.ToInt32(dataGridEmp.CurrentRow.Cells[0].Value);
                txtINumber.Text = dataGridEmp.CurrentRow.Cells[1].Value.ToString();
                txtName.Text = dataGridEmp.CurrentRow.Cells[2].Value.ToString();
                txtMail.Text = dataGridEmp.CurrentRow.Cells[3].Value.ToString();
                txtDate.Value = Convert.ToDateTime(dataGridEmp.CurrentRow.Cells[4].Value);
            }
            else MessageBox.Show("Select a Row");

        }

        private void btnRemove_Click(object sender, EventArgs e)
        {
            if (dataGridEmp.SelectedRows.Count > 0)
            {
                employee.State = EntityState.Deleted;
                employee.IdPk = Convert.ToInt32(dataGridEmp.CurrentRow.Cells[0].Value);
                string result = employee.saveChange();
                MessageBox.Show(result);
                ListEmployee();
            }
            else
                MessageBox.Show("Select a row");
        }
    }
}
