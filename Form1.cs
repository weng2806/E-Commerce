using System;
using Microsoft.Data.SqlClient;
using System.Windows.Forms;

namespace E_Commerce

{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void login_Click(object sender, EventArgs e)
        {
            string connectionString = "Data Source=PAMARAN\\SQLEXPRESS;Initial Catalog=E-Commerce;Integrated Security=True;TrustServerCertificate=True";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                string query = "SELECT role FROM userpass WHERE username = @userName AND password = @password";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@userName", userName.Text);
                    command.Parameters.AddWithValue("@password", password.Text);

                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            string role = reader["role"].ToString();

                            if (role == "admin")
                            {
                                MessageBox.Show("Admin Login Successful");

                                this.Hide();
                                AdminForm adminForm = new AdminForm();
                                adminForm.ShowDialog();
                                this.Close();
                            }
                            else
                            {
                                MessageBox.Show("User Login Successful");

                                this.Hide();
                                SearchForm searchForm = new SearchForm();
                                searchForm.ShowDialog();
                                this.Close();
                            }
                        }
                        else
                        {
                            MessageBox.Show("Invalid Credentials.");
                        }
                    }
                }
            }
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {

        }
    }
}
