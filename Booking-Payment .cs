
-- payment .cs

using System;
using Microsoft.Data.SqlClient;
using System.Windows.Forms;
using System.Data;
namespace project_gui
{
    public partial class paymentform : Form
    {
        string connectionString = @"Data Source=localhost\SQLEXPRESS;Initial Catalog=""ciemna-system"";Integrated Security=True;TrustServerCertificate=True;";

        
        private int _customerID;
        private int _showID;
        private decimal _totalAmount;

        
        public paymentform() { InitializeComponent(); }

        // Custom constructor to receive data from the Booking form
        public paymentform(int customerID, int showID, decimal totalAmount)
        {
            InitializeComponent();
            _customerID = customerID;
            _showID = showID;
            _totalAmount = totalAmount;

            txtAmount.Text = _totalAmount.ToString("0.00"); 
        }

        private void btnConfirm_Click(object sender, EventArgs e)
        {
            if (cmbMethod.SelectedIndex == -1 || cmbStatus.SelectedIndex == -1)
            {
                MessageBox.Show("Please select a method and status.");
                return;
            }

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                try
                {
                    conn.Open();

                    //  Automatic ID Generation
                  
                    SqlCommand getIdCmd = new SqlCommand("SELECT ISNULL(MAX(payment_id), 0) + 1 FROM payment", conn);
                    int newPaymentId = Convert.ToInt32(getIdCmd.ExecuteScalar());

                    SqlCommand getBookingIdCmd = new SqlCommand("SELECT ISNULL(MAX(Booking_ID), 0) + 1 FROM Booking", conn);
                    int newBookingId = Convert.ToInt32(getBookingIdCmd.ExecuteScalar());

                    // Insert into Payment Table 
                    string insertPayment = "INSERT INTO payment (payment_id, Payment_Method, Amount, status) VALUES (@payId, @method, @amount, 'Completed')";
                    SqlCommand cmdPay = new SqlCommand(insertPayment, conn);
                    cmdPay.Parameters.AddWithValue("@payId", newPaymentId);
                    cmdPay.Parameters.AddWithValue("@method", cmbMethod.Text);
                    cmdPay.Parameters.AddWithValue("@amount", _totalAmount);
                    cmdPay.ExecuteNonQuery();

                    //  Insert into Booking Table Second 
                    string insertBooking = "INSERT INTO Booking (Booking_ID, Booking_Status, Total_Amount, Booking_date, B_shownumber, Customer_ID, Payment_ID) VALUES (@bookId, @bStatus, @amount, GETDATE(), @showId, @custId, @payId)";
                    SqlCommand cmdBook = new SqlCommand(insertBooking, conn);
                    cmdBook.Parameters.AddWithValue("@bookId", newBookingId);
                    cmdBook.Parameters.AddWithValue("@bStatus", cmbStatus.Text);
                    cmdBook.Parameters.AddWithValue("@amount", _totalAmount);
                    cmdBook.Parameters.AddWithValue("@showId", _showID);
                    cmdBook.Parameters.AddWithValue("@custId", _customerID);
                    cmdBook.Parameters.AddWithValue("@payId", newPaymentId); 
                    cmdBook.ExecuteNonQuery();

                    MessageBox.Show($"Booking Successful! \nYour Booking ID is: {newBookingId}");
                    this.Close(); // Closes the payment window
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Database Error: " + ex.Message);
                }
            }
        }
    }
}








-- booking .cs
using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;
using Microsoft.Data.SqlClient;

namespace project_gui
{
    public partial class booking : Form
    {
       
        string connectionString = @"Data Source=localhost\SQLEXPRESS;Initial Catalog=""ciemna-system"";Integrated Security=True;TrustServerCertificate=True;";

        public booking()
        {
            InitializeComponent();
        }

        // Runs automatically when the app starts
        private void booking_Load(object sender, EventArgs e)
        {
            LoadBookings();
            LoadDailyRevenue(); 
        }


        private void btnNext_Click(object sender, EventArgs e)
        {
            try
            {
                int customerId = int.Parse(txtCustomerID.Text);
                int showId = int.Parse(txtShowID.Text);
                decimal amount = 0;

                // Find the price of the show from the database
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    SqlCommand getPriceCmd = new SqlCommand("SELECT Price FROM Show WHERE show_Number = @showId", conn);
                    getPriceCmd.Parameters.AddWithValue("@showId", showId);

                    object result = getPriceCmd.ExecuteScalar();
                    if (result != null)
                    {
                        amount = Convert.ToDecimal(result);
                    }
                    else
                    {
                        MessageBox.Show("Show Number not found!");
                        return; // Stop if show doesn't exist
                    }
                }

                //  Open the Payment Form and pass the data to it
                paymentform payForm = new paymentform(customerId, showId, amount);
                if (payForm.ShowDialog() == DialogResult.OK || !payForm.Visible)
                {
                  
                    LoadBookings();
                    LoadDailyRevenue();
                }


            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: Please enter valid numbers. " + ex.Message);
            }
        }



        // Refreshes the DataGridView to show all bookings
        private void LoadBookings()
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                SqlDataAdapter da = new SqlDataAdapter("SELECT * FROM Booking", conn);
                DataTable dt = new DataTable();
                da.Fill(dt);
                dgvBookings.DataSource = dt;
            }
        }

        // Gets the total revenue for today 
        private void LoadDailyRevenue()
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand("SELECT dbo.fn_getDailyRevenue(GETDATE())", conn);
                object result = cmd.ExecuteScalar();
                lblRevenue.Text = "Today's Revenue: $" + (result != DBNull.Value ? result.ToString() : "0.00");
            }
        }

        // Cancels a booking using
        private void btnCancel_Click(object sender, EventArgs e)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                try
                {
                    conn.Open();
                    //  marks the booking as Cancelled 
                    string sql = "UPDATE Booking SET Booking_Status = 'Cancelled' WHERE Booking_ID = @id";
                    SqlCommand cmd = new SqlCommand(sql, conn);
                    cmd.Parameters.AddWithValue("@id", txtManageID.Text);

                    int rows = cmd.ExecuteNonQuery();

                    if (rows > 0)
                    {
                        MessageBox.Show("Booking Cancelled!");

                        
                        LoadBookings();     // Update the table so 'Cancelled' shows up
                        LoadDailyRevenue(); // Update the revenue label so the money is removed
                    }
                }
                catch (Exception ex) { MessageBox.Show(ex.Message); }
            }
        }
        // Updates the payment method 
        private void btnUpdate_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtManageID.Text) || cmbNewMethod.SelectedIndex == -1)
            {
                MessageBox.Show("Please enter a Booking ID and select a New Payment Method.");
                return;
            }

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                try
                {
                    conn.Open();
                    // update the 'payment' table using a Subquery 
                  
                    string sql = @"UPDATE payment 
                           SET Payment_Method = @method 
                           WHERE payment_id = (SELECT Payment_ID FROM Booking WHERE Booking_ID = @id)";

                    SqlCommand cmd = new SqlCommand(sql, conn);
                    cmd.Parameters.AddWithValue("@method", cmbNewMethod.Text);
                    cmd.Parameters.AddWithValue("@id", txtManageID.Text);

                    int rows = cmd.ExecuteNonQuery();

                    if (rows > 0)
                    {
                        MessageBox.Show("Payment method successfully updated!");
                        LoadBookings(); // Refresh the table
                    }
                    else
                    {
                        MessageBox.Show("Could not find a payment record for that Booking ID.");
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error updating payment method: " + ex.Message);
                }
            }
        }

        // Checks the combined status 
        private void btnStatus_Click(object sender, EventArgs e)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                try
                {
                    conn.Open();
                    SqlCommand cmd = new SqlCommand("SELECT dbo.get_payment_status(@BookingID)", conn);
                    cmd.Parameters.AddWithValue("@BookingID", int.Parse(txtManageID.Text));

                    string status = (string)cmd.ExecuteScalar();
                    lblStatusResult.Text = "Status: " + status;
                }
                catch (Exception ex) { MessageBox.Show("Error: " + ex.Message); }
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtManageID.Text))
            {
                MessageBox.Show("Please enter the Booking ID you want to confirm.");
                return;
            }

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                try
                {
                    conn.Open();
                    //   flip the status to 'Confirmed'
                    string sql = "UPDATE Booking SET Booking_Status = 'Confirmed' WHERE Booking_ID = @id";

                    SqlCommand cmd = new SqlCommand(sql, conn);
                    cmd.Parameters.AddWithValue("@id", txtManageID.Text);

                    int rows = cmd.ExecuteNonQuery();

                    if (rows > 0)
                    {
                        MessageBox.Show("Booking status updated to Confirmed!");

                        //  Refresh UI 
                        LoadBookings();
                        LoadDailyRevenue();
                    }
                    else
                    {
                        MessageBox.Show("Booking ID not found.");
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error updating status: " + ex.Message);
                }
            }
        }
    }
}




-- payment. desginer .cs
namespace project_gui
{
    partial class paymentform
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        private void InitializeComponent()
        {
            this.lblTitle = new System.Windows.Forms.Label();
            this.lblMethod = new System.Windows.Forms.Label();
            this.cmbMethod = new System.Windows.Forms.ComboBox();
            this.lblAmount = new System.Windows.Forms.Label();
            this.txtAmount = new System.Windows.Forms.TextBox();
            this.lblStatus = new System.Windows.Forms.Label();
            this.cmbStatus = new System.Windows.Forms.ComboBox();
            this.btnConfirm = new System.Windows.Forms.Button();
            this.SuspendLayout();
          
            this.lblTitle.AutoSize = true;
            this.lblTitle.Font = new System.Drawing.Font("Segoe UI", 16F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.lblTitle.Location = new System.Drawing.Point(40, 30);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(306, 45);
            this.lblTitle.TabIndex = 0;
            this.lblTitle.Text = "Complete Payment";
          
            this.lblMethod.AutoSize = true;
            this.lblMethod.Location = new System.Drawing.Point(45, 120);
            this.lblMethod.Name = "lblMethod";
            this.lblMethod.Size = new System.Drawing.Size(148, 25);
            this.lblMethod.TabIndex = 1;
            this.lblMethod.Text = "Payment Method:";
           
            this.cmbMethod.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbMethod.FormattingEnabled = true;
            this.cmbMethod.Items.AddRange(new object[] {
            "Visa",
            "MasterCard",
            "Cash",
            "Fawry",
            "Vodafone Cash",
            "InstaPay",
            "Apple Pay"});
            this.cmbMethod.Location = new System.Drawing.Point(200, 117);
            this.cmbMethod.Name = "cmbMethod";
            this.cmbMethod.Size = new System.Drawing.Size(200, 33);
            this.cmbMethod.TabIndex = 2;
          
            this.lblAmount.AutoSize = true;
            this.lblAmount.Location = new System.Drawing.Point(45, 180);
            this.lblAmount.Name = "lblAmount";
            this.lblAmount.Size = new System.Drawing.Size(122, 25);
            this.lblAmount.TabIndex = 3;
            this.lblAmount.Text = "Total Amount:";
           
            this.txtAmount.Location = new System.Drawing.Point(200, 177);
            this.txtAmount.Name = "txtAmount";
            this.txtAmount.ReadOnly = true;
            this.txtAmount.Size = new System.Drawing.Size(200, 31);
            this.txtAmount.TabIndex = 4;
           
            this.lblStatus.AutoSize = true;
            this.lblStatus.Location = new System.Drawing.Point(45, 240);
            this.lblStatus.Name = "lblStatus";
            this.lblStatus.Size = new System.Drawing.Size(133, 25);
            this.lblStatus.TabIndex = 5;
            this.lblStatus.Text = "Booking Status:";
          
            this.cmbStatus.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbStatus.FormattingEnabled = true;
            this.cmbStatus.Items.AddRange(new object[] {
            "Pending",
            "Confirmed"});
            this.cmbStatus.Location = new System.Drawing.Point(200, 237);
            this.cmbStatus.Name = "cmbStatus";
            this.cmbStatus.Size = new System.Drawing.Size(200, 33);
            this.cmbStatus.TabIndex = 6;
            
            this.btnConfirm.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.btnConfirm.Location = new System.Drawing.Point(200, 310);
            this.btnConfirm.Name = "btnConfirm";
            this.btnConfirm.Size = new System.Drawing.Size(200, 50);
            this.btnConfirm.TabIndex = 7;
            this.btnConfirm.Text = "Confirm && Save";
            this.btnConfirm.UseVisualStyleBackColor = true;
            this.btnConfirm.Click += new System.EventHandler(this.btnConfirm_Click);
           
            this.AutoScaleDimensions = new System.Drawing.SizeF(10F, 25F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(460, 420);
            this.Controls.Add(this.btnConfirm);
            this.Controls.Add(this.cmbStatus);
            this.Controls.Add(this.lblStatus);
            this.Controls.Add(this.txtAmount);
            this.Controls.Add(this.lblAmount);
            this.Controls.Add(this.cmbMethod);
            this.Controls.Add(this.lblMethod);
            this.Controls.Add(this.lblTitle);
            this.Name = "paymentform";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Checkout";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.Label lblMethod;
        private System.Windows.Forms.ComboBox cmbMethod;
        private System.Windows.Forms.Label lblAmount;
        private System.Windows.Forms.TextBox txtAmount;
        private System.Windows.Forms.Label lblStatus;
        private System.Windows.Forms.ComboBox cmbStatus;
        private System.Windows.Forms.Button btnConfirm;
    }
}




-- booking .desginer .cs


namespace project_gui
{
    partial class booking
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        private void InitializeComponent()
        {
            lblTitle = new Label();
            grpNewBooking = new GroupBox();
            labelCID = new Label();
            txtCustomerID = new TextBox();
            labelshnum = new Label();
            txtShowID = new TextBox();
            btnNext = new Button();
            grpManage = new GroupBox();
            btnConfirmStatus = new Button();
            dgvBookings = new DataGridView();
            lblManageID = new Label();
            txtManageID = new TextBox();
            btnCancel = new Button();
            lblNewMethod = new Label();
            cmbNewMethod = new ComboBox();
            btnUpdate = new Button();
            btnStatus = new Button();
            lblStatusResult = new Label();
            lblRevenue = new Label();
            grpNewBooking.SuspendLayout();
            grpManage.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dgvBookings).BeginInit();
            SuspendLayout();
            
            lblTitle.AutoSize = true;
            lblTitle.Font = new Font("Segoe UI", 16F, FontStyle.Bold);
            lblTitle.Location = new Point(20, 20);
            lblTitle.Name = "lblTitle";
            lblTitle.Size = new Size(382, 45);
            lblTitle.TabIndex = 0;
            lblTitle.Text = "Cinema Booking System";
           
            grpNewBooking.Controls.Add(labelCID);
            grpNewBooking.Controls.Add(txtCustomerID);
            grpNewBooking.Controls.Add(labelshnum);
            grpNewBooking.Controls.Add(txtShowID);
            grpNewBooking.Controls.Add(btnNext);
            grpNewBooking.Location = new Point(20, 80);
            grpNewBooking.Name = "grpNewBooking";
            grpNewBooking.Size = new Size(350, 250);
            grpNewBooking.TabIndex = 1;
            grpNewBooking.TabStop = false;
            grpNewBooking.Text = "1. Create New Booking";
           
            labelCID.AutoSize = true;
            labelCID.Location = new Point(20, 40);
            labelCID.Name = "labelCID";
            labelCID.Size = new Size(116, 25);
            labelCID.TabIndex = 0;
            labelCID.Text = "Customer ID:";
            
            txtCustomerID.Location = new Point(150, 37);
            txtCustomerID.Name = "txtCustomerID";
            txtCustomerID.Size = new Size(180, 31);
            txtCustomerID.TabIndex = 1;
           
            labelshnum.AutoSize = true;
            labelshnum.Location = new Point(20, 90);
            labelshnum.Name = "labelshnum";
            labelshnum.Size = new Size(130, 25);
            labelshnum.TabIndex = 2;
            labelshnum.Text = "Show Number:";
            
            txtShowID.Location = new Point(150, 87);
            txtShowID.Name = "txtShowID";
            txtShowID.Size = new Size(180, 31);
            txtShowID.TabIndex = 3;
             
            btnNext.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            btnNext.Location = new Point(20, 150);
            btnNext.Name = "btnNext";
            btnNext.Size = new Size(310, 50);
            btnNext.TabIndex = 4;
            btnNext.Text = "Proceed to Payment ➔";
            btnNext.UseVisualStyleBackColor = true;
            btnNext.Click += btnNext_Click;
           
            grpManage.Controls.Add(btnConfirmStatus);
            grpManage.Controls.Add(dgvBookings);
            grpManage.Controls.Add(lblManageID);
            grpManage.Controls.Add(txtManageID);
            grpManage.Controls.Add(btnCancel);
            grpManage.Controls.Add(lblNewMethod);
            grpManage.Controls.Add(cmbNewMethod);
            grpManage.Controls.Add(btnUpdate);
            grpManage.Controls.Add(btnStatus);
            grpManage.Controls.Add(lblStatusResult);
            grpManage.Location = new Point(390, 80);
            grpManage.Name = "grpManage";
            grpManage.Size = new Size(580, 480);
            grpManage.TabIndex = 2;
            grpManage.TabStop = false;
            grpManage.Text = "2. Manage Bookings";
          
            btnConfirmStatus.Location = new Point(398, 315);
            btnConfirmStatus.Name = "btnConfirmStatus";
            btnConfirmStatus.Size = new Size(162, 35);
            btnConfirmStatus.TabIndex = 9;
            btnConfirmStatus.Text = "confirm status";
            btnConfirmStatus.UseVisualStyleBackColor = true;
            btnConfirmStatus.Click += button1_Click;
           
            dgvBookings.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvBookings.Location = new Point(20, 40);
            dgvBookings.Name = "dgvBookings";
            dgvBookings.ReadOnly = true;
            dgvBookings.RowHeadersWidth = 62;
            dgvBookings.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvBookings.Size = new Size(540, 200);
            dgvBookings.TabIndex = 0;
           
            lblManageID.AutoSize = true;
            lblManageID.Location = new Point(20, 260);
            lblManageID.Name = "lblManageID";
            lblManageID.Size = new Size(158, 25);
            lblManageID.TabIndex = 1;
            lblManageID.Text = "Target Booking ID:";
           
            txtManageID.Location = new Point(180, 257);
            txtManageID.Name = "txtManageID";
            txtManageID.Size = new Size(100, 31);
            txtManageID.TabIndex = 2;
           
            btnCancel.Location = new Point(300, 254);
            btnCancel.Name = "btnCancel";
            btnCancel.Size = new Size(120, 35);
            btnCancel.TabIndex = 3;
            btnCancel.Text = "Cancel Booking";
            btnCancel.UseVisualStyleBackColor = true;
            btnCancel.Click += btnCancel_Click;
          
            NewMethod.AutoSize = true;
            lblNewMethod.Location = new Point(20, 380);
            lblNewMethod.Name = "lblNewMethod";
            lblNewMethod.Size = new Size(119, 25);
            lblNewMethod.TabIndex = 4;
            lblNewMethod.Text = "New Method:";
             
            cmbNewMethod.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbNewMethod.FormattingEnabled = true;
            cmbNewMethod.Items.AddRange(new object[] { "Visa", "MasterCard", "Cash", "Fawry", "Vodafone Cash", "InstaPay", "Apple Pay" });
            cmbNewMethod.Location = new Point(180, 380);
            cmbNewMethod.Name = "cmbNewMethod";
            cmbNewMethod.Size = new Size(180, 33);
            cmbNewMethod.TabIndex = 5;
            
            btnUpdate.Location = new Point(380, 378);
            btnUpdate.Name = "btnUpdate";
            btnUpdate.Size = new Size(180, 35);
            btnUpdate.TabIndex = 6;
            btnUpdate.Text = "Update ";
            btnUpdate.UseVisualStyleBackColor = true;
            btnUpdate.Click += btnUpdate_Click;
           
            btnStatus.Location = new Point(430, 254);
            btnStatus.Name = "btnStatus";
            btnStatus.Size = new Size(130, 35);
            btnStatus.TabIndex = 7;
            btnStatus.Text = "Check Status";
            btnStatus.UseVisualStyleBackColor = true;
            btnStatus.Click += btnStatus_Click;
            
            lblStatusResult.AutoSize = true;
            lblStatusResult.Font = new Font("Segoe UI", 9F, FontStyle.Italic);
            lblStatusResult.Location = new Point(138, 320);
            lblStatusResult.Name = "lblStatusResult";
            lblStatusResult.Size = new Size(192, 25);
            lblStatusResult.TabIndex = 8;
            lblStatusResult.Text = "Status will appear here";
           
            lblRevenue.AutoSize = true;
            lblRevenue.Font = new Font("Segoe UI", 12F, FontStyle.Bold);
            lblRevenue.ForeColor = Color.Green;
            lblRevenue.Location = new Point(20, 500);
            lblRevenue.Name = "lblRevenue";
            lblRevenue.Size = new Size(280, 32);
            lblRevenue.TabIndex = 3;
            lblRevenue.Text = "Today's Revenue: $0.00";
           
            AutoScaleDimensions = new SizeF(10F, 25F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1000, 600);
            Controls.Add(lblRevenue);
            Controls.Add(grpManage);
            Controls.Add(grpNewBooking);
            Controls.Add(lblTitle);
            Name = "booking";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Cinema Booking Dashboard";
            Load += booking_Load;
            grpNewBooking.ResumeLayout(false);
            grpNewBooking.PerformLayout();
            grpManage.ResumeLayout(false);
            grpManage.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)dgvBookings).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.GroupBox grpNewBooking;
        private System.Windows.Forms.Label labelCID;
        private System.Windows.Forms.TextBox txtCustomerID;
        private System.Windows.Forms.Label labelshnum;
        private System.Windows.Forms.TextBox txtShowID;
        private System.Windows.Forms.Button btnNext;

        private System.Windows.Forms.GroupBox grpManage;
        private System.Windows.Forms.DataGridView dgvBookings;
        private System.Windows.Forms.Label lblManageID;
        private System.Windows.Forms.TextBox txtManageID;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Label lblNewMethod;
        private System.Windows.Forms.ComboBox cmbNewMethod;
        private System.Windows.Forms.Button btnUpdate;
        private System.Windows.Forms.Button btnStatus;
        private System.Windows.Forms.Label lblStatusResult;

        private System.Windows.Forms.Label lblRevenue;
        private Button btnConfirmStatus;
    }
}
