using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MovieRental.Controllers
{
    public class RentalsController : Controller
    {
        [Authorize(Roles = Constants.RoleNames.CanManageMovies)]
        // GET: Rentals
        public ActionResult NewRental()
        {
            return View();
        }
    }
}