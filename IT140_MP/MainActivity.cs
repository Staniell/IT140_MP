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
    [Activity(Label = "@string/app_name", Theme = "@style/AppTheme", MainLauncher = true)]
    public class MainActivity : AppCompatActivity
    {
        EditText username, password, email, homeAdd;
        Button register, login;
        HttpWebResponse response;
        HttpWebRequest request;
        string res, ip;
        protected override void OnCreate(Bundle savedInstanceState)
        {


            base.OnCreate(savedInstanceState);
            Xamarin.Essentials.Platform.Init(this, savedInstanceState);
            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.RegisterLayout);


            username = FindViewById<EditText>(Resource.Id.username_reg);
            email = FindViewById<EditText>(Resource.Id.email_reg);
            homeAdd = FindViewById<EditText>(Resource.Id.address_txt);
            password = FindViewById<EditText>(Resource.Id.password_reg);
            register = FindViewById<Button>(Resource.Id.button1);
            login = FindViewById<Button>(Resource.Id.button2);


            AssetManager assets = this.Assets;
            using StreamReader sr = new StreamReader(assets.Open("ip_address.txt"));
            ip = sr.ReadToEnd();

            register.Click += this.RegisterAccount;
            login.Click += this.LoginAccount;
            


        }

        void LoginAccount(object sender, EventArgs e)
        {
            Intent i = new Intent(this, typeof(LoginActivity));
            StartActivity(i);
        }
        void RegisterAccount(object sender, EventArgs e)
        {
            request = (HttpWebRequest)WebRequest.Create($"http://{ip}/IT140P/REST/registerAcc.php?username=" + username.Text + "&email=" + email.Text + "&home_address="+ homeAdd.Text + "&password=" + password.Text);
            response = (HttpWebResponse)request.GetResponse();
            StreamReader reader = new StreamReader(response.GetResponseStream());
            res = reader.ReadToEnd();


            Toast.MakeText(this, res, ToastLength.Long).Show();


            if (res.Contains("Registered"))
            {
                Intent i = new Intent(this, typeof(HomeActivity));
                i.PutExtra("Name", username.Text);
                i.PutExtra("Email", email.Text);
                StartActivity(i);
            }
        }
        public override void OnRequestPermissionsResult(int requestCode, string[] permissions, [GeneratedEnum] Android.Content.PM.Permission[] grantResults)
        {
            Xamarin.Essentials.Platform.OnRequestPermissionsResult(requestCode, permissions, grantResults);

            base.OnRequestPermissionsResult(requestCode, permissions, grantResults);
        }
    }
}