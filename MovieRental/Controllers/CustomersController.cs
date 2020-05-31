using MovieRental.Models;
using MovieRental.ViewModels;
using System;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure.MappingViews;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MovieRental.Controllers
{
    public class CustomersController : Controller
    {
        private MovieRentalDbContext _context;

        public CustomersController()
        {
            _context = new MovieRentalDbContext();
        }

        // GET: Customers
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Details(int id)
        {
            var customer = _context.Customers.Include(nameof(Customer.MembershipType)).SingleOrDefault(obj => obj.Id == id);
            if (customer == null)
                return HttpNotFound();

            return View(customer);
        }
              
        [Authorize(Roles = Constants.RoleNames.CanManageMovies)]
        public ActionResult New()
        {
            var membershipTypes = _context.MembershipTypes.ToList();
            var viewModel = new CustomerFormViewModel()
            {                
                MembershipTypes = membershipTypes
            };

            return View("AddEditForm", viewModel);
        }
              
        [Authorize(Roles = Constants.RoleNames.CanManageMovies)]
        public ActionResult Edit(int id)
        {
            var customer = _context.Customers.SingleOrDefault(c => c.Id == id);
            if (customer == null)
                return HttpNotFound();

            var membershipTypes = _context.MembershipTypes.ToList();
            var viewModel = new CustomerFormViewModel(customer)
            {              
                MembershipTypes = membershipTypes
            };

            //ovveride naming convention for view with existing view name
            return View("AddEditForm", viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]     
        [Authorize(Roles = Constants.RoleNames.CanManageMovies)]
        public ActionResult Save(Customer customer)
        {
            if (!ModelState.IsValid)
            {
                var membershipTypes = _context.MembershipTypes.ToList();
                var viewModel = new CustomerFormViewModel(customer)
                {                    
                    MembershipTypes = membershipTypes
                };
                             
                return View("AddEditForm", viewModel);
            }

            if (customer.Id == 0)
                _context.Customers.Add(customer);
            else
            {
                var customerInDb = _context.Customers.Single(c => c.Id == customer.Id);
                //Using this method to update the complete object is not suggested because of security issues
                //There may be some properties which are not present in the form or should not be updated
                //but if someone pass the object with those values, then because of using this method, all
                //properties will be updated
                //TryUpdateModel(customerInDb);

                customerInDb.BirthDate = customer.BirthDate;
                customerInDb.IsSubscribedToNewsLetter = customer.IsSubscribedToNewsLetter;
                customerInDb.MembershipTypeId = customer.MembershipTypeId;
                customerInDb.Name = customer.Name;
            }

            _context.SaveChanges();
            return RedirectToAction(nameof(Index), "Customers");
        }

        protected override void Dispose(bool disposing)
        {
            _context.Dispose();
        }
    }
}