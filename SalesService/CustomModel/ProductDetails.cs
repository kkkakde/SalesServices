using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SalesService.CustomModel
{
    public class ProductDetails
    {
        public int? Product_Id { get; set; }
        public string Product_Name { get; set; }
        public string Product_Desc { get; set; }
        public bool IsActive { get; set; }
        public Nullable<DateTime> Created_Date { get; set; }
        public int? Created_By { get; set; }
        public Nullable<DateTime> Modified_Date { get; set; }
        public int? Modified_By { get; set; }
    }
}