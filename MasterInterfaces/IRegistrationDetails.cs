using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ViewModels;

namespace MasterInterfaces
{
    public interface IRegistrationDetails
    {
        bool InsertRegistrationDetails(UserCreadentialsVM userCreadentials);
        bool IsEmailExist(string emailID);
        bool SendVerificationLinkEmail(string emailID, string activationCode, string emailFor = "VerifyAccount", string controllerFor = "MailVerifications");
        bool VerifyAccount(string id);
    }
}
