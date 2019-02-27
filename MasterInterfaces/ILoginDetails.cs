using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TrainingContextLayer;
using ViewModels;

namespace MasterInterfaces
{
    public interface ILoginDetails
    {
        User GetUser(LoginVM login);
        string CreateJwtToken(User user);
    }
}
