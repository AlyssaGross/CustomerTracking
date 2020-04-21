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

            string[] custElems, dateElems;
            string customerLine, fname, lname, phoneNo, email;
            DateTime birthday;
            DateTime? customerSince, lastPurchase;
            int points;
            bool receiveTexts, receiveEmails, hasCard;

            while ((customerLine = customerFile.ReadLine()) != null)
            {

                custElems = customerLine.Split(',');
                fname = custElems[0];
                lname = custElems[1];
                dateElems = custElems[2].Split('/');
                if (dateElems.Length == 3) {
                    birthday = new DateTime(int.Parse(dateElems[2]), int.Parse(dateElems[0]), int.Parse(dateElems[1]));
                }
                else{
                    birthday = new DateTime(1, 1, 1);
                }
                phoneNo = custElems[3];
                email = custElems[4];
                dateElems = custElems[5].Split('/');
                if (dateElems.Length == 3)
                {
                    customerSince = new DateTime(int.Parse(dateElems[2]), int.Parse(dateElems[0]), int.Parse(dateElems[1]));
                }
                else
                {
                    customerSince = null;
                }
                dateElems = custElems[6].Split('/');
                if (dateElems.Length == 3)
                {
                    lastPurchase = new DateTime(int.Parse(dateElems[2]), int.Parse(dateElems[0]), int.Parse(dateElems[1]));
                }
                else
                {
                    lastPurchase = null;
                }
                points = int.Parse(custElems[7]);
                receiveTexts = bool.Parse(custElems[8]);
                receiveEmails = bool.Parse(custElems[9]);
                hasCard = bool.Parse(custElems[10]);
                Customer temp = new Customer(fname, lname, birthday, phoneNo, email, customerSince,
                                                lastPurchase, points, receiveTexts, receiveEmails, hasCard );
            }
        }

        public static void writeCustomersToFile(){
            string lineToWrite = "";
            StreamWriter customerFile = new StreamWriter("Customers.txt", false);
            foreach ( Customer c in _customerList){
                lineToWrite = c._fname + "," + c._lname + ","
                            + c._birthday.ToString("MM/dd/yyyy") + ","
                            + c._phoneNo + "," + c._email + ",";
                if(c._customerSince != null) {
                    lineToWrite += ((DateTime)c._customerSince).ToString("MM/dd/yyyy");
                }
                lineToWrite += ",";
                if (c._lastPurchase != null) {
                    lineToWrite += ((DateTime)c._lastPurchase).ToString("MM/dd/yyyy");
                }
                lineToWrite += "," + c._points + "," + c._receiveTexts.ToString() + "," 
                            + c._receiveEmails.ToString() + "," + c._hasCard.ToString();
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