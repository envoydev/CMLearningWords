using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using CMLearningWords.AccessToData.Repository.Interfaces;
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
            return View();
        }

        //Close all connections
        protected override void Dispose(bool disposing)
        {
            StageOfMethodsContext.Dispose();
            base.Dispose(disposing);
        }
    }
}