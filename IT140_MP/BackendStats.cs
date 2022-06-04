using Android.App;
using Android.Content;
using Android.Content.Res;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using System;
using System.IO;
using System.Net;
using System.Text.Json;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IT140_MP
{
    [Activity(Label = "Admin Statistics")]
    public class BackendStats : Activity
    {
        TextView bookStats, orderStats;
        HttpWebResponse response;
        HttpWebRequest request;
        string ip, res, bookCount, orderCount;
        Button manageOrdersBtn, manageBooksBtn;
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            Xamarin.Essentials.Platform.Init(this, savedInstanceState);
            SetContentView(Resource.Layout.BackendStatsLayout);

            bookStats = FindViewById<TextView>(Resource.Id.bookstats);
            orderStats = FindViewById<TextView>(Resource.Id.orderstats);
            manageOrdersBtn = FindViewById<Button>(Resource.Id.orderBtn);
            manageBooksBtn = FindViewById<Button>(Resource.Id.bookBtn);

            AssetManager assets = this.Assets;
            using StreamReader sr = new StreamReader(assets.Open("ip_address.txt"));
            ip = sr.ReadToEnd();

            request = (HttpWebRequest)WebRequest.Create($"http://{ip}/IT140P/REST/fetch_stats.php");
            response = (HttpWebResponse)request.GetResponse();
            res = response.ProtocolVersion.ToString();
            StreamReader reader = new StreamReader(response.GetResponseStream());
            var result = reader.ReadToEnd();
            //parse result to Json then kunin ang root element
            using JsonDocument doc = JsonDocument.Parse(result);
            JsonElement root = doc.RootElement;
            try
            {
                var u1 = root[0];

                bookCount = u1.GetProperty("BookCount").ToString();
                orderCount = u1.GetProperty("OrderCount").ToString();
                bookStats.Text = "Total Books: " + bookCount;
                orderStats.Text = "Total Orders: " + orderCount;

            }
            catch (Exception)
            {
                Toast.MakeText(this, "No Records!", ToastLength.Long).Show();
            }
            manageOrdersBtn.Click += this.GoToManageOrdersActivity;
            manageBooksBtn.Click += this.GoToManageBooksActivity;
        }
        void GoToManageBooksActivity(object sender, EventArgs e)
        {
            Intent i = new Intent(this, typeof(BackendMainActivity));
            StartActivity(i);
        }
        void GoToManageOrdersActivity(object sender, EventArgs e)
        {
            Intent i = new Intent(this, typeof(BackendMainActivity));
            StartActivity(i);
        }
    }
}