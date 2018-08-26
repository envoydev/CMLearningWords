using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using CMLearningWords.AccessToData.Repository.Interfaces;
using CMLearningWords.DataModels.Models;
using CMLearningWords.WebUI.Enums;
using CMLearningWords.WebUI.Extensions;
using CMLearningWords.WebUI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
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
            List<StageOfMethod> stages = StageOfMethodsContext.GetAllIQueryableWithInclude().ToList();
            stages.Insert(0, new StageOfMethod { Name = "Со всех стейджов", Id = 0 });
            GenerateTestYourselfViewModel model = new GenerateTestYourselfViewModel
            {
                AllStages = new SelectList(stages, "Id", "Name")
            };

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Index(GenerateTestYourselfViewModel model)
        {
            //Use for Titile in html
            ViewData["Title"] = "Список категорий";
            //Use for head in page
            ViewBag.HeadPageText = "Список категорий";

            int amountOfWords;

            if (!CountAllWordsOfStage(model.StageId, model.Number, out amountOfWords))
                ModelState.AddModelError("Number", $"Максимальное количесто слов в этом стейдже {amountOfWords}");


            if (model != null)
            {
                if (ModelState.IsValid)
                {
                    TempData["AmountOfWords"] = GetNumbers(model.Number, model.StageId);
                    TempData["StageId"] = model.StageId;

                    return RedirectToAction("TestPage", "TestYourself");
                }
            }

            List<StageOfMethod> stages = StageOfMethodsContext.GetAllIQueryableWithInclude().ToList();
            stages.Insert(0, new StageOfMethod { Name = "Со всех стейджов", Id = 0 });

            model.AllStages = new SelectList(stages, "Id", "Name", model.StageId);

            return View(model);
        }
        [HttpGet]
        public IActionResult TestPage()
        {
            //Use for Titile in html
            ViewData["Title"] = "Начало теста";
            //Use for head in page
            ViewBag.HeadPageText = "Начало теста";

            int[] arrayOfNumbers = TempData["AmountOfWords"] as int[];
            int stageId = (int)TempData["StageId"];

            List<WordInEnglish> words = new List<WordInEnglish>();

            if (stageId != 0)
            {
                long currentStageId = (long)stageId;
                words.AddRange(WordsInEnglishContext.FindWithInclude(w => w.StageOfMethodId == currentStageId, w => w.TranslationOfWords).ToList());
            }
            else
                words.AddRange(WordsInEnglishContext.GetAllIQueryableWithInclude(w => w.TranslationOfWords).ToList());

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
            //Use for Titile in html
            ViewData["Title"] = "Результат теста";
            
            int countCorrectWords = 0;

            if (words != null)
            {
                if (ModelState.IsValid)
                {
                    for (int i = 0; i < words.Count; i++)
                    {
                        words[i] = CompareResultWithTranslation(words[i]);

                        if (words[i].MadeMistake == ExeptionInTranslation.WithoutMistake)
                            countCorrectWords++;

                    }
                    //Use for head in page
                    ViewBag.HeadPageText = $"Ваш результат теста {countCorrectWords} из {words.Count}";

                    return View("ResultOfTest", words);
                }
            }
            return View(words);
        }

        public IActionResult ResultOfTest()
        {
            //Use for Titile in html
            ViewData["Title"] = "Результат теста";
            //Use for head in page
            ViewBag.HeadPageText = "Результат теста";

            return View();
        }

        private bool CountAllWordsOfStage(long stageId, int number, out int countedWords)
        {
            if (stageId == 0)
            {
                int allWords = WordsInEnglishContext.GetAllIQueryableWithInclude().Count();
                if (number > allWords)
                {
                    countedWords = allWords;
                    return false;
                }
                else
                {
                    countedWords = 0;
                    return true;
                }
            }

            int amountOfWords = WordsInEnglishContext.FindWithInclude(w => w.StageOfMethodId == stageId).Count();

            if (number > amountOfWords)
            {
                countedWords = amountOfWords;
                return false;
            }
            else
            {
                countedWords = 0;
                return true;
            }
        }

        private CreatedTestYourselfViewModel CompareResultWithTranslation(CreatedTestYourselfViewModel oneItem)
        {
            WordInEnglish word = WordsInEnglishContext.FindWithInclude(w => w.Id == oneItem.Id, w => w.TranslationOfWords).FirstOrDefault();

            oneItem.Name = word.Name;
            oneItem.TranslationOfWords = word.TranslationOfWords;
            oneItem.MadeMistake = ExeptionInTranslation.WithMistake;

            for (int i = 0; i < word.TranslationOfWords.Count(); i++)
            {
                if (word.TranslationOfWords[i].Name == oneItem.NameOfCurrentInputTranslation.Trim())
                {
                    oneItem.MadeMistake = ExeptionInTranslation.WithoutMistake;
                    break;
                }
            }
            return oneItem;
        }

        //Generate random numbers depending on amount of words in table and parametr
        private int[] GetNumbers(int value, long stageId)
        {
            Random rand = new Random();
            int CountAllWords = 0;
            if (stageId == 0)
            {
                CountAllWords = (WordsInEnglishContext.GetAllIQueryableWithInclude()).Count();
            }
            else
            {
                CountAllWords = (WordsInEnglishContext.FindWithInclude(w => w.StageOfMethodId == stageId)).Count();
            }
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