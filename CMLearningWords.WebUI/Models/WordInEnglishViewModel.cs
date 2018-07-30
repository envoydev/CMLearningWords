using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CMLearningWords.DataModels.Models;

namespace CMLearningWords.WebUI.Models
{
    public class CreateWordInEnglishViewModel
    {
        public List<CreateTranslationOfWordViewModel> TranslationOfWord { get; set; }
    }
}
