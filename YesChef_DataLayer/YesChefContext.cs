using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YesChef_DataLayer.DataClasses;

namespace YesChef_DataLayer
{
    public class YesChefContext : DbContext
    {
        public YesChefContext() : base("name=YesChef") { }

        public DbSet<QuantityType> QuantityTypes { get; set; }
        public DbSet<Ingredient> Ingredients { get; set; }
        public DbSet<Recipe> Recipies { get; set; }
        public DbSet<Step> Steps { get; set; }
        public DbSet<StepDependancy> StepDependancies { get; set; }
        public DbSet<SousChef> SousChefs { get; set; }
        public DbSet<Meal> Meals { get; set; }
        public DbSet<RecipeInstance> RecipeInstances { get; set; }
        public DbSet<RecipeInstanceStep> RecipeInstanceSteps { get; set; }
        public DbSet<StepRecipeDependancy> StepRecipeDependancies { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            //Step Dependancy
            modelBuilder.Entity<StepDependancy>()
                .HasRequired(sd => sd.ChildStep)
                .WithMany(sd => sd.ParentStepDependancies)
                .HasForeignKey(sd => sd.ChildStepId)
                .WillCascadeOnDelete(false);
            modelBuilder.Entity<StepDependancy>()
                .HasRequired(sd => sd.ParentStep)
                .WithMany(sd => sd.ChildStepDependancies)
                .HasForeignKey(sd => sd.ParentStepId)
                .WillCascadeOnDelete(false);

            //RecipeInstanceStep
            modelBuilder.Entity<RecipeInstanceStep>()
                .HasRequired(ris=>ris.Step)
                .WithMany(s=>s.RecipeInstanceSteps)
                .HasForeignKey(s=>s.StepId)
                .WillCascadeOnDelete(false);

            //StepRecipeDependancy
            modelBuilder.Entity<StepRecipeDependancy>()
                .HasRequired(srd => srd.Recipe)
                .WithMany(r => r.StepRecipeDependancies)
                .HasForeignKey(srd => srd.RecipeId)
                .WillCascadeOnDelete(false);
            modelBuilder.Entity<StepRecipeDependancy>()
                .HasRequired(srd => srd.DependantStep)
                .WithMany(s => s.StepRecipeDependancies)
                .HasForeignKey(srd => srd.StepId)
                .WillCascadeOnDelete(false);
        }
    }
}
