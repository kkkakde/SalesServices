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
    
    public partial class W2S_Customer_CompressorRoom_Mapping
    {
        public int PK_Cust_Cmprsor_Mapng_Id { get; set; }
        public Nullable<int> FK_Cust_Id { get; set; }
        public Nullable<int> Cust_Cmprsor_RoomDetails { get; set; }
        public string Cust_Cmprsor_Model { get; set; }
        public string Cust_Cmprsor_Mfg_Year { get; set; }
        public string Cust_Cmprsor_Commissioning_Year { get; set; }
        public Nullable<int> Cust_Cmprsor_Status { get; set; }
        public Nullable<int> IsActive { get; set; }
        public Nullable<System.DateTime> Created_Date { get; set; }
        public Nullable<int> Created_By { get; set; }
        public Nullable<System.DateTime> Modified_Date { get; set; }
        public Nullable<int> Modified_By { get; set; }
    }
}
