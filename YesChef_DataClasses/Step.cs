using System.Collections.Generic;

namespace YesChef_DataClasses
{
    public partial class Step
    {
        public Step()
        {
            StepDependancies = new List<StepDependancy>();
            StepDependants = new List<StepDependancy>();
        }

        public int Id { get; private set; }
        public string Description { get; set; }
        public int MinutesDuration { get; set; }

        public virtual Recipe Recipe { get; set; }
        public virtual ICollection<StepDependancy> StepDependancies { get; set; }
        public virtual ICollection<StepDependancy> StepDependants { get; set; }
    }
}
