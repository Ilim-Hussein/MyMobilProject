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
using Xamarin.Forms;

namespace MyMobilProject.Droid
{
    [Service]
    public class DataSource : Service
    {
        public override IBinder OnBind(Intent intent)
        {
            return null;
        }
        public const int ServiceRunningNotifID = 9000;



        public override StartCommandResult OnStartCommand(Intent intent, StartCommandFlags flags, int startId)
        {
            Notification notif = DependencyService.Get<INotification>().ReturnNotif();
            StartForeground(ServiceRunningNotifID, notif);

            _ = DoLongRunningOperationThings();

            return StartCommandResult.Sticky;
        }
        private object DoLongRunningOperationThings()
        {
            return null;
        } 


         
        public override void OnDestroy()
        {
            base.OnDestroy();
        }

        public override bool StopService(Intent name)
        {
            return base.StopService(name);
        }

    }
}