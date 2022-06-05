using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using Android.Content.Res;
using System.Net;
using System.IO;

namespace IT140_MP
{
    class BooksAdapter :BaseAdapter<Books>
    {
        public List<Books> sList;
        private Context sContext;
        public BooksAdapter(Context context, List<Books> list)
        {
            sList = list;
            sContext = context;
        }
        public override Books this[int position]
        {
            get
            {
                return sList[position];
            }
        }
        public override int Count
        {
            get
            {
                return sList.Count;
            }
        }
        public override long GetItemId(int position)
        {
            return position;
        }
        public override View GetView(int position, View convertView, ViewGroup parent)
        {
            View row = convertView;
            try
            {
                if (row == null)
                {
                    row = LayoutInflater.From(sContext).Inflate(Resource.Layout.BookPageLayout, null, false);
                }
                TextView txtBook_Title = row.FindViewById<TextView>(Resource.Id.book_title);
                TextView txtBook_Price = row.FindViewById<TextView>(Resource.Id.book_price);
                ImageView imgBook = row.FindViewById<ImageView>(Resource.Id.imageViewBooks);
                string resource = (sList[position].Book_img).ToString();
                if (resource == "empty")
                {
                    imgBook.SetImageResource(Resource.Drawable.NoImage);
                }
                else
                {
                    int resourceId = (int)typeof(Resource.Drawable).GetField(resource).GetValue(null);
                    imgBook.SetImageResource(resourceId);
                }
                txtBook_Title.Text = sList[position].Book_title;
                txtBook_Price.Text = "₱"+ sList[position].Book_price;
                
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex.Message);
            }
            finally { }
            return row;
        }
    }
}