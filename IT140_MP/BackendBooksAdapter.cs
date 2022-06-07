using Android.App;
using Android.Content;
using Android.Views;
using Android.Widget;
using System;
using System.Collections.Generic;
using Android.Content.Res;
using System.Net;
using System.IO;

namespace IT140_MP
{
    class BackendBooksAdapter : BaseAdapter<Books>
    {
        public List<Books> sList;
        HttpWebResponse response;
        HttpWebRequest request;
        private Context sContext;
        string res, ip;
        public BackendBooksAdapter(Context context, List<Books> list)
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
                    row = LayoutInflater.From(sContext).Inflate(Resource.Layout.BackendBooksLayout, null, false);
                }
                TextView txtBook_Title = row.FindViewById<TextView>(Resource.Id.bookNameList);
                TextView txtBook_Price = row.FindViewById<TextView>(Resource.Id.bookPriceList);
                ImageView imgBook = row.FindViewById<ImageView>(Resource.Id.bookImageList);
                Button editButton = row.FindViewById<Button>(Resource.Id.bookEditBtn);
                Button deleteButton = row.FindViewById<Button>(Resource.Id.bookDeleteBtn);
                string resource = (sList[position].Book_img).ToString();
                txtBook_Title.Text = sList[position].Book_title;
                txtBook_Price.Text = "₱ " + sList[position].Book_price;
                if (resource == "empty")
                {
                    imgBook.SetImageResource(Resource.Drawable.NoImage);
                }
                else
                {
                    int resourceId = (int)typeof(Resource.Drawable).GetField(resource).GetValue(null);
                    imgBook.SetImageResource(resourceId);
                }
                if (!editButton.HasOnClickListeners && !deleteButton.HasOnClickListeners)
                {
                    editButton.Click += (sender, args) => this.updateBook(sList[position].Book_id);
                    deleteButton.Click += (sender, args) =>
                    {
                        deleteBook(position);
                        this.NotifyDataSetChanged();
                    };
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex.Message);
            }
            finally { }
            return row;
        }

        void updateBook(string book_id)
        {
            Intent i = new Intent(sContext, typeof(BackendBooksInput));
            i.PutExtra("book_id", book_id);
            sContext.StartActivity(i);
            ((Activity)sContext).Finish();
        }
        void deleteBook(int book_id)
        {
            AssetManager assets = Application.Context.Assets;
            using StreamReader sr = new StreamReader(assets.Open("ip_address.txt"));
            ip = sr.ReadToEnd();

            request = (HttpWebRequest)WebRequest.Create($"http://{ip}/IT140P/REST/delete_book.php?book_id=" + book_id);
            response = (HttpWebResponse)request.GetResponse();
            res = response.ProtocolVersion.ToString();
            StreamReader reader = new StreamReader(response.GetResponseStream());
            var result = reader.ReadToEnd();

            Toast.MakeText(Application.Context, result, ToastLength.Short).Show();
            Intent i = new Intent(sContext, typeof(BackendBooksActivity));
            sContext.StartActivity(i);
            ((Activity)sContext).Finish();
        }
    }
}