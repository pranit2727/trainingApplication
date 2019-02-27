using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ViewModels;

namespace MasterInterfaces
{
    public interface IRoomDetails
    {
        List<RoomVM> GetRoomsByTime(TimeSlots times);
    }
}
