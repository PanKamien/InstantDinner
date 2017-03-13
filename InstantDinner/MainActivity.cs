using Android.App;
using Android.Widget;
using Android.OS;
using System;

namespace InstantDinner
{
    [Activity(Label = "InstantDinner", MainLauncher = true, Icon = "@drawable/icon", Theme = "@android:style/Theme.DeviceDefault.Light.NoActionBar")]
    public class MainActivity : Activity
    {
        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);
            SetContentView (Resource.Layout.Main);

            var btnAddProducts = FindViewById<Button>(Resource.Id.btnAddProducts);
            var btnMealsHistory = FindViewById<Button>(Resource.Id.btnMealsHistory);

            btnAddProducts.Click += btnAddProducts_Click;
        }

        private void btnAddProducts_Click(object sender, EventArgs e)
        {
            throw new NotImplementedException();
        }
    }
}

