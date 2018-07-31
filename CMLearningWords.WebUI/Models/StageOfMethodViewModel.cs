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

        [Required(ErrorMessage = "Название обязательно для заполнения")]
        [Display(Name = "Название")]
        [Remote(action: "CheckNameOfStage", controller: "StageOfMethod", ErrorMessage = "Такой стейдж уже существует")]
        public string Name { get; set; }
    }
}
