namespace MM1_map_editor
{
    partial class Form1
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
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.tBoxTitle = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.textBox2 = new System.Windows.Forms.TextBox();
            this.textBox4 = new System.Windows.Forms.TextBox();
            this.textBox3 = new System.Windows.Forms.TextBox();
            this.textBox5 = new System.Windows.Forms.TextBox();
            this.textBox6 = new System.Windows.Forms.TextBox();
            this.textBox10 = new System.Windows.Forms.TextBox();
            this.textBox9 = new System.Windows.Forms.TextBox();
            this.textBox8 = new System.Windows.Forms.TextBox();
            this.textBox7 = new System.Windows.Forms.TextBox();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // pictureBox1
            // 
            this.pictureBox1.Location = new System.Drawing.Point(4, 34);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(360, 360);
            this.pictureBox1.TabIndex = 0;
            this.pictureBox1.TabStop = false;
            this.pictureBox1.Paint += new System.Windows.Forms.PaintEventHandler(this.pictureBox1_Paint);
            this.pictureBox1.MouseClick += new System.Windows.Forms.MouseEventHandler(this.pictureBox1_MouseClick);
            // 
            // tBoxTitle
            // 
            this.tBoxTitle.BackColor = System.Drawing.SystemColors.Menu;
            this.tBoxTitle.Location = new System.Drawing.Point(4, 6);
            this.tBoxTitle.MaxLength = 90;
            this.tBoxTitle.Name = "tBoxTitle";
            this.tBoxTitle.ShortcutsEnabled = false;
            this.tBoxTitle.Size = new System.Drawing.Size(360, 20);
            this.tBoxTitle.TabIndex = 1;
            this.tBoxTitle.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.tBoxTitle.Enter += new System.EventHandler(this.DeselectTextBoxContent);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Enabled = false;
            this.label3.Location = new System.Drawing.Point(152, 554);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(0, 13);
            this.label3.TabIndex = 9;
            this.label3.Enter += new System.EventHandler(this.DeselectTextBoxContent);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Enabled = false;
            this.label2.Location = new System.Drawing.Point(152, 606);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(0, 13);
            this.label2.TabIndex = 12;
            this.label2.Enter += new System.EventHandler(this.DeselectTextBoxContent);
            // 
            // textBox1
            // 
            this.textBox1.BackColor = System.Drawing.SystemColors.Menu;
            this.textBox1.Enabled = false;
            this.textBox1.Location = new System.Drawing.Point(5, 397);
            this.textBox1.MaxLength = 90;
            this.textBox1.Name = "textBox1";
            this.textBox1.ShortcutsEnabled = false;
            this.textBox1.Size = new System.Drawing.Size(359, 20);
            this.textBox1.TabIndex = 2;
            this.textBox1.Text = "A - ";
            this.textBox1.Enter += new System.EventHandler(this.DeselectTextBoxContent);
            // 
            // textBox2
            // 
            this.textBox2.BackColor = System.Drawing.SystemColors.Menu;
            this.textBox2.Enabled = false;
            this.textBox2.Location = new System.Drawing.Point(5, 422);
            this.textBox2.MaxLength = 90;
            this.textBox2.Name = "textBox2";
            this.textBox2.ShortcutsEnabled = false;
            this.textBox2.Size = new System.Drawing.Size(359, 20);
            this.textBox2.TabIndex = 3;
            this.textBox2.Text = "B - ";
            this.textBox2.Enter += new System.EventHandler(this.DeselectTextBoxContent);
            // 
            // textBox4
            // 
            this.textBox4.BackColor = System.Drawing.SystemColors.Menu;
            this.textBox4.Enabled = false;
            this.textBox4.Location = new System.Drawing.Point(5, 472);
            this.textBox4.MaxLength = 90;
            this.textBox4.Name = "textBox4";
            this.textBox4.ShortcutsEnabled = false;
            this.textBox4.Size = new System.Drawing.Size(359, 20);
            this.textBox4.TabIndex = 5;
            this.textBox4.Text = "D - ";
            this.textBox4.Enter += new System.EventHandler(this.DeselectTextBoxContent);
            // 
            // textBox3
            // 
            this.textBox3.BackColor = System.Drawing.SystemColors.Menu;
            this.textBox3.Enabled = false;
            this.textBox3.Location = new System.Drawing.Point(5, 447);
            this.textBox3.MaxLength = 90;
            this.textBox3.Name = "textBox3";
            this.textBox3.ShortcutsEnabled = false;
            this.textBox3.Size = new System.Drawing.Size(359, 20);
            this.textBox3.TabIndex = 4;
            this.textBox3.Text = "C - ";
            this.textBox3.Enter += new System.EventHandler(this.DeselectTextBoxContent);
            // 
            // textBox5
            // 
            this.textBox5.BackColor = System.Drawing.SystemColors.Menu;
            this.textBox5.Enabled = false;
            this.textBox5.Location = new System.Drawing.Point(5, 497);
            this.textBox5.MaxLength = 90;
            this.textBox5.Name = "textBox5";
            this.textBox5.ShortcutsEnabled = false;
            this.textBox5.Size = new System.Drawing.Size(359, 20);
            this.textBox5.TabIndex = 6;
            this.textBox5.Text = "E - ";
            this.textBox5.Enter += new System.EventHandler(this.DeselectTextBoxContent);
            // 
            // textBox6
            // 
            this.textBox6.BackColor = System.Drawing.SystemColors.Menu;
            this.textBox6.Enabled = false;
            this.textBox6.Location = new System.Drawing.Point(5, 522);
            this.textBox6.MaxLength = 90;
            this.textBox6.Name = "textBox6";
            this.textBox6.ShortcutsEnabled = false;
            this.textBox6.Size = new System.Drawing.Size(359, 20);
            this.textBox6.TabIndex = 7;
            this.textBox6.Text = "F - ";
            this.textBox6.Enter += new System.EventHandler(this.DeselectTextBoxContent);
            // 
            // textBox10
            // 
            this.textBox10.BackColor = System.Drawing.SystemColors.Menu;
            this.textBox10.Enabled = false;
            this.textBox10.Location = new System.Drawing.Point(4, 622);
            this.textBox10.MaxLength = 90;
            this.textBox10.Name = "textBox10";
            this.textBox10.ShortcutsEnabled = false;
            this.textBox10.Size = new System.Drawing.Size(360, 20);
            this.textBox10.TabIndex = 11;
            this.textBox10.Text = "J - ";
            this.textBox10.Enter += new System.EventHandler(this.DeselectTextBoxContent);
            // 
            // textBox9
            // 
            this.textBox9.BackColor = System.Drawing.SystemColors.Menu;
            this.textBox9.Enabled = false;
            this.textBox9.Location = new System.Drawing.Point(5, 597);
            this.textBox9.MaxLength = 90;
            this.textBox9.Name = "textBox9";
            this.textBox9.ShortcutsEnabled = false;
            this.textBox9.Size = new System.Drawing.Size(359, 20);
            this.textBox9.TabIndex = 10;
            this.textBox9.Text = "I - ";
            this.textBox9.Enter += new System.EventHandler(this.DeselectTextBoxContent);
            // 
            // textBox8
            // 
            this.textBox8.BackColor = System.Drawing.SystemColors.Menu;
            this.textBox8.Enabled = false;
            this.textBox8.Location = new System.Drawing.Point(5, 572);
            this.textBox8.MaxLength = 90;
            this.textBox8.Name = "textBox8";
            this.textBox8.ShortcutsEnabled = false;
            this.textBox8.Size = new System.Drawing.Size(359, 20);
            this.textBox8.TabIndex = 9;
            this.textBox8.Text = "H - ";
            this.textBox8.Enter += new System.EventHandler(this.DeselectTextBoxContent);
            // 
            // textBox7
            // 
            this.textBox7.BackColor = System.Drawing.SystemColors.Menu;
            this.textBox7.Enabled = false;
            this.textBox7.Location = new System.Drawing.Point(5, 547);
            this.textBox7.MaxLength = 90;
            this.textBox7.Name = "textBox7";
            this.textBox7.ShortcutsEnabled = false;
            this.textBox7.Size = new System.Drawing.Size(359, 20);
            this.textBox7.TabIndex = 8;
            this.textBox7.Text = "G - ";
            this.textBox7.Enter += new System.EventHandler(this.DeselectTextBoxContent);
            // 
            // Form1
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.ClientSize = new System.Drawing.Size(368, 649);
            this.Controls.Add(this.textBox7);
            this.Controls.Add(this.textBox8);
            this.Controls.Add(this.textBox9);
            this.Controls.Add(this.textBox10);
            this.Controls.Add(this.textBox6);
            this.Controls.Add(this.textBox5);
            this.Controls.Add(this.textBox3);
            this.Controls.Add(this.textBox4);
            this.Controls.Add(this.textBox2);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.tBoxTitle);
            this.Controls.Add(this.pictureBox1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Location = new System.Drawing.Point(900, 0);
            this.Name = "Form1";
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.Text = "MM1 map editor";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Form1_FormClosing);
            this.Load += new System.EventHandler(this.Form1_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.Form1_KeyDown);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.TextBox tBoxTitle;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.TextBox textBox2;
        private System.Windows.Forms.TextBox textBox4;
        private System.Windows.Forms.TextBox textBox3;
        private System.Windows.Forms.TextBox textBox5;
        private System.Windows.Forms.TextBox textBox6;
        private System.Windows.Forms.TextBox textBox10;
        private System.Windows.Forms.TextBox textBox9;
        private System.Windows.Forms.TextBox textBox8;
        private System.Windows.Forms.TextBox textBox7;
    }
}

