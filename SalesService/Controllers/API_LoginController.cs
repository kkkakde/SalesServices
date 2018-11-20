using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using SalesService.Models;
using SalesService.CustomModel;
using System.Data.SqlClient;
using System.Net.Mail;
using System.Configuration;

namespace SalesService.Controllers
{
    public class API_LoginController : ApiController
    {
        AtlasW2SEntities _db = null;
        [HttpPost]
        [ActionName("Login")]
        public HttpResponseMessage Login(string username, string password)
        {
            try
            {
                using (_db = new AtlasW2SEntities())
                {
                      var data = _db.Database.SqlQuery<LoginModelBO>("W2S_SP_MASTER_USER @Action,@PK_Resource_Id,@FK_Designation_Id,@FK_Zone_Id,@FK_State_Id,@Resource_Name,@Resource_Login_Id,@Resource_Password,@Resource_Mobile_No,@Navigation_Parent_Id,@Resource_Email_Id,@isActive,@Created_By,@Created_Date,@Modified_By,@Modified_Date",
                           new SqlParameter("@Action", "GETUSERLOGINDETAILS"),
                           new SqlParameter("@PK_Resource_Id", ""),
                           new SqlParameter("@FK_Designation_Id", ""),
                           new SqlParameter("@FK_Zone_Id", ""),
                           new SqlParameter("@FK_State_Id", ""),
                           new SqlParameter("@Resource_Name", ""),
                           new SqlParameter("@Resource_Login_Id", username),
                           new SqlParameter("@Resource_Password", password),
                           new SqlParameter("@Resource_Mobile_No", ""),
                           new SqlParameter("@Navigation_Parent_Id", ""),
                           new SqlParameter("@Resource_Email_Id", ""),
                           new SqlParameter("@isActive", ""),
                           new SqlParameter("@Created_By", ""),
                           new SqlParameter("@Created_Date", ""),
                           new SqlParameter("@Modified_By", ""),
                           new SqlParameter("@Modified_Date", "")
                         ).FirstOrDefault();
                    if (data != null)
                    {
                        return Request.CreateResponse(HttpStatusCode.OK, data);
                    }
                    else
                    {
                        return Request.CreateResponse(HttpStatusCode.BadRequest, "False");
                    }
                }
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex);
            }
        }

        [HttpPost]
        [ActionName("ForgotPassword")]
        public HttpResponseMessage ForgotPassword(string EmailID)
        {
            try
            {
                using (_db = new AtlasW2SEntities())
                {
                    var data = _db.Database.SqlQuery<LoginModelBO>("SP_GetAllUser @EmailID,@Action",
                         new SqlParameter("@EmailID", EmailID),
                         new SqlParameter("@Action", "GETEMAILs")
                       ).FirstOrDefault();

                    if (data != null)
                    {
                        try
                        {
                            using (MailMessage mailMessage = new MailMessage())
                            {
                                mailMessage.From = new MailAddress(ConfigurationManager.AppSettings["MailFrom"]);
                                mailMessage.Subject = "Sales project reset password ";
                                mailMessage.Body = "Hi, <br/> Your current password="+ data.Resource_Password;
                                mailMessage.IsBodyHtml = true;
                                //if (!string.IsNullOrEmpty(ccDetailId))
                                //{
                                //    mailMessage.CC.Add(new MailAddress(ccDetailId));
                                //}
                                //krios.fdtraining@gmail.com
                                //fddms@12345
                                string arMailTo = data.Resource_Email_Id;
                                mailMessage.To.Add(new MailAddress(arMailTo));
                                SmtpClient smtp = new SmtpClient();
                                smtp.Host = ConfigurationManager.AppSettings["Host"]; //reading from web.config 
                                smtp.EnableSsl = Convert.ToBoolean(ConfigurationManager.AppSettings["EnableSsl"]); //reading from web.config 
                                System.Net.NetworkCredential NetworkCred = new System.Net.NetworkCredential();
                                NetworkCred.UserName = ConfigurationManager.AppSettings["MailFrom"]; //reading from web.config 
                                NetworkCred.Password = ConfigurationManager.AppSettings["Password"]; //reading from web.config 
                                smtp.UseDefaultCredentials = false;
                                smtp.Credentials = NetworkCred;
                                smtp.Port = int.Parse(ConfigurationManager.AppSettings["Port"]); //reading from web.config 
                                smtp.Send(mailMessage);
                            }
                        }
                        catch (Exception e)
                        {

                        }
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
        [ActionName("ChangePassword")]
        public HttpResponseMessage ChangePassword(ChangePasswordBO changePasswordBO)
        {
            try
            {
                using (_db = new AtlasW2SEntities())
                {
                    var data = _db.Database.SqlQuery<LoginModelBO>("SP_USERAUTHFOR_AtlasSales @User_ID,@Password,@NEWPassword,@Action",
                         new SqlParameter("@User_ID", changePasswordBO.User_ID),
                         new SqlParameter("@Password", changePasswordBO.Password),
                         new SqlParameter("@NEWPassword", changePasswordBO.NEWPassword),
                         new SqlParameter("@ACTION", "UPDATE")
                       ).FirstOrDefault();

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
        [ActionName("ResetPassword")]
        public HttpResponseMessage ResetPassword(ChangePasswordBO changePasswordBO)
        {
            try
            {
                using (_db = new AtlasW2SEntities())
                {
                    var data = _db.Database.SqlQuery<LoginModelBO>("W2S_SP_Reset_Password2 @User_ID,@NEWPassword,@Action",
                         new SqlParameter("@User_ID", changePasswordBO.User_ID),
                         new SqlParameter("@NEWPassword", changePasswordBO.NEWPassword),
                         new SqlParameter("@ACTION", "RESETPASSWORD")
                       ).FirstOrDefault();

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
