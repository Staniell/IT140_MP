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
    class API_Commands : Activity
    {
        HttpWebResponse response;
        HttpWebRequest request;
        string ip, res;
        public JsonElement FetchOrders(string email)
        {
            GetIP();
            request = (HttpWebRequest)WebRequest.Create($"http://{ip}/IT140P/REST/fetch_orders.php?email=" + "sampleEmail");
            response = (HttpWebResponse)request.GetResponse();
            res = response.ProtocolVersion.ToString();
            StreamReader reader = new StreamReader(response.GetResponseStream());
            var result = reader.ReadToEnd();

            using JsonDocument doc = JsonDocument.Parse(result);
            JsonElement root = doc.RootElement;
            return root;
        }

        private void GetIP()
        {
            AssetManager assets = this.Assets;
            using StreamReader sr = new StreamReader(assets.Open("ip_address.txt"));
            ip = sr.ReadToEnd();
        }
    }
}