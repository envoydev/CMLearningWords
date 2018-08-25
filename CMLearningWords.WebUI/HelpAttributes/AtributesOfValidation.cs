using CMLearningWords.WebUI.Models;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;

namespace CMLearningWords.WebUI.HelpAttributes
{

    public class MinValueAttribute : ValidationAttribute
    {
        private readonly int _number;
        public MinValueAttribute(int number)
        {
            _number = number;
        }

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            GenerateTestYourselfViewModel model = (GenerateTestYourselfViewModel)validationContext.ObjectInstance;
            if (model.Number < _number)
                return new ValidationResult(this.ErrorMessage);
            else
                return ValidationResult.Success;
        }
    }
}
