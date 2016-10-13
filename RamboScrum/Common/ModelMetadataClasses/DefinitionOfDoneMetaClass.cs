using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace RamboScrum.Models
{
    [MetadataType(typeof(DefinitionOfDoneMD))]
    public partial class DefinitionOfDone
    {
    }
    public partial class DefinitionOfDoneMD
    {

        [Display(Name = "Definition of done source")]
        public int definition_of_done_id { get; set; }

        public virtual ICollection<Project> Projects { get; set; }
    }
}