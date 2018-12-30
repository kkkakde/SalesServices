using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SalesService.CustomModel
{
    public class RangeMappingDetails
    {

        public int? PK_RangeMapping_Id { get; set; }
        public string FK_MapType_Id { get; set; }
        public string FK_Range_Id { get; set; }
        public string  FK_Sub_Range_Id { get; set; }
        
        public string FK_Prod_Id { get; set; }
        public bool IsActive  { get; set; }
        public DateTime Created_Date { get; set; }
        public int? Created_By { get; set; }
        public DateTime Modified_Date { get; set; }
        public int?  Modified_By { get; set; }
        public string FK_W2S_List_Id { get; set; }

    }

    public class RangeMapping
    {

        public int? PK_RangeMapping_Id { get; set; }
        public string FK_MapType_Id { get; set; }
        public int? FK_Prod_Id { get; set; }
        public string ProductName { get; set; }
        public string FK_Range_Id { get; set; }
        public int? FK_Sub_Range_Id { get; set; }
        public string SubRangeName { get; set; }

        //public int? IsActive { get; set; }
        //public Nullable<DateTime> Created_Date { get; set; }
        //public int? Created_By { get; set; }
        //public Nullable<DateTime> Modified_Date { get; set; }
        //public int? Modified_By { get; set; }
        //public string FK_W2S_List_Id { get; set; }

    }
}