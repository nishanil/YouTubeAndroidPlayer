using System;
using Android.Widget;
using Android.App;
using System.Collections.Generic;
using Android.Views;

namespace YouTubeDemo
{
	public class IntentsAdapter : BaseAdapter<IntentItem>
	{
		#region implemented abstract members of BaseAdapter

		public override long GetItemId (int position)
		{
			return position;
		}

		public override Android.Views.View GetView (int position, Android.Views.View convertView, Android.Views.ViewGroup parent)
		{
			View view = convertView; // re-use an existing view, if one is available
			if (view == null) // otherwise create a new one
				view = context.LayoutInflater.Inflate(Resource.Layout.list_item, null);
			view.FindViewById<TextView>(Resource.Id.list_item_text).Text = items[position].Title;
			return view;
		}

		public override int Count {
			get {
				return items.Count;
			}
		}

		#endregion

		#region implemented abstract members of BaseAdapter

		public override IntentItem this [int index] {
			get {
				return items[index];
			}
		}

		#endregion

		List<IntentItem> items;
		Activity context;

		public IntentsAdapter (Activity context, List<IntentItem> items)
		{
			this.context = context;
			this.items = items;
		}
	}

	public enum IntentType {
		PlayVideo,
		OpenPlaylist,
		PlayPlaylist,
		OperUser,
		OpenSearch,
		UploadVideo
	}

	public class IntentItem {

		public string Title {
			get;
			set;
		}
		public IntentType Type {
			get;
			set;
		}

		public IntentItem ()
		{
			
		}
	}

}

