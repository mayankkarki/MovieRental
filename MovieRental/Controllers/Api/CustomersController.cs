using AutoMapper;
using MovieRental.Dtos;
using MovieRental.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.WebSockets;

namespace MovieRental.Controllers.Api
{
    public class CustomersController : ApiController
    {
        private MovieRentalDbContext _context;
        private Mapper _mapperInstance;

        public CustomersController()
        {
            _context = new MovieRentalDbContext();
            _mapperInstance = new Mapper(MvcApplication.AutoMapperConfiguation);
        }

        //Get api/customers
        public IHttpActionResult GetCustomers(string query = null)
        {
            IEnumerable<Customer> customers = _context.Customers
                .Include(nameof(Customer.MembershipType));

            if (!string.IsNullOrWhiteSpace(query))
                customers = customers.Where(c => c.Name.ToLower().Contains(query.ToLower()));

            var filteredCustomers = customers.ToList()
            .Select(_mapperInstance.Map<Customer, CustomerDto>);
            return Ok(filteredCustomers);
        }

        //Get api/customers/1
        public IHttpActionResult GetCustomer(int id)
        {
            var customer = _context.Customers.SingleOrDefault(c => c.Id == id);
            if (customer == null)
                return NotFound();

            return Ok(_mapperInstance.Map<Customer, CustomerDto>(customer));
        }

        [Authorize(Roles = Constants.RoleNames.CanManageMovies)]
        //Post api/customers
        [HttpPost]
        public IHttpActionResult CreateCustomer(CustomerDto customerDto)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            var customer = _mapperInstance.Map<CustomerDto, Customer>(customerDto);
            _context.Customers.Add(customer);
            _context.SaveChanges();

            customerDto.Id = customer.Id;
            return Created(new Uri(Request.RequestUri + "/" + customer.Id), customerDto);
        }

        [Authorize(Roles = Constants.RoleNames.CanManageMovies)]
        //Put api/customers/1
        [HttpPut]
        public IHttpActionResult UpdateCustomer(int id, CustomerDto customerDto)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            var customerInDb = _context.Customers.SingleOrDefault(c => c.Id == id);
            if (customerInDb == null)
                return NotFound();

            _mapperInstance.Map(customerDto, customerInDb);
            _context.SaveChanges();

            return Ok();
        }

        [Authorize(Roles = Constants.RoleNames.CanManageMovies)]
        //Delete api/customers/1
        [HttpDelete]
        public IHttpActionResult DeleteCustomer(int id)
        {
            var customerInDb = _context.Customers.SingleOrDefault(c => c.Id == id);
            if (customerInDb == null)
                return NotFound();

            _context.Customers.Remove(customerInDb);
            _context.SaveChanges();
            return Ok();
        }
    }
}
