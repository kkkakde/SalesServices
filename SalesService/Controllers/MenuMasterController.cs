using SalesService.CustomModel;
using SalesService.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace SalesService.Controllers
     
{
   
    public class MenuMasterController : Api_CommonController
    {
        ResponseClass response = new ResponseClass();
        AtlasW2SEntities _db = null;

        [HttpPost]
        [ActionName("GetMenuList")]
        public HttpResponseMessage GetMenuList(MenuListBO menu)
        {
            try
            {
                using (_db = new AtlasW2SEntities())
                {
                    var data = _db.Database.SqlQuery<MenuListBO>("USP_GetMenuList @FK_Designation_Id",
                       new SqlParameter("FK_Designation_Id",menu.FK_Designation_Id)                      
                       ).ToList();

                    if (data != null)
                    {
                        response.IsSuccess = true;
                        response.Message = "";
                        response.Response = data;
                        return Request.CreateResponse(HttpStatusCode.OK, response);

                    }
                    else
                    {
                        response.IsSuccess = false;
                        response.Message = "";
                        response.Response = data;
                        return Request.CreateResponse(HttpStatusCode.OK, response);
                    }
                }
            }
            catch (Exception ex)
            {
                ExceptionHandling(ex.GetType().ToString(), "Menu List", ex.Message, "");
                return Request.CreateResponse(HttpStatusCode.BadRequest, new { Error = ex.InnerException.Message });
            }
        }
    }
}
