using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CMLearningWords.AccessToData.Repository.Classes;
using CMLearningWords.DataModels.Models;

namespace CMLearningWords.AccessToData.Repository.Interfaces
{
    public interface IStageOfMethodRepository : IRepository<StageOfMethod>
    {
        StageOfMethod GetOne(long id);
    }
}
