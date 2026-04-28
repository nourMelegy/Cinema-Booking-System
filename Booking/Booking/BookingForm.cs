using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;
using BookingFlow;
using Booking;


namespace Booking
{
    public partial class BookingForm : Form
    {
        public BookingForm()
        {
            InitializeComponent();
        }

        public void GoPay_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(label1.Text) || string.IsNullOrEmpty(label6.Text))
            {
                MessageBox.Show("Please fill in all IDs.");
                return;
            }

            PaymentForm payForm = new PaymentForm();

            payForm.Text = this.label6.Text;
            payForm.Text = "500";

            payForm.Show();
            this.Hide();
        }
    }
}
