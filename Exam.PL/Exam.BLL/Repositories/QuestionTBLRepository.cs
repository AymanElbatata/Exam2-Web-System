using Exam.BLL.Interfaces;
using Exam.DAL.Contexts;
using Exam.DAL.Entities;

namespace Exam.BLL.Repositories
{
    public class QuestionTBLRepository : GenericRepository<QuestionTBL>, IQuestionTBLRepository
    {
        private readonly ExamDbContext _context;

        public QuestionTBLRepository(ExamDbContext context) :base(context)
        {
            _context = context;
        }

    }
}
