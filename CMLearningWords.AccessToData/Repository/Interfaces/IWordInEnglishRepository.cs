using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CMLearningWords.DataModels.Models;

namespace CMLearningWords.AccessToData.Repository.Interfaces
{
    public interface IWordInEnglishRepository : IRepository<WordInEnglish>
    {
        WordInEnglish GetOne(long id);
    }
}
