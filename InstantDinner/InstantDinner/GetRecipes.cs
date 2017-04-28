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
    [Activity(Label = "", ScreenOrientation = ScreenOrientation.Portrait, Theme = "@android:style/Theme.DeviceDefault.Light")]
    public class GetRecipes : Activity
    {
        string recipeID;
        RootObject przepisDane;
        string key, label;
        ImageView imgViewRecipeImg_2;
        TextView txtViewGetRecipe_Title, txtViewGetRecipe_Ingredients;
        Button buttonViewInBrowser;



        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.GetRecipes);

            key = "9393b6d777ae25211c8b14a569882e64";
            recipeID = Intent.GetStringExtra("recipeID" ?? "");

            imgViewRecipeImg_2 = FindViewById<ImageView>(Resource.Id.imgViewRecipeImg_2);

            txtViewGetRecipe_Title = FindViewById<TextView>(Resource.Id.txtViewGetRecipe_Title);
            txtViewGetRecipe_Ingredients = FindViewById<TextView>(Resource.Id.txtViewGetRecipe_Ingredients);
            buttonViewInBrowser = FindViewById<Button>(Resource.Id.buttonViewInBrowser);

            GetRecipe();

            buttonViewInBrowser.Click += delegate {
                ViewInBrowser();
            };


        }

        public override bool OnCreateOptionsMenu(IMenu menu)
        {
            var inflater = MenuInflater;
            inflater.Inflate(Resource.Menu.getRecipesActionBar, menu);
            return true;

        }

        public override bool OnOptionsItemSelected(IMenuItem item)
        {
            int id = item.ItemId;
            if (id == Resource.Id.actionBarAddToFav)
            {
                Toast.MakeText(this, "Added to vaforites", ToastLength.Short).Show();
                return true;
            }
            return base.OnOptionsItemSelected(item);
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
            label = "Social rank: " + Math.Round(przepisDane.recipe.social_rank, 2).ToString();
            this.Title = label;
        }

        public void LoadRecipeTitle()
        {
            txtViewGetRecipe_Title.Text = przepisDane.recipe.title;
        }

        public void LoadIngredients()
        {
            int i = 0;
            while (i < przepisDane.recipe.ingredients.Count())
            {
                txtViewGetRecipe_Ingredients.Text += "• " + przepisDane.recipe.ingredients[i] + "\n";
                i++;
            }

        }


        public void ViewInBrowser()
        {
            var uri = Android.Net.Uri.Parse(przepisDane.recipe.source_url);
            var intent = new Intent(Intent.ActionView, uri);
            StartActivity(intent);
        }


    }
}