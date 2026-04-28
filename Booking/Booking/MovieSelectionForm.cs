using BookingFlow;
using System.Data;
using System.Data.SqlClient;

namespace Booking
{
    public partial class MovieSelectionForm : Form
    {
        public MovieSelectionForm()
        {
            InitializeComponent();
        }

        private void RefreshMovie_Click(object sender, EventArgs e)
        {
            using (SqlConnection con = DBHelper.GetConnection()) 
            {
                try
                {
                    con.Open();

                    string sql = "SELECT EIDR, Title, Duration, AgeRating FROM Movie";
                    SqlCommand cmd = new SqlCommand(sql, con);

                    SqlDataReader reader = cmd.ExecuteReader();

                    DataTable dt = new DataTable();
                    dt.Load(reader);
                    DGVMovie.DataSource = dt;

        }
                catch (Exception ex)
                {
                    MessageBox.Show("Error: " + ex.Message);
                }
            }
        }
    }
}
