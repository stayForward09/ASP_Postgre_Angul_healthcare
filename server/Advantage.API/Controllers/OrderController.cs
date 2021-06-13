using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Advantage.API.Controllers
{
    
    [Route("api/[controller]")]
    public class OrderController : Controller
    {
        private readonly ApiContext _ctx;

        public OrderController(ApiContext ctx)
        {
            _ctx = ctx;
        }

        // GET api/order/pageNumber/pageSize
        [HttpGet("{pageIndex:int}/{pageSize:int}")]
        public IActionResult Get(int pageIndex, int pageSize)
        {
            var data = _ctx.Orders.Include(o => o.Customer).OrderByDescending(c => c.Placed);
            var page = new PaginatedResponse<Order>(data, pageIndex, pageSize);

            var totalCount = data.Count();
            var totalPages = Math.Ceiling((double)totalCount / pageSize);

            var response = new
            {
                Page = page,
                TotalPages = totalPages
            };

            return Ok(response);
        }

        [HttpGet("bystate")]
        public IActionResult ByState()
        {
            // enumerated group due to ef error
            // https://github.com/aspnet/EntityFrameworkCore/issues/9551

            var orders = _ctx.Orders.Include(o => o.Customer).ToList();
            var groupedResult = orders
                .GroupBy(r => r.Customer.State)
                .ToList()
                .Select(grp => new 
                {
                    State = grp.Key,
                    Total = grp.Sum(x => x.Total)
                }).OrderByDescending(r => r.Total)
                .ToList();

            return Ok(groupedResult);
        }

        [HttpGet("bycustomer/{n}")]
        public IActionResult ByCustomer(int n)
        {
            // enumerated group due to ef error
            // https://github.com/aspnet/EntityFrameworkCore/issues/9551

            var orders = _ctx.Orders.Include(o => o.Customer).ToList();
            var groupedResult = orders
                .GroupBy(r => r.Customer.Id)
                .ToList()
                .Select(grp => new
                {
                    Name = _ctx.Customers.Find(grp.Key).Name,
                    Total = grp.Sum(x => x.Total)
                }).OrderByDescending(r => r.Total)
                .Take(n)
                .ToList();

            return Ok(groupedResult);
        }

        // GET api/order/5
        [HttpGet("getorder/{id}", Name ="GetOrder")]
        public Order GetOrder(int id)
        {
            return _ctx.Orders.Include(o => o.Customer)
                .First(o => o.Id == id);
        }

        // POST api/order
        [HttpPost]
        public IActionResult Post([FromBody] Order order)
        {
            if (order == null)
            {
                return BadRequest();
            }

            _ctx.Orders.Add(order);
            _ctx.SaveChanges();

            return CreatedAtRoute("GetOrder", new { id = order.Id }, order);
        }

        // PUT api/order/5
        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody] Order order)
        {
            if (order == null || order.Id != id)
            {
                return BadRequest();
            }

            var updatedOrder = _ctx.Orders.FirstOrDefault(c => c.Id == id);

            if (updatedOrder == null)
            {
                return NotFound();
            }

            updatedOrder.Customer = order.Customer;
            updatedOrder.Completed = order.Completed;
            updatedOrder.Total = order.Total;
            updatedOrder.Placed = order.Placed;

            _ctx.SaveChanges();
            return new NoContentResult();
        }

        // DELETE api/order/5
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var order = _ctx.Orders.FirstOrDefault(t => t.Id == id);
            if (order == null)
            {
                return NotFound();
            }

            _ctx.Orders.Remove(order);
            _ctx.SaveChanges();
            return new NoContentResult();
        }
    }
}
