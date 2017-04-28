using Android.App;
using Android.Widget;
using Android.OS;
using System;
using Android.Content;
using Android.Content.PM;

namespace InstantDinner
{
    [Activity(Label = "InstantDinner", MainLauncher = true, Icon = "@drawable/food1", ScreenOrientation = ScreenOrientation.Portrait, Theme = "@android:style/Theme.DeviceDefault.Light.NoActionBar")]
    public class MainActivity : Activity
    {
        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);
            SetContentView(Resource.Layout.Main);

            var btnAddProducts = FindViewById<Button>(Resource.Id.btnAddProducts);
            var btnFavMeals = FindViewById<Button>(Resource.Id.btnFavMeals);
            //var linearLayoutMainDown = FindViewById<LinearLayout>(Resource.Id.linearLayoutMainDown);



            btnAddProducts.Click += btnAddProducts_Click;
            btnFavMeals.Click += btnFavMeals_Click;
            //linearLayoutMainDown.Click += linearLayoutMainDown_Click;
        }


        private void btnAddProducts_Click(object sender, EventArgs e)
        {
            Intent addProductsActivity = new Intent(this, typeof(AddProducts));
            StartActivity(addProductsActivity);

        }

        private void btnFavMeals_Click(object sender, EventArgs e)
        {
            Intent favMealsActivity = new Intent(this, typeof(FavMeals));
            StartActivity(favMealsActivity);
        }

        //private void linearLayoutMainDown_Click(object sender, EventArgs e)
        //{
        //    Intent favMealsActivity = new Intent(this, typeof(FavMeals));
        //    StartActivity(favMealsActivity);
        //}

    }
}
