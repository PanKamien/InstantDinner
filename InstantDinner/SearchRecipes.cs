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
        string ingredient1, ingredient2, ingredient3, ingredient4, ingredient5, key, recipeID;
        int recipeCount, i;
        Button buttonPreviousRecipe, buttonNextRecipe, buttonGetRecipe;

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
            buttonGetRecipe = FindViewById<Button>(Resource.Id.buttonGetRecipe);

            buttonPreviousRecipe.Enabled = false;
            buttonNextRecipe.Enabled = false;
            buttonGetRecipe.Enabled = false;

            i = 0;
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

            buttonPreviousRecipe.Click += (s, e) =>
            {
                PreviousRecipe();
            };

            buttonGetRecipe.Click += (s, e) =>
            {
                SendRecipeId();
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
                
                if (recipeCount > 1)
                {
                    txtViewNumberOfRecipes.Text = "Recipe: 1/" +  dane.count.ToString();
                    Picasso.With(this).Load(dane.recipes[i].image_url).Into(imageViewRecipeImage);
                    txtViewRecipeTitle.Text = dane.recipes[i].title;
                    txtViewPublisher.Text = "Publisher: " + dane.recipes[i].publisher;
                    recipeID = dane.recipes[i].recipe_id;
                    buttonNextRecipe.Enabled = true;
                    buttonGetRecipe.Enabled = true;
                }
                else if (recipeCount == 1)
                {
                    txtViewNumberOfRecipes.Text = "Recipe: 1/" + dane.count.ToString();
                    Picasso.With(this).Load(dane.recipes[i].image_url).Into(imageViewRecipeImage);
                    txtViewRecipeTitle.Text = dane.recipes[i].title;
                    recipeID = dane.recipes[i].recipe_id;
                    txtViewPublisher.Text = "Publisher: " + dane.recipes[i].publisher;
                    buttonGetRecipe.Enabled = true;
                }
                else
                {
                    txtViewRecipeTitle.Text = "Recipes not found";
                    txtViewPublisher.Text = "";
                    buttonNextRecipe.Enabled = false;
                    buttonGetRecipe.Enabled = false;
                }

                
            }
        }

        public void NextRecipe()
        {

            if (i < recipeCount-1)
            {
                i++;
                txtViewNumberOfRecipes.Text = "Recipe: " + (i+1) + "/" + dane.count.ToString();
                Picasso.With(this).Load(dane.recipes[i].image_url).Into(imageViewRecipeImage);
                txtViewRecipeTitle.Text = dane.recipes[i].title;
                txtViewPublisher.Text = "Publisher: " + dane.recipes[i].publisher;
                recipeID = dane.recipes[i].recipe_id;
                buttonPreviousRecipe.Enabled = true;
                buttonGetRecipe.Enabled = true;
                if (i == recipeCount - 1)
                    buttonNextRecipe.Enabled = false;
            }
            else
            {
                Toast.MakeText(this, "No more recipes", ToastLength.Short).Show();
            }
        }

        public void PreviousRecipe()
        {
            if (i > 0)
            {
                i--;
                txtViewNumberOfRecipes.Text = "Recipe: " + (i + 1) + "/" + dane.count.ToString();
                Picasso.With(this).Load(dane.recipes[i].image_url).Into(imageViewRecipeImage);
                txtViewRecipeTitle.Text = dane.recipes[i].title;
                txtViewPublisher.Text = "Publisher: " + dane.recipes[i].publisher;
                recipeID = dane.recipes[i].recipe_id;
                buttonNextRecipe.Enabled = true;
                buttonGetRecipe.Enabled = true;
                if (i == 0)
                    buttonPreviousRecipe.Enabled = false;
                
            }
            else
            {
                Toast.MakeText(this, "First recipe", ToastLength.Short).Show();
            }
        }


        public void SendRecipeId()
        {
            Intent nextActivity = new Intent(this, typeof(GetRecipes));
            nextActivity.PutExtra("recipeID", recipeID);
            StartActivity(nextActivity);
        }




    }
}