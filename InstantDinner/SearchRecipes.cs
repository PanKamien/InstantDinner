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
using Newtonsoft.Json;
using System.Net.Http;
using Square.Picasso;
using Android.Content.PM;

namespace InstantDinner
{
    [Activity(Label = "SearchRecipes", ScreenOrientation = ScreenOrientation.Portrait, Theme = "@android:style/Theme.DeviceDefault.Light.NoActionBar")]
    public class SearchRecipes : Activity
    {
        RootObject dane;
        ImageView imageViewRecipeImage;
        TextView txtViewRecipeTitle;
        string ingredient1, ingredient2, ingredient3, ingredient4, ingredient5, key;
        int recipeCount;

        public override void OnBackPressed()
        {
            this.Finish();
        }
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.SearchRecipes);

            imageViewRecipeImage = FindViewById<ImageView>(Resource.Id.imageViewRecipeImage);
            txtViewRecipeTitle = FindViewById<TextView>(Resource.Id.txtViewRecipeTitle);

            key = "9393b6d777ae25211c8b14a569882e64";
            ingredient1 = Intent.GetStringExtra("ingredient1" ?? "");
            ingredient2 = Intent.GetStringExtra("ingredient2" ?? "");
            ingredient3 = Intent.GetStringExtra("ingredient3" ?? "");
            ingredient4 = Intent.GetStringExtra("ingredient4" ?? "");
            ingredient5 = Intent.GetStringExtra("ingredient5" ?? "");



            SearchRecipe();

        }

        public async void SearchRecipe()
        {
           

            using (var httpClient = new HttpClient())
            {
                string url = String.Format("http://food2fork.com/api/search?key={0}&q={1},{2},{3},{4},{5}", key, ingredient1, ingredient2, ingredient3, ingredient4, ingredient5);
                var json = await httpClient.GetStringAsync(url);
                dane = JsonConvert.DeserializeObject<RootObject>(json);
                recipeCount = dane.count;
                //if (recipeCount > 0)
                //{
                //    txtViewRecipeCount.Text = "Number of recipes: " + recipeCount;
                //    txtViewRecipeTitle.Text = (i + 1) + ". " + dane.recipes[i].title;
                //    i++;
                //}

                Picasso.With(this).Load(dane.recipes[0].image_url).Into(imageViewRecipeImage);

                txtViewRecipeTitle.Text = dane.recipes[0].title;

            }
        }
    }
}