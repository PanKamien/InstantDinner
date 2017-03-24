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
        EditText edtTxtProduct1, edtTxtProduct2, edtTxtProduct3, edtTxtProduct4, edtTxtProduct5;
        int i, recipeCount;
        string key, ingredient1, ingredient2, ingredient3, ingredient4, ingredient5;
        Button button1;


        public override void OnBackPressed()
        {
            i = 0;
            this.Finish();
        }

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            key = "9393b6d777ae25211c8b14a569882e64";




            // Create your application here
            SetContentView(Resource.Layout.AddProducts);
            txtViewRecipeTitle = FindViewById<TextView>(Resource.Id.txtViewRecipeTitle);
            button1 = FindViewById<Button>(Resource.Id.button1);
            button1.Enabled = true;
            i = 0;
            




            txtViewRecipeCount = FindViewById<TextView>(Resource.Id.txtViewRecipeCount);
            edtTxtProduct1 = FindViewById<EditText>(Resource.Id.edtTxtProduct1);
            edtTxtProduct2 = FindViewById<EditText>(Resource.Id.edtTxtProduct2);
            edtTxtProduct3 = FindViewById<EditText>(Resource.Id.edtTxtProduct3);
            edtTxtProduct4 = FindViewById<EditText>(Resource.Id.edtTxtProduct4);
            edtTxtProduct5 = FindViewById<EditText>(Resource.Id.edtTxtProduct5);

            recipeCount = 5;


            button1.Click += (s, e) =>
            {
                
                if ((edtTxtProduct1.Text == "" ) && (edtTxtProduct2.Text == "") && (edtTxtProduct3.Text == "") && (edtTxtProduct4.Text == "") && (edtTxtProduct5.Text == ""))
                {
                    i = 999999999;
                    
                }

                if(i < recipeCount )
                {
                    SearchRecipe();
                    
                }
                else
                {
                    button1.Enabled = false;
                    txtViewRecipeTitle.Text = "No more recipes";
                }

                
            };

            
        }

        //shredded%20chicken,salt,bread

        public async void SearchRecipe()
        {
            RootObject dane;

            ingredient1 = edtTxtProduct1.Text;
            ingredient2 = edtTxtProduct2.Text;
            ingredient3 = edtTxtProduct3.Text;
            ingredient4 = edtTxtProduct4.Text;
            ingredient5 = edtTxtProduct5.Text;



            button1.Text = "Next";

            

            using (var httpClient = new HttpClient())
            {
                string url = String.Format("http://food2fork.com/api/search?key={0}&q={1},{2},{3},{4},{5}", key, ingredient1, ingredient2, ingredient3, ingredient4, ingredient5);
                var json = await httpClient.GetStringAsync(url);
                dane = JsonConvert.DeserializeObject<RootObject>(json);
                recipeCount = dane.count;
                txtViewRecipeCount.Text = "Number of recipes: " + recipeCount;
                txtViewRecipeTitle.Text = (i+1) +  ". " + dane.recipes[i].title;
                i++;

            }
            

            
        }
    }
}