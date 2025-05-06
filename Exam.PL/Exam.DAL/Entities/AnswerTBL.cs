using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Exam.DAL.BaseEntity;

namespace Exam.DAL.Entities
{
    public class AnswerTBL : BaseEntity<int>
    {
        //[ForeignKey(nameof(QuestionTBL))]
        public int? QuestionTBLId { get; set; }
        public string? Name { get; set; } = null!;
        public bool IsRight { get; set; } = false;

        public virtual QuestionTBL? QuestionTBL { get; set; }
    }
}
