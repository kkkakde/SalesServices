//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace SalesService.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class W2S_Master_Holiday
    {
        public int PK_Holiday_Id { get; set; }
        public string Holiday_Name { get; set; }
        public string Holiday_Description { get; set; }
        public Nullable<System.DateTime> Holiday_Date { get; set; }
        public Nullable<int> IsActive { get; set; }
        public Nullable<System.DateTime> Created_Date { get; set; }
        public Nullable<int> Created_By { get; set; }
        public Nullable<System.DateTime> Modify_Date { get; set; }
        public Nullable<int> Modify_By { get; set; }
    }
}