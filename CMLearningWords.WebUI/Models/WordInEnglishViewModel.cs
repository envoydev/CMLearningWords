using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using CMLearningWords.DataModels.Models;
using CMLearningWords.WebUI.HelpAttributes;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace CMLearningWords.WebUI.Models
{
    public class CreateWordInEnglishViewModel
    {
        [Display(Name = "Слово на английском")]
        [Required(ErrorMessage = "Поле обязательно для ввода")]
        [StringLength(150, MinimumLength = 2, ErrorMessage = "Длина строки должна быть от 1 до 150 символов")]
        [Remote(action: "CheckWordInEnglishName", controller: "WordInEnglish", ErrorMessage = "Такое слово уже существует")]
        public string Name { get; set; }

        [Display(Name = "Выберете стейдж")]
        [Required(ErrorMessage = "Поле обязательно для заполнения")]
        public long StageOfMethodId { get; set; }


        public List<CreateTranslationOfWordViewModel> Translations { get; set; }

        //For dropdown list
        public SelectList StagesOfMethod { get; set; }
    }
}

