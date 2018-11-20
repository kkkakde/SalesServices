using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using SalesService.Models;
using SalesService.CustomModel;
using System.Data.SqlClient;
using System.IO;

namespace SalesService.Controllers
{
    public class Api_CommonController : ApiController
    {
        AtlasW2SEntities _db = null;
        [HttpPost]
        [ActionName("GetZoneList")]
        public HttpResponseMessage GetZoneList(Common common)
        {
            try
            {
                using (_db = new AtlasW2SEntities())
                {
                    var data = _db.Database.SqlQuery<ZoneListBO>("W2S_SP_Master_LIST @Action",
                         new SqlParameter("@Action", "GETZONELIST")
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
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex);
            }
        }

        [HttpPost]
        [ActionName("GetStateList")]
        public HttpResponseMessage GetStateList(int Zone_Id)
        {
            try
            {
                using (_db = new AtlasW2SEntities())
                {
                    var data = _db.Database.SqlQuery<StateListBO>("W2S_SP_Master_LIST @Action,@FK_Zone_Id",
                         new SqlParameter("@Action", "GETSTATELIST"),
                           new SqlParameter("@FK_Zone_Id", Zone_Id)
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
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex);
            }
        }

        [HttpPost]
        [ActionName("GetIndustryList")]
        public HttpResponseMessage GetIndustryList()
        {
            try
            {
                using (_db = new AtlasW2SEntities())
                {
                    var data = _db.Database.SqlQuery<Industry>("W2S_SP_Master_Industry '','',null,'GETALLINDUSTRies',0").ToList();

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
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex);
            }
        }

        [HttpPost]
        [ActionName("GetCityList")]
        public HttpResponseMessage GetCityList(int State_Id)
        {
            try
            {
                using (_db = new AtlasW2SEntities())
                {
                    var data = _db.Database.SqlQuery<CityListBO>("W2S_SP_Master_LIST @Action,@FK_Zone_Id,@FK_State_Id",
                         new SqlParameter("@Action", "GETCITYLIST"),
                         new SqlParameter("@FK_Zone_Id", ""),
                         new SqlParameter("@FK_State_Id", State_Id)
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
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex);
            }
        }

        [HttpPost]
        [ActionName("ExceptionHandling")]
        public void ExceptionHandling(string ExceptionType, string ModuleName, string ExceptionDetails, string UserInformation)
        {
            try
            {
                using (_db = new AtlasW2SEntities())
                {
                    var data = _db.Database.SqlQuery<int>("SP_LOGERROR @ExceptionType,@ModuleName,@ExceptionDetails,@UserInformation",
                         new SqlParameter("@ExceptionType", ExceptionType),
                         new SqlParameter("@ModuleName", ModuleName),
                         new SqlParameter("@ExceptionDetails", ExceptionDetails),
                         new SqlParameter("@UserInformation", UserInformation)
                       ).ToList();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


      
    }
}
