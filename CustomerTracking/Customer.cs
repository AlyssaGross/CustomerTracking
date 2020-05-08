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

        string _customerID;
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
       // List<Order> _orders;
       // List<Return> _returns;

        #region Constructor 
        public Customer(string customerID, string fname, string lname, DateTime birthday, string phoneNo, string email, DateTime? customerSince,
                        DateTime? lastPurchase, /*int points,*/ bool receiveTexts, bool receiveEmails, bool hasCard)
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

           // _customerList.Add(this);
           // _customerList.Sort((x, y) => x._lname.CompareTo(y._lname));
        }
        #endregion

        public void updatePoints(int points)
        {
            _points += points;
        }

        public void updateLastPurchase(DateTime purchaseDate) {

            if (purchaseDate > _lastPurchase) {
                _lastPurchase = purchaseDate;
            }
        }

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
            string customerLine, customerID, fname, lname, phoneNo, email;
            DateTime birthday;
            DateTime? customerSince, lastPurchase;
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
                    customerSince = null;
                }
                dateElems = custElems[7].Split('/');
                if (dateElems.Length == 3)
                {
                    lastPurchase = new DateTime(int.Parse(dateElems[2]), int.Parse(dateElems[0]), int.Parse(dateElems[1]));
                }
                else
                {
                    lastPurchase = null;
                }
               // points = int.Parse(custElems[8]);
                receiveTexts = bool.Parse(custElems[8]);
                receiveEmails = bool.Parse(custElems[9]);
                hasCard = bool.Parse(custElems[10]);
                //  Customer temp = new Customer(customerID, fname, lname, birthday, phoneNo, email, customerSince,
                //                                  lastPurchase, points, receiveTexts, receiveEmails, hasCard );
                _customerList.Add(new Customer(customerID, fname, lname, birthday, phoneNo, email, customerSince,
                                                lastPurchase, /*points,*/ receiveTexts, receiveEmails, hasCard));
            }
        }

   /*     public static void writeCustomersToFile(){
            string lineToWrite = "";
            StreamWriter customerFile = new StreamWriter("Customers.txt", false);
            foreach ( Customer c in _customerList){
                lineToWrite = c._fname + "," + c._lname + "/t"
                            + c._birthday.ToString("MM/dd/yyyy") + ","
                            + c._phoneNo + "," + c._email + ",";
                if(c._customerSince != null) {
                    lineToWrite += ((DateTime)c._customerSince).ToString("MM/dd/yyyy");
                }
                lineToWrite += ",";
                if (c._lastPurchase != null) {
                    lineToWrite += ((DateTime)c._lastPurchase).ToString("MM/dd/yyyy");
                }
                lineToWrite += "," + c._receiveTexts.ToString() + "," 
                            + c._receiveEmails.ToString() + "," + c._hasCard.ToString();

                customerFile.WriteLine(lineToWrite);
                Console.WriteLine(lineToWrite);
            }
        }*/

        public static List<Customer> CustomerList
        {
            get { return _customerList; }
        }

        public string CustomerID
        {
            get { return _customerID;  }
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