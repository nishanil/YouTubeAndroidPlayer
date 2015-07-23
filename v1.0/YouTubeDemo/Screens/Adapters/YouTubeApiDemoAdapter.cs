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

