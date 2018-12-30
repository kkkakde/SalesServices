using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SalesService.CustomModel
{
    public class NPSSourceBO
    {
        
            public int FK_Opportunity_Id { get; set; }
            public int Created_By { get; set; }
            public string CustomerName { get; set; }
            public string OpportunityName { get; set; }
            public string FK_Status { get; set; }
            public Int64? Won_Value { get; set; }
            public string Won_Remarks { get; set; }
            public Int64? Lost_Value { get; set; }
            public string Lost_Remarks { get; set; }
    }
}