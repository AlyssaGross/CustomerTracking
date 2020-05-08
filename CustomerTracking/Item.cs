using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CustomerTracking {
    public class Item {

        #region Static Fields
        public static List<Item> ItemList = new List<Item> { };
        #endregion

        #region Private Fields
        private string _itemID;
        private string _itemName;
        private decimal _itemPrice;
        #endregion

        #region Constructor
        public Item(string itemID, string itemName, decimal itemPrice) {
            _itemID = itemID;
            _itemName = itemName;
            _itemPrice = itemPrice;
        }
        #endregion

        #region Public Methods
        public override string ToString()
        {

            return _itemID.PadRight(7) + " " + _itemName.PadRight(15);
        }
        #endregion

        #region Properties
        public string ItemID {
            get {
                return _itemID;
            }
        }

        public string ItemName {
            get {
                return _itemName;
            }
        }

        public decimal ItemPrice {
            get {
                return _itemPrice;
            }
        }
        #endregion

        #region Static Methods
        static public void populateItemList()
        {

            StreamReader itemFile = new StreamReader("Items.txt");

            string itemLine, itemID, itemName;
            decimal itemPrice;
            string[] itemElems;

            while ((itemLine = itemFile.ReadLine()) != null)
            {

                itemElems = itemLine.Split('\t');

                itemID = itemElems[0];
                itemName = itemElems[1];
                itemPrice = decimal.Parse(itemElems[2]);

                ItemList.Add(new Item(itemID, itemName, itemPrice));
            }
        }
        #endregion
    }
}
