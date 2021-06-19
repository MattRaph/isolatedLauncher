
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
			this.PlayButton = new System.Windows.Forms.Button();
			this.VersionSubmitText = new System.Windows.Forms.TextBox();
			this.SuspendLayout();
			// 
			// PlayButton
			// 
			this.PlayButton.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
			this.PlayButton.BackColor = System.Drawing.Color.White;
			this.PlayButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(186)));
			this.PlayButton.Location = new System.Drawing.Point(398, 595);
			this.PlayButton.Name = "PlayButton";
			this.PlayButton.Size = new System.Drawing.Size(270, 75);
			this.PlayButton.TabIndex = 0;
			this.PlayButton.Text = "Check for updates";
			this.PlayButton.TextImageRelation = System.Windows.Forms.TextImageRelation.TextAboveImage;
			this.PlayButton.UseVisualStyleBackColor = false;
			this.PlayButton.Click += new System.EventHandler(this.button1_Click);
			// 
			// VersionSubmitText
			// 
			this.VersionSubmitText.AllowDrop = true;
			this.VersionSubmitText.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
			this.VersionSubmitText.BorderStyle = System.Windows.Forms.BorderStyle.None;
			this.VersionSubmitText.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
			this.VersionSubmitText.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(186)));
			this.VersionSubmitText.HideSelection = false;
			this.VersionSubmitText.Location = new System.Drawing.Point(923, 625);
			this.VersionSubmitText.Margin = new System.Windows.Forms.Padding(0, 16, 0, 16);
			this.VersionSubmitText.MaximumSize = new System.Drawing.Size(256, 64);
			this.VersionSubmitText.MaxLength = 7;
			this.VersionSubmitText.Name = "VersionSubmitText";
			this.VersionSubmitText.Size = new System.Drawing.Size(128, 22);
			this.VersionSubmitText.TabIndex = 2;
			this.VersionSubmitText.Text = ":)";
			this.VersionSubmitText.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
			this.VersionSubmitText.WordWrap = false;
			// 
			// Launcher
			// 
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
			this.BackgroundImage = global::Launcher.Properties.Resources.LauncherWindowBG;
			this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
			this.ClientSize = new System.Drawing.Size(1064, 681);
			this.Controls.Add(this.VersionSubmitText);
			this.Controls.Add(this.PlayButton);
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
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.Button PlayButton;
		private System.Windows.Forms.TextBox VersionSubmitText;
	}
}

