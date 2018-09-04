using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CMLearningWords.DataModels.Models
{
    public class StageOfMethod
    {
        //Id of current method
        [Key]
        [Required]
        public long Id { get; set; }

        //Name of current method
        [Required]
        [Column(TypeName = "nvarchar(150)")]
        public string Name { get; set; }
    }
}
