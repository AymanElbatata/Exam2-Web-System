using Exam.DAL.BaseEntity;
using Exam.DAL.Entities;

namespace Exam.PL.Models
{
    public class AnswerTBLViewModel : BaseEntity<int>
    {
        public int? QuestionTBLId { get; set; }
        public string? Name { get; set; } 
        public bool IsRight { get; set; }

        public virtual QuestionTBL? QuestionTBL { get; set; }
    }
}
