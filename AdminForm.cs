using System;
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

        private void btnAddUser_Click_1(object sender, EventArgs e)
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

        private void btnUpdateUser_Click_1(object sender, EventArgs e)
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

        private void btnDeleteUser_Click_1(object sender, EventArgs e)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                string query = "DELETE FROM userpass WHERE userId=@userId";
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@userId", txtUserId.Text);

                    conn.Open();
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("User Deleted Successfully!");
                    LoadUsers();
                }
            }
        }

        private void btnAddProduct_Click_1(object sender, EventArgs e)
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

        private void btnUpdateProduct_Click(object sender, EventArgs e)
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

        private void btnDeleteCart_Click_1(object sender, EventArgs e)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                string query = "DELETE FROM shopping_cart WHERE cart_id=@id";
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@id", txtCartId.Text);

                    conn.Open();
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Cart Item Deleted Successfully!");
                    LoadCart();
                }
            }
        }

        private void btnSearchUser_Click_1(object sender, EventArgs e)
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

        private void btnUploadImage_Click_1(object sender, EventArgs e)
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

        private void dgvProducts_CellClick(object sender, DataGridViewCellEventArgs e)
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

        private void btnShowImage_Click(object sender, EventArgs e)
        {
            
                if (int.TryParse(txtProductId.Text, out int productId))
                {
                    ShowProductImage(productId);
                }
                else
                {
                    MessageBox.Show("Please enter a valid Product ID.", "Invalid Input", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
        }

        private void btnLogout_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("logout ka na brad?", "Logoutnism",
                                       MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (result == DialogResult.Yes)
            {
                Form1 loginForm = new Form1();
                loginForm.Show();
                this.Close();
            }
        }


























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
    }
}


















