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
using System.Net.Http;
using Newtonsoft.Json;

namespace InstantDinner
{


    [Activity(Label = "AddProducts")]
    public class AddProducts : Activity
    {
        TextView txtViewRecipeTitle, txtViewRecipeCount;
        int i, recipeCount;
        public override void OnBackPressed()
        {
            i = 0;
            this.Finish();
        }

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);



            // Create your application here
            SetContentView(Resource.Layout.AddProducts);
            txtViewRecipeTitle = FindViewById<TextView>(Resource.Id.txtViewRecipeTitle);
            var button1 = FindViewById<Button>(Resource.Id.button1);
            button1.Enabled = true;
            i = 0;
            SearchRecipe();




            txtViewRecipeCount = FindViewById<TextView>(Resource.Id.txtViewRecipeCount);
            




            button1.Click += (s, e) =>
            {
                
                if(i < recipeCount -1 )
                {
                    SearchRecipe();
                    i++;
                }
                else
                {
                    button1.Enabled = false;
                    txtViewRecipeTitle.Text = "No more recipes";
                }
                

            };

            
        }



        public async void SearchRecipe()
        {
            RootObject dane;

            using (var httpClient = new HttpClient())
            {
                string url = "http://food2fork.com/api/search?key=9393b6d777ae25211c8b14a569882e64&q=shredded%20chicken,salt,bread";
                var json = await httpClient.GetStringAsync(url);
                dane = JsonConvert.DeserializeObject<RootObject>(json);
                recipeCount = dane.count;
                txtViewRecipeCount.Text = "Number of recipes: " + recipeCount;
                txtViewRecipeTitle.Text = (i+1) +  ". " + dane.recipes[i].title;


            }
            

            
        }
    }
}