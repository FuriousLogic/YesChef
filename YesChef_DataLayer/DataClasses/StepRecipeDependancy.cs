using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YesChef_DataLayer.DataClasses
{
    public class StepRecipeDependancy
    {
        public int Id  { get; set; }

        public int RecipeId { get; set; }
        public virtual Recipe Recipe { get; set; }

        public int StepId { get; set; }
        public virtual Step DependantStep { get; set; }
    }
}
