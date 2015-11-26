using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace YesChef_DataLayer.DataClasses
{
    public sealed partial class Step
    {
        public Step()
        {
        }

        public int Id { get; private set; }
        [Required]
        [MaxLength(140)]
        public string Description { get; set; }
        public int MinutesDuration { get; set; }
        public bool IsFreeTime { get; set; } //other things can be done e.g. put in oven for 30 mins

        public int RecipeId { get; set; }
        [ForeignKey("RecipeId")]
        public Recipe Recipe { get; set; }

        public ICollection<StepDependancy> StepDependancies { get; set; } = new List<StepDependancy>();
        public ICollection<StepDependancy> StepDependants { get; set; } = new List<StepDependancy>();
    }
}
