using System;
using Microsoft.Data.SqlClient;
using System.Windows.Forms;

namespace E_Commerce
{
    public partial class Form1 : Form
    {
        private string connectionString = "Data Source=PAMARAN\\SQLEXPRESS;Initial Catalog=E-Commerce;Integrated Security=True;TrustServerCertificate=True";

        public Form1()
        {
            InitializeComponent();
        }

        private void login_Click(object sender, EventArgs e)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                string query = "SELECT userId, role FROM userpass WHERE username = @userName AND password = @password";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    // Set parameters BEFORE executing the query
                    command.Parameters.AddWithValue("@userName", userName.Text);
                    command.Parameters.AddWithValue("@password", password.Text);

                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            int userId = Convert.ToInt32(reader["userId"]); // Get userId
                            string role = reader["role"].ToString();

                            MessageBox.Show("Hello, " + userName.Text + "!!");

                            if (role == "admin")
                            {
                                this.Hide();
                                AdminForm adminForm = new AdminForm();
                                adminForm.ShowDialog();
                            }
                            else
                            {
                                this.Hide();
                                SearchForm searchForm = new SearchForm(userId);
                                searchForm.ShowDialog();
                            }

                            this.Close();
                        }
                        else
                        {
                            MessageBox.Show("engk engkkkk. Invalid credentials!", "Login Failed", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        }
                    }
                }
            }
        }

        private void btnSignUp_Click(object sender, EventArgs e)
        {
            txtUsernameee.Visible = true;
            txtPassworddd.Visible = true;
            btnConfirmSignUp.Visible = true;
            lblUsernameee.Visible = true;
            lblPassworddd.Visible = true;
        }

        private bool SignUpUser(string username, string password, string role)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();

                // Check if username already exists
                string checkQuery = "SELECT COUNT(*) FROM userpass WHERE username = @username";
                using (SqlCommand checkCmd = new SqlCommand(checkQuery, conn))
                {
                    checkCmd.Parameters.AddWithValue("@username", username);
                    int count = Convert.ToInt32(checkCmd.ExecuteScalar());

                    if (count > 0)
                    {
                        MessageBox.Show("Username already exists.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return false;
                    }
                }

                // Insert new user
                string query = "INSERT INTO userpass (username, password, role) VALUES (@username, @password, 'user')";
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@username", username);
                    cmd.Parameters.AddWithValue("@password", password);
                    cmd.ExecuteNonQuery();
                }
            }

            MessageBox.Show("Sign-up successful! You can now log in.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
            return true;
        }

        private void btnConfirmSignUp_Click(object sender, EventArgs e)
        {
            string username = txtUsernameee.Text;
            string password = txtPassworddd.Text;

            if (!string.IsNullOrWhiteSpace(username) && !string.IsNullOrWhiteSpace(password))
            {
                if (SignUpUser(username, password, "user"))
                {
                    // Hide sign-up elements after successful registration
                    txtUsernameee.Visible = false;
                    txtPassworddd.Visible = false;
                    btnConfirmSignUp.Visible = false;
                    lblUsernameee.Visible = false;
                    lblPassworddd.Visible = false;
                }
            }
            else
            {
                MessageBox.Show("kulanggg bossing.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }
    }
}
