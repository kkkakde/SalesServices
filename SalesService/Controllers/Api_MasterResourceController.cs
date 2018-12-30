using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using SalesService.CustomModel;
using SalesService.Models;
using System.Data.SqlClient;
namespace SalesService.Controllers
{
    public class Api_MasterResourceController : Api_CommonController
    {
        AtlasW2SEntities _db = null;
        [HttpPost]
        [ActionName("GetResourceList")]
        public HttpResponseMessage GetResourceList()
        {
            try
            {
                using (_db = new AtlasW2SEntities())
                {
                    var data = _db.Database.SqlQuery<MasterResourceBO>("W2S_SP_Master_Resource @Action,@PK_Resource_Id,@FK_Designation_Id,@FK_Zone_Id,@FK_State_Id,@Resource_Name,@Resource_Login_Id,@Resource_Password,@Resource_Mobile_No,@Resource_Email_Id,@isActive,@Created_By",
                       new SqlParameter("@Action", "GETRESOURCELIST"),
                       new SqlParameter("@PK_Resource_Id", ""),
                       new SqlParameter("@FK_Designation_Id", ""),
                       new SqlParameter("@FK_Zone_Id", ""),
                       new SqlParameter("@FK_State_Id", ""),
                       new SqlParameter("@Resource_Name", ""),
                       new SqlParameter("@Resource_Login_Id", ""),
                       new SqlParameter("@Resource_Password", ""),
                       new SqlParameter("@Resource_Mobile_No", ""),
                       new SqlParameter("@Resource_Email_Id", ""),
                       new SqlParameter("@isActive", ""),
                       new SqlParameter("@Created_By", "")
                       ).ToList();

                    if (data != null)
                    {
                        return Request.CreateResponse(HttpStatusCode.OK, data);
                    }
                    else
                    {
                        return Request.CreateResponse(HttpStatusCode.OK, "False");
                    }
                }
            }
            catch (Exception ex)
            {
                ExceptionHandling(ex.GetType().ToString(), "GetResourceList", ex.Message, "");
                return Request.CreateResponse(HttpStatusCode.BadRequest, new { Error = ex.InnerException.Message });
            }

        }

        [HttpPost]
        [ActionName("addResourceDetails")]
        public HttpResponseMessage addResourceDetails(MasterResourceBO masterResourceBO)
        {
            try
            {
                using (_db = new AtlasW2SEntities())
                {
                    var data = _db.Database.SqlQuery<MasterResourceBO>("W2S_SP_Master_Resource @Action,@PK_Resource_Id,@FK_Designation_Id,@FK_Zone_Id,@FK_State_Id,@Resource_Name,@Resource_Login_Id,@Resource_Password,@Resource_Mobile_No,@Resource_Email_Id,@isActive,@Created_By",
                       new SqlParameter("@Action", "INSERTUPDATE"),
                       new SqlParameter("@PK_Resource_Id",masterResourceBO.PK_Resource_Id),
                       new SqlParameter("@FK_Designation_Id", masterResourceBO.FK_Designation_Id),
                       new SqlParameter("@FK_Zone_Id",masterResourceBO.FK_Zone_Id == null? 0: masterResourceBO.FK_Zone_Id),
                       new SqlParameter("@FK_State_Id",masterResourceBO.FK_State_Id==null? 0: masterResourceBO.FK_State_Id),
                       new SqlParameter("@Resource_Name", masterResourceBO.Resource_Name),
                       new SqlParameter("@Resource_Login_Id", masterResourceBO.Resource_Login_Id),
                       new SqlParameter("@Resource_Password", masterResourceBO.Resource_Password),
                       new SqlParameter("@Resource_Mobile_No", masterResourceBO.Resource_Mobile_No),
                       new SqlParameter("@Resource_Email_Id", masterResourceBO.Resource_Email_Id),
                       new SqlParameter("@isActive", masterResourceBO.IsActive),
                       new SqlParameter("@Created_By", masterResourceBO.Created_By)
                       ).ToList();

                    if (data !=null)
                    {
                        return Request.CreateResponse(HttpStatusCode.OK, data);
                    }
                    else
                    {
                        return Request.CreateResponse(HttpStatusCode.OK, "False");
                    }
                }
            }
            catch (Exception ex)
            {
                ExceptionHandling(ex.GetType().ToString(), "addResourceDetails", ex.Message, "");
                return Request.CreateResponse(HttpStatusCode.BadRequest, new { Error = ex.InnerException.Message });
            }

        }

        
        [HttpPost]
        [ActionName("ResourceList")]
        public HttpResponseMessage ResourceList(int PK_Resource_Id)
        {
            try
            {
                using (_db = new AtlasW2SEntities())
                {
                    var data = _db.Database.SqlQuery<MasterResourceBO>("W2S_SP_Master_Resource @Action,@PK_Resource_Id,@FK_Designation_Id,@FK_Zone_Id,@FK_State_Id,@Resource_Name,@Resource_Login_Id,@Resource_Password,@Resource_Mobile_No,@Resource_Email_Id,@isActive,@Created_By",
                       new SqlParameter("@Action", "RESOURCELIST"),
                       new SqlParameter("@PK_Resource_Id", PK_Resource_Id),
                       new SqlParameter("@FK_Designation_Id", ""),
                       new SqlParameter("@FK_Zone_Id", ""),
                       new SqlParameter("@FK_State_Id", ""),
                       new SqlParameter("@Resource_Name", ""),
                       new SqlParameter("@Resource_Login_Id", ""),
                       new SqlParameter("@Resource_Password", ""),
                       new SqlParameter("@Resource_Mobile_No", ""),
                       new SqlParameter("@Resource_Email_Id", ""),
                       new SqlParameter("@isActive", ""),
                       new SqlParameter("@Created_By", "")
                       ).ToList();

                    if (data != null)
                    {
                        return Request.CreateResponse(HttpStatusCode.OK, data);
                    }
                    else
                    {
                        return Request.CreateResponse(HttpStatusCode.OK, "False");
                    }
                }
            }
            catch (Exception ex)
            {
                ExceptionHandling(ex.GetType().ToString(), "ResourceList", ex.Message, "");
                return Request.CreateResponse(HttpStatusCode.BadRequest, new { Error = ex.InnerException.Message });
            }

        }
    }
}
