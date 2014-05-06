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
			default :
				Finish ();
				break;
			}
		}
	}
}

