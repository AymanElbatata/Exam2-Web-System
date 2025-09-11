using Exam.BLL.Interfaces;
using Exam.DAL.BaseEntity;
using Microsoft.AspNetCore.Identity;

namespace Exam.BLL.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        public IAnswerTBLRepository AnswerTBLRepository { get; }
        public IExamTBLRepository ExamTBLRepository { get; }
        public IQuestionTBLRepository QuestionTBLRepository { get; }
        public IUserExamDetailTBLRepository UserExamDetailTBLRepository { get; }
        public IUserExamTBLRepository UserExamTBLRepository { get; }
        public SignInManager<AppUser> SignInManager { get; }
        public RoleManager<AppRole> RoleManager { get; }
        public UserManager<AppUser> UserManager { get; }
        public IMySPECIALGUID MySPECIALGUID { get; }

        public UnitOfWork(IAnswerTBLRepository AnswerTBLRepository, IExamTBLRepository ExamTBLRepository,
            IQuestionTBLRepository QuestionTBLRepository, IUserExamDetailTBLRepository UserExamDetailTBLRepository,
            IUserExamTBLRepository UserExamTBLRepository, SignInManager<AppUser> SignInManager,
            RoleManager<AppRole> RoleManager, UserManager<AppUser> UserManager, IMySPECIALGUID MySPECIALGUID
            )
        {
            this.AnswerTBLRepository = AnswerTBLRepository;
            this.ExamTBLRepository = ExamTBLRepository;
            this.QuestionTBLRepository = QuestionTBLRepository;
            this.UserExamDetailTBLRepository = UserExamDetailTBLRepository;
            this.UserExamTBLRepository = UserExamTBLRepository;
            this.SignInManager = SignInManager;
            this.RoleManager = RoleManager;
            this.UserManager = UserManager;
            this.MySPECIALGUID = MySPECIALGUID;
        }
    }
}
