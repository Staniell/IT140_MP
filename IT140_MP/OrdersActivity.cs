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
    [Activity(Label = "OrdersLayout", MainLauncher = true)]
    public class OrdersActivity : Activity
    {
        private ListView ordersListView;
        private List<Orders> mlist;
        OrdersAdapter adapter;
        HttpWebResponse response;
        HttpWebRequest request;
        string ip, res;
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Create your application here
            SetContentView(Resource.Layout.OrdersLayout);

            AssetManager assets = this.Assets;
            using StreamReader sr = new StreamReader(assets.Open("ip_address.txt"));
            ip = sr.ReadToEnd();

            fillData();

            ordersListView = FindViewById<ListView>(Resource.Id.orderListView);

            
            adapter = new OrdersAdapter(this, mlist);
            ordersListView.Adapter = adapter;
            ordersListView.ItemClick += Order_ItemClick;
        }

        private void Order_ItemClick(object sender, AdapterView.ItemClickEventArgs e)
        {
            var select = mlist[e.Position].Email + mlist[e.Position].Order_status;

            /*Toast.MakeText(this, select, ToastLength.Long).Show();*/
        }

        private void fillData()
        {
            request = (HttpWebRequest)WebRequest.Create($"http://{ip}/IT140P/REST/fetch_orders.php?email=" + "sampleEmail");
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
                    Order_status = root[i].GetProperty("order_status").ToString(),
                    Order_date = DateTime.Parse(root[i].GetProperty("order_date").ToString())
                });
            }

            
        }

    }
}