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
    [Activity(Label = "Book Store Page")]
    public class BookPage : Activity
    {
        string login_name;
        TextView displayName;
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.BookPageLayout);

            login_name = Intent.GetStringExtra("Name");
            displayName = FindViewById<TextView>(Resource.Id.textView1);

            displayName.Text = login_name;
            // Create your application here
        }
    }
}