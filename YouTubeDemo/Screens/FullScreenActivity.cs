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
using Android.Content.PM;

namespace YouTubeDemo
{
	//TODO: Some bugs around full screening. Logic revisit needed.
	[Activity (Label = "FullScreenActivity")]			
	public class FullScreenActivity : YouTubeFailureRecoveryActivity, IYouTubePlayerOnFullscreenListener
	{
		private static readonly ScreenOrientation PORTRAIT_ORIENTATION = (Build.VERSION.SdkInt < BuildVersionCodes.Gingerbread) ? ScreenOrientation.Portrait : ScreenOrientation.SensorPortrait;
		private LinearLayout baseLayout;
		private YouTubePlayerView playerView;
		private IYouTubePlayer player = null;
		private Button fullscreenButton;
		private CompoundButton checkbox;
		private View otherViews;

		private bool fullscreen;

		#region implemented abstract members of YouTubeFailureRecoveryActivity

		protected override IYouTubePlayerProvider GetYouTubePlayerProvider ()
		{
			return playerView;
		}

		#endregion

		protected override void OnCreate (Bundle bundle)
		{
			base.OnCreate (bundle);

			SetContentView (Resource.Layout.FullScreen);

			baseLayout = FindViewById<LinearLayout>(Resource.Id.layout);
			playerView = FindViewById<YouTubePlayerView>(Resource.Id.player);
			fullscreenButton = FindViewById<Button>(Resource.Id.fullscreen_button);
			checkbox = FindViewById<CompoundButton>(Resource.Id.landscape_fullscreen_checkbox);
			otherViews = FindViewById<View>(Resource.Id.other_views);

			checkbox.CheckedChange += (sender, e) => {
				int controlFlags = player.FullscreenControlFlags;
				if (e.IsChecked) {
					// If you use the FULLSCREEN_FLAG_ALWAYS_FULLSCREEN_IN_LANDSCAPE, your activity's normal UI
					// should never be laid out in landscape mode (since the video will be fullscreen whenever the
					// activity is in landscape orientation). Therefore you should set the activity's requested
					// orientation to portrait. Typically you would do this in your AndroidManifest.xml, we do it
					// programmatically here since this activity demos fullscreen behavior both with and without
					// this flag).
					RequestedOrientation = PORTRAIT_ORIENTATION;

					controlFlags |= YouTubePlayer.FullscreenFlagAlwaysFullscreenInLandscape;
				} else {
					RequestedOrientation = ScreenOrientation.Sensor;
					controlFlags &= ~YouTubePlayer.FullscreenFlagAlwaysFullscreenInLandscape;
				}
				player.FullscreenControlFlags = controlFlags;
			};

			fullscreenButton.Click += (sender, e) => OnFullscreen(!fullscreen);

			playerView.Initialize(DeveloperKey.Key, this);

			DoLayout();
		}

		private void DoLayout() {
			LinearLayout.LayoutParams playerParams =
				(LinearLayout.LayoutParams) playerView.LayoutParameters;
			if (fullscreen) {
				// When in fullscreen, the visibility of all other views than the player should be set to
				// GONE and the player should be laid out across the whole screen.
				playerParams.Width = LinearLayout.LayoutParams.MatchParent;
				playerParams.Height = LinearLayout.LayoutParams.MatchParent;

				otherViews.Visibility = ViewStates.Gone;
			} else {
				// This layout is up to you - this is just a simple example (vertically stacked boxes in
				// portrait, horizontally stacked in landscape).
				otherViews.Visibility = ViewStates.Visible;
				ViewGroup.LayoutParams otherViewsParams = otherViews.LayoutParameters;
				if (Resources.Configuration.Orientation == Android.Content.Res.Orientation.Landscape) {
					playerParams.Width = otherViewsParams.Width = 0;
					playerParams.Height = LinearLayout.LayoutParams.MatchParent;
					otherViewsParams.Height = LinearLayout.LayoutParams.MatchParent;
					playerParams.Weight = 1;
					baseLayout.Orientation = Orientation.Horizontal;
				} else {
					playerParams.Width = otherViewsParams.Width = LinearLayout.LayoutParams.MatchParent;
					playerParams.Height = LinearLayout.LayoutParams.WrapContent;
					playerParams.Weight = 0;
					otherViewsParams.Height = 0;
					baseLayout.Orientation = Orientation.Vertical;
				}
				SetControlsEnabled();
			}
		}

		private void SetControlsEnabled() {
			checkbox.Enabled = (player != null && Resources.Configuration.Orientation == Android.Content.Res.Orientation.Portrait );
			fullscreenButton.Enabled = (player != null);
		}

		public void OnFullscreen (bool isFullScreen)
		{
			fullscreen = isFullScreen;
			DoLayout();
		}

		public override void OnConfigurationChanged (Android.Content.Res.Configuration newConfig)
		{
			base.OnConfigurationChanged (newConfig);
			DoLayout ();
		}

		public override void OnInitializationSuccess (IYouTubePlayerProvider provider, IYouTubePlayer player, bool wasRestored)
		{
			this.player = player;
			SetControlsEnabled();
			// Specify that we want to handle fullscreen behavior ourselves.
			player.AddFullscreenControlFlag(YouTubePlayer.FullscreenFlagCustomLayout);
			player.SetOnFullscreenListener(this);
			if (!wasRestored) {
				player.CueVideo("avP5d16wEp0");
			}
		}

	}
}

