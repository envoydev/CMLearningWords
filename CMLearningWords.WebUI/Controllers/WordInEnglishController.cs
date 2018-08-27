using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using CMLearningWords.AccessToData.Repository.Interfaces;
using CMLearningWords.DataModels.Models;
using CMLearningWords.WebUI.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace CMLearningWords.WebUI.Controllers
{
    public class WordInEnglishController : Controller
    {
        private readonly IWordInEnglishRepository WordsInEnglishContext; // context of WordsInEnglish
        private readonly ITranslationOfWordRepository TranslationsOfWordContext; // context of TranslationsOfWord
        private readonly IStageOfMethodRepository StageOfMethodsContext; // context of StageOfMethods
        private readonly IMapper Mapper; // Mapper for ViewModels

        //Constructor with parameters
        public WordInEnglishController(IWordInEnglishRepository wordsInEnglishContext, 
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
        public IActionResult Create()
        {
            CreateWordInEnglishViewModel newWord = new CreateWordInEnglishViewModel
            {
                StagesOfMethod = new SelectList(StageOfMethodsContext.GetAllIQueryableWithInclude().ToList(), "Id", "Name")
            };
            //Use for Titile in html
            ViewData["Title"] = "Добавить слово";
            //Use for head in page
            ViewBag.HeadPageText = "Добавить слово и перевод";
            //Render to View Create
            return View(newWord);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CreateWordInEnglishViewModel word)
        {
            //Use for Titile in html
            ViewData["Title"] = "Добавить слово";
            //Use for head in page
            ViewBag.HeadPageText = "Добавить слово и перевод";
            //Check newStage on null
            if (word != null)
            {
                // Server validation
                if (ModelState.IsValid)
                {
                    //Work with data
                    WordInEnglish currentWord = Mapper.Map<CreateWordInEnglishViewModel, WordInEnglish>(word);
                    currentWord.Name = currentWord.Name.Trim();
                    foreach(var translation in word.Translations)
                    {
                        translation.Name = translation.Name.Trim();
                    }
                    await WordsInEnglishContext.Add(currentWord);
                    //Add many translations, user method "GetTranslationsFromStringArray" to make new object of TranslationOfWord type
                    await TranslationsOfWordContext.AddMany(GetTranslationsFromListAndGiveIdOfWordInEnglish(word.Translations, currentWord.Id));
                    //ViewBags for "_Success" view
                    ViewBag.SuccessText = "Слово успешно добавленно";
                    ViewBag.MethodRedirect = "Index";
                    ViewBag.ControllerRedirect = "Home";
                    //Render user on temporary view "Views/Shared/_Success"
                    return PartialView("_Success");
                }
            }
            //Update SelectList for html data and and all stages in it with current word
            word.StagesOfMethod = new SelectList(StageOfMethodsContext.GetAllIQueryableWithInclude().ToList(), "Id", "Name", word.StageOfMethodId);
            //If word is null or validation was false
            return View(word);
        }

        [HttpGet]
        public IActionResult Edit(long? Id)
        {
            //Use for Titile in html
            ViewData["Title"] = "Редактировать слово";
            //Use for head in page
            ViewBag.HeadPageText = "Редактировать слово и перевод";

            if (Id == null)
                return BadRequest();

            long currentId = Id ?? 0;

            WordInEnglish word = WordsInEnglishContext.FindWithInclude(w => w.Id == currentId, w => w.TranslationOfWords).FirstOrDefault();
            UpdateWordInEnglishViewModel model = Mapper.Map<WordInEnglish, UpdateWordInEnglishViewModel>(word);
            model.Translations = Mapper.Map<List<TranslationOfWord>, List<UpdateTranslationOfWordViewModel>>(word.TranslationOfWords);
            model.StagesOfMethod = new SelectList(StageOfMethodsContext.GetAllIQueryableWithInclude().ToList(), "Id", "Name", word.StageOfMethodId);

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(UpdateWordInEnglishViewModel word)
        {
            if (word != null)
            {
                // Server validation
                if (ModelState.IsValid)
                {
                    //Work with data
                    word.Name = word.Name.Trim();
                    foreach (var translation in word.Translations)
                    {
                        translation.Name = translation.Name.Trim();
                    }
                    WordInEnglish currentWord = Mapper.Map<UpdateWordInEnglishViewModel, WordInEnglish>(word);
                    //currentWord.TranslationOfWords = Mapper.Map<List<UpdateTranslationOfWordViewModel>, List<TranslationOfWord>>(word.Translations);

                    await WordsInEnglishContext.Update(currentWord);

                    List<TranslationOfWord> translations = Mapper.Map<List<UpdateTranslationOfWordViewModel>, List<TranslationOfWord>>(word.Translations);

                    foreach (var translation in translations)
                    {
                        await TranslationsOfWordContext.Update(translation);
                    }

                    //ViewBags for "_Success" view
                    ViewBag.SuccessText = "Слово успешно отредактированно";
                    ViewBag.MethodRedirect = "Index";
                    ViewBag.ControllerRedirect = "Home";
                    //Render user on temporary view "Views/Shared/_Success"
                    return PartialView("_Success");
                }
            }

            word.StagesOfMethod = new SelectList(StageOfMethodsContext.GetAllIQueryableWithInclude().ToList(), "Id", "Name", word.StageOfMethodId);

            return View(word);
        }

        //Chech Unique data for "Name"
        [AcceptVerbs("Get", "Post")]
        public IActionResult CheckWordInEnglishName(string name)
        {
            //Get item by Name
            WordInEnglish WordInEnglish = WordsInEnglishContext.FindWithInclude(a => a.Name == name).FirstOrDefault();
            //Check
            if (WordInEnglish == null)
                return Json(true); //if true current word name doesen`t exists in database
            return Json(false); //if false current word name exists in database
        }

        //Chech Unique data for "Name" and for name of current id
        [AcceptVerbs("Get", "Post")]
        public IActionResult CompareWordInEnglishName(string name, long id)
        {
            //Get category by Name and by id
            WordInEnglish currentWordInEnglishWithId = WordsInEnglishContext.FindWithInclude(a => a.Name == name && a.Id == id).FirstOrDefault();
            //Get category by Name
            WordInEnglish searchName = WordsInEnglishContext.FindWithInclude(a => a.Name == name && a.Id != id).FirstOrDefault();
            //Check
            var result = Json(true);
            if (currentWordInEnglishWithId != null)
                result = Json(true); //current "Name" with current "id" exist in DB
            else if (searchName != null)
                result = Json(false); //current "Name" exist in DB but without current id
            return result; //return result
        }


        //Close all connections
        protected override void Dispose(bool disposing)
        {
            WordsInEnglishContext.Dispose();
            TranslationsOfWordContext.Dispose();
            StageOfMethodsContext.Dispose();
            base.Dispose(disposing);
        }

        //Method which converts word.Translations to List of TranslationOfWord(Name and WordInEnglishId must have)
        private List<TranslationOfWord> GetTranslationsFromListAndGiveIdOfWordInEnglish(IEnumerable<CreateTranslationOfWordViewModel> arr, long idOfWordInEnglish)
        {
            List<TranslationOfWord> translations = Mapper.Map<IEnumerable<CreateTranslationOfWordViewModel>, List<TranslationOfWord>>(arr);
            for(int i = 0; i < translations.Count(); i++)
            {
                translations[i].WordInEnglishId = idOfWordInEnglish;
            }
            return translations;
        }
    }
}