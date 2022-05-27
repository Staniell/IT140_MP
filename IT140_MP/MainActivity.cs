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

namespace IT140_MP
{
    [Activity(Label = "@string/app_name", Theme = "@style/AppTheme", MainLauncher = true)]
    public class MainActivity : AppCompatActivity
    {
        EditText username, password, email;
        Button register;
        HttpWebResponse response;
        HttpWebRequest request;
        string res, ip;
        protected override void OnCreate(Bundle savedInstanceState)
        {


            base.OnCreate(savedInstanceState);
            Xamarin.Essentials.Platform.Init(this, savedInstanceState);
            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.Register);


            username = FindViewById<EditText>(Resource.Id.username_reg);
            email = FindViewById<EditText>(Resource.Id.email_reg);
            password = FindViewById<EditText>(Resource.Id.password_reg);
            register = FindViewById<Button>(Resource.Id.button1);


            AssetManager assets = this.Assets;
            using StreamReader sr = new StreamReader(assets.Open("ip_address.txt"));
            ip = sr.ReadToEnd();

            register.Click += this.RegisterAccount;
            


        }

        void RegisterAccount(object sender, EventArgs e)
        {
            request = (HttpWebRequest)WebRequest.Create($"http://{ip}/IT140P/REST/registerAcc.php?username=" + username.Text + "&email=" + email.Text + "&password=" + password.Text);
            response = (HttpWebResponse)request.GetResponse();
            StreamReader reader = new StreamReader(response.GetResponseStream());
            res = reader.ReadToEnd();
            Toast.MakeText(this, res, ToastLength.Long).Show();
        }
        public override void OnRequestPermissionsResult(int requestCode, string[] permissions, [GeneratedEnum] Android.Content.PM.Permission[] grantResults)
        {
            Xamarin.Essentials.Platform.OnRequestPermissionsResult(requestCode, permissions, grantResults);

            base.OnRequestPermissionsResult(requestCode, permissions, grantResults);
        }
    }
}