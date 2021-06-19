using System;
using System.Linq;
using System.Windows.Forms;
using System.Collections.Generic;

#pragma warning disable CS0108

namespace Launcher
{
	public class UIManager : Launcher
	{

		private static ComboBox VersionDropdown;
		private static Button MainButton;

		public UIManager(ComboBox VersionDropdown, Button MainButton)
		{
			UIManager.VersionDropdown = VersionDropdown;
			UIManager.MainButton = MainButton;
		}

		/// <summary>
		/// Updates the version dropdown with a list of versions.
		/// </summary>
		/// <param name="VersionDropdown">A reference to the version dropdown</param>
		/// <param name="versions">The list to update version dropdown to</param>
		public static void UpdateVersionDropdown(List<string> versions)
		{
			//Remove any text in the dropdown
			VersionDropdown.Text = string.Empty;
			//Clear any versions in the dropdown
			VersionDropdown.Items.Clear();

			//Update the items in the dropdown
			foreach (string version in versions)
			{
				VersionDropdown.Items.Add(version);
			}

			//Select the first version in the list
			VersionDropdown.SelectedIndex = 0;
		}

		/// <summary>
		/// The user's currently selected version.
		/// </summary>
		/// <returns>The currently selected version</returns>
		public static Version GetSelectedVersion()
		{
			Version version;
			version = new Version(DownloadsManager.Versions[VersionDropdown.SelectedIndex]);

			Console.WriteLine("Selected version: " + version);

			return version;
		}

		/// <summary>
		/// Changes the main button text to "Play".
		/// </summary>
		public static void MainButton_Play()
		{
			MainButton.Text = "Play";
		}

		/// <summary>
		/// Changes the main button text to "Download".
		/// </summary>
		public static void MainButton_Download()
		{
			MainButton.Text = "Download";
		}

		/// <summary>
		/// Changes the main button text to "Downloading".
		/// </summary>
		public static void MainButton_Downloading()
		{
			MainButton.Text = "Downloading";
		}
		/// <summary>
		/// Changes the main button text to "Unzipping".
		/// </summary>
		public static void MainButton_Unzipping()
		{
			MainButton.Text = "Unzipping";
		}

		/// <summary>
		/// Changes the main button text to "Download failed".
		/// </summary>
		public static void MainButton_Failed()
		{
			MainButton.Text = "Download failed";
		}
	}
}
