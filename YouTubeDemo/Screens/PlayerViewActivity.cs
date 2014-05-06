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
	[Activity (Label = "PlayerViewActivity")]			
	public class PlayerViewActivity : YouTubeFailureRecoveryActivity
	{
		#region implemented abstract members of YouTubeFailureRecoveryActivity

		protected override IYouTubePlayerProvider GetYouTubePlayerProvider ()
		{
			return FindViewById<YouTubePlayerView> (Resource.Id.youtube_view);
		}

		#endregion

		protected override void OnCreate (Bundle bundle)
		{
			base.OnCreate (bundle);

			// Create your application here
			SetContentView (Resource.Layout.PlayerView);

			YouTubePlayerView youTubeView = FindViewById<YouTubePlayerView>(Resource.Id.youtube_view);
			youTubeView.Initialize(DeveloperKey.Key, this);
		}

		public override void OnInitializationSuccess (IYouTubePlayerProvider provider, IYouTubePlayer player, bool wasRestored)
		{
			if (!wasRestored) {
				player.CueVideo("wKJ9KzGQq0w");
			}
		}
	}
}

