﻿using System;
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
        public RecipeInstance RecipeInstance { get; set; }

        public int StepId { get; set; }
        [ForeignKey("StepId")]
        public Step Step { get; set; }
    }
}
