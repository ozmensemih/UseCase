using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using UseCase.MVC.App_Start;

namespace UseCase.MVC.Controllers
{

    public class BaseController : Controller
    {

        private AuthBuilder authBuilder = new AuthBuilder();
        private UserLoginData userData;
        public UserLoginData UserData
        {
            get
            {
                if (userData == null)
                {
                    userData = authBuilder.GetUserData(HttpContext);

                }
                return userData;
            }
        }
    }
}
