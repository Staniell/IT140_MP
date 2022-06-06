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
    class Orders
    {
        private string email, book_title, order_status, book_price, book_id, order_id, address;
        private DateTime order_date;

        public string Order_id
        {
            get { return order_id; }
            set { order_id = value; }
        }

        public string Book_id
        {
            get { return book_id; }
            set { book_id = value; }
        }

        public string Address
        {
            get { return address; }
            set { address = value; }
        }
        public string Email
        {
            get { return email;}
            set { email = value; }
        }
        public string Book_price
        {
            get { return book_price; }
            set { book_price = value; }
        }
        public string Book_title
        {
            get { return book_title; }
            set { book_title = value; }
        }

        public string Order_status
        {
            get { return order_status; }
            set { order_status = value; }
        }

        public DateTime Order_date
        {
            get { return order_date; }
            set { order_date = value; }
        }



    }
}