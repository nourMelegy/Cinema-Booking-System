// movie after sample db
using System;
using System.Data;
using Microsoft.Data.SqlClient;
using System.Drawing;
using System.Windows.Forms;

namespace CinemaApp
{
    public class MovieForm : Form
    {
        string connectionString = @"Data Source=.;Initial Catalog=MyProject;Integrated Security=True;Encrypt=False";

        TextBox txtEIDR, txtTitle, txtDuration, txtPrice, txtTimeSlots, txtSearch;
        ComboBox cmbAge;
        DataGridView grid;

        public MovieForm()
        {
            this.Text = "🎬 Cinema System – Movie Management";
            this.Size = new Size(1000, 650);
            this.BackColor = Color.FromArgb(240, 248, 255);

            InitUI();
            LoadData();
        }

        void InitUI()
        {
            Panel card = new Panel()
            {
                BackColor = Color.White,
                Location = new Point(30, 30),
                Size = new Size(500, 440),
                BorderStyle = BorderStyle.FixedSingle
            };
            this.Controls.Add(card);

            Label header = new Label()
            {
                Text = "Movie Management",
                Font = new Font("Segoe UI", 16, FontStyle.Bold),
                ForeColor = Color.FromArgb(0, 102, 204),
                AutoSize = true
            };
            header.Location = new Point((card.Width - header.PreferredWidth) / 2, 15);
            card.Controls.Add(header);

            int y = 70;

            txtEIDR = Field(card, "EIDR", y); y += 45;
            txtTitle = Field(card, "Title", y); y += 45;
            txtDuration = Field(card, "Duration", y); y += 45;
            txtPrice = Field(card, "Price", y); y += 45;
            txtTimeSlots = Field(card, "Time Slots", y); y += 45;

            // Age dropdown
            int formWidth = 320;
            int startX = (card.Width - formWidth) / 2;

            Label ageLbl = new Label()
            {
                Text = "Age",
                Width = 110,
                TextAlign = ContentAlignment.MiddleRight,
                Location = new Point(startX, y),
                Font = new Font("Segoe UI", 10, FontStyle.Bold)
            };

            cmbAge = new ComboBox()
            {
                Width = 200,
                Location = new Point(startX + 120, y),
                Font = new Font("Segoe UI", 10),
                DropDownStyle = ComboBoxStyle.DropDownList
            };
            cmbAge.Items.AddRange(new string[] { "G", "PG", "PG-13", "R", "+13", "+18" });

            card.Controls.Add(ageLbl);
            card.Controls.Add(cmbAge);

            y += 50;

            txtSearch = Field(card, "Search Title", y);

            // Buttons
            int btnX = 700;
            int btnY = 80;

            Button("Add Movie", btnX, btnY, Add); btnY += 60;
            Button("Delete Movie", btnX, btnY, Delete); btnY += 60;
            Button("Update Price %", btnX, btnY, UpdatePrice); btnY += 60;
            Button("Search", btnX, btnY, Search);

            // Grid
            grid = new DataGridView()
            {
                Location = new Point(30, 480),
                Size = new Size(920, 140),
                BackgroundColor = Color.White,
                BorderStyle = BorderStyle.None,
                AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill
            };

            grid.EnableHeadersVisualStyles = false;
            grid.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(0, 120, 215);
            grid.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;

            this.Controls.Add(grid);
        }

        TextBox Field(Control p, string name, int y)
        {
            int formWidth = 320;
            int startX = (p.Width - formWidth) / 2;

            Label lbl = new Label()
            {
                Text = name,
                Width = 110,
                TextAlign = ContentAlignment.MiddleRight,
                Location = new Point(startX, y),
                Font = new Font("Segoe UI", 10, FontStyle.Bold)
            };

            TextBox t = new TextBox()
            {
                Width = 200,
                Location = new Point(startX + 120, y),
                BackColor = Color.FromArgb(245, 250, 255),
                Font = new Font("Segoe UI", 10)
            };

            p.Controls.Add(lbl);
            p.Controls.Add(t);

            return t;
        }

        void Button(string text, int x, int y, EventHandler e)
        {
            Button b = new Button()
            {
                Text = text,
                Location = new Point(x, y),
                Width = 170,
                Height = 45,
                BackColor = Color.FromArgb(0, 120, 215),
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat,
                Font = new Font("Segoe UI", 10, FontStyle.Bold)
            };

            b.FlatAppearance.BorderSize = 0;
            b.MouseEnter += (s, ev) => b.BackColor = Color.FromArgb(30, 144, 255);
            b.MouseLeave += (s, ev) => b.BackColor = Color.FromArgb(0, 120, 215);

            b.Click += e;
            this.Controls.Add(b);
        }

        // ================= DATABASE OPERATIONS =================

        void LoadData()
        {
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                con.Open();
                SqlDataAdapter da = new SqlDataAdapter("SELECT * FROM Movie", con);
                DataTable dt = new DataTable();
                da.Fill(dt);
                grid.DataSource = dt;
            }
        }

        void Add(object s, EventArgs e)
        {
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                con.Open();

                SqlCommand cmd = new SqlCommand("AddMovie", con);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@EIDR", txtEIDR.Text);
                cmd.Parameters.AddWithValue("@Title", txtTitle.Text);
                cmd.Parameters.AddWithValue("@Duration", int.Parse(txtDuration.Text));
                cmd.Parameters.AddWithValue("@Price", decimal.Parse(txtPrice.Text));
                cmd.Parameters.AddWithValue("@TimeSlots", txtTimeSlots.Text);
                cmd.Parameters.AddWithValue("@AgeRating", cmbAge.Text);

                cmd.ExecuteNonQuery();
            }

            LoadData();
        }

        void Delete(object s, EventArgs e)
        {
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                con.Open();

                SqlCommand cmd = new SqlCommand("DeleteMovie", con);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@MovieEIDR", txtEIDR.Text);

                cmd.ExecuteNonQuery();
            }

            LoadData();
        }

        void UpdatePrice(object s, EventArgs e)
        {
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                con.Open();

                SqlCommand cmd = new SqlCommand("UpdateMoviePrice", con);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@MovieEIDR", txtEIDR.Text);
                cmd.Parameters.AddWithValue("@PricePercentage", decimal.Parse(txtPrice.Text));

                cmd.ExecuteNonQuery();
            }

            LoadData();
        }

        //void Search(object s, EventArgs e)
        //{
        //    using (SqlConnection con = new SqlConnection(connectionString))
        //    {
        //        con.Open();

        //        string query = "SELECT * FROM Movie WHERE Title LIKE @title";

        //        SqlCommand cmd = new SqlCommand(query, con);
        //        cmd.Parameters.AddWithValue("@title", "%" + txtSearch.Text + "%");

        //        SqlDataAdapter da = new SqlDataAdapter(cmd);
        //        DataTable dt = new DataTable();
        //        da.Fill(dt);

        //        grid.DataSource = dt;
        //    }
        //}

        void Search(object s, EventArgs e)
        {
            string keyword = txtSearch.Text.Trim();

            if (string.IsNullOrEmpty(keyword))
            {
                LoadData(); // FIXED
                return;
            }

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                string query = @"
            SELECT EIDR, Title, Duration, Price, TimeSlots, AgeRating
            FROM Movie
            WHERE 
            LOWER(Title) LIKE LOWER(@keyword)
            OR LOWER(EIDR) LIKE LOWER(@keyword)
            OR LOWER(AgeRating) LIKE LOWER(@keyword)";

                SqlDataAdapter da = new SqlDataAdapter(query, conn);

                da.SelectCommand.Parameters.AddWithValue("@keyword", "%" + keyword + "%");

                DataTable dt = new DataTable();
                da.Fill(dt);
                if (dt.Rows.Count == 0)
                {
                    MessageBox.Show("No movies found.");
                }
                grid.DataSource = dt;
            }
        }

        //private void LoadMovies()
        //{
        //    throw new NotImplementedException();
        //}
    }

    static class Program
    {
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.Run(new MovieForm());
        }
    }
}
