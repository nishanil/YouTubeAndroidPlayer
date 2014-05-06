using System;
using Android.App;
using Android.Content;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;
using Google.YouTube.Player;
namespace YouTubeDemo
{
	[Activity (Label = "YouTubeDemo")]
	public class FragmentActivity : YouTubeFailureRecoveryActivity
	{
		#region implemented abstract members of YouTubeFailureRecoveryActivity

		protected override IYouTubePlayerProvider GetYouTubePlayerProvider ()
		{
			return FragmentManager.FindFragmentById<YouTubePlayerFragment>(Resource.Id.youtube_fragment);

		}

		#endregion

		protected override void OnCreate (Bundle bundle)
		{
			base.OnCreate (bundle);

			// Set our view from the "main" layout resource
			SetContentView (Resource.Layout.Fragments);
			YouTubePlayerFragment youTubePlayerFragment = FragmentManager.FindFragmentById<YouTubePlayerFragment>(Resource.Id.youtube_fragment);
			youTubePlayerFragment.Initialize(DeveloperKey.Key, this);
		
		}

		public override void OnInitializationSuccess (IYouTubePlayerProvider provider, IYouTubePlayer player, bool wasRestored)
		{
			if (!wasRestored) {
				player.CueVideo("wKJ9KzGQq0w");
			}
		}



	}
}


