//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace RamboScrum.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class ScrumRoleType
    {
        public ScrumRoleType()
        {
            this.ProjectRoleAssignments = new HashSet<ProjectRoleAssignment>();
        }
    
        public int scrum_role_type_id { get; set; }
        public string scrum_role_type_name { get; set; }
    
        public virtual ICollection<ProjectRoleAssignment> ProjectRoleAssignments { get; set; }
    }
}
