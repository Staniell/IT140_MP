﻿using Android.App;
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
    class BackendOrdersAdapter : BaseAdapter<Orders>
    {
        public List<Orders> sList;
        HttpWebResponse response;
        HttpWebRequest request;
        private Context sContext;
        string res, ip;
        public BackendOrdersAdapter(Context context, List<Orders> list)
        {
            sList = list;
            sContext = context;
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
                    row = LayoutInflater.From(sContext).Inflate(Resource.Layout.BackendOrdersLayout, null, false);
                }
                TextView txtEmail = row.FindViewById<TextView>(Resource.Id.orderEmailList);
                TextView txtBook_Title = row.FindViewById<TextView>(Resource.Id.orderBookNameList);
                TextView txtOrder_Status = row.FindViewById<TextView>(Resource.Id.orderStatusList);
                TextView txtOrder_Date = row.FindViewById<TextView>(Resource.Id.orderDateList);
                TextView txtBook_Price = row.FindViewById<TextView>(Resource.Id.orderBookPriceList);
                Button cancelButton = row.FindViewById<Button>(Resource.Id.shipCancelBtn);
                Button shipButton = row.FindViewById<Button>(Resource.Id.shipOrderBtn);
                
                txtEmail.Text = sList[position].Email;
                txtBook_Price.Text = "₱" + sList[position].Book_price;
                txtOrder_Status.Text = "Status: " + sList[position].Order_status;
                txtBook_Title.Text = "Book Title: " + sList[position].Book_title;
                txtOrder_Date.Text = "" + sList[position].Order_date;
                if (sList[position].Order_status == "Shipped" || sList[position].Order_status == "Cancelled" || sList[position].Order_status == "Received")
                {
                    shipButton.Enabled = false;
                    shipButton.Visibility = ViewStates.Gone;
                    cancelButton.Enabled = false;
                    cancelButton.Visibility = ViewStates.Gone;
/*                    cancelButton.Background.SetTint(Android.Graphics.Color.DarkGray);
                    shipButton.Background.SetTint(Android.Graphics.Color.DarkGray);*/
                }
                cancelButton.Click += (sender, args) =>
                {
                    sList[position].Order_status = "Cancelled";
                    updateOrder(sList[position].Email, "Cancelled", sList[position].Order_id);
                    shipButton.Enabled = false;
                    cancelButton.Enabled = false;
                    shipButton.Visibility = ViewStates.Gone;
                    cancelButton.Visibility = ViewStates.Gone;
                    this.NotifyDataSetChanged();

                };

                shipButton.Click += (sender, args) =>
                {
                    sList[position].Order_status = "Shipped";
                    updateOrder(sList[position].Email, "Shipped", sList[position].Order_id);
                    shipButton.Clickable = false;
                    cancelButton.Clickable = false;
                    shipButton.Visibility = ViewStates.Gone;
                    cancelButton.Visibility = ViewStates.Gone;
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

        void updateOrder(string userEmail, string status, string order_id)
        {
            AssetManager assets = Application.Context.Assets;
            using StreamReader sr = new StreamReader(assets.Open("ip_address.txt"));
            ip = sr.ReadToEnd();

            request = (HttpWebRequest)WebRequest.Create($"http://{ip}/IT140P/REST/update_order.php?email=" + userEmail + "&status_order=" + status + "&order_id=" + order_id);
            response = (HttpWebResponse)request.GetResponse();
            res = response.ProtocolVersion.ToString();
            StreamReader reader = new StreamReader(response.GetResponseStream());
            var result = reader.ReadToEnd();

            Toast.MakeText(Application.Context, result, ToastLength.Short).Show();
        }

    }
}