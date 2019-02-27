using System;
using Services;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using TrainingApp.MyFilter;
using MasterInterfaces;
using ViewModels;

namespace TrainingApp.Controllers
{
    public class RoomsController : ApiController
    {
        IRoomDetails roomDetails = new RoomDetails();

        // GET: api/Rooms/
        [HttpPost]
        [JwtTokenAuthentication]
        public HttpResponseMessage Post([FromBody]TimeSlots times)
        {
            try
            {
                var roomList = roomDetails.GetRoomsByTime(times);
                if (roomList != null)
                {
                    return Request.CreateResponse(HttpStatusCode.OK, roomList);
                }
                else
                {
                    return Request.CreateErrorResponse(HttpStatusCode.NotFound, "room is not available at this time slot");
                }
            }
            catch(Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex);
            }
        }
    }
}
