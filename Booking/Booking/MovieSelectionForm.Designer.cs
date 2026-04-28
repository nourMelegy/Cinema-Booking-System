namespace Booking
{
    partial class MovieSelectionForm
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            DGVMovie = new DataGridView();
            RefreshMovie = new Button();
            ((System.ComponentModel.ISupportInitialize)DGVMovie).BeginInit();
            SuspendLayout();
            // 
            // DGVMovie
            // 
            DGVMovie.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            DGVMovie.Location = new Point(2, -3);
            DGVMovie.Name = "DGVMovie";
            DGVMovie.RowHeadersWidth = 51;
            DGVMovie.Size = new Size(799, 457);
            DGVMovie.TabIndex = 0;
            // 
            // RefreshMovie
            // 
            RefreshMovie.Location = new Point(46, 56);
            RefreshMovie.Name = "RefreshMovie";
            RefreshMovie.Size = new Size(121, 29);
            RefreshMovie.TabIndex = 1;
            RefreshMovie.Text = "Refresh Movies";
            RefreshMovie.UseVisualStyleBackColor = true;
            RefreshMovie.Click += RefreshMovie_Click;
            // 
            // MovieSelectionForm
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Controls.Add(RefreshMovie);
            Controls.Add(DGVMovie);
            Name = "MovieSelectionForm";
            Text = "Movie Selection Form";
            ((System.ComponentModel.ISupportInitialize)DGVMovie).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private DataGridView DGVMovie;
        private Button RefreshMovie;
    }
}
