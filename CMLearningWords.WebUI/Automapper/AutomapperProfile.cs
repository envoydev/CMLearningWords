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
            CreateMap<WordInEnglish, CreateWordInEnglishViewModel>().ReverseMap();
            CreateMap<CreateWordInEnglishViewModel, WordInEnglish>().ReverseMap();
            CreateMap<WordInEnglish, UpdateWordInEnglishViewModel>().ReverseMap();
            CreateMap<UpdateWordInEnglishViewModel, WordInEnglish>().ReverseMap();
            CreateMap<WordInEnglish, CreatedTestYourselfViewModel>().ReverseMap();
            CreateMap<CreatedTestYourselfViewModel, WordInEnglish>().ReverseMap();


            //Mapping TranslationOfWord
            CreateMap<CreateTranslationOfWordViewModel, TranslationOfWord>().ReverseMap();
            CreateMap<TranslationOfWord, CreateTranslationOfWordViewModel>().ReverseMap();
            CreateMap<UpdateTranslationOfWordViewModel, TranslationOfWord>().ReverseMap();
            CreateMap<TranslationOfWord, UpdateTranslationOfWordViewModel>().ReverseMap();

            //Mapping StagesOfMethod
            CreateMap<CreateStageOfMethodViewModel, StageOfMethod>().ReverseMap();
            CreateMap<StageOfMethod, CreateStageOfMethodViewModel>().ReverseMap();
        }
    }
}
