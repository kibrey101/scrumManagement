using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace RamboScrum.Models
{
    [MetadataType(typeof(PersonMD))]
    public partial class Person
    {
    }
    public partial class PersonMD
    {
         [Display(Name = "Person ID")]
        public int person_id { get; set; }
         [Display(Name = "First Name")]
        public string first_name { get; set; }
         [Display(Name = "Last Name")]
        public string last_name { get; set; }
         [Display(Name = "Phone")]
        public string phone { get; set; }
         [Display(Name = "E-mail")]
        public string email { get; set; }

        public virtual ICollection<ProjectRoleAssignment> ProjectRoleAssignments { get; set; }
        public virtual ICollection<Task> Tasks { get; set; }
    }
}