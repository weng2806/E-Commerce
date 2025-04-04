﻿using System;
using System.Data;
using System.IO;
using System.Windows.Forms;
using Microsoft.Data.SqlClient;
using System.Drawing;
using System.Drawing.Imaging;


namespace E_Commerce
{
    public partial class AdminForm : Form
    {
        string connectionString = "Data Source=PAMARAN\\SQLEXPRESS;Initial Catalog=E-Commerce;Integrated Security=True;TrustServerCertificate=True";

        public AdminForm()
        {
            InitializeComponent();
            LoadUsers();
            LoadProducts();
            LoadCart();
        }
        private void UpdateCart(int cartId, int newQuantity)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();

                // get current quantity sa cart
                string getCartQuery = "SELECT product_id, quantity FROM shopping_cart WHERE cart_id = @cartId";
                int productId = 0, oldQuantity = 0;

                using (SqlCommand getCartCmd = new SqlCommand(getCartQuery, conn))
                {
                    getCartCmd.Parameters.AddWithValue("@cartId", cartId);
                    using (SqlDataReader reader = getCartCmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            productId = Convert.ToInt32(reader["product_id"]);
                            oldQuantity = Convert.ToInt32(reader["quantity"]);
                        }
                        else
                        {
                            MessageBox.Show("Cart item not found!");
                            return;
                        }
                    }
                }

                int quantityDifference = newQuantity - oldQuantity;

                if (quantityDifference > 0)
                {
                    string checkStockQuery = "SELECT stock FROM products WHERE id = @productId";
                    int stock = 0;

                    using (SqlCommand checkStockCmd = new SqlCommand(checkStockQuery, conn))
                    {
                        checkStockCmd.Parameters.AddWithValue("@productId", productId);
                        object stockObj = checkStockCmd.ExecuteScalar();
                        stock = Convert.ToInt32(stockObj ?? 0);
                    }

                    if (stock < quantityDifference)
                    {
                        MessageBox.Show("Not enough stock available.");
                        return;
                    }
                }

                // update stock
                string updateStockQuery = "UPDATE products SET stock = stock - @quantityDiff WHERE id = @productId";
                using (SqlCommand updateStockCmd = new SqlCommand(updateStockQuery, conn))
                {
                    updateStockCmd.Parameters.AddWithValue("@quantityDiff", quantityDifference);
                    updateStockCmd.Parameters.AddWithValue("@productId", productId);
                    updateStockCmd.ExecuteNonQuery();
                }

                // update quantity sa cart
                string updateCartQuery = "UPDATE shopping_cart SET quantity = @newQuantity WHERE cart_id = @cartId";
                using (SqlCommand updateCartCmd = new SqlCommand(updateCartQuery, conn))
                {
                    updateCartCmd.Parameters.AddWithValue("@cartId", cartId);
                    updateCartCmd.Parameters.AddWithValue("@newQuantity", newQuantity);
                    updateCartCmd.ExecuteNonQuery();
                }

                MessageBox.Show("Cart Item Updated Successfully!");
                LoadProducts(); // eme emehan stock display
                LoadCart();
            }
        }
        private void DeleteFromCart(int cartId)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();

                // ghetto product ID , quantity before deleting
                string getCartQuery = "SELECT product_id, quantity FROM shopping_cart WHERE cart_id = @cartId";
                int productId = 0, quantity = 0;

                using (SqlCommand getCartCmd = new SqlCommand(getCartQuery, conn))
                {
                    getCartCmd.Parameters.AddWithValue("@cartId", cartId);
                    using (SqlDataReader reader = getCartCmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            productId = Convert.ToInt32(reader["product_id"]);
                            quantity = Convert.ToInt32(reader["quantity"]);
                        }
                        else
                        {
                            MessageBox.Show("Cart item not found!");
                            return;
                        }
                    }
                }

                string updateStockQuery = "UPDATE products SET stock = stock + @quantity WHERE id = @productId";
                using (SqlCommand updateStockCmd = new SqlCommand(updateStockQuery, conn))
                {
                    updateStockCmd.Parameters.AddWithValue("@quantity", quantity);
                    updateStockCmd.Parameters.AddWithValue("@productId", productId);
                    updateStockCmd.ExecuteNonQuery();
                }

                string deleteCartQuery = "DELETE FROM shopping_cart WHERE cart_id = @cartId";
                using (SqlCommand deleteCartCmd = new SqlCommand(deleteCartQuery, conn))
                {
                    deleteCartCmd.Parameters.AddWithValue("@cartId", cartId);
                    deleteCartCmd.ExecuteNonQuery();
                }

                MessageBox.Show("Cart item deleted and stock restored!");
                LoadProducts();
                LoadCart();
            }
        }
        private void AddToCart(int userId, int productId, int quantity)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();

                string checkProductQuery = "SELECT stock FROM products WHERE id = @id";
                using (SqlCommand checkCmd = new SqlCommand(checkProductQuery, conn))
                {
                    checkCmd.Parameters.AddWithValue("@id", productId);
                    object stockObj = checkCmd.ExecuteScalar();

                    if (stockObj == null)
                    {
                        MessageBox.Show("Product not found!");
                        return;
                    }

                    int stock = Convert.ToInt32(stockObj);
                    if (stock < quantity)
                    {
                        MessageBox.Show("Not enough stock available.");
                        return;
                    }
                }

                string insertQuery = "INSERT INTO shopping_cart (user_id, product_id, quantity) VALUES (@userId, @productId, @quantity)";
                using (SqlCommand cmd = new SqlCommand(insertQuery, conn))
                {
                    cmd.Parameters.AddWithValue("@userId", userId);
                    cmd.Parameters.AddWithValue("@productId", productId);
                    cmd.Parameters.AddWithValue("@quantity", quantity);
                    cmd.ExecuteNonQuery();
                }

                string updateStockQuery = "UPDATE products SET stock = stock - @quantity WHERE id = @id";
                using (SqlCommand cmd = new SqlCommand(updateStockQuery, conn))
                {
                    cmd.Parameters.AddWithValue("@quantity", quantity);
                    cmd.Parameters.AddWithValue("@id", productId);
                    cmd.ExecuteNonQuery();
                }

                MessageBox.Show("Item added to cart and stock updated!");
                LoadProducts();
                LoadCart();
            }
        }
        private void DeleteProductImage(int productId)
        {
            if (!ExistsInDatabase("products", "id", productId))
            {
                MessageBox.Show("Product ID does not exist!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                string query = "UPDATE products SET image = NULL WHERE id = @id";
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@id", productId);
                    conn.Open();
                    cmd.ExecuteNonQuery();
                    pictureBoxProduct.Image = null;
                    MessageBox.Show("Product image deleted successfully!");
                }
            }
        }

        private bool ExistsInDatabase(string tableName, string columnName, int id)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                string query = $"SELECT COUNT(*) FROM {tableName} WHERE {columnName} = @id";
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@id", id);
                    conn.Open();
                    return (int)cmd.ExecuteScalar() > 0;
                }
            }
        }

        private void ShowProductImage(int productId)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                string query = "SELECT image FROM products WHERE id = @id";
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@id", productId);
                    conn.Open();
                    object result = cmd.ExecuteScalar();

                    if (result != DBNull.Value && result != null)
                    {
                        byte[] imageData = (byte[])result;
                        using (MemoryStream ms = new MemoryStream(imageData))
                        {
                            Image originalImage = Image.FromStream(ms);
                            pictureBoxProduct.Image = ResizeImage(originalImage, pictureBoxProduct.Width, pictureBoxProduct.Height);
                        }
                    }
                    else
                    {
                        pictureBoxProduct.Image = null;
                    }
                }
            }
        }

        private void LoadProductImage(int productId)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                string query = "SELECT image FROM products WHERE id = @id";
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@id", productId);
                    conn.Open();
                    object result = cmd.ExecuteScalar();

                    if (result != DBNull.Value && result != null)
                    {
                        byte[] imgBytes = (byte[])result;
                        using (MemoryStream ms = new MemoryStream(imgBytes))
                        {
                            pictureBoxProduct.Image = Image.FromStream(ms);  // dito na magsshow image g
                        }
                    }
                    else
                    {
                        pictureBoxProduct.Image = null;
                    }
                }
            }
        }

        // load users sa datagdridview
        private void LoadUsers()
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                string query = "SELECT userId, username, password, role FROM userpass";
                SqlDataAdapter adapter = new SqlDataAdapter(query, conn);
                DataTable dt = new DataTable();
                adapter.Fill(dt);
                dgvUsers.DataSource = dt;
            }
        }

        // load products sa datagridview
        private void LoadProducts()
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                string query = "SELECT id, name, description, price, stock, image FROM products";
                SqlDataAdapter adapter = new SqlDataAdapter(query, conn);
                DataTable dt = new DataTable();
                adapter.Fill(dt);
                dgvProducts.DataSource = dt;

                if (dt.Rows.Count > 0 && dt.Rows[0]["image"] != DBNull.Value)
                {
                    byte[] imgBytes = (byte[])dt.Rows[0]["image"];
                    using (MemoryStream ms = new MemoryStream(imgBytes)) // convert byte array to image
                    {
                        pictureBoxProduct.Image = Image.FromStream(ms); // display image basta ayan nakakapagod na
                    }
                }
            }
        }


        // load ulit kainis
        private void LoadCart()
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                string query = "SELECT cart_id, user_id, product_id, quantity FROM shopping_cart";
                SqlDataAdapter adapter = new SqlDataAdapter(query, conn);
                DataTable dt = new DataTable();
                adapter.Fill(dt);
                dgvCart.DataSource = dt;
            }
        }

        // save sa database
        private void SaveImage(int productId, string imagePath)
        {
            byte[] imageData;

            // convert image to byte array
            using (MemoryStream ms = new MemoryStream())
            {
                Image img = Image.FromFile(imagePath);
                img.Save(ms, ImageFormat.Png); // save as png to dawg
                imageData = ms.ToArray();
            }

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();

                string checkQuery = "SELECT COUNT(*) FROM products WHERE id = @id";
                using (SqlCommand checkCmd = new SqlCommand(checkQuery, conn))
                {
                    checkCmd.Parameters.AddWithValue("@id", productId);
                    int count = (int)checkCmd.ExecuteScalar();
                    if (count == 0)
                    {
                        MessageBox.Show("Product ID does not exist.");
                        return;
                    }
                }

                string query = "UPDATE products SET image = @image WHERE id = @id";
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@id", productId);
                    cmd.Parameters.AddWithValue("@image", imageData);
                    cmd.ExecuteNonQuery();
                }

                MessageBox.Show("Image uploaded successfully!");
            }
        }
        private void AddUser()
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                string query = "INSERT INTO userpass (username, password, role) VALUES (@username, @password, @role)";
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@username", txtUsername.Text);
                    cmd.Parameters.AddWithValue("@password", txtPassword.Text);
                    cmd.Parameters.AddWithValue("@role", txtRole.Text);

                    conn.Open();
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("User Added Successfully!");
                    LoadUsers();
                }
            }
        }

        private void UpdateUser()
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                string query = "UPDATE userpass SET username=@username, password=@password, role=@role WHERE userId=@userId";
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@userId", txtUserId.Text);
                    cmd.Parameters.AddWithValue("@username", txtUsername.Text);
                    cmd.Parameters.AddWithValue("@password", txtPassword.Text);
                    cmd.Parameters.AddWithValue("@role", txtRole.Text);

                    conn.Open();
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("User Updated Successfully!");
                    LoadUsers();
                }
            }
        }
        private void DeleteUser()
        {
            try
            {
                int userId = Convert.ToInt32(txtUserId.Text);

                if (!ExistsInDatabase("userpass", "userId", userId))
                {
                    MessageBox.Show("User ID does not exist!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    string query = "DELETE FROM userpass WHERE userId=@userId";
                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@userId", userId);
                        conn.Open();
                        cmd.ExecuteNonQuery();
                        MessageBox.Show("User Deleted Successfully!");
                        LoadUsers();
                    }
                }
            }
            catch (FormatException)
            {
                MessageBox.Show("Please enter a valid numeric User ID.", "Invalid Input", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }
        private void AddProduct()
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                string query = "INSERT INTO products (name, description, price, stock, image) VALUES (@name, @description, @price, @stock, NULL)"; // NULL for image initially
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@name", txtProductName.Text);
                    cmd.Parameters.AddWithValue("@description", txtDescription.Text);
                    cmd.Parameters.AddWithValue("@price", Convert.ToDecimal(txtProductPrice.Text));
                    cmd.Parameters.AddWithValue("@stock", Convert.ToInt32(txtProductStock.Text));

                    conn.Open();
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Product Added Successfully!");
                    LoadProducts();
                }
            }
        }
        private void UpdateProduct()
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                string query = "UPDATE products SET name=@name, description=@description, price=@price, stock=@stock WHERE id=@id";
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@id", txtProductId.Text);
                    cmd.Parameters.AddWithValue("@name", txtProductName.Text);
                    cmd.Parameters.AddWithValue("@description", txtDescription.Text);
                    cmd.Parameters.AddWithValue("@price", Convert.ToDecimal(txtProductPrice.Text));
                    cmd.Parameters.AddWithValue("@stock", Convert.ToInt32(txtProductStock.Text));

                    conn.Open();
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Product Updated Successfully!");
                    LoadProducts();
                }
            }
        }
        private void DeleteProduct()
        {
            try
            {
                int productId = Convert.ToInt32(txtProductId.Text);

                if (!ExistsInDatabase("products", "id", productId))
                {
                    MessageBox.Show("Product ID does not exist", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    string query = "DELETE FROM products WHERE id=@id";
                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@id", productId);
                        conn.Open();
                        cmd.ExecuteNonQuery();
                        MessageBox.Show("Product Deleted Successfully!");
                        LoadProducts();
                    }
                }
            }
            catch (FormatException)
            {
                MessageBox.Show("Please enter a valid numeric Product ID.", "Invalid Input", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }
        private void SearchUser()
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                string query = "SELECT userId, username, role FROM userpass WHERE username LIKE @keyword";
                SqlDataAdapter adapter = new SqlDataAdapter(query, conn);
                adapter.SelectCommand.Parameters.AddWithValue("@keyword", "%" + txtSearchUser.Text.Trim() + "%");
                DataTable dt = new DataTable();
                adapter.Fill(dt);
                dgvUsers.DataSource = dt;
            }
        }
        private void DeleteCart()
        {
            try
            {
                int cartId = Convert.ToInt32(txtCartId.Text);

                if (!ExistsInDatabase("shopping_cart", "cart_id", cartId))
                {
                    MessageBox.Show("Cart ID does not exist!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                DeleteFromCart(cartId);
            }
            catch (FormatException)
            {
                MessageBox.Show("Please enter a valid numeric Cart ID.", "Invalid Input", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }
        private void UploadImage()
        {
            using (OpenFileDialog openFileDialog = new OpenFileDialog())
            {
                openFileDialog.Filter = "Image Files|*.jpg;*.jpeg;*.png;*.gif;*.bmp";

                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    string imagePath = openFileDialog.FileName;
                    pictureBoxProduct.Image = Image.FromFile(imagePath);

                    if (int.TryParse(txtProductId.Text, out int productId))
                    {
                        SaveImage(productId, imagePath);
                        LoadProducts();
                    }
                    else
                    {
                        MessageBox.Show("Please enter a valid Product ID.");
                    }
                }
            }
        }

        private void DgvProducts(DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = dgvProducts.Rows[e.RowIndex];

                // assign value 'to from data gridview to textbox
                txtProductId.Text = row.Cells["id"].Value.ToString();
                txtProductName.Text = row.Cells["name"].Value.ToString();
                txtDescription.Text = row.Cells["description"].Value.ToString();
                txtProductPrice.Text = row.Cells["price"].Value.ToString();
                txtProductStock.Text = row.Cells["stock"].Value.ToString();

                // pasabog image
                int productId = Convert.ToInt32(row.Cells["id"].Value);
                ShowProductImage(productId);
            }
        }
        private void Logout()
        {
            DialogResult result = MessageBox.Show("Logout ka na brad?", "Logoutnism",
                                                  MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (result == DialogResult.Yes)
            {
                Application.Restart(); // restart app
                Environment.Exit(0);   // exit app
            }
        }
        private void ShowImage()
        {
            try
            {
                int productId = Convert.ToInt32(txtProductId.Text);
                ShowProductImage(productId);
            }
            catch (FormatException)
            {
                MessageBox.Show("Please enter a valid Product ID.", "Invalid Input", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }
        private void DeleteImage()
        {
            try
            {
                int productId = Convert.ToInt32(txtProductId.Text);
                DeleteProductImage(productId);
                LoadProducts();
            }
            catch (FormatException)
            {
                MessageBox.Show("Please enter a valid Product ID.", "Invalid Input", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }
        // basta dito yung inaayos niya yung view ng image para kita yung lahat ng content ng image
        private Image ResizeImage(Image image, int width, int height)
        {
            Bitmap resizedImage = new Bitmap(width, height);
            using (Graphics graphics = Graphics.FromImage(resizedImage))
            {
                graphics.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic;
                graphics.DrawImage(image, 0, 0, width, height);
            }
            return resizedImage;
        }
        private void UpdateeeCart()
        {
            try
            {
                int cartId = Convert.ToInt32(txtCartId.Text);
                int quantity = Convert.ToInt32(txtCartQuantity.Text);

                if (!ExistsInDatabase("shopping_cart", "cart_id", cartId))
                {
                    MessageBox.Show("Cart ID does not exist!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                UpdateCart(cartId, quantity); // new method to
            }
            catch (FormatException)
            {
                MessageBox.Show("Please enter valid numeric values for Cart ID and Quantity.", "Invalid Input", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }
        private void AddCartt()
        {
            try
            {
                int userId = Convert.ToInt32(txtUserId.Text);
                int productId = Convert.ToInt32(txtProductId.Text);
                int quantity = Convert.ToInt32(txtCartQuantity.Text);

                if (!ExistsInDatabase("userpass", "userId", userId))
                {
                    MessageBox.Show("User ID does not exist!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                if (!ExistsInDatabase("products", "id", productId))
                {
                    MessageBox.Show("Product ID does not exist!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                AddToCart(userId, productId, quantity); 
            }
            catch (FormatException)
            {
                MessageBox.Show("Please enter valid numeric values for User ID, Product ID, and Quantity.", "Invalid Input", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }




        private void btnAddUser_Click_1(object sender, EventArgs e)
        {
            string role = txtRole.Text.Trim().ToLower();

            if (role != "user" && role != "admin")
            {
                MessageBox.Show("'user' or 'admin' lang sah ayaw palamang e", "Input Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            AddUser();
        }
        private void btnUpdateUser_Click_1(object sender, EventArgs e)
        {
            UpdateUser();
        }

        private void btnDeleteUser_Click_1(object sender, EventArgs e)
        {
            DeleteUser();
        }

        private void btnAddProduct_Click_1(object sender, EventArgs e)
        {
            AddProduct();
        }

        private void btnUpdateProduct_Click(object sender, EventArgs e)
        {
            UpdateProduct();
        }

        private void btnDeleteProduct_Click_1(object sender, EventArgs e)
        {
            DeleteProduct();
        } 

        private void btnDeleteCart_Click_1(object sender, EventArgs e)
        {
            DeleteCart();
        }

        private void btnSearchUser_Click_1(object sender, EventArgs e)
        {
            SearchUser();
        }

        private void btnUploadImage_Click_1(object sender, EventArgs e)
        {
            UploadImage();
        }

        private void dgvProducts_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            DgvProducts(e);
        }

        private void btnShowImage_Click(object sender, EventArgs e)
        {
            ShowImage();
        }

        private void btnLogout_Click(object sender, EventArgs e)
        {
            Logout();
        }

        private void btnDeleteImage_Click_1(object sender, EventArgs e)
        {
            DeleteImage();   
        }

        private void btnUpdateCart_Click(object sender, EventArgs e)
        {
            UpdateeeCart();
        }

        private void btnAddCart_Click(object sender, EventArgs e)
        {
            AddCartt();
        }

       
    }
}