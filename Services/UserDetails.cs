using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TrainingContextLayer;
using ViewModels;
using MasterInterfaces;
namespace Services
{
    public class UserDetails : IUserDetails
    {
        public List<Users> GetUsersDetails()
        {
            try
            {
                var userList = new List<Users>();
                using (WebApiEntities dc = new WebApiEntities())
                {
                    //var data = dc1.Schedules.ToList();
                    var data = dc.Users.ToList();
                    foreach (var item in data)
                    {
                        Users user = new Users();
                        if (item.DeletedAt == null)
                        {
                            var name = string.Concat(item.FirstName + " " + item.LastName);
                            user.UserName = name;
                            user.UserId = item.UserId;
                            userList.Add(user);
                        }
                    }
                }
                return userList;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public bool SendMailForForgotPasswordLink(string emailId)
        {
            try
            {
                IRegistrationDetails details = new RegistrationDetails();
                using (WebApiEntities dc = new WebApiEntities())
                {
                    var account = dc.UserCredentials.Where(a => a.Email == emailId).FirstOrDefault();
                    if (account != null && account.IsEmailVerified == true)
                    {
                        //send mail to user
                        bool status = details.SendVerificationLinkEmail(account.Email, account.ActivationCode.ToString(), "VerifyPassword");
                        if (status)
                        {
                            dc.Configuration.ValidateOnSaveEnabled = false;
                            return true;
                        }
                        else
                        {
                            return false;
                        }
                    }
                    else
                    {
                        return false;
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
