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
    [Activity(Label = "HomeActivity")]
    public class HomeActivity : Activity
    {
        TextView emailTxt;
        Button logoutBtn, pendingBtn, doneBtn, orderBtn; 
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            SetContentView(Resource.Layout.HomeLayout);

            pendingBtn = FindViewById<Button>(Resource.Id.pending_btn);
            logoutBtn = FindViewById<Button>(Resource.Id.log_btn);
            doneBtn = FindViewById<Button>(Resource.Id.done_btn);
            orderBtn = FindViewById<Button>(Resource.Id.order_btn);

            emailTxt = FindViewById<TextView>(Resource.Id.email_txt);

            emailTxt.Text = Intent.GetStringExtra("Email");

            pendingBtn.Click += (sender, args) =>
            {
                Intent i = new Intent(this, typeof(OrdersActivity));
                i.PutExtra("Email", Intent.GetStringExtra("Email"));
                StartActivity(i);
            };

            logoutBtn.Click += (sender, args) =>
            {
                Intent i = new Intent(this, typeof(LoginActivity));
                StartActivity(i);
            };

            
            doneBtn.Click += (sender, args) =>
            {
                Intent i = new Intent(this, typeof(ReceivedOrdersActivity));
                i.PutExtra("Email", Intent.GetStringExtra("Email"));
                StartActivity(i);
            };

            orderBtn.Click += (sender, args) =>
            {
                Intent i = new Intent(this, typeof(BookActivity));
                i.PutExtra("Email", Intent.GetStringExtra("Email"));
                StartActivity(i);
            };


        }
    }
}