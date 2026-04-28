using BookingFlow;
using Booking;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Booking
{
    public partial class PaymentForm : Form
    {
        public PaymentForm()
        {
            InitializeComponent();
        }

        public void ConfirmBooking_Click(object sender, EventArgs e)
        {
            using (SqlConnection con = DBHelper.GetConnection())
            {
                con.Open();
                SqlTransaction transaction = con.BeginTransaction();

                try
                {

                    string sqlPay = @"INSERT INTO Payment (PaymentID, PaymentMethod, Amount, PaymentStatus) 
                              VALUES (@pid, @method, @amount, @status)";
                    SqlCommand cmdPay = new SqlCommand(sqlPay, con, transaction);
                    cmdPay.Parameters.AddWithValue("@pid", label1.Text);
                    cmdPay.Parameters.AddWithValue("@method", label2.Text);
                    cmdPay.Parameters.AddWithValue("@amount", decimal.Parse(label3.Text));
                    cmdPay.Parameters.AddWithValue("@status", comboBox1.Text);
                    cmdPay.ExecuteNonQuery();

                    // 2. Insert into Booking Table
                    // Note: B_shownumber and B_custid must exist in your DB already
                    string sqlBook = @"INSERT INTO Booking (BookingID, BookingStatus, BookingDate, B_shownumber, B_custid, B_paymentid) 
                               VALUES (@bid, @bstatus, @date, @show, @cust, @pid)";
                    SqlCommand cmdBook = new SqlCommand(sqlBook, con, transaction);
                    cmdBook.Parameters.AddWithValue("@bid", this.Tag.ToString());
                    cmdBook.Parameters.AddWithValue("@bstatus", "Confirmed");
                    cmdBook.Parameters.AddWithValue("@date", DateTime.Now);
                    cmdBook.Parameters.AddWithValue("@show", 1);
                    cmdBook.Parameters.AddWithValue("@cust", 1);
                    cmdBook.Parameters.AddWithValue("@pid", label1.Text);
                    cmdBook.ExecuteNonQuery();

                    transaction.Commit();
                    MessageBox.Show("Booking and Payment Successful!");
                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                    MessageBox.Show("Error: " + ex.Message);
                }
            }
        }
    }
}
