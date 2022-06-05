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
    class Books
    {
        private string book_id, book_title, book_price, book_img;

        public string Book_id
        {
            get { return book_id; }
            set { book_id = value; }
        }
        public string Book_title
        {
            get { return book_title; }
            set { book_title = value; }
        }
        public string Book_price
        {
            get { return book_price; }
            set { book_price = value; }
        }
        public string Book_img
        {
            get { return book_img; }
            set { book_img = value; }
        }
    }
}