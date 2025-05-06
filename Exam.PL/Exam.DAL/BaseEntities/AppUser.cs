using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Exam.DAL.BaseEntity
{
    public class AppUser : IdentityUser
    {
        public bool IsActivated { get; set; } = false;
        public bool IsDeleted { get; set; } = false;
        public DateTime DateOfJoin { get; set; } = DateTime.Now;
        public string? FirstName { get; set; } = null!;
        public string? LastName { get; set; } = null!;
    }
}
