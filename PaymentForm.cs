using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Windows.Forms;

namespace E_Commerce
{
    public partial class PaymentForm : Form
    {
        private string connectionString = "Data Source=PAMARAN\\SQLEXPRESS;Initial Catalog=E-Commerce;Integrated Security=True;TrustServerCertificate=True";
        private int userId;
        private decimal totalAmount;
        private ShoppingCartForm shoppingCartForm;

        public PaymentForm(int loggedInUserId, decimal total, ShoppingCartForm shoppingCartForm)
        {
            InitializeComponent();
            userId = loggedInUserId;
            totalAmount = total;
            lblTotal.Text = "Total Amount: ₱" + totalAmount.ToString("N2");

            cmbPaymentMethod.Items.Clear();
            cmbPaymentMethod.Items.Add("Cash on Delivery");
            cmbPaymentMethod.Items.Add("Bank Payment");

            cmbBank.Items.Add("BDO");
            cmbBank.Items.Add("UnionBank");
            cmbBank.Items.Add("DBP");


            cmbPaymentMethod.SelectedIndex = 0;


            this.shoppingCartForm = shoppingCartForm;
        }

        private void PaymentForm_Load(object sender, EventArgs e)
        {
              
                cmbBank.Visible = false;
                txtAccountNumber.Visible = false;
                aa.Visible = false;
                awit.Visible = false;

                lblCODMessage.Visible = false; 
        }

        public string PaymentMethod { get; private set; }

   
        private void btnConfirmPayment_Click_1(object sender, EventArgs e)
        {
            
        }

        private void cmbPaymentMethod_SelectedIndexChanged_1(object sender, EventArgs e)
        {
            string selectedPayment = cmbPaymentMethod.SelectedItem?.ToString();

            if (selectedPayment == "Bank Payment")
            {
                cmbBank.Visible = true;
                txtAccountNumber.Visible = true;
                aa.Visible = true;
                awit.Visible = true;

                lblCODMessage.Visible = false; 
            }
            else if (selectedPayment == "Cash on Delivery")
            {
                cmbBank.Visible = false;
                txtAccountNumber.Visible = false;
                aa.Visible = false;
                awit.Visible = false;

                lblCODMessage.Text = $"Please prepare {totalAmount:N2} pesos.";
                lblCODMessage.Visible = true;
            }
        }

        private void btnConfirmPayment_Click(object sender, EventArgs e)
        {
            {
                string paymentMethod = cmbPaymentMethod.SelectedItem?.ToString();
                string accountNumber = txtAccountNumber.Text.Trim();

                if (string.IsNullOrEmpty(paymentMethod))
                {
                    MessageBox.Show("Please select a payment method.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                if (paymentMethod == "Bank Payment")
                {
                    if (string.IsNullOrWhiteSpace(accountNumber) || accountNumber.Length != 12 || !accountNumber.All(char.IsDigit))
                    {
                        MessageBox.Show("Bank account number must be exactly 12 digits.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }
                }
                else
                {
                    accountNumber = null;
                }

                PaymentMethod = paymentMethod;

                try
                {
                    using (SqlConnection conn = new SqlConnection(connectionString))
                    {
                        conn.Open();
                        SqlTransaction transaction = conn.BeginTransaction();

                        try
                        {
                            // insert order
                            string orderQuery = @"
                    INSERT INTO orders (user_id, total_price, payment_method, account_number)
                    VALUES (@user_id, @total_price, @payment_method, @account_number);
                    SELECT SCOPE_IDENTITY();";

                            int orderIdd; 
                            using (SqlCommand cmd = new SqlCommand(orderQuery, conn, transaction))
                            {
                                cmd.Parameters.AddWithValue("@user_id", userId);
                                cmd.Parameters.AddWithValue("@total_price", totalAmount);
                                cmd.Parameters.AddWithValue("@payment_method", paymentMethod);
                                cmd.Parameters.AddWithValue("@account_number", (object)accountNumber ?? DBNull.Value);
                                orderIdd = Convert.ToInt32(cmd.ExecuteScalar());
                            }

                            List<(int productId, int quantity)> cartItems = new List<(int, int)>();
                            string cartQuery = "SELECT product_id, quantity FROM shopping_cart WHERE user_id = @user_id";

                            using (SqlCommand cmd = new SqlCommand(cartQuery, conn, transaction))
                            {
                                cmd.Parameters.AddWithValue("@user_id", userId);
                                using (SqlDataReader reader = cmd.ExecuteReader())
                                {
                                    while (reader.Read())
                                    {
                                        cartItems.Add((reader.GetInt32(0), reader.GetInt32(1)));
                                    }
                                }
                            }

                            foreach (var item in cartItems)
                            {
                                int productId = item.productId;
                                int quantity = item.quantity;

                                string updateStockQuery = "UPDATE products SET stock = stock - @quantity WHERE id = @product_id";
                                using (SqlCommand updateCmd = new SqlCommand(updateStockQuery, conn, transaction))
                                {
                                    updateCmd.Parameters.AddWithValue("@quantity", quantity);
                                    updateCmd.Parameters.AddWithValue("@product_id", productId);
                                    updateCmd.ExecuteNonQuery();
                                }

                                string insertOrderDetails = @"
                        INSERT INTO order_details (order_idd, product_id, quantity)
                        VALUES (@order_idd, @product_id, @quantity)"; 
                                using (SqlCommand orderDetailsCmd = new SqlCommand(insertOrderDetails, conn, transaction))
                                {
                                    orderDetailsCmd.Parameters.AddWithValue("@order_idd", orderIdd); 
                                    orderDetailsCmd.Parameters.AddWithValue("@product_id", productId);
                                    orderDetailsCmd.Parameters.AddWithValue("@quantity", quantity);
                                    orderDetailsCmd.ExecuteNonQuery();
                                }
                            }

                            string deleteCartQuery = "DELETE FROM shopping_cart WHERE user_id = @user_id";
                            using (SqlCommand cmd = new SqlCommand(deleteCartQuery, conn, transaction))
                            {
                                cmd.Parameters.AddWithValue("@user_id", userId);
                                cmd.ExecuteNonQuery();
                            }

                            transaction.Commit();

                            MessageBox.Show("Payment successful! Order placed.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            this.DialogResult = DialogResult.OK;
                            this.Close();
                        }
                        catch (Exception ex)
                        {
                            transaction.Rollback();
                            MessageBox.Show("Error processing payment: " + ex.Message, "Database Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Database connection error: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            } }
            
        }
    }
