using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace E_Commerce
{
    public partial class ShoppingCartForm : Form
    {
        private string connectionString = "Data Source=PAMARAN\\SQLEXPRESS;Initial Catalog=E-Commerce;Integrated Security=True;TrustServerCertificate=True";
        private int userId;
        private string loggedInUsername;
        private Form SearchForm; // Store reference to MainForm

        public ShoppingCartForm(int loggedInUserId, string loggedInUsername, Form mainForm)
        {
            InitializeComponent();
            userId = loggedInUserId;
            this.loggedInUsername = loggedInUsername;
            this.SearchForm = mainForm;

            GetUsername();
            LoadCart();
        }

        private void GetUsername()
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                string query = "SELECT username FROM userpass WHERE userId = @userId";

                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@userId", userId);
                    object usernameResult = cmd.ExecuteScalar();  // Renamed from result to usernameResult
                    if (usernameResult != null)
                    {
                        loggedInUsername = usernameResult.ToString();
                    }
                }
            }
        }

        private void LoadCart()
        {
            if (dgvCart.Columns.Count == 0)
            {
                dgvCart.Columns.Add("CartID", "Cart ID");
                dgvCart.Columns.Add("ProductName", "Product Name");
                dgvCart.Columns.Add("Quantity", "Quantity");
                dgvCart.Columns.Add("Price", "Price");
                dgvCart.Columns.Add("TotalPrice", "Total Price");
            }

            dgvCart.Rows.Clear(); // Clear existing rows

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                string query = @"
                SELECT sc.cart_id, p.name AS product_name, sc.quantity, p.price, 
                       (sc.quantity * p.price) AS total_price 
                FROM shopping_cart sc
                JOIN products p ON sc.product_id = p.id
                WHERE sc.user_id = @user_id;";

                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@user_id", userId);

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            dgvCart.Rows.Add(
                                reader.GetInt32(0),   // Cart ID
                                reader.GetString(1),  // Product Name
                                reader.GetInt32(2),   // Quantity
                                reader.GetDecimal(3), // Price
                                reader.GetDecimal(4)  // Total Price
                            );
                        }
                    }
                }
            }
            CalculateTotal(); // Update total price
        }

        private void CalculateTotal()
        {
            decimal total = 0;
            foreach (DataGridViewRow row in dgvCart.Rows)
            {
                total += Convert.ToDecimal(row.Cells["TotalPrice"].Value);
            }
            lblTotalAmount.Text = "Total: ₱" + total.ToString("N2");
        }

        private void btnPlaceOrder_Click_1(object sender, EventArgs e)
        {
                if (dgvCart.Rows.Count == 0)
                {
                    MessageBox.Show("Your cart is empty! Add items before placing an order.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                decimal totalAmount = 0;
                List<(int productId, int quantity, int stock)> cartItems = new List<(int, int, int)>();

                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();

                    // ✅ Calculate total amount
                    string totalQuery = @"
            SELECT SUM(sc.quantity * p.price) 
            FROM shopping_cart sc 
            JOIN products p ON sc.product_id = p.id 
            WHERE sc.user_id = @user_id";

                    using (SqlCommand cmdTotal = new SqlCommand(totalQuery, conn))
                    {
                        cmdTotal.Parameters.AddWithValue("@user_id", userId);
                        object totalResult = cmdTotal.ExecuteScalar();
                        totalAmount = (totalResult != DBNull.Value) ? Convert.ToDecimal(totalResult) : 0;
                    }

                    if (totalAmount == 0)
                    {
                        MessageBox.Show("Your cart is empty or contains invalid items!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }

                    // ✅ Check stock availability before placing order
                    string stockQuery = @"
            SELECT sc.product_id, sc.quantity, p.stock 
            FROM shopping_cart sc
            JOIN products p ON sc.product_id = p.id
            WHERE sc.user_id = @user_id";

                    using (SqlCommand cmdStock = new SqlCommand(stockQuery, conn))
                    {
                        cmdStock.Parameters.AddWithValue("@user_id", userId);
                        using (SqlDataReader reader = cmdStock.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                int productId = reader.GetInt32(0);
                                int quantity = reader.GetInt32(1);
                                int stock = reader.GetInt32(2);

                                if (stock < quantity)
                                {
                                    MessageBox.Show($"Not enough stock for product ID {productId}. Available: {stock}, Required: {quantity}.", "Stock Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                    return;
                                }

                                cartItems.Add((productId, quantity, stock));
                            }
                        }
                    }
                }

                // ✅ Check for low stock warning (after checking availability)
                foreach (var item in cartItems)
                {
                    if (item.stock - item.quantity < 5)
                    {
                        MessageBox.Show($"Warning: Product ID {item.productId} is running low! Remaining stock after order: {item.stock - item.quantity}.", "Low Stock Alert", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                }

                // ❌ REMOVE this.Hide();
                PaymentForm paymentForm = new PaymentForm(userId, totalAmount, this);
                DialogResult paymentResult = paymentForm.ShowDialog();

                if (paymentResult == DialogResult.OK)
                {
                    using (SqlConnection conn = new SqlConnection(connectionString))
                    {
                        conn.Open();

                        // ✅ Insert order
                        string insertOrderQuery = @"
                INSERT INTO orders (user_id, total_price, payment_method, order_date)
                VALUES (@user_id, @total_price, @payment_method, GETDATE());";

                        using (SqlCommand cmdOrder = new SqlCommand(insertOrderQuery, conn))
                        {
                            cmdOrder.Parameters.AddWithValue("@user_id", userId);
                            cmdOrder.Parameters.AddWithValue("@total_price", totalAmount);
                            cmdOrder.Parameters.AddWithValue("@payment_method", paymentForm.PaymentMethod);
                            cmdOrder.ExecuteNonQuery();
                        }
                    }

                    MessageBox.Show("Order placed successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    // ❌ REMOVE this.Close();
                    LoadCart(); // Refresh the cart after placing an order
            }
        }

        private void btnRemoveItem_Click_1(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtCartIdd.Text) || string.IsNullOrWhiteSpace(txtRemoveQuantity.Text))
            {
                MessageBox.Show("Please enter both Cart ID and Quantity.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (!int.TryParse(txtCartIdd.Text, out int cartItemId) || !int.TryParse(txtRemoveQuantity.Text, out int quantityToRemove) || quantityToRemove <= 0)
            {
                MessageBox.Show("Enter valid numeric values for Cart ID and Quantity.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();

                string checkQuery = "SELECT quantity FROM shopping_cart WHERE cart_id = @cart_id";
                int currentQuantity = 0;

                using (SqlCommand cmdCheck = new SqlCommand(checkQuery, conn))
                {
                    cmdCheck.Parameters.AddWithValue("@cart_id", cartItemId);
                    var checkResult = cmdCheck.ExecuteScalar(); // Renamed from result to checkResult
                    if (checkResult != null)
                    {
                        currentQuantity = Convert.ToInt32(checkResult);
                    }
                    else
                    {
                        MessageBox.Show("Cart ID not found.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }
                }

                if (quantityToRemove > currentQuantity)
                {
                    MessageBox.Show("Error: Quantity entered exceeds the current quantity in the cart.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                string query = (quantityToRemove == currentQuantity) ?
                    "DELETE FROM shopping_cart WHERE cart_id = @cart_id" :
                    "UPDATE shopping_cart SET quantity = quantity - @quantity WHERE cart_id = @cart_id";

                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@cart_id", cartItemId);
                    cmd.Parameters.AddWithValue("@quantity", quantityToRemove);
                    cmd.ExecuteNonQuery();
                }
            }

            MessageBox.Show("Item updated in cart!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
            LoadCart();
        }

        private void btnLogout_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Logout ka na brad?", "Logoutnism", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                Application.Restart();
                Environment.Exit(0);
            }
        }

        private void btnBack_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Go back ka na brad?", "Back", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                this.Hide();
                SearchForm?.Show();
                this.Close();
            }
        }
    }
}
