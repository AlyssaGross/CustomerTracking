using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CustomerTracking
{
    class ReturnLine
    {
        #region Private Fields
        private int       _returnLine;
        private OrderLine _orderLine;
        private int       _quantity;
        private string    _reasonForReturn;
        #endregion

        #region Constructor
        public ReturnLine(int returnLine, OrderLine orderLine, int quantity, string reasonForReturn) {

            _returnLine      = returnLine;
            _orderLine       = orderLine;
            _quantity        = quantity;
            _reasonForReturn = reasonForReturn;
        }
        #endregion

        #region Public Methods
        public decimal getLineTotal() {
        
            return (_orderLine.getItemTotal() * _quantity);
        }

        public override string ToString() {

            Item item = Item.ItemList.Where(i => i.ItemID == _orderLine.LineItemID).First();
            return _returnLine.ToString().PadRight(3) + " " + item.ToString() + " x" +
                         _quantity.ToString().PadRight(2) +  " @ " + getLineTotal().ToString().PadLeft(6);
        }
        #endregion

        #region Properties
        public string ItemName
        {
            get
            {
                return Item.ItemList.Where(i => i.ItemID == _orderLine.LineItemID).First().ItemName;
            }
        }

        public decimal ItemPricePaid
        {
            get
            {
                return _orderLine.getItemTotal();
            }
        }

        public int Quantity
        {
            get
            {
                return _quantity;
            }
        }

        public decimal Refund
        {
            get
            {
                return getLineTotal();
            }
        }

        public string ReasonForReturn
        {
            get
            {
                return _reasonForReturn;
            }
        }
        #endregion

    }
}
