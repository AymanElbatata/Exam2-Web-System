using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Exam.DAL.BaseEntity;

namespace Exam.DAL.Entities
{
    public class UserExamTBL : BaseEntity<int>
    {
        //[ForeignKey(nameof(AppUser))]
        //public string? AppUserId { get; set; }
        //[ForeignKey(nameof(ExamTBL))]
        public int? ExamTBLId { get; set; }
        public bool IsFinished { get; set; }
        public int? UserRate { get; set; }
        public string? Result { get; set; }


        public virtual AppUser? CreatedUser { get; set; }
        public virtual ExamTBL? ExamTBL { get; set; }
    }
}
