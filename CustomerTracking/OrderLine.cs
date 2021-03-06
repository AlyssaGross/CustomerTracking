﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CustomerTracking
{
    public class OrderLine
    {
        #region Private Fields
        private int _number;
        private string _itemID;
        private int _quantity;
        private decimal _discount;
        #endregion

        #region Constructor
        public OrderLine(int number, string itemID, int quantity, decimal discount)
        {
            _number = number;
            _itemID = itemID;
            _quantity = quantity;
            _discount = discount;
        }
        #endregion

        #region Public Method
        public decimal getItemTotal()
        {
            Item item = Item.ItemList.Where(i => i.ItemID == _itemID).First();
            return item.ItemPrice - _discount;
        }

        public decimal getLineTotal()
        {

            return getItemTotal() * (decimal)_quantity;
        }

        public override string ToString()
        {
            Item item = Item.ItemList.Where(i => i.ItemID == _itemID).First();
            return _number.ToString().PadRight(3) + " " + item.ToString() + " x" +
                         _quantity.ToString().PadRight(2) + "  @ (" + item.ItemPrice.ToString().PadLeft(5) +
                         " - " + _discount.ToString().PadLeft(5) + ")   " + getLineTotal().ToString().PadLeft(6);
        }
        #endregion

        #region Properties
        public int LineNumber
        {
            get
            {
                return _number;
            }
        }
        public string LineItemID
        {
            get
            {
                return _itemID;
            }
        }

        public string ItemName
        {
            get
            {
                return Item.ItemList.Where(i => i.ItemID == _itemID).First().ItemName;
            }
        }

        public decimal ItemPrice
        {
            get
            {
                return Item.ItemList.Where(i => i.ItemID == _itemID).First().ItemPrice;
            }
        }

        public decimal ItemDiscount
        {
            get
            {
                return _discount;
            }
        }

        public int ItemQuantity
        {
            get
            {
                return _quantity;
            }
        }
        
        public decimal LineTotal
        {
            get
            {
                return getLineTotal();
            }
        }
        #endregion
    }
}
