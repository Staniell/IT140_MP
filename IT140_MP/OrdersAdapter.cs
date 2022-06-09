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
    class OrdersAdapter : BaseAdapter<Orders>
    {
        public List<Orders> sList;
        HttpWebResponse response;
        HttpWebRequest request;
        string user_email;
        private Context sContext;
        string res, ip;
        public OrdersAdapter(Context context, List<Orders> list, string email)
        {
            sList = list;
            sContext = context;
            user_email = email;
        }
        public override Orders this[int position]
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
                    row = LayoutInflater.From(sContext).Inflate(Resource.Layout.OrdersLayout, null, false);
                }
                TextView txtBook_Title = row.FindViewById<TextView>(Resource.Id.book_title);
                TextView txtOrder_Status = row.FindViewById<TextView>(Resource.Id.order_status);
                TextView txtOrder_Date = row.FindViewById<TextView>(Resource.Id.order_date);
                TextView txtBook_Price = row.FindViewById<TextView>(Resource.Id.book_price);
                TextView txtOrder_Id = row.FindViewById<TextView>(Resource.Id.order_id);
                Button cancelButton = row.FindViewById<Button>(Resource.Id.button1);
                Button receiveButton = row.FindViewById<Button>(Resource.Id.button2);

                txtBook_Price.Text = "Price: ₱" + sList[position].Book_price;
                txtOrder_Status.Text = "Status: " + sList[position].Order_status;
                txtBook_Title.Text = "Book Title: " + sList[position].Book_title;
                txtOrder_Date.Text = "Order Date:" + sList[position].Order_date;
                txtOrder_Id.Text = "Order ID:" + sList[position].Order_id;


                if (!cancelButton.HasOnClickListeners && !receiveButton.HasOnClickListeners)
                {
                    if (sList[position].Order_status == "Shipped")
                    {
                        cancelButton.Enabled = false;
                        cancelButton.Visibility = ViewStates.Gone;
                    }

                    if (sList[position].Order_status == "Pending")
                    {
                        receiveButton.Enabled = false;
                        receiveButton.Visibility = ViewStates.Gone;
                    }

                    cancelButton.Click += (sender, args) =>
                    {
                    /*sList[position].Order_status = "Cancelled";*/
                        updateOrder("Cancelled", sList[position].Order_id);
                        cancelButton.Visibility = ViewStates.Gone;
                        receiveButton.Visibility = ViewStates.Gone;
                        this.NotifyDataSetChanged();
                    };

                    receiveButton.Click += (sender, args) =>
                    {
                    /*sList[position].Order_status = "Received";*/
                        updateOrder("Received", sList[position].Order_id);
                        cancelButton.Visibility = ViewStates.Gone;
                        receiveButton.Visibility = ViewStates.Gone;
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

        void updateOrder(string status, string order_id)
        {
            AssetManager assets = Application.Context.Assets;
            using StreamReader sr = new StreamReader(assets.Open("ip_address.txt"));
            ip = sr.ReadToEnd();

            request = (HttpWebRequest)WebRequest.Create($"http://{ip}/IT140P/REST/update_order.php?email=" + user_email + "&status_order=" + status + "&order_id=" + order_id);
            response = (HttpWebResponse)request.GetResponse();
            res = response.ProtocolVersion.ToString();
            StreamReader reader = new StreamReader(response.GetResponseStream());
            var result = reader.ReadToEnd();

            Toast.MakeText(Application.Context, result, ToastLength.Short).Show();

            Intent i = new Intent(sContext, typeof(OrdersActivity));
            sContext.StartActivity(i);
            ((Activity)sContext).Finish();
        }
        
    }
}