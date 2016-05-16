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
using System.Threading.Tasks;
using System.Threading;
using Xamarin.Controls;
using Android.Preferences;

namespace NewsManager
{
    [Service]
    class NotifyService : IntentService
    {
        protected override void OnHandleIntent(Intent intent)
        {

        }
        public override StartCommandResult OnStartCommand(Intent intent, StartCommandFlags flags, int startId)
        {
            newsNotify.NewsNotify newsN = new newsNotify.NewsNotify();
            newsN.LoginNotifyCompleted += NewsN_LoginNotifyCompleted;
            ISharedPreferences pref = PreferenceManager.GetDefaultSharedPreferences(this);
            // start a task here
            new Task(() =>
            {
                 // long running code
                 while (true)
                {
                    newsN.LoginNotifyAsync(pref.GetInt("newsID",0));
                    Thread.Sleep(10000);
                }
            }).Start();
            return StartCommandResult.Sticky;
        }

        private void NewsN_LoginNotifyCompleted(object sender, newsNotify.LoginNotifyCompletedEventArgs e)
        {
            if(e.Result.NewsID !=0)
            {
                Intent intent = new Intent();
                intent.SetAction("com.alr.text");
                intent.PutExtra("MyData", e.Result.NewsDesc);
                SendBroadcast(intent);
                saveID(e.Result.NewsID);
            }
        }
        private void saveID(int nID)
        {
            ISharedPreferences pref = PreferenceManager.GetDefaultSharedPreferences(this);
            ISharedPreferencesEditor edit = pref.Edit();
            edit.PutInt("newsID", nID);
            edit.Apply();
        }
    }
}