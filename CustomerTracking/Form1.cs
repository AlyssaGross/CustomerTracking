using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CustomerTracking
{
    public partial class CustomerTrackingForm : Form
    {
        public CustomerTrackingForm()
        {
            InitializeComponent();
            Customer.populateCustomerList();
        }

        private void CustomerTrackingForm_Load(object sender, EventArgs e)
        {
            CustomersListbox.DataSource = Customer.CustomerList;
            CustomersListbox.DisplayMember = "LastName";
            CustomersListbox.DisplayMember = "FirstName";
            CustomersListbox.DisplayMember = "Birthday";

        }

        private void AddCustomerButton_Click(object sender, EventArgs e)
        {
            //check that email and phoneNo do not exist
            string fname = FirstNameTextbox.Text;
            string lname = LastNameTextbox.Text;
            string[] dateArr = (BirthdayTextbox.Text).Split('/');
            if (dateArr.Length == 3){
                DateTime birthday = new DateTime(int.Parse(dateArr[2]), int.Parse(dateArr[0]), int.Parse(dateArr[1]));
            }
            string email = EmailTextbox.Text;
            string phoneNo = PhoneNumberTextbox.Text;
            dateArr = (CustomerSinceTextbox.Text).Split('/');
            DateTime customerSince = new DateTime(int.Parse(dateArr[2]), int.Parse(dateArr[0]), int.Parse(dateArr[1]));
            dateArr = (LastPurchaseTextbox.Text).Split('/');
            DateTime lastPurchase = new DateTime(int.Parse(dateArr[2]), int.Parse(dateArr[0]), int.Parse(dateArr[1]));
            int points = int.Parse(PointsTextbox.Text);
            bool receiveTexts = ReceiveTextsCheckbox.Checked;
            bool receiveEmails = ReceiveEmailsCheckbox.Checked;
            bool hasCard = HasCardCheckbox.Checked;

            Customer newCustomer = new Customer(fname, lname, birthday, phoneNo, email, customerSince, lastPurchase, points, receiveTexts, receiveEmails, hasCard);
        }
    }
}
