namespace UoB.SLR.SLRDataEntryV1
{
    partial class DataExtractionForm
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
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.cmbVersion = new System.Windows.Forms.ComboBox();
            this.cmbAccepted = new System.Windows.Forms.ComboBox();
            this.button1 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.cmdCitation = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.tbSearch = new System.Windows.Forms.TextBox();
            this.button3 = new System.Windows.Forms.Button();
            this.btnSpQry = new System.Windows.Forms.Button();
            this.cmbAA = new System.Windows.Forms.ComboBox();
            this.label4 = new System.Windows.Forms.Label();
            this.btnAA = new System.Windows.Forms.Button();
            this.cmbAAc = new System.Windows.Forms.ComboBox();
            this.label5 = new System.Windows.Forms.Label();
            this.btnND = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(25, 102);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(85, 25);
            this.label1.TabIndex = 0;
            this.label1.Text = "Version";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(25, 152);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(102, 25);
            this.label2.TabIndex = 1;
            this.label2.Text = "Accepted";
            // 
            // cmbVersion
            // 
            this.cmbVersion.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmbVersion.FormattingEnabled = true;
            this.cmbVersion.Items.AddRange(new object[] {
            "0",
            "1",
            "2"});
            this.cmbVersion.Location = new System.Drawing.Point(182, 99);
            this.cmbVersion.Name = "cmbVersion";
            this.cmbVersion.Size = new System.Drawing.Size(212, 33);
            this.cmbVersion.TabIndex = 2;
            // 
            // cmbAccepted
            // 
            this.cmbAccepted.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmbAccepted.FormattingEnabled = true;
            this.cmbAccepted.Items.AddRange(new object[] {
            "Yes",
            "No",
            "All"});
            this.cmbAccepted.Location = new System.Drawing.Point(183, 152);
            this.cmbAccepted.Name = "cmbAccepted";
            this.cmbAccepted.Size = new System.Drawing.Size(212, 33);
            this.cmbAccepted.TabIndex = 3;
            // 
            // button1
            // 
            this.button1.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button1.Location = new System.Drawing.Point(478, 152);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(141, 36);
            this.button1.TabIndex = 4;
            this.button1.Text = "Create Excel";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // button2
            // 
            this.button2.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button2.Location = new System.Drawing.Point(519, 471);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(141, 36);
            this.button2.TabIndex = 5;
            this.button2.Text = "Exit";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // cmdCitation
            // 
            this.cmdCitation.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmdCitation.Location = new System.Drawing.Point(30, 471);
            this.cmdCitation.Name = "cmdCitation";
            this.cmdCitation.Size = new System.Drawing.Size(141, 36);
            this.cmdCitation.TabIndex = 6;
            this.cmdCitation.Text = "Get Citations";
            this.cmdCitation.UseVisualStyleBackColor = true;
            this.cmdCitation.Click += new System.EventHandler(this.cmdCitation_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(25, 371);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(206, 25);
            this.label3.TabIndex = 7;
            this.label3.Text = "Text to be Searched";
            // 
            // tbSearch
            // 
            this.tbSearch.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tbSearch.Location = new System.Drawing.Point(183, 414);
            this.tbSearch.Name = "tbSearch";
            this.tbSearch.Size = new System.Drawing.Size(475, 31);
            this.tbSearch.TabIndex = 8;
            // 
            // button3
            // 
            this.button3.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button3.Location = new System.Drawing.Point(255, 471);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(258, 36);
            this.button3.TabIndex = 9;
            this.button3.Text = "Search and Create Excel";
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Click += new System.EventHandler(this.button3_Click);
            // 
            // btnSpQry
            // 
            this.btnSpQry.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSpQry.Location = new System.Drawing.Point(30, 22);
            this.btnSpQry.Name = "btnSpQry";
            this.btnSpQry.Size = new System.Drawing.Size(141, 36);
            this.btnSpQry.TabIndex = 10;
            this.btnSpQry.Text = "Special Query";
            this.btnSpQry.UseVisualStyleBackColor = true;
            this.btnSpQry.Click += new System.EventHandler(this.btnSpQry_Click);
            // 
            // cmbAA
            // 
            this.cmbAA.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmbAA.FormattingEnabled = true;
            this.cmbAA.Location = new System.Drawing.Point(183, 230);
            this.cmbAA.Name = "cmbAA";
            this.cmbAA.Size = new System.Drawing.Size(212, 33);
            this.cmbAA.TabIndex = 12;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(25, 233);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(101, 25);
            this.label4.TabIndex = 11;
            this.label4.Text = "App Area";
            // 
            // btnAA
            // 
            this.btnAA.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnAA.Location = new System.Drawing.Point(478, 276);
            this.btnAA.Name = "btnAA";
            this.btnAA.Size = new System.Drawing.Size(141, 36);
            this.btnAA.TabIndex = 13;
            this.btnAA.Text = "Create Excel";
            this.btnAA.UseVisualStyleBackColor = true;
            this.btnAA.Click += new System.EventHandler(this.btnAA_Click);
            // 
            // cmbAAc
            // 
            this.cmbAAc.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmbAAc.FormattingEnabled = true;
            this.cmbAAc.Items.AddRange(new object[] {
            "Yes",
            "No",
            "All"});
            this.cmbAAc.Location = new System.Drawing.Point(183, 278);
            this.cmbAAc.Name = "cmbAAc";
            this.cmbAAc.Size = new System.Drawing.Size(212, 33);
            this.cmbAAc.TabIndex = 15;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(25, 286);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(102, 25);
            this.label5.TabIndex = 14;
            this.label5.Text = "Accepted";
            // 
            // btnND
            // 
            this.btnND.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnND.Location = new System.Drawing.Point(177, 22);
            this.btnND.Name = "btnND";
            this.btnND.Size = new System.Drawing.Size(190, 36);
            this.btnND.TabIndex = 16;
            this.btnND.Text = "Get Normalized Data";
            this.btnND.UseVisualStyleBackColor = true;
            this.btnND.Click += new System.EventHandler(this.btnND_Click);
            // 
            // DataExtractionForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(674, 532);
            this.ControlBox = false;
            this.Controls.Add(this.btnND);
            this.Controls.Add(this.cmbAAc);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.btnAA);
            this.Controls.Add(this.cmbAA);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.btnSpQry);
            this.Controls.Add(this.button3);
            this.Controls.Add(this.tbSearch);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.cmdCitation);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.cmbAccepted);
            this.Controls.Add(this.cmbVersion);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Name = "DataExtractionForm";
            this.Text = "Excel Generation Form";
            this.Load += new System.EventHandler(this.DataExtractionForm_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox cmbVersion;
        private System.Windows.Forms.ComboBox cmbAccepted;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Button cmdCitation;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox tbSearch;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.Button btnSpQry;
        private System.Windows.Forms.ComboBox cmbAA;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Button btnAA;
        private System.Windows.Forms.ComboBox cmbAAc;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Button btnND;
    }
}