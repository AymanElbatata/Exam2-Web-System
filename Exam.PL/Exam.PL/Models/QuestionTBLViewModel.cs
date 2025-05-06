using Exam.DAL.BaseEntity;
using Exam.DAL.Entities;

namespace Exam.PL.Models
{
    public class QuestionTBLViewModel : BaseEntity<int>
    {
        public int? ExamTBLId { get; set; }
        public string? Title { get; set; }

        public virtual ExamTBL? ExamTBL { get; set; }
    }
}
