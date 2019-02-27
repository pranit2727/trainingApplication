using System;
using Services;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using ViewModels;
using MasterInterfaces;

namespace TrainingApp.Controllers
{
    public class LoginsController : ApiController
    {
        ILoginDetails loginDetails = new LoginDetails();
        // GET: api/Logins
        [HttpPost]
        public HttpResponseMessage Post(LoginVM login)
        {
            try
            {
                var user = loginDetails.GetUser(login);
                if (user == null)
                {
                    return Request.CreateResponse(HttpStatusCode.Unauthorized, "Invalid User", Configuration.Formatters.JsonFormatter);
                }
                else
                {
                    string token = loginDetails.CreateJwtToken(user);
                    if (token != null)
                    {
                        return Request.CreateResponse(HttpStatusCode.OK,  token );
                    }
                    return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "token is not generated");
                }
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex);
            }
        }
    }
}