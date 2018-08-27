using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using CMLearningWords.AccessToData.Repository.Interfaces;
using CMLearningWords.DataModels.Models;
using CMLearningWords.WebUI.Models;
using Microsoft.AspNetCore.Mvc;

namespace CMLearningWords.WebUI.Controllers
{
    public class TranslationOfWordController : Controller
    {
        private readonly IWordInEnglishRepository WordsInEnglishContext; // context of WordsInEnglish
        private readonly ITranslationOfWordRepository TranslationsOfWordContext; // context of TranslationsOfWord
        private readonly IStageOfMethodRepository StageOfMethodsContext; // context of StageOfMethods
        private readonly IMapper Mapper; // Mapper for ViewModels

        //Constructor with parameters
        public TranslationOfWordController(IWordInEnglishRepository wordsInEnglishContext,
                                        ITranslationOfWordRepository translationsOfWordContext,
                                        IStageOfMethodRepository stageOfMethodsContext,
                                        IMapper mapper)
        {
            WordsInEnglishContext = wordsInEnglishContext;
            TranslationsOfWordContext = translationsOfWordContext;
            StageOfMethodsContext = stageOfMethodsContext;
            Mapper = mapper;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public IActionResult Create(long? id)
        {
            //Use for Titile in html
            ViewData["Title"] = "Добавить перевод";
            //Use for head in page
            ViewBag.HeadPageText = "Добавить перевод";
            //Render to View Create
            if (id == null)
                return BadRequest();

            long currentId = id ?? 0;

            WordInEnglish word = WordsInEnglishContext.FindWithInclude(w => w.Id == currentId, w => w.StageOfMethod).FirstOrDefault();

            if (word == null)
                return BadRequest();

            ListOfCreateTranslationOfWordSeparateViewModel model = new ListOfCreateTranslationOfWordSeparateViewModel()
            {
                WordInEnglish = word,
                Translations = new List<CreateTranslationOfWordViewModel>()
                {
                    new CreateTranslationOfWordViewModel { WordInEnglishId = word.Id },
                }
            };

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Create(ListOfCreateTranslationOfWordSeparateViewModel model)
        {
            if (model != null)
            {
                if (ModelState.IsValid)
                {
                    foreach (var translation in model.Translations)
                    {
                        translation.Name = translation.Name.Trim();
                    }

                    List<TranslationOfWord> translations = Mapper.Map<List<CreateTranslationOfWordViewModel>, List<TranslationOfWord>>(model.Translations);
                    await TranslationsOfWordContext.AddMany(translations);

                    //ViewBags for "_Success" view
                    ViewBag.SuccessText = "Перевод успешно добавлен";
                    ViewBag.MethodRedirect = "Index";
                    ViewBag.ControllerRedirect = "Home";
                    //Render user on temporary view "Views/Shared/_Success"
                    return PartialView("_Success");
                }
            }
            return View();
        }

        [HttpPost]
        [ActionName("Delete")]
        public async Task<IActionResult> Delete(long? id)
        {
            if (id == null)
                return BadRequest();

            long currentId = id ?? 0;

            await TranslationsOfWordContext.RemoveById(currentId);

            return Ok();
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