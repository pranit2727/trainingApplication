using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ViewModels
{
    public class TrainingListVM
    {
        public int TrainingId { get; set; }
        public string Topic { get; set; }
        public string TrainerName { get; set; }
        public string Description { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public string RoomName { get; set; }
    }
}
