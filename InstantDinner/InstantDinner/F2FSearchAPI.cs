﻿using System;
using System.Collections.Generic;


namespace InstantDinner
{

    public class Recipe
    {
        public string publisher { get; set; }
        public string f2f_url { get; set; }
        public string title { get; set; }
        public string source_url { get; set; }
        public string recipe_id { get; set; }
        public string image_url { get; set; }
        public double social_rank { get; set; }
        public string publisher_url { get; set; }


        public List<string> ingredients { get; set; } //tylko GetRecipe
    }

    public class RootObject
    {
        public int count { get; set; } //tylko SearchRecipe
        public List<Recipe> recipes { get; set; } //tylko SearchRecipe


        public Recipe recipe { get; set; } //tylko GetRecipe
    }

}