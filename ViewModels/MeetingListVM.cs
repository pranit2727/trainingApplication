using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ViewModels
{
    public class MeetingListVM
    {
        public int MeetingId { get; set; }
        public string MeetingName { set; get; }
        public string OrganiserName { set; get; }
        public string Agenda { set; get; }
        public List<Attendee> AttendeeList { set; get; }
        public DateTime StartTime { set; get; }
        public DateTime EndTime { set; get; }
        public string RoomName { set; get; }
    }
}
