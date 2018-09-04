using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using CMLearningWords.DataModels.Models;

namespace CMLearningWords.WebUI.Models
{
    public class CreateTranslationOfWordViewModel
    {
        [Display(Name = "Перевод")]
        //validation of required field
        [Required(ErrorMessage = "Введите слово которое нужно добавить")]
        //Validation for length of string
        [StringLength(150, MinimumLength = 3, ErrorMessage = "Длина строки должна быть от 3 до 150 символов")]
        //Check if word exist in database
        //[Remote(action: "CheckTranslationOfWordNameInSameWordInEnglish", controller: "TranslationOfWord", ErrorMessage = "Такой перевод в данном слове уже существует")]
        public string Name { get; set; }

        public long WordInEnglishId { get; set; }
    }

    public class UpdateTranslationOfWordViewModel
    {
        public long Id { get; set; }

        public long WordInEnglishId { get; set; }

        [Display(Name = "Перевод")]
        //validation of required field
        [Required(ErrorMessage = "Поле не может быть пустым")]
        //Validation for length of string
        [StringLength(150, MinimumLength = 3, ErrorMessage = "Длина строки должна быть от 3 до 150 символов")]
        //Check if word exist in database
        //[Remote(action: "CheckTranslationOfWordNameInSameWordInEnglish", controller: "TranslationOfWord", ErrorMessage = "Такой перевод в данном слове уже существует")]
        public string Name { get; set; }
    }

    public class ListOfCreateTranslationOfWordSeparateViewModel
    {
        public WordInEnglish WordInEnglish { get; set; }

        public List<CreateTranslationOfWordViewModel> Translations { get; set; }
    }
}