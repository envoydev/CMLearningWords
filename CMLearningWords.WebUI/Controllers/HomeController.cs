using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using CMLearningWords.WebUI.Models;
using CMLearningWords.AccessToData.Repository.Interfaces;
using AutoMapper;
using CMLearningWords.DataModels.Models;
using CMLearningWords.WebUI.Enums;
using System.Text.RegularExpressions;

namespace CMLearningWords.WebUI.Controllers
{
    public class HomeController : Controller
    {
        private readonly IWordInEnglishRepository WordsInEnglishContext; // context of WordsInEnglish
        private readonly IStageOfMethodRepository StageOfMethodContext; // context of StageOfMethod
        private readonly ITranslationOfWordRepository TranslationsOfWordContext; // context of StageOfMethod
        private readonly IMapper Mapper; // Mapper for ViewModels

        //Constructor with parameters
        public HomeController(IWordInEnglishRepository wordsInEnglishContext,
                              IStageOfMethodRepository stageOfMethodContext,
                              ITranslationOfWordRepository translationsOfWordContext,
                                IMapper mapper)
        {
            this.WordsInEnglishContext = wordsInEnglishContext;
            this.StageOfMethodContext = stageOfMethodContext;
            this.TranslationsOfWordContext = translationsOfWordContext;
            this.Mapper = mapper;
        }
        //Index view with filters
        public IActionResult Index(long? stage, string nameOfWord, SortIndexHome sortOrder = SortIndexHome.WordInEnglishAsc)
        {
            //Get data from database
            IQueryable<StageOfMethod> stagesOfMethod = StageOfMethodContext.GetAllIQueryableWithInclude();
            IQueryable<WordInEnglish> wordsInEnglish = WordsInEnglishContext.GetAllIQueryableWithInclude(w => w.StageOfMethod, w => w.TranslationOfWords);
            //Filer by StageOfMethod
            if (stage != null && stage != 0)
                wordsInEnglish = wordsInEnglish.Where(w => w.StageOfMethodId == stage);
            //Search word by eglish word or by his translations
            if(!String.IsNullOrEmpty(nameOfWord))
            {
                if (!Regex.IsMatch(nameOfWord, @"\P{IsCyrillic}"))
                    wordsInEnglish = wordsInEnglish.Where(w => w.Name.Contains(nameOfWord));
                else
                    wordsInEnglish = wordsInEnglish.Where(w => w.Name.Contains(nameOfWord));
            }
            //Sorting by Name of word in english and by name of stage
            //TODO add to html this possibility
            switch (sortOrder)
            {
                case SortIndexHome.WordInEnglishAsc:
                    wordsInEnglish = wordsInEnglish.OrderBy(w => w.Name);
                    break;
                case SortIndexHome.WordInEnglishDesc:
                    wordsInEnglish = wordsInEnglish.OrderByDescending(w => w.Name);
                    break;
                case SortIndexHome.StageOfMethodAsc :
                    wordsInEnglish = wordsInEnglish.OrderBy(w => w.StageOfMethod.Name);
                    break;
                case SortIndexHome.StageOfMethodDesc :
                    wordsInEnglish = wordsInEnglish.OrderByDescending(w => w.StageOfMethod.Name);
                    break;
            }
            //Add sotr add filter with data from database to view model
            IndexHomeViewModel model = new IndexHomeViewModel
            {
                Sorting = new SortIndexHomeViewModel(sortOrder),
                Filters = new FilterIndexHomeViewModel(stagesOfMethod.ToList(), stage, nameOfWord),
                WordsInEnglish = wordsInEnglish
            };

            return View(model);
        }

        public IActionResult About()
        {
            ViewData["Message"] = "Your application description page.";

            return View();
        }

        public IActionResult Contact()
        {
            ViewData["Message"] = "Your contact page.";

            return View();
        }

        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        //Close all connections
        protected override void Dispose(bool disposing)
        {
            WordsInEnglishContext.Dispose();
            base.Dispose(disposing);
        }

        //Test method
        //private IQueryable<WordInEnglish> GetWordsInEnglishByTranslationName(string name)
        //{
        //    IQueryable<TranslationOfWord> listOfTrans = TranslationsOfWordContext.FindWithInclude(t => t.Name.Contains(name), t => t.WordInEnglish);
        //    IQueryable<WordInEnglish> list = null;
        //    for (int i = 0; i < listOfTrans.Count(); i++)
        //    {
        //        list.Add(listOfTrans[i].WordInEnglish);
        //    }
        //    return list;
        //}
    }
}
