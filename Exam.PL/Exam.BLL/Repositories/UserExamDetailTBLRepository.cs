using Exam.BLL.Interfaces;
using Exam.DAL.Contexts;
using Exam.DAL.Entities;

namespace Exam.BLL.Repositories
{
    public class UserExamDetailTBLRepository : GenericRepository<UserExamDetailTBL>, IUserExamDetailTBLRepository
    {
        private readonly ExamDbContext _context;

        public UserExamDetailTBLRepository(ExamDbContext context) :base(context)
        {
            _context = context;
        }

    }
}
