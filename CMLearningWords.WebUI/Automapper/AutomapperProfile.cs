using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using CMLearningWords.DataModels.Models;
using CMLearningWords.WebUI.Models;

namespace CMLearningWords.WebUI.Automapper
{
    public class AutomapperProfile : Profile
    {
        public AutomapperProfile()
        {
            //Mapping WordIntEnglish
            CreateMap<CreateWordInEnglishViewModel, WordInEnglish>().ReverseMap();
            CreateMap<WordInEnglish, CreateWordInEnglishViewModel>().ReverseMap();

            //Mapping TranslationOfWord
            CreateMap<CreateTranslationOfWordViewModel, TranslationOfWord>().ReverseMap();
            CreateMap<TranslationOfWord, CreateTranslationOfWordViewModel>().ReverseMap();

            //Mapping StagesOfMethod
            CreateMap<CreateStageOfMethodViewModel, StageOfMethod>().ReverseMap();
            CreateMap<StageOfMethod, CreateStageOfMethodViewModel>().ReverseMap();
        }
    }
}
