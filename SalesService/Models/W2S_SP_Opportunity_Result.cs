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
    
    public partial class W2S_SP_Opportunity_Result
    {
        public long PK_Opportunity_Id { get; set; }
        public string Opportunity_Name { get; set; }
        public string Cust_Name { get; set; }
        public string Closed_Date { get; set; }
        public string FK_Status { get; set; }
        public string Added_Date { get; set; }
        public Nullable<int> Expected_Value { get; set; }
        public string Sales_Phase { get; set; }
        public string Status { get; set; }
        public string Resource_Name { get; set; }
        public string FK_Status1 { get; set; }
    }
}