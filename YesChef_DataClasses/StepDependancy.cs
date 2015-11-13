using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YesChef_DataClasses
{
    public class StepDependancy
    {
        public int Id { get; set; }
        public int ChildStepId { get; set; }
        public int ParentStepId { get; set; }

        public virtual Step ChildStep { get; set; }
        public virtual Step ParentStep { get; set; }
    }
}
