using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
