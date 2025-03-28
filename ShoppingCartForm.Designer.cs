namespace E_Commerce
{
    partial class ShoppingCartForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ShoppingCartForm));
            this.btnLogout = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.btnShippingCart = new System.Windows.Forms.Button();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.panel1 = new System.Windows.Forms.Panel();
            this.dgvCart = new System.Windows.Forms.DataGridView();
            this.lblTotalAmount = new System.Windows.Forms.Label();
            this.btnRemoveItem = new System.Windows.Forms.Button();
            this.btnPlaceOrder = new System.Windows.Forms.Button();
            this.txtCartIdd = new System.Windows.Forms.TextBox();
            this.txtRemoveQuantity = new System.Windows.Forms.TextBox();
            this.txtCartID = new System.Windows.Forms.Label();
            this.txtQuantityToRemove = new System.Windows.Forms.Label();
            this.btnBack = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvCart)).BeginInit();
            this.SuspendLayout();
            // 
            // btnLogout
            // 
            this.btnLogout.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(83)))), ((int)(((byte)(66)))), ((int)(((byte)(194)))));
            this.btnLogout.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(83)))), ((int)(((byte)(66)))), ((int)(((byte)(194)))));
            this.btnLogout.FlatAppearance.BorderSize = 5;
            this.btnLogout.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(83)))), ((int)(((byte)(66)))), ((int)(((byte)(194)))));
            this.btnLogout.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(83)))), ((int)(((byte)(66)))), ((int)(((byte)(194)))));
            this.btnLogout.Font = new System.Drawing.Font("Segoe UI", 14.25F);
            this.btnLogout.ForeColor = System.Drawing.Color.WhiteSmoke;
            this.btnLogout.Location = new System.Drawing.Point(6, 213);
            this.btnLogout.Name = "btnLogout";
            this.btnLogout.Size = new System.Drawing.Size(200, 36);
            this.btnLogout.TabIndex = 7;
            this.btnLogout.Text = "Logout";
            this.btnLogout.UseVisualStyleBackColor = false;
            this.btnLogout.Click += new System.EventHandler(this.btnLogout_Click);
            // 
            // button2
            // 
            this.button2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(83)))), ((int)(((byte)(66)))), ((int)(((byte)(194)))));
            this.button2.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(83)))), ((int)(((byte)(66)))), ((int)(((byte)(194)))));
            this.button2.FlatAppearance.BorderSize = 5;
            this.button2.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(83)))), ((int)(((byte)(66)))), ((int)(((byte)(194)))));
            this.button2.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(83)))), ((int)(((byte)(66)))), ((int)(((byte)(194)))));
            this.button2.Font = new System.Drawing.Font("Segoe UI", 14.25F);
            this.button2.ForeColor = System.Drawing.Color.WhiteSmoke;
            this.button2.Location = new System.Drawing.Point(6, 162);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(200, 40);
            this.button2.TabIndex = 6;
            this.button2.Text = "Reset Password";
            this.button2.UseVisualStyleBackColor = false;
            // 
            // btnShippingCart
            // 
            this.btnShippingCart.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(83)))), ((int)(((byte)(66)))), ((int)(((byte)(194)))));
            this.btnShippingCart.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(83)))), ((int)(((byte)(66)))), ((int)(((byte)(194)))));
            this.btnShippingCart.FlatAppearance.BorderSize = 5;
            this.btnShippingCart.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(83)))), ((int)(((byte)(66)))), ((int)(((byte)(194)))));
            this.btnShippingCart.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(83)))), ((int)(((byte)(66)))), ((int)(((byte)(194)))));
            this.btnShippingCart.Font = new System.Drawing.Font("Segoe UI", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnShippingCart.ForeColor = System.Drawing.Color.WhiteSmoke;
            this.btnShippingCart.Location = new System.Drawing.Point(6, 111);
            this.btnShippingCart.Name = "btnShippingCart";
            this.btnShippingCart.Size = new System.Drawing.Size(200, 40);
            this.btnShippingCart.TabIndex = 5;
            this.btnShippingCart.Text = "Shipping Cart";
            this.btnShippingCart.UseVisualStyleBackColor = false;
            // 
            // pictureBox1
            // 
            this.pictureBox1.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("pictureBox1.BackgroundImage")));
            this.pictureBox1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.pictureBox1.Dock = System.Windows.Forms.DockStyle.Top;
            this.pictureBox1.Location = new System.Drawing.Point(0, 0);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(216, 96);
            this.pictureBox1.TabIndex = 4;
            this.pictureBox1.TabStop = false;
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(83)))), ((int)(((byte)(66)))), ((int)(((byte)(194)))));
            this.panel1.Controls.Add(this.btnBack);
            this.panel1.Controls.Add(this.pictureBox1);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Left;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(216, 658);
            this.panel1.TabIndex = 8;
            // 
            // dgvCart
            // 
            this.dgvCart.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvCart.Location = new System.Drawing.Point(233, 99);
            this.dgvCart.Name = "dgvCart";
            this.dgvCart.Size = new System.Drawing.Size(553, 441);
            this.dgvCart.TabIndex = 9;
            // 
            // lblTotalAmount
            // 
            this.lblTotalAmount.AutoSize = true;
            this.lblTotalAmount.Font = new System.Drawing.Font("Segoe UI", 14.25F);
            this.lblTotalAmount.Location = new System.Drawing.Point(228, 71);
            this.lblTotalAmount.Name = "lblTotalAmount";
            this.lblTotalAmount.Size = new System.Drawing.Size(124, 25);
            this.lblTotalAmount.TabIndex = 10;
            this.lblTotalAmount.Text = "Total Amount";
            // 
            // btnRemoveItem
            // 
            this.btnRemoveItem.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.btnRemoveItem.Font = new System.Drawing.Font("Segoe UI", 14.25F);
            this.btnRemoveItem.Location = new System.Drawing.Point(358, 555);
            this.btnRemoveItem.Name = "btnRemoveItem";
            this.btnRemoveItem.Size = new System.Drawing.Size(146, 34);
            this.btnRemoveItem.TabIndex = 11;
            this.btnRemoveItem.Text = "Remove Item";
            this.btnRemoveItem.UseVisualStyleBackColor = false;
            this.btnRemoveItem.Click += new System.EventHandler(this.btnRemoveItem_Click_1);
            // 
            // btnPlaceOrder
            // 
            this.btnPlaceOrder.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.btnPlaceOrder.Font = new System.Drawing.Font("Segoe UI", 14.25F);
            this.btnPlaceOrder.Location = new System.Drawing.Point(233, 555);
            this.btnPlaceOrder.Name = "btnPlaceOrder";
            this.btnPlaceOrder.Size = new System.Drawing.Size(119, 34);
            this.btnPlaceOrder.TabIndex = 12;
            this.btnPlaceOrder.Text = "Place Order";
            this.btnPlaceOrder.UseVisualStyleBackColor = false;
            this.btnPlaceOrder.Click += new System.EventHandler(this.btnPlaceOrder_Click_1);
            // 
            // txtCartIdd
            // 
            this.txtCartIdd.Location = new System.Drawing.Point(404, 595);
            this.txtCartIdd.Name = "txtCartIdd";
            this.txtCartIdd.Size = new System.Drawing.Size(100, 20);
            this.txtCartIdd.TabIndex = 13;
            // 
            // txtRemoveQuantity
            // 
            this.txtRemoveQuantity.Location = new System.Drawing.Point(404, 621);
            this.txtRemoveQuantity.Name = "txtRemoveQuantity";
            this.txtRemoveQuantity.Size = new System.Drawing.Size(100, 20);
            this.txtRemoveQuantity.TabIndex = 14;
            // 
            // txtCartID
            // 
            this.txtCartID.AutoSize = true;
            this.txtCartID.Location = new System.Drawing.Point(360, 598);
            this.txtCartID.Name = "txtCartID";
            this.txtCartID.Size = new System.Drawing.Size(40, 13);
            this.txtCartID.TabIndex = 15;
            this.txtCartID.Text = "Cart ID";
            // 
            // txtQuantityToRemove
            // 
            this.txtQuantityToRemove.AutoSize = true;
            this.txtQuantityToRemove.Location = new System.Drawing.Point(353, 625);
            this.txtQuantityToRemove.Name = "txtQuantityToRemove";
            this.txtQuantityToRemove.Size = new System.Drawing.Size(49, 13);
            this.txtQuantityToRemove.TabIndex = 16;
            this.txtQuantityToRemove.Text = "Quantity ";
            // 
            // btnBack
            // 
            this.btnBack.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(83)))), ((int)(((byte)(66)))), ((int)(((byte)(194)))));
            this.btnBack.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(83)))), ((int)(((byte)(66)))), ((int)(((byte)(194)))));
            this.btnBack.FlatAppearance.BorderSize = 5;
            this.btnBack.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(83)))), ((int)(((byte)(66)))), ((int)(((byte)(194)))));
            this.btnBack.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(83)))), ((int)(((byte)(66)))), ((int)(((byte)(194)))));
            this.btnBack.Font = new System.Drawing.Font("Segoe UI", 14.25F);
            this.btnBack.ForeColor = System.Drawing.Color.WhiteSmoke;
            this.btnBack.Location = new System.Drawing.Point(6, 261);
            this.btnBack.Name = "btnBack";
            this.btnBack.Size = new System.Drawing.Size(200, 36);
            this.btnBack.TabIndex = 17;
            this.btnBack.Text = "Go Back ";
            this.btnBack.UseVisualStyleBackColor = false;
            this.btnBack.Click += new System.EventHandler(this.btnBack_Click);
            // 
            // ShoppingCartForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(798, 658);
            this.Controls.Add(this.txtQuantityToRemove);
            this.Controls.Add(this.txtCartID);
            this.Controls.Add(this.txtRemoveQuantity);
            this.Controls.Add(this.txtCartIdd);
            this.Controls.Add(this.btnPlaceOrder);
            this.Controls.Add(this.btnRemoveItem);
            this.Controls.Add(this.lblTotalAmount);
            this.Controls.Add(this.dgvCart);
            this.Controls.Add(this.btnLogout);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.btnShippingCart);
            this.Controls.Add(this.panel1);
            this.Name = "ShoppingCartForm";
            this.Text = "ShoppingCartForm";
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvCart)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnLogout;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Button btnShippingCart;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.DataGridView dgvCart;
        private System.Windows.Forms.Label lblTotalAmount;
        private System.Windows.Forms.Button btnRemoveItem;
        private System.Windows.Forms.Button btnPlaceOrder;
        private System.Windows.Forms.TextBox txtCartIdd;
        private System.Windows.Forms.TextBox txtRemoveQuantity;
        private System.Windows.Forms.Label txtCartID;
        private System.Windows.Forms.Label txtQuantityToRemove;
        private System.Windows.Forms.Button btnBack;
    }
}