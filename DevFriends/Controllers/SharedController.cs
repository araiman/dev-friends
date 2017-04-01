using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DevFriends.Controllers
{
    public class SharedController : Controller
    {
        // GET: Common
        public ActionResult Error()
        {
            return View();
        }
    }
}