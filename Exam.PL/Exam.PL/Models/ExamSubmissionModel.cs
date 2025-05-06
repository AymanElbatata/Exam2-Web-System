using System.ComponentModel.DataAnnotations;

namespace Exam.PL.Models
{
    public class ExamSubmissionModel
    {
        public int ExamTBLId { get; set; }
        public List<QuestionSubmission> Questions { get; set; } = new List<QuestionSubmission>();
    }

    public class QuestionSubmission
    {
        public int? QuestionTBLId { get; set; }
        [Required(ErrorMessage = "Please select an answer")]
        public int? SelectedAnswerId { get; set; }
    }

}
