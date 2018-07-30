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
            return View();
        }

        [HttpPost]
        public IActionResult Create(CreateWordInEnglishViewModel word)
        {
            return View();
        }

        //Chech Unique data for "Name"
        [AcceptVerbs("Get", "Post")]
        public IActionResult CheckWordInEnglishName(string name)
        {
            //Get item by Name
            WordInEnglish WordInEnglish = WordsInEnglishContext.FindWithInclude(a => a.Name == name).FirstOrDefault();
            //Check
            if (WordInEnglish != null)
                return Json(false); //if false current "Name" exist in DB
            return Json(true); //if true current "Name" not exist in DB
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
    }
}