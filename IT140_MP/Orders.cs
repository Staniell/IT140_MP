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
        private string email;
        private string book_title;
        private string order_status;
        private DateTime order_date;
        public string Email
        {
            get { return email;}
            set { email = value; }
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