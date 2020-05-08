using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CustomerTracking
{
    public partial class CustomerTrackingForm : Form
    {
        public CustomerTrackingForm()
        {
            InitializeComponent();
            Customer.populateCustomerList();
            Item.populateItemList();
            Order.populateOrderList();
            Return.populateReturnList();

            CustomerGrid.DataSource = Customer.CustomerList;
            CustomerGrid.Columns["CustomerID"].Visible = false;
            CustomerGrid.Columns["Birthday"].Visible = false;
            CustomerGrid.Columns["CustomerSince"].Visible = false;
            CustomerGrid.Columns["LastPurchase"].Visible = false;
            CustomerGrid.Columns["Points"].Visible = false;
            CustomerGrid.Columns["ReceiveTexts"].Visible = false;
            CustomerGrid.Columns["ReceiveEmails"].Visible = false;
            CustomerGrid.Columns["HasCard"].Visible = false;

            CustomerGrid.Columns["FirstName"].HeaderText = "First Name";
            
            CustomerGrid.Columns["LastName"].HeaderText = "Last Name";
            CustomerGrid.Columns["PhoneNumber"].HeaderText = "Phone Numbers";



            CustomerGrid.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

            tabControl1.SelectedTab = tabControl1.TabPages["CustomersTab"];

            styleHeader(CustomerGrid);
        }

        

         private void CustomerTrackingForm_Load(object sender, EventArgs e) {

         }

        private void ViewCustomerButton_Click(object sender, EventArgs e)
        {
            if (CustomerGrid.CurrentRow != null)
            {
                tabControl1.SelectedTab = tabControl1.TabPages["DetailsTab"];
                var selectedCustomer = CustomerGrid.CurrentRow.DataBoundItem as Customer;
                FirstNameTextbox.Text = selectedCustomer.FirstName;
                LastNameTextbox.Text = selectedCustomer.LastName;
                EmailTextbox.Text = selectedCustomer.Email;
                PhoneNumberTextbox.Text = selectedCustomer.PhoneNumber;
                BirthdayTextbox.Text = ((DateTime)selectedCustomer.Birthday).ToString("MM/dd/yyyy");
                CustomerSinceTextbox.Text = ((DateTime)selectedCustomer.CustomerSince).ToString("MM/dd/yyyy");
                LastPurchaseTextbox.Text = ((DateTime)selectedCustomer.LastPurchase).ToString("MM/dd/yyyy");
                PointsTextbox.Text = selectedCustomer.Points.ToString();
                ReceiveEmailsCheckbox.Checked = selectedCustomer.ReceiveEmails;
                ReceiveTextsCheckbox.Checked = selectedCustomer.ReceiveTexts;
                HasCardCheckbox.Checked = selectedCustomer.HasCard;
                FirstNameTextbox.Parent.Focus();
            }
        }

        private void SearchButton_Click(object sender, EventArgs e)
        {
            string searchFor = SearchTextbox.Text;
            if (searchFor != "") {

                CustomerGrid.DataSource = Customer.CustomerList.Where(c => c.FirstName.StartsWith(searchFor) ||
                                                            c.LastName.StartsWith(searchFor)  ||
                                                            c.Email.StartsWith(searchFor)     ||
                                                            c.PhoneNumber.StartsWith(searchFor) ).ToList();

            }
            else
            {
                CustomerGrid.DataSource = Customer.CustomerList;
            }
        }

        private void ViewTransactionsButton_Click(object sender, EventArgs e)
        {
            if (CustomerGrid.CurrentRow != null)
            {
                tabControl1.SelectedTab = tabControl1.TabPages["TransactionsTab"];
                var customer = CustomerGrid.CurrentRow.DataBoundItem as Customer;
                var transactions = new List<Transaction> { };
                Transaction transaction;

                transactions.AddRange(Order.OrderList.Where(o => o.CustomerID == customer.CustomerID).ToList());
                transactions.AddRange(Return.ReturnList.Where(r => r.CustomerID == customer.CustomerID).ToList());
                transactions = transactions.OrderBy(t => t.TransactionDate).ToList();
                TransactionGrid.DataSource = transactions;
                DataGridViewTextBoxColumn column = new DataGridViewTextBoxColumn();
                column.HeaderText = "Type";
                column.Name = "TransactionType";
                TransactionGrid.Columns.Insert(0, column);

                TransactionGrid.Columns["OrderID"].Visible = false;
                TransactionGrid.Columns["CustomerID"].Visible = false;

                TransactionGrid.Columns["TransactionDate"].HeaderText = "Transaction Date";

                foreach (DataGridViewRow r in TransactionGrid.Rows)
                {
                    transaction = r.DataBoundItem as Transaction;

                    if (transaction.GetType().IsEquivalentTo(typeof(Order)))
                        r.Cells["TransactionType"].Value = "Order";
                    else
                        r.Cells["TransactionType"].Value = "Return";
                }

                TransactionGrid.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
                foreach (DataGridViewColumn c in TransactionGrid.Columns)
                {
                    c.HeaderCell.Style.BackColor = Color.Gainsboro;
                    c.HeaderCell.Style.ForeColor = Color.Black;
                    c.HeaderCell.Style.SelectionBackColor = Color.Gainsboro;
                    c.HeaderCell.Style.SelectionForeColor = Color.Black;
                }

                styleHeader(TransactionGrid);
            }
        }



        private void ViewTransactionButton_Click(object sender, EventArgs e)
        {
            if (TransactionGrid.CurrentRow != null)
            {
                var selectedTransaction = TransactionGrid.CurrentRow.DataBoundItem as Transaction;
                if (selectedTransaction.GetType().Equals(typeof(Order)))
                {
                    Order order = selectedTransaction as Order;
                    tabControl1.SelectedTab = tabControl1.TabPages["OrderTab"];
                    PurchaseDateTextbox.Text = order.TransactionDate.ToString("MM/dd/yyyy");
                    ShippedDateTextbox.Text = order.ShippedDate.ToString("MM/dd/yyyy");
                    ArrivalDateTextbox.Text = order.ArrivalDate.ToString("MM/dd/yyyy");
                    CustomerFeedbackTextbox.Text = order.OrderFeedback;

                    OrderLinesGrid.DataSource = order.OrderLines;

                    OrderLinesGrid.Columns["LineNumber"].Visible = false;
                    OrderLinesGrid.Columns["LineItemID"].Visible = false;
             
                    OrderLinesGrid.Columns["ItemName"].HeaderText = "Item";
                    OrderLinesGrid.Columns["ItemName"].DefaultCellStyle.WrapMode = DataGridViewTriState.True;
                    OrderLinesGrid.Columns["ItemName"].Width = 700;
                  
                    OrderLinesGrid.Columns["ItemPrice"].HeaderText = "Price";
                    OrderLinesGrid.Columns["ItemPrice"].Width = 175;
               
                    OrderLinesGrid.Columns["ItemQuantity"].HeaderText = "Quantity";
                    OrderLinesGrid.Columns["ItemQuantity"].Width = 175;
                    OrderLinesGrid.Columns["ItemQuantity"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                    
                    OrderLinesGrid.Columns["ItemDiscount"].HeaderText = "Discount";
                    OrderLinesGrid.Columns["ItemDiscount"].Width = 174;
                 
                    OrderLinesGrid.Columns["LineTotal"].HeaderText = "Total";
                    OrderLinesGrid.Columns["LineTotal"].Width = 175;
                 
                    OrderLinesGrid.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells;

                    TotalTextbox.Text = order.getTotal().ToString();

                    foreach (DataGridViewColumn c in OrderLinesGrid.Columns)
                    {
                        c.HeaderCell.Style.BackColor = Color.Gainsboro;
                        c.HeaderCell.Style.ForeColor = Color.Black;
                        c.HeaderCell.Style.SelectionBackColor = Color.Gainsboro;
                        c.HeaderCell.Style.SelectionForeColor = Color.Black;
                    }

                    styleHeader(OrderLinesGrid);
                }
                else
                {
                    Return return1 = selectedTransaction as Return;
                    tabControl1.SelectedTab = tabControl1.TabPages["ReturnTab"];
                    
                    ReturnDateTextbox.Text = return1.TransactionDate.ToString("MM/dd/yyyy");

                    ReturnLinesGrid.DataSource = return1.ReturnLines;
                    
                    ReturnLinesGrid.Columns["ItemName"].HeaderText = "Item";
                    ReturnLinesGrid.Columns["ItemName"].DefaultCellStyle.WrapMode = DataGridViewTriState.True;
                    ReturnLinesGrid.Columns["ItemName"].Width = 490;

                    ReturnLinesGrid.Columns["ItemPricePaid"].HeaderText = "Price Paid";
                    ReturnLinesGrid.Columns["ItemPricePaid"].Width = 170;

                    ReturnLinesGrid.Columns["Quantity"].Width = 130;
                    ReturnLinesGrid.Columns["Quantity"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

                    ReturnLinesGrid.Columns["Refund"].Width = 120;

                    ReturnLinesGrid.Columns["ReasonForReturn"].HeaderText = "Reason for Return";
                    ReturnLinesGrid.Columns["ReasonForReturn"].DefaultCellStyle.WrapMode = DataGridViewTriState.True;
                    ReturnLinesGrid.Columns["ReasonForReturn"].Width = 490;

                    ReturnLinesGrid.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells;

                    Total2Textbox.Text = return1.getTotal().ToString();

                    foreach (DataGridViewColumn c in ReturnLinesGrid.Columns)
                    {
                        c.HeaderCell.Style.BackColor = Color.Gainsboro;
                        c.HeaderCell.Style.ForeColor = Color.Black;
                        c.HeaderCell.Style.SelectionBackColor = Color.Gainsboro;
                        c.HeaderCell.Style.SelectionForeColor = Color.Black;
                    }
                    styleHeader(ReturnLinesGrid);
                }
            }
        }

        private void textbox_Click(object sender, EventArgs e)
        {
           ((TextBox)sender).Parent.Focus();
        }

        private void checkbox_Click(object sender, EventArgs e)
        {
            ((CheckBox)sender).Parent.Focus();
        }

        private void styleHeader(DataGridView grid)
        {
            foreach (DataGridViewColumn c in grid.Columns)
            {
                c.HeaderCell.Style.BackColor = Color.DarkSeaGreen;
                c.HeaderCell.Style.ForeColor = Color.Black;
                c.HeaderCell.Style.SelectionBackColor = Color.DarkSeaGreen;
                c.HeaderCell.Style.SelectionForeColor = Color.Black;
                c.HeaderCell.Style.Font = new Font("Segoe UI", 10, FontStyle.Bold);
            }
        }
    }
}
