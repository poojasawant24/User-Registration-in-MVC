using MVC_Form.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MVC_Form.Controllers
{
    public class UserDasboardController : Controller
    {
        public ActionResult GetAllUsers()
        {
            UserRepository userRepository = new UserRepository(); 
            var users = userRepository.GetAllUsers();
            return View(users);
        }

    }
}