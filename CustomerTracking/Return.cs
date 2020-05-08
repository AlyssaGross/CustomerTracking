using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CustomerTracking
{
    class Return : Transaction
    {
        #region Static Fields
        public static List<Return> ReturnList = new List<Return> { };
        #endregion

        #region Private Fields
        private string _returnID;
        private List<ReturnLine> _returnLines;
        #endregion

        #region Constructor
        public Return(string returnID, string orderID, string customerID, DateTime returnDate) : base(orderID, customerID, returnDate)
        {
            _returnID = returnID;
            _returnLines = new List<ReturnLine> { };
        }
        #endregion
        
        #region Public Methods
        public override decimal getTotal(){

            decimal total = 0;

            foreach (ReturnLine line in _returnLines) {
                total += line.getLineTotal();
            }

            return total;
        }

        public void updatePoints()
        {
            Customer customer = Customer.CustomerList.Where(c => c.CustomerID == _customerID).First();
            customer.updatePoints((int)(-getTotal() / 10));
        }
        #endregion

        #region Properties
        public List<ReturnLine> ReturnLines
        {
            get
            {
                return _returnLines;
            }
        }
        #endregion

        #region Static Methods
        static public void populateReturnList()
        {

            StreamReader returnFile = new StreamReader("Returns.txt");

            string[] returnElems, dateElems;
            string line, returnID, orderID, customerID, feedback;
            DateTime returnDate;
            int lineNumber, orderLineNumber, quantity;
            Return tempReturn;
            OrderLine orderLine;

            line = returnFile.ReadLine();

            while (line != null)
            {

                returnElems = line.Split('\t');

                returnID = returnElems[0];
                orderID = returnElems[1];
                customerID = returnElems[2];
                dateElems = returnElems[3].Split('/');
                if (dateElems.Length == 3)
                {
                    returnDate = new DateTime(int.Parse(dateElems[2]), int.Parse(dateElems[0]), int.Parse(dateElems[1]));
                }
                else
                {
                    returnDate = new DateTime(1, 1, 1);
                }

                
                tempReturn = new Return(returnID, orderID, customerID, returnDate);

                line = returnFile.ReadLine();

                while (line != null && line.Substring(0, 2) == "RL")
                {

                    returnElems = line.Split('\t');

                    lineNumber = int.Parse(returnElems[1]);
                    orderLineNumber = int.Parse(returnElems[2]);
                    orderLine = Order.OrderList.Where(o => o.OrderID == orderID).First()
                                        .OrderLines.Where(l => l.LineNumber == orderLineNumber).First();
                    quantity = int.Parse(returnElems[3]);
                    feedback = returnElems[4];

                    tempReturn._returnLines.Add(new ReturnLine(lineNumber, orderLine, quantity, feedback));

                    line = returnFile.ReadLine();
                }

                ReturnList.Add(tempReturn);
                tempReturn.updatePoints();
            }
        }
        #endregion

    }
}
