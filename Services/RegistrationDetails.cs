using MasterInterfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using TrainingContextLayer;
using ViewModels;

namespace Services
{
    public class RegistrationDetails: IRegistrationDetails
    {
        
        public bool InsertRegistrationDetails(UserCreadentialsVM userCreadentials)
        {
            try
            {
                bool status = false;
                #region//Save Data to Database
                using (WebApiEntities dc = new WebApiEntities())
                {
                    User user = new User();
                    user.FirstName = userCreadentials.FirstName;
                    user.LastName = userCreadentials.LastName;
                    user.IsActive = true;
                    user.RoleId = 1;
                    user.CreatedAt = DateTime.Now;
                    user.UpdatedAt = DateTime.Now;
                    var obj = dc.Users.Add(user);
                    dc.SaveChanges();

                    #region//Taking data fromviewmodel and assigning to database entity
                    UserCredential creadentials = new UserCredential();
                    creadentials.Email = userCreadentials.Email;

                    #region//Generate Activation
                    creadentials.ActivationCode = Guid.NewGuid();
                    #endregion

                    #region//Password Hashing
                    creadentials.Password = Crypto.Hash(userCreadentials.Password);
                    #endregion

                    creadentials.IsActivated = true;
                    creadentials.IsEmailVerified = false;
                    creadentials.UserId = obj.UserId;
                    dc.UserCredentials.Add(creadentials);
                    dc.SaveChanges();
                    #endregion

                    #region//Send Email to User
                    SendVerificationLinkEmail(userCreadentials.Email, creadentials.ActivationCode.ToString());
                    #endregion
                    status = true;
                }
                #endregion
                return status;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public bool IsEmailExist(string emailID)
        {
            try
            {
                using (WebApiEntities dc = new WebApiEntities())
                {
                    var result = dc.UserCredentials.Where(a => a.Email == emailID).FirstOrDefault();
                    return result == null ? false : true;
                }
            }
            catch (Exception)
            {
                return false;
            }
        }

        public bool SendVerificationLinkEmail(string emailID, string activationCode,string emailFor= "VerifyAccount",string controllerFor= "MailVerifications")
        {
            try
            {
                var verifyUrl = "/"+ controllerFor + "/"+emailFor+"/" + activationCode;
                var link = HttpContext.Current.Request.Url.AbsoluteUri.Replace(HttpContext.Current.Request.Url.PathAndQuery, verifyUrl);

                var fromEmail = new MailAddress("pranit.jagtap2020@gmail.com", "Dotnet Awesome");
                var toEmail = new MailAddress(emailID);
                var fromEmailPassword = "20pranit"; // Replace with actual password

                string subject = "";
                string body = "";
                if (emailFor== "VerifyAccount")
                {
                     subject = "Your account is successfully created!";
                     body = "<br/><br/>We are excited to tell you that your Dotnet Awesome account is" +
                        " successfully created. Please click on the below link to verify your account" +
                        " <br/><br/><a href='" + link + "'>" + link + "</a> ";
                }
                else if (emailFor == "VerifyPassword")
                {
                    subject = "Reset Password";
                    body = "<br/><br/>Click here to reset " +" password" +" <br/><br/><a href='" + link + "'>" + link + "</a> ";
                }

                var smtp = new SmtpClient
                {
                    Host = "smtp.gmail.com",
                    Port = 587,
                    EnableSsl = true,
                    DeliveryMethod = SmtpDeliveryMethod.Network,
                    UseDefaultCredentials = false,
                    Credentials = new NetworkCredential(fromEmail.Address, fromEmailPassword)
                };

                using (var messag = new MailMessage(fromEmail, toEmail)
                {
                    Subject = subject,
                    Body = body
                })smtp.Send(messag);

                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public bool VerifyAccount(string id)
        {
            try
            {
                using (WebApiEntities dc = new WebApiEntities())
                {
                    dc.Configuration.ValidateOnSaveEnabled = false;
                    var compare = dc.UserCredentials.Where(a => a.ActivationCode == new Guid(id)).FirstOrDefault();
                    if (compare != null)
                    {
                        compare.IsEmailVerified = true;
                        dc.SaveChanges();
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
            }
            catch (Exception )
            {
                return false;
            }
        }

    }
}
