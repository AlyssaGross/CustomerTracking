using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CustomerTracking
{
    abstract class Transaction
    {
        #region Protected Fields
        protected string _orderID;
        protected string _customerID;
        protected DateTime _transactionDate;
        #endregion

        #region Constructor
        public Transaction(string orderID, string customerID, DateTime transactionDate)
        {
            _orderID         = orderID;
            _customerID      = customerID;
            _transactionDate = transactionDate;
        }
        #endregion

        #region Public Method
        public abstract decimal getTotal();
        #endregion

        #region Properties
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
        #endregion

    }
}
