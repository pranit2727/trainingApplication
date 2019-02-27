using Services;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Web.Http;
using System.Linq;
using TrainingContextLayer;
using System.Net;
using TrainingApp.MyFilter;
using MasterInterfaces;

namespace TrainingApp.Controllers
{
    public class UsersController : ApiController
    {
        IUserDetails userDetails = new UserDetails();

        // GET: api/Users
        [HttpGet]
        [JwtTokenAuthentication]
        public HttpResponseMessage Get()
        {
            try
            {
                var users = userDetails.GetUsersDetails();
                return Request.CreateResponse(HttpStatusCode.OK,users);
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex);
            }
        }

        [HttpPost]
        //[JwtTokenAuthentication]
        public HttpResponseMessage Post(string emailId)
        {
            try
            {
                bool status=userDetails.SendMailForForgotPasswordLink(emailId);
                if (status)
                {
                    return Request.CreateResponse(HttpStatusCode.OK,"Link is sent to mail and reset your password via link");
                }
                else
                {
                    return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "Something went wrong");
                }
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex);
            }
        }
    }
}
