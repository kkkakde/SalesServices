using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SalesService.CustomModel
{
    public class MasterCompetitorBO
    {
        public int? PK_Competitor_Id { get; set; }
        public string Competitor_Name { get; set; }
        public string Competitor_Desc { get; set; }
        public int? IsActive { get; set; }
        public Nullable<DateTime> Created_Date { get; set; }
        public int? Created_By { get; set; }
        public Nullable<DateTime> Modified_Date { get; set; }
        public int? Modified_By { get; set; }
        public string FK_Competitor_Type_Id { get; set; }
    }
}