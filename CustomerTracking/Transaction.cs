using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CustomerTracking
{
    abstract class Transaction
    {
        protected string _orderID;
        protected string _customerID;
        protected DateTime _transactionDate;

        public Transaction(string orderID, string customerID, DateTime transactionDate)
        {
            _orderID         = orderID;
            _customerID      = customerID;
            _transactionDate = transactionDate;
        }

        public abstract decimal getTotal();

        public abstract void print();

        public string OrderID
        {
            get
            {
                return _orderID;
            }
        }

        public string CustomerID
        {
            get
            {
                return _customerID;
            }
        }

        public DateTime TransactionDate
        {
            get
            {
                return _transactionDate;
            }
        }

    }
}
