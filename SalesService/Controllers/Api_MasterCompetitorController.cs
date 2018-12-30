using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using SalesService.Models;
using SalesService.CustomModel;
using System.Data.SqlClient;

namespace SalesService.Controllers
{
    public class Api_MasterCompetitorController : Api_CommonController
    {
        AtlasW2SEntities _db = null;
        ResponseClass response = new ResponseClass();
        [HttpPost]
        [ActionName("GetCompetitorList")]
        public HttpResponseMessage GetCompetitorList(MasterCompetitorBO CompetitorMasterBO)
        {
            try
            {
                using (_db = new AtlasW2SEntities())
                {
                    var data = _db.Database.SqlQuery<MasterCompetitorBO>("W2S_SP_Master_Competitor @FK_Competitor_Type_Id,@Competitor_Name,@Competitor_Desc,@IsActive,@Action,@PK_Competitor_Id,@Created_By",
                       new SqlParameter("@FK_Competitor_Type_Id", ""),
                       new SqlParameter("@Competitor_Name", ""),
                       new SqlParameter("@Competitor_Desc", ""),
                       new SqlParameter("@IsActive", ""),
                       new SqlParameter("@Action", "GETALLCOMPETITORs"),
                       new SqlParameter("@PK_Competitor_Id", ""),
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
                ExceptionHandling(ex.GetType().ToString(), "GetCompetitorList", ex.Message, "");
                return Request.CreateResponse(HttpStatusCode.BadRequest, new { Error = ex.InnerException.Message });
            }

        }

        [HttpPost]
        [ActionName("AddCompetitor")]
        public HttpResponseMessage AddCompetitor(MasterCompetitorBO competitorMasterBO)
        {
            try
            {
                using (_db = new AtlasW2SEntities())
                {
                    var data = _db.Database.SqlQuery<MasterCompetitorBO>("W2S_SP_Master_Competitor @FK_Competitor_Type_Id,@Competitor_Name,@Competitor_Desc,@IsActive,@Action,@PK_Competitor_Id,@Created_By",
                       new SqlParameter("@FK_Competitor_Type_Id", competitorMasterBO.FK_Competitor_Type_Id),
                       new SqlParameter("@Competitor_Name", competitorMasterBO.Competitor_Name),
                       new SqlParameter("@Competitor_Desc", competitorMasterBO.Competitor_Desc),
                       new SqlParameter("@IsActive", competitorMasterBO.IsActive),
                       new SqlParameter("@Action", "INSERTUPDATE"),
                       new SqlParameter("@PK_Competitor_Id", competitorMasterBO.PK_Competitor_Id),
                       new SqlParameter("@Created_By", competitorMasterBO.Created_By)
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
                ExceptionHandling(ex.GetType().ToString(), "GetCompetitorList", ex.Message, "");
                return Request.CreateResponse(HttpStatusCode.BadRequest, new { Error = ex.InnerException.Message });
            }

        }

        [HttpPost]
        [ActionName("EditCompetitor")]
        public HttpResponseMessage EditCompetitor(int PK_Competitor_Id)
        {
            try
            {
                using (_db = new AtlasW2SEntities())
                {
                    var data = _db.Database.SqlQuery<MasterCompetitorBO>("W2S_SP_Master_Competitor @FK_Competitor_Type_Id,@Competitor_Name,@Competitor_Desc,@IsActive,@Action,@PK_Competitor_Id,@Created_By",
                       new SqlParameter("@FK_Competitor_Type_Id", ""),
                       new SqlParameter("@Competitor_Name", ""),
                       new SqlParameter("@Competitor_Desc", ""),
                       new SqlParameter("@IsActive", ""),
                       new SqlParameter("@Action", "GETBYID"),
                       new SqlParameter("@PK_Competitor_Id", PK_Competitor_Id),
                       new SqlParameter("@Created_By", "")
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
                ExceptionHandling(ex.GetType().ToString(), "GetCompetitorList", ex.Message, "");
                return Request.CreateResponse(HttpStatusCode.BadRequest, new { Error = ex.InnerException.Message });
            }

        }
    }
}
