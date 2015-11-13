using System.Collections.Generic;

namespace YesChef_DataClasses
{
    public class Step
    {
        public Step()
        {
            Dependancies = new List<StepDependancy>();
            Dependants = new List<StepDependancy>();
        }

        public int Id { get; private set; }
        public string Description { get; set; }
        public int MinutesDuration { get; set; }

        public virtual Recipe Recipe { get; set; }
        public virtual ICollection<StepDependancy> Dependancies { get; set; }
        public virtual ICollection<StepDependancy> Dependants { get; set; }
    }
}
