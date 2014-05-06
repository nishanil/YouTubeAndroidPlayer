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
using Android.Graphics.Drawables;


namespace YouTubeDemo
{
	[Activity (Label = "ActionBarActivity")]			
	public class ActionBarActivity : YouTubeFailureRecoveryActivity, IYouTubePlayerOnFullscreenListener
	{

		private ActionBarPaddedFrameLayout viewContainer;
		private YouTubePlayerFragment playerFragment;
		private View tutorialTextView;


		public void OnFullscreen (bool fullscreen)
		{
			viewContainer.SetEnablePadding(!fullscreen);

			ViewGroup.LayoutParams playerParams = playerFragment.View.LayoutParameters;
			if (fullscreen) {
				tutorialTextView.Visibility = ViewStates.Gone;
				playerParams.Width = ViewGroup.LayoutParams.MatchParent;
				playerParams.Height = ViewGroup.LayoutParams.MatchParent;
			} else {
				tutorialTextView.Visibility = ViewStates.Visible;
				playerParams.Width = 0;
				playerParams.Height = ViewGroup.LayoutParams.WrapContent;
			}
		}

		#region implemented abstract members of YouTubeFailureRecoveryActivity

		protected override IYouTubePlayerProvider GetYouTubePlayerProvider ()
		{
			return FragmentManager.FindFragmentById<YouTubePlayerFragment>(Resource.Id.player_fragment);

		}

		#endregion

		protected override void OnCreate (Bundle bundle)
		{
			base.OnCreate (bundle);

			SetContentView (Resource.Layout.ActionBar);

			viewContainer =  FindViewById<ActionBarPaddedFrameLayout>(Resource.Id.view_container);
			playerFragment =
				FragmentManager.FindFragmentById<YouTubePlayerFragment>(Resource.Id.player_fragment);
			tutorialTextView = FindViewById(Resource.Id.tutorial_text);
			playerFragment.Initialize(DeveloperKey.Key, this);
			viewContainer.SetActionBar(ActionBar);

			// Action bar background is transparent by default.
			// 0xAA0000000
			ActionBar.SetBackgroundDrawable(new ColorDrawable(Android.Graphics.Color.Black));

			// Create your application here
		}

		public override void OnInitializationSuccess (IYouTubePlayerProvider provider, IYouTubePlayer player, bool wasRestored)
		{
			player.AddFullscreenControlFlag(YouTubePlayer.FullscreenFlagCustomLayout);
			player.SetOnFullscreenListener(this);

			if (!wasRestored) {
				player.CueVideo("9c6W4CCU9M4");
			}
		}
	}

	/** * This is a FrameLayout which adds top-padding equal to the height of the ActionBar unless
   * disabled by {@link #setEnablePadding(boolean)}.
   */
	public class ActionBarPaddedFrameLayout : FrameLayout {

		private ActionBar actionBar;
		private bool paddingEnabled;


		public ActionBarPaddedFrameLayout(Context context) : this(context, null){

		}

		public ActionBarPaddedFrameLayout(Context context, Android.Util.IAttributeSet attrs) : this(context, attrs, 0) {

		}

		public ActionBarPaddedFrameLayout(Context context, Android.Util.IAttributeSet attrs, int defStyle) : base(context, attrs, defStyle)
		{

			paddingEnabled = true;
		}

		public void SetActionBar(ActionBar actionBar) {
			this.actionBar = actionBar;
			RequestLayout();
		}

		public void SetEnablePadding(bool enable) {
			paddingEnabled = enable;
			RequestLayout();
		}

		protected override void OnMeasure (int widthMeasureSpec, int heightMeasureSpec)
		{
			int topPadding =
				paddingEnabled && actionBar != null && actionBar.IsShowing ? actionBar.Height : 0;
			SetPadding(0, topPadding, 0, 0);
			base.OnMeasure (widthMeasureSpec, heightMeasureSpec);
		}

		}

}

