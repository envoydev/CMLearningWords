using CMLearningWords.DataModels.Models;
using Microsoft.EntityFrameworkCore;

namespace CMLearningWords.AccessToData.Context
{
    public class ApplicationContext : DbContext
    {
        public ApplicationContext() { }
        public ApplicationContext(DbContextOptions<ApplicationContext> options) : base(options) { }

        //Table of Methods in Callan Method Lessons
        public DbSet<StageOfMethod> StageOfMethods { get; set; }
        //Table of words in english 
        public DbSet<WordInEnglish> WordsInEnglish { get; set; }
        //Table of translations of word in english
        public DbSet<TranslationOfWord> TranslationOfWords { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            //add unique field to Name of Table "WordsInEnglish"
            builder.Entity<WordInEnglish>().HasIndex(w => w.Name).IsUnique();
        }
    }
}
