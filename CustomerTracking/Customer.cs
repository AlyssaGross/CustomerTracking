using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace CustomerTracking
{
    class Customer
    {
        //static int numberCustomers=0;
        static List<Customer> _customerList = new List<Customer> { };

        string _fname;
        string _lname;
        DateTime _birthday;
        string _phoneNo;
        string _email;
        DateTime? _customerSince;
        DateTime? _lastPurchase;
        int _points;
        bool _receiveTexts;
        bool _receiveEmails;
        bool _hasCard;

        #region Constructor 
        public Customer(string fname, string lname, DateTime birthday, string phoneNo, string email, DateTime? customerSince,
                        DateTime? lastPurchase, int points, bool receiveTexts, bool receiveEmails, bool hasCard)
        {
            _fname = fname;
            _lname = lname;
            _birthday = birthday;
            _phoneNo = phoneNo;
            _email = email;
            _customerSince = customerSince;
            _lastPurchase = lastPurchase;
            _points = points;
            _receiveTexts = receiveTexts;
            _receiveEmails = receiveEmails;
            _hasCard = hasCard;

            _customerList.Add(this);
            _customerList.Sort((x, y) => x._lname.CompareTo(y._lname));
        }
        #endregion


        public int getAge()
        {
            int age;

            age = (DateTime.Today - _birthday).Days / 365;

            return age;
        }

        static public void populateCustomerList()
        {

            StreamReader customerFile = new StreamReader("Customers.txt");
            string customerLine = "";
            string[] customerElements;

            string fname, lname, phoneNo, email;
            string[] birthdayElements;
            int day, month, year;

            while ((customerLine = customerFile.ReadLine()) != null)
            {

                customerElements = customerLine.Split(',');
                birthdayElements = customerElements[2].Split('/');
                fname = customerElements[0];
                lname = customerElements[1];
                month = int.Parse(birthdayElements[0]);
                day = int.Parse(birthdayElements[1]);
                year = int.Parse(birthdayElements[2]);
                phoneNo = customerElements[3];
                email = customerElements[4];
                //Customer temp = new Customer(fname, lname, new DateTime(year, month, day), phoneNo, email);

            }
        }

        public static List<Customer> CustomerList
        {
            get { return _customerList; }
        }

        public string FirstName {
            get { return _fname; }
        }

        public string LastName {
            get { return _lname; }
        }

        public DateTime? Birthday {
            get { return _birthday; }
        }

        public string PhoneNumber {
            get { return _phoneNo; }
        }

        public string Email {
            get { return _email; }
        }

        public DateTime? CustomerSince
        {
            get { return _customerSince; }
        }

        public DateTime? LastPurchase
        {
            get { return _lastPurchase; }
        }

        public int Points {
            get { return _points; }
        }

        public bool ReceiveTexts {
            get { return _receiveTexts; }
        }

        public bool ReceiveEmails {
            get { return _receiveEmails; }
        }    

        public bool HasCard {
            get { return _hasCard; }
        }    
    }
}