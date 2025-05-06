using Exam.DAL.BaseEntity;
using Exam.DAL.Entities;

namespace Exam.PL.Models
{
    public class UserExamTBLViewModel : BaseEntity<int>
    {
        //public string? AppUserId { get; set; }
        public int? ExamTBLId { get; set; }
        public bool IsFinished { get; set; }
        public int? UserRate { get; set; }
        public string? Result { get; set; }

        public virtual AppUser? CreatedUser { get; set; }
        public virtual ExamTBL? ExamTBL { get; set; }

    }
}
