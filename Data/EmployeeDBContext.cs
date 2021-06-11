using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EF_DBFirst_Approach.Models;
using Microsoft.EntityFrameworkCore;

namespace EF_DBFirst_Approach.Data
{
    public class EmployeeDBContext :DbContext
    {
        public EmployeeDBContext(DbContextOptions<EmployeeDBContext> options) : base(options)
        {

        }

        public DbSet<tblEmployee> tblEmployees { get; set; }

        public DbSet<tblSkill> tblSkills { get; set; }

        public DbSet<EF_DBFirst_Approach.Models.EmployeeViewModel> EmployeeViewModel { get; set; }
    }
}
