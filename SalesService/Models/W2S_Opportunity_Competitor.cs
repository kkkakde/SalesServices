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
    
    public partial class W2S_Opportunity_Competitor
    {
        public long PK_Competitor_Id { get; set; }
        public Nullable<long> FK_Opportunity_Id { get; set; }
        public string FK_Competitor_Type_Id { get; set; }
        public Nullable<int> FK_Competitor_Id { get; set; }
        public Nullable<bool> Is_Main { get; set; }
        public string Comp_Product { get; set; }
        public string Comp_Price { get; set; }
    }
}