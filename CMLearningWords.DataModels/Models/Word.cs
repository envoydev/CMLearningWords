using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CMLearningWords.DataModels.Models
{
    public abstract class Word
    {
        //Id of word
        [Key]
        [Required]
        public long Id { get; set; }
        //Name(value) of word
        [Required]
        [Column(TypeName = "nvarchar(150)")]
        public string Name { get; set; }
        //Show if we get exception in this.Name. true = we have exception, false = we haven`t. 
        [Required]
        public bool ExeptionInWord { get; set; }
    }
}
