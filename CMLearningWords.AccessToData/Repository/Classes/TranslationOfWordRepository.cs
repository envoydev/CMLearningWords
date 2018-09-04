using CMLearningWords.AccessToData.Context;
using CMLearningWords.AccessToData.Repository.Interfaces;
using CMLearningWords.DataModels.Models;

namespace CMLearningWords.AccessToData.Repository.Classes
{
    public class TranslationOfWordRepository : Repository<TranslationOfWord>, ITranslationOfWordRepository
    {
        public TranslationOfWordRepository(ApplicationContext context) : base(context) { }

        public TranslationOfWord GetOne(long id)
        {
            return Context.TranslationOfWords.Find(id) ?? null;
        }
    }
}
