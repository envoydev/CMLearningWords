using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CMLearningWords.WebUI.Models
{
    public class CreateStageOfMethodViewModel
    {
        public long Id { get; set; }

        [Display(Name = "Название стейджа")]
        [Required(ErrorMessage = "Название обязательно для заполнения")]
        [StringLength(150, MinimumLength = 2, ErrorMessage = "Длина строки должна быть от 1 до 150 символов")]
        [Remote(action: "CheckNameOfStage", controller: "StageOfMethod", ErrorMessage = "Такой стейдж уже существует")]
        public string Name { get; set; }
    }
}
