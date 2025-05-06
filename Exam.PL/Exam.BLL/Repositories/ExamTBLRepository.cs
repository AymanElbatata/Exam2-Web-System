using Exam.BLL.Interfaces;
using Exam.DAL.Contexts;
using Exam.DAL.Entities;

namespace Exam.BLL.Repositories
{
    public class ExamTBLRepository : GenericRepository<ExamTBL>, IExamTBLRepository
    {
        private readonly ExamDbContext _context;

        public ExamTBLRepository(ExamDbContext context) :base(context)
        {
            _context = context;
        }

    }
}
