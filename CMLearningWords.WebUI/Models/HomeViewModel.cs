using CMLearningWords.DataModels.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CMLearningWords.WebUI.Models
{
    public class IndexHomeViewModel
    {
        public IEnumerable<WordInEnglish> WordsInEnglish { get; set; }
        public FilterIndexHomeViewModel Filters { get; set; }
        public SortIndexHomeViewModel Sorting { get; set; }
    }
}
