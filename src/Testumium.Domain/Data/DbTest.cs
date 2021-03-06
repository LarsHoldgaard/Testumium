//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Testumium.Domain.Data
{
    using System;
    using System.Collections.Generic;
    
    public partial class DbTest
    {
        public DbTest()
        {
            this.DbTestItems = new HashSet<DbTestItem>();
        }
    
        public int Id { get; set; }
        public string Name { get; set; }
        public TestType TestType { get; set; }
        public string ContactName { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string WebsiteUrl { get; set; }
        public string Company { get; set; }
        public string Comments { get; set; }
        public string CallTime { get; set; }
        public string Domain { get; set; }
        public System.DateTime DateCreated { get; set; }
        public System.DateTime DateUpdated { get; set; }
    
        public virtual ICollection<DbTestItem> DbTestItems { get; set; }
    }
}
