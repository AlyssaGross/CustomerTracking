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
            Console.WriteLine(Customer.CustomerList[0]);
            CustomersListbox.DataSource = Customer.CustomerList;
            CustomersListbox.DisplayMember = "LastName";
            CustomersListbox.DisplayMember = "FirstName";
            CustomersListbox.DisplayMember = "Birthday";
        }

        public void CustomerTrackingForm_Closing(){
            //Customer.
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
            DateTime birthday;
            if (dateArr.Length == 3){
                birthday = new DateTime(int.Parse(dateArr[2]), int.Parse(dateArr[0]), int.Parse(dateArr[1]));
            }
            else{
                birthday = new DateTime(1, 1, 1);
               //some sort of error because it needs to be assigned
            }
            string email = EmailTextbox.Text;
            string phoneNo = PhoneNumberTextbox.Text;
            dateArr = (CustomerSinceTextbox.Text).Split('/');
            DateTime? customerSince;
            if (dateArr.Length == 3) {
                customerSince = new DateTime(int.Parse(dateArr[2]), int.Parse(dateArr[0]), int.Parse(dateArr[1]));
            }
            else {
                customerSince = null;
            }
            dateArr = (LastPurchaseTextbox.Text).Split('/');
            DateTime? lastPurchase;
            if (dateArr.Length == 3) {
                lastPurchase = new DateTime(int.Parse(dateArr[2]), int.Parse(dateArr[0]), int.Parse(dateArr[1]));
            }
            else {
                lastPurchase = null;
            }
            int points;
            if (int.TryParse(PointsTextbox.Text, out points) == false ) {
                points = 0;
            }
            bool receiveTexts = ReceiveTextsCheckbox.Checked;
            bool receiveEmails = ReceiveEmailsCheckbox.Checked;
            bool hasCard = HasCardCheckbox.Checked;

            Customer newCustomer = new Customer(fname, lname, birthday, phoneNo, email, customerSince, lastPurchase, points, receiveTexts, receiveEmails, hasCard);
        }
    }
}
