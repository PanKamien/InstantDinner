using System;
using Android.App;
using Android.OS;
using Android.Widget;
using System.Net.Http;
using Newtonsoft.Json;
using Android.Content.PM;
using System.Threading.Tasks;
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


            // Create your application here
            SetContentView(Resource.Layout.AddProducts);
            txtViewRecipeTitle = FindViewById<TextView>(Resource.Id.txtViewRecipeTitle);
            buttonSearch = FindViewById<Button>(Resource.Id.buttonSearch);

            //i = 0;
            




            txtViewRecipeCount = FindViewById<TextView>(Resource.Id.txtViewRecipeCount);
            edtTxtProduct1 = FindViewById<EditText>(Resource.Id.edtTxtProduct1);
            edtTxtProduct2 = FindViewById<EditText>(Resource.Id.edtTxtProduct2);
            edtTxtProduct3 = FindViewById<EditText>(Resource.Id.edtTxtProduct3);
            edtTxtProduct4 = FindViewById<EditText>(Resource.Id.edtTxtProduct4);
            edtTxtProduct5 = FindViewById<EditText>(Resource.Id.edtTxtProduct5);



            buttonSearch.Click += (s, e) =>
            {
                //i = 0;
                //if ((edtTxtProduct1.Text == "" ) && (edtTxtProduct2.Text == "") && (edtTxtProduct3.Text == "") && (edtTxtProduct4.Text == "") && (edtTxtProduct5.Text == ""))
                //{
                //    i = 999999999;

                //}


                GetRecipe();
                
            };


            //buttonNext.Click += (s, e) =>
            //{
              

            //    if (i < recipeCount)
            //    {
            //        ShowRecipes();

            //    }
            //    else
            //    {
            //        txtViewRecipeTitle.Text = "No more recipes";
            //        buttonNext.Enabled = false;
            //        buttonSearch.Enabled = true;
            //    }
            //};
        }

        //shredded%20chicken,salt,bread

        

        //public void ShowRecipes()
        //{
        //    txtViewRecipeCount.Text = "Number of recipes: " + recipeCount;
        //    txtViewRecipeTitle.Text = (i + 1) + ". " + dane.recipes[i].title;
        //    i++;
        //}

        public void GetRecipe()
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