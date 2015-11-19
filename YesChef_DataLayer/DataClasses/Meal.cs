using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YesChef_DataLayer.DataClasses
{
    public class Meal
    {
        public int Id { get; set; }
        public DateTime CreatedAt { get; set; }
        public string Name { get; set; }
        public SousChef SousChef { get; set; }

        public ICollection<RecipeInstance> RecipeInstances { get; set; }
    }
}
