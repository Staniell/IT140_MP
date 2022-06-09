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
    [Activity(Label = "OrdersLayout", ParentActivity = typeof(BookActivity))]
    public class OrdersActivity : Activity
    {
        private ListView ordersListView;
        private List<Orders> mlist;
        OrdersAdapter adapter;
        TextView sum_order;
        HttpWebResponse response;
        HttpWebRequest request;
        string ip, res;
        int total;
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Create your application here
            SetContentView(Resource.Layout.OrdersLayoutMain);

            AssetManager assets = this.Assets;
            using StreamReader sr = new StreamReader(assets.Open("ip_address.txt"));
            ip = sr.ReadToEnd();


            ordersListView = FindViewById<ListView>(Resource.Id.orderListView);
            sum_order = FindViewById<TextView>(Resource.Id.textView2);

            fillData();


            
            adapter = new OrdersAdapter(this, mlist, Intent.GetStringExtra("Email"));
            ordersListView.Adapter = adapter;
        }


        private void fillData()
        {
            
            request = (HttpWebRequest)WebRequest.Create($"http://{ip}/IT140P/REST/fetch_orders.php?email=" + Intent.GetStringExtra("Email") + "&order_type=" + "pending|shipped");
            response = (HttpWebResponse)request.GetResponse();
            res = response.ProtocolVersion.ToString();
            StreamReader reader = new StreamReader(response.GetResponseStream());
            var result = reader.ReadToEnd();

            using JsonDocument doc = JsonDocument.Parse(result);
            JsonElement root = doc.RootElement;
            mlist = new List<Orders>();

            for (int i = 0; i < root.GetArrayLength(); i++)
            {

                mlist.Add(new Orders
                {
                    Book_title = root[i].GetProperty("book_title").ToString(),
                    Book_price = root[i].GetProperty("book_price").ToString(),
                    Order_status = root[i].GetProperty("order_status").ToString(),
                    Order_id = root[i].GetProperty("order_id").ToString(),
                    Book_id = root[i].GetProperty("book_id").ToString(),
                    Order_date = DateTime.Parse(root[i].GetProperty("order_date").ToString())
                });
                total += Convert.ToInt32(root[i].GetProperty("book_price").ToString());
            }


            sum_order.Text = "Total Pending Order Price:"+total.ToString();
            
        }

    }
}