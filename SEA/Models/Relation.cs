//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class Relation
    {
        public int Id { get; set; }
        public string AccountId { get; set; }
        public string PartnerId { get; set; }
        public string Type { get; set; }
        public System.DateTime CreateDate { get; set; }
        public bool IsActive { get; set; }
    
        public virtual AspNetUser AspNetUser { get; set; }
        public virtual AspNetUser AspNetUser1 { get; set; }
    }
}
