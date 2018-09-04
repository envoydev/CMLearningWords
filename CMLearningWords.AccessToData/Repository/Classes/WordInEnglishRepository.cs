using CMLearningWords.AccessToData.Context;
using CMLearningWords.AccessToData.Repository.Interfaces;
using CMLearningWords.DataModels.Models;

namespace CMLearningWords.AccessToData.Repository.Classes
{
    public class WordInEnglishRepository : Repository<WordInEnglish>, IWordInEnglishRepository
    {
        public WordInEnglishRepository(ApplicationContext context) : base(context) { }

        public WordInEnglish GetOne(long id)
        {
            return Context.WordsInEnglish.Find(id) ?? null;
        }

    }
}
