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
    
    public partial class W2S__Master_Enquiry_Visit_
    {
        public int Enqry_Visit_Id { get; set; }
        public Nullable<int> Enqry_Id { get; set; }
        public string Visit_Cd { get; set; }
        public Nullable<System.DateTime> Visit_Date { get; set; }
        public string Visit_Start_Time { get; set; }
        public string Visit_End_Time { get; set; }
        public Nullable<int> IsActive { get; set; }
        public Nullable<System.DateTime> Created_Date { get; set; }
        public Nullable<int> Created_By { get; set; }
        public Nullable<System.DateTime> Modified_Date { get; set; }
        public Nullable<int> Modified_By { get; set; }
    }
}
