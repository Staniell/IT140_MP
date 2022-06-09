using Android.App;
using Android.Content;
using Android.Content.Res;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.Json;

namespace IT140_MP
{
    [Activity(Label = "Book Store Page")]
    public class BookActivity : Activity
    {
        private ListView booksListView;
        private List<Books> mlist;
        BooksAdapter adapter;
        HttpWebResponse response;
        HttpWebRequest request;
        string ip, res;
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.BookPageLayoutMain);

            AssetManager assets = this.Assets;
            using StreamReader sr = new StreamReader(assets.Open("ip_address.txt"));
            ip = sr.ReadToEnd();

            // Create your application here
            booksListView = FindViewById<ListView>(Resource.Id.listViewBooks);

            fillData();

            adapter = new BooksAdapter(this, mlist, Intent.GetStringExtra("Email"));
            booksListView.Adapter = adapter;



        }

        private void fillData()
        {
            request = (HttpWebRequest)WebRequest.Create($"http://{ip}/IT140P/REST/fetch_books.php?");
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