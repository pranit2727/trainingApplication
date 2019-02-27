using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ViewModels
{
    public class MeetingVM
    {
        public string MeetingName { set; get; }
        public string Agenda { set; get; }
        public int UserId { get; set; }
        public List<int> MeetingAttendeeID { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public int RoomId { get; set; }
    }
}
