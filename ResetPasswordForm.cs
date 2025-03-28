using System;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace E_Commerce
{
    public partial class ResetPasswordForm : Form
    {
        private string connectionString = "Data Source=PAMARAN\\SQLEXPRESS;Initial Catalog=E-Commerce;Integrated Security=True;TrustServerCertificate=True";
        private string username;

        // Constructor that receives the username
        public ResetPasswordForm(string loggedInUsername)
        {
            InitializeComponent();
            username = loggedInUsername;
            txtUsername.Text = username;
            txtUsername.ReadOnly = true; // Prevent editing
        }

        private void btnReset_Click(object sender, EventArgs e)
        {
            string oldPassword = txtOldPassword.Text.Trim();
            string newPassword = txtNewPassword.Text.Trim();

            if (string.IsNullOrWhiteSpace(oldPassword) || string.IsNullOrWhiteSpace(newPassword))
            {
                MessageBox.Show("Please fill in all fields.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();

                // Check if old password is correct
                string query = "SELECT password FROM userpass WHERE username = @username";

                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@username", username);
                    object result = cmd.ExecuteScalar();

                    if (result == null || result.ToString() != oldPassword)
                    {
                        MessageBox.Show("Incorrect old password!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }
                }

                // Update password
                string updateQuery = "UPDATE userpass SET password = @newPassword WHERE username = @username";

                using (SqlCommand cmdUpdate = new SqlCommand(updateQuery, conn))
                {
                    cmdUpdate.Parameters.AddWithValue("@newPassword", newPassword);
                    cmdUpdate.Parameters.AddWithValue("@username", username);
                    cmdUpdate.ExecuteNonQuery();
                }

                MessageBox.Show("Password updated successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.Close();
            }
        }

        private void label3_Click(object sender, EventArgs e)
        {

        }
    }
}