namespace WindowsFormsApplication1
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.height = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.width = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.button1 = new System.Windows.Forms.Button();
            this.Name = new System.Windows.Forms.Label();
            this.tbName = new System.Windows.Forms.TextBox();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.Breakable = new System.Windows.Forms.RadioButton();
            this.Unbreakable = new System.Windows.Forms.RadioButton();
            this.Flag = new System.Windows.Forms.RadioButton();
            this.RedHQ = new System.Windows.Forms.RadioButton();
            this.GreenHQ = new System.Windows.Forms.RadioButton();
            this.BlueHQ = new System.Windows.Forms.RadioButton();
            this.YellowHQ = new System.Windows.Forms.RadioButton();
            this.None = new System.Windows.Forms.RadioButton();
            this.generate = new System.Windows.Forms.Button();
            this.random = new System.Windows.Forms.CheckBox();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.SuspendLayout();
            // 
            // height
            // 
            this.height.Location = new System.Drawing.Point(311, 61);
            this.height.Name = "height";
            this.height.Size = new System.Drawing.Size(100, 22);
            this.height.TabIndex = 2;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(248, 64);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(57, 17);
            this.label1.TabIndex = 2;
            this.label1.Text = "Height :";
            // 
            // width
            // 
            this.width.Location = new System.Drawing.Point(82, 59);
            this.width.Name = "width";
            this.width.Size = new System.Drawing.Size(100, 22);
            this.width.TabIndex = 1;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(24, 64);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(52, 17);
            this.label2.TabIndex = 3;
            this.label2.Text = "Width :";
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(583, 54);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(144, 37);
            this.button1.TabIndex = 3;
            this.button1.Text = "Generate Grid";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // Name
            // 
            this.Name.AutoSize = true;
            this.Name.Location = new System.Drawing.Point(23, 21);
            this.Name.Name = "Name";
            this.Name.Size = new System.Drawing.Size(53, 17);
            this.Name.TabIndex = 7;
            this.Name.Text = "Name :";
            // 
            // tbName
            // 
            this.tbName.Location = new System.Drawing.Point(86, 18);
            this.tbName.Name = "tbName";
            this.tbName.Size = new System.Drawing.Size(100, 22);
            this.tbName.TabIndex = 0;
            // 
            // dataGridView1
            // 
            this.dataGridView1.AllowUserToAddRows = false;
            this.dataGridView1.AllowUserToDeleteRows = false;
            this.dataGridView1.AllowUserToResizeColumns = false;
            this.dataGridView1.AllowUserToResizeRows = false;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.ColumnHeadersVisible = false;
            this.dataGridView1.Location = new System.Drawing.Point(27, 117);
            this.dataGridView1.MultiSelect = false;
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.ReadOnly = true;
            this.dataGridView1.RowHeadersVisible = false;
            this.dataGridView1.RowTemplate.Height = 24;
            this.dataGridView1.ScrollBars = System.Windows.Forms.ScrollBars.None;
            this.dataGridView1.Size = new System.Drawing.Size(1059, 766);
            this.dataGridView1.TabIndex = 8;
            this.dataGridView1.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView1_CellClick);
            // 
            // Breakable
            // 
            this.Breakable.AutoSize = true;
            this.Breakable.Location = new System.Drawing.Point(1092, 117);
            this.Breakable.Name = "Breakable";
            this.Breakable.Size = new System.Drawing.Size(93, 21);
            this.Breakable.TabIndex = 4;
            this.Breakable.TabStop = true;
            this.Breakable.Text = "Breakable";
            this.Breakable.UseVisualStyleBackColor = true;
            // 
            // Unbreakable
            // 
            this.Unbreakable.AutoSize = true;
            this.Unbreakable.Location = new System.Drawing.Point(1092, 144);
            this.Unbreakable.Name = "Unbreakable";
            this.Unbreakable.Size = new System.Drawing.Size(110, 21);
            this.Unbreakable.TabIndex = 5;
            this.Unbreakable.TabStop = true;
            this.Unbreakable.Text = "Unbreakable";
            this.Unbreakable.UseVisualStyleBackColor = true;
            // 
            // Flag
            // 
            this.Flag.AutoSize = true;
            this.Flag.Location = new System.Drawing.Point(1092, 171);
            this.Flag.Name = "Flag";
            this.Flag.Size = new System.Drawing.Size(56, 21);
            this.Flag.TabIndex = 6;
            this.Flag.TabStop = true;
            this.Flag.Text = "Flag";
            this.Flag.UseVisualStyleBackColor = true;
            // 
            // RedHQ
            // 
            this.RedHQ.AutoSize = true;
            this.RedHQ.Location = new System.Drawing.Point(1092, 198);
            this.RedHQ.Name = "RedHQ";
            this.RedHQ.Size = new System.Drawing.Size(80, 21);
            this.RedHQ.TabIndex = 7;
            this.RedHQ.TabStop = true;
            this.RedHQ.Text = "Red HQ";
            this.RedHQ.UseVisualStyleBackColor = true;
            // 
            // GreenHQ
            // 
            this.GreenHQ.AutoSize = true;
            this.GreenHQ.Location = new System.Drawing.Point(1092, 225);
            this.GreenHQ.Name = "GreenHQ";
            this.GreenHQ.Size = new System.Drawing.Size(94, 21);
            this.GreenHQ.TabIndex = 8;
            this.GreenHQ.TabStop = true;
            this.GreenHQ.Text = "Green HQ";
            this.GreenHQ.UseVisualStyleBackColor = true;
            // 
            // BlueHQ
            // 
            this.BlueHQ.AutoSize = true;
            this.BlueHQ.Location = new System.Drawing.Point(1091, 252);
            this.BlueHQ.Name = "BlueHQ";
            this.BlueHQ.Size = new System.Drawing.Size(82, 21);
            this.BlueHQ.TabIndex = 9;
            this.BlueHQ.TabStop = true;
            this.BlueHQ.Text = "Blue HQ";
            this.BlueHQ.UseVisualStyleBackColor = true;
            // 
            // YellowHQ
            // 
            this.YellowHQ.AutoSize = true;
            this.YellowHQ.Location = new System.Drawing.Point(1091, 279);
            this.YellowHQ.Name = "YellowHQ";
            this.YellowHQ.Size = new System.Drawing.Size(94, 21);
            this.YellowHQ.TabIndex = 10;
            this.YellowHQ.TabStop = true;
            this.YellowHQ.Text = "Yellow HQ";
            this.YellowHQ.UseVisualStyleBackColor = true;
            // 
            // None
            // 
            this.None.AutoSize = true;
            this.None.Location = new System.Drawing.Point(1091, 306);
            this.None.Name = "None";
            this.None.Size = new System.Drawing.Size(63, 21);
            this.None.TabIndex = 11;
            this.None.TabStop = true;
            this.None.Text = "None";
            this.None.UseVisualStyleBackColor = true;
            // 
            // generate
            // 
            this.generate.Location = new System.Drawing.Point(1091, 333);
            this.generate.Name = "generate";
            this.generate.Size = new System.Drawing.Size(144, 55);
            this.generate.TabIndex = 12;
            this.generate.Text = "Generate arena";
            this.generate.UseVisualStyleBackColor = true;
            this.generate.Click += new System.EventHandler(this.generate_Click);
            // 
            // random
            // 
            this.random.AutoSize = true;
            this.random.Location = new System.Drawing.Point(251, 17);
            this.random.Name = "random";
            this.random.Size = new System.Drawing.Size(221, 21);
            this.random.TabIndex = 18;
            this.random.Text = "Randomly generate breakable";
            this.random.UseVisualStyleBackColor = true;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1254, 901);
            this.Controls.Add(this.random);
            this.Controls.Add(this.generate);
            this.Controls.Add(this.None);
            this.Controls.Add(this.YellowHQ);
            this.Controls.Add(this.BlueHQ);
            this.Controls.Add(this.GreenHQ);
            this.Controls.Add(this.RedHQ);
            this.Controls.Add(this.Flag);
            this.Controls.Add(this.Unbreakable);
            this.Controls.Add(this.Breakable);
            this.Controls.Add(this.dataGridView1);
            this.Controls.Add(this.Name);
            this.Controls.Add(this.tbName);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.width);
            this.Controls.Add(this.height);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name.Name = "Form1";
            this.Text = "ArenaMaker";
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox height;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox width;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Label Name;
        private System.Windows.Forms.TextBox tbName;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.RadioButton Breakable;
        private System.Windows.Forms.RadioButton Unbreakable;
        private System.Windows.Forms.RadioButton Flag;
        private System.Windows.Forms.RadioButton RedHQ;
        private System.Windows.Forms.RadioButton GreenHQ;
        private System.Windows.Forms.RadioButton BlueHQ;
        private System.Windows.Forms.RadioButton YellowHQ;
        private System.Windows.Forms.RadioButton None;
        private System.Windows.Forms.Button generate;
        private System.Windows.Forms.CheckBox random;
    }
}

