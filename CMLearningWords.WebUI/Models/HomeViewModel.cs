using CMLearningWords.DataModels.Models;
using System.Collections.Generic;

namespace CMLearningWords.WebUI.Models
{
    public class IndexHomeViewModel
    {
        public IEnumerable<WordInEnglish> WordsInEnglish { get; set; }
        public FilterIndexHomeViewModel Filters { get; set; }
        public SortIndexHomeViewModel Sorting { get; set; }
    }
}
