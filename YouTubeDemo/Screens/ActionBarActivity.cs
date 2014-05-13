
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


using Android.App;
using Android.Content;
using Android.OS;
using Android.Views;
using Android.Widget;
using Google.YouTube.Player;
using Android.Graphics.Drawables;


namespace YouTubeDemo
{
	/**
	 * A sample showing how to use the ActionBar as an overlay when the video is playing in fullscreen.
	 *
	 * The ActionBar is the only view allowed to overlay the player, so it is a useful place to put
	 * custom application controls when the video is in fullscreen. The ActionBar can not change back
	 * and forth between normal mode and overlay mode, so to make sure our application's content
	 * is not covered by the ActionBar we want to pad our root view when we are not in fullscreen.
	 */
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

