using System;
using Android.App;
using Android.Content;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;
using Xamarin.Controls;

namespace NewsManager
{
    [Activity(Label = "NewsManager", MainLauncher = true, Icon = "@drawable/icon")]
    public class MainActivity : Activity
    {
        EditText txtNewsDesc;
        Button btnSend;
        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.Main);

            StartService(new Intent(this, typeof(NotifyService)));
            // Get our button from the layout resource,
            // and attach an event to it
            btnSend = FindViewById<Button>(Resource.Id.btnSendNews);
            txtNewsDesc = FindViewById<EditText>(Resource.Id.txtNewsDesc);
            if (txtNewsDesc.Text == string.Empty)
                btnSend.Enabled = false;

            btnSend.Click += BtnSend_Click;
            
            txtNewsDesc.TextChanged += TxtNewsDesc_TextChanged;
        }
        private void TxtNewsDesc_TextChanged(object sender, Android.Text.TextChangedEventArgs e)
        {
            if (txtNewsDesc.Text != string.Empty)
                btnSend.Enabled = true;
        }
        private void BtnSend_Click(object sender, EventArgs e)
        {
            sendNews.myNewsWebService nws = new sendNews.myNewsWebService();
            nws.sendNewsCompleted += Nws_sendNewsCompleted;
            nws.sendNewsAsync(txtNewsDesc.Text);
            txtNewsDesc.Text = string.Empty;
            btnSend.Enabled = false;
        }
        private void Nws_sendNewsCompleted(object sender, sendNews.sendNewsCompletedEventArgs e)
        {      
            Toast.MakeText(this, e.Result.MessegeDet, ToastLength.Short).Show();
        }
    }
}

