namespace NCBIFilter
{
    partial class Map
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
            this.MapControl = new GMap.NET.WindowsForms.GMapControl();
            this.Save_Button = new System.Windows.Forms.Button();
            this.ShowCountry_Button = new System.Windows.Forms.Button();
            this.LatLng_Button = new System.Windows.Forms.Button();
            this.All_Button = new System.Windows.Forms.Button();
            this.ShowCity_Button = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // MapControl
            // 
            this.MapControl.Bearing = 0F;
            this.MapControl.CanDragMap = true;
            this.MapControl.EmptyTileColor = System.Drawing.Color.Navy;
            this.MapControl.GrayScaleMode = false;
            this.MapControl.HelperLineOption = GMap.NET.WindowsForms.HelperLineOptions.DontShow;
            this.MapControl.LevelsKeepInMemmory = 5;
            this.MapControl.Location = new System.Drawing.Point(12, 15);
            this.MapControl.MarkersEnabled = true;
            this.MapControl.MaxZoom = 18;
            this.MapControl.MinZoom = 2;
            this.MapControl.MouseWheelZoomType = GMap.NET.MouseWheelZoomType.MousePositionAndCenter;
            this.MapControl.Name = "MapControl";
            this.MapControl.NegativeMode = false;
            this.MapControl.PolygonsEnabled = true;
            this.MapControl.RetryLoadTile = 0;
            this.MapControl.RoutesEnabled = true;
            this.MapControl.ScaleMode = GMap.NET.WindowsForms.ScaleModes.Integer;
            this.MapControl.SelectedAreaFillColor = System.Drawing.Color.FromArgb(((int)(((byte)(33)))), ((int)(((byte)(65)))), ((int)(((byte)(105)))), ((int)(((byte)(225)))));
            this.MapControl.ShowTileGridLines = false;
            this.MapControl.Size = new System.Drawing.Size(1022, 818);
            this.MapControl.TabIndex = 1;
            this.MapControl.Zoom = 0D;
            // 
            // Save_Button
            // 
            this.Save_Button.Location = new System.Drawing.Point(1050, 57);
            this.Save_Button.Name = "Save_Button";
            this.Save_Button.Size = new System.Drawing.Size(75, 23);
            this.Save_Button.TabIndex = 5;
            this.Save_Button.Text = "SaveImage";
            this.Save_Button.UseVisualStyleBackColor = true;
            this.Save_Button.Click += new System.EventHandler(this.Save_Button_Click);
            // 
            // ShowCountry_Button
            // 
            this.ShowCountry_Button.Location = new System.Drawing.Point(1050, 125);
            this.ShowCountry_Button.Name = "ShowCountry_Button";
            this.ShowCountry_Button.Size = new System.Drawing.Size(75, 23);
            this.ShowCountry_Button.TabIndex = 6;
            this.ShowCountry_Button.Text = "Country";
            this.ShowCountry_Button.UseVisualStyleBackColor = true;
            this.ShowCountry_Button.Click += new System.EventHandler(this.ShowCountry_Button_Click);
            // 
            // LatLng_Button
            // 
            this.LatLng_Button.Location = new System.Drawing.Point(1050, 205);
            this.LatLng_Button.Name = "LatLng_Button";
            this.LatLng_Button.Size = new System.Drawing.Size(75, 23);
            this.LatLng_Button.TabIndex = 7;
            this.LatLng_Button.Text = "LatLon";
            this.LatLng_Button.UseVisualStyleBackColor = true;
            this.LatLng_Button.Click += new System.EventHandler(this.LatLng_Button_Click);
            // 
            // All_Button
            // 
            this.All_Button.Location = new System.Drawing.Point(1050, 268);
            this.All_Button.Name = "All_Button";
            this.All_Button.Size = new System.Drawing.Size(75, 23);
            this.All_Button.TabIndex = 8;
            this.All_Button.Text = "All";
            this.All_Button.UseVisualStyleBackColor = true;
            this.All_Button.Click += new System.EventHandler(this.All_Button_Click);
            // 
            // ShowCity_Button
            // 
            this.ShowCity_Button.Location = new System.Drawing.Point(1050, 165);
            this.ShowCity_Button.Name = "ShowCity_Button";
            this.ShowCity_Button.Size = new System.Drawing.Size(75, 23);
            this.ShowCity_Button.TabIndex = 9;
            this.ShowCity_Button.Text = "City";
            this.ShowCity_Button.UseVisualStyleBackColor = true;
            this.ShowCity_Button.Click += new System.EventHandler(this.ShowCity_Button_Click);
            // 
            // Map
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1137, 845);
            this.Controls.Add(this.ShowCity_Button);
            this.Controls.Add(this.All_Button);
            this.Controls.Add(this.LatLng_Button);
            this.Controls.Add(this.ShowCountry_Button);
            this.Controls.Add(this.Save_Button);
            this.Controls.Add(this.MapControl);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Name = "Map";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Map";
            this.ResumeLayout(false);

        }

        #endregion

        public GMap.NET.WindowsForms.GMapControl MapControl;
        private System.Windows.Forms.Button Save_Button;
        private System.Windows.Forms.Button ShowCountry_Button;
        private System.Windows.Forms.Button LatLng_Button;
        private System.Windows.Forms.Button All_Button;
        private System.Windows.Forms.Button ShowCity_Button;
    }
}