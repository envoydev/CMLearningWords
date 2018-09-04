using CMLearningWords.DataModels.Models;
using CMLearningWords.WebUI.Enums;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;

namespace CMLearningWords.WebUI.Models
{
    public class FilterIndexHomeViewModel
    {
        public SelectList StagesOfMethod { get; }
        public long? SelectedStage { get; }
        public string SelectedName { get; }

        public FilterIndexHomeViewModel(List<StageOfMethod> stagesOfMethod, long? selectedStage, string searchName)
        {
            stagesOfMethod.Insert(0, new StageOfMethod { Name = "Все стейджи", Id = 0 });
            StagesOfMethod = new SelectList(stagesOfMethod, "Id", "Name", selectedStage);
            SelectedStage = selectedStage;
            SelectedName = searchName;
        }
    }

    public class SortIndexHomeViewModel
    {
        public SortIndexHome WordInEnglishSort { get; set; }
        public SortIndexHome StageOfMethodSort { get; set; }
        public SortIndexHome CurrentSort { get; set; }

        public SortIndexHomeViewModel(SortIndexHome sortOrder)
        {
            WordInEnglishSort = sortOrder == SortIndexHome.WordInEnglishAsc ? SortIndexHome.WordInEnglishDesc : SortIndexHome.WordInEnglishAsc;
            StageOfMethodSort = sortOrder == SortIndexHome.StageOfMethodAsc ? SortIndexHome.StageOfMethodDesc : SortIndexHome.StageOfMethodAsc;
            CurrentSort = sortOrder;
        }
    }
}
