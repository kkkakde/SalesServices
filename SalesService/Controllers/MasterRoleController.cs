using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using SalesService.Models;
using SalesService.CustomModel;
using System.Data.SqlClient;
using System.Data;
using System.IO;


namespace SalesService.Controllers
{
    public class MasterRoleController : ApiController
    {
        ResponseClass response = new ResponseClass();
        AtlasW2SEntities _db = null;
        [HttpGet]
        [ActionName("GetRoleDetails")]
        public HttpResponseMessage GetRoleDetails()
        {
            try
            {
                using (_db = new AtlasW2SEntities())
                {
                    var data = _db.Database.SqlQuery<Roles>("USP_RoleDetails @PK_Role_Id, @Name, @Description, @IsActive, @Created_Date, @Created_By, @Modified_Date, @Modified_By, @ACTION",
                       new SqlParameter("@PK_Role_Id", ""),
                       new SqlParameter("@Name", ""),
                       new SqlParameter("@Description", ""),
                       new SqlParameter("@IsActive", ""),
                       new SqlParameter("@Created_Date", ""),
                       new SqlParameter("@Created_By", ""),
                       new SqlParameter("@Modified_Date", ""),
                       new SqlParameter("@Modified_By", ""),
                       new SqlParameter("@Action", "GET_ROLE_DETAILS")
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
        [ActionName("AddRoleDetails")]
        public HttpResponseMessage AddRoleDetails(Roles roleDetails)
        {
            try
            {
                int result = 0;
                using (_db = new AtlasW2SEntities())
                {
                    // using (SqlConnection connection = new SqlConnection("data source=DESKTOP-F8J3KIS\\SQLSERVER;initial catalog=AtlasW2S;user id=sa;password=welcome@123;"))
                    using (SqlConnection connection = new SqlConnection(_db.Database.Connection.ConnectionString))
                    {
                        connection.Open();
                        using (var command = new SqlCommand())
                        {
                            try
                            {
                                command.CommandType = CommandType.StoredProcedure;
                                command.Connection = connection;
                                command.CommandText = "USP_RoleDetails";
                                command.Parameters.Add(new SqlParameter("@PK_Role_Id", roleDetails.PK_Role_Id));
                                command.Parameters.Add(new SqlParameter("@Name", roleDetails.Name));
                                command.Parameters.Add(new SqlParameter("@Description", roleDetails.Description));
                                command.Parameters.Add(new SqlParameter("@IsActive", roleDetails.IsActive));
                                command.Parameters.Add(new SqlParameter("@Created_Date", ""));
                                command.Parameters.Add(new SqlParameter("@Created_By", roleDetails.Created_By));
                                command.Parameters.Add(new SqlParameter("@Modified_Date", ""));
                                command.Parameters.Add(new SqlParameter("@Modified_By", 0));
                                command.Parameters.Add(new SqlParameter("@ACTION", "INSERT_ROLE_DETAILS"));
                                result = command.ExecuteNonQuery();
                                response.IsSuccess = true;
                                response.Message = "Roles information save successfully.";
                                return Request.CreateResponse(HttpStatusCode.OK, result);
                            }
                            catch (Exception ex)
                            {
                                response.IsSuccess = false;
                                response.Message = "error";
                                response.ResponseData = new { ex };
                                return Request.CreateResponse(HttpStatusCode.BadRequest, response);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                response.IsSuccess = false;
                response.Message = "error";
                response.ResponseData = new { ex };
                return Request.CreateResponse(HttpStatusCode.BadRequest, response);
            }
        }

        [HttpPost]
        [ActionName("UpdateRoleDetails")]
        public HttpResponseMessage UpdateRoleDetails(Roles roleDetails)
        {
            try
            {
                int result = 0;
                using (_db = new AtlasW2SEntities())
                {
                    // using (SqlConnection connection = new SqlConnection("data source=DESKTOP-F8J3KIS\\SQLSERVER;initial catalog=AtlasW2S;user id=sa;password=welcome@123;"))
                    using (SqlConnection connection = new SqlConnection(_db.Database.Connection.ConnectionString))
                    {
                        connection.Open();
                        using (var command = new SqlCommand())
                        {
                            try
                            {
                                command.CommandType = CommandType.StoredProcedure;
                                command.Connection = connection;
                                command.CommandText = "USP_RoleDetails";
                                command.Parameters.Add(new SqlParameter("@PK_Role_Id", roleDetails.PK_Role_Id));
                                command.Parameters.Add(new SqlParameter("@Name", roleDetails.Name));
                                command.Parameters.Add(new SqlParameter("@Description", roleDetails.Description));
                                command.Parameters.Add(new SqlParameter("@IsActive", roleDetails.IsActive));
                                command.Parameters.Add(new SqlParameter("@Created_Date", ""));
                                command.Parameters.Add(new SqlParameter("@Created_By", ""));
                                command.Parameters.Add(new SqlParameter("@Modified_Date", ""));
                                command.Parameters.Add(new SqlParameter("@Modified_By", roleDetails.Modified_By));
                                command.Parameters.Add(new SqlParameter("@ACTION", "UPDATE_ROLE_DETAILS"));
                                result = command.ExecuteNonQuery();
                                response.IsSuccess = true;
                                response.Message = "Roles information save successfully.";
                                return Request.CreateResponse(HttpStatusCode.OK, result);
                            }
                            catch (Exception ex)
                            {
                                response.IsSuccess = false;
                                response.Message = "error";
                                response.ResponseData = new { ex };
                                return Request.CreateResponse(HttpStatusCode.BadRequest, response);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                response.IsSuccess = false;
                response.Message = "error";
                response.ResponseData = new { ex };
                return Request.CreateResponse(HttpStatusCode.BadRequest, response);
            }
        }

        [HttpGet]
        [ActionName("GetUserRoleDetails")]
        public HttpResponseMessage GetUserRoleDetails()
        {
            try
            {
                using (_db = new AtlasW2SEntities())
                {
                    var data = _db.Database.SqlQuery<UserRolesDetails>("USP_Get_UserRoleMapping_Details @PK_UserRole_Id, @FK_User_Id, @ResourceName, @FK_Role_Id, @RoleName, @FK_Zone_Id, @ZoneName, @FK_State_Id, @StateName, @IsActive, @ACTION",
                       new SqlParameter("@PK_UserRole_Id", ""),
                       new SqlParameter("@FK_User_Id", ""),
                       new SqlParameter("@ResourceName", ""),
                       new SqlParameter("@FK_Role_Id", ""),
                       new SqlParameter("@RoleName", ""),
                       new SqlParameter("@FK_Zone_Id", ""),
                       new SqlParameter("@ZoneName", ""),
                       new SqlParameter("@FK_State_Id", ""),
                       new SqlParameter("@StateName", ""),
                       new SqlParameter("@IsActive", ""),
                       new SqlParameter("@Action", "GET_USER_ROLE_DETAILS")
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
        [ActionName("AddUserRoleDetails")]
        public HttpResponseMessage AddUserRoleDetails(UserRoles userRoleDetails)
        {
            try
            {
                int result = 0;
                using (_db = new AtlasW2SEntities())
                {
                    using (SqlConnection connection = new SqlConnection(_db.Database.Connection.ConnectionString))
                    {
                        connection.Open();
                        using (var command = new SqlCommand())
                        {
                            try
                            {
                                command.CommandType = CommandType.StoredProcedure;
                                command.Connection = connection;
                                command.CommandText = "USP_UserRoleMapping_Details";
                                command.Parameters.Add(new SqlParameter("@PK_UserRole_Id", userRoleDetails.PK_UserRole_Id));
                                command.Parameters.Add(new SqlParameter("@FK_User_Id", userRoleDetails.FK_User_Id));
                                command.Parameters.Add(new SqlParameter("@FK_Role_Id", userRoleDetails.FK_Role_Id));
                                command.Parameters.Add(new SqlParameter("@FK_Zone_Id", userRoleDetails.FK_Zone_Id));
                                command.Parameters.Add(new SqlParameter("@FK_State_Id", userRoleDetails.FK_State_Id));
                                command.Parameters.Add(new SqlParameter("@IsActive", userRoleDetails.IsActive));
                                command.Parameters.Add(new SqlParameter("@Created_Date", ""));
                                command.Parameters.Add(new SqlParameter("@Created_By", userRoleDetails.Created_By));
                                command.Parameters.Add(new SqlParameter("@Modified_Date", ""));
                                command.Parameters.Add(new SqlParameter("@Modified_By", 0));
                                command.Parameters.Add(new SqlParameter("@ACTION", "INSERT_USER_ROLE_DETAILS"));
                                result = command.ExecuteNonQuery();
                                response.IsSuccess = true;
                                response.Message = "User Roles information save successfully.";
                                return Request.CreateResponse(HttpStatusCode.OK, result);
                            }
                            catch (Exception ex)
                            {
                                response.IsSuccess = false;
                                response.Message = "error";
                                response.ResponseData = new { ex };
                                return Request.CreateResponse(HttpStatusCode.BadRequest, response);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                response.IsSuccess = false;
                response.Message = "error";
                response.ResponseData = new { ex };
                return Request.CreateResponse(HttpStatusCode.BadRequest, response);
            }
        }

        [HttpPost]
        [ActionName("UpdateUserRoleDetails")]
        public HttpResponseMessage UpdateUserRoleDetails(UserRoles userRoleDetails)
        {
            try
            {
                int result = 0;
                using (_db = new AtlasW2SEntities())
                {
                    using (SqlConnection connection = new SqlConnection(_db.Database.Connection.ConnectionString))
                    {
                        connection.Open();
                        using (var command = new SqlCommand())
                        {
                            try
                            {
                                command.CommandType = CommandType.StoredProcedure;
                                command.Connection = connection;
                                command.CommandText = "USP_UserRoleMapping_Details";
                                command.Parameters.Add(new SqlParameter("@PK_UserRole_Id", userRoleDetails.PK_UserRole_Id));
                                command.Parameters.Add(new SqlParameter("@FK_User_Id", userRoleDetails.FK_User_Id));
                                command.Parameters.Add(new SqlParameter("@FK_Role_Id", userRoleDetails.FK_Role_Id));
                                command.Parameters.Add(new SqlParameter("@FK_Zone_Id", userRoleDetails.FK_Zone_Id));
                                command.Parameters.Add(new SqlParameter("@FK_State_Id", userRoleDetails.FK_State_Id));
                                command.Parameters.Add(new SqlParameter("@IsActive", userRoleDetails.IsActive));
                                command.Parameters.Add(new SqlParameter("@Created_Date", ""));
                                command.Parameters.Add(new SqlParameter("@Created_By", ""));
                                command.Parameters.Add(new SqlParameter("@Modified_Date", ""));
                                command.Parameters.Add(new SqlParameter("@Modified_By", userRoleDetails.Modified_By));
                                command.Parameters.Add(new SqlParameter("@ACTION", "UPDATE_USER_ROLE_DETAILS"));
                                result = command.ExecuteNonQuery();
                                response.IsSuccess = true;
                                response.Message = "User Role information save successfully.";
                                return Request.CreateResponse(HttpStatusCode.OK, result);
                            }
                            catch (Exception ex)
                            {
                                response.IsSuccess = false;
                                response.Message = "error";
                                response.ResponseData = new { ex };
                                return Request.CreateResponse(HttpStatusCode.BadRequest, response);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                response.IsSuccess = false;
                response.Message = "error";
                response.ResponseData = new { ex };
                return Request.CreateResponse(HttpStatusCode.BadRequest, response);
            }
        }



    }
}
