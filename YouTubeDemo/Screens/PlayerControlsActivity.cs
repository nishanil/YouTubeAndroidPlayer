
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Google.YouTube.Player;
using Android.Views.InputMethods;

namespace YouTubeDemo
{
	[Activity (Label = "PlayerControlsActivity")]			
	public class PlayerControlsActivity : YouTubeFailureRecoveryActivity
	{
		private List<ListEntry> entries = new List<ListEntry>() {
			new ListEntry("Androidify App", "irH3OSOskcE", false),
			new ListEntry("Chrome Speed Tests", "nCgQDjiotG0", false),
			new ListEntry("Playlist: Google I/O 2012", "PL56D792A831D0C362", true) };

		private const String KEY_CURRENTLY_SELECTED_ID = "currentlySelectedId";

		private YouTubePlayerView youTubePlayerView;

		public static IYouTubePlayer Player {
			get;
			set;
		}

		private ArrayAdapter<ListEntry> videoAdapter;
		private Spinner videoChooser;
		private Button playButton;
		private Button pauseButton;
		private EditText skipTo;
		private RadioGroup styleRadioGroup;

		private MyPlaylistEventListener playlistEventListener;
		private MyPlayerStateChangeListener playerStateChangeListener;
		private MyPlaybackEventListener playbackEventListener;

		private int currentlySelectedPosition;
		private String currentlySelectedId;

		public static StringBuilder LogString {
			get;
			set;
		}

		public static TextView EventLogTextView {
			get;
			set;
		}

		public static TextView StateTextView {
			get;
			set;
		}



		#region implemented abstract members of YouTubeFailureRecoveryActivity

		protected override Google.YouTube.Player.IYouTubePlayerProvider GetYouTubePlayerProvider ()
		{
			return youTubePlayerView;
		}

		#endregion

		protected override void OnCreate (Bundle bundle)
		{
			base.OnCreate (bundle);

			SetContentView (Resource.Layout.PlayerControls);

			youTubePlayerView =  FindViewById<YouTubePlayerView>(Resource.Id.youtube_view);
			StateTextView = FindViewById<TextView> (Resource.Id.state_text);
			videoChooser = FindViewById<Spinner>(Resource.Id.video_chooser);
			playButton = FindViewById<Button>(Resource.Id.play_button);
			pauseButton = FindViewById<Button>(Resource.Id.pause_button);
			skipTo = FindViewById<EditText>(Resource.Id.skip_to_text);
			EventLogTextView = FindViewById<TextView>(Resource.Id.event_log);

			styleRadioGroup = FindViewById<RadioGroup>(Resource.Id.style_radio_group);
			(FindViewById<RadioButton> (Resource.Id.style_default)).CheckedChange += HandleCheckedChange;
			(FindViewById<RadioButton>(Resource.Id.style_minimal)).CheckedChange += HandleCheckedChange;
			(FindViewById<RadioButton>(Resource.Id.style_chromeless)).CheckedChange += HandleCheckedChange;

			// Initialize PlayerHelper - Used by Listeners
			LogString = new StringBuilder();


			videoAdapter = new ArrayAdapter<ListEntry>(this, Android.Resource.Layout.SimpleSpinnerItem, entries);
			videoAdapter.SetDropDownViewResource(Android.Resource.Layout.SimpleSpinnerDropDownItem);
			videoChooser.Adapter = videoAdapter;

			videoChooser.ItemSelected += (object sender, AdapterView.ItemSelectedEventArgs e) => {
				currentlySelectedPosition = e.Position;
				PlayVideoAtSelection();
			};

			playButton.Click += (sender, e) => Player.Play ();
			pauseButton.Click += (sender, e) => Player.Pause();
			skipTo.EditorAction += (sender, e) => {
					int skipToSecs;
					int.TryParse(skipTo.Text, out skipToSecs);
				Player.SeekToMillis(skipToSecs * 1000);
					InputMethodManager imm = (InputMethodManager)GetSystemService(Context.InputMethodService);
					imm.HideSoftInputFromWindow(skipTo.WindowToken, 0);
			};

			youTubePlayerView.Initialize(DeveloperKey.Key, this);

			playlistEventListener = new MyPlaylistEventListener();
			playerStateChangeListener = new MyPlayerStateChangeListener();
			playbackEventListener = new MyPlaybackEventListener();

			SetControlsEnabled(false);
		}

		public override void OnInitializationSuccess (IYouTubePlayerProvider provider, IYouTubePlayer player, bool wasRestored)
		{
			Player = player;
			Player.SetPlaylistEventListener(playlistEventListener);
			Player.SetPlayerStateChangeListener(playerStateChangeListener);
			Player.SetPlaybackEventListener(playbackEventListener);

			if (!wasRestored) {
				PlayVideoAtSelection();
			}
			SetControlsEnabled(true);
		}


		private void PlayVideoAtSelection() {
			ListEntry selectedEntry = videoAdapter.GetItem(currentlySelectedPosition);
			if (selectedEntry.Id != currentlySelectedId && Player != null) {
				currentlySelectedId = selectedEntry.Id;
				if (selectedEntry.IsPlayList) {
					Player.CuePlaylist(selectedEntry.Id);
				} else {
					Player.CueVideo(selectedEntry.Id);
				}
			}
		}
			

		void HandleCheckedChange (object sender, CompoundButton.CheckedChangeEventArgs e)
		{
			if (e.IsChecked && Player != null) {
				switch (((Button)sender).Id) {
				case Resource.Id.style_default:
					Player.SetPlayerStyle(YouTubePlayerPlayerStyle.Default);
					break;
				case Resource.Id.style_minimal:
					Player.SetPlayerStyle(YouTubePlayerPlayerStyle.Minimal);
					break;
				case Resource.Id.style_chromeless:
					Player.SetPlayerStyle(YouTubePlayerPlayerStyle.Chromeless);
					break;
				}
			}
			
		}

		protected override void OnSaveInstanceState (Bundle outState)
		{
			base.OnSaveInstanceState (outState);
			outState.PutString (KEY_CURRENTLY_SELECTED_ID, currentlySelectedId);
		}

		protected override void OnRestoreInstanceState (Bundle savedInstanceState)
		{
			base.OnRestoreInstanceState (savedInstanceState);
			currentlySelectedId = savedInstanceState.GetString(KEY_CURRENTLY_SELECTED_ID);
		}

		private void SetControlsEnabled(bool enabled) {
			playButton.Enabled = enabled;
			pauseButton.Enabled = enabled;
			skipTo.Enabled = enabled;
			videoChooser.Enabled = enabled;
			for (int i = 0; i < styleRadioGroup.ChildCount; i++) {
				styleRadioGroup.GetChildAt(i).Enabled = enabled;
			}
		}

		#region Helpers
		public static void Log(string message)
		{
			LogString.AppendLine(message);
			EventLogTextView.Text = LogString.ToString();
		}
		public static void UpdateState ()
		{
			StateTextView.Text = string.Format ("Current State: {0} {1} {2}", MyPlayerStateChangeListener.PlayerState, MyPlaybackEventListener.PlaybackState, MyPlaybackEventListener.BufferingState);
		}

		public static string GetTimesText() {
			return String.Format("({0}/{1})", FormatTime(Player.CurrentTimeMillis), FormatTime(Player.DurationMillis));
		}

		public static string FormatTime(int millis) {
			int seconds = millis / 1000;
			int minutes = seconds / 60;
			int hours = minutes / 60;
			return (hours == 0 ? "" : hours + ":")
				+ String.Format("{0}:{1}", minutes % 60, seconds % 60);
		}
		#endregion
			
	}
		


	class MyPlaybackEventListener : Java.Lang.Object, IYouTubePlayerPlaybackEventListener
	{

		public static string PlaybackState = "NOT_PLAYING";
		public static string BufferingState = "";
		#region IYouTubePlayerPlaybackEventListener implementation

		public void OnBuffering (bool p0)
		{
			BufferingState = p0 ? "(BUFFERING)" : "";
			PlayerControlsActivity.UpdateState();
			PlayerControlsActivity.Log("\t\t" + (p0 ? "BUFFERING " : "NOT BUFFERING ") + PlayerControlsActivity.GetTimesText());
		}

		public void OnPaused ()
		{
			PlaybackState = "PAUSED";
			PlayerControlsActivity.UpdateState();
			PlayerControlsActivity.Log("\tPAUSED " + PlayerControlsActivity.GetTimesText());
		}

		public void OnPlaying ()
		{
			PlaybackState = "PLAYING";
			PlayerControlsActivity.UpdateState();
			PlayerControlsActivity.Log("\tPLAYING " + PlayerControlsActivity.GetTimesText());
		}

		public void OnSeekTo (int p0)
		{
			PlayerControlsActivity.Log(String.Format("\tSEEKTO: ({0}/{1})",
				PlayerControlsActivity.FormatTime(p0),
				PlayerControlsActivity.FormatTime(PlayerControlsActivity.Player.DurationMillis)));
		}

		public void OnStopped ()
		{
			PlaybackState = "STOPPED";
			PlayerControlsActivity.UpdateState();
			PlayerControlsActivity.Log("\tSTOPPED");
		}

		#endregion
	}
		

	class MyPlaylistEventListener : Java.Lang.Object, IYouTubePlayerPlaylistEventListener 
	{

		#region IYouTubePlayerPlaylistEventListener implementation

		public void OnNext ()
		{
			PlayerControlsActivity.Log ("NEXT VIDEO");
		}

		public void OnPlaylistEnded ()
		{
			PlayerControlsActivity.Log ("PLAYLIST ENDED");
		}

		public void OnPrevious ()
		{
			PlayerControlsActivity.Log ("PREVIOUS VIDEO");
		}

		#endregion


	}

	class MyPlayerStateChangeListener : Java.Lang.Object, IYouTubePlayerPlayerStateChangeListener{

		public static string PlayerState = "UNINITIALIZED";

		#region IYouTubePlayerPlayerStateChangeListener implementation

		public void OnAdStarted ()
		{
			PlayerState = "AD_STARTED";
			PlayerControlsActivity.UpdateState();
			PlayerControlsActivity.Log(PlayerState);
		}

		public void OnError (YouTubePlayerErrorReason p0)
		{
			PlayerState = "ERROR (" + p0 + ")";
			if (p0 == YouTubePlayerErrorReason.UnexpectedServiceDisconnection) {
				// When this error occurs the player is released and can no longer be used.
				PlayerControlsActivity.Player = null;
				//TODO: No Access! 
				//setControlsEnabled(false);
			}
			PlayerControlsActivity.UpdateState();
			PlayerControlsActivity.Log(PlayerState);
		}

		public void OnLoaded (string p0)
		{
			PlayerState = String.Format("VIDEO LOADED");
			PlayerControlsActivity.UpdateState();
			PlayerControlsActivity.Log(PlayerState);
		}

		public void OnLoading ()
		{
			PlayerState = "LOADING";
			PlayerControlsActivity.UpdateState();
			PlayerControlsActivity.Log(PlayerState);
		}

		public void OnVideoEnded ()
		{
			PlayerState = "VIDEO_ENDED";
			PlayerControlsActivity.UpdateState();
			PlayerControlsActivity.Log(PlayerState);
		}

		public void OnVideoStarted ()
		{
			PlayerState = "VIDEO_STARTED";
			PlayerControlsActivity.UpdateState();
			PlayerControlsActivity.Log(PlayerState);
		}

		#endregion



	}


	 class ListEntry {

		public string Title {
			get;
			set;
		}
		public string Id {
			get;
			set;
		}

		public bool IsPlayList {
			get;
			set;
		}

		public ListEntry(String title, String videoId, bool isPlaylist) {
			this.Title = title;
			this.Id = videoId;
			this.IsPlayList = isPlaylist;
		}

		public override string ToString ()
		{
			return Title;
		}

	}


}

