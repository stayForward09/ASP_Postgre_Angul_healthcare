using System;
using System.Linq;
using Microsoft.AspNetCore.Mvc;


namespace Advantage.API.Controllers
{

    [Route("api/[controller]")]
    public class CustomerController : Controller
    {
        private readonly ApiContext _ctx;

        public CustomerController(ApiContext ctx)
        {
            _ctx = ctx;
        }



        // GET API
        [HttpGet]
        public IActionResult Get()
        {
            var data = _ctx.Customers.OrderBy(c => c.Id);

            return Ok(data);
        }//end GET



        // GET api/customer/pageNumber/pageSize
        [HttpGet("{pageIndex:int}/{pageSize:int}")]
        public IActionResult Get(int pageIndex, int pageSize)
        {
            var data = _ctx.Customers.OrderBy(c => c.Id);
            var page = new PaginatedResponse<Customer>(data, pageIndex, pageSize);
            var totalCount = data.Count();
            var totalPages = Math.Ceiling((double)totalCount / pageSize);

            var response = new
            {
                Page = page,
                TotalPages = totalPages
            };

            return Ok(response);
        }//end get by pagesize



        // GET api/values/5
        [HttpGet("{id}", Name = "GetCustomer")]
        public IActionResult Get(int id)
        {
            var customer = _ctx.Customers.Find(id);
            return Ok(customer);
        }//end get by size



        // POST api/values
        [HttpPost]
        public IActionResult Post([FromBody] Customer customer)
        {
            if (customer == null)
            {
                return BadRequest();
            }

            _ctx.Customers.Add(customer);
            _ctx.SaveChanges();

            return CreatedAtRoute("GetCustomer", new { id = customer.Id }, customer);
        }//end post



        // PUT api/values/5
        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody] Customer customer)
        {
            if (customer == null || customer.Id != id)
            {
                return BadRequest();
            }

            var updatedCustomer = _ctx.Customers.FirstOrDefault(c => c.Id == id);

            if (updatedCustomer == null)
            {
                return NotFound();
            }

            updatedCustomer.Email = customer.Email;
            updatedCustomer.Name = customer.Name;
            updatedCustomer.State = customer.State;

            _ctx.SaveChanges();
            return new NoContentResult();
        }//end put




        // DELETE api/values/5
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var customer = _ctx.Customers.FirstOrDefault(t => t.Id == id);
            if (customer == null)
            {
                return NotFound();
            }

            _ctx.Customers.Remove(customer);
            _ctx.SaveChanges();
            return new NoContentResult();
        }//end delete



    }//end class

}//end namespace