using Exam.BLL.Interfaces;
using Exam.DAL.Contexts;
using Exam.DAL.Entities;

namespace Exam.BLL.Repositories
{
    public class AnswerTBLRepository : GenericRepository<AnswerTBL>, IAnswerTBLRepository
    {
        private readonly ExamDbContext _context;

        public AnswerTBLRepository(ExamDbContext context) :base(context)
        {
            _context = context;
        }

    }
}
