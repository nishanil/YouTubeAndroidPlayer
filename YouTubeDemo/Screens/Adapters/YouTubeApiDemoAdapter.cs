using System;
using Android.Widget;
using Android.App;
using Android.Views;
using System.Collections.Generic;

namespace YouTubeDemo
{
	public class YouTubeApiDemoAdapter : BaseAdapter<DemoItem>
	{
		List<DemoItem> items;
		Activity context;

		public YouTubeApiDemoAdapter (Activity context, List<DemoItem> items)
		{
			this.context = context;
			this.items = items;
		}

		#region implemented abstract members of BaseAdapter
		public override long GetItemId (int position)
		{
			return position;
		}
		public override int Count {
			get {
				return this.items.Count;
			}
		}
		public override DemoItem this [int index] {
			get {
				return items [index];
			}
		}


	

		public override Android.Views.View GetView (int position, Android.Views.View convertView, Android.Views.ViewGroup parent)
		{
			View view = convertView;
			if (view == null)
				view = context.LayoutInflater.Inflate (Resource.Layout.list_item, null);
			TextView textView =  view.FindViewById<TextView>(Resource.Id.list_item_text);
			textView.Text = items[position].Title;
			TextView disabledText = view.FindViewById<TextView>(Resource.Id.list_item_disabled_text);
			disabledText.Text = items[position].DisabledText;

			if (items[position].IsEnabled) {
				disabledText.Visibility = ViewStates.Invisible;
				textView.SetTextColor(Android.Graphics.Color.White);
			} else {
				disabledText.Visibility = ViewStates.Visible;
				textView.SetTextColor(Android.Graphics.Color.Gray);
			}

			return view;
		}

		#endregion

	}

	public class DemoItem {

		public int Id {
			get;
			set;
		}

		public string Title {
			get;
			set;
		}
		public bool IsEnabled {
			get;
			set;
		}
		public string DisabledText {
			get;
			set;
		}
	}
}

