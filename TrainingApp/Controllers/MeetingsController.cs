using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using ViewModels;
using Services;
using TrainingApp.MyFilter;
using System.IdentityModel.Claims;
using MasterInterfaces;

namespace TrainingApp.Controllers
{
    public class MeetingsController : ApiController
    {
        IMeetingDetails meetingDetails = new MeetingDetails();

        // GET: api/Meetings
        [HttpGet]
        [JwtTokenAuthentication]
        public HttpResponseMessage Get()
        {
            try
            {
                var data = meetingDetails.GetMeetingDetails();
                if (data != null)
                {
                    return Request.CreateResponse(HttpStatusCode.OK, data);
                }
                else
                {
                    return Request.CreateErrorResponse(HttpStatusCode.NotFound,"There are no meetings scheduled yet");
                }
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex);
            }
        }

        // GET: api/Meetings/5
        [HttpGet]
        [JwtTokenAuthentication]
        public HttpResponseMessage Get(int id)
        {
            try
            {
                var result = meetingDetails.GetMeetingDetails().Where(a => a.MeetingId == id).FirstOrDefault();
                if (result != null)
                {
                    return Request.CreateResponse(HttpStatusCode.OK, result);
                }
                else
                {
                    return Request.CreateErrorResponse(HttpStatusCode.NotFound, "There is no meeting of ID= " + id + "  scheduled yet");
                }
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex);
            }
        }

        // POST: api/Meetings
        [HttpPost]
        [JwtTokenAuthentication]
        public HttpResponseMessage Post(MeetingVM meeting)
        {
            try
            {
                int userId = int.Parse((GetClaims.GetClaimsTypes(ActionContext.Request.Headers.Authorization.Parameter)).FindFirst(ClaimTypes.NameIdentifier).Value);
                meeting.UserId = userId;
                bool status = meetingDetails.InsertMeetingDetails(meeting);
                if (status)
                {
                    var message = Request.CreateResponse(HttpStatusCode.Created, "New meeting is created successfully");
                    return message;
                }
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "cannot insert meeting details(check the data)");
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex);
            }
        }

        // PUT: api/Meetings/5
        [HttpPut]
        [JwtTokenAuthentication]
        public HttpResponseMessage Put(int id,MeetingVM meeting)
        {
            try
            {
                bool status = meetingDetails.UpdateMeetingDetails(id, meeting);
                if (status)
                {
                    return Request.CreateResponse(HttpStatusCode.OK, "The meeting is updated successfully");
                }
                else
                {
                    return Request.CreateErrorResponse(HttpStatusCode.NotFound, "Meeting's ID  = " + id.ToString() + "  not found to update");
                }
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex);
            }
        }

        // DELETE: api/Meetings/5
        [HttpDelete]
        [JwtTokenAuthentication]
        public HttpResponseMessage Delete(int id)
        {
            try
            {
                bool status = meetingDetails.DeleteMeetingDetails(id);
                if (status)
                {
                    return Request.CreateResponse(HttpStatusCode.OK, "Item Deleted Successfully");
                }
                else
                {
                    return Request.CreateErrorResponse(HttpStatusCode.NotFound, "Meeting's ID  = " + id.ToString() + "  not found to delete");
                }
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex);
            }
        }
    }
}
