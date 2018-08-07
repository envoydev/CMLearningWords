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
        //private List<CreatedTestYourselfViewModel> List { get; set; } = new List<CreatedTestYourselfViewModel>();

        public IActionResult Index()
        {
            return View();
        }


        [HttpGet]
        public IActionResult TestPage()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult TestPage(GenerateTestYourselfViewModel model)
        {   
            if (model != null)
            {
                if (ModelState.IsValid)
                {
                    Random rand = new Random();
                    List<WordInEnglish> words = WordsInEnglishContext.GetAllIQueryableWithInclude(w => w.StageOfMethod, w => w.TranslationOfWords).ToList();
                    List<WordInEnglish> currentWords = new List<WordInEnglish>();

                    for (int i = 0; i < model.Number; i++)
                    {
                        int rnd = rand.Next(1, words.Count());
                        currentWords.Add(words[rnd]);
                        words.Remove(words[rnd]);
                    }
                    
                    List<CreatedTestYourselfViewModel> mapped = Mapper.Map<List<WordInEnglish>, List<CreatedTestYourselfViewModel>>(currentWords);
                    return View(mapped);
                }
            }
            return View("Index", model);
        }

        [HttpGet]
        public IActionResult ResultOfTest()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult ResultOfTest(List<CreatedTestYourselfViewModel> words)
        {
            if (words != null)
            {
                if (ModelState.IsValid)
                {
                    for (int i = 0; i < words.Count; i++)
                    {
                        if (CompareResultWithTranslation(words[i]))
                        {
                            words[i].MadeMistake = true;
                        }
                    }
                    return View(words);
                }
            }
            return View("TestPage", words);
        }

        //If find a compare returns true
        private bool CompareResultWithTranslation(CreatedTestYourselfViewModel oneItem)
        {
            WordInEnglish word = WordsInEnglishContext.FindWithInclude(w => w.Id == oneItem.Id, w => w.TranslationOfWords).FirstOrDefault();
            bool result = false;
            for (int i = 0; i < word.TranslationOfWords.Count(); i++)
            {
                if (word.TranslationOfWords[i].Name.Contains(oneItem.NameOfCurrentInputTranslation))
                {
                    result = true;
                    break;
                }
            }
            return result;
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