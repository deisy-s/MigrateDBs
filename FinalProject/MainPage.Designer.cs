namespace FinalProject
{
    partial class MainPage
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
            this.tvDBs = new System.Windows.Forms.TreeView();
            this.btnRefresh = new DevExpress.XtraEditors.SimpleButton();
            this.btnExecute = new System.Windows.Forms.Button();
            this.btnClear = new DevExpress.XtraEditors.SimpleButton();
            this.btnMigrate = new System.Windows.Forms.Button();
            this.panel1 = new System.Windows.Forms.Panel();
            this.dgvResults = new System.Windows.Forms.DataGridView();
            this.tbQuery = new System.Windows.Forms.TextBox();
            this.cbDB = new System.Windows.Forms.ComboBox();
            this.btnAddConnect = new DevExpress.XtraEditors.SimpleButton();
            this.btnDeleteConnect = new DevExpress.XtraEditors.SimpleButton();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvResults)).BeginInit();
            this.SuspendLayout();
            // 
            // tvDBs
            // 
            this.tvDBs.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.tvDBs.Location = new System.Drawing.Point(0, 51);
            this.tvDBs.Name = "tvDBs";
            this.tvDBs.Size = new System.Drawing.Size(307, 564);
            this.tvDBs.TabIndex = 0;
            this.tvDBs.NodeMouseClick += new System.Windows.Forms.TreeNodeMouseClickEventHandler(this.tvDBs_NodeMouseClick);
            // 
            // btnRefresh
            // 
            this.btnRefresh.ImageOptions.SvgImage = global::FinalProject.Properties.Resources.actions_reload;
            this.btnRefresh.Location = new System.Drawing.Point(255, -4);
            this.btnRefresh.Margin = new System.Windows.Forms.Padding(21);
            this.btnRefresh.Name = "btnRefresh";
            this.btnRefresh.PaintStyle = DevExpress.XtraEditors.Controls.PaintStyles.Light;
            this.btnRefresh.Size = new System.Drawing.Size(52, 52);
            this.btnRefresh.TabIndex = 1;
            this.btnRefresh.Click += new System.EventHandler(this.btnRefresh_Click);
            // 
            // btnExecute
            // 
            this.btnExecute.Font = new System.Drawing.Font("Mongolian Baiti", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnExecute.ForeColor = System.Drawing.Color.MidnightBlue;
            this.btnExecute.Location = new System.Drawing.Point(660, 5);
            this.btnExecute.Name = "btnExecute";
            this.btnExecute.Size = new System.Drawing.Size(161, 42);
            this.btnExecute.TabIndex = 3;
            this.btnExecute.Text = "EJECUTAR";
            this.btnExecute.UseVisualStyleBackColor = true;
            this.btnExecute.Click += new System.EventHandler(this.btnExecute_Click);
            // 
            // btnClear
            // 
            this.btnClear.ImageOptions.SvgImage = global::FinalProject.Properties.Resources.actions_trash;
            this.btnClear.Location = new System.Drawing.Point(1003, -1);
            this.btnClear.Margin = new System.Windows.Forms.Padding(21);
            this.btnClear.Name = "btnClear";
            this.btnClear.PaintStyle = DevExpress.XtraEditors.Controls.PaintStyles.Light;
            this.btnClear.Size = new System.Drawing.Size(52, 52);
            this.btnClear.TabIndex = 4;
            this.btnClear.Click += new System.EventHandler(this.btnClear_Click);
            // 
            // btnMigrate
            // 
            this.btnMigrate.Font = new System.Drawing.Font("Mongolian Baiti", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnMigrate.ForeColor = System.Drawing.Color.MidnightBlue;
            this.btnMigrate.Location = new System.Drawing.Point(839, 5);
            this.btnMigrate.Name = "btnMigrate";
            this.btnMigrate.Size = new System.Drawing.Size(161, 42);
            this.btnMigrate.TabIndex = 5;
            this.btnMigrate.Text = "MIGRAR";
            this.btnMigrate.UseVisualStyleBackColor = true;
            this.btnMigrate.Click += new System.EventHandler(this.btnMigrate_Click);
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.White;
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel1.Controls.Add(this.dgvResults);
            this.panel1.Controls.Add(this.tbQuery);
            this.panel1.Location = new System.Drawing.Point(303, 51);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(824, 564);
            this.panel1.TabIndex = 6;
            // 
            // dgvResults
            // 
            this.dgvResults.BackgroundColor = System.Drawing.Color.White;
            this.dgvResults.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvResults.Location = new System.Drawing.Point(12, 280);
            this.dgvResults.Name = "dgvResults";
            this.dgvResults.RowHeadersWidth = 62;
            this.dgvResults.RowTemplate.Height = 28;
            this.dgvResults.Size = new System.Drawing.Size(800, 272);
            this.dgvResults.TabIndex = 1;
            // 
            // tbQuery
            // 
            this.tbQuery.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.tbQuery.Font = new System.Drawing.Font("Mongolian Baiti", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tbQuery.Location = new System.Drawing.Point(12, 10);
            this.tbQuery.Multiline = true;
            this.tbQuery.Name = "tbQuery";
            this.tbQuery.Size = new System.Drawing.Size(800, 250);
            this.tbQuery.TabIndex = 0;
            // 
            // cbDB
            // 
            this.cbDB.Font = new System.Drawing.Font("Mongolian Baiti", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbDB.FormattingEnabled = true;
            this.cbDB.Location = new System.Drawing.Point(313, 10);
            this.cbDB.Name = "cbDB";
            this.cbDB.Size = new System.Drawing.Size(340, 29);
            this.cbDB.TabIndex = 7;
            // 
            // btnAddConnect
            // 
            this.btnAddConnect.ImageOptions.SvgImage = global::FinalProject.Properties.Resources.actions_add;
            this.btnAddConnect.Location = new System.Drawing.Point(0, -1);
            this.btnAddConnect.Margin = new System.Windows.Forms.Padding(4);
            this.btnAddConnect.Name = "btnAddConnect";
            this.btnAddConnect.PaintStyle = DevExpress.XtraEditors.Controls.PaintStyles.Light;
            this.btnAddConnect.Size = new System.Drawing.Size(52, 52);
            this.btnAddConnect.TabIndex = 8;
            this.btnAddConnect.Click += new System.EventHandler(this.btnAddConnect_Click);
            // 
            // btnDeleteConnect
            // 
            this.btnDeleteConnect.ImageOptions.SvgImage = global::FinalProject.Properties.Resources.del;
            this.btnDeleteConnect.Location = new System.Drawing.Point(62, -1);
            this.btnDeleteConnect.Margin = new System.Windows.Forms.Padding(6);
            this.btnDeleteConnect.Name = "btnDeleteConnect";
            this.btnDeleteConnect.PaintStyle = DevExpress.XtraEditors.Controls.PaintStyles.Light;
            this.btnDeleteConnect.Size = new System.Drawing.Size(52, 52);
            this.btnDeleteConnect.TabIndex = 9;
            this.btnDeleteConnect.Click += new System.EventHandler(this.btnDeleteConnect_Click);
            // 
            // MainPage
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.WhiteSmoke;
            this.ClientSize = new System.Drawing.Size(1126, 614);
            this.Controls.Add(this.btnDeleteConnect);
            this.Controls.Add(this.btnAddConnect);
            this.Controls.Add(this.cbDB);
            this.Controls.Add(this.btnClear);
            this.Controls.Add(this.btnMigrate);
            this.Controls.Add(this.btnExecute);
            this.Controls.Add(this.btnRefresh);
            this.Controls.Add(this.tvDBs);
            this.Controls.Add(this.panel1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Name = "MainPage";
            this.Text = "MainPage";
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvResults)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TreeView tvDBs;
        private DevExpress.XtraEditors.SimpleButton btnRefresh;
        private System.Windows.Forms.Button btnExecute;
        private DevExpress.XtraEditors.SimpleButton btnClear;
        private System.Windows.Forms.Button btnMigrate;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.TextBox tbQuery;
        private System.Windows.Forms.ComboBox cbDB;
        private DevExpress.XtraEditors.SimpleButton btnAddConnect;
        private System.Windows.Forms.DataGridView dgvResults;
        private DevExpress.XtraEditors.SimpleButton btnDeleteConnect;
    }
}