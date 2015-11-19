using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YesChef_DataLayer.DataClasses
{
    public class RecipeInstance
    {
        public int Id { get; set; }
        public Recipe Recipe { get; set; }
        public Meal Meal { get; set; }
    }
}
