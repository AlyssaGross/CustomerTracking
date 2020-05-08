using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CustomerTracking
{

    class Order : Transaction
    {
        #region Static Field
        public static List<Order> OrderList = new List<Order> { };
        #endregion

        #region Private Fields
        private DateTime _shippedDate;
        private DateTime _arrivalDate;
        private string _orderFeedback;
        private List<OrderLine> _orderLines;
        #endregion

        #region Contructor
        public Order(string orderID, string customerID, DateTime purchaseDate,
                        DateTime shippedDate, DateTime recievedDate, string orderFeedback) : base(orderID, customerID, purchaseDate)
        {
            _shippedDate    = shippedDate;
            _arrivalDate    = recievedDate;
            _orderFeedback  = orderFeedback;
            _orderLines = new List<OrderLine> { };
        }
        #endregion

        #region Public Methods
        public override decimal getTotal()
        {

            decimal total = 0;

            foreach (OrderLine line in _orderLines)
            {
                total += line.getLineTotal();
            }

            return total;
        }

        public void updatePoints()
        {
            Customer customer = Customer.CustomerList.Where(c => c.CustomerID == _customerID).First();
            customer.updatePoints((int)(getTotal() / 10));
        }

        public void updateLastPurchase()
        {
            Customer customer = Customer.CustomerList.Where(c => c.CustomerID == _customerID).First();
            customer.updateLastPurchase(_transactionDate);
        }

        public void updateCustomerSince()
        {
            Customer customer = Customer.CustomerList.Where(c => c.CustomerID == _customerID).First();
            customer.updateCustomerSince(_transactionDate);
        }
        #endregion

        #region Properties
        public DateTime ShippedDate
        {
            get
            {
                return _shippedDate;
            }
        }
        public DateTime ArrivalDate
        {
            get
            {
                return _arrivalDate;
            }
        }
        public string OrderFeedback
        {
            get
            {
                return _orderFeedback;
            }
        }

        public List<OrderLine> OrderLines
        {
            get
            {
                return _orderLines;
            }
        }
        #endregion

        #region Static Methods
        static public void populateOrderList()
        {

            StreamReader orderFile = new StreamReader("Orders.txt");

            string[] orderElems, dateElems;
            string line, orderID, customerID, feedback, itemID;
            DateTime purchaseDate, shippedDate, arrivalDate;
            int lineNumber, quantity;
            decimal discount;
            Order tempOrder;

            line = orderFile.ReadLine();

            while (line != null)
            {

                orderElems = line.Split('\t');

                orderID = orderElems[0];
                customerID = orderElems[1];
                dateElems = orderElems[2].Split('/');
                if (dateElems.Length == 3) {
                    purchaseDate = new DateTime(int.Parse(dateElems[2]), int.Parse(dateElems[0]), int.Parse(dateElems[1]));
                }
                else {
                    purchaseDate = new DateTime(1, 1, 1);
                }

                dateElems = orderElems[3].Split('/');
                if (dateElems.Length == 3) {
                    shippedDate = new DateTime(int.Parse(dateElems[2]), int.Parse(dateElems[0]), int.Parse(dateElems[1]));
                }
                else {
                    shippedDate = new DateTime(1, 1, 1);
                }

                dateElems = orderElems[4].Split('/');
                if (dateElems.Length == 3) {
                    arrivalDate = new DateTime(int.Parse(dateElems[2]), int.Parse(dateElems[0]), int.Parse(dateElems[1]));
                }
                else {
                    arrivalDate = new DateTime(1, 1, 1);
                }

                feedback = orderElems[5];

                tempOrder = new Order(orderID, customerID, purchaseDate, shippedDate, arrivalDate, feedback);

                line = orderFile.ReadLine();
                while (line != null && line.Substring(0, 2) == "OL") {

                    orderElems = line.Split('\t');

                    lineNumber = int.Parse(orderElems[1]);
                    itemID = orderElems[2];
                    quantity = int.Parse(orderElems[3]);
                    discount = decimal.Parse(orderElems[4]);

                    tempOrder._orderLines.Add(new OrderLine(lineNumber, itemID, quantity, discount));

                    line = orderFile.ReadLine();
                }

                OrderList.Add(tempOrder);

                tempOrder.updatePoints();
                tempOrder.updateLastPurchase();
                tempOrder.updateCustomerSince();
            }
        }
        #endregion

    }
}
