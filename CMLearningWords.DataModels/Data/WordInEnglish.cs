using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CMLearningWords.DataModels.Models
{
    public class WordInEnglish : Word
    {
        //Filed for right gramma written word
        [Column(TypeName = "nvarchar(150)")]
        public string RightWord { get; set; }

        //Get stage id of current word
        [Required]
        public long StageOfMethodId { get; set; }
        public StageOfMethod StageOfMethod { get; set; }

        //List of exceptions in translations
        public List<TranslationOfWord> TranslationOfWords { get; set; }

        public WordInEnglish()
        {
            TranslationOfWords = new List<TranslationOfWord>();
        }
    }
}
