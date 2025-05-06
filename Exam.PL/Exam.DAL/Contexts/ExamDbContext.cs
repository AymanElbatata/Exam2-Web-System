using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Exam.DAL.BaseEntity;
using Exam.DAL.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Exam.DAL.Contexts
{
    public class ExamDbContext : IdentityDbContext<AppUser, AppRole, string>
    {
        public ExamDbContext()
        {
            ChangeTracker.LazyLoadingEnabled = false;
        }
        public ExamDbContext(DbContextOptions<ExamDbContext> options) : base(options)
        {
        }

        //protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        //    => optionsBuilder.UseSqlServer("server=.; database= DemoDb; integrated security= true;");

        public DbSet<AnswerTBL> AnswerTBLs { get; set; }
        public DbSet<ExamTBL> ExamTBLs { get; set; }
        public DbSet<QuestionTBL> QuestionTBLs { get; set; }
        public DbSet<UserExamTBL> UserExamTBLs { get; set; }
        public DbSet<UserExamDetailTBL> UserExamDetailTBLs { get; set; }
    }
}
