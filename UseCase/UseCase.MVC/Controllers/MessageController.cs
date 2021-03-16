using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;

namespace UseCase.MVC.Controllers
{
    [Authorize]
    public class MessageController : BaseController
    {
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult Success()
        {
            return View();
        }

        public IActionResult Error()
        {
            return View();
        }

    }
}
