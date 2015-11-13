using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YesChef_DataClasses;

namespace YesChef_DataLayer
{
    public class StepHandler
    {
        public static Step CreateStep(string description, int minutesDuration, Recipe recipe)
        {
            var db = new YesChefContext();
            var s = db.Steps.Add(new Step
            {
                Description = description,
                MinutesDuration = minutesDuration,
                Recipe = recipe
            });
            db.SaveChanges();
            return s;
        }

        public static Step GetStep(int stepId)
        {
            var db = new YesChefContext();
            return db.Steps.Find(stepId);
        }
    }
}
