namespace FinalProject
{
    partial class Migrate
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
            this.panel1 = new System.Windows.Forms.Panel();
            this.panel4 = new System.Windows.Forms.Panel();
            this.rbOracle2 = new System.Windows.Forms.RadioButton();
            this.rbFirebird2 = new System.Windows.Forms.RadioButton();
            this.rbPostgres2 = new System.Windows.Forms.RadioButton();
            this.rbMySQL2 = new System.Windows.Forms.RadioButton();
            this.label2 = new System.Windows.Forms.Label();
            this.panel3 = new System.Windows.Forms.Panel();
            this.rbSQLServer1 = new System.Windows.Forms.RadioButton();
            this.label1 = new System.Windows.Forms.Label();
            this.btnMigrate = new System.Windows.Forms.Button();
            this.clbTables = new System.Windows.Forms.CheckedListBox();
            this.label4 = new System.Windows.Forms.Label();
            this.cbDbs = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.panel1.SuspendLayout();
            this.panel4.SuspendLayout();
            this.panel3.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.White;
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel1.Controls.Add(this.clbTables);
            this.panel1.Controls.Add(this.panel4);
            this.panel1.Controls.Add(this.btnMigrate);
            this.panel1.Controls.Add(this.label2);
            this.panel1.Controls.Add(this.label4);
            this.panel1.Controls.Add(this.panel3);
            this.panel1.Controls.Add(this.cbDbs);
            this.panel1.Controls.Add(this.label3);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Location = new System.Drawing.Point(12, 12);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(472, 545);
            this.panel1.TabIndex = 0;
            // 
            // panel4
            // 
            this.panel4.Controls.Add(this.rbOracle2);
            this.panel4.Controls.Add(this.rbFirebird2);
            this.panel4.Controls.Add(this.rbPostgres2);
            this.panel4.Controls.Add(this.rbMySQL2);
            this.panel4.Font = new System.Drawing.Font("Mongolian Baiti", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.panel4.Location = new System.Drawing.Point(160, 67);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(302, 127);
            this.panel4.TabIndex = 3;
            // 
            // rbOracle2
            // 
            this.rbOracle2.AutoSize = true;
            this.rbOracle2.Font = new System.Drawing.Font("Mongolian Baiti", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rbOracle2.Location = new System.Drawing.Point(15, 91);
            this.rbOracle2.Name = "rbOracle2";
            this.rbOracle2.Size = new System.Drawing.Size(89, 25);
            this.rbOracle2.TabIndex = 4;
            this.rbOracle2.TabStop = true;
            this.rbOracle2.Text = "Oracle";
            this.rbOracle2.UseVisualStyleBackColor = true;
            this.rbOracle2.CheckedChanged += new System.EventHandler(this.rbOracle2_CheckedChanged);
            // 
            // rbFirebird2
            // 
            this.rbFirebird2.AutoSize = true;
            this.rbFirebird2.Font = new System.Drawing.Font("Mongolian Baiti", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rbFirebird2.Location = new System.Drawing.Point(15, 65);
            this.rbFirebird2.Name = "rbFirebird2";
            this.rbFirebird2.Size = new System.Drawing.Size(101, 25);
            this.rbFirebird2.TabIndex = 3;
            this.rbFirebird2.TabStop = true;
            this.rbFirebird2.Text = "Firebird";
            this.rbFirebird2.UseVisualStyleBackColor = true;
            this.rbFirebird2.CheckedChanged += new System.EventHandler(this.rbFirebird2_CheckedChanged);
            // 
            // rbPostgres2
            // 
            this.rbPostgres2.AutoSize = true;
            this.rbPostgres2.Font = new System.Drawing.Font("Mongolian Baiti", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rbPostgres2.Location = new System.Drawing.Point(15, 39);
            this.rbPostgres2.Name = "rbPostgres2";
            this.rbPostgres2.Size = new System.Drawing.Size(104, 25);
            this.rbPostgres2.TabIndex = 2;
            this.rbPostgres2.TabStop = true;
            this.rbPostgres2.Text = "Postgres";
            this.rbPostgres2.UseVisualStyleBackColor = true;
            this.rbPostgres2.CheckedChanged += new System.EventHandler(this.rbPostgres2_CheckedChanged);
            // 
            // rbMySQL2
            // 
            this.rbMySQL2.AutoSize = true;
            this.rbMySQL2.Font = new System.Drawing.Font("Mongolian Baiti", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rbMySQL2.Location = new System.Drawing.Point(15, 13);
            this.rbMySQL2.Name = "rbMySQL2";
            this.rbMySQL2.Size = new System.Drawing.Size(100, 25);
            this.rbMySQL2.TabIndex = 1;
            this.rbMySQL2.TabStop = true;
            this.rbMySQL2.Text = "MySQL";
            this.rbMySQL2.UseVisualStyleBackColor = true;
            this.rbMySQL2.CheckedChanged += new System.EventHandler(this.rbMySQL2_CheckedChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Mongolian Baiti", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(8, 67);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(102, 25);
            this.label2.TabIndex = 2;
            this.label2.Text = "Migrar a:";
            // 
            // panel3
            // 
            this.panel3.Controls.Add(this.rbSQLServer1);
            this.panel3.Location = new System.Drawing.Point(160, 3);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(302, 51);
            this.panel3.TabIndex = 1;
            // 
            // rbSQLServer1
            // 
            this.rbSQLServer1.AutoSize = true;
            this.rbSQLServer1.Font = new System.Drawing.Font("Mongolian Baiti", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rbSQLServer1.Location = new System.Drawing.Point(15, 13);
            this.rbSQLServer1.Name = "rbSQLServer1";
            this.rbSQLServer1.Size = new System.Drawing.Size(130, 25);
            this.rbSQLServer1.TabIndex = 0;
            this.rbSQLServer1.TabStop = true;
            this.rbSQLServer1.Text = "SQL Server";
            this.rbSQLServer1.UseVisualStyleBackColor = true;
            this.rbSQLServer1.CheckedChanged += new System.EventHandler(this.rbSQLServer1_CheckedChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Mongolian Baiti", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(8, 13);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(146, 25);
            this.label1.TabIndex = 0;
            this.label1.Text = "Migrar desde:";
            // 
            // btnMigrate
            // 
            this.btnMigrate.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnMigrate.Font = new System.Drawing.Font("Mongolian Baiti", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnMigrate.ForeColor = System.Drawing.Color.MidnightBlue;
            this.btnMigrate.Location = new System.Drawing.Point(124, 502);
            this.btnMigrate.Name = "btnMigrate";
            this.btnMigrate.Size = new System.Drawing.Size(190, 38);
            this.btnMigrate.TabIndex = 2;
            this.btnMigrate.Text = "MIGRAR";
            this.btnMigrate.UseVisualStyleBackColor = true;
            this.btnMigrate.Click += new System.EventHandler(this.btnMigrate_Click);
            // 
            // clbTables
            // 
            this.clbTables.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.clbTables.Font = new System.Drawing.Font("Mongolian Baiti", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.clbTables.FormattingEnabled = true;
            this.clbTables.Location = new System.Drawing.Point(160, 269);
            this.clbTables.Name = "clbTables";
            this.clbTables.Size = new System.Drawing.Size(302, 224);
            this.clbTables.TabIndex = 0;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Mongolian Baiti", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(2, 269);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(83, 25);
            this.label4.TabIndex = 3;
            this.label4.Text = "Tablas:";
            // 
            // cbDbs
            // 
            this.cbDbs.Font = new System.Drawing.Font("Mongolian Baiti", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbDbs.FormattingEnabled = true;
            this.cbDbs.Location = new System.Drawing.Point(160, 214);
            this.cbDbs.Name = "cbDbs";
            this.cbDbs.Size = new System.Drawing.Size(302, 29);
            this.cbDbs.TabIndex = 2;
            this.cbDbs.SelectedValueChanged += new System.EventHandler(this.cbDbs_SelectedValueChanged);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Mongolian Baiti", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(2, 215);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(151, 25);
            this.label3.TabIndex = 1;
            this.label3.Text = "Base de datos:";
            // 
            // Migrate
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(494, 569);
            this.Controls.Add(this.panel1);
            this.Name = "Migrate";
            this.Text = "Migrar";
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.panel4.ResumeLayout(false);
            this.panel4.PerformLayout();
            this.panel3.ResumeLayout(false);
            this.panel3.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.RadioButton rbSQLServer1;
        private System.Windows.Forms.Panel panel4;
        private System.Windows.Forms.RadioButton rbOracle2;
        private System.Windows.Forms.RadioButton rbFirebird2;
        private System.Windows.Forms.RadioButton rbPostgres2;
        private System.Windows.Forms.RadioButton rbMySQL2;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox cbDbs;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.CheckedListBox clbTables;
        private System.Windows.Forms.Button btnMigrate;
    }
}