using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace RamboScrum.Models
{
    [MetadataType(typeof(MeetingTypeMD))]
    public partial class MeetingType
    {
    }
    public partial class MeetingTypeMD
    {
         [Display(Name = "Meetiny type ID")]
        public int meeting_type_id { get; set; }
         [Display(Name = "Meeting type name")]
        public string meeting_type_name { get; set; }

        public virtual ICollection<Meeting> Meetings { get; set; }
    }
}