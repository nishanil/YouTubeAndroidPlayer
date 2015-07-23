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
using Android.Widget;
using Google.YouTube.Player;

namespace YouTubeDemo
{
	//[Activity (Label = "YouTubeFailureRecoveryActivity")]			
	public abstract class YouTubeFailureRecoveryActivity : YouTubeBaseActivity, IYouTubePlayerOnInitializedListener
	{
		private static int RECOVERY_DIALOG_REQUEST = 1;
		public void OnInitializationFailure (IYouTubePlayerProvider provider, YouTubeInitializationResult errorReason)
		{
			if (errorReason.IsUserRecoverableError) {
				errorReason.GetErrorDialog(this, RECOVERY_DIALOG_REQUEST).Show();
			} else {
				String errorMessage = String.Format(GetString(Resource.String.error_player), errorReason.ToString());
				Toast.MakeText(this, errorMessage, ToastLength.Long).Show();
			}
		}

		public virtual void OnInitializationSuccess (IYouTubePlayerProvider provider, IYouTubePlayer player, bool wasRestored)
		{
			//throw new NotImplementedException ();
		}

		protected override void OnActivityResult (int requestCode, Result resultCode, Intent data)
		{
			//base.OnActivityResult (requestCode, resultCode, data);
			if(requestCode == RECOVERY_DIALOG_REQUEST)
			GetYouTubePlayerProvider ().Initialize (DeveloperKey.Key, this);
		}

		protected abstract IYouTubePlayerProvider GetYouTubePlayerProvider();

	}
}

