using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using CMLearningWords.AccessToData.Repository.Interfaces;
using CMLearningWords.DataModels.Models;
using CMLearningWords.WebUI.Extensions;
using CMLearningWords.WebUI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ViewFeatures;

namespace CMLearningWords.WebUI.Controllers
{
    public class TestYourselfController : Controller
    {
        private readonly IWordInEnglishRepository WordsInEnglishContext; // context of WordsInEnglish
        private readonly ITranslationOfWordRepository TranslationsOfWordContext; // context of TranslationsOfWord
        private readonly IStageOfMethodRepository StageOfMethodsContext; // context of StageOfMethods
        private readonly IMapper Mapper; // Mapper for ViewModels

        public TestYourselfController(IWordInEnglishRepository wordsInEnglishContext,
                                        ITranslationOfWordRepository translationsOfWordContext,
                                        IStageOfMethodRepository stageOfMethodsContext,
                                        IMapper mapper)
        {
            WordsInEnglishContext = wordsInEnglishContext;
            TranslationsOfWordContext = translationsOfWordContext;
            StageOfMethodsContext = stageOfMethodsContext;
            Mapper = mapper;
        }

        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Index(GenerateTestYourselfViewModel model)
        {
            if (model != null)
            {
                if (ModelState.IsValid)
                {
                    return RedirectToAction("TestPage", "TestYourself", new { arrayOfNumbers = GetNumbers(model.Number) });
                }
            }
            return View(model);
        }

        [HttpGet]
        public IActionResult TestPage(int[] arrayOfNumbers)
        {
            List<WordInEnglish> words = WordsInEnglishContext.GetAllIQueryableWithInclude(w => w.TranslationOfWords).ToList();
            List<WordInEnglish> currentWords = new List<WordInEnglish>();

            for (int i = 0; i < arrayOfNumbers.Length; i++)
            {
                currentWords.Add(words[arrayOfNumbers[i]]);
            }

            List<CreatedTestYourselfViewModel> mapped = Mapper.Map<List<WordInEnglish>, List<CreatedTestYourselfViewModel>>(currentWords);
            return View(mapped);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult TestPage(List<CreatedTestYourselfViewModel> words)
        {
            if (words != null)
            {
                if (ModelState.IsValid)
                {
                    for (int i = 0; i < words.Count; i++)
                    {
                        if (CompareResultWithTranslation(words[i]))
                            words[i].MadeMistake = true;
                    }
                    return View("ResultOfTest", words);
                }
            }
            return View(words);
        }

        public IActionResult ResultOfTest()
        {
            return View();
        }

        //If find a compare returns true
        private bool CompareResultWithTranslation(CreatedTestYourselfViewModel oneItem)
        {
            WordInEnglish word = WordsInEnglishContext.FindWithInclude(w => w.Id == oneItem.Id, w => w.TranslationOfWords).FirstOrDefault();
            bool result = false;
            for (int i = 0; i < word.TranslationOfWords.Count(); i++)
            {
                if (word.TranslationOfWords[i].Name == oneItem.NameOfCurrentInputTranslation.Trim())
                {
                    result = true;
                    break;
                }
            }
            return result;
        }

        //Generate random numbers depending on amount of words in table and parametr
        private int[] GetNumbers(int value)
        {
            Random rand = new Random();
            int CountAllWords = (WordsInEnglishContext.GetAllIQueryableWithInclude()).Count();
            int[] arrayValues = new int[value];

            for (int i = 0; i < value; i++)
            {
                int rnd = rand.Next(0, CountAllWords);

                if (!arrayValues.Contains(rnd))
                    arrayValues[i] = rnd;
                else
                    i--;
            }
            return arrayValues;
        }

        //Close all connections
        protected override void Dispose(bool disposing)
        {
            WordsInEnglishContext.Dispose();
            TranslationsOfWordContext.Dispose();
            StageOfMethodsContext.Dispose();
            base.Dispose(disposing);
        }
    }
}