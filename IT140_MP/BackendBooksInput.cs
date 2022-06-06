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
    [Activity(Label = "Add/Edit Books", NoHistory = true)]
    public class BackendBooksInput : Activity
    {

        string book_id;
        HttpWebResponse response;
        HttpWebRequest request;
        string res, ip;
        protected override void OnCreate(Bundle savedInstanceState)
        {
            
            base.OnCreate(savedInstanceState);
            Xamarin.Essentials.Platform.Init(this, savedInstanceState);
            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.BackendBooksInputLayout);
            EditText bookName = FindViewById<EditText>(Resource.Id.bookNameInput);
            EditText bookPrice = FindViewById<EditText>(Resource.Id.bookPriceInput);
            EditText bookImage = FindViewById<EditText>(Resource.Id.bookImageInput);
            TextView bookPageTitle = FindViewById<TextView>(Resource.Id.bookInputTitle);
            Button applyButton = FindViewById<Button>(Resource.Id.addeditBtn);
            Button cancelButton = FindViewById<Button>(Resource.Id.cancelButton);
            if (Intent.Extras != null)
            {
                book_id = Intent.GetStringExtra("book_id");
                bookPageTitle.Text = "Edit Book Information";
                applyButton.Text = "Apply";
                this.GetInputField(book_id, bookName, bookPrice, bookImage);
                applyButton.Click += (sender, args) => this.UpdateBook(book_id, bookName, bookPrice, bookImage);
            }
            else
            {
                bookPageTitle.Text = "Add New Book";
                applyButton.Text = "Add Book";
                applyButton.Click += (sender, args) => this.AddBook(bookName, bookPrice, bookImage);
            }
            cancelButton.Click += this.GoBack;
        }
        void GoBack(object sender, EventArgs e)
        {
            Intent i = new Intent(this, typeof(BackendBooksActivity));
            StartActivity(i);
            Finish();
        }
        void GetInputField(string book_id, EditText bName, EditText bPrice, EditText bImage)
        {
            AssetManager assets = Application.Context.Assets;
            using StreamReader sr = new StreamReader(assets.Open("ip_address.txt"));
            ip = sr.ReadToEnd();

            request = (HttpWebRequest)WebRequest.Create($"http://{ip}/IT140P/REST/fetch_editbook.php?book_id=" + book_id);
            response = (HttpWebResponse)request.GetResponse();
            res = response.ProtocolVersion.ToString();
            StreamReader reader = new StreamReader(response.GetResponseStream());
            var result = reader.ReadToEnd();
            using JsonDocument doc = JsonDocument.Parse(result);
            JsonElement root = doc.RootElement;
            try
            {
                var u1 = root[0];
                bName.Text = u1.GetProperty("book_title").ToString();
                bPrice.Text = u1.GetProperty("book_price").ToString();
                bImage.Text = u1.GetProperty("book_img").ToString();
            }
            catch (Exception)
            {
                Toast.MakeText(this, "No Records!", ToastLength.Long).Show();
            }
        }
        void UpdateBook(string book_id, EditText bName, EditText bPrice, EditText bImage)
        {
            AssetManager assets = Application.Context.Assets;
            using StreamReader sr = new StreamReader(assets.Open("ip_address.txt"));
            ip = sr.ReadToEnd();

            request = (HttpWebRequest)WebRequest.Create($"http://{ip}/IT140P/REST/update_book.php?book_id=" + book_id +"&book_name=" + bName.Text + "&book_price=" + bPrice.Text + "&book_img=" + bImage.Text);
            response = (HttpWebResponse)request.GetResponse();
            res = response.ProtocolVersion.ToString();
            StreamReader reader = new StreamReader(response.GetResponseStream());
            var result = reader.ReadToEnd();

            Toast.MakeText(Application.Context, result, ToastLength.Short).Show();
            Intent i = new Intent(this, typeof(BackendBooksActivity));
            StartActivity(i);
            Finish();
        }
        void AddBook(EditText bName, EditText bPrice, EditText bImage)
        {
            AssetManager assets = Application.Context.Assets;
            using StreamReader sr = new StreamReader(assets.Open("ip_address.txt"));
            ip = sr.ReadToEnd();

            request = (HttpWebRequest)WebRequest.Create($"http://{ip}/IT140P/REST/add_book.php?book_name=" + bName.Text + "&book_price=" + bPrice.Text + "&book_img=" + bImage.Text);
            response = (HttpWebResponse)request.GetResponse();
            res = response.ProtocolVersion.ToString();
            StreamReader reader = new StreamReader(response.GetResponseStream());
            var result = reader.ReadToEnd();

            Toast.MakeText(Application.Context, result, ToastLength.Short).Show();

            Intent i = new Intent(this, typeof(BackendBooksActivity));
            StartActivity(i);
            Finish();
        }
    }
}