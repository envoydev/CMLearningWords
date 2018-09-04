using CMLearningWords.DataModels.Models;
using CMLearningWords.WebUI.Enums;
using CMLearningWords.WebUI.HelpAttributes;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace CMLearningWords.WebUI.Models
{
    public class GenerateTestYourselfViewModel
    {
        
        [MinValue(5, ErrorMessage = "Нельзя ниже 5")]
        [Required(ErrorMessage = "Поле не может быть пустым")]
        public int Number { get; set; }

        public long StageId { get; set; }

        public SelectList AllStages { get; set; }
    }

    public class CreatedTestYourselfViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public List<TranslationOfWord> TranslationOfWords { get; set; }

        [Required(ErrorMessage = "Поля обязательно для заполнения")]
        public string NameOfCurrentInputTranslation { get; set; }

        public ExeptionInTranslation MadeMistake { get; set; } = ExeptionInTranslation.WithMistake;
    }
}
