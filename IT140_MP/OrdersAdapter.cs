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
        private int sum_price;
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
                TextView txtBook_Price = row.FindViewById<TextView>(Resource.Id.book_price);
                Button cancelButton = row.FindViewById<Button>(Resource.Id.button1);
                Button receiveButton = row.FindViewById<Button>(Resource.Id.button2);

                txtBook_Price.Text = "Price: " + sList[position].Book_price;
                txtOrder_Status.Text = "Status: " + sList[position].Order_status;
                txtBook_Title.Text = "Book Title: " + sList[position].Book_title;
                txtOrder_Date.Text = "Order Date:" + sList[position].Order_date;
               /* sum_price += Convert.ToInt32(sList[position].Book_price);
*/
                cancelButton.Click += (sender, args) =>
                {
       
                    sList[position].Order_status = "Cancelled";


                    sum_price = 0;
                    
                    for (int i = 0; i<sList.Count; i++)
                    {
                        sum_price += Convert.ToInt32(sList[i].Book_price);
                    }
                    Toast.MakeText(Application.Context, "pos: " + sList[position].Order_id + " Length: " + sList.Count +" Sum:" +sum_price, ToastLength.Short).Show();
                    /*sList[position].Order_status = "Cancelled";*/
                    /*sList.Remove(sList[position]);*/

                    this.NotifyDataSetChanged();

                    row = LayoutInflater.From(sContext).Inflate(Resource.Layout.OrdersLayoutMain, null, false);
                    TextView sum_order;
                    sum_order = row.FindViewById<TextView>(Resource.Id.textView2);
                    sum_order.Text = "450";

                };

                receiveButton.Click += (sender, args) =>
                {
                    sList[position].Order_status = "Received";
                    /*sList.Remove(sList[position]);*/
                    /*                    List<Orders> newList = new List<Orders>();
                                        for(int i = 0; i < sList.Count; i++)
                                        {
                                            if (sList[position].Order_id != sList[i].Order_id)
                                                newList.Add(sList[position]);
                                            else
                                                newList.Add(new Orders
                                                {
                                                    Book_title = "999",
                                                    Book_price = "99",
                                                    Order_status = "99",
                                                    Order_id = "99",
                                                    Book_id = "99",
                                                    Order_date = DateTime.Parse("2015-04-03 14:00:45")
                                                });
                                        }
                                        sList = newList;*/
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
    }
}