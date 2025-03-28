namespace E_Commerce
{
    partial class PaymentForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(PaymentForm));
            this.lblTotal = new System.Windows.Forms.Label();
            this.txtAccountNumber = new System.Windows.Forms.TextBox();
            this.btnConfirmPayment = new System.Windows.Forms.Button();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.cmbPaymentMethod = new System.Windows.Forms.ComboBox();
            this.lblCODMessage = new System.Windows.Forms.Label();
            this.awit = new System.Windows.Forms.Label();
            this.aa = new System.Windows.Forms.Label();
            this.cmbBank = new System.Windows.Forms.ComboBox();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // lblTotal
            // 
            this.lblTotal.AutoSize = true;
            this.lblTotal.Font = new System.Drawing.Font("Segoe UI", 14.25F);
            this.lblTotal.Location = new System.Drawing.Point(132, 9);
            this.lblTotal.Name = "lblTotal";
            this.lblTotal.Size = new System.Drawing.Size(52, 25);
            this.lblTotal.TabIndex = 0;
            this.lblTotal.Text = "Total";
            // 
            // txtAccountNumber
            // 
            this.txtAccountNumber.Location = new System.Drawing.Point(578, 229);
            this.txtAccountNumber.Name = "txtAccountNumber";
            this.txtAccountNumber.Size = new System.Drawing.Size(100, 20);
            this.txtAccountNumber.TabIndex = 3;
            this.txtAccountNumber.Visible = false;
            // 
            // btnConfirmPayment
            // 
            this.btnConfirmPayment.Font = new System.Drawing.Font("Segoe UI", 14.25F);
            this.btnConfirmPayment.Location = new System.Drawing.Point(509, 401);
            this.btnConfirmPayment.Name = "btnConfirmPayment";
            this.btnConfirmPayment.Size = new System.Drawing.Size(169, 37);
            this.btnConfirmPayment.TabIndex = 5;
            this.btnConfirmPayment.Text = "Confirm Payment";
            this.btnConfirmPayment.UseVisualStyleBackColor = true;
            this.btnConfirmPayment.Click += new System.EventHandler(this.btnConfirmPayment_Click);
            // 
            // pictureBox1
            // 
            this.pictureBox1.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("pictureBox1.BackgroundImage")));
            this.pictureBox1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.pictureBox1.Dock = System.Windows.Forms.DockStyle.Top;
            this.pictureBox1.Location = new System.Drawing.Point(0, 0);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(800, 451);
            this.pictureBox1.TabIndex = 6;
            this.pictureBox1.TabStop = false;
            // 
            // cmbPaymentMethod
            // 
            this.cmbPaymentMethod.Font = new System.Drawing.Font("Segoe UI", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmbPaymentMethod.FormattingEnabled = true;
            this.cmbPaymentMethod.Location = new System.Drawing.Point(526, 169);
            this.cmbPaymentMethod.Name = "cmbPaymentMethod";
            this.cmbPaymentMethod.Size = new System.Drawing.Size(152, 28);
            this.cmbPaymentMethod.TabIndex = 7;
            this.cmbPaymentMethod.SelectedIndexChanged += new System.EventHandler(this.cmbPaymentMethod_SelectedIndexChanged_1);
            // 
            // lblCODMessage
            // 
            this.lblCODMessage.AutoSize = true;
            this.lblCODMessage.Font = new System.Drawing.Font("Segoe UI", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblCODMessage.Location = new System.Drawing.Point(134, 68);
            this.lblCODMessage.Name = "lblCODMessage";
            this.lblCODMessage.Size = new System.Drawing.Size(50, 25);
            this.lblCODMessage.TabIndex = 8;
            this.lblCODMessage.Text = "COD";
            // 
            // awit
            // 
            this.awit.AutoSize = true;
            this.awit.Location = new System.Drawing.Point(506, 206);
            this.awit.Name = "awit";
            this.awit.Size = new System.Drawing.Size(66, 13);
            this.awit.TabIndex = 9;
            this.awit.Text = "Bank Name:";
            this.awit.Visible = false;
            // 
            // aa
            // 
            this.aa.AutoSize = true;
            this.aa.Location = new System.Drawing.Point(482, 232);
            this.aa.Name = "aa";
            this.aa.Size = new System.Drawing.Size(92, 17);
            this.aa.TabIndex = 10;
            this.aa.Text = "Account Number:";
            this.aa.UseCompatibleTextRendering = true;
            this.aa.Visible = false;
            // 
            // cmbBank
            // 
            this.cmbBank.FormattingEnabled = true;
            this.cmbBank.Location = new System.Drawing.Point(579, 202);
            this.cmbBank.Name = "cmbBank";
            this.cmbBank.Size = new System.Drawing.Size(99, 21);
            this.cmbBank.TabIndex = 11;
            this.cmbBank.Visible = false;
            // 
            // PaymentForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.cmbBank);
            this.Controls.Add(this.aa);
            this.Controls.Add(this.awit);
            this.Controls.Add(this.lblCODMessage);
            this.Controls.Add(this.cmbPaymentMethod);
            this.Controls.Add(this.btnConfirmPayment);
            this.Controls.Add(this.txtAccountNumber);
            this.Controls.Add(this.lblTotal);
            this.Controls.Add(this.pictureBox1);
            this.Name = "PaymentForm";
            this.Text = "PaymentForm";
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblTotal;
        private System.Windows.Forms.TextBox txtAccountNumber;
        private System.Windows.Forms.Button btnConfirmPayment;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.ComboBox cmbPaymentMethod;
        private System.Windows.Forms.Label lblCODMessage;
        private System.Windows.Forms.Label awit;
        private System.Windows.Forms.Label aa;
        private System.Windows.Forms.ComboBox cmbBank;
    }
}