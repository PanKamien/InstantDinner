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
using Square.Picasso;

namespace InstantDinner
{
    [Activity(Label = "GetRecipes", ScreenOrientation = ScreenOrientation.Portrait, Theme = "@android:style/Theme.DeviceDefault.Light.NoActionBar")]
    public class GetRecipes : Activity
    {
        string recipeID;
        RootObject przepisDane;
        string key;
        ImageView imgViewRecipeImg_2;
        TextView txtViewGetRecipe_SocialRank, txtViewGetRecipe_Title, txtViewGetRecipe_Ingredients;



        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.GetRecipes);

            key = "9393b6d777ae25211c8b14a569882e64";
            recipeID = Intent.GetStringExtra("recipeID" ?? "");

            imgViewRecipeImg_2 = FindViewById<ImageView>(Resource.Id.imgViewRecipeImg_2);
            txtViewGetRecipe_SocialRank = FindViewById<TextView>(Resource.Id.txtViewGetRecipe_SocialRank);
            txtViewGetRecipe_Title = FindViewById<TextView>(Resource.Id.txtViewGetRecipe_Title);
            txtViewGetRecipe_Ingredients = FindViewById<TextView>(Resource.Id.txtViewGetRecipe_Ingredients);



            GetRecipe();
        }

        public async void GetRecipe()
        {
            using (var httpClient = new HttpClient())
            {
                string url = String.Format(" http://food2fork.com/api/get?key={0}&&rId={1}", key, recipeID);
                var json = await httpClient.GetStringAsync(url);
                przepisDane = JsonConvert.DeserializeObject<RootObject>(json);
            }
            LoadImage();
            LoadSocialRank();
            LoadRecipeTitle();
            LoadIngredients();

        }



        public void LoadImage()
        {
            Picasso.With(this).Load(przepisDane.recipe.image_url).Into(imgViewRecipeImg_2);
        }

        public void LoadSocialRank()
        {
            txtViewGetRecipe_SocialRank.Text = "Social rank: " + Math.Round(przepisDane.recipe.social_rank, 2).ToString();
        }

        public void LoadRecipeTitle()
        {
            txtViewGetRecipe_Title.Text = przepisDane.recipe.title;
        }

        public void LoadIngredients()
        {
            int i = 0;
            while(i < przepisDane.recipe.ingredients.Count())
            {
                txtViewGetRecipe_Ingredients.Text += "• " + przepisDane.recipe.ingredients[i] + "\n";
                i++;
            }
            
        }

    }
}