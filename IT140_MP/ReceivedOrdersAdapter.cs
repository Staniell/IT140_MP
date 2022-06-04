using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using System;
using System.Collections.Generic;

namespace IT140_MP
{
    class ReceivedOrdersAdapter : BaseAdapter<Orders>
    {
        public List<Orders> cList;
        private Context sContext;
        public ReceivedOrdersAdapter(Context context, List<Orders> list)
        {
            cList = list;
            sContext = context;
        }
        public override Orders this[int position]
        {
            get
            {
                return cList[position];
            }
        }
        public override int Count
        {
            get
            {
                return cList.Count;
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
                    row = LayoutInflater.From(sContext).Inflate(Resource.Layout.ReceivedOrdersLayout, null, false);
                }
                TextView txtBook_Title = row.FindViewById<TextView>(Resource.Id.book_titlec);
                TextView txtOrder_Date = row.FindViewById<TextView>(Resource.Id.order_datec);
                TextView txtBook_Price = row.FindViewById<TextView>(Resource.Id.book_pricec);
                TextView txtOrder_Id = row.FindViewById<TextView>(Resource.Id.order_idc);

                txtBook_Price.Text = "Price: " + cList[position].Book_price;
                txtBook_Title.Text = "Book Title: " + cList[position].Book_title;
                txtOrder_Date.Text = "Order Date:" + cList[position].Order_date;
                txtOrder_Id.Text = "Order ID:" + cList[position].Order_id;


            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex.Message);
            }
            finally { }
            return row;
        }


    }
}