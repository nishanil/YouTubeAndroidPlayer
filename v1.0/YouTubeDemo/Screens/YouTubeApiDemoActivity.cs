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
using System.Collections.Generic;
using Android.App;
using Android.OS;
using Android.Views;
using Android.Widget;

namespace YouTubeDemo
{
	[Activity (Label = "YouTubeApiDemoActivity", MainLauncher = true)]			
	public class YouTubeApiDemoActivity : ListActivity
	{
		List<DemoItem> demoItems;
		protected override void OnCreate (Bundle bundle)
		{
			base.OnCreate (bundle);

			demoItems = new List<DemoItem>{ 
				new DemoItem(){ Id = 1, Title = "Player View Demo", IsEnabled = true },
				new DemoItem(){ Id = 2, Title = "Standalone Player Demo", IsEnabled = true },
				new DemoItem(){ Id = 3, Title = "Fragment Player Demo", IsEnabled = true },
				new DemoItem(){ Id = 4, Title = "Full Screen Player Demo", IsEnabled = true },
				new DemoItem(){ Id = 5, Title = "Action Bar Player Demo", IsEnabled = true },
				new DemoItem(){ Id = 6, Title = "Intents Activity", IsEnabled = true },
				new DemoItem(){ Id = 7, Title = "Player Controls Activity", IsEnabled = true },

			};

			ListAdapter = new YouTubeApiDemoAdapter (this, demoItems);

			// Create your application here
		}

		protected override void OnListItemClick (ListView l, View v, int position, long id)
		{
			var item = demoItems [position];

			switch (item.Id) {
			case 1:
				StartActivity (typeof(PlayerViewActivity));
				break;
			case 2:
				StartActivity (typeof(StandAloneActivity));
				break;
			case 3:
				StartActivity (typeof(FragmentActivity));
				break;
			case 4:
				StartActivity (typeof(FullScreenActivity));
				break;
			case 5:
				StartActivity (typeof(ActionBarActivity));
				break;
			case 6:
				StartActivity (typeof(IntentsActivity));
				break;
			case 7:
				StartActivity (typeof(PlayerControlsActivity));
				break;

			default :
				Finish ();
				break;
			}
		}
	}
}

