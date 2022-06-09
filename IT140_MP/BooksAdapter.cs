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
        HttpWebResponse response;
        HttpWebRequest request;
        string user_email;
        private Context sContext;
        string res, ip;
        public BooksAdapter(Context context, List<Books> list, string email)
        {
            sList = list;
            sContext = context;
            user_email = email;
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
                Button orderBook = row.FindViewById<Button>(Resource.Id.buttonOrderBook);
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

                orderBook.Click += (sender, args) =>
                {
                    /*sList[position].Order_status = "Cancelled";*/
                    AddOrder(sList[position].Book_id);
                    orderBook.Visibility = ViewStates.Invisible;
                    this.NotifyDataSetChanged();
                };

            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex.Message);
            }
            finally { }
            return row;
        }

        void AddOrder(string bId)
        {
            AssetManager assets = Application.Context.Assets;
            using StreamReader sr = new StreamReader(assets.Open("ip_address.txt"));
            ip = sr.ReadToEnd();
            DateTime date1 = DateTime.Now;
            string date = date1.ToString("yyyy-MM-dd H:mm:ss");

            request = (HttpWebRequest)WebRequest.Create($"http://{ip}/IT140P/REST/add_order.php?book_id=" + bId + "&email=" + user_email + "&order_status=" + "Pending" + "&order_date=" + date);
            response = (HttpWebResponse)request.GetResponse();
            res = response.ProtocolVersion.ToString();
            StreamReader reader = new StreamReader(response.GetResponseStream());
            var result = reader.ReadToEnd();

            Toast.MakeText(Application.Context, "Added Order", ToastLength.Short).Show();
            Intent i = new Intent(sContext, typeof(OrdersActivity));
            i.PutExtra("Email", user_email);
            sContext.StartActivity(i);
            ((Activity)sContext).Finish();
        }

    }
}