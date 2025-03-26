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
        private string connectionString = "Server=localhost;Database=ecommerce_db;User Id=sa;Password=yourpassword;";

        public SearchForm()
        {
            InitializeComponent();
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            SearchProducts(txtSearch.Text.Trim());
        }

        private void SearchProducts(string keyword)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                try
                {
                    conn.Open();
                    string query = "SELECT id, name, description, price, stock, image FROM products WHERE name LIKE @keyword OR description LIKE @keyword";
                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@keyword", "%" + keyword + "%");
                        SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                        DataTable dt = new DataTable();
                        adapter.Fill(dt);
                        dgvProducts.DataSource = dt;

                        if (dt.Rows.Count > 0 && dt.Rows[0]["image"] != DBNull.Value)
                        {
                            byte[] imgData = (byte[])dt.Rows[0]["image"];
                            pictureBox.Image = ByteArrayToImage(imgData);
                        }
                        else
                        {
                            pictureBox.Image = null;
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error: " + ex.Message);
                }
            }
        }

        private Image ByteArrayToImage(byte[] byteArray)
        {
            using (MemoryStream ms = new MemoryStream(byteArray))
            {
                return Image.FromStream(ms);
            }
        }
    }
}
