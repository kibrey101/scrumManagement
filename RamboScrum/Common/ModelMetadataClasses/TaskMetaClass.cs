using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace RamboScrum.Models
{
    [MetadataType(typeof(TaskMD))]
    public partial class Task
    {
    }
    public partial class TaskMD
    {
         [Display(Name = "Task ID")]
        public int task_id { get; set; }
         [Display(Name = "Task Name")]
        public string name { get; set; }
         [Display(Name = "Task Description")]
        public string task_description { get; set; }
         [Display(Name = "Estimated Hours")]
        public decimal estimated_hours { get; set; }
         [Display(Name = "Task Priority")]
        public int task_priority { get; set; }
         [Display(Name = "PBL Item ID")]
        public int pbl_item_id { get; set; }
         [Display(Name = "Status ID")]
        public int status_id { get; set; }
         [Display(Name = "Person ID")]
        public int person_id { get; set; }
         [Display(Name = "Assignment Date")]
        public System.DateTime assignment_date { get; set; }
         [Display(Name = "Completion Date")]
        public Nullable<System.DateTime> completion_date { get; set; }

        public virtual PBLItem PBLItem { get; set; }
        public virtual Person Person { get; set; }
        public virtual Status Status { get; set; }
    }
}