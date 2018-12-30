using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SalesService.CustomModel
{
    public class SubRangeDetails
    {
        public int? PK_SubRange_Id { get; set; }
        public string FK_W2S_List_Id { get; set; }
        public string SubRange_Name { get; set; }
        public string SubRange_Description { get; set; }
        public bool IsActive { get; set; }
        public Nullable<DateTime> Created_Date { get; set; }
        public int? Created_By { get; set; }
        public Nullable<DateTime> Modified_Date { get; set; }
        public int? Modified_By { get; set; }

    }
}


                 
