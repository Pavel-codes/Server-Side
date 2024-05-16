using Microsoft.AspNetCore.Mvc;
using WebApplication1.BL;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WebApplication1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class InstructorsController : ControllerBase
    {
        Instructor instructor = new Instructor();
        // GET: api/<InstructorsController>
        [HttpGet]
        public IEnumerable<Instructor> Get()
        {
            return instructor.Read();
        }

        // GET api/<InstructorsController>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<InstructorsController>
        [HttpPost]
        public bool Post([FromBody] Instructor value)
        {
            return value.Insert();
        }

        // PUT api/<InstructorsController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<InstructorsController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
