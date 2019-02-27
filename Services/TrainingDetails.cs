using MasterInterfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TrainingContextLayer;
using ViewModels;

namespace Services
{
    public class TrainingDetails: ITrainingDetails
    {
        //to retrieve the data to display list
        public List<TrainingListVM> GetTrainingDetails()
        {
            try
            {
                var trainingList = new List<TrainingListVM>();
                using (WebApiEntities dc = new WebApiEntities())
                {
                    var data = dc.Trainings.ToList();
                    foreach (var item in data)
                    {
                        TrainingListVM training = new TrainingListVM();
                        if (item.DeletedAt == null)
                        {
                            training.TrainingId = item.TrainingId;
                            training.EndTime = item.Schedule.EndTime.Value;
                            training.StartTime = item.Schedule.StartTime.Value;
                            training.Topic = item.Topic;
                            var name = string.Concat(item.User.FirstName + item.User.LastName);
                            training.TrainerName = name;
                            training.Description = item.Description;
                            training.RoomName = item.Schedule.RoomDetail.RoomName;
                            trainingList.Add(training);
                        }                        
                    }
                }
                return trainingList;
            }
            catch (Exception)
            {
                throw null;
            }
        }

        //to insert the data from create training form
        public bool InsertTrainingDetails(TrainingVM trainer)
        {
            try
            {
                bool status = false;
                using (WebApiEntities dc = new WebApiEntities())
                {
                    Schedule schedule = new Schedule();
                    schedule.StartTime = trainer.StartTime;
                    schedule.EndTime = trainer.EndTime;
                    schedule.RoomId = trainer.RoomId;
                    var obj = dc.Schedules.Add(schedule);
                    dc.SaveChanges();

                    Training traine = new Training();
                    traine.Topic = trainer.Topic;
                    traine.Description = trainer.Description;
                    traine.ScheduleId = obj.ScheduleId;
                    traine.UserId = trainer.UserId;
                    traine.CreatedAt = DateTime.Now;
                    traine.UpdatedAt = DateTime.Now;
                    dc.Trainings.Add(traine);
                    dc.SaveChanges();
                    status = true;
                }
                return status;
            }
            catch(Exception)
            {
                return false;
            }
        }

        //to update the data from edit training form
        public bool UpdateTrainingDetails(int id,TrainingVM trainer)
        {
            try
            {
                bool status = false;
                using (WebApiEntities dc = new WebApiEntities())
                {
                    var trainerEntity = dc.Trainings.FirstOrDefault(a => a.TrainingId == id);

                    if (trainerEntity == null)
                    {
                        return status;
                    }
                    else
                    {
                        //updating schedule for specific record 
                        var scheduleEntity = dc.Schedules.FirstOrDefault(a => a.ScheduleId == trainerEntity.ScheduleId);
                        scheduleEntity.StartTime = trainer.StartTime;
                        scheduleEntity.EndTime = trainer.EndTime;
                        scheduleEntity.RoomId = trainer.RoomId;
                        dc.SaveChanges();

                        //updating training for specific record
                        trainerEntity.Topic = trainer.Topic;
                        trainerEntity.Description = trainer.Description;
                        //for update schedule Id is not require cause it is already present in database.
                        //for update User Id is not require cause it is already present in database.
                        trainerEntity.UpdatedAt = DateTime.Now;
                        dc.SaveChanges();
                        status = true;

                        return status;
                    }
                }
            }
            catch (Exception)
            {
               return false;
            }
          
        }

        //to delete the specific training 
        public bool DeleteTrainingDetails(int id)
        {
            try
            {
                bool status = false;
                using (WebApiEntities dc = new WebApiEntities())
                {
                    var trainerEntity = dc.Trainings.FirstOrDefault(a => a.TrainingId == id);
                    if (trainerEntity == null)
                    {
                        return status;
                    }
                    else
                    {
                        var scheduleEntity = dc.Schedules.FirstOrDefault(a => a.ScheduleId == trainerEntity.ScheduleId);
                        dc.Schedules.Remove(scheduleEntity);
                        dc.SaveChanges();
                        //dc.Trainings.Remove(trainerEntity);
                        trainerEntity.DeletedAt = DateTime.Now;
                        dc.SaveChanges();
                        status = true;
                        return status;
                    }
                }
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}
