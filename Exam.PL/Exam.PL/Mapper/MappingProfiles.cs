using AutoMapper;
using Exam.DAL.Entities;
using Exam.PL.Models;
using Microsoft.AspNetCore.Identity;

namespace Exam.PL.Mapper
{
    public class MappingProfiles : Profile
    {
        public MappingProfiles()
        {
            CreateMap<ExamTBL, ExamTBLViewModel>().ReverseMap();
            CreateMap<AnswerTBL, AnswerTBLViewModel>().ReverseMap();
            CreateMap<QuestionTBL, QuestionTBLViewModel>().ReverseMap();
            CreateMap<UserExamTBL, UserExamTBLViewModel>().ReverseMap();
            CreateMap<UserExamDetailTBL, UserExamDetailTBLViewModel>().ReverseMap();
        }
    }
}
