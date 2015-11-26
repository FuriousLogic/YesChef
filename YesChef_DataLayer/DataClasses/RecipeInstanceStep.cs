using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.AccessControl;
using System.Text;
using System.Threading.Tasks;

namespace YesChef_DataLayer.DataClasses
{
    public class RecipeInstanceStep
    {
        public int Id { get; set; }
        public RecipeInstance RecipeInstance { get; set; }
        public Step Step { get; set; }
        public DateTime? Started { get; set; }
        public DateTime? Finished { get; set; }
    }
}
