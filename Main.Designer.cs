namespace NCBIFilter
{
    partial class Main
    {
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.File_Textbox = new System.Windows.Forms.TextBox();
            this.LoadFile_Button = new System.Windows.Forms.Button();
            this.SamePrimers_Check = new System.Windows.Forms.CheckBox();
            this.SameOrganism_Check = new System.Windows.Forms.CheckBox();
            this.SameCountry_Check = new System.Windows.Forms.CheckBox();
            this.SameLatLon_Check = new System.Windows.Forms.CheckBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.SameCity_Check = new System.Windows.Forms.CheckBox();
            this.Output_Button = new System.Windows.Forms.Button();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.Download_Button = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.Floder_Combobox = new System.Windows.Forms.ComboBox();
            this.Format_Combobox = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.BuildMap_Button = new System.Windows.Forms.Button();
            this.ShowMap_Button = new System.Windows.Forms.Button();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.SuspendLayout();
            // 
            // File_Textbox
            // 
            this.File_Textbox.Location = new System.Drawing.Point(12, 23);
            this.File_Textbox.Name = "File_Textbox";
            this.File_Textbox.ReadOnly = true;
            this.File_Textbox.Size = new System.Drawing.Size(462, 21);
            this.File_Textbox.TabIndex = 3;
            // 
            // LoadFile_Button
            // 
            this.LoadFile_Button.Location = new System.Drawing.Point(480, 23);
            this.LoadFile_Button.Name = "LoadFile_Button";
            this.LoadFile_Button.Size = new System.Drawing.Size(75, 23);
            this.LoadFile_Button.TabIndex = 4;
            this.LoadFile_Button.Text = "LoadFile";
            this.LoadFile_Button.UseVisualStyleBackColor = true;
            this.LoadFile_Button.Click += new System.EventHandler(this.LoadFile_Button_Click);
            // 
            // SamePrimers_Check
            // 
            this.SamePrimers_Check.AutoSize = true;
            this.SamePrimers_Check.Location = new System.Drawing.Point(6, 20);
            this.SamePrimers_Check.Name = "SamePrimers_Check";
            this.SamePrimers_Check.Size = new System.Drawing.Size(90, 16);
            this.SamePrimers_Check.TabIndex = 6;
            this.SamePrimers_Check.Text = "SamePrimers";
            this.SamePrimers_Check.UseVisualStyleBackColor = true;
            // 
            // SameOrganism_Check
            // 
            this.SameOrganism_Check.AutoSize = true;
            this.SameOrganism_Check.Location = new System.Drawing.Point(102, 20);
            this.SameOrganism_Check.Name = "SameOrganism_Check";
            this.SameOrganism_Check.Size = new System.Drawing.Size(96, 16);
            this.SameOrganism_Check.TabIndex = 7;
            this.SameOrganism_Check.Text = "SameOrganism";
            this.SameOrganism_Check.UseVisualStyleBackColor = true;
            // 
            // SameCountry_Check
            // 
            this.SameCountry_Check.AutoSize = true;
            this.SameCountry_Check.Location = new System.Drawing.Point(204, 20);
            this.SameCountry_Check.Name = "SameCountry_Check";
            this.SameCountry_Check.Size = new System.Drawing.Size(90, 16);
            this.SameCountry_Check.TabIndex = 8;
            this.SameCountry_Check.Text = "SameCountry";
            this.SameCountry_Check.UseVisualStyleBackColor = true;
            // 
            // SameLatLon_Check
            // 
            this.SameLatLon_Check.AutoSize = true;
            this.SameLatLon_Check.Location = new System.Drawing.Point(378, 20);
            this.SameLatLon_Check.Name = "SameLatLon_Check";
            this.SameLatLon_Check.Size = new System.Drawing.Size(84, 16);
            this.SameLatLon_Check.TabIndex = 9;
            this.SameLatLon_Check.Text = "SameLatLon";
            this.SameLatLon_Check.UseVisualStyleBackColor = true;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.SameCity_Check);
            this.groupBox1.Controls.Add(this.Output_Button);
            this.groupBox1.Controls.Add(this.SamePrimers_Check);
            this.groupBox1.Controls.Add(this.SameLatLon_Check);
            this.groupBox1.Controls.Add(this.SameOrganism_Check);
            this.groupBox1.Controls.Add(this.SameCountry_Check);
            this.groupBox1.Location = new System.Drawing.Point(12, 60);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(558, 54);
            this.groupBox1.TabIndex = 10;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "ClassifyData";
            // 
            // SameCity_Check
            // 
            this.SameCity_Check.AutoSize = true;
            this.SameCity_Check.Location = new System.Drawing.Point(300, 20);
            this.SameCity_Check.Name = "SameCity_Check";
            this.SameCity_Check.Size = new System.Drawing.Size(72, 16);
            this.SameCity_Check.TabIndex = 11;
            this.SameCity_Check.Text = "SameCity";
            this.SameCity_Check.UseVisualStyleBackColor = true;
            // 
            // Output_Button
            // 
            this.Output_Button.Location = new System.Drawing.Point(468, 16);
            this.Output_Button.Name = "Output_Button";
            this.Output_Button.Size = new System.Drawing.Size(75, 23);
            this.Output_Button.TabIndex = 10;
            this.Output_Button.Text = "Output";
            this.Output_Button.UseVisualStyleBackColor = true;
            this.Output_Button.Click += new System.EventHandler(this.Output_Button_Click);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.Download_Button);
            this.groupBox2.Controls.Add(this.label2);
            this.groupBox2.Controls.Add(this.Floder_Combobox);
            this.groupBox2.Controls.Add(this.Format_Combobox);
            this.groupBox2.Controls.Add(this.label1);
            this.groupBox2.Location = new System.Drawing.Point(12, 126);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(558, 54);
            this.groupBox2.TabIndex = 11;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "DownloadData";
            // 
            // Download_Button
            // 
            this.Download_Button.Location = new System.Drawing.Point(468, 18);
            this.Download_Button.Name = "Download_Button";
            this.Download_Button.Size = new System.Drawing.Size(75, 23);
            this.Download_Button.TabIndex = 13;
            this.Download_Button.Text = "Download";
            this.Download_Button.UseVisualStyleBackColor = true;
            this.Download_Button.Click += new System.EventHandler(this.Download_Button_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("宋体", 10F);
            this.label2.Location = new System.Drawing.Point(245, 23);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(49, 14);
            this.label2.TabIndex = 12;
            this.label2.Text = "Format";
            // 
            // Floder_Combobox
            // 
            this.Floder_Combobox.BackColor = System.Drawing.SystemColors.Window;
            this.Floder_Combobox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.Floder_Combobox.Font = new System.Drawing.Font("宋体", 9F);
            this.Floder_Combobox.FormattingEnabled = true;
            this.Floder_Combobox.Items.AddRange(new object[] {
            "SamePrimers",
            "SameOrganism",
            "SameCountry",
            "SameLatLon"});
            this.Floder_Combobox.Location = new System.Drawing.Point(105, 21);
            this.Floder_Combobox.Name = "Floder_Combobox";
            this.Floder_Combobox.Size = new System.Drawing.Size(128, 20);
            this.Floder_Combobox.TabIndex = 3;
            // 
            // Format_Combobox
            // 
            this.Format_Combobox.BackColor = System.Drawing.SystemColors.Window;
            this.Format_Combobox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.Format_Combobox.Font = new System.Drawing.Font("宋体", 9F);
            this.Format_Combobox.FormattingEnabled = true;
            this.Format_Combobox.Items.AddRange(new object[] {
            "GenBank",
            "GenBank(Full)",
            "FASTA",
            "XML",
            "INSDSeq XML",
            "TinySeq XML",
            "Feature Table",
            "GI List"});
            this.Format_Combobox.Location = new System.Drawing.Point(300, 21);
            this.Format_Combobox.Name = "Format_Combobox";
            this.Format_Combobox.Size = new System.Drawing.Size(128, 20);
            this.Format_Combobox.TabIndex = 2;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("宋体", 10F);
            this.label1.Location = new System.Drawing.Point(50, 23);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(49, 14);
            this.label1.TabIndex = 1;
            this.label1.Text = "Folder";
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.BuildMap_Button);
            this.groupBox3.Controls.Add(this.ShowMap_Button);
            this.groupBox3.Location = new System.Drawing.Point(12, 192);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(558, 57);
            this.groupBox3.TabIndex = 12;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Map";
            // 
            // BuildMap_Button
            // 
            this.BuildMap_Button.Location = new System.Drawing.Point(139, 20);
            this.BuildMap_Button.Name = "BuildMap_Button";
            this.BuildMap_Button.Size = new System.Drawing.Size(128, 23);
            this.BuildMap_Button.TabIndex = 15;
            this.BuildMap_Button.Text = "BuildMap";
            this.BuildMap_Button.UseVisualStyleBackColor = true;
            this.BuildMap_Button.Click += new System.EventHandler(this.BuildMap_Button_Click);
            // 
            // ShowMap_Button
            // 
            this.ShowMap_Button.Location = new System.Drawing.Point(334, 20);
            this.ShowMap_Button.Name = "ShowMap_Button";
            this.ShowMap_Button.Size = new System.Drawing.Size(128, 23);
            this.ShowMap_Button.TabIndex = 14;
            this.ShowMap_Button.Text = "ShowMap";
            this.ShowMap_Button.UseVisualStyleBackColor = true;
            this.ShowMap_Button.Click += new System.EventHandler(this.ShowMap_Button_Click);
            // 
            // Main
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(585, 261);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.LoadFile_Button);
            this.Controls.Add(this.File_Textbox);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Name = "Main";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "NCBIFilter";
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox File_Textbox;
        private System.Windows.Forms.Button LoadFile_Button;
        private System.Windows.Forms.CheckBox SamePrimers_Check;
        private System.Windows.Forms.CheckBox SameOrganism_Check;
        private System.Windows.Forms.CheckBox SameCountry_Check;
        private System.Windows.Forms.CheckBox SameLatLon_Check;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button Output_Button;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox Format_Combobox;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox Floder_Combobox;
        private System.Windows.Forms.Button Download_Button;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.Button ShowMap_Button;
        private System.Windows.Forms.Button BuildMap_Button;
        private System.Windows.Forms.CheckBox SameCity_Check;
    }
}

