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
    public class StageOfMethodController : Controller
    {
        private readonly IStageOfMethodRepository StageOfMethodsContext; // context of StageOfMethods
        private readonly IMapper Mapper; // Mapper for ViewModels

        //Constructor with parameters
        public StageOfMethodController(IStageOfMethodRepository stageOfMethodsContext,
                                        IMapper mapper)
        {
            StageOfMethodsContext = stageOfMethodsContext;
            Mapper = mapper;
        }

        public IActionResult Index()
        {
            //Use for Titile in html
            ViewData["Title"] = "Создать стейдж";
            //Use for head in page
            ViewBag.HeadPageText = "Создать стейдж";
            //Get all stages from database
            return View(StageOfMethodsContext.GetAllIQueryableWithInclude().ToList());
        }

        [HttpGet]
        public IActionResult Create()
        {
            //Use for Titile in html
            ViewData["Title"] = "Создать стейдж";
            //Use for head in page
            ViewBag.HeadPageText = "Создать стейдж";
            //Go to create view
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CreateStageOfMethodViewModel newStage)
        {
            //Use for Titile in html
            ViewData["Title"] = "Создать стейдж";
            //Use for head in page
            ViewBag.HeadPageText = "Создать стейдж";
            //Check newStage on null
            if (newStage != null)
            {
                //Sever validation
                if (ModelState.IsValid)
                {
                    //Work with data
                    StageOfMethod currentStage = Mapper.Map<CreateStageOfMethodViewModel, StageOfMethod>(newStage);
                    await StageOfMethodsContext.Add(currentStage);
                    //ViewBags for "_Success" view
                    ViewBag.SuccessText = "Стейдж успешно добавлен";
                    ViewBag.MethodRedirect = "Index";
                    ViewBag.ControllerRedirect = "StageOfMethod";
                    //Render user on temporary view "Views/Shared/_Success"
                    return PartialView("_Success");
                }
            }
            //If newStage is null or validation was false
            return View(newStage);
        }

        //Method for checking the Name of current Stage
        public IActionResult CheckNameOfStage(string name)
        {
            StageOfMethod stage = StageOfMethodsContext.FindWithInclude(s => s.Name == name).FirstOrDefault();
            if (stage == null)
                return Json(true); //If true current name doesn`t exist in database
            return Json(false); //If false current name exists in database
        }

        //Close all connections
        protected override void Dispose(bool disposing)
        {
            StageOfMethodsContext.Dispose();
            base.Dispose(disposing);
        }
    }
}