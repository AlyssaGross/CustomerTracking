using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CustomerTracking
{
    class ReturnLine
    {
        int       _returnLine;
        OrderLine _orderLine;
        int       _quantity;
        string    _reasonForReturn;

        public ReturnLine(int returnLine, OrderLine orderLine, int quantity, string reasonForReturn) {

            _returnLine      = returnLine;
            _orderLine       = orderLine;
            _quantity        = quantity;
            _reasonForReturn = reasonForReturn;
        }

        public decimal getLineTotal() {
        
            return (_orderLine.getItemTotal() * _quantity);
        }

        public override string ToString() {

            Item item = Item.ItemList.Where(i => i.ItemID == _orderLine.LineItemID).First();
            return _returnLine.ToString().PadRight(3) + " " + item.ToString() + " x" +
                         _quantity.ToString().PadRight(2) +  " @ " + getLineTotal().ToString().PadLeft(6);
        }

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

    }
}
