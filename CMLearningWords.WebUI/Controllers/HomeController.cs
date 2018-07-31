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

namespace CMLearningWords.WebUI.Controllers
{
    public class HomeController : Controller
    {
        private readonly IWordInEnglishRepository WordsInEnglishContext; // context of WordsInEnglish
        private readonly IMapper Mapper; // Mapper for ViewModels

        //Constructor with parameters
        public HomeController(IWordInEnglishRepository wordsInEnglishContext,
                                IMapper mapper)
        {
            WordsInEnglishContext = wordsInEnglishContext;
            Mapper = mapper;
        }
        public IActionResult Index()
        {
            List<WordInEnglish> words = WordsInEnglishContext.GetAllIQueryableWithInclude(a => a.TranslationOfWords).ToList();
            return View(words);
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
    }
}
