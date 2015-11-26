using System.ComponentModel.DataAnnotations.Schema;

namespace YesChef_DataLayer.DataClasses
{
    public sealed class StepDependancy
    {
        public int Id { get; set; }

        public int ChildStepId { get; set; }
        public Step ChildStep { get; set; }

        public int ParentStepId { get; set; }
        public Step ParentStep { get; set; }
    }
}
