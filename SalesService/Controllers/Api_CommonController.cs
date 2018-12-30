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
        [ActionName("GetZoneAllList")]
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
                    var data = _db.Database.SqlQuery<Industry>("W2S_SP_Master_Industry 'GETALLINDUSTRies','','','',0").ToList();

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
                    var data = _db.Database.SqlQuery<int>("USP_ErrorLog @ExceptionType,@ModuleName,@ExceptionDetails,@UserInformation",
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

        [HttpGet]
        [ActionName("GetSegmentList")]
        public HttpResponseMessage GetSegmentList()
        {
            try
            {
                using (_db = new AtlasW2SEntities())
                {
                    var data = _db.Database.SqlQuery<SegmentType>("USP_Common_List @W2S_List_Id, @List_Code, @List_Desc, @ACTION",
                        new SqlParameter("@W2S_List_Id", ""),
                        new SqlParameter("@List_Code", ""),
                        new SqlParameter("@List_Desc", ""),
                        new SqlParameter("@Action", "GETSEGMENTLIST")
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

        [HttpGet]
        [ActionName("GetMappingType")]
        public HttpResponseMessage GetMappingType()
        {
            try
            {
                using (_db = new AtlasW2SEntities())
                {
                    var data = _db.Database.SqlQuery<SegmentType>("USP_Common_List @W2S_List_Id, @List_Code, @List_Desc, @ACTION",
                        new SqlParameter("@W2S_List_Id", ""),
                        new SqlParameter("@List_Code", ""),
                        new SqlParameter("@List_Desc", ""),
                        new SqlParameter("@Action", "GETMAPPINGTYPE")
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

        [HttpGet]
        [ActionName("GetRangeType")]
        public HttpResponseMessage GetRangeType()
        {
            try
            {
                using (_db = new AtlasW2SEntities())
                {
                    var data = _db.Database.SqlQuery<SegmentType>("USP_Common_List @W2S_List_Id, @List_Code, @List_Desc, @ACTION",
                        new SqlParameter("@W2S_List_Id", ""),
                        new SqlParameter("@List_Code", ""),
                        new SqlParameter("@List_Desc", ""),
                        new SqlParameter("@Action", "GETRANGETYPE")
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

        [ActionName("GetDesignationList")]
        [HttpPost]
        public HttpResponseMessage GetDesignationList()
        {

            try
            {
                using (_db = new AtlasW2SEntities())
                {
                    var data = _db.Database.SqlQuery<DesignationListBO>("W2S_SP_Master_LIST @Action,@FK_Zone_Id,@FK_State_Id",
                         new SqlParameter("@Action", "GETDESIGNATIONLIST"),
                         new SqlParameter("@FK_Zone_Id", ""),
                         new SqlParameter("@FK_State_Id", "")
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

        [HttpGet]
        [ActionName("GetUserDetails")]
        public HttpResponseMessage GetUserDetails()
        {
            try
            {
                using (_db = new AtlasW2SEntities())
                {
                    var data = _db.Database.SqlQuery<LoginModelBO>("W2S_SP_Master_Resource @Action",
                         new SqlParameter("@Action", "GETRESOURCELIST")
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

       
    }
}

