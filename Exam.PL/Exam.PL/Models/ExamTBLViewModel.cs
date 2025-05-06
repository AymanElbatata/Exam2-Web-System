using Exam.DAL.BaseEntity;

namespace Exam.PL.Models
{
    public class ExamTBLViewModel : BaseEntity<int>
    {
        //public string? AppUserId { get; set; }
        public string? Name { get; set; } 
        public string? Body { get; set; } 
        public int NumberofQuestions { get; set; }
        public int SuccessRate { get; set; }
        public int DurationInMinutes { get; set; }
        public bool IsPublished { get; set; }


        public virtual AppUser? CreatedUser { get; set; }
    }
}
