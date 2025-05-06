using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Exam.DAL.BaseEntity;

namespace Exam.DAL.Entities
{

    public class UserExamDetailTBL : BaseEntity<int>
    {
        //[ForeignKey(nameof(ExamTBL))]
        public int? UserExamTBLId { get; set; }
        public int? QuestionTBLId { get; set; }
        public int? AnswerTBLId { get; set; }

        public virtual UserExamTBL? UserExamTBL { get; set; }
        public virtual QuestionTBL? QuestionTBL { get; set; }
        public virtual AnswerTBL? AnswerTBL { get; set; }

    }
}