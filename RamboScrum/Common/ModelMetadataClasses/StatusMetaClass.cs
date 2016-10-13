using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace RamboScrum.Models
{
    [MetadataType(typeof(StatusMD))]
    public partial class Status
    {
    }
    public partial class StatusMD
    {
         [Display(Name = "Status ID")]
        public int status_id { get; set; }
         [Display(Name = "Status")]
        public string status1 { get; set; }

        public virtual ICollection<PBLItem> PBLItems { get; set; }
        public virtual ICollection<Task> Tasks { get; set; }
    }
}