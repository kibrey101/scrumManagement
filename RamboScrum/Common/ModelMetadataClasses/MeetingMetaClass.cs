using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace RamboScrum.Models
{
    [MetadataType(typeof(MeetingMD))]
    public partial class Meeting
    {
    }
    public partial class MeetingMD
    {

        [Display(Name = "Meeting ID")]
        public int meeting_id { get; set; }
        [Display(Name = "Meeting type ID")]
        public int meeting_type_id { get; set; }
        [Display(Name = "Sprint ID")]
        public int sprint_id { get; set; }
        [Display(Name = "Start time")]
        public System.DateTime start_time { get; set; }

        public virtual MeetingType MeetingType { get; set; }
        public virtual Sprint Sprint { get; set; }
    }
}