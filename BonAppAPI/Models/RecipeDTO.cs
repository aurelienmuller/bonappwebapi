using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BonAppAPI.Models
{
    public class RecipeDTO
    {
        public String recipe_id { get; set; }
        public String source_url { get; set; }
        public String title { get; set; }
    }
}