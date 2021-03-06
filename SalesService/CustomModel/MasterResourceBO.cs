﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SalesService.CustomModel
{
    public class MasterResourceBO
    {
            public int? PK_Resource_Id { get; set; }
            public string Resource_Name { get; set; }
            public string Resource_Login_Id { get; set; }
            public string Resource_Password { get; set; }
            public string Resource_Mobile_No { get; set; }
            public string Resource_Email_Id { get; set; }
            public string Designation_Desc { get; set; }
            public int? FK_Designation_Id { get; set; }
            public int? FK_Zone_Id { get; set; }
            public int? FK_State_Id { get; set; }
            public int? IsActive { get; set; }
            public int? Created_By { get; set; }
            public string Zone_Desc { get; set; }
            public string State_Desc { get; set; }

    }
}