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

