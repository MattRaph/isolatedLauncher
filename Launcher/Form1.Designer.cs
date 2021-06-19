
namespace Launcher
{
	partial class Launcher
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Launcher));
			this.MainButton = new System.Windows.Forms.Button();
			this.VersionDropdown = new System.Windows.Forms.ComboBox();
			this.SuspendLayout();
			// 
			// MainButton
			// 
			this.MainButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.MainButton.BackColor = System.Drawing.Color.White;
			this.MainButton.Cursor = System.Windows.Forms.Cursors.Hand;
			this.MainButton.FlatAppearance.MouseDownBackColor = System.Drawing.Color.White;
			this.MainButton.FlatAppearance.MouseOverBackColor = System.Drawing.Color.White;
			this.MainButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.MainButton.Font = new System.Drawing.Font("Consolas", 21.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(186)));
			this.MainButton.Location = new System.Drawing.Point(736, 567);
			this.MainButton.Name = "MainButton";
			this.MainButton.Size = new System.Drawing.Size(316, 102);
			this.MainButton.TabIndex = 0;
			this.MainButton.Text = "Download";
			this.MainButton.TextImageRelation = System.Windows.Forms.TextImageRelation.TextAboveImage;
			this.MainButton.UseVisualStyleBackColor = false;
			this.MainButton.Click += new System.EventHandler(this.MainButton_Click);
			// 
			// VersionDropdown
			// 
			this.VersionDropdown.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.VersionDropdown.Cursor = System.Windows.Forms.Cursors.Default;
			this.VersionDropdown.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.VersionDropdown.Font = new System.Drawing.Font("Microsoft Sans Serif", 21.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(186)));
			this.VersionDropdown.IntegralHeight = false;
			this.VersionDropdown.ItemHeight = 33;
			this.VersionDropdown.Location = new System.Drawing.Point(12, 598);
			this.VersionDropdown.MaximumSize = new System.Drawing.Size(316, 0);
			this.VersionDropdown.MinimumSize = new System.Drawing.Size(316, 0);
			this.VersionDropdown.Name = "VersionDropdown";
			this.VersionDropdown.Size = new System.Drawing.Size(316, 41);
			this.VersionDropdown.TabIndex = 1;
			this.VersionDropdown.SelectedIndexChanged += new System.EventHandler(this.VersionDropdown_SelectedIndexChanged);
			// 
			// Launcher
			// 
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
			this.BackgroundImage = global::Launcher.Properties.Resources.LauncherWindowBG;
			this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
			this.ClientSize = new System.Drawing.Size(1064, 681);
			this.Controls.Add(this.VersionDropdown);
			this.Controls.Add(this.MainButton);
			this.DoubleBuffered = true;
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.MaximizeBox = false;
			this.MaximumSize = new System.Drawing.Size(2160, 1440);
			this.MinimumSize = new System.Drawing.Size(800, 600);
			this.Name = "Launcher";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "Launcher";
			this.Load += new System.EventHandler(this.Launcher_Load);
			this.ResumeLayout(false);

		}

		#endregion
		protected internal System.Windows.Forms.Button MainButton;
		public System.Windows.Forms.ComboBox VersionDropdown;
	}
}

