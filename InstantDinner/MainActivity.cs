using Android.App;
using Android.Widget;
using Android.OS;

namespace InstantDinner
{
    [Activity(Label = "InstantDinner", MainLauncher = true, Icon = "@drawable/icon")]
    public class MainActivity : Activity
    {
        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);
            
            SetContentView (Resource.Layout.Main);
        }
    }
}

