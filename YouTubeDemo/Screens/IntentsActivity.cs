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
	[Activity (Label = "IntentsActivity")]			
	public class IntentsActivity : Activity
	{
		private const string extraLocalOnly = "android.intent.extra.LOCAL_ONLY";
		private const string videoId = "-Uwjt32NvVA";
		private const string playlistId = "PLF3DFB800F05F551A";
		private const string userId = "Google";
		private const int selectVideoRequest = 1000;

		ListView intentsList;
		List<IntentItem> intentItems;

		void OnListItemClick (object sender, Android.Widget.AdapterView.ItemClickEventArgs e)
		{
			Intent intent;
			switch (intentItems [e.Position].Type) {
			case IntentType.PlayVideo:
				intent = YouTubeIntents.CreatePlayVideoIntentWithOptions (this, videoId, true, false);
				StartActivity (intent);
				break;
			case IntentType.OpenPlaylist:
				intent = YouTubeIntents.CreateOpenPlaylistIntent (this, playlistId);
				StartActivity (intent);
				break;
			case IntentType.PlayPlaylist:
				intent = YouTubeIntents.CreatePlayPlaylistIntent (this, playlistId);
				StartActivity (intent);
				break;
			case IntentType.OpenSearch:
				intent = YouTubeIntents.CreateSearchIntent (this, userId);
				StartActivity (intent);
				break;
			case IntentType.OperUser:
				intent = YouTubeIntents.CreateUserIntent (this, userId);
				StartActivity (intent);
				break;
			case IntentType.UploadVideo:
				// This will load a picker view in the users' gallery.
				// The upload activity is started in the function onActivityResult.
				intent = new Intent (Intent.ActionPick, null).SetType ("video/*");
				intent.PutExtra (extraLocalOnly, true);
				StartActivityForResult (intent, selectVideoRequest);
				break;
			}
		}

		protected override void OnCreate (Bundle bundle)
		{
			base.OnCreate (bundle);

			SetContentView (Resource.Layout.Intents);

			intentsList = FindViewById<ListView> (Resource.Id.intent_list);


			intentItems = new List<IntentItem>();
			intentItems.Add(new IntentItem () { Title = "Play Video", Type = IntentType.PlayVideo });
			intentItems.Add(new IntentItem () { Title = "Open Playlist", Type = IntentType.OpenPlaylist });
			intentItems.Add(new IntentItem () { Title = "Play Playlist", Type = IntentType.PlayPlaylist });
			intentItems.Add(new IntentItem () { Title = "Open User", Type = IntentType.OperUser });
			intentItems.Add(new IntentItem () { Title = "Open Search Results", Type = IntentType.OpenSearch });
			intentItems.Add(new IntentItem () { Title = "Upload Video", Type = IntentType.UploadVideo });


			intentsList.Adapter = new IntentsAdapter (this, intentItems);

			intentsList.ItemClick += OnListItemClick;

			TextView youTubeVersionText = FindViewById<TextView>(Resource.Id.youtube_version_text);
			String version = YouTubeIntents.GetInstalledYouTubeVersionName(this);
			if (version != null) {
				youTubeVersionText.Text = String.Format(GetString(Resource.String.youtube_currently_installed), version);
			} else {
				youTubeVersionText.Text = (GetString(Resource.String.youtube_not_installed));
			}

		}

		protected override void OnActivityResult (int requestCode, Result resultCode, Intent data)
		{
			if (resultCode == Result.Ok) {
				switch (requestCode) {
				case selectVideoRequest:
					Intent intent = YouTubeIntents.CreateUploadIntent(this, data.Data);
					StartActivity(intent);
					break;
				}
			}
			base.OnActivityResult (requestCode, resultCode, data);
		}
	}


}

