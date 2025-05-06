using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Exam.DAL.BaseEntity;
using Microsoft.AspNetCore.Identity;

namespace Exam.BLL.Interfaces
{
    public interface IUnitOfWork
    {
        IAnswerTBLRepository AnswerTBLRepository { get; }
        IExamTBLRepository ExamTBLRepository { get; }
        IQuestionTBLRepository QuestionTBLRepository { get; }
        IUserExamDetailTBLRepository UserExamDetailTBLRepository { get; }
        IUserExamTBLRepository UserExamTBLRepository { get; }
        SignInManager<AppUser> SignInManager { get; }
        RoleManager<AppRole> RoleManager { get; }
        UserManager<AppUser> UserManager { get; }
    }
}
