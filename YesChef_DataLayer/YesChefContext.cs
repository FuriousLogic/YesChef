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

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            ////Recipe
            //modelBuilder.Entity<Recipe>().Property(r => r.Name).IsRequired();

            ////Ingredient
            //modelBuilder.Entity<Ingredient>().Property(i => i.Name).IsRequired();
            //modelBuilder.Entity<Ingredient>()
            //    .HasRequired(i => i.Recipe)
            //    .WithMany(r => r.Ingredients)
            //    .HasForeignKey(i => i.RecipeId)
            //    .WillCascadeOnDelete(true);
            //modelBuilder.Entity<Ingredient>()
            //    .HasRequired(i => i.QuantityType)
            //    .WithMany(qt => qt.Ingredients)
            //    .HasForeignKey(i => i.QuantityTypeId)
            //    .WillCascadeOnDelete(true);

            ////Step
            //modelBuilder.Entity<Step>().Property(i => i.Description).IsRequired();
            //modelBuilder.Entity<Step>()
            //    .HasRequired(s => s.Recipe)
            //    .WithMany(r => r.Steps)
            //    .HasForeignKey(s => s.RecipeId)
            //    .WillCascadeOnDelete(true);

            //Step Dependancy
            modelBuilder.Entity<StepDependancy>()
                .HasRequired(sd => sd.ChildStep)
                .WithMany(sd => sd.StepDependancies)
                .HasForeignKey(sd => sd.ChildStepId)
                .WillCascadeOnDelete(false);
            modelBuilder.Entity<StepDependancy>()
                .HasRequired(sd => sd.ParentStep)
                .WithMany(sd => sd.StepDependants)
                .HasForeignKey(sd => sd.ParentStepId)
                .WillCascadeOnDelete(false);

            //RecipeInstanceStep
            modelBuilder.Entity<RecipeInstanceStep>()
                .HasRequired(ris=>ris.Step)
                .WithMany(s=>s.RecipeInstanceSteps)
                .HasForeignKey(s=>s.StepId)
                .WillCascadeOnDelete(false);
        }
    }
}
