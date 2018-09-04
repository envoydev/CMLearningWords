using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

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
