using Android.App;
using Android.OS;
using Android.Widget;
using Android.Content.PM;
using Android.Content;

namespace InstantDinner
{

    [Activity(Label = "AddProducts", ScreenOrientation = ScreenOrientation.Portrait, Theme = "@android:style/Theme.DeviceDefault.Light.NoActionBar")]
    public class AddProducts : Activity
    {
        TextView txtViewRecipeTitle, txtViewRecipeCount;
        EditText edtTxtProduct1, edtTxtProduct2, edtTxtProduct3, edtTxtProduct4, edtTxtProduct5;
        Button buttonSearch;

        public override void OnBackPressed()
        {

            this.Finish();
        }

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            SetContentView(Resource.Layout.AddProducts);

            txtViewRecipeTitle = FindViewById<TextView>(Resource.Id.txtViewRecipeTitle);
            buttonSearch = FindViewById<Button>(Resource.Id.buttonSearch);
            edtTxtProduct1 = FindViewById<EditText>(Resource.Id.edtTxtProduct1);
            edtTxtProduct2 = FindViewById<EditText>(Resource.Id.edtTxtProduct2);
            edtTxtProduct3 = FindViewById<EditText>(Resource.Id.edtTxtProduct3);
            edtTxtProduct4 = FindViewById<EditText>(Resource.Id.edtTxtProduct4);
            edtTxtProduct5 = FindViewById<EditText>(Resource.Id.edtTxtProduct5);

            buttonSearch.Click += (s, e) =>
            {
                GetRecipes();
            };

        }

        public void GetRecipes()
        {
            Intent nextActivity = new Intent(this, typeof(SearchRecipes));
            nextActivity.PutExtra("ingredient1", edtTxtProduct1.Text);
            nextActivity.PutExtra("ingredient2", edtTxtProduct2.Text);
            nextActivity.PutExtra("ingredient3", edtTxtProduct3.Text);
            nextActivity.PutExtra("ingredient4", edtTxtProduct4.Text);
            nextActivity.PutExtra("ingredient5", edtTxtProduct5.Text);
            StartActivity(nextActivity);
        }
    }
}