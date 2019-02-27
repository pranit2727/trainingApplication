using System;
using System.Net;
using System.Net.Http;
using Services;
using System.Web.Http;
using ViewModels;
using MasterInterfaces;

namespace TrainingApp.Controllers
{
    public class RegistrationsController : ApiController
    {
        IRegistrationDetails registrationDetails = new RegistrationDetails();

        // POST: api/Registrations
        [HttpPost]
        public HttpResponseMessage Post(UserCreadentialsVM userCreadentials)
        {
            try
            {
                #region //Email already exists
                var isExists = registrationDetails.IsEmailExist(userCreadentials.Email);
                if (isExists)
                {
                    return Request.CreateErrorResponse(HttpStatusCode.Ambiguous, "Email already Exists in please change  email"); ;
                }
                #endregion

                #region//Save Data to Database
                bool status = registrationDetails.InsertRegistrationDetails(userCreadentials);
                #endregion
                if (status)
                {
                    return Request.CreateResponse(HttpStatusCode.OK, "Registration successfull please verify through email");
                }
                else
                {
                    return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "Registration unsuccessfull");
                }
            }
            catch(Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex);
            }
        }
    }
}
