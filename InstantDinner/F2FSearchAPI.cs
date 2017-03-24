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
using System.Net;
using System.Net.Http;
using Newtonsoft.Json;
using System.


namespace InstantDinner
{
    class F2FSearchAPI
    {
        public async static RootObject SearchRecipe(string APIKey, string ingredients)
        {
            var httpClient = new HttpClient();
            var response = await httpClient.GetAsync("http://food2fork.com/api/search?key=9393b6d777ae25211c8b14a569882e64&q=shredded%20chicken,salt,bread");
            var result = await response.Content.ReadAsStringAsync();
            var serializer = JsonConvert.SerializeObject(typeof(RootObject));


        
        }

    }

    
    public class Recipe
    {
        [DataMember]
        public string publisher { get; set; }
        public string f2f_url { get; set; }
        public string title { get; set; }
        public string source_url { get; set; }
        public string recipe_id { get; set; }
        public string image_url { get; set; }
        public double social_rank { get; set; }
        public string publisher_url { get; set; }
    }

    public class RootObject
    {
        public int count { get; set; }
        public List<Recipe> recipes { get; set; }
    }

}