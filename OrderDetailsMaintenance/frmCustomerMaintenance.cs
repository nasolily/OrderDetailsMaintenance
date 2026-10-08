using Microsoft.Data.SqlClient;
using OrderDetailsMaintenance.Models.DataLayer;


namespace OrderDetailsMaintenance
{
    // Nadia Cowins
    public partial class frmCustomerMaintenance : Form
    {
        public frmCustomerMaintenance()
        {
            InitializeComponent();
        }

        // Nadia Cowins
        private void btnFind_Click(object sender, EventArgs e)
        {
            string customerID = txtCustomerId.Text.Trim();

            if (string.IsNullOrWhiteSpace(customerID))// check empty customerID
            {
                MessageBox.Show("Please enter a Customer ID.");
                return;
            }

            // Clear previous customer information
            txtContact.Clear();
            txtAddress.Clear();
            txtCity.Clear();
            txtCountry.Clear();

            using (NorthwindContext db = new NorthwindContext())
            {
                //Customer? customer = db.Customers.Find(customerID);
                try
                {
                    var customer = db.Customers.Find(customerID);
                    if (customer != null)
                    {
                        txtContact.Text = customer.ContactName;
                        txtAddress.Text = customer.Address;
                        txtCity.Text = customer.City;
                        txtCountry.Text = customer.Country;
                    }
                    else
                    {
                        MessageBox.Show("Customer not found.");
                    }
                }
                catch (SqlException ex)
                {
                    MessageBox.Show("Database error: " + ex.Message);
                }

            }
        }

        // Nadia Cowins
        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        // Nadia Cowins (ACCIDENTAL DOUBLE CLICK)
        private void frmCustomerMaintenance_Load(object sender, EventArgs e)
        {

        }

        // Nadia Cowins
        private void btnSave_Click(object sender, EventArgs e)
        {
            string customerID = txtCustomerId.Text.Trim();

            if (string.IsNullOrWhiteSpace(customerID))
            {
                MessageBox.Show("Please enter a Customer ID.");
                return;
            }

            try
            {
                using (NorthwindContext db = new NorthwindContext())
                {
                    Customer? customer = db.Customers.Find(customerID);

                    if (customer != null)
                    {
                        customer.ContactName = txtContact.Text;
                        customer.Address = txtAddress.Text;
                        customer.City = txtCity.Text;
                        customer.Country = txtCountry.Text;

                        db.Customers.Update(customer);
                        db.SaveChanges();

                        MessageBox.Show("Customer updated successfully!");
                    }
                    else
                    {
                        MessageBox.Show("Customer not found.");
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error saving customer: " + ex.Message);
            }
        }
    }
}