using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YesChef_DataLayer.DataClasses;

namespace YesChef_DataLayer
{
    public class StepHandler
    {
        public static Step CreateStep(string description, int minutesDuration, Recipe recipe, bool isFreeTime = false)
        {
            var db = new YesChefContext();
            var s = db.Steps.Add(new Step
            {
                Description = description,
                MinutesDuration = minutesDuration,
                Recipe = recipe,
                IsFreeTime = isFreeTime
            });
            db.SaveChanges();
            return s;
        }

        public static Step GetStep(int stepId)
        {
            var db = new YesChefContext();
            return db.Steps.Find(stepId);
        }

        public static List<Step> GetChildSteps(int stepId)
        {
            var db = new YesChefContext();
            return (from sd in db.StepDependancies where sd.ParentStepId == stepId select sd.ChildStep).ToList();
        }

        public static List<Step> GetParentSteps(int stepId)
        {
            var db = new YesChefContext();
            return (from sd in db.StepDependancies where sd.ChildStepId == stepId select sd.ParentStep).ToList();
        }
    }
}
