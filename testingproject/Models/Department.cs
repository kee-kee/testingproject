using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace testingproject.Models
{
    public class Department
    {
        public string DepartmentName { get; set; }
        [Display(Name = "Subject")]
        [StringLength(50)]
        public string Subject { get; set; }
       

        [Display(Name = "Chapter")]
        public string Chapter { get; set; }
       

        [Display(Name = "Primary")]
        public string Primary { get; set; }
       
    }


}