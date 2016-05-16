using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Xamarin.Controls;

namespace NewsManager
{
    [BroadcastReceiver(Label = "BroadCast Receiver")]
    [IntentFilter(new string[] {"com.alr.text"})]
    public class MyReciever : BroadcastReceiver
    {
        public override void OnReceive(Context context, Intent intent)
        {
            // read the SendBroadcast data 
            if (intent.Action == "com.alr.text")
            {
                string text = intent.GetStringExtra("MyData") ?? "Data not available";
                AlertCenter.Default.Init(context);
                AlertCenter.Default.PostMessage("Œ»—", text, Resource.Drawable.Icon);
            }
        }
    }
}