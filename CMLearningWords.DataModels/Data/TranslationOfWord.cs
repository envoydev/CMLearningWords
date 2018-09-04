using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CMLearningWords.DataModels.Models
{
    public class TranslationOfWord : Word
    {
        //Id of word from table of english words
        [Required]
        public long WordInEnglishId { get; set; }
        public WordInEnglish WordInEnglish { get; set; }

        //Rigth word of current bad translation
        [Column(TypeName = "nvarchar(150)")]
        public string RightTranslation { get; set; }

        //Show us if this tranlstion need
        //If this value = true, we must remove current translation. If false, stay without changes
        public bool MustBeRemove { get; set; }
    }
}
