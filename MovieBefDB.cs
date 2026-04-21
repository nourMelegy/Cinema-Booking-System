//movie gui bef db conn
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace CinemaApp
{
    public class Movie
    {
        public string EIDR { get; set; }
        public string Title { get; set; }
        public int Duration { get; set; }
        public decimal Price { get; set; }
        public string TimeSlots { get; set; }
        public string AgeRating { get; set; }
    }

    public class MovieForm : Form
    {
        List<Movie> movies = new List<Movie>();

        TextBox txtEIDR, txtTitle, txtDuration, txtPrice, txtTimeSlots, txtSearch;
        ComboBox cmbAge;
        DataGridView grid;

        public MovieForm()
        {
            this.Text = " Cinema System – Movie Management";
            this.Size = new Size(1000, 650);
            this.BackColor = Color.FromArgb(240, 248, 255);

            InitUI();
            LoadSample();
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

            // Header
            Label header = new Label()
            {
                Text = " Movie Management",
                Font = new Font("Segoe UI", 16, FontStyle.Bold),
                ForeColor = Color.FromArgb(0, 102, 204),
                AutoSize = true
            };

            header.Location = new Point(
                (card.Width - header.PreferredWidth) / 2,
                15
            );

            card.Controls.Add(header);

            int y = 70;

            txtEIDR = Field(card, "EIDR", y); y += 45;
            txtTitle = Field(card, "Title", y); y += 45;
            txtDuration = Field(card, "Duration", y); y += 45;
            txtPrice = Field(card, "Price", y); y += 45;
            txtTimeSlots = Field(card, "Time Slots", y); y += 45;

            // Age ComboBox (fixed alignment)
            int formWidth = 320;
            int startX = (card.Width - formWidth) / 2;

            int labelWidth = 110;
            int textWidth = 200;

            Label ageLbl = new Label()
            {
                Text = "Age",
                Width = labelWidth,
                TextAlign = ContentAlignment.MiddleRight,
                Location = new Point(startX, y),
                Font = new Font("Segoe UI", 10, FontStyle.Bold)
            };

            cmbAge = new ComboBox()
            {
                Width = textWidth,
                Location = new Point(startX + labelWidth + 10, y),
                Font = new Font("Segoe UI", 10),
                DropDownStyle = ComboBoxStyle.DropDownList
            };

            cmbAge.Items.AddRange(new string[] { "G", "PG", "PG-13", "R", "+13", "+18" });

            card.Controls.Add(ageLbl);
            card.Controls.Add(cmbAge);

            y += 50;

            txtSearch = Field(card, "Search Title", y);

            // Buttons (aligned nicely)
            int btnX = 700;
            int btnY = 80;

            Button("Add Movie", btnX, btnY, Add); btnY += 60;
            Button("Delete Movie", btnX, btnY, Delete); btnY += 60;
            Button("Update Price %", btnX, btnY, Update); btnY += 60;
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
            grid.DefaultCellStyle.SelectionBackColor = Color.LightBlue;

            this.Controls.Add(grid);
        }

        TextBox Field(Control p, string name, int y)
        {
            int formWidth = 320;
            int startX = (p.Width - formWidth) / 2;

            int labelWidth = 110;
            int textWidth = 200;

            Label lbl = new Label()
            {
                Text = name,
                Width = labelWidth,
                TextAlign = ContentAlignment.MiddleRight,
                Location = new Point(startX, y),
                Font = new Font("Segoe UI", 10, FontStyle.Bold)
            };

            TextBox t = new TextBox()
            {
                Width = textWidth,
                Location = new Point(startX + labelWidth + 10, y),
                BorderStyle = BorderStyle.FixedSingle,
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

            // Hover effect
            b.MouseEnter += (s, ev) => b.BackColor = Color.FromArgb(30, 144, 255);
            b.MouseLeave += (s, ev) => b.BackColor = Color.FromArgb(0, 120, 215);

            b.Click += e;
            this.Controls.Add(b);
        }

        void LoadSample()
        {
            movies.AddRange(new List<Movie>
            {
                new Movie { EIDR="E001", Title="Inception", Duration=148, Price=13, TimeSlots="15:00", AgeRating="PG-13"},
                new Movie { EIDR="E002", Title="Shrek", Duration=90, Price=9.5m, TimeSlots="12:00", AgeRating="PG"},
                new Movie { EIDR="E003", Title="Interstellar", Duration=169, Price=14, TimeSlots="18:00", AgeRating="PG-13"},
                new Movie { EIDR="E004", Title="The Conjuring", Duration=112, Price=12, TimeSlots="21:00", AgeRating="R"},
                new Movie { EIDR="E005", Title="Toy Story", Duration=100, Price=10, TimeSlots="11:00", AgeRating="G"},
                new Movie { EIDR="E006", Title="Titanic", Duration=194, Price=12.5m, TimeSlots="19:00", AgeRating="PG-13"},
                new Movie { EIDR="E007", Title="Deadpool", Duration=108, Price=13.5m, TimeSlots="22:00", AgeRating="R"},
                new Movie { EIDR="E008", Title="Encanto", Duration=102, Price=10.5m, TimeSlots="14:00", AgeRating="PG"},
                new Movie { EIDR="E009", Title="Avatar", Duration=162, Price=14, TimeSlots="20:00", AgeRating="PG-13"},
                new Movie { EIDR="E010", Title="Joker", Duration=122, Price=12, TimeSlots="21:30", AgeRating="R"}
            });

            RefreshGrid();
        }

        void Add(object s, EventArgs e)
        {
            if (!int.TryParse(txtDuration.Text, out int duration) ||
                !decimal.TryParse(txtPrice.Text, out decimal price))
            {
                MessageBox.Show("Invalid numeric input!");
                return;
            }

            movies.Add(new Movie
            {
                EIDR = txtEIDR.Text,
                Title = txtTitle.Text,
                Duration = duration,
                Price = price,
                TimeSlots = txtTimeSlots.Text,
                AgeRating = cmbAge.Text
            });

            RefreshGrid();
        }

        void Delete(object s, EventArgs e)
        {
            var m = movies.FirstOrDefault(x => x.EIDR == txtEIDR.Text);
            if (m != null) movies.Remove(m);

            RefreshGrid();
        }

        void Update(object s, EventArgs e)
        {
            var m = movies.FirstOrDefault(x => x.EIDR == txtEIDR.Text);
            if (m != null && decimal.TryParse(txtPrice.Text, out decimal percent))
            {
                m.Price += m.Price * percent / 100;
            }

            RefreshGrid();
        }

        void Search(object s, EventArgs e)
        {
            string keyword = txtSearch.Text.Trim().ToLower();

            if (string.IsNullOrEmpty(keyword))
            {
                RefreshGrid();
                return;
            }

            var res = movies
                .Where(m => m.Title.ToLower().Contains(keyword))
                .ToList();

            grid.DataSource = null;
            grid.DataSource = res;
        }

        void RefreshGrid()
        {
            grid.DataSource = null;
            grid.DataSource = movies;
        }
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
