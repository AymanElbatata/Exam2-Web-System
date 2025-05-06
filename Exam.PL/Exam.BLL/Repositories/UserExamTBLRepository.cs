using Exam.BLL.Interfaces;
using Exam.DAL.Contexts;
using Exam.DAL.Entities;

namespace Exam.BLL.Repositories
{
    public class UserExamTBLRepository : GenericRepository<UserExamTBL>, IUserExamTBLRepository
    {
        private readonly ExamDbContext _context;

        public UserExamTBLRepository(ExamDbContext context) :base(context)
        {
            _context = context;
        }
        public int AddAndReturnNewRowID(UserExamTBL obj)
        {
            var NewRow = _context.UserExamTBLs.Add(obj);
            _context.SaveChanges();
            return obj.ID;
        }
    }
}
