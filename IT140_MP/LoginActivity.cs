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
    [Activity(Label = "Login Layout")]
    public class LoginActivity : Activity
    {
        string ip, res;
        Button login, has_acc;
        EditText email, password;
        HttpWebResponse response;
        HttpWebRequest request;
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Create your application here
            SetContentView(Resource.Layout.LoginLayout);

            login = FindViewById<Button>(Resource.Id.button1);
            has_acc = FindViewById<Button>(Resource.Id.button2);

            email = FindViewById<EditText>(Resource.Id.editText1);
            password = FindViewById<EditText>(Resource.Id.editText2);

            login.Click += this.LoginAccount;
            has_acc.Click += this.GoToRegister;

            AssetManager assets = this.Assets;
            using StreamReader sr = new StreamReader(assets.Open("ip_address.txt"));
            ip = sr.ReadToEnd();
        }

        void LoginAccount(object sender, EventArgs e)
        {
            request = (HttpWebRequest)WebRequest.Create($"http://{ip}/IT140P/REST/user_login.php?email=" + email.Text + "&password=" + password.Text);
            response = (HttpWebResponse)request.GetResponse();
            res = response.ProtocolVersion.ToString();
            StreamReader reader = new StreamReader(response.GetResponseStream());
            var result = reader.ReadToEnd();

            if (result.Contains("Invalid"))
                Toast.MakeText(this, "Invalid email or password.", ToastLength.Long).Show();

            else
            {

                using JsonDocument doc = JsonDocument.Parse(result);
                JsonElement root = doc.RootElement;

                var u1 = root[0];
                string searchedname = u1.GetProperty("username").ToString();

                Intent i = new Intent(this, typeof(HomeActivity));
                i.PutExtra("Name", searchedname);
                i.PutExtra("Email", email.Text);
                StartActivity(i);
            }

        }

        void GoToRegister(object sender, EventArgs e)
        {
            Intent i = new Intent(this, typeof(MainActivity));
            StartActivity(i);
        }
    }
}