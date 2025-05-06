using Exam.DAL.BaseEntity;
using Exam.DAL.Entities;

namespace Exam.PL.Models
{
    public class TakeExamModel 
    {
        public string? CreatedUserID { get; set; }

        public int? ExamTBLId { get; set; }
        public string? Name { get; set; } = null!;
        public string? Body { get; set; } = null!;
        public int NumberofQuestions { get; set; }
        public int? SuccessRate { get; set; } = 0;
        public int DurationInnMinutes { get; set; }

        public List<QuestionViewModel> Questions { get; set; } = new List<QuestionViewModel>();
    }

    public class QuestionViewModel : BaseEntity<int>
    {
        public int? QuestionTBLId { get; set; }
        public string? Title { get; set; }
        public bool SelectedAnswer { get; set; }

        public List<AnswersViewModel> Answers { get; set; } = new List<AnswersViewModel>();

    }


    public class AnswersViewModel : BaseEntity<int>
    {
        public int? AnswerTBLId { get; set; }
        public string? Name { get; set; }
    }
}
