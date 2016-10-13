using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace RamboScrum.Models
{
    [MetadataType(typeof(ProjectMD))]
    public partial class Project
    {
    }
    public partial class ProjectMD
    {

         [Display(Name = "Project ID")]
        public int project_id { get; set; }
         [Display(Name = "Project name")]
        public string project_name { get; set; }
        [Display(Name = "Definition Of Done ID")]
        public int definition_of_done_id { get; set; }

        public virtual DefinitionOfDone DefinitionOfDone { get; set; }
        public virtual ICollection<PBLItem> PBLItems { get; set; }
        public virtual ICollection<ProjectRoleAssignment> ProjectRoleAssignments { get; set; }
        public virtual ICollection<Sprint> Sprints { get; set; }
    }
}