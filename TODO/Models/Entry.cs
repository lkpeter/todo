//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace TODO.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class Entry
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public System.DateTime Created { get; set; }
        public System.DateTime Expire { get; set; }
        public string Desc { get; set; }
        public Nullable<System.DateTime> DoneDate { get; set; }
        public string UserId { get; set; }
        public string OwnerId { get; set; }
    }
}
