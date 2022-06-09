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
    [Activity(Label = "CompletedOrdersActivity")]
    public class ReceivedOrdersActivity : Activity
    {
        private ListView receivedOrdersListView;
        private List<Orders> clist;
        ReceivedOrdersAdapter adapter;
        TextView completed_sum;
        HttpWebResponse response;
        string ip, res;
        HttpWebRequest request;
        int total_received;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            SetContentView(Resource.Layout.ReceivedOrdersLayoutMain);

            AssetManager assets = this.Assets;
            using StreamReader sr = new StreamReader(assets.Open("ip_address.txt"));
            ip = sr.ReadToEnd();


            receivedOrdersListView = FindViewById<ListView>(Resource.Id.listView1);
            completed_sum = FindViewById<TextView>(Resource.Id.textView2);

            getOrders();

            adapter = new ReceivedOrdersAdapter(this, clist);
            receivedOrdersListView.Adapter = adapter;

        }

        private void getOrders()
        {
            
            request = (HttpWebRequest)WebRequest.Create($"http://{ip}/IT140P/REST/fetch_orders.php?email=" + Intent.GetStringExtra("Email") + "&order_type=" + "Received");
            response = (HttpWebResponse)request.GetResponse();
            res = response.ProtocolVersion.ToString();
            StreamReader reader = new StreamReader(response.GetResponseStream());
            var result = reader.ReadToEnd();

            using JsonDocument doc = JsonDocument.Parse(result);
            JsonElement root = doc.RootElement;
            clist = new List<Orders>();

            for (int i = 0; i < root.GetArrayLength(); i++)
            {

                clist.Add(new Orders
                {
                    Book_title = root[i].GetProperty("book_title").ToString(),
                    Book_price = root[i].GetProperty("book_price").ToString(),
                    Order_id = root[i].GetProperty("order_id").ToString(),
                    Order_date = DateTime.Parse(root[i].GetProperty("order_date").ToString())
                });
                total_received += Convert.ToInt32(root[i].GetProperty("book_price").ToString());
            }


            completed_sum.Text = "Total Received Order Price:" + total_received.ToString();

        }
    
    }
}