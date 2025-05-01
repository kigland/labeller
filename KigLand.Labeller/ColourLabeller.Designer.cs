namespace KigLand.Labeller
{
    partial class frmColourLabeller
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
            picBox = new PictureBox();
            colourBox = new PictureBox();
            lblColour = new Label();
            btnOpen = new Button();
            listBoxFile = new ListBox();
            tableLayoutPanel1 = new TableLayoutPanel();
            flowLayoutPanel1 = new FlowLayoutPanel();
            listBoxColours = new ListBox();
            flowLayoutPanel2 = new FlowLayoutPanel();
            ((System.ComponentModel.ISupportInitialize)picBox).BeginInit();
            ((System.ComponentModel.ISupportInitialize)colourBox).BeginInit();
            tableLayoutPanel1.SuspendLayout();
            flowLayoutPanel1.SuspendLayout();
            SuspendLayout();
            // 
            // picBox
            // 
            picBox.Dock = DockStyle.Fill;
            picBox.Location = new Point(466, 108);
            picBox.Name = "picBox";
            picBox.Size = new Size(1719, 976);
            picBox.TabIndex = 0;
            picBox.TabStop = false;
            picBox.MouseClick += picBox_MouseClick;
            // 
            // colourBox
            // 
            colourBox.Location = new Point(3, 3);
            colourBox.Name = "colourBox";
            colourBox.Size = new Size(82, 80);
            colourBox.TabIndex = 1;
            colourBox.TabStop = false;
            // 
            // lblColour
            // 
            lblColour.AutoSize = true;
            lblColour.Location = new Point(91, 0);
            lblColour.Name = "lblColour";
            lblColour.Size = new Size(187, 39);
            lblColour.TabIndex = 2;
            lblColour.Text = "Colour Result";
            // 
            // btnOpen
            // 
            btnOpen.Dock = DockStyle.Fill;
            btnOpen.Location = new Point(3, 3);
            btnOpen.Name = "btnOpen";
            btnOpen.Size = new Size(457, 99);
            btnOpen.TabIndex = 3;
            btnOpen.Text = "Open Folder";
            btnOpen.UseVisualStyleBackColor = true;
            btnOpen.Click += btnOpen_Click;
            // 
            // listBoxFile
            // 
            listBoxFile.Dock = DockStyle.Fill;
            listBoxFile.FormattingEnabled = true;
            listBoxFile.DrawMode = DrawMode.OwnerDrawFixed;
            listBoxFile.Location = new Point(3, 108);
            listBoxFile.Name = "listBoxFile";
            listBoxFile.Size = new Size(457, 976);
            listBoxFile.TabIndex = 4;
            listBoxFile.DrawItem += listBoxFile_DrawItem;
            listBoxColours.DrawMode = DrawMode.OwnerDrawFixed;
            listBoxColours.DrawItem += listBoxColours_DrawItem;
            listBoxFile.SelectedIndexChanged += listBoxFile_SelectedIndexChanged;
            // 
            // tableLayoutPanel1
            // 
            tableLayoutPanel1.ColumnCount = 3;
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 21.1788216F));
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 78.82118F));
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 395F));
            tableLayoutPanel1.Controls.Add(btnOpen, 0, 0);
            tableLayoutPanel1.Controls.Add(listBoxFile, 0, 1);
            tableLayoutPanel1.Controls.Add(flowLayoutPanel1, 1, 0);
            tableLayoutPanel1.Controls.Add(picBox, 1, 1);
            tableLayoutPanel1.Controls.Add(listBoxColours, 2, 1);
            tableLayoutPanel1.Controls.Add(flowLayoutPanel2, 2, 0);
            tableLayoutPanel1.Dock = DockStyle.Fill;
            tableLayoutPanel1.Location = new Point(0, 0);
            tableLayoutPanel1.Name = "tableLayoutPanel1";
            tableLayoutPanel1.RowCount = 2;
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 9.692898F));
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 90.3071F));
            tableLayoutPanel1.Size = new Size(2584, 1087);
            tableLayoutPanel1.TabIndex = 5;
            // 
            // flowLayoutPanel1
            // 
            flowLayoutPanel1.Controls.Add(colourBox);
            flowLayoutPanel1.Controls.Add(lblColour);
            flowLayoutPanel1.Dock = DockStyle.Fill;
            flowLayoutPanel1.Location = new Point(466, 3);
            flowLayoutPanel1.Name = "flowLayoutPanel1";
            flowLayoutPanel1.Size = new Size(1719, 99);
            flowLayoutPanel1.TabIndex = 5;
            // 
            // listBoxColours
            // 
            listBoxColours.Dock = DockStyle.Fill;
            listBoxColours.FormattingEnabled = true;
            listBoxColours.Location = new Point(2191, 108);
            listBoxColours.Name = "listBoxColours";
            listBoxColours.Size = new Size(390, 976);
            listBoxColours.TabIndex = 6;
            // 
            // flowLayoutPanel2
            // 
            flowLayoutPanel2.Location = new Point(2191, 3);
            flowLayoutPanel2.Name = "flowLayoutPanel2";
            flowLayoutPanel2.Size = new Size(390, 99);
            flowLayoutPanel2.TabIndex = 7;
            // 
            // frmColourLabeller
            // 
            AutoScaleDimensions = new SizeF(16F, 39F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(2584, 1087);
            Controls.Add(tableLayoutPanel1);
            KeyPreview = true;
            Name = "frmColourLabeller";
            Text = "KigLabeller";
            KeyDown += frmColourLabeller_KeyDown;
            ((System.ComponentModel.ISupportInitialize)picBox).EndInit();
            ((System.ComponentModel.ISupportInitialize)colourBox).EndInit();
            tableLayoutPanel1.ResumeLayout(false);
            flowLayoutPanel1.ResumeLayout(false);
            flowLayoutPanel1.PerformLayout();
            ResumeLayout(false);
        }

        #endregion

        private PictureBox picBox;
        private PictureBox colourBox;
        private Label lblColour;
        private Button btnOpen;
        private ListBox listBoxFile;
        private TableLayoutPanel tableLayoutPanel1;
        private FlowLayoutPanel flowLayoutPanel1;
        private ListBox listBoxColours;
        private FlowLayoutPanel flowLayoutPanel2;
    }
}
