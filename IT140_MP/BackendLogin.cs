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
using System.Net;
using Android.Content.Res;
using System.Text.Json;
using System.IO;

namespace IT140_MP
{
    [Activity(Label = "Admin Login")]
    public class BackendLogin : Activity
    {
        string ip, res;
        Button login;
        EditText adminuser, adminpass;
        HttpWebResponse response;
        HttpWebRequest request;
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Create your application here
            SetContentView(Resource.Layout.BackendLoginLayout);

            login = FindViewById<Button>(Resource.Id.button1);

            adminuser = FindViewById<EditText>(Resource.Id.editText1);
            adminpass = FindViewById<EditText>(Resource.Id.editText2);

            login.Click += this.LoginAccount;

            AssetManager assets = this.Assets;
            using StreamReader sr = new StreamReader(assets.Open("ip_address.txt"));
            ip = sr.ReadToEnd();
        }

        void LoginAccount(object sender, EventArgs e)
        {
            request = (HttpWebRequest)WebRequest.Create($"http://{ip}/IT140P/REST/admin_login.php?adminusername=" + adminuser.Text + "&adminpassword=" + adminpass.Text);
            response = (HttpWebResponse)request.GetResponse();
            res = response.ProtocolVersion.ToString();
            StreamReader reader = new StreamReader(response.GetResponseStream());
            var result = reader.ReadToEnd();

            if (result.Contains("Invalid"))
                Toast.MakeText(this, "Invalid username or password.", ToastLength.Long).Show();

            else
            {
                Intent i = new Intent(this, typeof(BackendMainActivity));
                Toast.MakeText(this, "Login Successful", ToastLength.Long).Show();
                StartActivity(i);
            }

        }
    }
}