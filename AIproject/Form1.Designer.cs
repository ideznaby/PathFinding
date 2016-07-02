namespace AIproject
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
            this.Online = new System.Windows.Forms.Button();
            this.Astar = new System.Windows.Forms.Button();
            this.RBFS = new System.Windows.Forms.Button();
            this.SMA = new System.Windows.Forms.Button();
            this.Agent = new System.Windows.Forms.PictureBox();
            this.Load_input = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.Agent)).BeginInit();
            this.SuspendLayout();
            // 
            // Online
            // 
            this.Online.Enabled = false;
            this.Online.Location = new System.Drawing.Point(1034, 122);
            this.Online.Name = "Online";
            this.Online.Size = new System.Drawing.Size(126, 27);
            this.Online.TabIndex = 0;
            this.Online.Text = "Online";
            this.Online.UseVisualStyleBackColor = true;
            this.Online.Click += new System.EventHandler(this.Online_Click);
            // 
            // Astar
            // 
            this.Astar.Enabled = false;
            this.Astar.Location = new System.Drawing.Point(1034, 201);
            this.Astar.Name = "Astar";
            this.Astar.Size = new System.Drawing.Size(126, 27);
            this.Astar.TabIndex = 4;
            this.Astar.Text = "Offline(using A*)";
            this.Astar.UseVisualStyleBackColor = true;
            this.Astar.Click += new System.EventHandler(this.Astar_Click);
            // 
            // RBFS
            // 
            this.RBFS.Enabled = false;
            this.RBFS.Location = new System.Drawing.Point(1034, 285);
            this.RBFS.Name = "RBFS";
            this.RBFS.Size = new System.Drawing.Size(126, 27);
            this.RBFS.TabIndex = 5;
            this.RBFS.Text = "Offline(using RBFS)";
            this.RBFS.UseVisualStyleBackColor = true;
            this.RBFS.Click += new System.EventHandler(this.RBFS_Click);
            // 
            // SMA
            // 
            this.SMA.Enabled = false;
            this.SMA.Location = new System.Drawing.Point(1034, 360);
            this.SMA.Name = "SMA";
            this.SMA.Size = new System.Drawing.Size(126, 27);
            this.SMA.TabIndex = 6;
            this.SMA.Text = "Offline(using SMA*)";
            this.SMA.UseVisualStyleBackColor = true;
            this.SMA.Click += new System.EventHandler(this.SMA_Click);
            // 
            // Agent
            // 
            this.Agent.BackColor = System.Drawing.Color.Transparent;
            this.Agent.Image = global::AIproject.Properties.Resources.kcron;
            this.Agent.Location = new System.Drawing.Point(0, 3);
            this.Agent.Name = "Agent";
            this.Agent.Size = new System.Drawing.Size(25, 26);
            this.Agent.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.Agent.TabIndex = 7;
            this.Agent.TabStop = false;
            // 
            // Load_input
            // 
            this.Load_input.Location = new System.Drawing.Point(1047, 21);
            this.Load_input.Name = "Load_input";
            this.Load_input.Size = new System.Drawing.Size(97, 36);
            this.Load_input.TabIndex = 9;
            this.Load_input.Text = "Load Input File";
            this.Load_input.UseVisualStyleBackColor = true;
            this.Load_input.Click += new System.EventHandler(this.Load_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(1182, 762);
            this.Controls.Add(this.Load_input);
            this.Controls.Add(this.Agent);
            this.Controls.Add(this.SMA);
            this.Controls.Add(this.RBFS);
            this.Controls.Add(this.Astar);
            this.Controls.Add(this.Online);
            this.Name = "Form1";
            this.Text = "Form1";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.Form1_Load);
            ((System.ComponentModel.ISupportInitialize)(this.Agent)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button Online;
        private System.Windows.Forms.Button Astar;
        private System.Windows.Forms.Button RBFS;
        private System.Windows.Forms.Button SMA;
        private System.Windows.Forms.PictureBox Agent;
        private System.Windows.Forms.Button Load_input;
    }
}

