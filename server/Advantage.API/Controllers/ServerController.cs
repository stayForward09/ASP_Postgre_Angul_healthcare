using System;
using System.Linq;
using Microsoft.AspNetCore.Mvc;


namespace Advantage.API.Controllers
{

    [Route("api/[controller]")]
    public class ServerController : Controller
    {
        private readonly ApiContext _ctx;

        //constructor
        public ServerController(ApiContext ctx)
        {
            _ctx = ctx;
        }



        // GET api/server
        [HttpGet]
        public IActionResult Get()
        {
            var response = _ctx.Servers.OrderBy(s=>s.Id).ToList(); 
            return Ok(response);
        }//end get servers



        // GET api/server/5
        [HttpGet("{id}", Name ="GetServer")]
        public Server Get(int id)
        {
            return _ctx.Servers.Find(id);
        }//end get server by id



        // POST api/server
        [HttpPost]
        public IActionResult Post([FromBody] Server server)
        {
            if (server == null)
            {
                return BadRequest();
            }

            _ctx.Servers.Add(server);
            _ctx.SaveChanges();

            return CreatedAtRoute("GetServer", new { id = server.Id }, server);
        }//end post server



        //PUT Server API 
        [HttpPut("{id}")]
        public IActionResult Message(int id, [FromBody] ServerMessage msg)
        {

            var server = _ctx.Servers.FirstOrDefault(s => s.Id == id);

            if (server == null)
            {
                return NotFound();
            }

            // move update handling to a service, perhaps
            if(msg.Payload == "activate")
            {
                server.IsOnline = true;
                _ctx.SaveChanges();
            }

            if(msg.Payload == "deactivate")
            {
                server.IsOnline = false;
                _ctx.SaveChanges();
            }

            return new NoContentResult();
        }//end put server



        // DELETE api/server/5
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var server = _ctx.Servers.FirstOrDefault(t => t.Id == id);
            if (server == null)
            {
                return NotFound();
            }

            _ctx.Servers.Remove(server);
            _ctx.SaveChanges();
            return new NoContentResult();
        }//end delete server


    }//end class

}//end namespace
