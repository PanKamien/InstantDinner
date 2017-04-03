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
using Android.Content.PM;
using System.Net.Http;
using Newtonsoft.Json;

namespace InstantDinner
{
    [Activity(Label = "GetRecipes", ScreenOrientation = ScreenOrientation.Portrait, Theme = "@android:style/Theme.DeviceDefault.Light.NoActionBar")]
    public class GetRecipes : Activity
    {
        string recipeID;
        RootObject przepisDane;
        string key;
        TextView txtViewRecipe1;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.GetRecipes);

            key = "9393b6d777ae25211c8b14a569882e64";
            recipeID = Intent.GetStringExtra("recipeID" ?? "");

            txtViewRecipe1 = FindViewById<TextView>(Resource.Id.txtViewIngredient1);

            SearchRecipe();
        }

        public async void SearchRecipe()
        {
            using (var httpClient = new HttpClient())
            {
                string url = String.Format(" http://food2fork.com/api/get?key={0}&&rId={1}", key, recipeID);
                var json = await httpClient.GetStringAsync(url);
                przepisDane = JsonConvert.DeserializeObject<RootObject>(json);
            }

            txtViewRecipe1.Text = "title: " + przepisDane.recipe.title + ",\n" + "ingr 1: " + przepisDane.recipe.ingredients[0] + ",\ningr 2: " + przepisDane.recipe.ingredients[1];
        }


    }
}