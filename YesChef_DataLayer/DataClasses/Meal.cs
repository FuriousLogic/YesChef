using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
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

        public int SousChefId { get; set; }
        [ForeignKey("SousChefId")]
        public virtual SousChef SousChef { get; set; }

        public virtual ICollection<RecipeInstance> RecipeInstances { get; set; }
    }
}
