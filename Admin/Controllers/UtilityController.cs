using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Admin.Controllers
{
    public class UtilityController : AuthorizeController
    {
        // GET: Utility
        public ActionResult ContactUs()
        {
            return View();
        }
    }
}