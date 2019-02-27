using Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TrainingContextLayer;
using ViewModels;

namespace TrainingApp.Controllers
{
    public class MailVerificationsController : Controller
    {
        RegistrationDetails registrationDetails = new RegistrationDetails();
        // GET: MailVerifications

        [HttpGet]
        public ActionResult VerifyAccount(string id)
        {
            try
            {
                bool stat = false;
                #region//email verification against the activation code(id)
                bool status = registrationDetails.VerifyAccount(id);
                #endregion

                if (status)
                {
                    stat = true;
                    ViewBag.Status = stat;
                    return View();
                }
                else
                {
                    return View();
                }
                
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        // GET: MailVerifications/Passwd Verification/5
        [HttpGet]
        public ActionResult VerifyPassword(string id)
        {
            try
            {
                bool stat = false;
                #region//forgot password verification against the activation code(id)
                bool status = registrationDetails.VerifyAccount(id);
                #endregion

                if (status)
                {
                    ResetPasswordVM resetPassword = new ResetPasswordVM();
                    resetPassword.ActivationCode = new Guid(id);
                    stat = true;
                    ViewBag.Status = stat;
                    return View(resetPassword);
                }
                else
                {
                    return View();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        public ActionResult VerifyPassword(ResetPasswordVM reset)
        {
            try
            {
                var message = "";
                #region// reseting the password
                using (WebApiEntities dc = new WebApiEntities())
                {
                    var user = dc.UserCredentials.Where(a => a.ActivationCode.ToString() == reset.ActivationCode.ToString()).FirstOrDefault();
                    if (user != null)
                    {
                        if (reset.NewPassword == reset.ConfirmPassword)
                        {
                            user.Password = Crypto.Hash(reset.NewPassword);
                            dc.SaveChanges();
                            message = "new password updated successfully go and login";
                        }
                        else
                        {
                            message = "password and confirm password does not match";
                        }
                    }
                    else
                    {
                        message = "something went wrong";
                    }
                }
                #endregion

                ViewBag.Message = message;
                return View(reset);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
