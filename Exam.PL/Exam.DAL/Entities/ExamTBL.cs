using Exam.DAL.BaseEntity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Exam.DAL.Entities
{
    public class ExamTBL : BaseEntity<int>
    {
        //[ForeignKey(nameof(AppUser))]
        //public string? AppUserId { get; set; }
        public string? Name { get; set; } = null!;
        public string? Body { get; set; } = null!;
        public int NumberofQuestions { get; set; }
        public int? SuccessRate { get; set; } = 0;
        public int DurationInMinutes { get; set; } = 30;
        public bool IsPublished { get; set; } = false;

        //public ICollection<QuestionTBL> Questions { get; set; }

        public virtual AppUser? CreatedUser { get; set; }

    }
}
