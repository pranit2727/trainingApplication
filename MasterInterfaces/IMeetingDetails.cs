using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ViewModels;

namespace MasterInterfaces
{
    public interface IMeetingDetails
    {
        List<MeetingListVM> GetMeetingDetails();
        bool InsertMeetingDetails(MeetingVM meeting);
        bool UpdateMeetingDetails(int id, MeetingVM meeting);
        bool DeleteMeetingDetails(int id);
    }
}
