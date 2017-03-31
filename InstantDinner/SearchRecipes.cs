using System;
using Android.App;
using Android.Content;
using Android.OS;
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
        TextView txtViewRecipeTitle, txtViewPublisher, txtViewNumberOfRecipes;
        string ingredient1, ingredient2, ingredient3, ingredient4, ingredient5, key;
        int recipeCount, i;
        Button buttonPreviousRecipe ,buttonNextRecipe;

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
            txtViewPublisher = FindViewById<TextView>(Resource.Id.txtViewPublisher);
            txtViewNumberOfRecipes = FindViewById<TextView>(Resource.Id.textViewNumberOfRecipes);
            buttonPreviousRecipe = FindViewById<Button>(Resource.Id.buttonPreviousRecipe);
            buttonNextRecipe = FindViewById<Button>(Resource.Id.buttonNextRecipe);
            buttonNextRecipe.Enabled = false;

            i = 1;
            key = "9393b6d777ae25211c8b14a569882e64";
            ingredient1 = Intent.GetStringExtra("ingredient1" ?? "");
            ingredient2 = Intent.GetStringExtra("ingredient2" ?? "");
            ingredient3 = Intent.GetStringExtra("ingredient3" ?? "");
            ingredient4 = Intent.GetStringExtra("ingredient4" ?? "");
            ingredient5 = Intent.GetStringExtra("ingredient5" ?? "");


            SearchRecipe();
            buttonNextRecipe.Click += (s, e) =>
            {
                NextRecipe();
            };

        }

        public async void SearchRecipe()
        {
            using (var httpClient = new HttpClient())
            {
                string url = String.Format("http://food2fork.com/api/search?key={0}&q={1},{2},{3},{4},{5}", key, ingredient1, ingredient2, ingredient3, ingredient4, ingredient5);
                var json = await httpClient.GetStringAsync(url);
                dane = JsonConvert.DeserializeObject<RootObject>(json);
                recipeCount = dane.count;
                if (recipeCount > 0)
                {
                    txtViewNumberOfRecipes.Text = "Recipe: 1/" +  dane.count.ToString();
                    Picasso.With(this).Load(dane.recipes[0].image_url).Into(imageViewRecipeImage);
                    txtViewRecipeTitle.Text = dane.recipes[0].title;
                    txtViewPublisher.Text = dane.recipes[0].publisher;
                    buttonNextRecipe.Enabled = true;
                }
                else
                {
                    txtViewRecipeTitle.Text = "Recipes not found";
                    txtViewPublisher.Text = "";
                    buttonNextRecipe.Enabled = false;
                }

                
            }
        }

        public void NextRecipe()
        {

            if (i < recipeCount)
            {
                txtViewNumberOfRecipes.Text = "Recipe: " + (i+1) + "/" + dane.count.ToString();
                Picasso.With(this).Load(dane.recipes[i].image_url).Into(imageViewRecipeImage);
                txtViewRecipeTitle.Text = dane.recipes[i].title;
                txtViewPublisher.Text = "Publisher: " + dane.recipes[i].publisher;
                i++;
            }
            else
            {
                Toast.MakeText(this, "No more recipes", ToastLength.Short).Show();
            }
        }





    }
}