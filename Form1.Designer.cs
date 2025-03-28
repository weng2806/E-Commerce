namespace E_Commerce
{
    partial class Form1
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.password = new System.Windows.Forms.TextBox();
            this.userName = new System.Windows.Forms.TextBox();
            this.login = new System.Windows.Forms.Button();
            this.pictureBox2 = new System.Windows.Forms.PictureBox();
            this.asda = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.btnSignUp = new System.Windows.Forms.Button();
            this.txtUsernameee = new System.Windows.Forms.TextBox();
            this.lblPassworddd = new System.Windows.Forms.Label();
            this.lblUsernameee = new System.Windows.Forms.Label();
            this.txtPassworddd = new System.Windows.Forms.TextBox();
            this.btnConfirmSignUp = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).BeginInit();
            this.SuspendLayout();
            // 
            // password
            // 
            this.password.BackColor = System.Drawing.Color.MediumPurple;
            this.password.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.password.Font = new System.Drawing.Font("Quicksand", 9.75F);
            this.password.ForeColor = System.Drawing.Color.White;
            this.password.Location = new System.Drawing.Point(552, 193);
            this.password.Name = "password";
            this.password.PasswordChar = '*';
            this.password.Size = new System.Drawing.Size(113, 24);
            this.password.TabIndex = 1;
            // 
            // userName
            // 
            this.userName.BackColor = System.Drawing.Color.MediumPurple;
            this.userName.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.userName.Font = new System.Drawing.Font("Quicksand", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.userName.ForeColor = System.Drawing.Color.White;
            this.userName.Location = new System.Drawing.Point(552, 120);
            this.userName.Name = "userName";
            this.userName.Size = new System.Drawing.Size(113, 24);
            this.userName.TabIndex = 0;
            // 
            // login
            // 
            this.login.BackColor = System.Drawing.Color.GhostWhite;
            this.login.FlatAppearance.BorderColor = System.Drawing.Color.White;
            this.login.FlatAppearance.BorderSize = 0;
            this.login.Location = new System.Drawing.Point(590, 238);
            this.login.Name = "login";
            this.login.Size = new System.Drawing.Size(75, 23);
            this.login.TabIndex = 4;
            this.login.Text = "Login";
            this.login.UseVisualStyleBackColor = false;
            this.login.Click += new System.EventHandler(this.login_Click);
            // 
            // pictureBox2
            // 
            this.pictureBox2.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("pictureBox2.BackgroundImage")));
            this.pictureBox2.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.pictureBox2.Location = new System.Drawing.Point(-4, -40);
            this.pictureBox2.Name = "pictureBox2";
            this.pictureBox2.Size = new System.Drawing.Size(811, 440);
            this.pictureBox2.TabIndex = 5;
            this.pictureBox2.TabStop = false;
            // 
            // asda
            // 
            this.asda.AutoSize = true;
            this.asda.BackColor = System.Drawing.Color.Thistle;
            this.asda.Location = new System.Drawing.Point(551, 104);
            this.asda.Name = "asda";
            this.asda.Size = new System.Drawing.Size(55, 13);
            this.asda.TabIndex = 6;
            this.asda.Text = "Username";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.Thistle;
            this.label1.Location = new System.Drawing.Point(549, 177);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(53, 13);
            this.label1.TabIndex = 7;
            this.label1.Text = "Password";
            // 
            // btnSignUp
            // 
            this.btnSignUp.BackColor = System.Drawing.Color.GhostWhite;
            this.btnSignUp.FlatAppearance.BorderColor = System.Drawing.Color.White;
            this.btnSignUp.FlatAppearance.BorderSize = 0;
            this.btnSignUp.Location = new System.Drawing.Point(590, 267);
            this.btnSignUp.Name = "btnSignUp";
            this.btnSignUp.Size = new System.Drawing.Size(75, 23);
            this.btnSignUp.TabIndex = 8;
            this.btnSignUp.Text = "Sign Up?";
            this.btnSignUp.UseVisualStyleBackColor = false;
            this.btnSignUp.Click += new System.EventHandler(this.btnSignUp_Click);
            // 
            // txtUsernameee
            // 
            this.txtUsernameee.Location = new System.Drawing.Point(472, 337);
            this.txtUsernameee.Name = "txtUsernameee";
            this.txtUsernameee.Size = new System.Drawing.Size(100, 20);
            this.txtUsernameee.TabIndex = 9;
            this.txtUsernameee.Visible = false;
            // 
            // lblPassworddd
            // 
            this.lblPassworddd.AutoSize = true;
            this.lblPassworddd.Location = new System.Drawing.Point(578, 340);
            this.lblPassworddd.Name = "lblPassworddd";
            this.lblPassworddd.Size = new System.Drawing.Size(53, 13);
            this.lblPassworddd.TabIndex = 10;
            this.lblPassworddd.Text = "Password";
            this.lblPassworddd.Visible = false;
            // 
            // lblUsernameee
            // 
            this.lblUsernameee.AutoSize = true;
            this.lblUsernameee.Location = new System.Drawing.Point(411, 340);
            this.lblUsernameee.Name = "lblUsernameee";
            this.lblUsernameee.Size = new System.Drawing.Size(55, 13);
            this.lblUsernameee.TabIndex = 11;
            this.lblUsernameee.Text = "Username";
            this.lblUsernameee.Visible = false;
            // 
            // txtPassworddd
            // 
            this.txtPassworddd.Location = new System.Drawing.Point(637, 337);
            this.txtPassworddd.Name = "txtPassworddd";
            this.txtPassworddd.Size = new System.Drawing.Size(100, 20);
            this.txtPassworddd.TabIndex = 13;
            this.txtPassworddd.Visible = false;
            // 
            // btnConfirmSignUp
            // 
            this.btnConfirmSignUp.BackColor = System.Drawing.Color.GhostWhite;
            this.btnConfirmSignUp.FlatAppearance.BorderColor = System.Drawing.Color.White;
            this.btnConfirmSignUp.FlatAppearance.BorderSize = 0;
            this.btnConfirmSignUp.Location = new System.Drawing.Point(530, 368);
            this.btnConfirmSignUp.Name = "btnConfirmSignUp";
            this.btnConfirmSignUp.Size = new System.Drawing.Size(110, 23);
            this.btnConfirmSignUp.TabIndex = 14;
            this.btnConfirmSignUp.Text = "Confirm Sign Up";
            this.btnConfirmSignUp.UseVisualStyleBackColor = false;
            this.btnConfirmSignUp.Visible = false;
            this.btnConfirmSignUp.Click += new System.EventHandler(this.btnConfirmSignUp_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.AliceBlue;
            this.ClientSize = new System.Drawing.Size(814, 430);
            this.Controls.Add(this.btnConfirmSignUp);
            this.Controls.Add(this.txtPassworddd);
            this.Controls.Add(this.lblUsernameee);
            this.Controls.Add(this.lblPassworddd);
            this.Controls.Add(this.txtUsernameee);
            this.Controls.Add(this.btnSignUp);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.asda);
            this.Controls.Add(this.login);
            this.Controls.Add(this.userName);
            this.Controls.Add(this.password);
            this.Controls.Add(this.pictureBox2);
            this.Name = "Form1";
            this.Text = "Form1";
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox password;
        private System.Windows.Forms.TextBox userName;
        private System.Windows.Forms.Button login;
        private System.Windows.Forms.PictureBox pictureBox2;
        private System.Windows.Forms.Label asda;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnSignUp;
        private System.Windows.Forms.TextBox txtUsernameee;
        private System.Windows.Forms.Label lblPassworddd;
        private System.Windows.Forms.Label lblUsernameee;
        private System.Windows.Forms.TextBox txtPassworddd;
        private System.Windows.Forms.Button btnConfirmSignUp;
    }
}

