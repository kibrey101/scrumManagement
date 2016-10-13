using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace RamboScrum.Models
{
    [MetadataType(typeof(PBLItemMD))]
    public partial class PBLItem
    {
    }
    public partial class PBLItemMD
    {
         [Display(Name = "PBL Item ID")]
        public int pbl_item_id { get; set; }
         [Display(Name = "Project ID")]
        public int project_id { get; set; }
         [Display(Name = "Sprint ID")]
        public int sprint_id { get; set; }
         [Display(Name = "Status ID")]
        public int status_id { get; set; }
         [Display(Name = "Item Priority")]
        public int item_priority { get; set; }
         [Display(Name = "Estimated Hours")]
        public decimal estimated_hours { get; set; }
         [Display(Name = "PBL Item Name")]
        public string name { get; set; }
         [Display(Name = "Item Description")]
        public string item_description { get; set; }

        public virtual Project Project { get; set; }
        public virtual Sprint Sprint { get; set; }
        public virtual Status Status { get; set; }
        public virtual ICollection<Task> Tasks { get; set; }
    }
}