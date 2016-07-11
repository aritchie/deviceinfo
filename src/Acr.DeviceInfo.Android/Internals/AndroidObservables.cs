using System;
using System.Reactive.Linq;
using Android.App;
using Android.Content;


namespace Acr.DeviceInfo.Internals
{
    public static class AndroidObservables
    {

        public static IObservable<Intent> WhenIntentReceived(params string[] intentActions)
        {
            return Observable.Create<Intent>(ob =>
            {
                var filter = new IntentFilter();
                foreach (var action in intentActions)
                    filter.AddAction(action);

                var receiver = new ObservableBroadcastReceiver
                {
                    OnEvent = ob.OnNext
                };
                Application.Context.RegisterReceiver(receiver, filter);
                return () => Application.Context.UnregisterReceiver(receiver);
            });
        }
    }


    public class ObservableBroadcastReceiver : BroadcastReceiver
    {
        public Action<Intent> OnEvent { get; set; }

        public override void OnReceive(Context context, Intent intent)
        {
            this.OnEvent?.Invoke(intent);
        }
    }
}