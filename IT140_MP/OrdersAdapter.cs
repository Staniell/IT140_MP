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

namespace IT140_MP
{
    class OrdersAdapter : BaseAdapter<Orders>
    {
        public List<Orders> sList;
        private Context sContext;
        public OrdersAdapter(Context context, List<Orders> list)
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
                    row = LayoutInflater.From(sContext).Inflate(Resource.Layout.OrdersLayout, null, false);
                }
                TextView txtBook_Title = row.FindViewById<TextView>(Resource.Id.book_title);
                TextView txtOrder_Status = row.FindViewById<TextView>(Resource.Id.order_status);
                TextView txtOrder_Date = row.FindViewById<TextView>(Resource.Id.order_date);

                txtOrder_Status.Text = "Status: " + sList[position].Order_status;
                txtBook_Title.Text = "Book Title: " + sList[position].Book_title;
                txtOrder_Date.Text = "Order Date:" + sList[position].Order_date;

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