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
using System.Text.Json;
using Android.Content.Res;
using System.Net;
using System.IO;

namespace IT140_MP
{
    [Activity(Label = "Backend Books")]
    public class BackendBooksActivity : Activity
    {
        private ListView bookListView;
        private List<Books> mlist;
        BackendBooksAdapter adapter;
        HttpWebResponse response;
        HttpWebRequest request;
        Button addbookBtn;
        string ip, res;
        protected override void OnCreate(Bundle savedInstanceState)
        {
            if (Intent.Extras != null)
            {
                adapter.NotifyDataSetChanged();
            }
            base.OnCreate(savedInstanceState);

            // Create your application here
            SetContentView(Resource.Layout.BackendBooksLayoutMain);

            addbookBtn = FindViewById<Button>(Resource.Id.addNewBookBtn);
            AssetManager assets = this.Assets;
            using StreamReader sr = new StreamReader(assets.Open("ip_address.txt"));
            ip = sr.ReadToEnd();

            bookListView = FindViewById<ListView>(Resource.Id.bookListView);
            fillData();


            adapter = new BackendBooksAdapter(this, mlist);
            bookListView.Adapter = adapter;
            addbookBtn.Click += this.AddNewBook;
        }

        void AddNewBook(object sender, EventArgs e)
        {
            Intent i = new Intent(this, typeof(BackendBooksInput));
            StartActivity(i);
            Finish();
        }
        private void fillData()
        {
            request = (HttpWebRequest)WebRequest.Create($"http://{ip}/IT140P/REST/fetch_books_backend.php");
            response = (HttpWebResponse)request.GetResponse();
            res = response.ProtocolVersion.ToString();
            StreamReader reader = new StreamReader(response.GetResponseStream());
            var result = reader.ReadToEnd();

            using JsonDocument doc = JsonDocument.Parse(result);
            JsonElement root = doc.RootElement;
            mlist = new List<Books>();

            for (int i = 0; i < root.GetArrayLength(); i++)
            {

                mlist.Add(new Books
                {
                    Book_id = root[i].GetProperty("book_id").ToString(),
                    Book_title = root[i].GetProperty("book_title").ToString(),
                    Book_price = root[i].GetProperty("book_price").ToString(),
                    Book_img = root[i].GetProperty("book_img").ToString(),
                });
            }

        }

    }
}