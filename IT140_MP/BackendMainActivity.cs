using Android.App;
using Android.OS;
using Android.Runtime;
using AndroidX.AppCompat.App;
using Android.Widget;
using System.Text.Json;
using System.IO;
using Android.Content.Res;
using System;
using System.Net;
using Android.Content;

namespace IT140_MP
{
    [Activity(Label = "Admin Dashboard")]
    public class BackendMainActivity : Activity
    {
        Button adminStatsBtn, manageOrdersBtn, manageBooksBtn, logoutBtn;
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            Xamarin.Essentials.Platform.Init(this, savedInstanceState);
            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.BackendMain);

            adminStatsBtn = FindViewById<Button>(Resource.Id.statBtn);
            manageOrdersBtn = FindViewById<Button>(Resource.Id.orderBtn);
            manageBooksBtn = FindViewById<Button>(Resource.Id.bookBtn);
            logoutBtn = FindViewById<Button>(Resource.Id.adminoutBtn);

            adminStatsBtn.Click += this.GoToStatsActivity;
            manageOrdersBtn.Click += this.GoToManageOrdersActivity;
            manageBooksBtn.Click += this.GoToManageBooksActivity;
            logoutBtn.Click += this.LogoutAdmin;
        }
        void GoToStatsActivity(object sender, EventArgs e)
        {
            Intent i = new Intent(this, typeof(BackendStats));
            StartActivity(i);
        }
        void GoToManageBooksActivity(object sender, EventArgs e)
        {
            Intent i = new Intent(this, typeof(BackendBooksActivity));
            StartActivity(i);
        }
        void GoToManageOrdersActivity(object sender, EventArgs e)
        {
            Intent i = new Intent(this, typeof(BackendOrdersActivity));
            StartActivity(i);
        }
        void LogoutAdmin(object sender, EventArgs e)
        {
            Intent i = new Intent(this, typeof(BackendLogin));
            StartActivity(i);
        }
    }
}