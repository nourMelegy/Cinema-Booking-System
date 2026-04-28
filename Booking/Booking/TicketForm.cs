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
using Booking;
using BookingFlow;


namespace Booking
{
    public partial class TicketForm : Form
    {
        public TicketForm()
        {
            InitializeComponent();
            this.Load += new EventHandler(TicketForm_Load);
        }
        private void btnReserve_Click(object sender, EventArgs e)
        {
            using (SqlConnection con = DBHelper.GetConnection())
            {
                try
                {
                    con.Open();


                    string sql = @"INSERT INTO Ticket (TicketID, TicketStatus, NumberOfTickets, BookingID, HNumber, Seatnumber) 
                                   VALUES (@TicketID, @Status, @Count, @BookingID, @HallNum, @SeatNum)";

                    SqlCommand cmd = new SqlCommand(sql, con);

                    cmd.Parameters.AddWithValue("@TicketID", TicketID.Text);
                    cmd.Parameters.AddWithValue("@Status", TicketStatus.Text);
                    cmd.Parameters.AddWithValue("@Count", (int)NumberOfTickets.Value);
                    cmd.Parameters.AddWithValue("@BookingID", int.Parse(BookingID.Text));
                    cmd.Parameters.AddWithValue("@HallNum", int.Parse(HallNumber.Text));
                    cmd.Parameters.AddWithValue("@SeatNum", int.Parse(SeatNumber.Text));

                    int rowsAffected = cmd.ExecuteNonQuery();

                    if (rowsAffected > 0)
                    {
                        MessageBox.Show("Ticket successfully reserved!");
                    }
                }
                catch (SqlException ex)
                {
                    MessageBox.Show("Database Error: " + ex.Message);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Input Error: Ensure IDs are numbers.");
                }

            }
        }

        private void TicketForm_Load(object sender, EventArgs e)
        {
            using (SqlConnection con = DBHelper.GetConnection())
            {
                try
                {
                    string sql = "SELECT Title FROM Movie";
                    SqlDataAdapter da = new SqlDataAdapter(sql, con);
                    DataTable dt = new DataTable(); da.Fill(dt);

                    MovieTitle.DataSource = dt;
                    MovieTitle.DisplayMember = "Title";

                    TicketStatus.Items.AddRange(new string[] { "Regular", "Gold", "VIP" });
                    TicketStatus.SelectedIndex = 0;

                    DateTime time = DateTime.Today.AddHours(10);
                    for (int i = 0; i < 6; i++)
                    {
                        ShowTime.Items.Add(time.ToShortTimeString());
                        time = time.AddHours(2);
                    }

                    for (int h = 1; h <= 10; h++)
                    {
                        HallNumber.Items.Add(h);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Initialization Error: " + ex.Message);
                }
            }
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }
    }
}