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
	public class MainActivity : Activity
	{
		int count = 1;



		protected override void OnCreate (Bundle bundle)
		{
			base.OnCreate (bundle);

			// Set our view from the "main" layout resource
			SetContentView (Resource.Layout.Main);

		
		}


	}
}


