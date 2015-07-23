/*
 *  Ported to C# by Nish Anil, Xamarin (@nishanil on twitter)
 * 
 * Copyright 2012 Google Inc. All Rights Reserved.
 *
 * Licensed under the Apache License, Version 2.0 (the "License");
 * you may not use this file except in compliance with the License.
 * You may obtain a copy of the License at
 *
 *      http://www.apache.org/licenses/LICENSE-2.0
 *
 * Unless required by applicable law or agreed to in writing, software
 * distributed under the License is distributed on an "AS IS" BASIS,
 * WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
 * See the License for the specific language governing permissions and
 * limitations under the License.
 */
using System;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Widget;
using Google.YouTube.Player;

namespace YouTubeDemo
{
	[Activity (Label = "YouTube Demo")]			
	public class StandAloneActivity : Activity

	{
		private const int RegStartStandalonePlayer= 1;
		//private static  int REQ_RESOLVE_SERVICE_MISSING = 2;

		private static  String VIDEO_ID = "cdgQpa1pUUE";
		private static  String PLAYLIST_ID =  "7E952A67F31C58A3";

		private Button playVideoButton;
		private Button playPlaylistButton;

		private EditText startIndexEditText;
		private EditText startTimeEditText;
		private CheckBox autoplayCheckBox;
		private CheckBox lightboxModeCheckBox;

		void Play (string id)
		{
			int startIndex; 
			int startTimeMillis; 
			bool autoplay;
			bool lightboxMode;
			int.TryParse (startIndexEditText.Text, out startIndex);
			int.TryParse (startTimeEditText.Text, out startTimeMillis);
			startTimeMillis *= 1000;
			autoplay = autoplayCheckBox.Checked;
			lightboxMode = lightboxModeCheckBox.Checked;
			var intent = YouTubeStandalonePlayer.CreateVideoIntent (this, DeveloperKey.Key, id, startTimeMillis, autoplay, lightboxMode);
			StartActivityForResult (intent, RegStartStandalonePlayer);
		}

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

			playVideoButton.Click += (sender, e) => Play (VIDEO_ID);

			playPlaylistButton.Click += (sender, e) => Play (PLAYLIST_ID);

		}

		protected override void OnActivityResult (int requestCode, Result resultCode, Intent data)
		{
			base.OnActivityResult (requestCode, resultCode, data);
			if (requestCode == RegStartStandalonePlayer && resultCode != Result.Ok) {
				YouTubeInitializationResult errorReason =
					YouTubeStandalonePlayer.GetReturnedInitializationResult (data);
				if (errorReason.IsUserRecoverableError) {
					errorReason.GetErrorDialog (this, 0).Show ();
				} else {
					String errorMessage =
						String.Format (GetString (Resource.String.error_player), errorReason.ToString ());
					Toast.MakeText (this, errorMessage, ToastLength.Long).Show ();
				}
			}
		}
	}
}

