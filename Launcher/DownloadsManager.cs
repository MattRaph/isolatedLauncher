using System;
using System.IO;
using System.Net;
using System.Linq;
using System.Diagnostics;
using System.Windows.Forms;
using System.ComponentModel;
using System.Collections.Generic;
using System.IO.Compression;

namespace Launcher
{
	public class DownloadsManager : Launcher
	{
		//Paths:
		public static string RootPath { get; private set; }

		//Lists:
		public static List<string> Versions { get; private set; }
		public static List<string> LocalVersions { get; private set; }

		public DownloadsManager()
		{
			RootPath = Directory.GetCurrentDirectory();

			//Create static lists for online and local versions to be (a little) more efficient
			Versions = GetAllVersions();
			LocalVersions = GetLocalVersions();
		}

		/// <summary>
		/// All of the current (.iso)lated versions from GitHub.
		/// </summary>
		/// <returns>A list of all the versions of (.iso)lated</returns>
		public static List<string> GetAllVersions()
		{
			//Assign the URL to a string with a short identifier
			string url = "https://raw.githubusercontent.com/MattRaph/isolated/main/Version.txt";

			try
			{
				using (WebClient webClient = new WebClient())
				{
					//Get the version entire string
					string unsplitVersions = webClient.DownloadString(url);
					Console.WriteLine("Entire version string: " + unsplitVersions);

					//Split at every comma
					List<string> splitVersions = unsplitVersions.Split(',').ToList();

					//Remove any spaces
					for (int i = 0; i < splitVersions.Count; i++)
					{
						if(splitVersions[i].StartsWith(" "))
						{
							List<char> versionChars = splitVersions[i].ToCharArray().ToList();
							versionChars.RemoveAt(0);
							splitVersions[i] = new string(versionChars.ToArray());
						}
					}

					//Remove the end line character from the last version
					int lastIndex = splitVersions.Count - 1;
					Console.WriteLine("Length of first string: " + splitVersions[0].Length);
					Console.WriteLine("Length of last string: " + splitVersions[lastIndex].Length);
					if(splitVersions[lastIndex].Length > 6)
					{
						splitVersions[lastIndex] = splitVersions[lastIndex].Remove(6);
						Console.WriteLine("Split versions last string without new line: "
							+ splitVersions[lastIndex]);
					}

					return splitVersions;
				}
			}
			catch
			{
				//Show a message box reporting the issue
				MessageBox.Show
				(
					"An error occured while obtaining the version list! Please restart the application.",
					"Fatal error!"
				);
				//Close the program
				Instance.Close();
				return null;
			}
		}
		/// <summary>
		/// All of the versions of (.iso)lated downloaded on this computer.
		/// </summary>
		/// <returns>A list of strings of all the downloaded versions</returns>
		public static List<string> GetLocalVersions()
		{
			//Instantiate a list
			List<string> LocalVersions = new List<string>();

			//Loop through the online versions
			for (int i = 0; i < Versions.Count; i++)
			{
				Console.WriteLine("Checking if " + Versions[i] + " is downloaded locally");

				//Check if the directory exists
				if (Directory.Exists(Path.Combine(RootPath, Versions[i])))
				{
					//Add it to the list
					Console.WriteLine(Versions[i] + " is downloaded");
					LocalVersions.Add(Versions[i]);
				}
				else
				{
					Console.WriteLine(Versions[i] + " is not downloaded");
				}
			}

			//Return the list
			return LocalVersions;
		}

		/// <summary>
		/// The latest version of (.iso)lated online.
		/// </summary>
		/// <returns>The latest version of (.iso)lated represented as a Version</returns>
		public static Version LatestOnlineVersion() { return new Version(Versions[0]); }
		/// <summary>
		/// The latest version of (.iso)lated on this computer.
		/// </summary>
		/// <returns>The latest version of (.iso)lated on this computer represented as a Version</returns>
		public static Version LatestLocalVersion() { return new Version(LocalVersions[0]); }

		/// <summary>
		/// Is this version installed on this computer?
		/// </summary>
		/// <param name="version">The version to check</param>
		/// <returns>True if the specified version is installed</returns>
		public static bool IsVersionInstalled(Version version)
		{
			return LocalVersions.Contains(version.ToString());
		}

		/// <summary>
		/// Downloads an (.iso)lated build from GitHub.
		/// </summary>
		/// <param name="version">The version of the build</param>
		public static void DownloadVersion(Version version)
		{
			//Set the state to downloading
			State = LauncherState.Downloading;

			//Assign the URL to a string with a short identifier
			string url = $"https://github.com/MattRaph/isolated/releases/download/{version}/{version}.zip";

			try
			{
				using (WebClient webClient = new WebClient())
				{
					//Start async download
					webClient.DownloadFileCompleted += new AsyncCompletedEventHandler(UnzipDownload);
					webClient.DownloadFileAsync(new Uri(url), version.ToString() + ".zip", version);
				}
			}
			catch
			{
				//Show a message box reporting the issue
				MessageBox.Show
				(
					"An error occured while downloading! I'm sorry :(",
					"Downloading error!"
				);
				//Close the program
				Instance.Close();
			}
		}
		/// <summary>
		/// Unzips the download.
		/// </summary>
		/// <param name="sender">The sender</param>
		/// <param name="e">Async completed event arguments</param>
		private static void UnzipDownload(object sender, AsyncCompletedEventArgs e)
		{
			try
			{
				//Unzipping
				State = LauncherState.Unzipping;

				//Get the version
				Version version = (Version)e.UserState;

				//Paths
				string path = Path.Combine(RootPath, version.ToString());
				string zip = path + ".zip";

				//Unzip and delete zip file
				ZipFile.ExtractToDirectory(zip, path);
				File.Delete(zip);

				//Reload local downloads
				LocalVersions = GetLocalVersions();

				//Ready!
				State = LauncherState.Ready;
			}
			catch (Exception ex)
			{
				State = LauncherState.Failed;

				MessageBox.Show
				(
					$"An error occured while uncompressing the download. Sorry about that :( {ex}",
					"Zip error"
				);
			}
		}

		/// <summary>
		/// Launches the game
		/// </summary>
		/// <param name="version">The version of the game to launch</param>
		public static void Play(Version version)
		{
			string path = Path.Combine(RootPath, version.ToString());
			string executablepath = path + "\\(.iso)lated.exe";
			ProcessStartInfo startInfo = new ProcessStartInfo(executablepath);
			startInfo.WorkingDirectory = path;
			Process.Start(startInfo);
		}
	}
}
