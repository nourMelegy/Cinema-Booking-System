using System;
using System.Data;
using System.Windows.Forms;
using System.Data.SqlClient; 
using BookingFlow;
using Booking;

namespace Booking
{
    public partial class SeatsForm : Form
    {
        public SeatsForm()
        {
            InitializeComponent();
        }

        public void BuyTicket_Click(object sender, EventArgs e)
        {
            int selectedHall = int.Parse(HallNum.SelectedItem.ToString());
            int selectedSeat = (int)SeatNum.Value;
            string arrangement = SeatArrangement.SelectedItem.ToString();

            TicketForm nextForm = new TicketForm();

            nextForm.Text = selectedHall.ToString() + "-" + arrangement + " - Seat " + selectedSeat.ToString();

            nextForm.Show();
            this.Hide();
        }

        private void HallNum_Click(object sender, EventArgs e)
        {

        }

        private void SeatArrangement_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}