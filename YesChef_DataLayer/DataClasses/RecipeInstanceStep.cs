using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Security.AccessControl;
using System.Text;
using System.Threading.Tasks;

namespace YesChef_DataLayer.DataClasses
{
    public class RecipeInstanceStep
    {
        public int Id { get; set; }
        public DateTime? Started { get; set; }
        public DateTime? Finished { get; set; }

        public int RecipeInstanceId { get; set; }
        [ForeignKey("RecipeInstanceId")]
        public virtual RecipeInstance RecipeInstance { get; set; }

        public int StepId { get; set; }
        //[ForeignKey("StepId")] - defined in Fluent API
        public virtual Step Step { get; set; }

        [NotMapped]
        public int MinutesBeforeRecipeEndToStart { get; set; }
    }
}
