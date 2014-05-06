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

namespace YouTubeDemo
{
	[Activity (Label = "YouTube Demo", MainLauncher = true)]			
	public class StandAloneActivity : Activity

	{
		private static  int REQ_START_STANDALONE_PLAYER = 1;
		private static  int REQ_RESOLVE_SERVICE_MISSING = 2;

		private static  String VIDEO_ID = "cdgQpa1pUUE";
		private static  String PLAYLIST_ID =  "7E952A67F31C58A3";

		private Button playVideoButton;
		private Button playPlaylistButton;

		private EditText startIndexEditText;
		private EditText startTimeEditText;
		private CheckBox autoplayCheckBox;
		private CheckBox lightboxModeCheckBox;
		protected override void OnCreate (Bundle bundle)
		{
			base.OnCreate (bundle);

			SetContentView(Resource.Layout.StandAlone);

			playVideoButton =  FindViewById<Button>(Resource.Id.start_video_button);
			playPlaylistButton = FindViewById<Button>(Resource.Id.start_playlist_button);
			startIndexEditText = FindViewById<EditText>(Resource.Id.start_index_text);
			startTimeEditText =  FindViewById<EditText>(Resource.Id.start_time_text);
			autoplayCheckBox =   FindViewById<CheckBox>(Resource.Id.autoplay_checkbox);
			lightboxModeCheckBox = FindViewById<CheckBox>(Resource.Id.lightbox_checkbox);

			playVideoButton.Click += (sender, e) => {
				 
				int startIndex; int.TryParse(startIndexEditText.Text, out startIndex);
				int startTimeMillis; int.TryParse(startTimeEditText.Text, out startTimeMillis);
				startTimeMillis *= 1000;
				bool autoplay = autoplayCheckBox.Checked;
				bool lightboxMode = lightboxModeCheckBox.Checked;
				var intent = YouTubeStandalonePlayer.CreateVideoIntent(this, DeveloperKey.Key,VIDEO_ID, startTimeMillis, autoplay, lightboxMode);
				StartActivityForResult(intent, REQ_START_STANDALONE_PLAYER);
			};

			// Create your application here
		}
	}
}

