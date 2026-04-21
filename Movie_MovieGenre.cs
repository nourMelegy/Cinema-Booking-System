using System;
using System.Data;
using Microsoft.Data.SqlClient;
using System.Windows.Forms;

namespace movies
{
    public partial class Form1 : Form
    {
        string connStr = "Data Source=.;Initial Catalog=MyProject;Integrated Security=True;Encrypt=False";

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            LoadMovies();

        
        }

        // ================= LOAD ALL =================
        private void LoadMovies()
        {
            using (SqlConnection con = new SqlConnection(connStr))
            {
                SqlDataAdapter da = new SqlDataAdapter("GetAllMoviesWithGenres", con);
                da.SelectCommand.CommandType = CommandType.StoredProcedure;

                DataTable dt = new DataTable();
                da.Fill(dt);

                grid.DataSource = dt;
            }
        }

        // ================= ADD MOVIE ONLY =================
        private void btnAdd_Click(object sender, EventArgs e)
        {
            try
            {
                // ================= VALIDATION (PUT IT FIRST) =================

                if (string.IsNullOrWhiteSpace(txtEIDR.Text) ||
                    string.IsNullOrWhiteSpace(txtTitle.Text))
                {
                    MessageBox.Show("EIDR and Title are required");
                    return;
                }

                if (!int.TryParse(txtDuration.Text, out int duration))
                {
                    MessageBox.Show("Invalid duration");
                    return;
                }

                if (!decimal.TryParse(txtPrice.Text, out decimal price))
                {
                    MessageBox.Show("Invalid price");
                    return;
                }
                if (cmbAge.SelectedIndex == -1)
                {
                    MessageBox.Show("Select age rating!");
                    return;
                }
                // ================= SQL PART STARTS HERE =================

                using (SqlConnection con = new SqlConnection(connStr))
                {
                    SqlCommand cmd = new SqlCommand("AddMovie", con);
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@EIDR", txtEIDR.Text);
                    cmd.Parameters.AddWithValue("@Title", txtTitle.Text);

                    // use validated values (NOT parsing again)
                    cmd.Parameters.AddWithValue("@Duration", duration);
                    cmd.Parameters.AddWithValue("@Price", price);

                    cmd.Parameters.AddWithValue("@TimeSlots", txtTimeSlots.Text);
                    cmd.Parameters.AddWithValue("@AgeRating", cmbAge.Text);

                    con.Open();
                    cmd.ExecuteNonQuery();

                    //// ================= GENRE (ONLY IF YOU WANT AUTO-ADD) =================
                    //if (cmbGenre.SelectedIndex != -1)
                    //{
                    //    SqlCommand genreCmd = new SqlCommand("AddGenreToMovie", con);
                    //    genreCmd.CommandType = CommandType.StoredProcedure;

                    //    genreCmd.Parameters.AddWithValue("@EIDR", txtEIDR.Text);
                    //    genreCmd.Parameters.AddWithValue("@Genre", cmbGenre.Text);

                    //    genreCmd.ExecuteNonQuery();
                    //}
                }

                LoadMovies();
                MessageBox.Show("Movie added successfully!");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        // ================= ADD GENRE TO MOVIE =================
        private void btnAddGenre_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtEIDR.Text))
            {
                MessageBox.Show("Enter EIDR first!");
                return;
            }

            if (cmbGenre.SelectedIndex == -1)
            {
                MessageBox.Show("Select a genre!");
                return;
            }

            try
            {
                using (SqlConnection con = new SqlConnection(connStr))
                {
                    SqlCommand cmd = new SqlCommand("AddGenreToMovie", con);
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@EIDR", txtEIDR.Text);
                    cmd.Parameters.AddWithValue("@Genre", cmbGenre.Text);

                    con.Open();
                    cmd.ExecuteNonQuery();
                }

                LoadMovies();
                MessageBox.Show("Genre added successfully!");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        // ================= DELETE =================
        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtEIDR.Text))
            {
                MessageBox.Show("Enter EIDR first!");
                return;
            }

            try
            {
                using (SqlConnection con = new SqlConnection(connStr))
                {
                    SqlCommand cmd = new SqlCommand("DeleteMovie", con);
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@EIDR", txtEIDR.Text);

                    con.Open();
                    cmd.ExecuteNonQuery();
                }

                LoadMovies();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        // ================= UPDATE PRICE =================
        private void btnUpdate_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtEIDR.Text))
            {
                MessageBox.Show("Enter EIDR!");
                return;
            }

            if (!decimal.TryParse(txtPrice.Text, out decimal percentage))
            {
                MessageBox.Show("Enter valid percentage!");
                return;
            }

            try
            {
                using (SqlConnection con = new SqlConnection(connStr))
                {
                    SqlCommand cmd = new SqlCommand("UpdateMoviePrice", con);
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@EIDR", txtEIDR.Text);
                    cmd.Parameters.AddWithValue("@PricePercentage", percentage);

                    con.Open();
                    cmd.ExecuteNonQuery();
                }

                LoadMovies();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        // ================= SEARCH =================
        private void btnSearch_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtSearch.Text))
            {
                MessageBox.Show("Enter a movie title or EIDR to search.");
                LoadMovies();
                return;
            }

            using (SqlConnection con = new SqlConnection(connStr))
            {
                SqlCommand cmd = new SqlCommand("SearchMovie", con);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@Keyword", txtSearch.Text);

                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);

                grid.DataSource = dt;
            }
        }

        // ================= FILTER BY GENRE =================
        private void btnGetByGenre_Click(object sender, EventArgs e)
        {
            if (cmbGenre.SelectedIndex == -1)
            {
                MessageBox.Show("Select genre!");
                return;
            }

            using (SqlConnection con = new SqlConnection(connStr))
            {
                SqlCommand cmd = new SqlCommand("GetMoviesByGenre", con);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@Genre", cmbGenre.Text);

                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);

                grid.DataSource = dt;
            }
        }

        // ================= RESET =================
        private void btnReset_Click(object sender, EventArgs e)
        {
            txtEIDR.Clear();
            txtTitle.Clear();
            txtDuration.Clear();
            txtPrice.Clear();
            txtTimeSlots.Clear();
            txtSearch.Clear();

            cmbAge.SelectedIndex = -1;
            cmbGenre.SelectedIndex = -1;

            LoadMovies();
        }

        
    }
}
