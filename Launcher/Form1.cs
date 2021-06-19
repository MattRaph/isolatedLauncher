using System;
using System.Linq;
using System.Windows.Forms;
using System.Collections.Generic;

#pragma warning disable CS0660
#pragma warning disable CS0661

namespace Launcher
{
	public enum LauncherState
	{
		Ready,
		AwaitingDownloadConfirmation,
		Downloading,
		Unzipping,
		Failed,
	}

	public partial class Launcher : Form
	{
		private static LauncherState _state;
		protected static LauncherState State
		{
			get => _state;
			set
			{
				_state = value;

				switch (_state)
				{
					case LauncherState.Ready:
						UIManager.MainButton_Play();
						break;
					case LauncherState.AwaitingDownloadConfirmation:
						UIManager.MainButton_Download();
						break;
					case LauncherState.Downloading:
						UIManager.MainButton_Downloading();
						break;
					case LauncherState.Unzipping:
						UIManager.MainButton_Unzipping();
						break;
					case LauncherState.Failed:
						UIManager.MainButton_Failed();
						break;
					default:
						break;
				}
			}
		}

		public static Launcher Instance { get; private set; }

		public Launcher()
		{
			Instance = this;
			InitializeComponent();
		}

		private void InitializeOtherClasses()
		{
			new UIManager(VersionDropdown, MainButton);
			new DownloadsManager();
		}

		private void Launcher_Load(object sender, EventArgs e)
		{
			//Initialization
			InitializeOtherClasses();

			//Update the versions dropdown
			UIManager.UpdateVersionDropdown(DownloadsManager.Versions);
		}

		private void MainButton_Click(object sender, EventArgs e)
		{
			if(State == LauncherState.AwaitingDownloadConfirmation)
			{
				DownloadsManager.DownloadVersion(UIManager.GetSelectedVersion());
				return;
			}

			if(State == LauncherState.Ready)
			{
				DownloadsManager.Play(UIManager.GetSelectedVersion());
				return;
			}

			if(State == LauncherState.Failed)
			{
				State = LauncherState.AwaitingDownloadConfirmation;
				return;
			}
		}

		private void VersionDropdown_SelectedIndexChanged(object sender, EventArgs e)
		{
			if (DownloadsManager.IsVersionInstalled(UIManager.GetSelectedVersion()))
			{
				State = LauncherState.Ready;
			}
			else
			{
				State = LauncherState.AwaitingDownloadConfirmation;
			}
		}
	}

	/// <summary>
	/// A struct for handling anything version related
	/// </summary>
	public struct Version
	{
		internal static Version zero = new Version('N', 'U', 1, 1);

		private char devStage;
		private char releaseType;
		private int devStageReleaseNumber;
		private int releaseTypeVersion;

		internal Version(char devStage, char releaseType, short devStageReleaseNumber, short releaseTypeVersion)
		{
			this.devStage = devStage;
			this.releaseType = releaseType;
			this.devStageReleaseNumber = devStageReleaseNumber;
			this.releaseTypeVersion = releaseTypeVersion;
		}

		internal Version(string version)
		{
			List<char> versionChars = version.ToCharArray().ToList();

			devStage = versionChars[0];
			releaseType = versionChars[2];
			devStageReleaseNumber = int.Parse(versionChars[3].ToString());
			releaseTypeVersion = int.Parse(versionChars[5].ToString());
		}

		/// <summary>
		/// Is this version different than the specified version?
		/// </summary>
		/// <param name="otherVersion">The version to compare to</param>
		/// <returns>True if this version is different than the otherVersion</returns>
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

		/// <summary>
		/// Is this version newer than the specified version?
		/// </summary>
		/// <param name="otherVersion">The version to compare to</param>
		/// <returns>True if this version is newer than the otherVersion</returns>
		internal bool IsNewerThan(Version otherVersion)
		{
			Console.WriteLine($"This version: {this}; Other version: {otherVersion}");

			char[] otherChars = otherVersion.ToString().ToCharArray();

			DevStage devStage = (DevStage)Enum.Parse(typeof(DevStage), otherChars[0].ToString());
			ReleaseType releaseType = (ReleaseType)Enum.Parse(typeof(ReleaseType), otherChars[2].ToString());
			int devStageReleaseNumber = int.Parse(otherChars[3].ToString());
			int releaseTypeVersion = int.Parse(otherChars[5].ToString());

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