using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CMLearningWords.AccessToData.Context;
using CMLearningWords.AccessToData.Repository.Interfaces;
using CMLearningWords.DataModels.Models;

namespace CMLearningWords.AccessToData.Repository.Classes
{
    public class TranslationOfWordRepository : Repository<TranslationOfWord>, ITranslationOfWordRepository
    {
        public TranslationOfWordRepository(ApplicationContext context) : base(context) { }
    }
}
