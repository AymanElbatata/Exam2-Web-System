using Exam.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Exam.BLL.Interfaces
{
    public interface IUserExamTBLRepository : IGenericRepository<UserExamTBL>
    {
        int AddAndReturnNewRowID(UserExamTBL obj);

    }
}
