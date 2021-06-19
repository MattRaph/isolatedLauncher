using System;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.IO.Compression;
using System.Net;
using System.Windows.Forms;

#pragma warning disable

namespace Launcher
{
	enum LauncherState
	{
		Ready,
		Failed,
		Downloading,
		Updating,
		Launched,
		UpToDate,
	}

	public partial class Launcher : Form
	{
		private string rootPath;
		private string versionFile;
		private string gameZip;
		private string gameExe;

		private string latestVersion;

		private LauncherState _state;
		internal LauncherState State
		{
			get => _state;
			set
			{
				_state = value;
				switch (_state)
				{
					case LauncherState.Ready:
						PlayButton.Text = "Play";
						break;
					case LauncherState.Failed:
						PlayButton.Text = "Something went wrong! - Try again?";
						break;
					case LauncherState.Downloading:
						PlayButton.Text = "Downloading";
						break;
					case LauncherState.Updating:
						PlayButton.Text = "Updating";
						break;
					case LauncherState.Launched:
						PlayButton.Text = "Check for updates";
						break;
					case LauncherState.UpToDate:
						PlayButton.Text = "Up to date!";
						break;
					default:
						break;
				}
			}
		}

		public Launcher()
		{
			InitializeComponent();

			rootPath = Directory.GetCurrentDirectory();
			versionFile = Path.Combine(rootPath, "current.version");
			State = LauncherState.Launched;
		}

		private void InitGamePaths(string version)
		{
			gameZip = Path.Combine(rootPath, version + ".zip");
			gameExe = Path.Combine(rootPath, version, "(.iso)lated.exe");

			Console.WriteLine(gameZip);
			Console.WriteLine(gameExe);
		}

		private void CheckForUpdates()
		{
			if(File.Exists(versionFile))
			{
				Version localVersion = new Version(File.ReadAllText(versionFile));
				VersionSubmitText.Text = localVersion.ToString();
				latestVersion = localVersion.ToString();
				InitGamePaths(latestVersion);

				try
				{
					WebClient webClient = new WebClient();
					Version downloadedLatestVersion = new Version
					(
						webClient.DownloadString
						(
							"https://drive.google.com/uc?export=download&id=112MCP5XByeC3l4NbD2CUV0hF4kcUcJTp"
						)
					);

					if (downloadedLatestVersion.IsNewerThan(localVersion))
					{
						latestVersion = downloadedLatestVersion.ToString();
						File.WriteAllText(versionFile, downloadedLatestVersion.ToString());
						InstallGameFiles(true, new Version(latestVersion));
					}
					else
					{
						State = LauncherState.UpToDate;
					}
				}
				catch(Exception ex)
				{
					State = LauncherState.Failed;
					MessageBox.Show($"An error occured while checking for game updates! Please report this error to me. Ty :)\n {ex}", "Update check error!");
				}
			}
			else
			{
				InstallGameFiles(false, Version.zero);
			}
		}

		#region Version getting

		private Version DownloadLatestVersionFile()
		{
			try
			{
				WebClient webClient = new WebClient();
				return new Version(webClient.DownloadString("https://raw.githubusercontent.com/MattRaph/isolated/main/Version.txt"));
			}
			catch
			{
				return Version.zero;
			}
		}

		private bool DoesVersionExist(Version versionToCheck)
		{
			using (WebClient webClient = new WebClient())
			{
				try
				{
					string version = webClient.DownloadString($"https://github.com/MattRaph/isolated/releases/tag/{versionToCheck}");
				}
				catch
				{
					MessageBox.Show("Please input a valid version number!", "Invalid version number!");
					return false;
				}

				return true;
			}
		}
		#endregion //Version getting

		private bool IsVersionInstalled(Version check)
		{
			if (Directory.Exists(Path.Combine(rootPath, check.ToString())))
				return true;

			return false;
		}

		#region Downloading & Installing
		private void InstallGameFiles(bool isUpdate, Version versionToInstall)
		{
			if(versionToInstall == Version.zero)
			{
				MessageBox.Show("Cannot install N-U1.1 (empty) version!", "Installation error!");
				MessageBox.Show("Installing latest version instead.", "Information");
				versionToInstall = DownloadLatestVersionFile();
			}

			try
			{
				WebClient webClient = new WebClient();
				if(isUpdate)
				{
					State = LauncherState.Updating;
				}
				else
				{
					State = LauncherState.Downloading;
					//versionToInstall = new Version(webClient.DownloadString("https://raw.githubusercontent.com/MattRaph/isolated/main/Version.txt"));
				}
				
				InitGamePaths(versionToInstall.ToString());
				webClient.DownloadFileCompleted += new AsyncCompletedEventHandler(DownloadGameCompletedCallback);
				webClient.DownloadFileAsync(new Uri($"https://github.com/MattRaph/isolated/releases/download/{versionToInstall}/{versionToInstall}.zip"), gameZip, versionToInstall);
			}
			catch (Exception ex)
			{
				State = LauncherState.Failed;
				MessageBox.Show($"An error occured while installing the game! Please report this error to me. Ty :)\n {ex}", "Instalation error!");
			}
		}

		private void DownloadGameCompletedCallback(object sender, AsyncCompletedEventArgs e)
		{
			try
			{
				string version = ((Version)e.UserState).ToString();
				InitGamePaths(version);
				ZipFile.ExtractToDirectory(gameZip, Path.Combine(rootPath, version));
				File.Delete(gameZip);

				if(new Version(version).IsNewerThan(new Version(File.ReadAllText(versionFile))))
				{
					Console.WriteLine($"\n{new Version(File.ReadAllText(versionFile))} > {new Version(version)}");
					Console.WriteLine(new Version(version).IsNewerThan(new Version(File.ReadAllText(versionFile))));
					File.WriteAllText(versionFile, version);
				}

				VersionSubmitText.Text = version;
				State = LauncherState.Ready;
			}
			catch (Exception ex)
			{
				State = LauncherState.Failed;
				MessageBox.Show($"An error occured while finishing the download! Please report this error to me. Ty :)\n {ex}", "Download finish error!");
			}
		}

		#endregion //Downloading & Installing

		private void Launcher_Load(object sender, EventArgs e)
		{
			VersionSubmitText.KeyDown += new KeyEventHandler(ChangeVersion);

			if (File.Exists(versionFile))
			{
				VersionSubmitText.Text = File.ReadAllText(versionFile);
			}
			else
			{
				File.WriteAllText(versionFile, Version.zero.ToString());
				VersionSubmitText.Text = Version.zero.ToString();
			}
		}

		private void button1_Click(object sender, EventArgs e)
		{
			if(State == LauncherState.Launched)
			{
				CheckForUpdates();
			}
			if(State == LauncherState.UpToDate)
			{
				State = LauncherState.Ready;
				return;
			}

			if (File.Exists(gameExe) && State == LauncherState.Ready)
			{
				ProcessStartInfo startInfo = new ProcessStartInfo(gameExe);
				startInfo.WorkingDirectory = Path.Combine(rootPath, DownloadLatestVersionFile().ToString());
				Process.Start(startInfo);

				Close();
			}
			else if (State == LauncherState.Failed)
			{
				CheckForUpdates();
			}
		}

		private void ChangeVersion(object sender, KeyEventArgs e)
		{
			if (e.KeyCode != Keys.Enter) return;

			try
			{
				Version newVer = new Version(VersionSubmitText.Text);
				if(DoesVersionExist(newVer))
				{
					if(IsVersionInstalled(newVer))
					{
						InitGamePaths(newVer.ToString());
						State = LauncherState.Ready;
					}
					else
					{
						InstallGameFiles(false, newVer);
					}
				}
			}
			catch
			{
				MessageBox.Show("Invalid version number! The version number is the releases tag in github.", "Invalid version number");
			}
		}
	}

	struct Version
	{
		internal static Version zero = new Version('N', 'U', 1, 1);

		private char devStage;
		private char releaseType;
		private short devStageReleaseNumber;
		private short releaseTypeVersion;

		internal Version(char devStage, char releaseType, short devStageReleaseNumber, short releaseTypeVersion)
		{
			this.devStage = devStage;
			this.releaseType = releaseType;
			this.devStageReleaseNumber = devStageReleaseNumber;
			this.releaseTypeVersion = releaseTypeVersion;
		}

		internal Version(string version)
		{
			char[] versionChars = version.ToCharArray();

			devStage = versionChars[0];
			releaseType = versionChars[2];
			devStageReleaseNumber = short.Parse(versionChars[3].ToString());
			releaseTypeVersion = short.Parse(versionChars[5].ToString());
		}

		internal bool IsDifferentThan(Version otherVersion)
		{
			if (otherVersion.devStage != devStage) return true;
			else
				if (otherVersion.releaseType != releaseType) return true;
			else
				if (otherVersion.devStageReleaseNumber != devStageReleaseNumber) return true;
			else
				if (otherVersion.releaseTypeVersion != releaseTypeVersion) return true;
			else
				return false;
		}

		internal bool IsNewerThan(Version otherVersion)
		{
			Console.WriteLine($"This version: {this}; Other version: {otherVersion}");

			char[] otherChars = otherVersion.ToString().ToCharArray();

			DevStage devStage = (DevStage)Enum.Parse(typeof(DevStage), otherChars[0].ToString());
			ReleaseType releaseType = (ReleaseType)Enum.Parse(typeof(ReleaseType), otherChars[2].ToString());
			short devStageReleaseNumber = short.Parse(otherChars[3].ToString());
			short releaseTypeVersion = short.Parse(otherChars[5].ToString());

			if ((int)Enum.Parse(typeof(DevStage), this.devStage.ToString()) > (int)devStage) return true;
			else
				if ((int)Enum.Parse(typeof(ReleaseType), this.releaseType.ToString()) > (int)releaseType) return true;
			else
				if (this.devStageReleaseNumber > devStageReleaseNumber) return true;
			else
				if (this.releaseTypeVersion > releaseTypeVersion) return true;
			else
				return false;
		}

		public override string ToString()
		{
			return $"{devStage}-{releaseType}{devStageReleaseNumber}.{releaseTypeVersion}";
		}

		#region Overloaded operators
		public static bool operator ==(Version a, Version b)
		{
			if (a.IsDifferentThan(b)) return false;

			return true;
		}
		public static bool operator !=(Version a, Version b)
		{
			if (a.IsDifferentThan(b)) return true;

			return false;
		}
		#endregion //Overloaded operators

		enum DevStage
		{
			N,
			A,
			B,
			R,
		}
		enum ReleaseType
		{
			U,
			D,
			R,
		}
	}
}