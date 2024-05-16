using Microsoft.AspNetCore.Mvc;
using WebApplication1.BL;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WebApplication1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CoursesController : ControllerBase
    {
        Course course = new Course();
        // GET: api/<CoursesController>
        [HttpGet]
        public IEnumerable<Course> Get()
        {
            return course.Read();
        }

        // GET api/<CoursesController>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<CoursesController>
        [HttpPost]
        public bool Post([FromBody] Course value)
        {
            return value.Insert();
        }

        // PUT api/<CoursesController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        [HttpGet("search")]
        public IEnumerable<Course> GetByDurationRange(int fromDuration, int toDuration)
        {
            Course course = new Course();

            return course.GetByDurationRange(fromDuration, toDuration);
        }

        [HttpGet("searchByRouting/fromRating/{fromRating}/toRating/{toRating}")]
        public IEnumerable<Course> GetByRatingRange(int fromRating, int toRating)
        {
            Course course = new Course();

            return course.GetByRatingRange(fromRating, toRating);
        }

        // DELETE api/<CoursesController>/5
        [HttpDelete("{id}")]
        public IActionResult DeleteById(int id) // need to fix the exception
        {
            try
            {
                Course course = new Course();
                course.DeleteById(id);
                return Ok("Course Deleted Successfully");
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message); // Return 500 Internal Server Error with the exception message
            }
        }
    }
}
