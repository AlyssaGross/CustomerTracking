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
        #region Static Fields
        static List<Customer> _customerList = new List<Customer> { };
        #endregion

        #region Private Fields
        private string _customerID;
        private string _fname;
        private string _lname;
        private DateTime _birthday;
        private string _phoneNo;
        private string _email;
        private DateTime _customerSince;
        private DateTime _lastPurchase;
        private int _points;
        private bool _receiveTexts;
        private bool _receiveEmails;
        private bool _hasCard;
        #endregion

        #region Constructor 
        public Customer(string customerID, string fname, string lname, DateTime birthday, string phoneNo, string email, DateTime customerSince,
                        DateTime lastPurchase, bool receiveTexts, bool receiveEmails, bool hasCard)
        {
            _customerID = customerID;
            _fname         = fname;
            _lname         = lname;
            _birthday      = birthday;
            _phoneNo       = phoneNo;
            _email         = email;
            _customerSince = customerSince;
            _lastPurchase  = lastPurchase;
            _points        = 0;
            _receiveTexts  = receiveTexts;
            _receiveEmails = receiveEmails;
            _hasCard       = hasCard;
        }
        #endregion

        #region Public Methods
        public void updatePoints(int points)
        {
            _points += points;
        }

        public void updateLastPurchase(DateTime purchaseDate) {

            if (purchaseDate > _lastPurchase) {
                _lastPurchase = purchaseDate;
            }
        }

        public void updateCustomerSince(DateTime purchaseDate) {

            if (purchaseDate < _customerSince)
            {
                _customerSince = purchaseDate;
            }
        }

        public int getAge()
        {
            int age;

            age = (DateTime.Today - _birthday).Days / 365;

            return age;
        }
        #endregion

        #region Properties
        public static List<Customer> CustomerList
        {
            get { return _customerList; }
        }

        public string CustomerID
        {
            get { return _customerID; }
        }

        public string LastName
        {
            get { return _lname; }
        }

        public string FirstName
        {
            get { return _fname; }
        }

        public DateTime Birthday
        {
            get { return _birthday; }
        }

        public string PhoneNumber
        {
            get { return _phoneNo; }
        }

        public string Email
        {
            get { return _email; }
        }

        public DateTime CustomerSince
        {
            get { return _customerSince; }
        }

        public DateTime LastPurchase
        {
            get { return _lastPurchase; }
        }

        public int Points
        {
            get { return _points; }
        }

        public bool ReceiveTexts
        {
            get { return _receiveTexts; }
        }

        public bool ReceiveEmails
        {
            get { return _receiveEmails; }
        }

        public bool HasCard
        {
            get { return _hasCard; }
        }
        #endregion

        #region Static Methods
        static public void populateCustomerList()
        {

            StreamReader customerFile = new StreamReader("Customers.txt");

            string[] custElems, dateElems;
            string customerLine, customerID, fname, lname, phoneNo, email;
            DateTime birthday, customerSince, lastPurchase;
            int points;
            bool receiveTexts, receiveEmails, hasCard;

            while ((customerLine = customerFile.ReadLine()) != null)
            {

                custElems = customerLine.Split('\t');
                customerID = custElems[0];
                fname = custElems[1];
                lname = custElems[2];
                dateElems = custElems[3].Split('/');
                if (dateElems.Length == 3) {
                    birthday = new DateTime(int.Parse(dateElems[2]), int.Parse(dateElems[0]), int.Parse(dateElems[1]));
                }
                else{
                    birthday = new DateTime(1, 1, 1);
                }
                phoneNo = custElems[4];
                email = custElems[5];
                dateElems = custElems[6].Split('/');
                if (dateElems.Length == 3)
                {
                    customerSince = new DateTime(int.Parse(dateElems[2]), int.Parse(dateElems[0]), int.Parse(dateElems[1]));
                }
                else
                {
                    customerSince = new DateTime(1, 1, 1);
                }
                dateElems = custElems[7].Split('/');
                if (dateElems.Length == 3)
                {
                    lastPurchase = new DateTime(int.Parse(dateElems[2]), int.Parse(dateElems[0]), int.Parse(dateElems[1]));
                }
                else
                {
                    lastPurchase = new DateTime(1, 1, 1);
                }
                receiveTexts = bool.Parse(custElems[8]);
                receiveEmails = bool.Parse(custElems[9]);
                hasCard = bool.Parse(custElems[10]);

                _customerList.Add(new Customer(customerID, fname, lname, birthday, phoneNo, email, customerSince,
                                                lastPurchase, receiveTexts, receiveEmails, hasCard));
            }

            _customerList = _customerList.OrderBy(c => c.LastName).ToList();
        }
        #endregion
    }
}