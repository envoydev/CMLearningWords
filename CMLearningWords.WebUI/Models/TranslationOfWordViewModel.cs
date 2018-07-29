using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace CMLearningWords.WebUI.Models
{
    public class CreateTranslationOfWordViewModel
    {
        [Display(Name = "Название")]
        //validation of required field
        [Required(ErrorMessage = "Введите слово которое нужно добавить")]
        //Validation for length of string
        [StringLength(150, MinimumLength = 3, ErrorMessage = "Длина строки должна быть от 3 до 150 символов")]
        //Check if word exist in database
        [Remote(action: "CheckTranslationOfWordName", controller: "TranslationOfWord", ErrorMessage = "Такой перевод в данном слове уже существует")]
        public string Name { get; set; }
    }
}
