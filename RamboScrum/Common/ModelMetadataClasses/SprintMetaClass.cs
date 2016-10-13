using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace RamboScrum.Models
{
    [MetadataType(typeof(SprintMD))]
    public partial class Sprint
    {
    }
    public partial class SprintMD
    {
         [Display(Name = "Sprint ID")]
        public int sprint_id { get; set; }
         [Display(Name = "Sprint Name")]
        public string sprint_name { get; set; }
         [Display(Name = "Project ID")]
        public int project_id { get; set; }
         [Display(Name = "Start Date")]
        public System.DateTime start_date { get; set; }
         [Display(Name = "End Date")]
        public System.DateTime end_date { get; set; }

        public virtual ICollection<Meeting> Meetings { get; set; }
        public virtual ICollection<PBLItem> PBLItems { get; set; }
        public virtual Project Project { get; set; }
    }
}