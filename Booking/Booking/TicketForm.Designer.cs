namespace Booking
{
    partial class TicketForm
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
            label1 = new Label();
            label2 = new Label();
            button1 = new Button();
            label3 = new Label();
            label4 = new Label();
            label5 = new Label();
            label7 = new Label();
            label8 = new Label();
            TicketStatus = new ComboBox();
            ShowTime = new ComboBox();
            MovieTitle = new ComboBox();
            NumberOfTickets = new NumericUpDown();
            SeatNumber = new NumericUpDown();
            HallNumber = new ComboBox();
            TicketID = new TextBox();
            BookingID = new TextBox();
            label6 = new Label();
            ((System.ComponentModel.ISupportInitialize)NumberOfTickets).BeginInit();
            ((System.ComponentModel.ISupportInitialize)SeatNumber).BeginInit();
            SuspendLayout();
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(229, 39);
            label1.Name = "label1";
            label1.Size = new Size(194, 20);
            label1.TabIndex = 0;
            label1.Text = "Movie Title OR Show Name:";
            label1.Click += Label1_Click;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(229, 90);
            label2.Name = "label2";
            label2.Size = new Size(85, 20);
            label2.TabIndex = 1;
            label2.Text = "Show Time:";
            // 
            // button1
            // 
            button1.Location = new Point(611, 482);
            button1.Name = "button1";
            button1.Size = new Size(125, 29);
            button1.TabIndex = 2;
            button1.Text = "Reserve Ticket";
            button1.UseVisualStyleBackColor = true;
            button1.Click += btnReserve_Click;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(230, 139);
            label3.Name = "label3";
            label3.Size = new Size(70, 20);
            label3.TabIndex = 3;
            label3.Text = "Ticket ID:";
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Location = new Point(230, 187);
            label4.Name = "label4";
            label4.Size = new Size(95, 20);
            label4.TabIndex = 4;
            label4.Text = "Ticket Status:";
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Location = new Point(230, 233);
            label5.Name = "label5";
            label5.Size = new Size(133, 20);
            label5.TabIndex = 5;
            label5.Text = "Number of Tickets:";
            // 
            // label7
            // 
            label7.AutoSize = true;
            label7.Location = new Point(231, 337);
            label7.Name = "label7";
            label7.Size = new Size(97, 20);
            label7.TabIndex = 7;
            label7.Text = "Hall Number:";
            // 
            // label8
            // 
            label8.AutoSize = true;
            label8.Location = new Point(233, 390);
            label8.Name = "label8";
            label8.Size = new Size(99, 20);
            label8.TabIndex = 8;
            label8.Text = "Seat Number:";
            // 
            // TicketStatus
            // 
            TicketStatus.FormattingEnabled = true;
            TicketStatus.Items.AddRange(new object[] { "VIP", "Regular", "Gold" });
            TicketStatus.Location = new Point(433, 186);
            TicketStatus.Name = "TicketStatus";
            TicketStatus.Size = new Size(151, 28);
            TicketStatus.TabIndex = 9;
            // 
            // ShowTime
            // 
            ShowTime.FormattingEnabled = true;
            ShowTime.Items.AddRange(new object[] { "00:00 - 2:00", "2:00 - 4:00", "4:00 - 6:00", "6:00 - 8:00", "8:00 - 10:00", "10:00 - 12:00", "12:00 - 14:00", "14:00 - 16:00", "16:00 - 18:00", "18:00 - 20:00", "20:00 -22:00", "22:00 - 00:00" });
            ShowTime.Location = new Point(433, 89);
            ShowTime.Name = "ShowTime";
            ShowTime.Size = new Size(151, 28);
            ShowTime.TabIndex = 10;
            // 
            // MovieTitle
            // 
            MovieTitle.FormattingEnabled = true;
            MovieTitle.Location = new Point(433, 33);
            MovieTitle.Name = "MovieTitle";
            MovieTitle.Size = new Size(151, 28);
            MovieTitle.TabIndex = 11;
            // 
            // NumberOfTickets
            // 
            NumberOfTickets.Location = new Point(434, 235);
            NumberOfTickets.Name = "NumberOfTickets";
            NumberOfTickets.Size = new Size(150, 27);
            NumberOfTickets.TabIndex = 12;
            // 
            // SeatNumber
            // 
            SeatNumber.Location = new Point(435, 390);
            SeatNumber.Name = "SeatNumber";
            SeatNumber.Size = new Size(150, 27);
            SeatNumber.TabIndex = 13;
            // 
            // HallNumber
            // 
            HallNumber.FormattingEnabled = true;
            HallNumber.Items.AddRange(new object[] { "1", "2", "3", "4", "5", "6", "7", "8", "9", "10" });
            HallNumber.Location = new Point(434, 336);
            HallNumber.Name = "HallNumber";
            HallNumber.Size = new Size(151, 28);
            HallNumber.TabIndex = 14;
            // 
            // TicketID
            // 
            TicketID.Location = new Point(434, 138);
            TicketID.Name = "TicketID";
            TicketID.Size = new Size(150, 27);
            TicketID.TabIndex = 15;
            // 
            // BookingID
            // 
            BookingID.Location = new Point(435, 286);
            BookingID.Name = "BookingID";
            BookingID.Size = new Size(150, 27);
            BookingID.TabIndex = 17;
            // 
            // label6
            // 
            label6.AutoSize = true;
            label6.Location = new Point(231, 287);
            label6.Name = "label6";
            label6.Size = new Size(86, 20);
            label6.TabIndex = 16;
            label6.Text = "Booking ID:";
            label6.Click += Label1_Click;
            // 
            // TicketForm
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(820, 530);
            Controls.Add(BookingID);
            Controls.Add(label6);
            Controls.Add(TicketID);
            Controls.Add(HallNumber);
            Controls.Add(SeatNumber);
            Controls.Add(NumberOfTickets);
            Controls.Add(MovieTitle);
            Controls.Add(ShowTime);
            Controls.Add(TicketStatus);
            Controls.Add(label8);
            Controls.Add(label7);
            Controls.Add(label5);
            Controls.Add(label4);
            Controls.Add(label3);
            Controls.Add(button1);
            Controls.Add(label2);
            Controls.Add(label1);
            Name = "TicketForm";
            Text = "TicketForm";
            Load += TicketForm_Load;
            ((System.ComponentModel.ISupportInitialize)NumberOfTickets).EndInit();
            ((System.ComponentModel.ISupportInitialize)SeatNumber).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        private void Label1_Click(object sender, EventArgs e)
        {
            throw new NotImplementedException();
        }

        #endregion

        private Label label1;
        private Label label2;
        private Button button1;
        private Label label3;
        private Label label4;
        private Label label5;
        private Label label7;
        private Label label8;
        private ComboBox TicketStatus;
        private ComboBox ShowTime;
        private ComboBox MovieTitle;
        private NumericUpDown NumberOfTickets;
        private NumericUpDown SeatNumber;
        private ComboBox HallNumber;
        private TextBox TicketID;
        private TextBox BookingID;
        private Label label6;
    }
}