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
    [Activity(Label = "Backend Orders")]
    public class BackendOrdersActivity : Activity
    {
        private ListView backendordersListView;
        private List<Orders> mlist;
        BackendOrdersAdapter adapter;
        HttpWebResponse response;
        HttpWebRequest request;
        string ip, res;
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Create your application here
            SetContentView(Resource.Layout.BackendOrdersLayoutMain);

            AssetManager assets = this.Assets;
            using StreamReader sr = new StreamReader(assets.Open("ip_address.txt"));
            ip = sr.ReadToEnd();

            backendordersListView = FindViewById<ListView>(Resource.Id.backendordersListView);
            fillData();


            adapter = new BackendOrdersAdapter(this, mlist);
            backendordersListView.Adapter = adapter;
        }


        private void fillData()
        {
            request = (HttpWebRequest)WebRequest.Create($"http://{ip}/IT140P/REST/fetch_orders_backend.php");
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
                    Email = root[i].GetProperty("email").ToString(),
                    Book_title = root[i].GetProperty("book_title").ToString(),
                    Book_price = root[i].GetProperty("book_price").ToString(),
                    Order_status = root[i].GetProperty("order_status").ToString(),
                    Order_id = root[i].GetProperty("order_id").ToString(),
                    Book_id = root[i].GetProperty("book_id").ToString(),
                    Order_date = DateTime.Parse(root[i].GetProperty("order_date").ToString()),
                    Address = root[i].GetProperty("home_address").ToString()
                });
            }

        }

    }
}