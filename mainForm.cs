using System;
using System.Data;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace E_Commerce
{
    public partial class SearchForm : Form
    {
        private string connectionString = "Data Source=PAMARAN\\SQLEXPRESS;Initial Catalog=E-Commerce;Integrated Security=True;TrustServerCertificate=True";
        private int userId;  
        private string loggedInUsername;


        public SearchForm(int loggedInUserId)  
        {
            InitializeComponent();
            userId = loggedInUserId; 
            GetUsername();
            LoadProducts();
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
                    object result = cmd.ExecuteScalar();
                    if (result != null)
                    {
                        loggedInUsername = result.ToString();
                    }
                }
            }
        }
        private void LoadProducts(string searchKeyword = "")
        {
                flowLayoutPanelProducts.Controls.Clear();
                flowLayoutPanelProducts.AutoScroll = true;

                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();

                    string query = "SELECT id, name, description, stock, price, image FROM products " +
                                   "WHERE name LIKE @search";

                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@search", "%" + searchKeyword + "%");

                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                Panel productPanel = new Panel()
                                {
                                    Width = 300,
                                    Height = 350,
                                    BorderStyle = BorderStyle.FixedSingle,
                                    Margin = new Padding(10)
                                };

                                Label lblName = new Label()
                                {
                                    Text = reader["name"].ToString(),
                                    AutoSize = true,
                                    Font = new Font("Arial", 12, FontStyle.Bold),
                                    Location = new Point(10, 10)
                                };

                                PictureBox productImage = new PictureBox()
                                {
                                    Width = 200,
                                    Height = 150,
                                    Location = new Point(10, 40),
                                    SizeMode = PictureBoxSizeMode.StretchImage
                                };

                                if (reader["image"] != DBNull.Value)
                                {
                                    byte[] imgBytes = (byte[])reader["image"];
                                    using (MemoryStream ms = new MemoryStream(imgBytes))
                                    {
                                        productImage.Image = Image.FromStream(ms);
                                    }
                                }

                                Label lblDescription = new Label()
                                {
                                    Text = "Description: " + reader["description"].ToString(),
                                    AutoSize = true,
                                    Location = new Point(10, 200)
                                };

                                Label lblPrice = new Label()
                                {
                                    Text = "₱" + reader["price"].ToString(),
                                    Font = new Font("Arial", 10, FontStyle.Bold),
                                    Location = new Point(10, 230)
                                };

                                int stock = Convert.ToInt32(reader["stock"]);

                                Label lblStock = new Label()
                                {
                                    Text = "Stock: " + stock,
                                    Location = new Point(10, 260),
                                    ForeColor = (stock <= 5) ? Color.Red : Color.Black  
                                };

                                Button btnAddToCart = new Button()
                                {
                                    Text = "Add to Cart",
                                    Width = 100,
                                    Location = new Point(10, 290),
                                    Tag = reader["id"],
                                    Enabled = stock > 0  
                                };

                                btnAddToCart.Click += BtnAddToCart_Click;

                                productPanel.Controls.Add(lblName);
                                productPanel.Controls.Add(productImage);
                                productPanel.Controls.Add(lblDescription);
                                productPanel.Controls.Add(lblPrice);
                                productPanel.Controls.Add(lblStock);
                                productPanel.Controls.Add(btnAddToCart);

                                flowLayoutPanelProducts.Controls.Add(productPanel);
                            }
                        }
                    }
                }
        }


        private void BtnAddToCart_Click(object sender, EventArgs e)
        {
                Button btn = (Button)sender;
                int productId = Convert.ToInt32(btn.Tag);

                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();

                    SqlTransaction transaction = conn.BeginTransaction(); 

                    try
                    {
                        
                        string stockQuery = "SELECT stock FROM products WHERE id = @product_id";
                        int currentStock;

                        using (SqlCommand stockCmd = new SqlCommand(stockQuery, conn, transaction))
                        {
                            stockCmd.Parameters.AddWithValue("@product_id", productId);
                            currentStock = Convert.ToInt32(stockCmd.ExecuteScalar());
                        }

                        if (currentStock <= 0)
                        {
                            MessageBox.Show("Out of stock!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            transaction.Rollback();
                            return;
                        }

                        string checkQuery = "SELECT quantity FROM shopping_cart WHERE user_id = @user_id AND product_id = @product_id";
                        int cartQuantity = 0;

                        using (SqlCommand checkCmd = new SqlCommand(checkQuery, conn, transaction))
                        {
                            checkCmd.Parameters.AddWithValue("@user_id", userId);
                            checkCmd.Parameters.AddWithValue("@product_id", productId);

                            object result = checkCmd.ExecuteScalar();
                            if (result != null)
                            {
                                cartQuantity = Convert.ToInt32(result);
                            }
                        }

                        if (cartQuantity + 1 > currentStock)
                        {
                            MessageBox.Show($"Only {currentStock} item(s) available in stock!", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            transaction.Rollback();
                            return;
                        }

                        if (cartQuantity > 0)
                        {
                            string updateQuery = "UPDATE shopping_cart SET quantity = quantity + 1 WHERE user_id = @user_id AND product_id = @product_id";
                            using (SqlCommand updateCmd = new SqlCommand(updateQuery, conn, transaction))
                            {
                                updateCmd.Parameters.AddWithValue("@user_id", userId);
                                updateCmd.Parameters.AddWithValue("@product_id", productId);
                                updateCmd.ExecuteNonQuery();
                            }
                        }
                        else
                        {
                            string insertQuery = "INSERT INTO shopping_cart (user_id, product_id, quantity) VALUES (@user_id, @product_id, 1)";
                            using (SqlCommand insertCmd = new SqlCommand(insertQuery, conn, transaction))
                            {
                                insertCmd.Parameters.AddWithValue("@user_id", userId);
                                insertCmd.Parameters.AddWithValue("@product_id", productId);
                                insertCmd.ExecuteNonQuery();
                            }
                        }

                        transaction.Commit();
                        MessageBox.Show("Product added to cart!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    catch (Exception ex)
                    {
                        transaction.Rollback();
                        MessageBox.Show("Error: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
        }


        private void Logout()
        {
            DialogResult result = MessageBox.Show("Logout ka na brad?", "Logoutnism",
                                                  MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (result == DialogResult.Yes)
            {
                Application.Restart();
                Environment.Exit(0);
            }
        }

        private void btnLogout_Click(object sender, EventArgs e) { }

        private void btnLogout_Click_1(object sender, EventArgs e)
        {
            Logout();
        }


        // dito oopen new form
        private void btnShippingCart_Click(object sender, EventArgs e)
        {
            this.Hide(); // ✅ Hide the mainForm
            ShoppingCartForm cartForm = new ShoppingCartForm(userId, loggedInUsername, this);
            cartForm.ShowDialog(); 
            this.Show();
        }
        private void ResetPassword()
        {
            ResetPasswordForm cartForm = new ResetPasswordForm(loggedInUsername);
            cartForm.ShowDialog();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            ResetPassword();
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            string keyword = txtSearch.Text.Trim();
            LoadProducts(keyword);
        }
    }
}
