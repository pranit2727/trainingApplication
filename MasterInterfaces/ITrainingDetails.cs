using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ViewModels;

namespace MasterInterfaces
{
    public interface ITrainingDetails
    {
        List<TrainingListVM> GetTrainingDetails();
        bool InsertTrainingDetails(TrainingVM trainer);
        bool UpdateTrainingDetails(int id, TrainingVM trainer);
        bool DeleteTrainingDetails(int id);
    }
}
