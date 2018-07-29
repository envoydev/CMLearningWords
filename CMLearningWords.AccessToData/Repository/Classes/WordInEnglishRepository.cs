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
    public class WordInEnglishRepository : Repository<WordInEnglish>, IWordInEnglishRepository
    {
        public WordInEnglishRepository(ApplicationContext context) : base(context) { }

        public WordInEnglish GetOne(long id)
        {
            return Context.WordsInEnglish.Find(id) ?? null;
        }

    }
}
