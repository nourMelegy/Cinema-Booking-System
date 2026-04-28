namespace Booking
{
    partial class SeatsForm
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
            SeatNum = new NumericUpDown();
            lblSeatNum = new Label();
            HallNum = new ComboBox();
            lblHallNum = new Label();
            lblSeatArrangement = new Label();
            SeatArrangement = new ComboBox();
            BuyTicket = new Button();
            ((System.ComponentModel.ISupportInitialize)SeatNum).BeginInit();
            SuspendLayout();
            // 
            // SeatNum
            // 
            SeatNum.Location = new Point(373, 125);
            SeatNum.Name = "SeatNum";
            SeatNum.Size = new Size(150, 27);
            SeatNum.TabIndex = 15;
            // 
            // lblSeatNum
            // 
            lblSeatNum.AutoSize = true;
            lblSeatNum.Location = new Point(226, 127);
            lblSeatNum.Name = "lblSeatNum";
            lblSeatNum.Size = new Size(99, 20);
            lblSeatNum.TabIndex = 14;
            lblSeatNum.Text = "Seat Number:";
            // 
            // HallNum
            // 
            HallNum.AutoCompleteCustomSource.AddRange(new string[] { "1", "2", "3", "4", "5", "6", "7" , "8", "9", "10" });
            HallNum.FormattingEnabled = true;
            HallNum.Items.AddRange(new object[] { "1", "2", "3", "4", "5", "6", "7", "8", "9", "10" });
            HallNum.Location = new Point(373, 62);
            HallNum.Name = "HallNum";
            HallNum.Size = new Size(151, 28);
            HallNum.TabIndex = 17;
            // 
            // lblHallNum
            // 
            lblHallNum.AutoSize = true;
            lblHallNum.Location = new Point(225, 65);
            lblHallNum.Name = "lblHallNum";
            lblHallNum.Size = new Size(97, 20);
            lblHallNum.TabIndex = 16;
            lblHallNum.Text = "Hall Number:";
            lblHallNum.Click += HallNum_Click;
            // 
            // lblSeatArrangement
            // 
            lblSeatArrangement.AutoSize = true;
            lblSeatArrangement.Location = new Point(226, 197);
            lblSeatArrangement.Name = "lblSeatArrangement";
            lblSeatArrangement.Size = new Size(132, 20);
            lblSeatArrangement.TabIndex = 18;
            lblSeatArrangement.Text = "Seat Arrangement:";
            // 
            // SeatArrangement
            // 
            SeatArrangement.AutoCompleteCustomSource.AddRange(new string[] { "Front Right", "Front Left", "Front Middle", "Back Right", "Back Left", "Back Middle" });
            SeatArrangement.FormattingEnabled = true;
            SeatArrangement.Items.AddRange(new object[] { "Front Right", "Front Left", "Front Middle", "Back Right", "Back Left", "Back Middle" });
            SeatArrangement.Location = new Point(373, 194);
            SeatArrangement.Name = "SeatArrangement";
            SeatArrangement.Size = new Size(151, 28);
            SeatArrangement.TabIndex = 19;
            SeatArrangement.SelectedIndexChanged += SeatArrangement_SelectedIndexChanged;
            // 
            // BuyTicket
            // 
            BuyTicket.Location = new Point(505, 313);
            BuyTicket.Name = "BuyTicket";
            BuyTicket.Size = new Size(143, 29);
            BuyTicket.TabIndex = 20;
            BuyTicket.Text = "Buy Ticket";
            BuyTicket.UseVisualStyleBackColor = true;
            BuyTicket.Click += BuyTicket_Click;
            // 
            // SeatsForm
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Controls.Add(BuyTicket);
            Controls.Add(SeatArrangement);
            Controls.Add(lblSeatArrangement);
            Controls.Add(HallNum);
            Controls.Add(lblHallNum);
            Controls.Add(SeatNum);
            Controls.Add(lblSeatNum);
            Name = "SeatsForm";
            Text = "SeatsForm";
            ((System.ComponentModel.ISupportInitialize)SeatNum).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private NumericUpDown SeatNum;
        private Label lblSeatNum;
        private ComboBox HallNum;
        private Label lblHallNum;
        private Label lblSeatArrangement;
        private ComboBox SeatArrangement;
        private Button BuyTicket;
    }
}