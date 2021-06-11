﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace EF_DBFirst_Approach.Models
{
    public class tblSkill
    {
        [Key]        
        public int SkillID { get; set; }

        [Display(Name = "Type of Skill")]
        public string Title { get; set; }
    }
}
