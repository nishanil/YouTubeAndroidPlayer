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


