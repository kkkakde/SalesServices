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
    
    public partial class W2S_SP_Master_Competitor_Result
    {
        public int PK_Competitor_Id { get; set; }
        public string FK_Competitor_Type_Id { get; set; }
        public string Competitor_Name { get; set; }
        public string Competitor_Desc { get; set; }
        public Nullable<int> IsMain { get; set; }
        public int IsActive { get; set; }
        public Nullable<System.DateTime> Created_Date { get; set; }
        public Nullable<int> Created_By { get; set; }
        public Nullable<System.DateTime> Modified_Date { get; set; }
        public Nullable<int> Modified_By { get; set; }
    }
}
